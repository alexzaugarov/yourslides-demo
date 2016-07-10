define(["handlebars"], function (hb) {
    hb.registerHelper("ifeq", function (v1, v2, option) {
        if (v1 === v2) {
            return option.fn(this);
        }else {
            return option.inverse(this);
        }
    });
})