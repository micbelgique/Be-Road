$(function () { 
    $('.delete-link').click(
        function () {
            var value = $(this).parent().parent().children().first().text();
            $('#modalValue').val(value);
        }
    );
});