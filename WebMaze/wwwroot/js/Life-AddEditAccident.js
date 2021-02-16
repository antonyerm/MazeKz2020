$(function () {

    /**
     * Function to handle Add Accident button click.
     * Adds content to div with id=#divAddAccident.
     */
    $("body").on("click", "#buttonAddAccident", function (event) {
        var url = "/Life/AddEditAccident";
        var accidentId = 0;
        var whereToPutContent = $(this).nextAll("#divAddAccident");
        var focusSelector = "input#AccidentDate";
        renderContent(url, accidentId, whereToPutContent, focusSelector);
        ToggleDisableAllButtons("disable");
        event.preventDefault();
    })

    /**
    * Function to add a new row and a big cell to the table and fill it with content
    * Receives: url - for ajax get,
    *           button - the query for html element in the table for which the position of a new row is calculated
    *           focusSelector - the element to focus on in the end
    */
    function AddRowAndFillIt(url, button, focusSelector) {
        var accidentId = button.attr("id").split("-")[1];

        var currentRow = button.closest("tr");
        currentRow.addClass("table-info");

        // create <tr class="table-info"><td></td></tr>
        currentRow.after($("<tr />").addClass("table-info").append("<td />"));
        var whereToPutContent = currentRow.next().children("td");
        whereToPutContent.attr("colspan", 12);

        renderContent(url, accidentId, whereToPutContent, focusSelector);
    }

    /**
    * Function to put content to the specified element.
    * Takes content from controller.
    */
    function renderContent(url, id, whereToPutContent, focusSelector) {
        $.get(url, { id: id })
            .done(function (response) {
                whereToPutContent.addClass("overflow-hidden");
                whereToPutContent.html(response);
                whereToPutContent.hide(0, function () {
                    whereToPutContent.stop(true, true);
                    whereToPutContent.slideDown();
                    whereToPutContent.find(focusSelector).focus();
                });
            });
    };

    /**
    * Function to handle "Edit Accident" button click
    * Fills the table with the ajax content
    */
    $("body").on("click", "a[id|='accidentEdit']", function (event) {
        var t = $(this);
        AddRowAndFillIt("/Life/AddEditAccident", t, "input#AccidentDate");
        ToggleDisableAllButtons("disable");
    })


    /**
    * Function to handle "OK" and "Cancel" buttons click after Add or Edit Accident operation.
    * Sends form to AddEditAccident(POST) controller, receives validated content. If it's OK, renders the whole page.
    */
    $("body").on("click", "#buttonOkAddAccident, #buttonCancelAddAccident, #buttonCancelAccidentDetails", function (event) {
        t = $(this);
        var newContent = t.closest("#newContent");
        if (t.attr("id") === "buttonCancelAddAccident" || t.attr("id") == "buttonCancelAccidentDetails") {
            // "Cancel" button in Add Accident
            HideForm(newContent);
        }
        else
            if (t.attr("id") == "buttonOkAddAccident") {
                // "OK" ("send") button handler
                // get validation result
                var url = "/Life/AddEditAccident";
                $.post(url,
                    newContent.serialize(),
                    function (result) {
                        if (result.success) {
                            // validation is OK
                            HideForm(newContent).done(function () {
                                $.get("/Life/ShowAccidents", function (result) {
                                    document.open();
                                    document.write(result);
                                    document.close();
                                });
                            });
                        }
                        else {
                            // validation has errors // TODO check if td is needed instead form
                            newContent.parent().html(result);
                        };
                    }
                );
            };
        event.preventDefault();
    });

    /**
    * Function to toggle disabled state of nearly all buttons.
    * It's needed when editing or adding new accident.
    */
    function ToggleDisableAllButtons(state) {
        if (state === "disable") {
            $("#buttonAddAccident").addClass("disabled");
            $("table#accidents .btn").addClass("disabled");
        }
        else {
            $("#buttonAddAccident").removeClass("disabled");
            $("table#accidents .btn").removeClass("disabled");
        }
    }

    /**
    * Function to hide form after Add Accident or Edit Accident operation.
    * It's needed to handel Cancel button or when validation of form is OK.
    */
    function HideForm(currentForm) {
        var wait = $.Deferred();
        var currentRow = currentForm.closest("tr");
        var previousRow = currentRow.prev("tr");

        currentForm.slideUp(function () {
            if (currentRow.length === 0) {
                // it was Add operation, remove form only
                currentForm.remove();
            } else {
                // it was Edit operation, remove the table row
                currentRow.remove();
                previousRow.removeClass("table-info");
            };
            ToggleDisableAllButtons("enable");
            wait.resolve();
        });
        return wait;
    }

    /**
    * Function to handle "Accident Details" button click
    * Adds a new row and a big cell to the table and fills it with content.
    */
    $("body").on("click", "a[id|='accidentDetails']", function (event) {
        var t = $(this);
        AddRowAndFillIt("/Life/AccidentDetails", t);
        ToggleDisableAllButtons("disable");
    })

})
