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
    //Cancel Project
    $(".js-cancel-project").click(function (e) {
        var link = $(e.target);
        bootbox.dialog({
            message:
            "Are you sure you want to cancel this Project? " +
                "Every envolved person will get the cancel note then.",
            title:
            "Confirm Project Cancel",
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
                            url: "/projects/delete/" +
                            link.attr("data-project-id"),
                            method:
                            "GET"
                        })
                            .done(function () {
                                link.parents("tr").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function () {
                                bootbox.alert("Failed to cancel the Project!");
                            });

                    }
                }
            }
        });
    });
    //Delete Worker
    $(".js-delete-worker").click(function (e) {
        var link = $(e.target);
        bootbox.dialog({
            message:
            "Are you sure you want to delete this Worker? " +
                "User will get the corresponding notice.",
            title:
            "Confirm Worker Delete",
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
                            url: "/workers/delete/" +
                            link.attr("data-worker-id"),
                            method:
                            "GET"
                        })
                            .done(function () {
                                link.parents("tr").fadeOut(function () {
                                    $(this).remove();
                                });
                            })
                            .fail(function () {
                                bootbox.alert("Failed to delete worker!");
                            });

                    }
                }
            }
        });
    });
})
