var countdowntimer;
var uiTimer;
var start;
var end;

function nextRound() {
    $('#btnnextround').trigger("click");
}

function redirectToGame() {
    $("#btncountdown").prop("disabled", true);
    $('#btncountdown').val("3");
    start = new Date();
    countdowntimer = setTimeout(nextRound, 3000);
    uiTimer = setInterval(showCountdown, 1000);
}

function showCountdown() {
    end = new Date();
    var seconds = end - start;
    seconds /= 1000;

    // get seconds 
    seconds = Math.round(seconds);
    var time = 3 - seconds;
    $('#btncountdown').val(time);
}