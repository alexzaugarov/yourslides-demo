requirejs(['jquery'], function ($) {
    $(function () {
        $(".info__buttons").on("click", ".link-button", function(event) {
            if (this === $(".info__buttons .link-selected")[0]) {
                return;
            }
            $(".info__description").toggleClass("ys-hide");
            $(".info__embed").toggleClass("ys-hide");
            $("#show-embed-button").toggleClass("link-selected");
            $("#show-description-button").toggleClass("link-selected");
        });
    });
});