import * as vscode from 'vscode';
import { terminalOptions } from './terminalProvider';
import { CommandBase } from './CommandBase';

export class OpenPoshSvnTerminalCommand extends CommandBase {
    constructor() {
        super("PoshSvn.openTerminal");
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
        for (const terminal of vscode.window.terminals) {
            if (terminal.name == "PoshSvn terminal") {
                return terminal;
            }
        }

        return null;
    }
}
