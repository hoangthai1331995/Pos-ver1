function Redirect(url, timeInterval = 0) {
    var tick = setInterval(function () {
        clearInterval(tick);
        window.location.href = url;
    }, timeInterval);
}