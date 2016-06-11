requirejs.config({
    baseUrl: "/scripts",
    paths: {
        jquery: "lib/jquery-2.2.3",
        moment: "lib/moment-with-locales",
        crossroads: "lib/crossroads",
        handlebars: "lib/handlebars-latest",
        signals: "lib/signals",
        fineuploader: "fine-uploader/fine-uploader",
        history: "lib/jquery.history",
        controllers : "app/controllers"
    },
    shim: {
        templates: ["handlebars"],
        crossroads: ["signals"],
        history: ["jquery"]
    },
    bundles: Yourslides.RequirejsBundlesConfig
});