

//Background loader
//Not sure if this is still to be done
/*
$(document).ready(function () {
    var rndNum = Math.ceil(Math.random() * 7);
    $("body").css({ background: "url(/Public/images/bg/sample" + rndNum + ".jpg) #222 no-repeat center center fixed" });
});
*/

head.ready(function () {
    $(".dob").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "-90:+0"
    });
    $(".dob").formatDate('yyyy-mm-dd');
});

//This fills in the Comments on the Image popups (tweets)
function changeDescription() {
    var photoID = $(".pp_description").html();
    $(".pp_description").html("");

    $.get('/Events/GetImageComments/' + photoID,
                function (data) {
                    $('.pp_Comments').html(data);
                }, "html");
}