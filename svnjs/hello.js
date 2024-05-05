var addon = require('../bin/Debug-x64/svnjs.node');

var client = new addon.SvnClient();

console.log(client.info((o) => { console.log(o) }));
