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

$("#deadline-strategy").change(function () {
    if ($(this).val() == 1) {
        $("#number-of-participants").show();
        $("#deadline-input").hide();
    }
    else {
        $("#deadline-input").show();
        $("#number-of-participants").hide();
    }
});

