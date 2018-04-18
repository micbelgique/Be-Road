class AjaxCalls {
    async outputGet(lookUpId) {
        var params = {
            'lookUpId': lookUpId,
            'contractId': 'GetAddressByOwnerId'
        };
        return await $.ajax({
            type: "POST",
            url: 'Contract/GetOutput',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(params),
            dataType: "json",
            success: function (result) {
                //alert(result.Key);
            },
            error: function () {
                alert('AJAX Error');
            }
        });
    }

    async contractsGet() {
        $('.lookupkey-select').append('<option selected="selected">Loading...</option>');
        return await $.ajax({
            type: "GET",
            url: 'Contract/GetContracts',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //alert(this.contracts + '\n' + result);
            },
            error: function () {
                alert('AJAX Error');
            }
        });
    }

    async createContract() {
        var contract = {
            'Id' : $('#Id').val(),
            'Description' : $('.contract-descr').find('#Description').val(),
            'Version' : $('#Version').val()
        };

        contract.Inputs = [];
        for (var i = 0; i < $('#inputs').children().length; i++) {
            contract.Inputs[i] = {
                'Key' : $('#' + $('#inputs').children()[i].id).find('#Key').val(),
                'Type' : $('#' + $('#inputs').children()[i].id).find('#Type').find(':selected').val(),
                'Required' : $('#' + $('#inputs').children()[i].id).find('#Required').val(),
                'Description' : $('#' + $('#inputs').children()[i].id).find('#Description').val()
            }
        }

        contract.Queries = [];
        for (var i = 0; i < $('#queries').children().length; i++) {
            contract.Queries[i] = {
                'Contract' : $('#' + $('#queries').children()[i].id).find('#Id').find(':selected').val()
            }
            contract.Queries[i].Mappings = [];
            for (var j = 0; j < $('#' + $('#queries').children()[i].id).find('#mappings').children().length; j++) {
                contract.Queries[i].Mappings[j] = {
                    'InputKey': $('#' + $('#queries').children()[i].id + ' #mapping' + (j + 1) + $('#queries').children()[i].id).find('#InputKey').find(':selected').val(),
                    'LookupInputId': $('#' + $('#queries').children()[i].id + ' #mapping' + (j + 1) + $('#queries').children()[i].id).find('#LookupInputId').find(':selected').val(),
                    'LookupInputKey': $('#' + $('#queries').children()[i].id + ' #mapping' + (j + 1) + $('#queries').children()[i].id).find('#LookupInputKey').find(':selected').val()
                }
            }
        }

        contract.Outputs = [];
        for (var i = 0; i < $('#outputs').children().length; i++) {
            contract.Outputs[i] = {
                'LookupInputId' : ($('#' + $('#outputs').children()[i].id).find('#LookupInputId').find(':selected').val() - 1),
                'Type' : $('#' + $('#outputs').children()[i].id).find('#Type').find(':selected').val(),
                'Description': $('#' + $('#outputs').children()[i].id).find('#Description').val(),
                'Key': $('#' + $('#outputs').children()[i].id).find('#LookupInputId').find(':selected').val() == 0 ? $('#' + $('#outputs').children()[i].id).find('#Key').val() : $('#' + $('#outputs').children()[i].id).find('#Key').find(':selected').val()
            }
        }

        return await $.ajax({
            type: "POST",
            url: 'Contract/CreateAsync',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(contract),
            dataType: "json",
            success: function (result) {
                //alert(result);
            },
            error: function () {
                alert('AJAX Error');
            }
        });
    }

    returnToList() {
        $.ajax({
            type: "GET",
            url: 'Contract/ReturnToList',
            success: function () {
                //alert(result);
            },
            error: function () {
                alert('AJAX Error');
            }
        });
    }
}