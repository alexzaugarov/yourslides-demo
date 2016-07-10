requirejs(["jquery", "common", "sortcontrols", "presentationloadform"], function ($, common, sortControls, presentationLoadForm) {
    $(function () {
        /*var requestData = $.extend({}, common.getSortControlsValues(), {
            ownerid: $("#ownerid").val(),
            count: 500,
            offset: $("#presentations-container").data("length")
        }),
            viewer = common.createPresetationsLazyView();
        if (requestData.offset === 15) {
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
            });
        } else {
            common.switchState("#state__complete");
        }
        $("#sort-control-inputs").on("change", function () {
            $("#user-page-controls-form").submit();
        });
        $("#user-page-controls-form").on("submit", function (event) {
            var requestObject = $.extend({}, common.getSortControlsValues(),
            {
                count: 500,
                ownerid: $("#ownerid").val(),
                searchstring: $("#searchstring").val()
            });
            $.ajax({
                type: "GET",
                url: $(this).attr("action"),
                data: requestObject,
                beforeSend: function () {
                    $("#state__request-in-progress").removeClass("ys-hide");
                    common.switchState("#state__loading");
                },
                success: function (data) {
                    viewer.reset();
                    $("#state__request-in-progress").addClass("ys-hide");
                    common.switchState("#state__more");
                    if (data.length > 0) {
                        viewer.addRange(data);
                    } else {
                        $(".state-container .state-item").addClass("ys-hide");
                        $("#state__empty").text("Не найдено презентаций по запросу \"" + $("#searchstring").val() + "\"")
                            .removeClass("ys-hide");
                    }
                },
                error: function (error) {

                }
            });

            event.preventDefault();
        });*/
        presentationLoadForm({
            beforeSend: function() {
                $("#state__request-in-progress").removeClass("ys-hide");
            },
            onComplete: function() {
                $("#state__request-in-progress").addClass("ys-hide");
            },
            onEmpty: function() {
                $(".state-container .state-item").addClass("ys-hide");
                $("#state__empty").text("Не найдено презентаций по заданным критериям")
                    .removeClass("ys-hide");
            }
        });
        sortControls({
            onChange: function () {
                common.setSortControlInputs(this);
                $("#presentation-load-form").submit();
            }
        });
    });
});