//featuredEvent

$(document).ready(function() {
    var randomNum = Math.ceil(Math.random() * 2);
    $("#homepageMainSpread").attr({ 'style': 'background: #000 url(/Public/Images/homepage/fullGBImage' + randomNum + '.png) center center;' });
});


head.ready(function () {
    if ($(".featuredEvent").length > 0) {
        $('.featuredEvent:first').show();
        $('.featuredPhotosListLink:first').addClass('active');
        start_Int();
    }
});

var intval = "";
function start_Int() {
    if (intval == "") {
        intval = window.setInterval(ChangeToNextPhoto, 2000);
    } else {
        stop_Int();
    }
    return false;
}
function stop_Int() {
    if (intval != "") {
        window.clearInterval(intval);
        intval = "";
    }
    return false;
}


$('.featuredPhotosListLink').click(function (e) {
    e.preventDefault();
    stop_Int();
    changePhoto(this, this.id, 100);
    return false;
});

function changePhoto(item, theID, speed) {

    $('.featuredEvent:visible').fadeOut(speed, function () {
        $('.featuredPhotosListLink').removeClass('active');
        $(item).addClass('active');
        $('#featuredPage' + theID).fadeIn(speed);
        return false;
    });
}


function ChangeToNextPhoto() {

    if ($('.featuredPhotosListLink.active').parent().next("li").find("a").length > 0) {
        changePhoto($('.featuredPhotosListLink.active').parent().next("li").find("a"), $('.featuredPhotosListLink.active').parent().next("li").find("a").attr("id"), 500);
    }
    else {
        changePhoto($('.featuredPhotosListLink:first'), $('.featuredPhotosListLink:first').attr("id"), 500);
    }
}









//head.ready(function () {
//    if ($(".featurePhotoBar").length > 0) {
//        $('.featurePhotoBar:first').show();
//        $('.featuredPhotosListLink:first').addClass('active');
//        start_Int();
//    }
//});

//var intval = "";
//function start_Int() {
//    if (intval == "") {
//        intval = window.setInterval(ChangeToNextPhoto, 5000);
//    } else {
//        stop_Int();
//    }
//}
//function stop_Int() {
//    if (intval != "") {
//        window.clearInterval(intval);
//        intval = "";
//    }
//}


//$('.featuredPhotosListLink').click(function (e) {
//    e.preventDefault();
//    stop_Int();
//    changePhoto(this, this.id, 100);
//});

//function changePhoto(item, theID, speed) {

//    $('.featurePhotoBar:visible').fadeOut(speed, function () {
//        $('.featuredPhotosListLink').removeClass('active');
//        $(item).addClass('active');
//        $('#FeaturedPhoto' + theID).fadeIn(speed);
//    });

//}


//function ChangeToNextPhoto() {

//    if ($('.featuredPhotosListLink.active').parent().next("li").find("a").length > 0) {
//        changePhoto($('.featuredPhotosListLink.active').parent().next("li").find("a"), $('.featuredPhotosListLink.active').parent().next("li").find("a").attr("id"), 500);
//    }
//    else {
//        changePhoto($('.featuredPhotosListLink:first'), $('.featuredPhotosListLink:first').attr("id"), 500);
//    }

//}

