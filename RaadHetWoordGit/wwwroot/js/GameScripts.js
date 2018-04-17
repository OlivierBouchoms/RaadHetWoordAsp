//All Javascript logic for the game

//https://localhost:44334/api/GameApi
var uri = "/api/GameApi";
var _increase;

var timer;

$(document).ready(startTimer());

function changeScore(event) {
    //event.target is the sender of the event, in this case the element that's clicked
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

//Initialize the timer with a timeout of 30 seconds
function startTimer() {
    timer = setTimeout(nextRound, 30000);
}

function nextRound() {
    $.ajax({
        type: 'POST',
        url: "/api/GameApi/NextRound",
        crossDomain: true,
        success: function(data) {
            window.location.href = data;
        },
        error: function () {
            alert("fout in backend");
        }
    })
}
