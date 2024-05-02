import * as vscode from 'vscode';

import { ILogger, Logger } from './logging';
import { PoshSvnTerminalProfileProvider, terminalOptions } from './terminalProvider';

const logger: ILogger = new Logger();

export class OpenPoshSvnTerminalCommand implements vscode.Disposable {
    private command: vscode.Disposable;

    constructor() {
        this.command = vscode.commands.registerCommand("PoshSvn.openTerminal", this.invokeCommand);
    }

    private invokeCommand() {
        let terminal = vscode.window.createTerminal(terminalOptions);
        terminal.show();
    }

    public dispose(): void {
        this.command.dispose();
    }
}

export function activate(context: vscode.ExtensionContext) {
    logger.log("Activating extension.");

    context.subscriptions.push(new OpenPoshSvnTerminalCommand());

    vscode.window.registerTerminalProfileProvider(
        'PoshSvn.terminalProfile',
        new PoshSvnTerminalProfileProvider());

    logger.log("Extension activation finished.");
}

export function deactivate() { }
