requirejs(['jquery', 'moment', "handlebars"], function ($, moment, handlebars) {
    moment.locale("ru");
    var formatDateFull = function (momentDate) {
        var fullTimeFormat = "D MMMM YYYY г. в H:mm";
        return momentDate.format(fullTimeFormat);
    },
    formatDateTimestamp = function (momentDate) {
        return momentDate.fromNow();
    },
    formatDate = function (unixTimestamp, formatType) {
        var datetime = moment.unix(unixTimestamp);
        if (formatType === "full") {
            return formatDateFull(datetime);
        }
        if (formatType === "timespan") {
            return formatDateTimestamp(datetime);
        }
    }
    $(function () {
        $("[data-datetime]").each(function (index) {
            var datetime = moment.unix($(this).data("datetime")),
                formattype = $(this).data("datetime-format");
            if (formattype === "full") {
                $(this).text(formatDateFull(datetime));
            }
            if (formattype === "timespan") {
                $(this).text(formatDateTimestamp(datetime));
                $(this).attr("title", formatDateFull(datetime));
            }
        });

        $.each($("textarea[autoresize]"), function () {
            var offset = this.offsetHeight - this.clientHeight;
            var resizeTextarea = function (el) {
                $(el).css('height', 'auto').css('height', el.scrollHeight + offset);
            };
            $(this).on('keyup input', function () { resizeTextarea(this); });
        });
        $("textarea[autoresize]").trigger('keyup');

        handlebars.registerHelper("ifeq", function (v1, v2, option) {
            if (v1 === v2) {
                return option.fn(this);
            } else {
                return option.inverse(this);
            }
        });
        handlebars.registerHelper("formatDatetime", function (unixTimeStamp, formatType, option) {
            return option.fn({ formattedDate: formatDate(unixTimeStamp, formatType) });
        });
        $(window).scroll(function(event) {
            if ($(window).scrollTop() > 300) {
                $("#back-to-top-button").fadeIn("slow");
            } else {
                $("#back-to-top-button").fadeOut("slow");
            }
        });
        $("#back-to-top-button").click(function (event) {
            $("html, body").animate({
                scrollTop: 0
            }, 200);
        });
    });
});