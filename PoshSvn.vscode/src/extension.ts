import * as vscode from 'vscode';

export function activate(context: vscode.ExtensionContext) {
    context.subscriptions.push(vscode.commands.registerCommand('PoshSvn.open.terminal', () => {
        let terminal = vscode.window.createTerminal(
            "PoshSvn terminal",
            "powershell.exe",
            `-NoExit -Command ipmo "${__dirname}\\PoshSvn.psd1"`
        );

        terminal.show();
    }))
}

export function deactivate() { }
