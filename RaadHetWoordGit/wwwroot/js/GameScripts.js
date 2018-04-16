//All Javascript logic for the game

//https://localhost:44334/api/GameApi
var uri = "/api/GameApi";
var domain = document.domain;
var _increase;


//event.target is the sender of the event, in this case the element that's clicked.
function changeValue(event) {
    var sender = event.target;
    var id = sender.id;
    var element = document.getElementById(id);

    var isCorrect = element.classList.contains("correct");
    if (isCorrect) {
        element.classList.remove("correct");
    }
    else {
        element.classList.add("correct");
    }
    element.innerHTML = score;
}

function changeScore(event) {
    //event.target is the sender of the event, in this case the element that's clicked
    var sender = event.target;
    var id = sender.id;
    var element = document.getElementById(id);

    var _increase = !element.classList.contains("correct");

    if (_increase) {
        $.ajax({
            type: 'PATCH',
            url: uri,
            data: { increase: _increase },
            dataType: "json",
            success: function () {
                element.classList.add("correct");
                _increase = false;
                alert("correct");
            },
            error: function () {
                alert("fout");
            }
        });
        //Ajax request om score van huidig team te verminderen
    } else {
        //Ajax request om score van huidig team te verhogen
        $.ajax({
            type: 'PATCH',
            url: uri,
            data: { increase: _increase },
            dataType: "json",
            success: function () {
                element.classList.remove("correct");
                _increase = true;
            },
            error: function () {
                alert("fout");
            }
        });
    }
}