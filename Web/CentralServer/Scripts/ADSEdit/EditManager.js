﻿$(function () {
    $('#contract').hide();
    
    var contractNb = 0;

    if (contracts.length == 0) {
        $('.contract-add-btn').attr('disabled', 'disabled');
    }

    $('.contract-add-btn').click(
        function () {
            $('#contract').clone().appendTo('#contracts').attr('id', 'contract' + contractNb);
            $('#contract' + contractNb).show();
            $('#contract' + contractNb).removeClass('contract');
            $('#contract' + contractNb).addClass('added-contract');
            $('#contract' + contractNb).find('.contract-rem-btn').bind('click', function () {
                var id = this.parentElement.id;
                contractNb--;
                $('#contracts').find('#' + id).remove();
            });

            var optDef = '<option value="';
            var optEnd = '</option>';

            if (contracts.length > 0) {
                for (var i = 0; i < contracts.length; i++) {
                    $('#contract' + contractNb).find('.contract-select').append(optDef + contracts[i] + '">' + contracts[i] + optEnd);
                }
            }
            else {
                $('#contract' + contractNb).find('.contract-select').append(optDef + 'null">No contract' + optEnd);
            }
            contractNb++;
        }
    );

    $('#save').click(
        function () {
            editADS();
        }
    );
});

async function editADS() {
    var ads = {
        'ISName': $('#ISName').val(),
        'Url': $('#Url').val(),
        'Root': $('#Root').val()
    };

    ads.ContractNames = [];
    for (var i = 0; i < $('#contracts').children().length; i++) {
        ads.ContractNames[i] = {
            'Id' : null,
            'Name' : $('#' + $('#contracts').children()[i].id).find('#contractName').find(':selected').val()
        };
    }

    await $.ajax({
        type: "POST",
        url: '/ADS/Save',
        data: JSON.stringify(ads),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            //alert(result);
        },
        error: function (result) {
            if (result.status != 200)
                alert('AJAX Error');
        }
    });
}