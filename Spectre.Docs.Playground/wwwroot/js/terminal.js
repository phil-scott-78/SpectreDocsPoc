// Log immediately when module is parsed
console.log('[terminal.js] Module loading started');

// Use dynamic imports to prevent WASM from blocking page load
// The ghostty-web module will only be loaded when the terminal is first initialized
let ghosttyModule = null;
let ghosttyInitialized = false;
let initPromise = null;
let initError = null;

// Helper to yield to the event loop, preventing main thread blocking
function yieldToEventLoop() {
    return new Promise(resolve => setTimeout(resolve, 0));
}

async function ensureInitialized() {
    // Fast path - already initialized
    if (ghosttyInitialized) return ghosttyModule;

    // If we already failed, throw the cached error
    if (initError) throw initError;

    // If initialization is in progress, wait for it
    if (initPromise) {
        await initPromise;
        return ghosttyModule;
    }

    // Start initialization with timeout, yielding to event loop first
    initPromise = (async () => {
        // Yield first to allow the UI to render
        await yieldToEventLoop();

        const timeoutMs = 30000; // 30 second timeout for module load + WASM init
        const timeoutPromise = new Promise((_, reject) => {
            setTimeout(() => reject(new Error('Ghostty WASM initialization timed out after 30 seconds')), timeoutMs);
        });

        try {
            // Yield again before heavy module load
            await yieldToEventLoop();

            // Dynamically import ghostty-web - this defers WASM loading until now
            console.log('Loading ghostty-web module...');
            const loadModule = async () => {
                const module = await import('https://cdn.jsdelivr.net/npm/ghostty-web@0.4.0/dist/ghostty-web.js');
                await yieldToEventLoop();
                console.log('Initializing ghostty WASM...');
                await module.init();
                return module;
            };

            ghosttyModule = await Promise.race([loadModule(), timeoutPromise]);
            ghosttyInitialized = true;
            console.log('Ghostty initialized successfully');
            return ghosttyModule;
        } catch (e) {
            console.error('Ghostty initialization failed:', e);
            initError = e;
            initPromise = null; // Allow retry on next page load
            throw e;
        }
    })();

    await initPromise;
    return ghosttyModule;
}

console.log('[terminal.js] Setting up terminalInterop...');

