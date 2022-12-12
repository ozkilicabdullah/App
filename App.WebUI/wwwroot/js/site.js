
// Alert pop'up fonk.
function SwalGenerate(icon, text, title, buttontext) {
    var ButtonColor = '#a5dc86';

    if (icon == "error") {
        ButtonColor = "#f27474";
    }
    else if (icon == "warning") {
        ButtonColor = "#f8bb86";
    }

    var message = "";

    if ($.isArray(text)) {
        if (text != undefined && text.length > 0) {
            for (var i = 0; i < text.length; i++) {
                message += text[i] + "<br />";
            }
        }
    }
    else {
        message = text;
    }

    Swal.fire({
        title: title,
        html: message,
        icon: icon,
        confirmButtonText: buttontext,
        confirmButtonColor: ButtonColor
    });
}

$(document).ready(function () {
    if ($(".beginDate-area").length > 0) {
        flatpickr('.beginDate-area #beginDate', {
            weekNumbers: true,
        });
    }
    if ($(".endDate-area").length > 0) {
        flatpickr('.endDate-area #endDate', {
            weekNumbers: true,
        });
    }
});
// Login
$(document).on("click", ".LoginFormContainer  .LoginForm .button-group .login", function () {
    var form = $(".LoginFormContainer .LoginForm");
    form.validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
            }
        }
    });
    if (form.valid()) {
        FormRequest(form);
    }
});
// Register
$(document).on("click", ".RegisterFormContainer .RegisterForm .button-group .register", function () {
    var form = $(".RegisterFormContainer .RegisterForm");
    form.validate({
        rules: {
            Name: {
                required: true
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
            }
        },
    });
    if (form.valid()) {
        FormRequest(form);
    }
});
// Forget my password
$(document).on("click", ".ForgetMyPasswordFormContainer .ForgetMyPaswordForm .button-group .forgetMyPassword", function () {
    var form = $(".ForgetMyPasswordFormContainer .ForgetMyPaswordForm");
    form.validate({
        rules: {
            Email: {
                required: true,
                email: true
            }
        },
    });
    if (form.valid()) {
        FormRequest(form);
    }
});
// Change Password
$(document).on("click", ".ChangePasswordFormContainer .ChangePasswordForm .button-group .changePassword", function () {
    var form = $(".ChangePasswordFormContainer .ChangePasswordForm");
    form.validate({
        rules: {
            Password: {
                required: true
            }
        },
    });
    if (form.valid()) {
        FormRequest(form);
    }
});


$(document).on("click", ".UserReportFilter .LoginForm .button-group .userReportFilterBtn", function () {
    var beginDate = $(".beginDate-area #beginDate").val();
    var endDate = $(".endDate-area #endDate").val();
    window.location.href = "/Home/Index?BeginDate=" + beginDate + "&EndDate=" + endDate;

});
// From istekleri
function FormRequest(form) {
    GenerateLocalLoading(form, "show");
    $.ajax({
        type: form.attr("method"),
        url: form.attr("action"),
        data: form.serialize(),
        dataType: "json",
        success: function (response) {
            GenerateLocalLoading(form, "hide");
            if (response.success) {
                if (response.isRedirect) {
                    window.location.href = response.message;
                } else {
                    SwalGenerate("success", response.message, "Operation Success", "Close");
                }
            } else {
                var message = "";
                response.errors.forEach(function (item, index) {
                    message = message + " " + item
                });
                SwalGenerate("error", message, "Operation Failed", "Close");
            }
        },
        error: function () {
            GenerateLocalLoading(form, "hide");
            SwalGenerate("error", "Bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.", "Operation Failed", "Close");
        }
    });
}

function GenerateLocalLoading(ParentElement, operation) {
    var AddingOverlayHtml = '<div class="loading-overlay"><i class="button-loader"></i></div>';

    if (operation == "show") {
        if (ParentElement.find("loading-overlay").length == 0) {
            ParentElement.append(AddingOverlayHtml);
        }
    }
    else if (operation == "hide") {
        ParentElement.find(".loading-overlay").remove();
    }
}