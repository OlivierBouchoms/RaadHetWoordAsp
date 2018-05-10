//All Javascript logic for the dice

var i;
var getal;
var countdowntimer;
var uiTimer;
var start;
var _value;
var nextroundtimer;

$(document).ready(function () {
    document.getElementById("btncountdown").disabled = true;
});

function throwDice(value) {
    i = 0;
    var dice = document.getElementById("dice");
    dice.classList.remove("w3-green");
    dice.classList.remove("w3-hover-white");
    dice.classList.add("w3-white");
    _value = parseInt(value);
    uiTimer = setInterval(changeValue, 125);
}

function changeValue() {
    getal = Math.random() * 3;
    getal = Math.floor(getal);
    $('dice').css("pointer-events", "none");
    document.getElementById("dice").disabled = true;
    document.getElementById("dice").innerHTML = getal;
    i++;
    if (i >= 40) {
        stopRolling();
        return;
    }
}

function stopRolling() {
    clearInterval(uiTimer);
    document.getElementById("btncountdown").disabled = false;
    document.getElementById.innerHTML = "Uitkomst: " + _value + " punten eraf!";
}

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
    var end = new Date();
    var seconds = end - start;
    seconds /= 1000;

    // get seconds 
    seconds = Math.round(seconds);
    var time = 3 - seconds;
    $('#btncountdown').val(time);
}