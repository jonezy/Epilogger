(function ($) {
    $.fn.gallery = function (images, selectOnClick) {
        return this.each(function () {
            var imagesPerColumn = 5; //6

            var html = '';
            $(images).each(function () {
                html += '<li>';
                html += '<div class="facebookGalleryImg unselectedImage">';
                        if (!selectOnClick)
                            html += '<a href=' + this.url + '>';

                        html += '<img width="180" src="' + this.src + '" />';
                    html += '</div>';

                    html += '<div class="facebookGalleryAlbumName">' + this.albumname + '</div>';
                    if (!selectOnClick)
                        html += '</a>';

                    html += '</li>';
            
            });

            $(this).html(html);

            if (selectOnClick) {
                $(this).find('img').click(function () {
                    $(this).parent().toggleClass('selectedImage').toggleClass('unselectedImage');
                });
            }
        });
    };
})(jQuery);





//(function ($) {
//    $.fn.gallery = function (images, selectOnClick) {
//        return this.each(function () {
//            var imagesPerColumn = 5; //6

//            var html = '<tr>';
//            var currentRowImagesCount = 0;
//            $(images).each(function () {
//                html += '<td>';
//                html += '<div>';
//                if (!selectOnClick)
//                    html += '<a href=' + this.url + '>';
//                html += '<img width="100" src="' + this.src + '" class="unselectedImage"/>';
//                html += '</div>';
//                html += '<div>' + this.albumname + '</div>';
//                if (!selectOnClick)
//                    html += '</a>';
//                html += '</td>';

//                currentRowImagesCount++;

//                if (currentRowImagesCount == imagesPerColumn) {
//                    html += "</tr><tr>";
//                    currentRowImagesCount = 0;
//                }
//            });

//            html += '</tr>';

//            $(this).html(html);

//            if (selectOnClick) {
//                $(this).find('img').click(function () {
//                    $(this).toggleClass('selectedImage').toggleClass('unselectedImage');
//                });
//            }
//        });
//    };
//})(jQuery);