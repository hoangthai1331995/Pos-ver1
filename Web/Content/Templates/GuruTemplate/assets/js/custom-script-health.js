"use strict";
$(document).ready(function() {
    var a = $(window).height() - 135;
    $(".main-friend-list").slimScroll({
        height: a,
        allowPageScroll: false,
        wheelStep: 5,
        color: '#1b8bf9'
    });
});