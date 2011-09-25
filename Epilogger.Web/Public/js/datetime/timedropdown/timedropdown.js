(function ($) {
    $.fn.timedropdown = function () {
        return this.each(function () {
            var txt = $(this);
            with (txt) {
                // Create a ul after the textbox containing the time values
                // for the dropdown (this list could be made dynamic)
                parent().append('<ul><li>12:00 AM</li><li>12:30 AM</li><li>1:00 AM</li><li>1:30 AM</li><li>2:00 AM</li><li>2:30 AM</li><li>3:00 AM</li><li>3:30 AM</li><li>4:00 AM</li><li>4:30 AM</li><li>5:00 AM</li><li>5:30 AM</li><li>6:00 AM</li><li>6:30 AM</li><li>7:00 AM</li><li>7:30 AM</li><li>8:00 AM</li><li>8:30 AM</li><li>9:00 AM</li><li>9:30 AM</li><li>10:00 AM</li><li>10:30 AM</li><li>11:00 AM</li><li>11:30 AM</li><li>12:00 PM</li><li>12:30 PM</li><li>1:00 PM</li><li>1:30 PM</li><li>2:00 PM</li><li>2:30 PM</li><li>3:00 PM</li><li>3:30 PM</li><li>4:00 PM</li><li>4:30 PM</li><li>5:00 PM</li><li>5:30 PM</li><li>6:00 PM</li><li>6:30 PM</li><li>7:00 PM</li><li>7:30 PM</li><li>8:00 PM</li><li>8:30 PM</li><li>9:00 PM</li><li>9:30 PM</li><li>10:00 PM</li><li>10:30 PM</li><li>11:00 PM</li><li>11:30 PM</li></ul>');

                // Build the dropdown, attach a callback
                txtdropdown(timedropdown_shown);
            }
        });
    };
})(jQuery);

// Callback function to auto-scroll the dropdown to the nearest time
function timedropdown_shown(txt, ddl) {
    // If unable to parse time, default position is...
    var timeIndex = 0;

    // Parse the time value in the textbox
    var time = new Date('1/1/2010 ' + txt.val());

    if (!isNaN(time)) {
        // Determine the index of the li with the nearest time (round down)
        // We assume the times are static, every half-hour, starting with 12:00 AM
        timeIndex = (time.getHours() * 2) + (time.getMinutes() / 30);
    }

    // Select the li at the matching index and scroll to it
    ddl.scrollTo(ddl.children('li').eq(timeIndex));
}
