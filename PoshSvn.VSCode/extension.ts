import * as vscode from 'vscode';

import { ILogger, Logger } from './logging';
import { findPowerShell } from './findPowerShell';

const helpMessage =
    "   Welcome to PoshSvn Terminal!\r\n" +
    "\r\n" +
    "Type Get-Command -Module PoshSvn to list avalible commands.\r\n" +
    "Type Get-Help <cmdlet-name> to get help of a specific command.\r\n";

const logger: ILogger = new Logger();

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
    logger.log("Activating extension.");

    context.subscriptions.push(vscode.commands.registerCommand('PoshSvn.openTerminal', () => {
        let terminal = vscode.window.createTerminal(terminalOptions);
        terminal.show();
    }))

    vscode.window.registerTerminalProfileProvider('PoshSvn.terminalProfile', {
        provideTerminalProfile: provideTerminalProfile
    });

    logger.log("Extension activation finished.");
}

export function deactivate() { }
