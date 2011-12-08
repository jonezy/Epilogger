
head.ready(function () {


    //Setup the Date span
    $('#EventTimePicker').dateSpanPicker();
    $('#CollectionTimePicker').dateSpanPicker();

    //Setup the page
    //$("#createSteps li").addClass("stepDisable");
    $("#Name").focus();
    //$("#step1").removeClass("stepDisable"); 

    //Block the Other sections
    $("#whattoinclude-info .blocking").block({ message: null });
    $("#whattoinclude-info h4").block({ message: null });

    $("#whenandwhere-info .blocking").block({ message: null });
    $("#whenandwhere-info h4").block({ message: null });

    $("#moreinformation-info .blocking").block({ message: null });
    $("#moreinformation-info h4").block({ message: null });

    $(".submitForm").attr('disabled', 'disabled');
    //$(".disabled").block({ message: null });



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


    function checkBlocking() {
        //        var i = 0;
        //        $(":input", "#createevent-info").not(":button, :submit, :reset, :hidden").each(function (index) {
        //            if ($(this).val().length > 0) {
        //                i++;
        //            }
        //        });
        //        if (i == 2) {
        //            $("#whattoinclude-info .blocking").unblock();
        //            $("#whattoinclude-info h4").unblock();
        //            $("#step2").removeClass("stepDisable");
        //        }

        //Add the Complete style
        if ($("#Name").val().length > 0 && $("#Subtitle").val().length > 0 && $("#CategoryID").val() != 0) {
            $("#whattoinclude-info .blocking").unblock();
            $("#whattoinclude-info h4").unblock();
            $("#step2").removeClass("stepDisable");
            $("#step1 span").html("&nbsp;")
            $("#step1").addClass("stepComplete");
        }

        if ($("#seachTerm1").val().length > 0) {
            $("#whenandwhere-info .blocking").unblock();
            $("#whenandwhere-info h4").unblock();
            $("#step3").removeClass("stepDisable");
            $("#step2 span").html("&nbsp;")
            $("#step2").addClass("stepComplete");
        }

        if (showMore) {
            $("#moreinformation-info .blocking").unblock();
            $("#moreinformation-info h4").unblock();
            $("#step4").removeClass("stepDisable");
            $("#step3 span").html("&nbsp;")
            $("#step3").addClass("stepComplete");
            $(".submitForm").removeClass("disabled");
            $(".submitForm").removeAttr("disabled");
        }

        if ($("#Description").val().length > 0 && $("#WebsiteURL").val().length > 0 && $("#TwitterAccount").val().length > 0 && $("#FacebookPageURL").val().length > 0 && $("#Cost").val().length > 0) {
            $("#step4 span").html("&nbsp;")
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

                $("#seachTerm" + counter).bind("keyup", function (e) {
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

    $(":input", "#rule-group").not(":button, :submit, :reset, :hidden").bind("keyup", function (e) {
        buildSearchString();
    });

    function buildSearchString() {
        var SearchString = "";
        $(":input", "#rule-group").not(":button, :submit, :reset, :hidden").each(function (index) {
            SearchString += $(this).val() + " ";
        });
        $("#SearchTerms").val(SearchString);
    }


});