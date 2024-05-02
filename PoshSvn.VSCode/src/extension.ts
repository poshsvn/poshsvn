import * as vscode from 'vscode';

import { ILogger, Logger } from './logging';
import { PoshSvnTerminalProfileProvider, terminalOptions } from './terminalProvider';

const logger: ILogger = new Logger();

export function activate(context: vscode.ExtensionContext) {
    logger.log("Activating extension.");

    context.subscriptions.push(vscode.commands.registerCommand('PoshSvn.openTerminal', () => {
        let terminal = vscode.window.createTerminal(terminalOptions);
        terminal.show();
    }))

    vscode.window.registerTerminalProfileProvider(
        'PoshSvn.terminalProfile',
        new PoshSvnTerminalProfileProvider());

    logger.log("Extension activation finished.");
}

export function deactivate() { }
