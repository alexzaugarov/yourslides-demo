define(["jquery"], function ($) {
    var defaults = {
        pageStart: 0,
        pageLength: 12,
        container: $("<div/>")[0],
        onPageNext: function () { },
        onCreateTemplate: function (item) { },
        onPageFull: function () { },
        onAllItemsShowed: function () { }
    };
    function getFirstChild(el) {
        var firstChild = el.firstChild;
        while (firstChild != null && firstChild.nodeType == 3) { // skip TextNodes
            firstChild = firstChild.nextSibling;
        }
        return firstChild;
    }
    return function (options) {
        var settings = $.extend({}, defaults, options),
            items = Array(),
            current = 0,// количество показанных
            n = settings.pageLength,// количество на странице
            page = settings.pageStart,// текущая страница, 0 если ничего не показывается
            show = function () {
                var itemsLength = items.length;
                if (current < page * n && current < itemsLength) {
                    var fragment = document.createDocumentFragment(),
                     div;
                    for (; current < page * n && current < itemsLength ; current++) {
                        div = document.createElement("div");
                        div.innerHTML = settings.onCreateTemplate.call(settings.container, items[current]);
                        fragment.appendChild(getFirstChild(div));
                    }
                    settings.container.appendChild(fragment);
                }
                if (itemsLength > page * n) {
                    settings.onPageFull.call(settings.container);
                }
                if(current === itemsLength) {
                    settings.onAllItemsShowed.call(settings.container);
                }
            },
            add = function (item) {
                items.push(item);
                show();
            },
            addRange = function (addingItems) {
                var length = addingItems.length;
                for (var i = 0; i < length; i++) {
                    items.push(addingItems[i]);
                }
                show();
            };
        return {
            add: add,
            addRange: addRange,
            nextPage: function () {
                if (current < items.length) {
                    page++;
                    show();
                }
            },
            reset: function() {
                $(settings.container).empty();
                items = Array();
                current = 0;
                n = settings.pageLength;
                page = settings.pageStart;
            }
        }
    }
});