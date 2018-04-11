$(function () {
    var inputNb = 0;
    $('.input-add-btn').click(
        function () {
            inputNb++;
            $('#input').clone().appendTo('#inputs').attr('id', inputNb);
            $('#' + inputNb).show();
            $('#' + inputNb).find('.input-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                $('#inputs').find('#' + id).remove();
            });
        }
    );
});