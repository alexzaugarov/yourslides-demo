requirejs(["jquery", "common", "sortcontrols", "presentationloadform"], function ($, common, sortControls, presentationLoadForm) {
    $(function () {
        $(".sort-controls__form-inputs").appendTo("#search-form");
        /*$(".sort-controls").on("click", "a", function (event) {
            var column = $(this).data("sort-column"),
                sorttype = $(this).data("sort-type");
            common.setSortControlInputs({ column: column, sorttype: sorttype });
            $("#search-form").submit();
            event.preventDefault();
        });
        var requestData = common.serializeSortFormToObject("input[name='searchstring']", 500),
            viewer = common.createPresetationsLazyView();
        common.switchState("#state__loading");
        common.loadPresentationsAndShow({
            requestData: requestData,
            onComplete: function () {
                common.switchState("#state__more");
            },
            onData: function (data) {
                viewer.addRange(data);
            },
            onEmpty: function () {
                common.switchState("#state__complete");
            }
        });*/
        sortControls({
            onChange: function() {
                common.setSortControlInputs(this);
                $("#search-form").submit();
            }
        });
        presentationLoadForm();
    });
});