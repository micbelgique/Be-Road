class UpdateSelect {
    constructor() {
        this.calls = new AjaxCalls();
        this.contracts = null;
        this.queryCpt = null;
    }

    updateSelects(cpt) {
        this.updateLookupIdSelect(cpt);
        this.updateKeySelect(cpt);
        this.updateLookupKeySelect(cpt);
    }

    refresh(cpt) {
        for (var i = 1; i <= cpt + 1; i++) {
            $('#query' + i).find('.lookupid-select').find('option').remove();
        }

        for (var i = 0; i <= cpt; i++) {
            this.updateLookupIdSelect(i);
        }
        
        this.updateLookupKeySelect(cpt);
    }

    updateLookupIdSelect(cpt) {
        var optSel = '<option selected="selected" value="0">';
        var optDef = '<option value="' + cpt + '">';
        var optEnd = '</option>';
        
        for (var i = 1; i <= cpt + 1; i++) {
            if (i > 1) {
                $('#query' + (cpt + 1)).find('.lookupid-select').append(optDef + "Query #" + (i - 1) + optEnd);
            }
            else {
                $('#query' + (cpt + 1)).find('.lookupid-select').append(optSel + 'Main contract' + optEnd);
            }
        }
    }

    updateLookupKeySelect(cpt) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var inputs;
        var inner;

        $('.lookupkey-select').find('option').remove();
        for (var i = 1; i <= cpt + 1; i++) {
            inputs = $('.added-input');
            if (inputs.length == 0) {
                $('#query' + i).find('.lookupkey-select').append('<option selected="selected">No input</option>');
            }
            else {
                for (var j = 0; j <= inputs.length; j++) {
                    if (inputs[j] != null) {
                        inner = inputs[j].children[1].children[1].children[0].value;
                        if (inner != '') {
                            $('#query' + i).find('.lookupkey-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
    }

    updateOneLookupKeySelect(mappingId, qId) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var inputs;
        var selectedContract;
        var inner;

        $(mappingId).find('.lookupkey-select').find('option').remove();
        var lookupQueryId = $(mappingId).find('.lookupid-select').find(':selected').val();
        if (lookupQueryId == '0') {
            inputs = $('.added-input');
            if (inputs.length == 0) {
                $(mappingId).find('.lookupkey-select').append('<option selected="selected">No input</option>');
            }
            else {
                for (var j = 0; j <= inputs.length; j++) {
                    if (inputs[j] != null) {
                        inner = inputs[j].children[1].children[1].children[0].value;
                        if (inner != '') {
                            $(mappingId).find('.lookupkey-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
        else {
            selectedContract = this.contracts.find(c => c.Id == $('#query' + lookupQueryId + ' .qcontractname-select').find(':selected').val());
            if (selectedContract.Outputs.length == 0) {
                $(mappingId).find('.lookupkey-select').append('<option selected="selected">No output</option>');
            }
            else {
                for (var j = 0; j <= selectedContract.Outputs.length; j++) {
                    if (selectedContract.Outputs[j] != null) {
                        inner = selectedContract.Outputs[j].Key;
                        if (inner != '') {
                            $(mappingId).find('.lookupkey-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
    }

    updateKeySelect(cpt) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var selectedContract;
        var queryId;

        $('.key-select ').find('option').remove();
        for (var i = 1; i <= cpt + 1; i++) {
            selectedContract = this.contracts.find(c => c.Id == $('#query' + i + ' .qcontractname-select').find(':selected').text());
            if (selectedContract.Inputs.length == 0) {
                $('#query' + i).find('.key-select ').append('<option selected="selected">None</option>');
            }
            else {
                for (var j = 0; j < selectedContract.Inputs.length; j++) {
                    $('#query' + i).find('.key-select ').append(optDef + selectedContract.Inputs[j].Key + '">' + selectedContract.Inputs[j].Key + optEnd);
                }
            }
        }
    }

    fillQContractNameSelect(cpt, i) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        
        this.calls.contractsGet().then(function (result) {
            this.contracts = result;
            $('#query' + i).find('.qcontractname-select').find('option').remove();
            for (var j = 0; j < result.length; j++) {
                $('#query' + i).find('.qcontractname-select').append(optDef + result[j].Id + '">' + result[j].Id + optEnd);
            }
            this.updateLookupKeySelect(cpt);
        }.bind(this));
    }

    updateOLookupIdSelect(outputId, cpt) {
        var optSel = '<option selected="selected" value="0">';
        var optDef;
        var optEnd = '</option>';

        for (var i = 0; i <= cpt + 1; i++) {
            if (i > 0) {
                optDef = '<option value="' + i + '">';
                $('#output' + outputId).find('.olookupid-select').append(optDef + "Query #" + i + optEnd);
            }
            else {
                $('#output' + outputId).find('.olookupid-select').append(optSel + 'Main contract' + optEnd);
            }
        }
    }

    updateOutputKeySelect(outputId) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var inputs;
        var selectedContract;
        var inner;

        $(outputId).find('.outputkey-select').find('option').remove();
        var lookupQueryId = $(outputId).find('.olookupid-select').find(':selected').val();
        if (lookupQueryId == '0') {
            inputs = $('.added-input');
            if (inputs.length == 0) {
                $(outputId).find('.outputkey-select').append('<option selected="selected">No input</option>');
            }
            else {
                for (var j = 0; j <= inputs.length; j++) {
                    if (inputs[j] != null) {
                        inner = inputs[j].children[1].children[1].children[0].value;
                        if (inner != '') {
                            $(outputId).find('.outputkey-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
        else {
            selectedContract = this.contracts.find(c => c.Id == $('#query' + lookupQueryId + ' .qcontractname-select').find(':selected').val());
            if (selectedContract.Outputs.length == 0) {
                $(outputId).find('.outputkey-select').append('<option selected="selected">No output</option>');
            }
            else {
                for (var j = 0; j <= selectedContract.Outputs.length; j++) {
                    if (selectedContract.Outputs[j] != null) {
                        inner = selectedContract.Outputs[j].Key;
                        if (inner != '') {
                            $(outputId).find('.outputkey-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
    }
}