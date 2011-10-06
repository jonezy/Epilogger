

head.ready(function () {
    $('.HottestEventBig:first').show();
    start_Int();
});

var intval = "";
function start_Int() {
    if (intval == "") {
        intval = window.setInterval(ChangeToNextHottest, 5000);
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


function ChangeToNextHottest() {

    if ($('.HottestEventBig:visible').next().length > 0) {
        var me = $('.HottestEventBig:visible').next();
        $('.HottestEventBig:visible').fadeOut(1000, function () {
            me.fadeIn(1000);
        });
    }
    else {
        $('.HottestEventBig:visible').fadeOut(1000, function () {
            $('.HottestEventBig:first').fadeIn(1000);
        });
    }

}


$('.HottestListLink').click(function (e) {
    e.preventDefault();
    stop_Int();
    theID = this.id;
    $('.HottestEventBig:visible').fadeOut(100, function () {
        $('#HotestEventDetails' + theID).fadeIn(100);
    });

});









$('.browseTabs').click(function (e) {
    e.preventDefault();
    $('.browseTabs').removeClass("active");
    $(".BEventListContainer").html("Loading...");

    $.ajax({
        type: "POST",
        url: "/Events/GetBrowseOverviewTabData",
        data: "{Tab:" + e.target.id + "}",
        contentType: "application/json; charset=utf-8",
        success: function (returnedHTML) {
            $(".BEventListContainer").html(returnedHTML);
            $(e.target).addClass("active");
        }
    });

});
