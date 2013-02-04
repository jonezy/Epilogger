

(function ($) {

    $.fn.saveClicks = function () {
        $(this).bind('mousedown.clickmap', function (evt) {
//            $.post('/Home/ClickMap', {
//                x: evt.pageX,
//                y: evt.pageY,
//                location: escape(document.location.pathname)
//            });
        });
    };

    $.fn.stopSaveClicks = function () {
        $(this).unbind('mousedown.clickmap');
    };

})(jQuery);


$.displayClicks = function (settings) {
    $('<div id="clickmap-overlay"></div>').appendTo('body');
    $('<div id="clickmap-loading"></div>').appendTo('body');
    $.get('/Home/_GetClickMap', { location: escape(document.location.pathname) },
        function (htmlContentFromServer) {
            $(htmlContentFromServer).appendTo('body');
            $('#clickmap-loading').remove();
        }
    );
};

$.removeClicks = function () {
    $('#clickmap-overlay').remove();
    $('#clickmap-container').remove();
};