import * as vscode from 'vscode';
import { terminalOptions } from './terminalProvider';
import { CommandBase } from './CommandBase';

export class OpenPoshSvnTerminalCommand extends CommandBase {
    constructor() {
        super("PoshSvn.openTerminal");
    }

    invokeCommand(): void {
        const oldTerminal = OpenPoshSvnTerminalCommand.findFirstPoshsvnTerminal();

        if (oldTerminal) {
            oldTerminal.show();
        } else {
            let terminal = vscode.window.createTerminal(terminalOptions);
            terminal.show();
        }
    }

    static findFirstPoshsvnTerminal(): vscode.Terminal | null {
        for (const terminal of vscode.window.terminals) {
            if (terminal.name == "PoshSvn terminal") {
                return terminal;
            }
        }

        return null;
    }
}
