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


$("#showImage").on("show.bs.modal", function (e) {
    var imageSrc = $(e.relatedTarget).data("location");
    var photoId = $(e.relatedTarget).data("photo-id");
    var hasVoted = $(e.relatedTarget).data("has-voted");
    if (hasVoted === "True") {
        $("#vote-photo").hide();
    }
    $(e.currentTarget).find("input[type=hidden]").val(photoId);
    $("#imagepreview").attr("src", imageSrc);
});

$("#vote-photo").click(function() {
    var photoId = $("#current-photo-id").val();
    console.log(photoId);

    $.get("/Photos/Vote/?photoId=" + photoId, function (response) {
        if (response.result === "success") {
            $("#vote-photo").hide();
        }
    });
});
