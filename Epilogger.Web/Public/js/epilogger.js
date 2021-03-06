﻿

//Background loader
//Not sure if this is still to be done
/*
$(document).ready(function () {
    var rndNum = Math.ceil(Math.random() * 7);
    $("body").css({ background: "url(/Public/images/bg/sample" + rndNum + ".jpg) #222 no-repeat center center fixed" });
});
*/


//This is a hack for IE. If the URL has a hash for PhotoDetails, then redirect to the correct URL
//#/events/PhotoDetails
url = location.href;
hashtag = (url.indexOf('#') != -1) ? decodeURI(url.substring(url.indexOf('#') + 1, url.length)) : false;
if (hashtag != false) {
    if (hashtag != "_=_") {
        window.location = 'http://epilogger.com/' + hashtag;    
    }
}



//Code to set the cookie to the current user's timezone.
head.ready(function () {
    var storedTimeZoneOffset = $.cookie("TimeZoneOffset");
    currentTimeZoneOffset = -(new Date()).getTimezoneOffset().toString();
    if (storedTimeZoneOffset == null || storedTimeZoneOffset != currentTimeZoneOffset) {
        $.cookie('TimeZoneOffset', currentTimeZoneOffset);
        document.location.reload(true);
    }

    $('input[title!=""]').hint();

});


//This fills in the Comments on the Image popups (tweets)
function changeDescription() {
    $(".pp_Comments").html("Loading...");
    
    //Loaded the comments
    var photoID = $(".pp_description").html();
    $(".pp_description").html("");

//    $.get('/Events/GetImageComments/' + photoID,
//        function (data) {
//            $('.pp_Comments').html(data);
//        }, "html");
    

    $.ajax({
        url: '/Events/ImageCommentControl/' + photoID,
        type: 'GET',
        contentType: 'html',
        success: function (data) {
            $('.pp_Comments').html(data);
        },
        error: function () {
            $('.pp_Comments').html("error");
        }
    });

   

    //Load the TweetBox
    $.get('/Events/TweetBox/' + photoID,
        function (data) {
            $('.pp_TweetBox').html(data);
        }, "html");


    $('.pp_shareBar').hide();
    //Load the social media bar
    $.get('/Home/SocialBar/' + photoID,
    function (data) {
        $('.pp_shareBar').html(data);
    }, "html");


    $(document).ajaxComplete(function () {
        //Make the FB button work
        try {
            FB.XFBML.parse();
        } catch (ex) { }
        //Make the Twitter button work
        try {
            (function () {
                var twitterScriptTag = document.createElement('script');
                twitterScriptTag.type = 'text/javascript';
                twitterScriptTag.async = true;
                twitterScriptTag.src = 'http://platform.twitter.com/widgets.js';
                var s = document.getElementsByTagName('script')[0];
                s.parentNode.insertBefore(twitterScriptTag, s);
            })();
        } catch (ex) { }
    });
    $('.pp_shareBar').show();

}

function SearchVenues(url) {
    $('#searchresults').html("Searching foursquare for your venue...");
    var venue_name = $("#VenueName").val();
    var venue_city = $("#City").val();
    var venue_state = $("#ProvinceState").val();
    var venue_zip = $("#ZipPostal").val();
    var venue_address = $("#Address").val();

    $.post(url, 
        {
            name: venue_name,
            city: venue_city,
            state: venue_state,
            zip: venue_zip,
            address: venue_address
        },
        function (data) {
            $('#searchresults').html(data);
        });
        return false;
}

//function SetSelectedVenue(venueId, venueName, venueAddress, venueCity, venueState, venueZip) {
//    $("#FoursquareVenueID").val(venueId);
//    //$("#selectedVenue").html("You selected: <strong>" + venueName + "</strong> as your venue!");
//    $("#selectedVenue").html("<strong>" + venueName + "</strong><br />" + venueAddress + "<br />" + venueCity + ", " + venueState + "<br />" + venueZip + "<br />");
//    $("#foursquare-search").text("Change venue");
//    //Dismis the popup
//    //$.colorbox.close();
//    parent.$.fn.colorbox.close();
//}





function AddBlogPost(url) {
    var blogPost = {
            BlogURL: $("#BlogURL").val(),
            Title: $("#Title").val(),
            Description: $("#Description").val()
    };

    $.post(url, blogPost,
        function (data) {
            if (data == "True") {
                FlashMessage("Your blog post was added", "Message_Success");
                //Dismis the popup
                $.colorbox.close();
                window.location.reload();
            } else {
                FlashMessage("There was a problem adding your blog post", "Message_Error");
            }
        });
    return false;
}


/***** TWITTER ACTIONS ******/
head.ready(function () {
    setupTwitterAction();
});

