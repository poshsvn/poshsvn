import * as vscode from 'vscode';

let terminalOptions: vscode.TerminalOptions = {
    name: "PoshSvn terminal",
    shellPath: "powershell",
    shellArgs: `-NoExit -Command ipmo "${__dirname}\\PoshSvn.psd1"`,
    // TODO: fill other parameters
}

function provideTerminalProfile(token: vscode.CancellationToken):
    vscode.ProviderResult<vscode.TerminalProfile> {
    return {
        options: terminalOptions
    };
}

export function activate(context: vscode.ExtensionContext) {
    context.subscriptions.push(vscode.commands.registerCommand('PoshSvn.open.terminal', () => {
        let terminal = vscode.window.createTerminal(terminalOptions);
        terminal.show();
    }))

    vscode.window.registerTerminalProfileProvider('PoshSvn.terminal.profile', {
        provideTerminalProfile: provideTerminalProfile
    });
}

export function deactivate() { }
