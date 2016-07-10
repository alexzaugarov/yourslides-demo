(function ($) {
    var defaults = {
        onColorPicked: function () { },
        onColorChanged: function () { },
        setBackgroundElements: $("<div/>"),
        color: "#fffff"
    },
        getColor = function (event) {
            var x = 0, y = 0;
            // chrome
            if (event.offsetX) {
                x = event.offsetX;
                y = event.offsetY;
            }
                // firefox
            else if (event.layerX) {
                x = event.layerX;
                y = event.layerY;
            }
            try {
                return new $.ColorData(this.getContext('2d').getImageData(x, y, 1, 1).data);
            } catch (e) {
                console.log(e);
                return null;
            }

        },
        init = function (options) {
            var settings = $.extend({}, defaults, options);
            this.data("picked-color", settings.color);
            function setColor(color) {
                settings.setBackgroundElements.css("background", color);
                settings.onColorChanged.call(this, color);
            }
            return this.each(function () {
                var canvas = createCanvas(this);
                $(this).bind("mousemove.imagecolorpicker", function (event) {
                    var color = getColor.call(canvas, event);
                    setColor.call(this, color.toHexString());
                });
                $(this).bind("mouseleave.imagecolorpicker", function (event) {
                    setColor.call(this, $(this).data("picked-color"));
                });
                $(this).bind("click.imagecolorpicker", function (event) {
                    var color = getColor.call(canvas, event).toHexString();
                    $(this).data("picked-color", color);
                    settings.onColorPicked.call(this, color);
                });
            });
        },
        destroy = function() {
            return this.each(function(parameters) {
                $(this).unbind(".imagecolorpicker");
            });
        },
        methods = {
            init: init,
            destroy: destroy
        };
    function createCanvas(img) {
        var canvas = document.createElement("canvas");
        canvas.width = img.width;
        canvas.height = img.height;
        canvas.getContext("2d").drawImage(img, 0, 0, img.width, img.height);
        return canvas;
    }
    $.fn.imageColorPicker = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Метод с именем ' + method + ' не существует для jQuery.imagecolorpicker');
        }
    }
})(jQuery);