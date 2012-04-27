



head.ready(function () {

    //Setup the Date span
    $("#start_time, #end_time").timePicker({
        show24Hours: false,
        separator: ':',
        step: 15,
        leadingZero: false
    });

    // Store time used by duration.
    var oldTime = $.timePicker("#start_time").getTime();

    // Keep the duration between the two inputs.
    $("#start_time").change(function () {
        if ($("#end_time").val()) { // Only update when second input has a value.
            // Calculate duration.
            var duration = ($.timePicker("#end_time").getTime() - oldTime);
            var time = $.timePicker("#start_time").getTime();
            // Calculate and update the time in the second input.
            $.timePicker("#end_time").setTime(new Date(new Date(time.getTime() + duration)));
            oldTime = time;
        }
    });

    //Collection Time
    $("#collection_start_time, #collection_end_time").timePicker({
        show24Hours: false,
        separator: ':',
        step: 15,
        leadingZero: false
    });

    // Store time used by duration.
    var oldTime = $.timePicker("#collection_start_time").getTime();

    // Keep the duration between the two inputs.
    $("#collection_start_time").change(function () {
        if ($("#collection_end_time").val()) { // Only update when second input has a value.
            // Calculate duration.
            var duration = ($.timePicker("#collection_end_time").getTime() - oldTime);
            var time = $.timePicker("#collection_start_time").getTime();
            // Calculate and update the time in the second input.
            $.timePicker("#collection_end_time").setTime(new Date(new Date(time.getTime() + duration)));
            oldTime = time;
        }
    });

    //Setup up the date boxes
    $(function () {
        var dates = $("#start_date, #end_date").datepicker({
            changeMonth: true,
            numberOfMonths: 3,
            onSelect: function (selectedDate) {
                var option = this.id == "start_date" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    });

    $(function () {
        var dates = $("#collection_start_date, #collection_end_date").datepicker({
            changeMonth: true,
            numberOfMonths: 3,
            onSelect: function (selectedDate) {
                var option = this.id == "collection_start_date" ? "minDate" : "maxDate",
					instance = $(this).data("datepicker"),
					date = $.datepicker.parseDate(
						instance.settings.dateFormat ||
						$.datepicker._defaults.dateFormat,
						selectedDate, instance.settings);
                dates.not(this).datepicker("option", option, date);
            }
        });
    });


    //Setup the page
    //$("#createSteps li").addClass("stepDisable");
    $("#Name").focus();
    //$("#step1").removeClass("stepDisable"); 

    //Block the Other sections
    if (page == 'Create') {
        $("#whattoinclude-info .blocking").block({ message: null });
        $("#whattoinclude-info h4").block({ message: null });

        $("#whenandwhere-info .blocking").block({ message: null });
        $("#whenandwhere-info h4").block({ message: null });

        $("#moreinformation-info .blocking").block({ message: null });
        $("#moreinformation-info h4").block({ message: null });

        $(".submitForm").attr('disabled', 'disabled');



        $(":input", "#createevent-info").not(":button, :submit, :reset, :hidden").bind("blur", function (e) {
            checkBlocking();
        });
        $("#CategoryID").bind("change", function (e) {
            checkBlocking();
        });

        $(":input", "#whattoinclude-info").not(":button, :submit, :reset, :hidden").bind("keyup", function (e) {
            checkBlocking();
        });
        var showMore = false;
        $(":input", "#whenandwhere-info").not(":button, :submit, :reset, :hidden").bind("click", function (e) {
            showMore = true;
            checkBlocking();
        });
        $(":input", "#moreinformation-info").not(":button, :submit, :reset, :hidden").bind("keyup", function (e) {
            checkBlocking();
        });

        $("#AdvancedToggle").bind("click", function (e) {
            e.preventDefault();
            $("#AdvancedSearchTerms").toggle();
        });

    }

    function checkBlocking() {

        //Add the Complete style
        if ($("#Name").val().length > 0 && $("#CategoryID").val() != 0) {
            $("#whattoinclude-info .blocking").unblock();
            $("#whattoinclude-info h4").unblock();
            $("#step2").removeClass("stepDisable");
            $("#step1 span").html("&nbsp;");
            $("#step1").addClass("stepComplete");
        }

        if ($("#seachTerm1").val().length > 0) {
            $("#whenandwhere-info .blocking").unblock();
            $("#whenandwhere-info h4").unblock();
            $("#step3").removeClass("stepDisable");
            $("#step2 span").html("&nbsp;");
            $("#step2").addClass("stepComplete");
        }

        if (showMore) {
            $("#moreinformation-info .blocking").unblock();
            $("#moreinformation-info h4").unblock();
            $("#step4").removeClass("stepDisable");
            $("#step3 span").html("&nbsp;");
            $("#step3").addClass("stepComplete");
            $(".submitForm").removeClass("disabled");
            $(".submitForm").removeAttr("disabled");
        }

        if ($("#Description").val().length > 0 && $("#WebsiteURL").val().length > 0 && $("#TwitterAccount").val().length > 0 && $("#FacebookPageURL").val().length > 0 && $("#Cost").val().length > 0) {
            $("#step4 span").html("&nbsp;");
            $("#step4").addClass("stepComplete");
        }

    }

    checkBlocking();




    //Search term rules
    var counter = 1;
    checkChildCount();
    buildSearchString();

    $('.rule').live('click', function (e) {

        e.preventDefault();

        switch ($(e.target).attr("class")) {

            case 'plus blue-action-button':

                counter++;

                var newItem = $("#template").clone();
                newItem.removeClass("hidden");
                newItem.insertAfter(this).attr('id', 'rule' + counter);
                $(':input', '#rule' + counter)
				 .not(':button, :submit, :reset, :hidden')
				 .val('')
				 .removeAttr('checked')
				 .removeAttr('selected')
                 .attr("id", "seachTerm" + counter);

                $("select", "#rule" + counter).attr("id", "operator" + counter);

                $("#seachTerm" + counter).bind("keyup", function () {
                    buildSearchString();
                });

                break;

            case 'minus blue-action-button':

                $(this).remove();
                break;

            case 'minus blue-action-button disabled':
                break;
        }
        checkChildCount();
        buildSearchString();
        return false;
    }

	);

    function checkChildCount() {
        if ($("#rule-group").children().size() >= 2) {
            $("#rule-group .rule .minus").removeClass("disabled");
        } else {
            $("#rule-group .rule .minus").addClass("disabled");
        }
    }



    //    function buildSearchBoxes() {
    //        //Take the value from the DB and parse it into all the correct search boxes
    //        

    //        
    //        counter++;

    //        var newItem = $("#template").clone();
    //        newItem.removeClass("hidden");
    //        newItem.insertAfter(this).attr('id', 'rule' + counter);
    //        $(':input', '#rule' + counter)
    //			.not(':button, :submit, :reset, :hidden')
    //			.val('')
    //			.removeAttr('checked')
    //			.removeAttr('selected')
    //            .attr("id", "seachTerm" + counter);

    //        $("select", "#rule" + counter).attr("id", "operator" + counter);

    //        $("#seachTerm" + counter).bind("keyup", function (e) {
    //            buildSearchString();
    //        });
    //        
    //    }


    //Friendly URL creation

    $("#Name").bind("keyup", function (e) {
        buildFriendlyURL($("#Name").val());
    });
    $("#EventSlug").bind("keyup", function (e) {
        buildFriendlyURL($("#EventSlug").val());
    });


    function buildFriendlyURL(title) {
        var url = title
        .toLowerCase() // change everything to lowercase
		.replace(/^\s+|\s+$/g, "") // trim leading and trailing spaces		
		.replace(/[_|\s]+/g, "-") // change all spaces and underscores to a hyphen
		.replace(/[^a-zA-Z0-9-]+/g, "") // remove all non-alphanumeric characters except the hyphen
		.replace(/[-]+/g, "-") // replace multiple instances of the hyphen with a single instance
		.replace(/^-+|-+$/g, "") // trim leading and trailing hyphens				
		;

        $("#EventSlug").val(url);
    }









    $(":input", "#rule-group").not(":button, :submit, :reset, :hidden").bind("keyup", function (e) {
        buildSearchString();
    });
    function buildSearchString() {
        var searchString = "";
        $(":input", "#rule-group").not(":button, :submit, :reset, :hidden").each(function (index) {

            switch ($(this).val()) {
                case "OR":
                case "AND":
                case "NOT":
                    if ($(this).parent().find(":text").val().length > 0) {
                        searchString += $(this).val() + " ";
                    }
                    break;
                default:
                    searchString += $(this).val() + " ";
                    break;
            }

            //            if ($(this).val().length > 0) {
            //                searchString += $(this).val() + " ";
            //            }

        });
        $("#SearchTerms").val(searchString);
        if ($("#seachTerm1").val().length > 1) {
            $("#PlainTextSearch").html("You're searching for data containing <strong>" + $("#seachTerm1").val() + "</strong>");
        }

        if ($("#seachTerm1").parent().next().find(":input").next(":input").val().length != 0) {
            PlainTextString = "";
            PlainTextString = "You're searching for data containing <strong>" + $("#seachTerm1").val() + "</strong> ";

            $(":input", "#rule-group").not(":button, :submit, :reset, :hidden, #seachTerm1").each(function (index) {
                switch ($(this).val()) {
                    case "OR":
                        PlainTextString += "<span class='blueBalls'>" + $(this).val() + "</span> ";
                        break;
                    case "AND":
                        PlainTextString += "<span class='blueBalls'>combined with</span> ";
                        break;
                    case "NOT":
                        PlainTextString += "<span class='blueBalls'>but " + $(this).val() + "</span> ";
                        break;
                    default:
                        PlainTextString += "<strong>" + $(this).val() + "</strong> ";
                        break;
                }
            });

            //            $("#seachTerm1").parent().next().find(":input").each(function (index) {
            //                PlainTextString += $(this).val() + " ";
            //            });
            $("#PlainTextSearch").html(PlainTextString);

        }
    }

});


function SetSelectedVenue(venueId, venueName, venueAddress, venueCity, venueState, venueZip) {
    $("#FoursquareVenueID").val(venueId);
    $("#selectedVenue").html("<strong>" + venueName + "</strong><br />" + venueAddress + "<br />" + venueCity + ", " + venueState + "<br />" + venueZip + "<br />");
    $("#foursquare-search").val("Change venue");

    //Dismis the popup
    //$.colorbox.close();
    parent.$.fn.colorbox.close();

    $("#moreinformation-info .blocking").unblock();
    $("#moreinformation-info h4").unblock();
    $("#step4").removeClass("stepDisable");
    $("#step3 span").html("&nbsp;");
    $("#step3").addClass("stepComplete");
    $(".submitForm").removeClass("disabled");
    $(".submitForm").removeAttr("disabled");

}