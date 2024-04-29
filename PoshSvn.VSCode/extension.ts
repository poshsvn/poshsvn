import * as vscode from 'vscode';
import child_process from 'child_process';

const helpMessage =
    "   Welcome to PoshSvn Terminal!\r\n" +
    "\r\n" +
    "Type Get-Command -Module PoshSvn to list avalible commands.\r\n" +
    "Type Get-Help <cmdlet-name> to get help of a specific command.\r\n";

function checkIsCommandExists(command: string): boolean {
    try {
        var stdout = child_process.execSync(`where "${command}"`);
        return !!stdout;
    } catch (ex) {
        return false;
    }
}

function* enumeratePowerShellInstallations(): Iterable<string> {
    yield "pwsh.exe";
    yield "powershell.exe";
}

function findPowerShell(): string {
    for (const powershell of enumeratePowerShellInstallations()) {
        if (checkIsCommandExists(powershell)) {
            return powershell;
        }
    }

    throw new Error(
        "Couldn't find PowerShell installation." +
        "Please make sure you have PowerShell installed and added to PATH."
    );
}

let terminalOptions: vscode.TerminalOptions = {
    name: "PoshSvn terminal",
    shellPath: findPowerShell(),
    message: helpMessage,
    env: {
        "PSModulePath": __dirname
    },
    shellArgs: `-NoExit -NoLogo -ExecutionPolicy RemoteSigned`,
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
