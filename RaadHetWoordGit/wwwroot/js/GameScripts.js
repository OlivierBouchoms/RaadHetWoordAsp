//All Javascript logic for the game

//https://localhost:44334/api/GameApi
var uri = "/api/GameApi";
var domain = document.domain;

function changeScore(event) {
    //event.target is the sender of the event, in this case the element that's clicked
    var sender = event.target;
    var score = Number(sender.textContent);
    var id = sender.id;
    var element = document.getElementById(id);

    var isCorrect = element.classList.contains("correct");
    if (isCorrect) {
        $.ajax({
            type: 'PATCH',
            url: uri,
            data: { increase: isCorrect },
            dataType: "json",
            success: function() {
                element.classList.remove("correct");
            },
            error: function() {
                alert("fout");
            }
        });
        //Ajax request om score van huidig team te verminderen
    } else {
        //Ajax request om score van huidig team te verhogen
        $.ajax({
            type: 'PATCH',
            url: uri,
            data: { increase: isCorrect },
            dataType: "json",
            success: function () {
                element.classList.add("correct");
            },
            error: function () {
                alert("fout");
            }
        });
    }
    element.innerHTML = score;
}