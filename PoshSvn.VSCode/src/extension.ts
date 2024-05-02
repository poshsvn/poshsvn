import * as vscode from 'vscode';

import { ILogger, Logger } from './logging';
import { PoshSvnTerminalProfileProvider } from './terminalProvider';
import { OpenPoshSvnTerminalCommand } from './OpenPoshSvnTerminalCommand';

const logger: ILogger = new Logger();

export function activate(context: vscode.ExtensionContext) {
    logger.log("Activating extension.");

    context.subscriptions.push(new OpenPoshSvnTerminalCommand());

    vscode.window.registerTerminalProfileProvider(
        'PoshSvn.terminalProfile',
        new PoshSvnTerminalProfileProvider());

    logger.log("Extension activation finished.");
}

export function deactivate() { }
