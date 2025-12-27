// Editor completion provider for Monaco
window.EditorInterop = {
    dotNetHelper: null,
    completionProviderDisposable: null,

    initialize: function(dotNetHelper) {
        this.dotNetHelper = dotNetHelper;
        this.registerCompletionProvider();
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

    dispose: function() {
        if (this.completionProviderDisposable) {
            this.completionProviderDisposable.dispose();
            this.completionProviderDisposable = null;
        }
        this.dotNetHelper = null;
    }
};
