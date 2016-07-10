define(["jquery", "handlebars", "lazyviewer"], function ($, handlebars) {
    function getFirstChild(el) {
        var firstChild = el.firstChild;
        while (firstChild != null && firstChild.nodeType == 3) { // skip TextNodes
            firstChild = firstChild.nextSibling;
        }
        return firstChild;
    }
    return function (container) {
        var track,
            thumbnailItems = Array(),
            template = handlebars.compile($("#thumbnail-item-template").html()),
            current = 0,// количество показанных
            n = 12,// количество на странице
            page = 1,// текущая страница, 0 если ничего не показывается
            show = function () {
                if (current < page * n && current < thumbnailItems.length) {
                    var fragment = document.createDocumentFragment(),
                     div;
                    for (; current < page * n && current < thumbnailItems.length ; current++) {
                        div = document.createElement("div");
                        div.innerHTML = template(thumbnailItems[current]);
                        fragment.appendChild(getFirstChild(div));
                    }
                    container.appendChild(fragment);
                }
                if (thumbnailItems.length > page * n && $(container).data("is-tracking") !== true) {
                    track(true);
                }
            },
            add = function (item) {
                thumbnailItems.push(item);
                show();
            },
            addRange = function (items) {
                for (var i = 0; i < items.length; i++) {
                    thumbnailItems.push(items[i]);
                }
                show();
            },
            $trackObject = $("<div/>").insertAfter(container);
        track = function (state) {
            $trackObject.data("is-tracking", state);
            $(window).trigger("scroll");
        };
        $trackObject.lazyviewer({
            onShow: function() {
                track(false);
                page++;
                show();
            }
        });
        track(true);
        return {
            add: add,
            addRange: addRange
        }
    }
});