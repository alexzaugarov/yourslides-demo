define(["jquery", "viewer", "handlebars", "onshow"], function ($, viewer, handlebars) {
	var defaults = {
		modifyModelItem: function (item) {
			return item;
		},
		itemTemplateHtml: "",
		pageLength: 12,
		onAllItemsShowed: function () { }
	};

	return function (options) {
		var settings = $.extend({}, defaults, options);
		var itemTemplate;
		if (settings.itemTemplate) {
			itemTemplate = settings.itemTemplate;
		} else {
			itemTemplate = handlebars.compile(settings.itemTemplateHtml);
		}
		var $trackObject = settings.$trackObject,
            v = viewer({
            	pageStart: settings.pageStart,
            	pageLength: settings.pageLength,
            	container: settings.container,
            	onPageFull: function () {
            		$trackObject.data("is-tracking", true);
            		$(window).trigger("scroll");
            	},
            	onCreateTemplate: function (item) {
            		return itemTemplate(settings.modifyModelItem(item));
            	},
            	onAllItemsShowed: function () {
            		settings.onAllItemsShowed.call(settings.container);
            		//$trackObject.data("is-tracking", true);
            	}
            });
		$trackObject.onShow({
			startTrack: false,
			onShow: function () {
				$trackObject.data("is-tracking", false);
				v.nextPage();
			}
		});
		if (settings.data) {
			v.addRange(settings.data);
		}
		return v;
	}
});