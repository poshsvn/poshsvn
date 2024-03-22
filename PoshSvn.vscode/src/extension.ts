import * as vscode from 'vscode';

export function activate(context: vscode.ExtensionContext) {
    console.log('Congratulations, your extension "PoshSvn" is now active!');

    let disposable = vscode.commands.registerCommand('PoshSvn.helloWorld', () => {
        console.log("hello!!!!");

        vscode.window.showInformationMessage('Hello World from !');
    });

    context.subscriptions.push(disposable);
}

export function deactivate() { }
