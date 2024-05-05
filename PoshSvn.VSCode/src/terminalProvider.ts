import * as vscode from 'vscode';

import { findPowerShell } from "./findPowerShell";
import { helpMessage } from "./texts";
import path from 'path';

const shellIntegrationScript = path.join(
    vscode.env.appRoot,
    "out",
    "vs",
    "workbench",
    "contrib",
    "terminal",
    "browser",
    "media",
    "shellIntegration.ps1");

export const terminalOptions: vscode.TerminalOptions = {
    name: "PoshSvn terminal",
    shellPath: findPowerShell(),
    message: helpMessage,
    env: {
        "PSModulePath": __dirname
    },
    // It should work, but it doesn't. I don't know why.
    // TODO: fix it
    shellArgs: `-NoExit -NoLogo -ExecutionPolicy RemoteSigned -Command "& '${shellIntegrationScript}'"`,
    // TODO: fill other parameters
}

export class PoshSvnTerminalProfileProvider implements vscode.TerminalProfileProvider {
    provideTerminalProfile(token: vscode.CancellationToken): vscode.ProviderResult<vscode.TerminalProfile> {
        return {
            options: terminalOptions
        };
    }
}
