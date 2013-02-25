// JS for the new Edit Event - CB Feb 20, 2013

head.ready(function () {

    //Show the first section
    $("#left-nav li:first-child a").addClass("current");
    //$("#editBasicInfoSection").show();
    $("#venueSection").show();

    //Setup date time stuff
    $(function () {
        $("#StartDateTime").datepicker({ dateFormat: 'M dd, yy' });
        $("#anim").change(function () {
            $("#StartDateTime").datepicker("option", "showAnim", $(this).val());
        });
    });

    $(function () {
        $("#EndDateTime").datepicker({ dateFormat: 'M dd, yy' });
        $("#anim").change(function () {
            $("#EndDateTime").datepicker("option", "showAnim", $(this).val());
        });
    });

    $("#StartDateTime").change(function () {
        var test = $(this).datepicker('getDate');
        var testm = new Date(test.getTime());
        testm.setDate(testm.getDate());

        $("#EndDateTime").datepicker("option", "minDate", testm);
    });


    //Hastags
    $('#searchButton').bind("click", function () {
        $("#preview_tweet_box").removeClass("invisible");
        $('#preview_tweet_box').slideDown(200);
        //$('.clear_away').remove();
        //$("#warningBox").attr('class', 'warning_small');
        //$(this).removeClass("disabled_blue_button");
        //$(this).addClass("preview_stretch_btn");
        //$(this).prop('value', 'refresh results in preview box');
    });
    var i = 0;
    $('#addAnotherTermBTN').bind("click", function () {
        i++;
        $("#addAnother" + i).removeClass("invisible");
        if (i == 5)
            $(this).remove();
    });



    //Search Blur
    $('.searchText').bind("blur", function () {

        //Add quotes
        if ($('#IsAdvanceMode').val() == 'False') {
            if ($(this).val().indexOf(" ") >= 0) {
                if ($(this).val().substring(0, 1) != '"' && $(this).val().substring($(this).val().legnth - 1, 1) != '"') {
                    $(this).val('"' + $.trim($(this).val()) + '"');
                }
            }
        }

    });


    //Search Keyup
    $('.searchText').bind("keyup", function(e) {
        //If the search terms are too complex, disable simple mode.
        if ($('#searchText').val().indexOf("AND") > 0 ||
        $('#searchText').val().indexOf("(") > 0 ||
        $('#searchText').val().indexOf(")") > 0 ||
        $('#searchText').val().indexOf("-") > 0) {
            if ($('#IsAdvanceMode').val() == 'False') {
                $('#advance_link').hide();
                advancedMode();
                $('#advancedOnlyMsg').show();
            }
        }
        else {
            $('#advancedOnlyMsg').hide();
            $('#advance_link').show();
        }
    });

    

    function showTOperators() {
        $("#SearchHelpPopUpDiv").toggleClass("uiPopUpHidden");
        $('#twitter_operators').text($('#twitter_operators').text() == 'view twitter operators' ? 'hide twitter operators' : 'view twitter operators');
    }

    //If the search terms are too complex, disable simple mode.
    if ($('#searchText').val().indexOf("AND") > 0 ||
        $('#searchText').val().indexOf("(") > 0 ||
        $('#searchText').val().indexOf(")") > 0 ||
        $('#searchText').val().indexOf("-") > 0) {

        $('#advance_link').hide();
        advancedMode();
        $('#advancedOnlyMsg').show();
    }



});






function advancedMode() {
    $('#normal_mode').toggle();

    if ($('#IsAdvanceMode').val() == 'False') {
        $('#IsAdvanceMode').val('True');
        $('#modeTitle').text('Advanced Mode: ');
        
        var query = "";
        $.each($('.searchText'), function () {
            if ($(this).val() != "") {
                query = query + $(this).val().replace('#', '') + ' OR ';
            }
        });
        query = query.substring(0, query.length - 4);

        $('#searchText').val(query);
    }
    else {
        $('#IsAdvanceMode').val('False');
        $('#modeTitle').text('Official Hashtag:');

        //Populate the other boxes if they changed something in advanced mode
        var parts = $('#searchText').val().split(" OR "), i, l;

        $("#searchText").val(parts[0]);
        for (i = 0, l = parts.length; i < l; i += 1) {
            $("#searchText" + (i + 2)).val(parts[i + 1]);
            if ($("#searchText" + (i + 2)).val().length > 0) {
                $("#addAnother" + (i + 1)).removeClass("invisible");
            }
            else {
                $("#addAnother" + (i + 1)).addClass("invisible");
            }
        }
    }

    $('#t_op_section').toggleClass('twitterOperatorSectionOn twitterOperatorSectionOff');
    $('#advance_link').text($('#advance_link').text() == '(Switch to advanced mode)' ? '(Switch to simple mode)' : '(Switch to advanced mode)');

    $("#SearchHelpPopUpDiv").addClass("uiPopUpHidden");
}



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

    //Move the map
    setLocation(venueName + ', ' + venueAddress + ', ' + venueCity + ', ' + venueState + ', ' + venueZip);

    //Dismis the popup
    parent.$.fn.colorbox.close();

}



/* Google Maps */
var map;
function initialize() {

    var mapOptions = {
        zoom: 3,
        center: new google.maps.LatLng(37.7750, -95.4183),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);

    //If there is already a venue, go to it.
    if (venueAddress.length > 0) {
            setLocation(venueAddress);
    }

}

function loadScript() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyACSiRC-8eEdPrS6qs9LLRb8m-EZ-GNhEo&sensor=false&callback=initialize";
    document.body.appendChild(script);
}

window.onload = loadScript;


var geocoder;
var marker;
var infowindow;
function setLocation(location) {
    
    if(!geocoder) {
        geocoder = new google.maps.Geocoder();	
    }

    var geocoderRequest = {
        address: location
    };

    geocoder.geocode(geocoderRequest, function (results, status) {

        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            if (!marker) {
                marker = new google.maps.Marker({
                    map: map
                });
            }

            marker.setPosition(results[0].geometry.location);

            map.setZoom(16);


            //            if (!infowindow) {
            //                infowindow = new google.maps.InfoWindow();
            //            }
            //            var content = '<strong>' + results[0].formatted_address + '</strong>';
            //            infowindow.setContent(content);
            //            infowindow.open(map, marker);
        }

    });
}
