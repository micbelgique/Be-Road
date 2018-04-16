$(function () {
    var inputNb = 0;
    var us = new UpdateSelect();
    us.queryCpt = -1;

    $('.input-add-btn').click(
        function () {
            inputNb++;
            $('#input').clone().appendTo('#inputs').attr('id', 'input' + inputNb);
            $('#input' + inputNb).show();
            $('#input' + inputNb).removeClass('input');
            $('#input' + inputNb).addClass('added-input');
            us.updateSelects(us.queryCpt);
            $('#input' + inputNb).find('.input-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                $('#inputs').find('#' + id).remove();
                us.refresh(us.queryCpt);
            });

            $('#input' + inputNb).find('.input-key').bind('keyup', 
                function () {
                    us.updateSelects(us.queryCpt);
                }
            );
        }
    );

    var queryNb = 0;
    $('.query-add-btn').click(
        function () {
            queryNb++;
            $('#query').clone().appendTo('#queries').attr('id', 'query' + queryNb);
            $('#query' + queryNb).show();
            $('#query' + queryNb).find('.query-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                us.queryCpt--;
                us.refresh(us.queryCpt);
                $('#queries').find('#' + id).remove();
            });

            var mappingNb = 0;
            us.queryCpt++;

            $('#query' + queryNb).find('.qcontractname-select').bind('change',
                function () {
                    us.updateSelects(cpt);
                }
            );

            $('#query' + queryNb).find('.mapping-add-btn').bind('click',
                function () {
                    var qId = this.parentElement.parentElement.parentElement.parentElement.id;
                    mappingNb++;
                    $('#mapping').clone().appendTo('#' + qId + ' #mappings').attr('id', 'mapping' + mappingNb + qId);
                    $('#mapping' + mappingNb + qId).show();
                    $('#mapping' + mappingNb + qId).find('.mapping-rem-btn').bind('click', function () {
                        var id = this.parentElement.parentElement.id;
                        us.refresh(us.queryCpt);
                        $('#' + qId + ' #mappings').find('#' + id).remove();
                    });

                    us.updateSelects(us.queryCpt);
                }
            );
            us.fillQContractNameSelect(us.queryCpt, queryNb);
        }
    );
});

