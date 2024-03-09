svn-mkdir -url file:///C:/Demo/repos/foo -m "Add foo."
svn-mkdir -url file:///C:/Demo/repos/bar -m "Add bar."
Clear-Host
svn-checkout file:///C:/Demo/repos wc
cd wc
"Hello, World!" > "baz.txt"
svn-add "baz.txt"
svn-commit -m "Add baz!!"
$info = svn-info
"$($info.Path) $($info.Revision)"
