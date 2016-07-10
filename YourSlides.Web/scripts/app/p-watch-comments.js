requirejs(["jquery", "moment", "handlebars", "lazyviewcontainer", "onshow"], function ($, moment, handlebars, lazyViewContainer) {
    function createFormattedDate(unixtime) {
        return moment.unix(unixtime).fromNow();
    }
    function formatDate() {
        $("[data-comment-post-date]").each(function (index) {
            $(this).text(createFormattedDate($(this).data("comment-post-date")));
        });
    }
    function setSubmitButtonState(enable) {
        $("#post-comment-button").prop("disabled", !enable);
    }
    $(function () {
        moment.locale("ru");
        setInterval(formatDate, 60000);
        formatDate();
        $("#comment-input-field").on("focus", function (event) {
            $(".comments__post-button-container").removeClass("ys-hide");
        });
        var commentItemTemplate = handlebars.compile($("#comment-item-template").html(), { noEscape: true });
        $("#comment-input-field").on("input", function (event) {
            $(".comments__post-button-container span").addClass("ys-hide");
            if ($(this).val()) {
                setSubmitButtonState(true);
            } else {
                setSubmitButtonState(false);
            }
        }).on("blur", function (event) {
            if (!$(this).val()) {
                $(".comments__post-button-container").addClass("ys-hide");
            }
        });
        $("#comments__post-form").submit(function (event) {
            var data = {
                    presentationid: $("#presentationid").val(),
                    text: $("#comment-input-field").val().replace(/\n/g, "<br />")
                };
            $.ajax({
                type: "POST",
                url: $(this).attr("action"),
                data: data,
                dataType: "json",
                beforeSend: function () {
                    setSubmitButtonState(false);
                    $(".comments-post-form__form-submitting-progress-icon").removeClass("ys-hide");
                },
                success: function (data) {
                    $(".comments-post-form__form-submitting-progress-icon").addClass("ys-hide");
                    if (data) {
                        $(".comments-post-form__form-submitting-success-icon").removeClass("ys-hide");
                        $(".comments__container").prepend(commentItemTemplate(data));
                        $("#comment-input-field").val("");
                        formatDate();
                    } else {
                        $(".comments-post-form__form-submitting-error").removeClass("ys-hide");
                    }
                },
                error: function (error) {
                    $(".comments-post-form__form-submitting-progress-icon").addClass("ys-hide");
                    $(".comments-post-form__form-submitting-error").removeClass("ys-hide");
                }
            });

            event.preventDefault();
        });
        $("#state__loading").onShow({
            onShow: function () {
                var $showedElement = $(this);
                $(this).onShow("destroy");
                $.getJSON("/api/comments", {
                    count: 500,
                    presentationid: $("input[name='presentationid']").val()
                }, function (data) {
                    if (data.length > 0) {
                        lazyViewContainer({
                            $trackObject: $("#state__more").removeClass("ys-hide"),
                            itemTemplate: commentItemTemplate,
                            pageStart: 1,
                            pageLength: 10,
                            container: $(".comments__container")[0],
                            onAllItemsShowed: function () {
                                $("#state__more").addClass("ys-hide");
                                $("#state__complete").removeClass("ys-hide");
                            },
                            modifyModelItem: function (item) {
                                item.formattedDate = createFormattedDate(item.Created);
                                return item;
                            },
                            data: data
                        });
                    } else {
                        $("#state__no").removeClass("ys-hide");
                    }
                    $("#state__loading").addClass("ys-hide");
                });
            }
        });
        $("#comments__container").on("click", ".action-delete", function (event) {
            var id = $(this).data("comment-id"),
                $source = $(this);
            $.ajax({
                type: "DELETE",
                data: { "": id },
                url: $("#comment-form-delete").attr("action"),
                success: function (result) {
                    if (result === true) {
                        var $item = $source.closest(".comment-item")
                            .html($("<div/>").addClass("state-item").text("Комментарий удален")
                            .css("display", "block"));
                        setTimeout(function () {
                            $item.remove();
                        }, 5000);
                    } else {
                        $source.find(".error-message").removeClass("ys-hide");
                    }
                }
            });
        });
    });
});