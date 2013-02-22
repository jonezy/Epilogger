// JS for the new Edit Event - CB Feb 20, 2013

head.ready(function () {

    //Show the first section
    $("#left-nav li:first-child a").addClass("current");
    //$("#editBasicInfoSection").show();
    $("#hashTagsSection").show();

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