import * as vscode from 'vscode';
import { terminalOptions } from './terminalProvider';
import { CommandBase } from './CommandBase';

export class OpenPoshSvnTerminalCommand extends CommandBase {
    constructor() {
        super("PoshSvn.openTerminal");
    }

    invokeCommand(): void {
        let terminal = vscode.window.createTerminal(terminalOptions);
        terminal.show();
    }
}
