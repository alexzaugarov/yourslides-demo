define(["jquery"], function ($) {
    var defaults = {
        onChange: function () { }
    };
    return function(options) {
        var settings = $.extend({}, defaults, options);
        $(".sort-controls").on("click", "a", function (event) {
            var column = $(this).data("sort-column"),
                sorttype = $(this).data("sort-type"),
                sortTypeClass;
            if (this !== $(this).closest(".sort-controls").find(".link-selected")[0]) {
                sorttype = $(this).data("default-sort-type");
            }
            $(".sort-controls .link-selected").removeClass("link-selected sort-type-ascending sort-type-descending");
            if (sorttype === "Ascending") {
                sortTypeClass = "sort-type-ascending";
                $(this).data("sort-type", "Descending");
            } else {
                sortTypeClass = "sort-type-descending";
                $(this).data("sort-type", "Ascending");
            }
            $(this).addClass("link-selected " + sortTypeClass);
            settings.onChange.call({column: column, sorttype: sorttype});
            event.preventDefault();
        });
    }
});