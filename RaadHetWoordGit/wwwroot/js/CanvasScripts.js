$(document).ready(function loadChart() {
    var wins = parseInt(document.getElementById("wins").innerHTML);
    var losses = parseInt(document.getElementById("losses").innerHTML);

    var chart = new CanvasJS.Chart("chartcontainer",
        {
            animationEnabled: true,
            data: [
                {
                    type: "pie",
                    startAngle: 0,
                    yValueFormatString: "##0.00\"%\"",
                    indexLabel: "{label} {y}",
                    dataPoints: [
                        {
                            y: wins / (losses + wins) * 100,
                            label: "Wins"
                        },
                        {
                            y: losses / (losses + wins) * 100,
                            label: "Losses"
                        }
                    ]
                }
            ]
        });
    chart.render();
});