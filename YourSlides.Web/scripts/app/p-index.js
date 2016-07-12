requirejs(["jquery", "common", "sortcontrols", "presentationloadform"], function ($, common, sortControls, presentationLoadForm) {
    $(function () {
        $(".sort-controls__form-inputs").appendTo("#search-form");
        sortControls({
            onChange: function() {
                common.setSortControlInputs(this);
                $("#search-form").submit();
            }
        });
        presentationLoadForm();
    });
});