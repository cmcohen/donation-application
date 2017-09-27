$(function () {

    var isEmailValid = false;
    var isPasswordMatch = false;
    $("#email").on('keyup', function () {
        $.get('/account/CheckEmailExists', { email: $("#email").val() }, function (result) {
            isEmailValid = !result.exists;
            console.log(result.exists);
            if (isEmailValid) {
                $("#email-error").hide();
            } else {
                $("#email-error").show();
            }
            setSubmitButton();
        });
    });

    $("#password, #confirm").on('keyup', function () {
        isPasswordMatch = $("#password").val() == $("#confirm").val();
        if (isPasswordMatch) {
            $("#password-error").hide();
        } else {
            $("#password-error").show();
        }
        setSubmitButton();
    });

    function setSubmitButton() {
        $("#btn-submit").prop('disabled', !isEmailValid || !isPasswordMatch);
    }
})

