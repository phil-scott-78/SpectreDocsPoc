// Theme update function for Monaco
window.updateMonacoTheme = function(isDark) {
    if (typeof monaco !== 'undefined') {
        monaco.editor.setTheme(isDark ? 'vs-dark' : 'vs');
    }
};

// Editor completion and hover provider for Monaco
window.EditorInterop = {
    dotNetHelper: null,
    completionProviderDisposable: null,
    hoverProviderDisposable: null,

    initialize: function(dotNetHelper) {
        this.dotNetHelper = dotNetHelper;
        this.registerCompletionProvider();
        this.registerHoverProvider();
    },

    registerCompletionProvider: function() {
        if (this.completionProviderDisposable) {
            this.completionProviderDisposable.dispose();
        }

        const self = this;

        this.completionProviderDisposable = monaco.languages.registerCompletionItemProvider('csharp', {
            triggerCharacters: ['.', ' ', '(', '<', '[', '"'],

            provideCompletionItems: async function(model, position, context, token) {
                if (!self.dotNetHelper) {
                    return { suggestions: [] };
                }

                try {
                    const code = model.getValue();
                    const lineNumber = position.lineNumber;
                    const column = position.column;

                    const completions = await self.dotNetHelper.invokeMethodAsync(
                        'GetCompletions',
                        code,
                        lineNumber,
                        column
                    );

                    if (!completions || completions.length === 0) {
                        return { suggestions: [] };
                    }

                    const wordInfo = model.getWordUntilPosition(position);
                    const range = {
                        startLineNumber: position.lineNumber,
                        endLineNumber: position.lineNumber,
                        startColumn: wordInfo.startColumn,
                        endColumn: wordInfo.endColumn
                    };

                    const suggestions = completions.map(function(item) {
                        return {
                            label: item.label,
                            kind: item.kind,
                            insertText: item.insertText || item.label,
                            detail: item.detail || '',
                            sortText: item.sortText || item.label,
                            range: range
                        };
                    });

                    return { suggestions: suggestions };
                } catch (error) {
                    console.error('Completion error:', error);
                    return { suggestions: [] };
                }
            }
        });
    },

    registerHoverProvider: function() {
        if (this.hoverProviderDisposable) {
            this.hoverProviderDisposable.dispose();
        }

        const self = this;

        this.hoverProviderDisposable = monaco.languages.registerHoverProvider('csharp', {
            provideHover: async function(model, position, token) {
                if (!self.dotNetHelper) {
                    return null;
                }

                try {
                    const code = model.getValue();
                    const lineNumber = position.lineNumber;
                    const column = position.column;

                    const hoverData = await self.dotNetHelper.invokeMethodAsync(
                        'GetHover',
                        code,
                        lineNumber,
                        column
                    );

                    if (!hoverData || !hoverData.contents) {
                        return null;
                    }

                    return {
                        contents: [
                            { value: hoverData.contents, isTrusted: true }
                        ],
                        range: {
                            startLineNumber: hoverData.startLine,
                            startColumn: hoverData.startColumn,
                            endLineNumber: hoverData.endLine,
                            endColumn: hoverData.endColumn
                        }
                    };
                } catch (error) {
                    console.error('Hover error:', error);
                    return null;
                }
            }
        });
    },

    dispose: function() {
        if (this.completionProviderDisposable) {
            this.completionProviderDisposable.dispose();
            this.completionProviderDisposable = null;
        }
        if (this.hoverProviderDisposable) {
            this.hoverProviderDisposable.dispose();
            this.hoverProviderDisposable = null;
        }
        this.dotNetHelper = null;
    }
};
