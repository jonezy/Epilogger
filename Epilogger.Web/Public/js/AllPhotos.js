

head.ready(function () {
    $('.topPhotosBar:first').show();
    getComments($('.topPhotosListLink:first').attr("id"));
    $('.topPhotosListLink:first').addClass('active');

    start_Int();

});

var intval = "";
function start_Int() {
    if (intval == "") {
        intval = window.setInterval(ChangeToNextPhoto, 5000);
    } else {
        stop_Int()
    }
}
function stop_Int() {
    if (intval != "") {
        window.clearInterval(intval)
        intval = ""
    }
}


$('.topPhotosListLink').click(function (e) {
    e.preventDefault();

    stop_Int();

    changePhoto(this, this.id, 100);

});

function changePhoto(item, theID, speed) {

    $('.topPhotosBar:visible').fadeOut(speed, function () {
        $('.topPhotosListLink').removeClass('active');
        $(item).addClass('active');
        $('#TopPhoto' + theID).fadeIn(speed);
    });
    

    getComments(theID);

}


function ChangeToNextPhoto() {

    if ($('.topPhotosListLink.active').parent().next("li").find("a").length > 0) {
        //$('.topPhotosListLink.active').parent().next("li").find("a").click()
        changePhoto($('.topPhotosListLink.active').parent().next("li").find("a"), $('.topPhotosListLink.active').parent().next("li").find("a").attr("id"), 1000);
    }
    else {
        changePhoto($('.topPhotosListLink:first'), $('.topPhotosListLink:first').attr("id"), 1000);
    }

}

//This fills in the Comments on the Image
function getComments(photoID) {
    $(".topPhotoComments").html("");

    $.get('/Events/GetImageComments/' + photoID,
        function (data) {
            $('.topPhotoComments').html(data);
        }, "html");
}
