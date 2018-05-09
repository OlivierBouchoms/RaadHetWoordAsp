//All Javascript logic for the dice

var i;
var getal;
var countdowntimer;
var uiTimer;
var _value;

function throwDice(value) {
    i = 0;
    _value = parseInt(value);
    uiTimer = setInterval(changeValue, 260);
}

function changeValue() {
    getal = Math.random() * 3;
    getal = Math.floor(getal);
    $('dice').css("pointer-events", "none");
    document.getElementById("dice").disabled = true;
    document.getElementById("dice").innerHTML = getal;
    i++;
    if (i >= 20) {
        stopRolling();
        return;
    }
}

function stopRolling() {
    clearInterval(uiTimer);
    document.getElementById("dice").innerHTML = _value;
}