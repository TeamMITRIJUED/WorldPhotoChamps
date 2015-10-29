$("#editContest").on("show.bs.modal", function (e) {
    var contestId = $(e.relatedTarget).data("contest-id");
    $(e.currentTarget).find("input[type=hidden]").val(contestId);
});

$("#inviteUsers").on("show.bs.modal", function (e) {
    var contestId = $(e.relatedTarget).data("contest-id");
    $(e.currentTarget).find("input[type=hidden]").val(contestId);
});

$("#user-search-field").keyup(function (input) {
    $("#user-search-result").html("");

    if ($(input.target).val().length > 0) {
        var username = $(input.target).val();
        var contestId = $("#user-search-contestId").val();
        console.log(contestId);
        $.get("/Users/SearchUsers/?username=" + username + "&contestId=" + contestId, function (result) {
            $("#user-search-result").html(result);
        });
    }
});