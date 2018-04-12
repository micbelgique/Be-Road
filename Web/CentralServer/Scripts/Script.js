$(function () {
    var inputNb = 0;
    $('.input-add-btn').click(
        function () {
            inputNb++;
            $('#input').clone().appendTo('#inputs').attr('id', 'input' + inputNb);
            $('#input' + inputNb).show();
            $('#input' + inputNb).find('.input-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                $('#inputs').find('#' + id).remove();
            });
        }
    );

    var queryNb = 0;
    var cpt = -1;
    $('.query-add-btn').click(
        function () {
            queryNb++;
            $('#query').clone().appendTo('#queries').attr('id', 'query' + queryNb);
            $('#query' + queryNb).show();
            $('#query' + queryNb).find('.query-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                cpt--;
                updateLookupIdSelect(cpt);
                updateLookupKeySelect(cpt)
                $('#queries').find('#' + id).remove();
            });

            var mappingNb = 0;
            cpt++;
            $('#query' + queryNb).find('.mapping-add-btn').bind('click',
                function () {
                    var qId = this.parentElement.parentElement.parentElement.parentElement.id;
                    mappingNb++;
                    $('#mapping').clone().appendTo('#' + qId + ' #mappings').attr('id', 'mapping' + mappingNb + qId);
                    $('#mapping' + mappingNb + qId).show();
                    $('#mapping' + mappingNb + qId).find('.mapping-rem-btn').bind('click', function () {
                        var id = this.parentElement.parentElement.id;
                        $('#' + qId + ' #mappings').find('#' + id).remove();
                    });
                }
            );
            updateLookupIdSelect(cpt);
            updateLookupKeySelect(cpt)
        }
    );
});

function updateLookupIdSelect(cpt) {
    var optSel = '<option selected="selected" value="0">';
    var optDef = '<option value="'+cpt+'">';
    var optEnd = '</option>';

    $('.lookupid-select').find('option').remove();
    for (var i = 0; i <= cpt; i++) {
        if (i > 0) {
            $('.lookupid-select').append(optDef + "Query #"+ i + optEnd);
        }
        else {
            $('.lookupid-select').append(optSel + 'Main contract' + optEnd);
        }
    }
}

function updateLookupKeySelect(cpt) {
    var optSel = '<option selected="selected" value="0">';
    var optDef = '<option value="' + cpt + '">';
    var optEnd = '</option>';

    $('.lookupkey-select').find('option').remove();
    for (var i = 0; i <= cpt; i++) {
        outputGet(i).then(function (result) {
            for (var j = 0; j <= result.length; j++) {
                $('.lookupkey-select').append(optDef + result[j].Key + optEnd);
            }
        });
    }
}

async function outputGet(lookUpId) {
    var params = {
        'lookUpId': lookUpId,
        'contractId': 'GetAddressByOwnerId'
    };
    return await $.ajax({
        type: "POST",
        url: 'GetOutput',
        contentType: "application/json; charset=utf-8",
        data:  JSON.stringify(params),
        dataType: "json",
        success: function (result) {
            //alert(result.Key);
        },
        error: function () {
            alert('AJAX Error');
        }
    });
}