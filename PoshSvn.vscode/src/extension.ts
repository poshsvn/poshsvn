import * as vscode from 'vscode';

const helpMessage =
    "   Welcome to PoshSvn Terminal!\r\n" +
    "\r\n" +
    "Type Get-Command -Module PoshSvn to list avalible commands.\r\n" +
    "Type Get-Help <cmdlet-name> to get help of a specific command.\r\n";

let terminalOptions: vscode.TerminalOptions = {
    name: "PoshSvn terminal",
    shellPath: "powershell",
    message: helpMessage,
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
