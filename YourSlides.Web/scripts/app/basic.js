requirejs(['jquery', 'moment'], function($, moment) {
    moment.locale("ru");
    $(function () {
        $("[data-p-created]").each(function(index) {
            var elem = $(this),
                datetime = moment.unix(elem.data("p-created"));
            elem.text(datetime.fromNow());
            elem.attr("title", datetime.calendar(null, {sameElse: "D MMMM YYYYг. в H:mm"}));
        });
    });
});