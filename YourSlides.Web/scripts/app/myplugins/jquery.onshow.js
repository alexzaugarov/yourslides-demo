(function ($) {
    console.log("lazyviewer");
    var defaults = {
        onShow: function () { },
        offset: {
            top: 0.5,
            bottom: 0.5
        },
        startTrack: true
    },
		isInViewport = function (offset) {
		    var el = this;

		    var rect = el.getBoundingClientRect();
		    return (
				rect.top + rect.height * offset.top >= 0 &&
				rect.bottom - rect.height * offset.bottom <= (window.innerHeight || document.documentElement.clientHeight)
			);
		},
		init = function (options) {
		    var settings = $.extend({}, defaults, options);
		    return this.each(function () {
		        var element = this;
		        var scrollHandler = function (event) {
		            var isTracking = $(element).data("is-tracking");
		            if (isInViewport.call(element, settings.offset) && isTracking === true) {
		                settings.onShow.call(element);
		            }
		        }
		        $(this).data("scroll-handler", scrollHandler);
		        $(this).data("is-tracking", settings.startTrack);
		        $(window).bind("scroll", scrollHandler)
				    .bind("resize", scrollHandler);
		        scrollHandler();
		    });
		},
        destroy = function () {
            $(window).unbind("scroll", $(this).data("scroll-handler"))
                .unbind("resize", $(this).data("scroll-handler"));
        },
		methods = {
		    init: init,
		    destroy: destroy
		};
    $.fn.onShow = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('Метод с именем ' + method + ' не существует для jQuery.lazyviewer');
        }
    }

})(jQuery);