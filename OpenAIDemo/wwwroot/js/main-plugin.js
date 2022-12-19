; (function ($) {
    "use strict";
    $.fn.getWebControls = function (value) {
        var data = {};
        if (value && typeof value == 'object') {
            data = value;
        }
        $(this).find("[col]").each(function (i, control) {
            var id = $(control).attr("id");
            var field = $(control).attr("col");

            if (control.tagName == "INPUT") {
                if (control.type == "checkbox") {
                    if ($(control).prop("checked")) {
                        if (data[field]) {
                            data[field] = data[field] + "," + $(control).val();
                        } else {
                            data[field] = $(control).val();
                        }
                    }
                }
                else if (control.type == "radio") {
                    if ($(control).prop("checked")) {
                        data[field] = $(control).val();
                    }
                }
                else {
                    data[field] = $(control).val();
                }
            }
            else if (control.tagName == "SELECT") {
                data[field] = $(control).val();
            }
            else if (control.tagName == "DIV") {
                if ($(control).find("#" + id + "_tree").length > 0) {
                    data[field] = $(control).ysComboBoxTree("getValue");
                } else if ($(control).find("#" + id + "_select").length > 0) {
                    data[field] = $(control).ysComboBox("getValue");
                }
                else if ($(control).find("input[type=checkbox]").length > 0) {
                    data[field] = $(control).ysCheckBox("getValue");
                } else if ($(control).find("input[type=radio]").length > 0) {
                    data[field] = $(control).ysRadioBox("getValue");
                } else {
                    data[field] = $(control).html();
                }
            }
            else if (control.tagName == "IMG") {
                data[field] = $(control).prop("src");
            }
            else if (control.tagName == "SPAN") {
                if ($(control).find("#" + id + "_select").length > 0) {
                    data[field] = $(control).ysComboBox("getValue");
                } else {
                    data[field] = $(control).html();
                }
            }
            else if (control.tagName == "TEXTAREA") {
                data[field] = $(control).val();
            }
        });
        return data;
    };
    $.fn.setWebControls = function (data) {
        $(this).find("[col]").each(function (i, control) {
            var id = $(control).attr("id");
            var field = $(control).attr("col");
            if (control.tagName == "INPUT") {
                if (control.type == "checkbox") {
                    if ($(control).val() == data[field]) {
                        $(control).prop("checked", "checked");
                    }
                }
                else if (control.type == "radio") {
                    if ($(control).val() == data[field]) {
                        if ($(control).iCheck) {
                            $(control).iCheck('check');
                        }
                        else {
                            $(control).prop("checked", true);
                        }
                    }
                }
                else {
                    $(control).val(data[field]);
                }
            }
            else if (control.tagName == "SELECT") {
                $(control).val(data[field]);
            }
            else if (control.tagName == "DIV") {
                if ($(control).find("#" + id + "_tree").length > 0) {
                    $(control).ysComboBoxTree("setValue", data[field]);
                } else if ($(control).find("#" + id + "_select").length > 0) {
                    $(control).ysComboBox("setValue", data[field]);
                } else if ($(control).find("input[type=checkbox]").length > 0) {
                    $(control).ysCheckBox("setValue", data[field]);
                } else if ($(control).find("input[type=radio]").length > 0) {
                    $(control).ysRadioBox("setValue", data[field]);
                } else {
                    $(control).html(data[field]);
                }
            }
            else if (control.tagName == "SPAN") {
                $(control).html(data[field]);
            }
            else if (control.tagName == "TEXTAREA") {
                $(control).val(data[field]);
            }
        });
        return data;
    }
})(window.jQuery);