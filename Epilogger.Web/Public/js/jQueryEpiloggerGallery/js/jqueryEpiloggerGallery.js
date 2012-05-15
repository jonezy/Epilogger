(function ($) {
    $.fn.gallery = function (images, selectOnClick) {
        return this.each(function () {
            var imagesPerColumn = 6;

            var html = '<tr>';
            var currentRowImagesCount = 0;
            $(images).each(function () {
                html += '<td>';
                if (!selectOnClick)
                    html += '<a href=' + this.url + '>';
                html += '<img width="100" src="' + this.src + '"/>';
                if (!selectOnClick)
                    html += '</a>';
                html += '</td>';

                currentRowImagesCount++;

                if (currentRowImagesCount == imagesPerColumn) {
                    html += "</tr><tr>";
                    currentRowImagesCount = 0;
                }
            });

            html += '</tr>';

            $(this).html(html);

            if (selectOnClick) {
                $(this).find('img').click(function () {
                    $(this).toggleClass('selectedImage');
                });
            }
        });
    };
})(jQuery);