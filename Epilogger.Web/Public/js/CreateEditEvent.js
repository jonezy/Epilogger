
head.ready(function () {

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