function setupTwitterAction() {
    $('.Reply').click(function (e) {
        e.preventDefault();

        $.colorbox({
            width: 550,
            height: 320,
            href: "TweetReply?eventId=" + EventID + "&tweetId=" + this.id
        });

    });

    $('.Retweet').click(function (e) {
        e.preventDefault();

        $.colorbox({
            width: 550,
            height: 230,
            href: "TweetRetweet?eventId=" + EventID + "&tweetId=" + this.id
        });

    });

    $('.Favorite').click(function (e) {
        e.preventDefault();

        var twitterFavorite = {
            TwitterID: this.id
        };

        $.post('/Events/TweetFavorite/', twitterFavorite,
        function (data) {
            if (data == "True") {
                FlashMessage("Your tweet has been favorited!", "Message_Success Message_Flash");
            }
            else if (data == "Auth") {
                $.colorbox({
                    width: 550,
                    height: 220,
                    href: "NeedTwitterAuth"
                });
            }
            else 
            {
                FlashMessage("There was a problem favoriting your tweet, please try again.", "Message_Error Message_Flash");
            }
        });
        return false;

    });
}


function ReplyToTweet(url) {
    $("#submitTweet").attr('disabled', 'disabled');
    $("#submitTweet").addClass('disabled');
    $("#twitterLoading").show();

    var twitterReply = {
        ReplyNewTweet: $("#ReplyNewTweet").val(),
        TwitterID: $("#TwitterID").val()
    };

    $.post(url, twitterReply,
        function (data) {
            if (data == "True") {
                FlashMessage("Your tweet has been sent!", "Message_Success Message_Flash");
                $.colorbox.close();
            } else {
                FlashMessage("There was a problem sending your tweet, please try again.", "Message_Error Message_Flash");
                $.colorbox.close();
            }
        });
    return false;
}

function ActionRetweet(url) {
    $("#submitTweet").attr('disabled', 'disabled');
    $("#submitTweet").addClass('disabled');
    $("#twitterLoading").show();

    var twitterRetweet = {
        RetweetText: $("#RetweetText").val(),
        TwitterID: $("#TwitterID").val(),
        ClassicRT: $("#ClassicRT").val()
    };

    $.post(url, twitterRetweet,
        function (data) {
            if (data == "True") {
                FlashMessage("Your tweet has been sent!", "Message_Success Message_Flash");
                $.colorbox.close();
            } else {
                FlashMessage("There was a problem sending your tweet, please try again.", "Message_Error Message_Flash");
                $.colorbox.close();
            }
        });
    return false;
}


function Tweetbox(url) {
    $("#submitTweet").attr('disabled', 'disabled');
    $("#submitTweet").addClass('disabled');
    $("#twitterLoading").show();

    var twitterBox = {
        NewTweet: $("#tweetBoxPhoto").val(),
        TwitterID: $("#TwitterID").val(),
        VisibleConfirmation: $("#VisibleConfirmation").val()
    };

    $.post(url, twitterBox,
        function (data) {
            if (data == "True") {
                //FlashMessage("Your tweet has been sent!", "Message_Success Message_Flash");
                $("#statusMessage").html("Your tweet has been sent!");
                //$.colorbox.close();
            } else {
                //FlashMessage("There was a problem sending your tweet, please try again.", "Message_Error Message_Flash");
                $("#statusMessage").html("There was a problem sending your tweet, please try again.");
                //$.colorbox.close();
            }
            $("#twitterLoading").hide();
            $("#submitTweet").removeAttr("disabled");
            $("#submitTweet").removeClass('disabled');
        });
    return false;
}




function cancelPopUp() {
    $.colorbox.close();
}
function resizePopUp(width, height) {
    $.colorbox.resize({ width: width, height: height });
}


function FlashMessage(message, cssClas) {
    $('#flash').html(message);
    $('#flash').removeClass();
    $('#flash').addClass(cssClas);
    $('#flash').slideDown('med');
    $('#flash').click(function () { $('#flash').toggle('highlight'); });

    window.setTimeout(function () {
        $("#flash").fadeOut("slow");
    }, 3000);
}



//PopUp for Advanced Operators
head.ready(function () {

    var myHelp = $("#SearchHelpPopUpDiv");
    var myHelp_link = $("#AdvancedSearchHelp");

    myHelp_link.toggle(
        function () {
            myHelp.show();
            return false;
        },
        function () {
            myHelp.hide();
            return false;
        });
    });

