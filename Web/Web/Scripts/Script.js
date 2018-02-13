$(function () {
    $("a.lang").each(function (index) {
        $(this).click(function (evt) {
            evt.preventDefault();
            var langValue = $(this).text();
            var langCode;
            switch (langValue) {
                case 'EN':
                    langCode = 'en-US';
                    break;
                case 'FR':
                    langCode = 'fr-BE';
                    break;
                case 'NL':
                    langCode = 'nl-BE';
                    break;
            }
            document.cookie = "Language=" + langCode + ";";
            location.reload();
        });
    });
});

$('#myModal').on('shown.bs.modal', function () {
    $('#myInput').focus()
})