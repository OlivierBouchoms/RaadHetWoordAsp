$(document).ready(function () {
    $('#sortmenu a').click(GetTeams);
});

var GetTeams = function (event) {
    event.preventDefault(); // Blocks following of hyperlink

    var targetElement = $('#leaderboardcontainer');
    targetElement.html('Laden...');

    var element = $(event.currentTarget);
    var url = element.attr('href');

    $.get(url).done(function (view) {
        targetElement.html(view);
    });
}

