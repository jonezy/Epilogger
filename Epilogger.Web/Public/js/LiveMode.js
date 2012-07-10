﻿

var prologuePostsUpdates = true;

// Ajax activity indicator bound to ajax start/stop document events
$(document).ajaxStart(function () {
    $('#ajaxBusy').show();
}).ajaxStop(function () {
    $('#ajaxBusy').hide();
});


jQuery(function ($) {
    var column = 1;
    var stopAnimation = false;

    //For Testing
    $("#updateLink").bind('click', function (e) {
        e.preventDefault();
        //getTweets();

        toggleUpdates('newphotos', 'on');
        getPhotos();
    });
    $("#stopLink").bind('click', function (e) {
        e.preventDefault();
        stopAnimation = true;
    });


    //AUTO refreshing tweets for Live Mode
    function getTweets() {
        //Turns the Timer off while the page is updating
        toggleUpdates('newtweets', 'off');
        stopAnimation = true;

        //Make the call get the JSON of new Tweets.
        $.ajax({
            type: "POST",
            url: "/events/LiveGetLastTweetsJson/",
            data: "{count:18,pageLoadTime:'" + pageLoadTime + "',eventID:" + EventID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (newTweets) {
                //Got new Tweets JSON object
                //numberofnewtweets
                //lasttweettime
                //tweetcount
                //tweetsInhtml

                if (newTweets.lasttweettime != '') {
                    pageLoadTime = newTweets.lasttweettime;

                    if (typeof newTweets.tweetsInhtml != "undefined") {

                        stopAnimation = false;
                        animateTweets(newTweets.tweetsInhtml);

                        //Update the page Totals
                        $("#tweetCount").html(newTweets.epiloggerCounts.TweetCount);
                        $("#photoCount").html(newTweets.epiloggerCounts.PhotoCount);
                        $("#uniqueCount").html(newTweets.epiloggerCounts.UniqueTweeterCount);
                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('newtweets', 'on');


                //toggleUpdates('newphotos', 'on');
            }
        });
    }

    toggleUpdates('newtweets', 'on');
    getTweets();


    function animateTweets(tweetList) {

        //var d = 1;
        //var tweetList = newTweets.tweetsInhtml;
        //var tweetList = $("#divStaging .tweet");
        var i = -1;
        var animationCallback = null;
        animationCallback = function () {
            //tweetList = $("#divStaging .tweet");
            if (++i < tweetList.length) {
                if (stopAnimation) return false;

                var colName = "#tweetColumn" + column;
                $(colName).find(".tweet:first").before(tweetList[i]);
                var newElement = $(".newupdates");
                column++;
                if (column == 4) column = 1;

                var itemHeight2 = $(newElement).height() - 80; //-80; // -40;


                $(newElement).css("margin-top", "-" + itemHeight2 + "px");

                $(newElement).animate({ 'margin-top': '0px' }, 'slow');

                $(newElement).removeClass('newupdates');
                $(newElement).parent().find(".tweet:last").fadeOut('slow', function () {
                    $(newElement).parent().find(".tweet:last").remove();

                    $(newElement).delay(2000).fadeIn(0, function () {
                        animationCallback();
                    });
                });
            }
            else {
                i = -1;
                animationCallback();
            }
        };

        animationCallback();
    }

    animateTweets();





    //AUTO refreshing Photos on Event Page
    function getPhotos() {
        //Turns the Timer off while the page is updating
        toggleUpdates('newphotos', 'off');

        //Make the call get the JSON of new photos.
        $.ajax({
            type: "POST",
            url: "/Events/LiveGetLastPhotosJson/",
            data: "{Count:5,pageLoadTime:'" + photoPageLoadTime + "',EventID:" + EventID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (newPhotos) {
                //Got new Photos JSON object

                if (newPhotos.lastphototime != '') {

                    //Top Tweeters
                    $("#topTweets").html(newPhotos.topTweeters);

                    photoPageLoadTime = newPhotos.lastphototime;

                    if (typeof newPhotos.html != "undefined") {

                        $("#photos div.images:first").before(newPhotos.html);

                        $("#photos br").remove();

                        var allPhotos = $(".images");

                        allPhotos.removeClass('imgLarge');
                        allPhotos.removeClass('imgSmall');


                        allPhotos.each(function (index) {

                            if (index <= 1) {
                                $(this).addClass("imgLarge");
                                $(this).find("img").width(350);
                            } else {
                                if (index == 2) { $(this).before('<br style="clear:both"" />'); }
                                $(this).addClass("imgSmall");
                                $(this).find("img").width(150);
                            }

                            if (index <= 4) {
                                $(this).fadeIn(2500, function () { $(this).removeClass('newImage'); });
                            } else {
                                $(this).hide();
                                $(this).remove();
                            }


                        });


                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('newphotos', 'on');

            },
            error: function (xhr, ajaxOptions, thrownError) {
                //                alert(xhr.status);
                //                alert(thrownError);
            }
        });
    }

    toggleUpdates('newphotos', 'on');
    getPhotos();



    //    function isElementVisible(elem) {
    //        elem = $(elem); if (!elem.length) { return false; }
    //        var docViewTop = $(window).scrollTop(); var docViewBottom = docViewTop + $(window).height(); var elemTop = elem.offset().top; var elemBottom = elemTop + elem.height(); var isVisible = ((elemBottom >= docViewTop) && (elemTop <= docViewBottom) && (elemBottom <= docViewBottom) && (elemTop >= docViewTop)); return isVisible;
    //    }
    var getNewTweets = '0';
    var getPhotosUpdate = '0';
    function toggleUpdates(updater, newStatus) {
        switch (updater) {
            case "newtweets":
                if (newStatus == 'on') {
                    getNewTweets = setInterval(getTweets, updateRate);
                }
                else {
                    clearInterval(getNewTweets); getNewTweets = '0';
                }
                break;
            case "newphotos":
                if (newStatus == 'on') {
                    getPhotosUpdate = setInterval(getPhotos, photoUpdateRate);
                }
                else {
                    clearInterval(getPhotosUpdate); getPhotosUpdate = '0';
                }
                break;
        }
    }



    //    function titleCount() {
    //        if (isFirstFrontPage) { var n = $('li.newupdates').length; } else { var n = newUnseenUpdates; }
    //        if (n <= 0) { if (document.title.match(/\([\d+]\)/)) { document.title = document.title.replace(/(.*)\([\d]+\)(.*)/, "$1$2"); } } else { if (document.title.match(/\((\d+)\)/)) { document.title = document.title.replace(/\((\d+)\)/, "(" + n + ")"); } else { document.title = '(1) ' + document.title; } }
    //    }

    //    function autgrow(textarea, min) {
    //        var linebreaks = textarea.value.match(/\n/g); if (linebreaks != null && linebreaks.length + 1 >= min) { textarea.rows = (linebreaks.length + 1); }
    //        else { textarea.rows = min; }
    //    }


    $.ajaxSetup({
        timeout: updateTimeout,
        cache: false,
        error: function ()
        { toggleUpdates('newtweets', 'on'); toggleUpdates('newphotos', 'on'); }
    });

    //    //Photos
    //    $.ajaxSetup({
    //        timeout: updateTimeout,
    //        cache: false,
    //        error: function ()
    //        { toggleUpdates('newphotos', 'on'); }
    //    });





    //    if (prologuePostsUpdates) { toggleUpdates('newtweets', 'on'); }

    //    if (disableAutoupdate) {
    //        toggleUpdates('newtweets', 'off');
    //    }
    //    else {
    //        toggleUpdates('newtweets', 'on');
    //    }

    //    $("#tweetlist li.tweet").each(function () {
    //        var thisId = $(this).attr("id");
    //        vpostId = thisId.substring(thisId.indexOf('-') + 1);
    //        postsOnPage.push(thisId);
    //        postsOnPageQS += "&vp[]=" + vpostId;
    //    });

    //    function removeYellow() {
    //        if (isFirstFrontPage) {
    //            $('#tweetlist li.newupdates').each(function () {
    //                if (isElementVisible(this)) {
    //                    $(this).animate({ backgroundColor: '#FFF' }, { duration: 2500 });
    //                    $(this).removeClass('newupdates');
    //                }
    //            });
    //        }
    //    }

    //    $(window).scroll(function () { removeYellow(); });

});




