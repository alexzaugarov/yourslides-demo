define(["jquery", "handlebars", "moment", "lazyviewcontainer"], function ($, handlebars, moment, lazyViewContainer) {
    var methods = {
        compileTemplate: function (html) {
            return handlebars.compile(html);
        },
        createFormattedDateFromNow: function (unixTimeStamp) {
            return moment.unix(unixTimeStamp).fromNow();
        },
        createFormattedDateFull: function(unixTimeStamp) {
            return moment.unix(unixTimeStamp).format("D MMMM YYYY г. в H:mm");
        },
        setSortControlInputs: function (values) {
            $("input[name='sortcolumn'][value='" + values.column + "']").prop("checked", true);
            $("input[name='sorttype'][value='" + values.sorttype + "']").prop("checked", true);
        },
        loadPresentationsAndShow: function (options) {
            var lazyview = methods.createPresetationsLazyView();
            $.getJSON("/api/presentations", options.requestData, function (data) {
                if (data.length > 0) {
                    options.onData(data);
                } else {
                    options.onEmpty();
                }
                $("#state__loading").addClass("ys-hide");
                options.onComplete();
            });
            return lazyview;
        },
        createPresetationsLazyView: function () {
            return lazyViewContainer({
                $trackObject: $("#state__more").removeClass("ys-hide"),
                itemTemplateHtml: $("#presentation-preview-item-template").html(),
                pageLength: 15,
                container: $("#presentations-container")[0],
                onAllItemsShowed: function () {
                    methods.switchState("#state__complete");
                },
                modifyModelItem: function (item) {
                    item.formattedDate = methods.createFormattedDateFromNow(item.Created);
                    return item;
                }
            });
        },
        serializeSortFormToObject: function (searchStringSelector, count, offset) {
            if(offset === undefined) {
                offset = $("#presentations-container").data("length");
            }
            return {
                sortcolumn: $("input[name='sortcolumn']:checked").val(),
                sorttype: $("input[name='sorttype']:checked").val(),
                searchstring: $(searchStringSelector).val(),
                count: count,
                offset: offset
            }
        },
        getSortControlsValues: function () {
            return {
                sortcolumn: $("input[name='sortcolumn']:checked").val(),
                sorttype: $("input[name='sorttype']:checked").val()
            }
        },
        switchState: function (stateSelector) {
            $(".state-container .state-item").addClass("ys-hide");
            $(stateSelector).removeClass("ys-hide");
        }
    };
    return methods;
});