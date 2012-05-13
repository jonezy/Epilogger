

head.ready(function () {
    if ($(".topPhotosBar").length > 0) {
        $('.topPhotosBar:first').show();
        getComments($('.topPhotosListLink:first').attr("id"));
        $('.topPhotosListLink:first').addClass('active');

        start_Int();
    }
});

var intval = "";
function start_Int() {
    if (intval == "") {
        intval = window.setInterval(ChangeToNextPhoto, 10000);
    } else {
        stop_Int();
    }
}
function stop_Int() {
    if (intval != "") {
        window.clearInterval(intval);
        intval = "";
    }
}


$('.topPhotosListLink').click(function (e) {
    e.preventDefault();

    stop_Int();

    changePhoto(this, this.id, 100);

});

function changePhoto(item, theID, speed) {

    $('.topPhotosBar:visible').fadeOut(speed, function () {
        getComments(theID);
        $('.topPhotosListLink').removeClass('active');
        $(item).addClass('active');
        $('#TopPhoto' + theID).fadeIn(speed);
    });

}


function ChangeToNextPhoto() {

    if ($('.topPhotosListLink.active').parent().next("li").find("a").length > 0) {
        changePhoto($('.topPhotosListLink.active').parent().next("li").find("a"), $('.topPhotosListLink.active').parent().next("li").find("a").attr("id"), 1000);
    }
    else {
        changePhoto($('.topPhotosListLink:first'), $('.topPhotosListLink:first').attr("id"), 1000);
    }

}

//This fills in the Comments on the Image
function getComments(photoId) {
    
    $(".topPhotoComments").html("");
    $.ajax({
        url: '/Events/ImageCommentControl',
        type: 'GET',
        data: {
            imageid: photoId,
            eventid: EventID
        },
        contentType: 'html',
        success: function (data) {
            $('.topPhotoComments').html(data);
        },
        error: function () {
            alert("error");
        }
    });
    //@Html.Action("ImageCommentControl", "Events", new { eventId = Model.EventId, imageid = Model.Image.ID })
    
    
    
    
//    $(".topPhotoComments").html("");
//    $.get('/Events/GetImageComments/' + EventID + '/' + photoId,
//        function (data) {
//            $('.topPhotoComments').html(data);
//        }, "html");
}
