$("#show-my-contests").click(function () {
    $("#main-content").html("");

    $.get("/Users/MyContests", function (result) {
        $("#main-content").html(result);
    });
});

$("#show-photos").click(function () {
    $("#main-content").html("");

    $.get("/Photos/GetPhotos", function (result) {
        $("#main-content").html(result);
    });
});
