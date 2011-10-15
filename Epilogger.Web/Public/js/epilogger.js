

//Background loader
//Not sure if this is still to be done
/*
$(document).ready(function () {
    var rndNum = Math.ceil(Math.random() * 7);
    $("body").css({ background: "url(/Public/images/bg/sample" + rndNum + ".jpg) #222 no-repeat center center fixed" });
});
*/

//Code to set the cookie to the current user's timezone.
head.ready(function () {
    var storedTimeZoneOffset = $.cookie("TimeZoneOffset");
    currentTimeZoneOffset = -(new Date()).getTimezoneOffset().toString();
    if (storedTimeZoneOffset == null || storedTimeZoneOffset != currentTimeZoneOffset) {
        $.cookie('TimeZoneOffset', currentTimeZoneOffset);
        document.location.reload(true);
    }
});

//attaches  a calendar to date of birth fields.
$(".dob").datepicker({
    changeMonth: true,
    changeYear: true,
    yearRange: "-90:+0"
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

function SetSelectedVenue(venueId, venueName, venueAddress, venueCity, venueState, venueZip) {
    $("#FoursquareVenueID").val(venueId);
    //$("#selectedVenue").html("You selected: <strong>" + venueName + "</strong> as your venue!");
    $("#selectedVenue").html("<strong>" + venueName + "</strong><br />" + venueAddress + "<br />" + venueCity + ", " + venueState + "<br />" + venueZip);
    $("#foursquare-search").text("Change venue")
    //Dismis the popup
    $.colorbox.close();
}

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
            } else {
                FlashMessage("There was a problem adding your blog post", "Message_Error");
            }
        });
}

function FlashMessage(message, cssClas) {
    $('#flash').html(message);
    $('#flash').removeClass();
    $('#flash').addClass(cssClas);
    $('#flash').slideDown('med');
    $('#flash').click(function () { $('#flash').toggle('highlight') });

    window.setTimeout(function () {
        $("#flash").fadeOut("slow");
    }, 3000);
}