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

    $('article').click(function () {
        var url = $(this).attr('data-action');

        if (url !== undefined)
            window.location.href = url;
    });
});

function displayAccessInfoPopup(accessInfo) {
    console.log(accessInfo);
    var modal = $('#accessInfoModal');
    modal.modal();
    modal.find('.modal-title').text(accessInfo.Name);
    var access = "";
    $.each(accessInfo.AccessInfos, function (i, v) {
        access += "<p>";
        access += v.Name + " accessed this data at " + v.Date + " for " + v.Reason;
        access += "</p>";
    });
    modal.find('.modal-body').text("");
    modal.find('.modal-body').append(access);
}