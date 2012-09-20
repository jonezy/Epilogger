

head.ready(function () {

//    var button = $('#uploadavator')[0];
//    var uploader = new qq.FileUploader({
//        element: button,
//        allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
//        sizeLimit: 2147483647, // max size
//        action: '/Account/UploadAvatar',
//        multiple: false,
//        uploadButtonText: 'upload an avatar',
//        onComplete: function (id, fileName, responseJSON) {
//            $("#theProfileImage").attr("src", responseJSON.imageurl);
//            $("#ProfileImage").val(responseJSON.imageurl);
//            $("#DisplayProfileImage").val(responseJSON.imageurl);
//        }
//    });


    var button2 = $('#profileuploadavator')[0];
    var uploader2 = new qq.FileUploader({
        element: button2,
        allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
        sizeLimit: 2147483647, // max size
        action: '/Account/UploadAvatar',
        multiple: false,
        uploadButtonClass: 'blue-action-button',
        uploadButtonText: 'SELECT FILE',
        onComplete: function (id, fileName, responseJSON) {
            $("#theProfileImage").attr("src", responseJSON.imageurl);
            $("#ProfileImage").val(responseJSON.imageurl);
            $("#DisplayProfileImage").val(responseJSON.imageurl);
        }
    });



    var usernameValid = false;

    checkIfCreateIsDisabled();

    $('#Username').live('keyup', function () {
        $("#Username").val($("#Username").val().replace(/^\s+|\s+$/g, "")); // trim leading and trailing spaces
        $("#Username").val($("#Username").val().replace(/[_|\s]+/g, "")); // no spaces
        $("#Username").val($("#Username").val().replace(/[^a-zA-Z0-9-]+/g, "")); // remove all non-alphanumeric characters except the hyphen


        var value = $("#Username").val();
        setTimeout(function () {
            if ($("#Username").val() == value) {

                if ($("#Username").val() != "Username" && $("#Username").val() != "") {
                    $("#usernameCheck").html("Checking...&nbsp;&nbsp;<img src='/Public/images/signup/createAccountLoader.gif' alt=''/>");
                    $("#usernameCheck").show();

                    $.ajax({
                        type: "POST",
                        url: "/Account/CheckUsername",
                        data: "{username:'" + $("#Username").val() + "'}",
                        contentType: "application/json; charset=utf-8",
                        success: function (isUserOk) {
                            if (isUserOk == "True") {
                                $("#usernameCheck").html("<img src='/Public/images/signup/checkmark.gif' alt='' />&nbsp;&nbsp; Username is available!");
                                usernameValid = true;
                                checkIfCreateIsDisabled();
                            } else {
                                $("#usernameCheck").html("Username " + $('#Username').val() + " is not available!");
                                $(".submitForm").attr('disabled', 'disabled');
                                usernameValid = false;
                                checkIfCreateIsDisabled();
                            }
                        }
                    });


                } else {
                    $("#usernameCheck").hide();
                }
            }
            checkIfCreateIsDisabled();
        }, 500);

        checkIfCreateIsDisabled();
    });


    $('#EmailAddress').live('keyup', function () {

        var value = $("#EmailAddress").val();
        setTimeout(function () {
            if ($("#EmailAddress").val() == value) {

                if ($("#EmailAddress").val() != "Email Address" && $("#EmailAddress").val() != "") {

                    if (isValidEmailAddress($("#EmailAddress").val())) {

                        $("#emailCheck").html("Checking...&nbsp;&nbsp;<img src='/Public/images/signup/createAccountLoader.gif' alt=''/>");
                        $("#emailCheck").show();

                        $.ajax({
                            type: "POST",
                            url: "/Account/CheckEmailAddress",
                            data: "{emailaddress:'" + $("#EmailAddress").val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            success: function (isUserOk) {
                                if (isUserOk == "True") {
                                    $("#emailCheck").html("<img src='/Public/images/signup/checkmark.gif' alt='' />&nbsp;&nbsp; Email Address is available!");
                                    checkIfCreateIsDisabled();
                                } else {
                                    $("#emailCheck").html("This email address already exists in Epilogger, would you like to <a href='/login'>login</a> or <a href='/account/forgotpassword'>recover your password</a>.");
                                    $(".submitForm").attr('disabled', 'disabled');
                                    checkIfCreateIsDisabled();
                                }
                            }
                        });


                    } else {
                        $("#emailCheck").hide();
                    }

                } else {
                    $("#emailCheck").hide();
                }
            }
            checkIfCreateIsDisabled();
        }, 500);

        checkIfCreateIsDisabled();
    });

    $('#Password').live('keyup', function () {
        checkIfCreateIsDisabled();
    });

    $("#d_DOB").change(function () {
        checkIfCreateIsDisabled();
    });
    $("#m_DOB").change(function () {
        checkIfCreateIsDisabled();
    });
    $("#y_DOB").change(function () {
        checkIfCreateIsDisabled();
    });


    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    };


    function checkIfCreateIsDisabled() {

        if ($("#Username").val() != "Username" &&
                    $("#EmailAddress").val() != "Email Address" &&
                    $("#Password").val() != "Password" &&
                    isValidEmailAddress($("#EmailAddress").val()) == true &&
                    usernameValid == true &&
                    $("#d_DOB option:selected").text() != "Day" &&
                    $("#m_DOB option:selected").text() != "Month" &&
                    $("#y_DOB option:selected").text() != "Year") {

            $(".submitForm").removeClass("disabled");
            $(".submitForm").removeAttr("disabled");
        } else {
            $(".submitForm").addClass("disabled");
            $(".submitForm").attr('disabled', 'disabled');
        }

    }


    if ($("#Username").val() != "Username") {
        $('#Username').trigger('keyup');
    }


    checkIfCreateIsDisabled();

});

function popTheWindow(url) {
    window.open(url, 'twitterAuth', "width=400,height=300,scrollbars=no");
    return false;
}

