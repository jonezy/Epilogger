

head.ready(function () {
    $('.HottestEventBig:first').show();
//    $('.HottestEventBig:first').addClass('HottestEventBigActive');
//    $('.HottestEventBig:first').css("margin-left", '0px');
});

$('.HottestListLink').click(function () {


//    theID = this.id;

//    $('.HottestEventBigActive').animate({
//        marginLeft: '-600px',
//        opacity: 'toggle'
//    }, 200, function () {
//        // Animation complete.
//        $('#HottestEventDetails' + this.id).css("margin-left", '600px')
//        $('#HottestEventDetails' + this.id).removeClass('HottestEventBigActive');

//        $('#HotestEventDetails' + theID).animate({
//            marginLeft: '0px',
//            opacity: 'toggle'
//        }, 200, function () {
//            // Animation complete.
//            $('#HotestEventDetails' + theID).addClass('HottestEventBigActive');
//        });

//    });


    //This is the working shit. laeve if for now.
    theID = this.id;
    $('.HottestEventBig:visible').fadeOut(100, function () {
        $('#HotestEventDetails' + theID).fadeIn(100);
    });
    

});