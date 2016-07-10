(function ($) {
    function componentToHex(c) {
        var hex = c.toString(16);
        return hex.length == 1 ? "0" + hex : hex;
    }
    function rgbToHex(r, g, b) {
        return "#" + componentToHex(r) + componentToHex(g) + componentToHex(b);
    }
    $.ColorData = function(data) {
        this.data = data;
    };
    $.ColorData.prototype.toHexString = function () {
        return rgbToHex(this.data[0], this.data[1], this.data[2]);
    }
})(jQuery);