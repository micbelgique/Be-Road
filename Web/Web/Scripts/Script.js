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

    $('div').click(function () {
        var url = $(this).attr('data-action');

        if (url !== undefined)
            window.location.href = url;
    });
});

function formatDate(date) {
    var newDate = new Date(date);
    var day = newDate.getDate();
    var month = newDate.getMonth();
    var year = newDate.getFullYear();

    return day + '/' + month + '/' + year;
}


function displayAccessInfoPopup(accessInfo) {
    var modal = $('#accessInfoModal');
    modal.modal();
    modal.find('#exampleModalLabel').text(accessInfo.Name);
    var access = "<ul>";
    $.each(accessInfo.AccessInfos, function (i, v) {
        access += "<li>";
        access += "<strong>" + v.Name + "</strong> " + Resources.Accessed + " <strong>" + formatDate(v.Date) + "</strong> " + Resources.For + " <strong>" + v.Reason + "</strong>";
        access += "</li>";
    });
    access += "</ul>"
    modal.find('.modal-body').text("");
    modal.find('.modal-body').append(access);
    $('#accessInfoModal').modal('open');
}

$(document).ready(function () {
    $('.collapsible').collapsible();

    clicked = false;

    $("#ps-identity").click(
        function (event) {
            if ($(event.target).is('section')) {
                if (!clicked) {
                    $(this).animate({ width: '+=230px' }, 350);
                    $('#ps-sections').animate({ width: '-=230px' }, 350);
                    $('#collap').css('display', 'block');
                    $('#chkid').fadeTo(350, 1, function () {
                        $('#chkid').css('display', 'inline-block');
                    });
                    $('#closer').css('display', 'block');
                    $('#collap').fadeTo(350, 1);
                    $('#opener').fadeTo(200, 0, function () {
                        $('#opener').css('display', 'none');
                    });
                    clicked = true;
                }
            }
        });

    $("#closer").click(
        function (event) {
            if (clicked) {
                $('#ps-identity').animate({ width: '-=230px' }, 350);
                $('#ps-sections').animate({ width: '+=230px' }, 350);
                $('#chkid').fadeTo(200, 0, function () {
                    $('#chkid').css('display', 'none');
                });
                $('#collap').fadeTo(200, 0, function () {
                    $('#collap').css('display', 'none');
                });
                $('#opener').fadeTo(350, 1, function () {
                    $('#opener').css('display', 'block');
                });
                $(this).css('display', 'none');
                clicked = false;
            }
        });

    $("#ps-identity").hover(
        function (event) {
            if ($(event.target).is('section')) {
                $(this).css("background-color", "#4fc3f7");
            }
        },
        function (event) {
            if ($(event.target).is('section')) {
                $(this).css("background-color", "#29b6f6");
            }
        });
});