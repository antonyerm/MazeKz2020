$(function () {

    // Add Accident button click
    $("body").on("click", "#buttonAddAccident", function (event) {
        var whereToPutForm = $(this).nextAll("#divAddAccident");
        renderForm(null, whereToPutForm);
        $(this).addClass("disabled");
        event.preventDefault();
    })

    function renderForm(id, whereToPutForm) {
        var url = "/Life/AddEditAccident";
        $.get(url, { id: id })
            .done(function (response) {
                whereToPutForm.hide().html(response)
                    .stop(true, true).slideDown()
                    .find("input").first().focus();
            });
    }

    // Cancel Adding Accident button click
    $("body").on("click", "#buttonCancelAddAccident", function (event) {
        $("#divAddAccident").slideUp(function () {
            $("#buttonAddAccident").removeClass("disabled");
            $("#divAddAccident").empty();
        });
        event.preventDefault();
    })
})
