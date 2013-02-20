// JS for the new Edit Event - CB Feb 20, 2013

head.ready(function () {

    //Show the first section
    $("#left-nav li:first-child a").addClass("current");
    $("#editBasicInfoSection").show();

    

});



$("#left-nav li a").bind("click", function (e) {
    e.preventDefault();

    //Hide all sections
    $(".editSection").hide();
    $("#left-nav li a").removeClass("current");

    switch (this.id) {
        case "menuBasic":
            $("#editBasicInfoSection").show();
            $(this).addClass("current");
            break;
        case "menuHashtag":
            $("#hashTagsSection").show();
            $(this).addClass("current");    
            break;
        case "menuVenue":
            $("#venueSection").show();
            $(this).addClass("current");    
            break;
        case "menuDesc":
            $("#descriptionSection").show();
            $(this).addClass("current");    
            break;
        case "menuTick":
            $("#ticketingSection").show();
            $(this).addClass("current");
            break;
    }

});


function SetSelectedVenue(venueId, venueName, venueAddress, venueCity, venueState, venueZip) {
    $("#FoursquareVenueID").val(venueId);
    $("#selectedVenue").html("<strong>" + venueName + "</strong><br />" + venueAddress + "<br />" + venueCity + ", " + venueState + "<br />" + venueZip + "<br />");
    $("#foursquare-search").val("Change venue");

    //Dismis the popup
    parent.$.fn.colorbox.close();

    $("#moreinformation-info .blocking").unblock();
    $("#moreinformation-info h4").unblock();
    $("#step4").removeClass("stepDisable");
    $("#step3 span").html("&nbsp;");
    $("#step3").addClass("stepComplete");
    $(".submitForm").removeClass("disabled");
    $(".submitForm").removeAttr("disabled");

}