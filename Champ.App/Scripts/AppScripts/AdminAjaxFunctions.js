$("#show-contests").click(function () {
    $("#main-window").html("");

    $.get("/Dashboard/ContestManagement/Get", function (result) {
        $("#main-window").html(result);
    });
});

$("#show-users").click(function () {
    $("#main-window").html("");

    $.get("/Dashboard/UsersManagement/Get", function (result) {
        $("#main-window").html(result);
    });
});

$("#show-photos").click(function () {
    $("#main-window").html("");

    $.get("/Dashboard/Pictures/Get", function (result) {
        $("#main-window").html(result);
    });
});

$("#user-search-field").keyup(function (input) {
    $("#user-search-result").html("");

    if ($(input.target).val().length > 0) {
        $.get("/Users/SearchUsers/?username=" + $(input.target).val(), function (result) {
            $("#user-search-result").html(result);
        });
    }
});