﻿

var prologuePostsUpdates = true;

jQuery(function ($) {
    var column = 1;
    var stopAnimation = false;

    //For Testing
    $("#updateLink").bind('click', function (e) {
        e.preventDefault();
        getTweets();
    });
    $("#stopLink").bind('click', function (e) {
        e.preventDefault();
        //stopAnimation = true;
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



                        //There are new tweets, update the count
                        $("#tweetCount").text(newTweets.tweetcount);

                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('newtweets', 'on');

            }
        });
    }

    toggleUpdates('newtweets', 'on');
    getTweets();


    function animateTweets(tweetList2) {


        var d = 1;
        //var tweetList = newTweets.tweetsInhtml;
        var i = -1;
        var animationCallback = null;
        animationCallback = function (tweetList) {

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
                        if (!stopAnimation) animationCallback(tweetList);
                    });
                });
            }
            else {
                i = -1;
                if (stopAnimation) return false;
                animationCallback(tweetList);
            }
        };

        animationCallback(tweetList2);
    }




    function animateXTweets(tweetList) {

        for (var i = 1; i < tweetList.length; i + 2) {

            //            setTimeout(function() {
            //                //var newList = [tweetList[i - 1], tweetList[i]];
            //                animateTweets(tweetList);
            //            }, (i*5000));

            //            window.setInterval(function () {
            //                var newList = [tweetList[i - 1], tweetList[i]];
            //                animateTweets(newList);
            //            }, i * 5000);


            //            $.doTimeout(i * 5000, function () {
            //                var newList=[tweetList[i-1], tweetList[i]];
            //                animateTweets(newList);
            //            });
        }

    }


    //    //AUTO refreshing Photos on Event Page
    //    function getPhotos() {
    //        //Turns the Timer off while the page is updating
    //        toggleUpdates('unewphotos', 'off');

    //        //Make the call get the JSON of new Tweets.
    //        $.ajax({
    //            type: "POST",
    //            url: "/Events/GetLastPhotosJSON/",
    //            data: "{Count:9,pageLoadTime:'" + photoPageLoadTime + "',EventID:" + EventID + "}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (newPhotos) {
    //                //Got new Photos JSON object

    //                if (newPhotos.lastphototime != '') {
    //                    photoPageLoadTime = newPhotos.lastphototime;

    //                    if (photoIsFirstFrontPage && (typeof newPhotos.html != "undefined")) {
    //                        $("#photosvideos div.withcomment:first").before(newPhotos.html);
    //                        var PhotosnewUpdatesLi = $("#photosvideos div.newPhotoupdates");
    //                        //PhotosnewUpdatesLi.slideDown();
    //                        var counter = 0;

    //                        //After the new HTML is added to the Page, go through the new items and animate them
    //                        PhotosnewUpdatesLi.each(function (index) {
    //                            var pthisId = $(this).attr("id");
    //                            vpostId = pthisId.substring(pthisId.indexOf('-') + 1);
    //                            photoPostsOnPageQS += "&vp[]=" + vpostId;
    //                            if (!(pthisId in photoPostsOnPage)) photoPostsOnPage.unshift(pthisId);
    //                            if (isElementVisible(this)) { $(this).fadeIn(2500, function () { $(this).removeClass('newPhotoupdates'); }); }
    //                            counter++;
    //                        });


    //                        //This removes items that move off the end of the visible area
    //                        $("#photosvideos div.withcomment").each(function (index) {
    //                            if (index >= photoMaxItemsOnPage) {
    //                                $(this).hide();
    //                                $(this).remove();
    //                            }
    //                        });

    //                        //Add the Pretty photo stuff to the photos
    //                        $("a[rel^='EPLPhoto']").prettyPhoto({
    //                            theme: 'light_square',
    //                            animation_speed: 'fast',
    //                            overlay_gallery: false,
    //                            changepicturecallback: function () { changeDescription(); }
    //                        });

    //                        //There are new photos, update the count
    //                        $("#photoCount").text(newPhotos.photocount);

    //                    }
    //                }

    //                //Turns the Timer back on after the new Posts have been loaded.
    //                toggleUpdates('unewphotos', 'on');

    //            },
    //            error: function (xhr, ajaxOptions, thrownError) {
    //                //                alert(xhr.status);
    //                //                alert(thrownError);
    //            }
    //        });
    //    }

    function isElementVisible(elem) {
        elem = $(elem); if (!elem.length) { return false; }
        var docViewTop = $(window).scrollTop(); var docViewBottom = docViewTop + $(window).height(); var elemTop = elem.offset().top; var elemBottom = elemTop + elem.height(); var isVisible = ((elemBottom >= docViewTop) && (elemTop <= docViewBottom) && (elemBottom <= docViewBottom) && (elemTop >= docViewTop)); return isVisible;
    }

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
            case "unewphotos":
                if (newStatus == 'on') {
                    getPhotosUpdate = setInterval(getPhotos, photoUpdateRate);
                }
                else {
                    clearInterval(getPhotosUpdate); getPhotosUpdate = '0';
                }
                break;
        }
    }



    function titleCount() {
        if (isFirstFrontPage) { var n = $('li.newupdates').length; } else { var n = newUnseenUpdates; }
        if (n <= 0) { if (document.title.match(/\([\d+]\)/)) { document.title = document.title.replace(/(.*)\([\d]+\)(.*)/, "$1$2"); } } else { if (document.title.match(/\((\d+)\)/)) { document.title = document.title.replace(/\((\d+)\)/, "(" + n + ")"); } else { document.title = '(1) ' + document.title; } }
    }

    function autgrow(textarea, min) {
        var linebreaks = textarea.value.match(/\n/g); if (linebreaks != null && linebreaks.length + 1 >= min) { textarea.rows = (linebreaks.length + 1); }
        else { textarea.rows = min; }
    }

    //Tweets


    $.ajaxSetup({
        timeout: updateTimeout,
        cache: false,
        error: function ()
        { toggleUpdates('newtweets', 'on'); }
    });

    if (prologuePostsUpdates) { toggleUpdates('newtweets', 'on'); }

    if (disableAutoupdate) {
        toggleUpdates('newtweets', 'off');
    }
    else {
        toggleUpdates('newtweets', 'on');
    }

    $("#tweetlist li.tweet").each(function () {
        var thisId = $(this).attr("id");
        vpostId = thisId.substring(thisId.indexOf('-') + 1);
        postsOnPage.push(thisId);
        postsOnPageQS += "&vp[]=" + vpostId;
    });

    function removeYellow() {
        if (isFirstFrontPage) {
            $('#tweetlist li.newupdates').each(function () {
                if (isElementVisible(this)) {
                    $(this).animate({ backgroundColor: '#FFF' }, { duration: 2500 });
                    $(this).removeClass('newupdates');
                }
            });
        }
    }

    $(window).scroll(function () { removeYellow(); });

});




