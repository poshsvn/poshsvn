import child_process from 'child_process';
import { powerShellNotFoundError } from './texts';

function checkIsCommandExists(command: string): boolean {
    try {
        var stdout = child_process.execSync(`where "${command}"`);
        return !!stdout;
    } catch (ex) {
        return false;
    }
}

function* enumeratePowerShellInstallations(): Iterable<string> {
    yield "pwsh.exe";
    yield "powershell.exe";
}

export function findPowerShell(): string {
    for (const powershell of enumeratePowerShellInstallations()) {
        if (checkIsCommandExists(powershell)) {
            return powershell;
        }
    }

    throw new Error(powerShellNotFoundError);
}
