var addon = require('../bin/Debug-x64/svnjs.node');;

console.log(addon.info((o) => { console.log(o) })); // 'world'
