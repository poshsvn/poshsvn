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
        return terminal.name == "PoshSvn terminal";
    }
}
