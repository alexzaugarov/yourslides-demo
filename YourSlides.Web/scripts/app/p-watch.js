requirejs(['jquery','moment'], function ($, moment) {
    var currentActiveButton = $("#show-description-button")[0];
    function toggleEmbedDescription(e) {
        e.preventDefault();
        if(this === currentActiveButton) {
            return;
        }
        $(".info__description").toggle();
        $(".info__embed").toggle();
        $("#show-embed-button").toggleClass("info__embed_inactive");
        $("#show-description-button").toggleClass("info__description_inactive");
        currentActiveButton = this;
    }

    function hideEmbed() {
        $("#show-embed-button").toggleClass("info__embed_inactive");
        $(".info__embed").toggle();
    }

    function formatDate() {
        $("[data-comment-post-date]").each(function (index) {
            $(this).text(moment.unix($(this).data("comment-post-date")).fromNow());
        });
    }
    $(function () {
        hideEmbed();
        $(".info__buttons a").on("click", toggleEmbedDescription);
        formatDate();
        setInterval(formatDate, 5000);
    });
});