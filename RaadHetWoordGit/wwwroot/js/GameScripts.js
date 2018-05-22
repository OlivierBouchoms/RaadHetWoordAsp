//All Javascript logic for the game

var uri = "/api/GameApi";
var _increase;
var timer;
var uiTimer;
var start;
var totaltime = 30;

//Start the timer when document is loaded
$(document).ready(startTimer());

//Send a request to increase or decrease a team its score
function changeScore(event) {
    var sender = event.target;
    var id = sender.id;
    var element = document.getElementById(id);

    var _increase = !element.classList.contains("correct");

        $.ajax({
            type: 'PATCH',
            url: uri,
            data: { increase: _increase },
            dataType: "json",
            success: function () {
                if (_increase) {
                    element.classList.add("correct");
                    _increase = false;

                } else {
                    element.classList.remove("correct");
                    _increase = true;
                }
            },
            error: function () {
                alert("fout in backend");
            }
        });
}

function startTimer() {
    $("#btncountdown").text("30");
    timer = setTimeout(nextRound, 30000);
    start = new Date();
    uiTimer = setInterval(showCountdown, 1000);
}

function nextRound() {
    $("#btnnextround").trigger("click");
}

function showCountdown() {
    var end = new Date();
    var seconds = end - start;
    seconds /= 1000;

    // get seconds 
    seconds = Math.round(seconds);
    var time = totaltime - seconds;
    $("#remainingTime").text(time);
}
