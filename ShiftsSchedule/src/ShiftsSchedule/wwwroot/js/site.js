// Write your Javascript code.
$(document).ready(function () {

    //Cancel Shift
    $(".js-cancel-shift").click(function (e) {
        var link = $(e.target);
        bootbox.dialog({
            message:
            "Are you sure you want to cancel this shift?",
            title:
            "Confirm",
            buttons:
            {
                no: {
                    label: "No",
                    className:
                    "btn-default",
                    callback:
                    function () {
                        bootbox.hideAll();
                    }
                },
                yes: {
                    label: "Yes",
                    className:
                    "btn-danger",
                    callback:
                    function () {
                        $.ajax({
                            url: "/api/projects/" +
                            link.attr("data-project-id") +
                            "/shifts/delete/" +
                            link.attr(
                                "data-shift-id"),
                            method:
                            "DELETE"
                        })
                            .done(function () {
                                link.parents("tr").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function () {
                                bootbox.alert("Failed to cancel the shift!");
                            });

                    }
                }
            }
        });


    });
})
