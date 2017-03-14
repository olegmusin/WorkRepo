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

    $(".qty").click(function (e) {
        e.stopPropagation();
    });

    $("#shiftslist tr").click(function() {
        $(this).addClass('selected').siblings().removeClass('selected');
        var value = $(this).find('td:first').html();
        alert(value);
    });



    $(function () {
        $(".list-group.checked-list-box .list-group-item").each(function () {

            // Settings
            var $widget = $(this),
             
                $checkbox = $('<input type="checkbox" class="hidden" />'),
                color = ($widget.data("color") ? $widget.data("color") : "primary"),
                style = ($widget.data("style") === "button" ? "btn-" : "list-group-item-"),
                settings = {
                    on: {
                        icon: "glyphicon glyphicon-check"
                    },
                    off: {
                        icon: "glyphicon glyphicon-unchecked"
                    }
                };
            
            $widget.css("cursor", "pointer");
            $widget.append($checkbox);

             //Event Handlers
      
            $widget.on("click", function () {
                $checkbox.prop("checked", !$checkbox.is(":checked"));
                $checkbox.triggerHandler("change");
                updateDisplay();
            });
            $checkbox.on("change", function () {
                updateDisplay();
            });


            // Actions
            function updateDisplay() {
                var isChecked = $checkbox.is(":checked");

                // Set the button's state
                $widget.data("state", (isChecked) ? "on" : "off");

                // Set the button's icon
                $widget.find(".state-icon")
                    .removeClass()
                    .addClass("state-icon " + settings[$widget.data("state")].icon);

                // Update the button's color
                if (isChecked) {
                    $widget.addClass(style + color);
                } else {
                    $widget.removeClass(style + color);
                }
            }

            // Initialization
            function init() {

                if ($widget.data("checked") === true) {
                    $checkbox.prop("checked", !$checkbox.is(":checked"));
                }

                updateDisplay();

                // Inject the icon if applicable
                if ($widget.find(".state-icon").length === 0) {
                    $widget.prepend('<span class="state-icon ' + settings[$widget.data("state")].icon + '"></span>');
                }
            }
            init();
        });

        $("#get-checked-data").on("click", function (event) {
            event.preventDefault();
            var checkedItems = {}, counter = 0;
            $("#check-list-box li.active").each(function (idx, li) {
                checkedItems[counter] = $(li).text();
                counter++;
            });
            $("#display-json").html(JSON.stringify(checkedItems, null, "\t"));
        });
    });
})
