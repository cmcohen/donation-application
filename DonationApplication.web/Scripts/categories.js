$(function () {
    $("#add-cat-btn").on('click', function () {
        $(".new-cat").modal();
    })

    $(".edit-cat-btn").on('click', function () {
        $(".cat-name").remove();
        $(".cat-txt-bx").show();
        $(".edit-cat-btn").hide();
        $(".update-cat-btn").show();
    })

    $(".update-cat-btn").on('click', function () {
        var id = $(this).data('id');
        var name = $(`#cat-txt-${id}`).val();
        $.post('/admin/updatecategory', { id: id, name: name }, function () {
            window.location.reload();
        });
    });
   
})