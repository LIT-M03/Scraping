$(function () {
    $(".details").on('click', function() {
        var url = $(this).data('url');
        $.post("/home/getdetails", { url: url }, function(result) {
            $(".modal-body").html(result.Html);
            $(".modal").modal();
        });
    });
});