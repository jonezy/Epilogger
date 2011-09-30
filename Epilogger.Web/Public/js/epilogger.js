

//Background loader
//Not sure if this is still to be done
/*
$(document).ready(function () {
    var rndNum = Math.ceil(Math.random() * 7);
    $("body").css({ background: "url(/Public/images/bg/sample" + rndNum + ".jpg) #222 no-repeat center center fixed" });
});
*/




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
    var venue_name = $("#VenueName").val();
    var venue_city = $("#City").val();
    var venue_state = "";
    var venue_zip = "";
    var venue_address = "";

    $.post(url,
        {
            name: venue_name,
            city: venue_city,
            state: venue_state,
            zip: venue_zip,
            address: venue_address
        },
        function (data) {
            $('#VenueContentDiv').html(data);
        });
}
function SetSelectedVenue(venueId) {
    $("#selectedVenue").text(venueId);
}