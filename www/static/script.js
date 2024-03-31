if (localStorage.getItem("theme") == null) {
    if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
        localStorage.setItem("theme", "dark");
    } else {
        localStorage.setItem("theme", "light");
    }
}

document.documentElement.setAttribute("data-bs-theme", localStorage.getItem("theme"));

function toggleDarkMode() {
    localStorage.setItem("theme", (localStorage.getItem("theme") == "dark") ? "light" : "dark")
    document.documentElement.setAttribute("data-bs-theme", localStorage.getItem("theme"));
}

(function () {
    var langs = ["en", "fr", "ru"];
    for (var lang of navigator.languages) {
        for (var l of langs) {
            if (lang.match(l)) {
                document.documentElement.lang = l;
                return;
            }
        }
    }
}());
