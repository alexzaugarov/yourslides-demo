requirejs(["jquery", "common", "sortcontrols", "presentationloadform"], function ($, common, sortControls, presentationLoadForm) {
    $(function () {
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