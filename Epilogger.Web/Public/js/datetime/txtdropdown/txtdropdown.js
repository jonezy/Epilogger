(function ($) {
    $.fn.txtdropdown = function (onshown) {
        return this.each(function () {
            var txt = $(this);

            // The first ul after the textbox becomes the dropdown
            var ddl = txt.next('ul:first');

            with (ddl) {
                // Apply CSS
                addClass('txtdropdown-ddl');

                // Position the dropdown directly below the textbox
                css('position', 'absolute');
                css('width', txt.width() + 29);
                css('left', txt.position().left + 23);
                css('top', txt.position().top + txt.height() + 12);

                // Hide the dropdown
                hide();
                css('visibility', 'visible');

                // Prevent the dropdown from auto-hiding when clicking the scroll buttons
                click(function (e) {
                    e.stopPropagation();
                });

                // Clicking an li sets the textbox val and hides the dropdown
                find('li').click(function () {
                    txt.val($(this).html()).select().focus().change();
                    txtdropdown_hide();
                });
            }

            with (txt) {
                // Surround the textbox with an outer div
                var outer = wrap('<div class="txtdropdown-outer"></div>').parent();

                // Create a "button" div inside the outer div
                var btn = outer.prepend('<div class="txtdropdown-btn">&nbsp;</div>').find('.txtdropdown-btn');
                btn.click(function () {
                    with (ddl) {
                        if (is(':visible')) {
                            txtdropdown_hide();
                        }
                        else {
                            txtdropdown_hide();
                            show();
                            if (onshown != null) onshown(txt, ddl);
                        }
                    }
                });

                // Make the textbox width smaller to make room for the button
                width(width() - btn.width() - 4);
                //css('border-right', '0');

                // Keypress in the textbox hides the dropdown
                keypress(function () {
                    txtdropdown_hide();
                });

                // Change in the textbox hides the dropdown
                change(function () {
                    txtdropdown_hide();
                });

                // Prevent the dropdown from auto-hiding when clicking the textbox
                outer.click(function (e) {
                    e.stopPropagation();
                });
            }

            // Auto-hide the dropdown(s)
            $(document).click(function () {
                txtdropdown_hide();
            });
        });
    };
})(jQuery);

function txtdropdown_hide() {
    $('.txtdropdown-ddl').hide();
}
