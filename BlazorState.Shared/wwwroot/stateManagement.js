window.stateManager = {
    save: function (key, str) {
        localStorage[key] = str;
    },
    load: function (key) {
        return localStorage[key];
    }
};