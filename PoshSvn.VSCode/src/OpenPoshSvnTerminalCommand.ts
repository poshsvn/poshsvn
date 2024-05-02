import * as vscode from 'vscode';
import { terminalOptions } from './terminalProvider';


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
