import * as vscode from 'vscode';
import { terminalOptions } from './terminalProvider';

export class OpenPoshSvnTerminalCommand implements vscode.Disposable {
    private command: vscode.Disposable;

    constructor() {
        this.command = vscode.commands.registerCommand("PoshSvn.openTerminal", this.invokeCommand);
    }

    dispose() {
        this.command.dispose();
    }

    invokeCommand(): void {
        const terminal = OpenPoshSvnTerminalCommand.ensurePoshSvnTerminal();
        terminal.show();
    }

    static ensurePoshSvnTerminal(): vscode.Terminal {
        const terminal = OpenPoshSvnTerminalCommand.findFirstPoshsvnTerminal();

        if (terminal) {
            return terminal;
        } else {
            return vscode.window.createTerminal(terminalOptions);
        }
    }

    static findFirstPoshsvnTerminal(): vscode.Terminal | null {
        const activeTerminal = vscode.window.activeTerminal;
        if (activeTerminal && OpenPoshSvnTerminalCommand.isTerminalPoshSvn(activeTerminal)) {
            return activeTerminal;
        }

        for (const terminal of vscode.window.terminals) {
            if (OpenPoshSvnTerminalCommand.isTerminalPoshSvn(terminal)) {
                return terminal;
            }
        }

        return null;
    }

    static isTerminalPoshSvn(terminal: vscode.Terminal) {
        // TODO: use better way to chech terminal type ???
        return terminal.name == "PoshSvn terminal";
    }
}
