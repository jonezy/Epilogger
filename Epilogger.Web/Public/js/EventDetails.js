



jQuery(function ($) {
    //AUTO refreshing tweets on Event Page
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
                            if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#fff' }, 2500, function () { $(this).removeClass('newupdates'); titleCount(); }); }
                            counter++;
                        });
                        $("#tweetlist li.tweet").each(function (index) {
                            if (index >= maxItemsOnPage)
                            { $(this).slideUp(); }
                        });

                        //There are new photos, update the count
                        $("#tweetCount").text(newTweets.tweetcount);

                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('unewposts', 'on');

            }
        });
    }


    //AUTO refreshing Photos on Event Page
    function getPhotos() {
        //Turns the Timer off while the page is updating
        toggleUpdates('unewphotos', 'off');

        //Make the call get the JSON of new Tweets.
        $.ajax({
            type: "POST",
            url: "/Events/GetLastPhotosJSON/",
            data: "{Count:9,pageLoadTime:'" + photoPageLoadTime + "',EventID:" + EventID + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (newPhotos) {
                //Got new Photos JSON object

                if (newPhotos.lastphototime != '') {
                    photoPageLoadTime = newPhotos.lastphototime;

                    if (photoIsFirstFrontPage && (typeof newPhotos.html != "undefined")) {
                        $("#photosvideos div.withcomment:first").before(newPhotos.html);
                        var PhotosnewUpdatesLi = $("#photosvideos div.newPhotoupdates");
                        //PhotosnewUpdatesLi.slideDown();
                        var counter = 0;

                        //After the new HTML is added to the Page, go through the new items and animate them
                        PhotosnewUpdatesLi.each(function (index) {
                            var pthisId = $(this).attr("id");
                            vpostId = pthisId.substring(pthisId.indexOf('-') + 1);
                            photoPostsOnPageQS += "&vp[]=" + vpostId;
                            if (!(pthisId in photoPostsOnPage)) photoPostsOnPage.unshift(pthisId);
                            if (isElementVisible(this)) { $(this).fadeIn(2500, function () { $(this).removeClass('newPhotoupdates'); }); }
                            counter++;
                        });


                        //This removes items that move off the end of the visible area
                        $("#photosvideos div.withcomment").each(function (index) {
                            if (index >= photoMaxItemsOnPage)
                            { $(this).hide(); }
                        });

                        //Add the Pretty photo stuff to the photos
                        $("a[rel^='prettyPhoto']").prettyPhoto({
                            theme: 'light_square',
                            animation_speed: 'fast',
                            overlay_gallery: false,
                            changepicturecallback: function () { changeDescription(); }
                        });

                        //There are new photos, update the count
                        $("#photoCount").text(newPhotos.photocount);

                    }
                }

                //Turns the Timer back on after the new Posts have been loaded.
                toggleUpdates('unewphotos', 'on');

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
            case "unewphotos": if (newStatus == 'on') { getPhotosUpdate = setInterval(getPhotos, updateRate); }
                else { clearInterval(getPhotosUpdate); getPhotosUpdate = '0'; }
                break;
        }
    }


    function titleCount() {
        if (isFirstFrontPage) { var n = $('li.newupdates').length; } else { var n = newUnseenUpdates; }
        if (n <= 0) { if (document.title.match(/\([\d+]\)/)) { document.title = document.title.replace(/(.*)\([\d]+\)(.*)/, "$1$2"); } } else { if (document.title.match(/\((\d+)\)/)) { document.title = document.title.replace(/\((\d+)\)/, "(" + n + ")"); } else { document.title = '(1) ' + document.title; } }
    }

    //    function photoTitleCount() {
    //        if (photoIsFirstFrontPage) { var n = $('div.newupdates').length; } else { var n = newUnseenUpdates; }
    //        if (n <= 0) { if (document.title.match(/\([\d+]\)/)) { document.title = document.title.replace(/(.*)\([\d]+\)(.*)/, "$1$2"); } } else { if (document.title.match(/\((\d+)\)/)) { document.title = document.title.replace(/\((\d+)\)/, "(" + n + ")"); } else { document.title = '(1) ' + document.title; } }
    //    }

    function autgrow(textarea, min) {
        var linebreaks = textarea.value.match(/\n/g); if (linebreaks != null && linebreaks.length + 1 >= min) { textarea.rows = (linebreaks.length + 1); }
        else { textarea.rows = min; }
    }

    //Tweets
    $.ajaxSetup({ timeout: updateTimeout, cache: false, error: function () { toggleUpdates('unewposts', 'on'); } });
    if (prologuePostsUpdates) { toggleUpdates('unewposts', 'on'); }
    if (disableAutoupdate) {
        toggleUpdates('unewposts', 'off');
    }
    else {
        toggleUpdates('unewposts', 'on');
    }

    $("#tweetlist li.tweet").each(function () { var thisId = $(this).attr("id"); vpostId = thisId.substring(thisId.indexOf('-') + 1); postsOnPage.push(thisId); postsOnPageQS += "&vp[]=" + vpostId; }); function removeYellow() {
        if (isFirstFrontPage) { $('#tweetlist li.newupdates').each(function () { if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#FFF' }, { duration: 2500 }); $(this).removeClass('newupdates'); } }); }
        //titleCount();
    }
    $(window).scroll(function () { removeYellow(); });












    //Photos
    $.ajaxSetup({ timeout: photoUpdateTimeout,
        cache: false,
        error: function () { toggleUpdates('unewphotos', 'on'); }
    });

    if (photoProloguePostsUpdates) { toggleUpdates('unewphotos', 'on'); }
    if (disableAutoupdate) {
        toggleUpdates('unewphotos', 'off');
    }
    else {
        toggleUpdates('unewphotos', 'on');
    }

    $("#photosvideos div.withcomment").each(function () {
        var pthisId = $(this).attr("id");
        pvpostId = pthisId.substring(pthisId.indexOf('-') + 1);
        photoPostsOnPage.push(pthisId);
        photoPostsOnPageQS += "&vp[]=" + pvpostId;
    });

    //    function removeYellowPhoto() {
    //        if (photoIsFirstFrontPage) {
    //            //$('#photosvideos div.newPhotoupdates').each(function () { if (isElementVisible(this)) { $(this).animate({ backgroundColor: '#fff' }, { duration: 2500 }); $(this).removeClass('newPhotoupdates'); } });
    //            $('#photosvideos div.newPhotoupdates').each(function () { if (isElementVisible(this)) { $(this).fadeIn(2500); $(this).removeClass('newPhotoupdates'); } });
    //        }
    //    }
    //    $(window).scroll(function () { removeYellowPhoto(); });
});