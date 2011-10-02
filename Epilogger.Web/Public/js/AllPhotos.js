

head.ready(function () {
    $('.topPhotosBar:first').show();
    getComments($('.topPhotosListLink:first').attr("id"));
    $('.topPhotosListLink:first').addClass('active');

    //window.setInterval(ChangeToNextPhoto, 10000);
    
    
});

$('.topPhotosListLink').click(function (e) {
    e.preventDefault();

    $('.topPhotosListLink').removeClass('active');

    //This is the working shit. laeve if for now.
    theID = this.id;
    $('.topPhotosBar:visible').fadeOut(100, function () {
        $('#TopPhoto' + theID).fadeIn(100);
    });
    $(this).addClass('active');

    getComments(theID);

});

//function ChangeToNextPhoto() {
//    $('.topPhotosListLink.active:next').css(
////}

//This fills in the Comments on the Image
function getComments(photoID) {
    $(".topPhotoComments").html("");

    $.get('/Events/GetImageComments/' + photoID,
        function (data) {
            $('.topPhotoComments').html(data);
        }, "html");
}
