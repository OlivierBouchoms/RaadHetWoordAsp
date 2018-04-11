//All Javascript logic for the game

function changeScore(event) {
    //event.target is the sender of the event, in this case the element that's clickedi
    var sender = event.target;
    var score = Number(sender.textContent);
    var id = sender.id;
    var element = document.getElementById(id);

    var isCorrect = element.classList.contains("correct");
    if (isCorrect) {

        //Ajax request om score van huidig team te verminderen
        element.classList.remove("correct");
    } else {
        //Ajax request om score van huidig team te verhogen
        element.classList.add("correct");
    }
    element.innerHTML = score;
}