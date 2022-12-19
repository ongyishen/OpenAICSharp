; window.ys = {};
(function ($, ys) {
    "use strict";
    $.extend(ys, {
        msgWarning: function (content) {
            layer.msg(content, { icon: 0, time: 1000, shift: 5 });
        },
        msgSuccess: function (content) {
            if (ys.isNullOrEmpty(content)) {
                content = "Operation Success";
            }
            top.layer.msg(content, { icon: 1, time: 1000, shift: 5 });
        },
        msgError: function (content) {
            if (ys.isNullOrEmpty(content)) {
                content = "Operation Failed";
            }
            layer.msg(content, { icon: 2, time: 3000, shift: 5 });
        },
        alertWarning: function (content) {
            layer.alert(content, {
                icon: 0,
                title: "System Alert",
                btn: ['OK'],
                btnclass: ['btn btn-primary'],
            });
        },
        alertSuccess: function (content) {
            layer.alert(content, {
                icon: 1,
                title: "System Alert",
                btn: ['OK'],
                btnclass: ['btn btn-primary'],
            });
        },
        alertError: function (content) {
            layer.alert(content, {
                icon: 2,
                title: "System Alert",
                btn: ['OK'],
                btnclass: ['btn btn-primary'],
            });
        },
        confirm: function (content, callback) {
            layer.confirm(content, {
                icon: 3,
                title: "System Alert",
                btn: ['OK', 'CANCEL'],
                btnclass: ['btn btn-primary', 'btn btn-danger'],
            }, function (index) {
                layer.close(index);
                callback(true);
            });
        },
        showLoading: function (message) {
            $.blockUI({ message: '<div class="loaderbox"><div class="loading-activity"></div> ' + message + '</div>', css: { border: "none", backgroundColor: 'transparent' } });
        },
        closeLoading: function () {
            setTimeout(function () { $.unblockUI(); }, 50);
        },
        openLink: function (href, target) {
            var a = document.createElement('a')
            if (target) {
                a.target = target;
            }
            else {
                a.target = '_blank';
            }
            a.href = href;
            a.click();
        },
        isNullOrEmpty: function (obj) {
            if ((typeof (obj) == "string" && obj == "") || obj == null || obj == undefined) {
                return true;
            }
            else {
                return false;
            }
        },
        // Html.Raw()方法会提示语法错误，所以用这个函数包装一下
        getJson: function (value) {
            return value;
        },
        ajax: function (option) {
            var opt = $.extend({
                url: option.url,
                async: true,
                type: "get",
                data: option.data || {},
                dataType: option.dataType || "json",
                error: function (xhr, status, obj) { ys.alertError("System Error"); },
                success: function (rdata) {
                    ys.msgSuccess();
                },
                beforeSend: function (xhr) {
                    ys.showLoading("Processing...");
                },
                complete: function (xhr, status) {
                    ys.closeLoading();
                }
            }, option);

            if (ys.isNullOrEmpty(opt.url)) {
                ys.alertError("url Empty");
                return;
            }
            $.ajax({
                url: opt.url,
                async: opt.async,
                type: opt.type,
                data: opt.data,
                dataType: opt.dataType,
                error: opt.error,
                success: opt.success,
                beforeSend: opt.beforeSend,
                complete: opt.complete,
            });
        }

    });
    

})(window.jQuery, window.ys);