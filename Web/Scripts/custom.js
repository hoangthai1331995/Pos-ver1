function ShowMessage(type, title, message, timer, url) {
    if (typeof timer == 'undefined') {
        timer = 1000;
    }
    setTimeout(function () {
        $.notify({
            title: '<strong>' + title + '</strong>',
            message: message
        }, {
                type: type
            });
    }, timer);
    if (url != '') {
        window.location.href = url;
    }
}

function Redirect(url) {
    window.location.href = url;
}

function RedirectWithWait(url, timeInterval = 0) {
    var tick = setInterval(function () {
        clearInterval(tick);
        window.location.href = url;
    }, timeInterval);
}
