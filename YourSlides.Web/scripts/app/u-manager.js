requirejs(["jquery"], function ($) {
    var setVisibility = function (visibility) {
        $("input[name='visibility'][value='" + visibility + "']").prop("checked", "checked");
    },
        setStatus = function (status) {
            $("input[name='status'][value='" + status + "']").prop("checked", "checked");
        },
        switchSelectedLinkAndSubmit = function () {
            $(".filters .link-selected").removeClass("link-selected");
            $(this).addClass("link-selected");
            $("#presentation-load-form").submit();
        };
    $(function () {
        $("#presentations-container").on("click", ".action-delete", function (event) {
            var $cofirmContainer = $(this).next().removeClass("ys-hide");
            $(this).addClass("ys-hide");
            $(this).parent().find(".delete__error").addClass("ys-hide");
            /*intervalId = setTimeout(function () {
                $cofirmContainer.addClass("ys-hide");
            }, 10000);
        clearTimeout($cofirmContainer.data("hide-handler"));
        $cofirmContainer.data("hide-handler", intervalId);*/
        });
        $("#presentations-container").on("click", ".delete-deny", function (event) {
            $(this).parent().addClass("ys-hide");
            $(this).closest(".presentation-view-list-item__actions").find(".action-delete").removeClass("ys-hide");
        }).on("click", ".delete-confirm", function (event) {
            var $source = $(this),
                $parent = $source.closest(".presentation-view-list-item__actions"),
                showError = function (xhr, textStatus, errorThrown) {
                    console.log(textStatus);
                    console.log(errorThrown);
                    $parent.find(".delete__error").removeClass("ys-hide");
                    $parent.find(".delete__progress").addClass("ys-hide");
                };
            console.log($parent);
            $source.parent().addClass("ys-hide");
            $.ajax({
                type: "DELETE",
                data: { "": $source.data("presentation-id") },
                url: $("#form-delete").attr("action"),
                beforeSend: function () {
                    $parent.find(".delete__progress").removeClass("ys-hide");
                },
                success: function (result) {
                    if (result === true) {
                        var $item = $parent.closest(".presentation-view-list-item")
                            .html($("<div/>").addClass("state-item").text("Презентация удалена")
                            .css("display", "block"));
                        setTimeout(function () {
                            $item.remove();
                        }, 5000);
                    } else {
                        showError();
                    }
                },
                error: showError
            });
        });
        $(".filter__visibility").on("click", ".link-button", function (event) {
            var visibility = $(this).data("visibility");
            $("#status-default").prop("checked", "checked");
            setVisibility(visibility);
            switchSelectedLinkAndSubmit.call(this);
        });
        $(".filter__status").on("click", ".link-button", function (event) {
            var status = $(this).data("status");
            $("#visibility-default").prop("checked", "checked");
            setStatus(status);
            switchSelectedLinkAndSubmit.call(this);
        });
        $("#presetation-load-form-reset").on("click", function(event) {
            $(".filters .link-selected").removeClass("link-selected");
            $("#presentation-load-form")[0].reset();
            $("#presentation-load-form").submit();
            $(".sort-controls .link-selected").removeClass("link-selected sort-type-ascending sort-type-descending");
            $("#sort-by-date").addClass("link-selected sort-type-descending").data("sorttype", "ascending");
        });
    });
});