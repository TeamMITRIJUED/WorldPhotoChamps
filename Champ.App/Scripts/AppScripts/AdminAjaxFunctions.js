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