// Write your Javascript code.
$(document).ready(function () {
    $(".js-cancel-shift").click(function (e) {
        var link = $(e.target);
        if (confirm("Are you sure you want to cancel this shift?")) {
            $.ajax({
                url: "/api/projects/" + link.attr("data-project-id") + "/shifts/delete/" + link.attr("data-shift-id"),
                method: "DELETE"
            })
            .done(function () {
                link.parents("tr").fadeOut(function () {
                    $(this).remove();
                });
            })
            .fail(function () {
                alert("Failed to cancel the shift!")
            });
        }
    })
})
