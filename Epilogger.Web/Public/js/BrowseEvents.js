

head.ready(function () {
    $('.HottestEventBig:first').show();
    $('.HottestListLink:first').addClass("active");
    
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


function changeHottest(item, theID, speed) {

    $('.HottestEventBig:visible').fadeOut(speed, function () {
        $('.HottestListLink').removeClass('active');
        $(item).addClass('active');
        $('#HotestEventDetails' + theID).fadeIn(speed);
    });

}

function ChangeToNextHottest() {
    if ($('.HottestListLink.active').parent().next("li").find("a").length > 0) {
        changeHottest($('.HottestListLink.active').parent().next("li").find("a"), $('.HottestListLink.active').parent().next("li").find("a").attr("id"), 1000);
    }
    else {
        changeHottest($('.HottestListLink:first'), $('.HottestListLink:first').attr("id"), 1000);
    }
}

$('.HottestListLink').click(function (e) {
    e.preventDefault();
    stop_Int();
    changeHottest(this, this.id, 800);
});

$('.browseTabs').click(function (e) {
    e.preventDefault();
    $(".BEventListContainer").css('margin-top', '10px');
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
