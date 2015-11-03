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
    $("#imagepreview").attr("src", imageSrc);
    //$(e.currentTarget).find("input[type=hidden]").val(contestId);
});
