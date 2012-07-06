

var prologuePostsUpdates = true;

jQuery(function ($) {
    var column = 1;


    //For Testing
    $("#updateLink").bind('click', function (e) {
        e.preventDefault();
        getTweets();
    });

    //AUTO refreshing tweets for Live Mode
    function getTweets() {
        //Turns the Timer off while the page is updating
        toggleUpdates('newtweets', 'off');

        //Make the call get the JSON of new Tweets.
        $.ajax({
            type: "POST",
            url: "/events/LiveGetLastTweetsJson/",
            data: "{count:2,pageLoadTime:'" + pageLoadTime + "',eventID:" + EventID + "}",
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









                        //                        var column = 1;
                        //                        for (var item in newTweets.tweetsInhtml) {
                        //                            var colName = "#tweetColumn" + column;
                        //                            $(colName).find(".tweet:first").before(newTweets.tweetsInhtml[item]);
                        //                            column++;
                        //                            if (column == 4) column = 1;
                        //                            break;
                        //                        }

                        //                        var i = -1;
                        //                        var tweetList = $(".newupdates");


                        var i = -1;
                        
                        var tweetList = newTweets.tweetsInhtml;

                        var animationCallback = function () {
                            if (++i < tweetList.length) {

                                var colName = "#tweetColumn" + column;
                                $(colName).find(".tweet:first").before(tweetList[i]);
                                var newElement = $(".newupdates");
                                column++;
                                if (column == 4) column = 1;

                                var itemHeight = $(newElement).height();
                                var itemHeight2 = $(newElement).height() - 80; //-80; // -40;


                                $(newElement).css("margin-top", "-" + itemHeight2 + "px");

                                $(newElement).animate({ 'margin-top': '0px' },  'slow');
                                //$(newElement).fadeIn('slow');
                                $(newElement).removeClass('newupdates');
                                $(newElement).parent().find(".tweet:last").fadeOut('slow', function () {
                                    $(newElement).parent().find(".tweet:last").remove();
                                    animationCallback();
                                });

                                //, 




                                //                                $(newElement).css("margin-top", "-" + itemHeight2 + "px");
                                //                                $(newElement).fadeIn('slow');
                                //                                $(newElement).animate({ 'margin-top': '0px' }, 'slow', function () {
                                //                                });
                                //                                $(".newupdates").removeClass('newupdates');
                                //                                $(newElement).parent().find(".tweet:last").fadeOut('slow', animationCallback);


                                //                                //                                $(tweetList[i]).css("margin-top", "-" + itemHeight2 + "px");
                                //                                //                                $(tweetList[i]).fadeIn('slow');
                                //                                //                                $(tweetList[i]).animate({ 'margin-top': '0px', opacity: 1 }, 'slow', function () {
                                //                                //                                    $(tweetList[i]).parent().find(".tweet:last").fadeOut('slow', animationCallback);
                                //                                //                                });



                                //                                //$(tweetList[i]).slideDown('slow', animationCallback);
                                //                                //$(tweetList[i]).parent().find(".tweet").css("top", "-" + itemHeight2 + "px");

                                //                                //                                $(tweetList[i]).parent().find(".tweet").animate({ 'top': '+=' + itemHeight2 + 'px', opacity: 1 }, 1500, function () {

                                //                                //                                });
                                //                                //$(tweetList[i]).parent().find(".tweet:last").fadeOut('slow', animationCallback);

                                //                                //                                $(tweetList[i]).parent().find(".tweet:last").animate({ 'top': '+=500px', opacity: 0 }, function () {
                                //                                //                                                                    $(this).remove();
                                //                                //                                                                });

                                //                                //                                                                $(tweetList[i]).css("margin-top", "-200px");
                                //                                //                                                                $(tweetList[i]).fadeIn(1000).animate({ marginTop: 0 }, 1000);
                                //                                //                                                                $(tweetList[i]).parent().find(".tweet:last").fadeOut(1000, animationCallback);



                                //                                //                                $(tweetList[i]).css("top", "-300px");
                                //                                //                                $(tweetList[i]).fadeIn(1000);
                                //                                //                                $(tweetList[i]).animate({ top: 0 }, 1000);
                                //                                //                                $(tweetList[i]).parent().find(".tweet:last").fadeOut();

                                //                                //$(tweetList[i]).slideDown('slow', animationCallback);

                                //                                //$(tweetList[i]).css("top", "-200px");
                                //                                //                                $(tweetList[i]).fadeIn();




                                //                                //                                $(tweetList[i]).parent().find(".tweet:last").animate({ 'top': '+=500px', opacity: 0 }, function () {
                                //                                //                                    $(this).remove();
                                //                                //                                });

                            }
                            else {
                                $(".newupdates").removeClass('newupdates');
                            }
                        };
                        animationCallback();







                        //This works but it's not right
                        //                        var column = 1;
                        //                        for (var item in newTweets.tweetsInhtml) {
                        //                            var colName = "#tweetColumn" + column;
                        //                            $(colName).find(".tweet:first").before(newTweets.tweetsInhtml[item]);
                        //                            column++;
                        //                            if (column == 4) column = 1;
                        //                        }

                        //                        var i = -1;
                        //                        var tweetList = $(".newupdates");
                        //                        var animationCallback = function () {
                        //                            if (++i < tweetList.length) {
                        //                                $(tweetList[i]).slideDown('slow', animationCallback);
                        //                            }
                        //                            else {
                        //                                $(".newupdates").removeClass('newupdates');
                        //                            }
                        //                        };
                        //                        animationCallback();








                        //                        for (var item in newTweets.tweetsInhtml) {
                        //                            var colName = "#tweetColumn" + column;



                        //                            $(colName).find(".single-tweet:first").before(newTweets.tweetsInhtml[item]);
                        //                            $(".newupdates").slideDown(1000);
                        //                            $(".newupdates").removeClass('newupdates');

                        //                            setTimeout(function () {

                        //                            }, initialTimeout);
                        //                            initialTimeout += 1000;

                        //                            column++;
                        //                            if (column == 4) column = 1;
                        //                        }


                        //                            $(".newupdates").animate({
                        //                                 backgroundColor: '#fff'
                        //                            }, 2500, function () {
                        //                                 $(this).removeClass('newupdates');
                        //                            });




                        //                        var i = 0;
                        //                        $(".tweet-column").each(function () {
                        //                            $(this).find(".single-tweet:first").before(newTweets.tweetsInhtml[i]);
                        //                            i++;
                        //                        });

                        //$("#tweetlist li.tweet:first").before(newTweets.html);

                        //                        var newUpdatesLi = $("#tweetlist li.newupdates");
                        //                        newUpdatesLi.slideDown();
                        //                        var counter = 0;
                        //                        //$('#posttext_error, #commenttext_error').hide();
                        //                        newUpdatesLi.each(function (index) {
                        //                            var thisId = $(this).attr("id");
                        //                            vpostId = thisId.substring(thisId.indexOf('-') + 1);
                        //                            postsOnPageQS += "&vp[]=" + vpostId;
                        //                            if (!(thisId in postsOnPage)) postsOnPage.unshift(thisId);
                        //                            if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#fff' }, 2500, function () { $(this).removeClass('newupdates'); titleCount(); }); }
                        //                            counter++;
                        //                        });
                        //                        $("#tweetlist li.tweet").each(function (index) {
                        //                            if (index >= maxItemsOnPage) {
                        //                                $(this).slideUp();
                        //                                $(this).remove();
                        //                            }
                        //                        });

                        //setupTwitterAction();

                        //There are new tweets, update the count
                        $("#tweetCount").text(newTweets.tweetcount);

                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('newtweets', 'on');

            }
        });
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




