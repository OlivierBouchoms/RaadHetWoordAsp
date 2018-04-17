//All Javascript logic for the game

var uri = "/api/GameApi";
var domain = document.domain;
var _increase;
var timer;

//Start the timer when document is loaded
$(document).ready(startTimer() );

function changeScore(event) {
    //event.target is the sender of the event, in this case the element that's clicked
    var sender = event.target;
    var id = sender.id;
    var element = document.getElementById(id);

    var _increase = !element.classList.contains("correct");

    //If else statement kan in callback, is netter
    
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
    timer = setTimeout(nextRound, 30000);
}

function nextRound(event) {
    $.ajax({
        type: 'POST',
        url: uri,
        success: function (uri) {
            console.log(uri);
            window.location.href = uri;
        },
        error: function () {
            alert("fout in backend");
        }
    })
}