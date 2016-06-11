requirejs(['crossroads', 'jquery', 'history'], function (crossroads, $) {
    crossroads.addRoute("/", function () {
        console.log("Root");
        //console.log(this);
        //document.title = "Главная страница";
        //path.history.replaceState({ title: "Главная страница" },'');
    });
    crossroads.addRoute("/presentation/watch/{presentation_id}", function (presentationId) {
        //path.history.replaceState({});
    });
    crossroads.addRoute("/info", function() {
        //path.history.replaceState({});
    });
    $(window).on("statechange", function () {
        crossroads.parse(document.location.pathname + document.location.search);
    });
    console.log("main module");
    $(function () {
        $(".wrap").on("click", "a:not([default-behaviour])", function (event) {
            event.preventDefault();
            var url = $(this).attr("href");
            History.pushState({}, "", url);
        });
        crossroads.parse(document.location.pathname + document.location.search);
    });

    function runController(controller) {
        if(Yourslides.justLoaded) {
            controller.Run(window.Yourslides.data);
        }
        api.load(document.location.pathname + document.location.search, controller.Run());
    }
});