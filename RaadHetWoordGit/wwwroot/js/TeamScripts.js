$(document).ready(function setWinLossColor() {
    var winLossScore = document.getElementById("winloss").innerText;
    if (winLossScore < 1) {
        document.getElementById("winloss").style.color = "red";
    }
    if (winLossScore > 1) {
        document.getElementById("winloss").style.color = "green";
    }
    if (winLossScore === 1) {
        document.getElementById("winloss").style.color = "black";
    }
});