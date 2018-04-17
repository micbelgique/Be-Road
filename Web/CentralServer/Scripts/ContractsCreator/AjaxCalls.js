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
}