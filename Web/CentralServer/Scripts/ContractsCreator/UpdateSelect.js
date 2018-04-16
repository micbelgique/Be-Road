﻿class UpdateSelect {
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

    updateKeySelect(cpt) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var inputs;
        var inner;

        $('.key-select').find('option').remove();
        for (var i = 1; i <= cpt + 1; i++) {
            inputs = $('.added-input');
            if (inputs.length == 0) {
                $('#query' + i).find('.key-select').append('<option selected="selected">No input</option>');
            }
            else {
                for (var j = 0; j <= inputs.length; j++) {
                    if (inputs[j] != null) {
                        inner = inputs[j].children[1].children[1].children[0].value;
                        if (inner != '') {
                            $('#query' + i).find('.key-select').append(optDef + inner + '">' + inner + optEnd);
                        }
                    }
                }
            }
        }
    }

    updateLookupKeySelect(cpt) {
        var optDef = '<option value="';
        var optEnd = '</option>';
        var selectedContract;
        var queryId;

        $('.lookupkey-select ').find('option').remove();
        for (var i = 1; i <= cpt + 1; i++) {
            //queryId = $('#mapping' + i +'query' + i + ' .lookupid-select').find(':selected').value;
            //alert(queryId);
            selectedContract = this.contracts.find(c => c.Id == $('#query' + i + ' .qcontractname-select').find(':selected').text());
            if (selectedContract.Inputs.length == 0) {
                $('#query' + i).find('.lookupkey-select ').append('<option selected="selected">None</option>');
            }
            else {
                for (var j = 0; j < selectedContract.Inputs.length; j++) {
                    $('#query' + i).find('.lookupkey-select ').append(optDef + selectedContract.Inputs[j].Key + '">' + selectedContract.Inputs[j].Key + optEnd);
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
}