jQuery.fn.hint = function (blurClass) {
    if (!blurClass) {
        blurClass = 'blur';
    }

    return this.each(function () {
        // get jQuery version of 'this'
        var $input = jQuery(this),

        // capture the rest of the variable to allow for reuse
            title = $input.attr('title'),
            $form = jQuery(this.form),
            $win = jQuery(window);

        function remove() {
            if ($input.val() === title && $input.hasClass(blurClass)) {
                $input.val('').removeClass(blurClass);
            }
        }

        // only apply logic if the element has the attribute
        if (title) {
            // on blur, set value to title attr if text is blank
            $input.blur(function () {
                if (this.value === '') {
                    $input.val(title).addClass(blurClass);
                }
            }).focus(remove).blur(); // now change all inputs to title

            // clear the pre-defined text when form is submitted
            $form.submit(remove);
            $win.unload(remove); // handles Firefox's autocomplete
        }
    });
};

//Delete Tweets
head.ready(function () {
    $(".deleteTweet").bind("click", function (e) {
        e.preventDefault();

        var theLink = this;

        $.ajax({
            url: '/Events/DeleteTweetAjax/' + EventSlug + '/' + this.id,
            type: 'POST',
            success: function (data) {
                //Remove the image from the page
                $(theLink).parent().parent().fadeOut();
                //Update the total count

            },
            error: function () {
            }
        });

    });
});


//Delete Images
head.ready(function () {
    $(".imagedelete").bind("click", function (e) {
        e.preventDefault();
        var theLink = this;
        $.ajax({
            url: '/Events/DeleteImageAjax/' + EventSlug + '/' + this.id,
            type: 'POST',
            data: { href: e.srcElement.href, PathName: e.srcElement.pathname, UserAgent: navigator.userAgent, Host: e.srcElement.host },
            success: function (result) {
                //Remove the image from the page.
                $(theLink).parent().fadeOut();
                //Update the total count

            }
        });


    });
});


head.ready(function () {
    new function ($) {
        $.fn.setCursorPosition = function (pos) {
            if ($(this).get(0).setSelectionRange) {
                $(this).get(0).setSelectionRange(pos, pos);
            } else if ($(this).get(0).createTextRange) {
                var range = $(this).get(0).createTextRange();
                range.collapse(true);
                range.moveEnd('character', pos);
                range.moveStart('character', pos);
                range.select();
            }
        }
    } (jQuery);
});

//Validate Birthdate - Used on Create Account Pages - Aug 25, 2012
head.ready(function () {
    $("#m_DOB").change(function () {
        var month = $("#m_DOB").val();
        var year = $("#y_DOB").val();
        var totalDays;

        if (month == 2) {
            if (year % 4 == 0) {
                totalDays = 29;
            }
            else {
                totalDays = 28;
            }
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11) {
            totalDays = 30;
        }
        else {
            totalDays = 31;
        }

        //if ($("#d_DOB").val() > totalDays) {
        var dropDown = $("#d_DOB");
        dropDown.children().remove();

        dropDown.append("<option value='Day'>Day</option>");
        for (var key = 1; key <= totalDays; key++) {
            dropDown.append("<option value='" + key + "'>" + key + "</option>");
        }
        //}

        consolidateBirthday();
    });

    $("#y_DOB").change(function () {
        var year = $("#y_DOB").val();
        var month = $("#m_DOB").val();
        var totalDays;
        if (month == 2) {
            if (year % 4 == 0) {
                totalDays = 29;
            }
            else {
                totalDays = 28;
            }
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11) {
            totalDays = 30;
        }
        else {
            totalDays = 31;
        }

        if ($("#d_DOB option:selected").text() > totalDays) {
            var dropDown = $("#d_DOB");
            dropDown.children().remove();
            dropDown.append("<option value='Day'>Day</option>");
            for (var key = 1; key <= totalDays; key++) {
                dropDown.append("<option value='" + key + "'>" + key + "</option>");
            }
        }

        consolidateBirthday();
    });

    $("#d_DOB").change(function () {
        consolidateBirthday();
    });


    function consolidateBirthday() {

        if ($("#d_DOB option:selected").text() != "Day" &&
                    $("#m_DOB option:selected").text() != "Month" &&
                    $("#y_DOB option:selected").text() != "Year") {

            $("#DateOfBirth").val($("#m_DOB option:selected").val() + "/" + $("#d_DOB option:selected").val() + "/" + $("#y_DOB option:selected").val() + " 00:00:00");
            //alert($("#DateOfBirth").val());
            //08/25/2012 04:09:42
        }
    }
});

//Adds empty space to the flash div, so the flash_% message does not cover up the header
head.ready(function () {
    if ($('#flash').length != 0) {
        var messageSize = 30;
        //var heightAmount = messageSize + $('#flash').height();
        if ($('#[id^=flash_]').length > 0) {
            $('#flash').height($('#flash').height() + messageSize); ;
        }
        else {
            $('#flash').height(0);
        }
        // remove messageSize amount per click
        $('#[id^=flash_]').click(function () {
            $('#flash').height($('#flash').height() - messageSize);
        })
    }
});
