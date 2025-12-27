import { init, Terminal, FitAddon } from 'https://cdn.jsdelivr.net/npm/ghostty-web@0.4.0/dist/ghostty-web.js';

// Initialize ghostty WASM
let ghosttyInitialized = false;
let initPromise = null;

async function ensureInitialized() {
    if (ghosttyInitialized) return;
    if (initPromise) return initPromise;

    initPromise = init();
    await initPromise;
    ghosttyInitialized = true;
}

window.terminalInterop = {
    terminals: new Map(),

    init: async function (elementId, dotNetRef) {
        const container = document.getElementById(elementId);
        if (!container) {
            console.error('Terminal container not found:', elementId);
            return null;
        }

        // Ensure ghostty WASM is initialized
        await ensureInitialized();

        const term = new Terminal({
            cursorBlink: true,
            fontSize: 14,
            fontFamily: '"Cascadia Code", "Fira Code", Consolas, monospace',
            theme: {
                background: '#1e1e1e',
                foreground: '#d4d4d4',
                cursor: '#d4d4d4',
                cursorAccent: '#1e1e1e',
                black: '#1e1e1e',
                red: '#f44747',
                green: '#6a9955',
                yellow: '#d7ba7d',
                blue: '#569cd6',
                magenta: '#c586c0',
                cyan: '#4ec9b0',
                white: '#d4d4d4',
                brightBlack: '#808080',
                brightRed: '#f44747',
                brightGreen: '#6a9955',
                brightYellow: '#d7ba7d',
                brightBlue: '#569cd6',
                brightMagenta: '#c586c0',
                brightCyan: '#4ec9b0',
                brightWhite: '#ffffff'
            },
            scrollback: 1000
        });

        const fitAddon = new FitAddon();
        term.loadAddon(fitAddon);

        term.open(container);
        fitAddon.fit();

        // Use ghostty-web's observeResize for automatic fitting on container resize
        fitAddon.observeResize();

        // Also handle window resize events
        window.addEventListener('resize', () => {
            fitAddon.fit();
        });

        // Handle keyboard input - for regular characters only
        term.onData(data => {
            // Skip anything that looks like an escape sequence or control character
            // These are all handled by onKey instead
            if (data.startsWith('\x1b') || // Escape sequences (arrow keys, etc.)
                data === '\r' || data === '\n' || // Enter
                data === '\b' || data === '\x7f' || // Backspace
                data === '\t') { // Tab
                return;
            }

            dotNetRef.invokeMethodAsync('OnTerminalInput', data);
        });

        // Handle special keys - this is the primary handler for navigation keys
        term.onKey(e => {
            // Send key info for special handling
            dotNetRef.invokeMethodAsync('OnTerminalKey', {
                key: e.key,
                domEvent: {
                    key: e.domEvent.key,
                    code: e.domEvent.code,
                    ctrlKey: e.domEvent.ctrlKey,
                    altKey: e.domEvent.altKey,
                    shiftKey: e.domEvent.shiftKey
                }
            });
        });

        const terminalId = 'term_' + Date.now();
        this.terminals.set(terminalId, { term, fitAddon });

        return terminalId;
    },

    write: function (terminalId, text) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.term.write(text);
        }
    },

    writeLine: function (terminalId, text) {
        const entry = this.terminals.get(terminalId);
        if (entry) {
            entry.term.writeln(text);
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
