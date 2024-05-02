import * as vscode from 'vscode';

export interface ILogger {
    log(message: string): void;
}

export class Logger implements ILogger {
    private logChannel: vscode.OutputChannel;

    constructor() {
        this.logChannel = vscode.window.createOutputChannel("PoshSvn");
    }

    log(message: string): void {
        this.write(message);
    }

    private write(message: string): void {
        this.logChannel.appendLine(message);
    }
}