window.terminalInterop = {
    terminals: new Map(),

    // Input queue processor - ensures keystrokes are sent sequentially and not dropped
    _createInputProcessor: function(dotNetRef) {
        const queue = [];
        let processing = false;

        async function processQueue() {
            if (processing || queue.length === 0) return;
            processing = true;

            while (queue.length > 0) {
                const item = queue.shift();
                try {
                    if (item.type === 'input') {
                        await dotNetRef.invokeMethodAsync('OnTerminalInput', item.data);
                    } else if (item.type === 'key') {
                        await dotNetRef.invokeMethodAsync('OnTerminalKey', item.data);
                    }
                } catch (e) {
                    console.warn('Input processing error:', e.message);
                    // Continue processing remaining items even if one fails
                }
            }

            processing = false;
        }

        return {
            enqueueInput: function(data) {
                queue.push({ type: 'input', data });
                processQueue();
            },
            enqueueKey: function(data) {
                queue.push({ type: 'key', data });
                processQueue();
            }
        };
    },

    init: async function (elementId, dotNetRef) {
        console.log('[terminal.js] init() called for', elementId);
        const container = document.getElementById(elementId);
        if (!container) {
            console.error('Terminal container not found:', elementId);
            return null;
        }

        // Ensure ghostty WASM is initialized with error handling
        let module;
        try {
            module = await ensureInitialized();
        } catch (e) {
            console.error('Failed to initialize ghostty WASM:', e);
            // Show error message in container instead of hanging
            container.innerHTML = `<div style="color: #f44747; padding: 16px; font-family: monospace;">
                Terminal initialization failed: ${e.message}<br>
                Please refresh the page to try again.
            </div>`;
            return null;
        }

        // Get Terminal and FitAddon from the dynamically loaded module
        const { Terminal, FitAddon } = module;

        // Generate terminal ID early so we can return it
        const terminalId = 'term_' + Date.now();

        // Create terminal with a timeout to prevent indefinite blocking
        const createTerminal = () => {
            return new Promise((resolve, reject) => {
                const timeout = setTimeout(() => {
                    reject(new Error('Terminal creation timed out'));
                }, 10000);

                try {
                    const term = new Terminal({
                        cursorBlink: true,
                        fontSize: 14,
                        fontFamily: '"Cascadia Code", "Fira Code", Consolas, monospace',
                        theme: {
                            // One Dark theme
                            background: '#1e1e1e',
                            foreground: '#abb2bf',
                            cursor: '#d4d4d4',
                            cursorAccent: '#1e1e1e',
                            black: '#282c34',
                            red: '#e06c75',
                            green: '#98c379',
                            yellow: '#e5c07b',
                            blue: '#61afef',
                            magenta: '#c678dd',
                            cyan: '#56b6c2',
                            white: '#abb2bf',
                            brightBlack: '#5c6370',
                            brightRed: '#e06c75',
                            brightGreen: '#98c379',
                            brightYellow: '#e5c07b',
                            brightBlue: '#61afef',
                            brightMagenta: '#c678dd',
                            brightCyan: '#56b6c2',
                            brightWhite: '#ffffff'
                        },
                        scrollback: 1000
                    });

                    const fitAddon = new FitAddon();
                    term.loadAddon(fitAddon);

                    // Open in a deferred manner to prevent blocking
                    requestAnimationFrame(() => {
                        try {
                            term.open(container);

                            // Defer fit to allow layout to stabilize
                            setTimeout(() => {
                                try {
                                    fitAddon.fit();
                                    // Only observe resize after initial fit is complete
                                    fitAddon.observeResize();
                                } catch (e) {
                                    console.warn('FitAddon error:', e);
                                }
                            }, 100);

                            clearTimeout(timeout);
                            resolve({ term, fitAddon });
                        } catch (e) {
                            clearTimeout(timeout);
                            reject(e);
                        }
                    });
                } catch (e) {
                    clearTimeout(timeout);
                    reject(e);
                }
            });
        };

        try {
            const { term, fitAddon } = await createTerminal();

            // Debounced resize handler
            let resizeTimeout = null;
            const debouncedFit = () => {
                if (resizeTimeout) clearTimeout(resizeTimeout);
                resizeTimeout = setTimeout(() => {
                    try {
                        fitAddon.fit();
                    } catch (e) {
                        console.warn('Resize fit error:', e);
                    }
                }, 50);
            };

            window.addEventListener('resize', debouncedFit);

            // Create input processor for this terminal to ensure keystrokes are queued and processed in order
            const inputProcessor = this._createInputProcessor(dotNetRef);

            // Handle keyboard input - for regular characters only
            term.onData(data => {
                // Skip anything that looks like an escape sequence or control character
                if (data.startsWith('\x1b') ||
                    data === '\r' || data === '\n' ||
                    data === '\b' || data === '\x7f' ||
                    data === '\t') {
                    return;
                }

                inputProcessor.enqueueInput(data);
            });

            // Handle special keys only - regular characters are handled by onData
            // This prevents double-firing for keys like space that trigger both events
            term.onKey(e => {
                const domEvent = e.domEvent || {};
                const code = domEvent.code || '';

                // Only process special keys, not regular characters
                // Regular characters (letters, numbers, space, punctuation) are handled by onData
                const isSpecialKey = code.startsWith('Arrow') ||
                    code === 'Enter' || code === 'NumpadEnter' ||
                    code === 'Backspace' || code === 'Delete' ||
                    code === 'Tab' || code === 'Escape' ||
                    code === 'Home' || code === 'End' ||
                    code === 'PageUp' || code === 'PageDown' ||
                    code === 'Insert' ||
                    e.key.startsWith('\x1b'); // Escape sequences from terminal

                if (!isSpecialKey) {
                    return;
                }

                inputProcessor.enqueueKey({
                    key: e.key,
                    domEvent: {
                        key: domEvent.key || '',
                        code: code,
                        ctrlKey: domEvent.ctrlKey || false,
                        altKey: domEvent.altKey || false,
                        shiftKey: domEvent.shiftKey || false
                    }
                });
            });

            this.terminals.set(terminalId, { term, fitAddon, inputProcessor, ready: true });
            return terminalId;
        } catch (e) {
            console.error('Failed to create terminal:', e);
            container.innerHTML = `<div style="color: #f44747; padding: 16px; font-family: monospace;">
                Terminal creation failed: ${e.message}<br>
                Please refresh the page to try again.
            </div>`;
            return null;
        }
    },

    write: function (terminalId, text) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            try {
                // Convert standalone \n to \r\n for proper terminal line breaks
                // First normalize \r\n to \n, then convert all \n to \r\n
                const normalizedText = text.replace(/\r\n/g, '\n').replace(/\n/g, '\r\n');
                entry.term.write(normalizedText);
            } catch (e) {
                console.warn('Terminal write error:', e.message);
            }
        }
    },

    writeLine: function (terminalId, text) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            try {
                entry.term.writeln(text);
            } catch (e) {
                console.warn('Terminal writeln error (ignored):', e.message);
            }
        }
    },

    clear: function (terminalId) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.term.clear();
            entry.term.reset();
        }
    },

    focus: function (terminalId) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.term.focus();
        }
    },

    fit: function (terminalId) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.fitAddon.fit();
        }
    },

    getSize: function (terminalId) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            return {
                cols: entry.term.cols,
                rows: entry.term.rows
            };
        }
        return { cols: 80, rows: 24 };
    },

    setCursorPosition: function (terminalId, x, y) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            // ANSI escape sequence to move cursor
            entry.term.write(`\x1b[${y + 1};${x + 1}H`);
        }
    },

    dispose: function (terminalId) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.term.dispose();
            this.terminals.delete(terminalId);
        }
    }
};

console.log('[terminal.js] Module fully loaded, terminalInterop ready');
