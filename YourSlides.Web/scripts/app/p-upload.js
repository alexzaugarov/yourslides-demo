requirejs(["jquery", "fineuploader", "jqueryui"], function ($, qq) {


    $(function () {
        var uploader = new qq.FineUploader({
            debug: true,
            element: $(".p-upload__fine-uploader")[0],
            multiple: false,
            request: {
                endpoint: "/presentation/uploadfile"
            },
            retry: {
                enableAuto: false
            },
            validation: {
                acceptFiles: ".pdf",
                sizeLimit: 52428800
            },
            callbacks: {
                onSubmitted: function (id, name) {
                    $(".p-upload__upload-form").addClass("ys-hide");
                    $(".p-upload__file-upload-progressbar").removeClass("ys-hide");
                },
                onProgress: function (id, name, current, total) {
                    var progress = Math.round((current / total) * 100);
                    $("#file-uploading-progress-bar-ui").progressbar("option", "value", progress)
                        .children(".progress-bar__label")
                        .html(progress + "%");
                },
                onComplete: function(id, name, response, xhr) {
                    if(response.success) {
                        window.location.assign(response.url);
                    }
                },
                onError: function(id, name, error, xhr) {
                    $(".p-upload__error-message").removeClass("ys-hide")
                        .text(qq.format("При загрузке файла {} произошла ошибка. Причина: {}", name, error));
                }
            },
            text: {
                defaultResponseError: "неизвестная ошибка",
                fileInputTitle: "Форма загрузки"
            },
            messages: {
                emptyError: "файл пуст",
                onLeave: "Файл еще загружается. Если вы закроете страницу сейчас загрузка будет отменена.",
                sizeError: "превышен размер файла"
            }
        });
        $("#file-uploading-progress-bar-ui").progressbar({
            value: false
        });

    });
});