
//$(document).ready(function () {
//    var basehUrl = "http://search.twitter.com/search.json";
//    $('#searchButton').click(function () {
//        $("#tweetMainContainer").empty();
//        $("#getMoreTweetsButton").hide();
//        var query1 = $('#searchText').val().replace("#", "");
//        var query2 = $('#searchText2').val().replace("#", "");
//        
//        GetTweets(query, 20);
//    });
//    $("#getMoreTweetsButton").hide();
//});

$(document).ready(function () {
    var basehUrl = "http://search.twitter.com/search.json";
    $('#searchButton').click(function () {
        $("#tweetMainContainer").empty();
        var query = "";
        $("#getMoreTweetsButton").hide();
        $.each($('.searchText'), function () {
            if ($(this).val() != "") {
                if ($(this).val().indexOf(" ") >= 0) {
                    if ($(this).val().substring(0, 1) != '"' && $(this).val().substring($(this).val().legnth-1, 1) != '"')
                    {
                        $(this).val('"' + $(this).val() + '"');
                    }
                }
                query = query + $(this).val().replace('#', '') + ' OR ';
            }
        });
        query = query.substring(0, query.length - 4);
        GetTweets(query, 20);
    });
    $("#getMoreTweetsButton").hide();
});
function GetTweets(query, count) {
    var searchUrl = "http://search.twitter.com/search.json?q=" + query + "&rpp=" + count + "&lang=en&callback=?";
    $.getJSON(searchUrl, function (data) {
        if (data.results.length == 0) {
            NoResults();
        }
        else {
            ApplyTwitterTemplate(data);
        }
    });
}

function GetMoreTweets(query) {
    var searchUrl = "http://search.twitter.com/search.json" + query + "&callback=?";
    $.getJSON(searchUrl, function (data) {
        ApplyTwitterTemplate(data);
    });
}

function ApplyTwitterTemplate(data) {
    $tweetSubContainer = $("#tweetSubContainer").clone();
    $tweetSubContainer.hide();
    $tweetSubContainer.setTemplateURL('../Public/templates/Twitter.htm',
                                 null, { filter_data: false });
    $tweetSubContainer.processTemplate(data);
    $("#tweetMainContainer").append($tweetSubContainer);

    //show get more button and set next page url
    $("#getMoreTweetsButton").unbind('click', null);
    $("#getMoreTweetsButton").click(function () {
        GetMoreTweets(data.next_page);
    }).show();

    $tweetSubContainer.fadeIn("slow");
}

function NoResults() {
    $("#tweetMainContainer").html('<div class="noResults">There are no results for those search terms.</div>');

    //show get more button and set next page url
    $("#getMoreTweetsButton").unbind('click', null);
    $("#getMoreTweetsButton").click(function () {
        GetMoreTweets(data.next_page);
    }).show();

    $tweetSubContainer.fadeIn("slow");
}