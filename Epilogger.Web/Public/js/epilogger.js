

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





//AUTO refreshing up tweets on Event Page
jQuery(function ($) {
    function getPosts() {
        //Turns the Timer off while the page is updating
        toggleUpdates('unewposts', 'off');

        //Make the call get the JSON of new Tweets.
        $.ajax({
            type: "POST",
            url: "/Events/GetLastTweetsJSON/",
            data: "{Count:6,pageLoadTime:'" + pageLoadTime + "',EventID:" + EventID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (newTweets) {
                //Got new Tweets JSON object

                if (newTweets.lasttweettime != '') {
                    pageLoadTime = newTweets.lasttweettime;

                    if (isFirstFrontPage && (typeof newTweets.html != "undefined")) {
                        $("#tweetlist li.tweet:first").before(newTweets.html);
                        var newUpdatesLi = $("#tweetlist li.newupdates");
                        newUpdatesLi.slideDown();
                        var counter = 0;
                        //$('#posttext_error, #commenttext_error').hide();
                        newUpdatesLi.each(function (index) {
                            var thisId = $(this).attr("id");
                            vpostId = thisId.substring(thisId.indexOf('-') + 1);
                            postsOnPageQS += "&vp[]=" + vpostId;
                            if (!(thisId in postsOnPage)) postsOnPage.unshift(thisId);
                            if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#F1F8FA' }, 2500, function () { $(this).removeClass('newupdates'); titleCount(); }); }
                            counter++;
                        }); $("#tweetlist li.tweet").each(function (index) {
                            if (index >= maxItemsOnPage)
                            { $(this).slideUp(); }
                        });
                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('unewposts', 'on');

            }
        });
    }



    function isElementVisible(elem) {
        elem = $(elem); if (!elem.length) { return false; }
        var docViewTop = $(window).scrollTop(); var docViewBottom = docViewTop + $(window).height(); var elemTop = elem.offset().top; var elemBottom = elemTop + elem.height(); var isVisible = ((elemBottom >= docViewTop) && (elemTop <= docViewBottom) && (elemBottom <= docViewBottom) && (elemTop >= docViewTop)); return isVisible;
    }

    function toggleUpdates(updater, newStatus) {
        switch (updater) {
            case "unewposts": if (newStatus == 'on') { getPostsUpdate = setInterval(getPosts, updateRate); }
                else { clearInterval(getPostsUpdate); getPostsUpdate = '0'; }
                break;
        }
    }

    function titleCount() {
        if (isFirstFrontPage) { var n = $('div.newupdates').length; } else { var n = newUnseenUpdates; }
        if (n <= 0) { if (document.title.match(/\([\d+]\)/)) { document.title = document.title.replace(/(.*)\([\d]+\)(.*)/, "$1$2"); } } else { if (document.title.match(/\((\d+)\)/)) { document.title = document.title.replace(/\((\d+)\)/, "(" + n + ")"); } else { document.title = '(1) ' + document.title; } }
    }

    function autgrow(textarea, min) {
        var linebreaks = textarea.value.match(/\n/g); if (linebreaks != null && linebreaks.length + 1 >= min) { textarea.rows = (linebreaks.length + 1); }
        else { textarea.rows = min; }
    }
    $.ajaxSetup({ timeout: updateTimeout, cache: false, error: function () { toggleUpdates('unewposts', 'on'); } }); if (prologuePostsUpdates) { toggleUpdates('unewposts', 'on'); }
    $("#tweetlist li.tweet").each(function () { var thisId = $(this).attr("id"); vpostId = thisId.substring(thisId.indexOf('-') + 1); postsOnPage.push(thisId); postsOnPageQS += "&vp[]=" + vpostId; }); function removeYellow() {
        if (isFirstFrontPage) { $('#tweetlist li.newupdates').each(function () { if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#F1F8FA' }, { duration: 2500 }); $(this).removeClass('newupdates'); } }); }
        titleCount();
    }
    $(window).scroll(function () { removeYellow(); });
});