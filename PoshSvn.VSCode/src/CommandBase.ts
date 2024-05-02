import * as vscode from 'vscode';

export abstract class CommandBase implements vscode.Disposable {
    private command: vscode.Disposable;

    constructor(commandName: string) {
        this.command = vscode.commands.registerCommand(commandName, this.invokeCommand);
    }

    dispose() {
        this.command.dispose();
    }

    abstract invokeCommand(): void;
}
