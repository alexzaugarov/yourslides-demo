define(["jquery", "lazyviewcontainer", "common"], function ($, lazyViewContainer, common) {
    var deafults = {
        beforeSend: function () { },
        onEmpty: function () { },
        onSuccess: function () { },
        onComplete: function () { },
        dateFormat: "timespan"
    };
    return function (options) {
        var settings = $.extend({}, deafults, options),
            $presentationContainer = $("#presentations-container"),
            totalPageLength = $presentationContainer.data("total-page-length"),
            currentPageLength = $presentationContainer.data("current-page-length"),
            viewer = lazyViewContainer({
                $trackObject: $("#state__more").removeClass("ys-hide"),
                itemTemplateHtml: $("#presentation-preview-item-template").html(),
                pageLength: totalPageLength,
                container: $presentationContainer[0],
                onAllItemsShowed: function () {
                    common.switchState("#state__complete");
                },
                modifyModelItem: function (item) {
                    switch (settings.dateFormat) {
                        case "timespan":
                            item.formattedDate = common.createFormattedDateFromNow(item.Created);
                            break;
                        case "full":
                            item.formattedDate = common.createFormattedDateFull(item.Created);
                            break;
                        default:
                    }

                    return item;
                }
            }),
            $form = $("#presentation-load-form"),
            load = function (reset) {
                common.switchState("#state__loading");
                settings.beforeSend();
                $.getJSON($form.attr("action"), $form.serialize(), function (result) {
                    settings.onComplete();
                    if (reset) {
                        viewer.reset();
                    }
                    if (result.length > 0) {
                        common.switchState("#state__more");
                        viewer.addRange(result);
                        settings.onSuccess();
                    } else {
                        common.switchState("#state__complete");
                        if (reset) {
                            settings.onEmpty();
                        }
                    }
                });
            },
            setOffset = function (offset) {
                $("input[name='offset']").val(offset);
            };
        if (currentPageLength === totalPageLength) {
            setOffset(currentPageLength);
            load(false);
        } else {
            common.switchState("#state__complete");
        }
        $form.on("submit", function (event) {
            setOffset(0);
            load(true);
            event.preventDefault();
        });
    }
});