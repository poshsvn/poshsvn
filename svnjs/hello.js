var addon = require('../bin/Debug-x64/svnjs.node');;

console.log(addon.hello((o) => { console.log(o) })); // 'world'
