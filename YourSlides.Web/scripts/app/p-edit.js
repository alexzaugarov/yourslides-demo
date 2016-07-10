requirejs(["jquery", "lazyviewcontainer", "handlebars", "spectrum", "jqueryui", "lib/jquery.spectrum-ru", "signalr", "hubs", "imagecolorpicker", "colordata", "validate", "validateUnobtrusive", "onshow"], function ($, lazyViewContainer, handlebars) {
    function setPaddingToPreviewThumbnail() {
        var container = $(".edit-info-form__title-slide-container");
        var img = $(".edit-info-form__title-slide-img");
        $("<img/>").attr("src", img.attr("src"))
                .load(function (parameters) {
                    var conatainerAspectRatio = container.width() / container.height(),
                        imgAspectRatio = this.width / this.height;
                    if (imgAspectRatio > conatainerAspectRatio) {
                        container.css("padding", (container.height() - this.height) / 2 + 10 + "px 0px");
                    } else {
                        container.css("padding", "");
                    }
                });
    }

    function changeScreenBackgroundColorInput(value) {
        $("#ScreenBackgroundColor").val(value);
        $("#ScreenBackgroundColor").trigger("change");
    }

    function setSubmitButtonState(enable) {
        $("#edit-form__submit-button").prop("disabled", !enable);
    }

    function createThumbnail(id, thumbnailIndex) {
        return {
            index: thumbnailIndex,
            url: "/storage/" + id + "/preview_big/" + thumbnailIndex + ".png",
            selectedClass: $("input[name='LogoSlideIndex']").val() == thumbnailIndex ? "thumbnail-item_selected" : ""
        }
    }
    function createThumbnails(id, count) {
        var thumbnailItems = [];
        for (var i = 1; i <= count; i++) {
            thumbnailItems.push(createThumbnail(id, i));
        }
        return thumbnailItems;
    }

    function setProgressbarValue(value, label) {
        if (label === undefined) {
            label = value + "%";
        }
        $("#file-processing-progress-bar-ui").progressbar("option", "value", value)
                .children('.progress-bar__label')
                .html(label);
    }

    function setLogoPreview(url, index) {
        $(".edit-info-form__title-slide-img").attr("src", url);
        setPaddingToPreviewThumbnail();
        if (index !== undefined) {
            $("#slide-index-label").text(index);
            $("input[name='LogoSlideIndex']").val(index);
        }
    }

    function disableForm() {
        $("#current-status-text").text("произошла ошибка во время обработки");
        $(".edit-info-form__processing-status").toggleClass("edit-info-form__processing-status_theme_in-progress edit-info-form__processing-status_theme_error");
        setProgressbarValue(0, "Ошибка");
        $("#edit-form :input").attr("disabled", true);
        $("#spectrum").spectrum("disable");
    }
    $(function () {
        setPaddingToPreviewThumbnail();
        $("#spectrum").spectrum({
            showInput: true,
            showPalette: true,
            localStorageKey: "spectrum.colors",
            preferredFormat: "hex",
            palette: [
        ["#000", "#444", "#666", "#999", "#ccc", "#eee", "#f3f3f3", "#fff"],
        ["#f00", "#f90", "#ff0", "#0f0", "#0ff", "#00f", "#90f", "#f0f"],
        ["#f4cccc", "#fce5cd", "#fff2cc", "#d9ead3", "#d0e0e3", "#cfe2f3", "#d9d2e9", "#ead1dc"],
        ["#ea9999", "#f9cb9c", "#ffe599", "#b6d7a8", "#a2c4c9", "#9fc5e8", "#b4a7d6", "#d5a6bd"],
        ["#e06666", "#f6b26b", "#ffd966", "#93c47d", "#76a5af", "#6fa8dc", "#8e7cc3", "#c27ba0"],
        ["#c00", "#e69138", "#f1c232", "#6aa84f", "#45818e", "#3d85c6", "#674ea7", "#a64d79"],
        ["#900", "#b45f06", "#bf9000", "#38761d", "#134f5c", "#0b5394", "#351c75", "#741b47"],
        ["#600", "#783f04", "#7f6000", "#274e13", "#0c343d", "#073763", "#20124d", "#4c1130"]
            ],
            maxSelectionSize: 80,
            move: function (color) {
                $(".edit-info-form__title-slide-container").css("background", color.toHexString());
                $(".edit-info-form__title-slide-img").data("picked-color", color.toHexString());
                changeScreenBackgroundColorInput(color);
            },
            show: function (color) {
                $(".edit-info-form__title-slide-img").imageColorPicker({
                    setBackgroundElements: $(".edit-info-form__title-slide-container"),
                    color: $("#ScreenBackgroundColor").val(),
                    onColorPicked: function (color) {
                        changeScreenBackgroundColorInput(color);
                        $("#spectrum").spectrum("set", color);
                    }
                });
                $("body").toggleClass("color-picker-cursor");
            },
            hide: function (color) {
                $(".edit-info-form__title-slide-img").imageColorPicker("destroy");
                $("body").toggleClass("color-picker-cursor");
            }
        });
        $("#file-processing-progress-bar-ui").progressbar({
            value: false
        });
        var thumbnail = lazyViewContainer({
            $trackObject: $("<div/>").insertAfter($(".thumbnail-slide-container")),
            pageStart: 1,
            container: $(".thumbnail-slide-container")[0],
            itemTemplateHtml: $("#thumbnail-item-template").html()
        });
        if ($("input[name='Status']").val() == 0) {
            setProgressbarValue(100, "Завершено");
            thumbnail.addRange(createThumbnails($("input[name='Id']").val(), $("input[name='SlideCount']").val()));
        } else {
            var converter = $.connection.presentationProcessing,
                totalSlides,
                justLoaded = true,
                isLogSet = false;

            converter.client.onProgress = function (id, current, total) {
                if ($("input[name='Id']").val() == id) {
                    setProgressbarValue(Math.round((current / total) * 100));
                    $("#current-progress-text").text(current + " из " + total);
                    totalSlides = total;
                    var slideIndex = $("input[name='LogoSlideIndex']").val();
                    if (slideIndex < current && !isLogSet) {
                        setLogoPreview(createThumbnail(id, slideIndex).url);
                        isLogSet = true;
                    }
                    if (justLoaded) {
                        justLoaded = false;
                        thumbnail.addRange(createThumbnails(id, current - 1));
                    } else {
                        var t = createThumbnail(id, current - 1);
                        thumbnail.add(t);
                    }
                }
            }
            converter.client.onStarted = function (id) {
                console.log("server start convertation");
                if ($("input[name='Id']").val() == id) {
                    $("#current-status-text").text("идет обработка...");
                    setProgressbarValue(0);
                }
            }
            converter.client.onError = function (id) {
                console.log("server error");
                if ($("input[name='Id']").val() == id) {
                    disableForm();
                }
            }
            converter.client.onCompleted = function (id) {
                if ($("input[name='Id']").val() == id) {
                    $("#current-status-text").text("обработка заверешена");
                    $(".edit-info-form__processing-status").toggleClass("edit-info-form__processing-status_theme_in-progress edit-info-form__processing-status_theme_success");
                    thumbnail.add(createThumbnail(id, totalSlides));
                    setProgressbarValue(100, "Завершено");
                }
            }
            $.connection.logging = true;
            $.connection.hub.start().done(function () {
                console.log("connection established");
            });
            setTimeout(function () {
                var id = $("input[name='Id']").val();
                $.getJSON("/api/presentations/" + id, function (response) {
                    if (response === null) {
                        disableForm();
                    } else { console.log(response.Status) }

                });
            }, 5000);
        }
        $(".thumbnail-slide-container").on("click", ".thumbnail-item", function (event) {
            if (!$(this).hasClass(".thumbnail-item-selected")) {
                $(".thumbnail-item_selected").toggleClass("thumbnail-item_selected");
                $(this).toggleClass("thumbnail-item_selected");
                var index = $(this).data("slide-index"),
                    url = $(this).children("img").attr("src");
                setLogoPreview(url, index);
                setSubmitButtonState(true);
            }
        });
        $("#edit-form :input").change(function (event) {
            $(".edit-info-form__submint-container span").addClass("ys-hide");
            setSubmitButtonState(true);

        });
        $("#edit-form textarea").on("input", function (event) {
            if ($("#edit-form").valid()) {
                setSubmitButtonState(true);
            } else {
                setSubmitButtonState(false);
            }

        });
        $("#edit-form").submit(function (event) {
            $.ajax({
                type: "PUT",
                url: $(this).attr("action"),
                data: $("#edit-form").serialize(),
                beforeSend: function () {
                    setSubmitButtonState(false);
                    $(".edit-info-form__form-submitting-progress-icon").removeClass("ys-hide");
                },
                success: function (result) {
                    $(".edit-info-form__form-submitting-progress-icon").addClass("ys-hide");
                    if (result === true) {
                        $(".edit-info-form__form-submitting-success-icon").removeClass("ys-hide");
                        $(".edit-info-form__form-submitting-back-anchor").removeClass("ys-hide");
                        document.title = $("textarea[name='Title']").val() + " - Yourslides";
                    } else {
                        $(".edit-info-form__form-submitting-error").removeClass("ys-hide");
                    }
                },
                error: function (error) {
                    $(".edit-info-form__form-submitting-progress-icon").addClass("ys-hide");
                    $(".edit-info-form__form-submitting-error").removeClass("ys-hide");
                }
            });

            event.preventDefault();
        });
    });
});