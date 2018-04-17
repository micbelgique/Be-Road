$(function () {
    var inputNb = 0;
    var outputNb = 0;
    var us = new UpdateSelect();
    us.queryCpt = -1;
    $('.query-rem-btn').hide();

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
            $('.query-rem-btn').show();
            $('#queries').css('margin-top', '50px');

            var mappingNb = 0;
            us.queryCpt++;

            $('#query' + queryNb).find('.qcontractname-select').bind('change',
                function () {
                    us.updateSelects(us.queryCpt);
                }
            );

            $('#query' + queryNb).find('.mapping-add-btn').bind('click',
                function () {
                    var qId = this.parentElement.parentElement.parentElement.parentElement.id;
                    mappingNb++;
                    $('#mapping').clone().appendTo('#' + qId + ' #mappings').attr('id', 'mapping' + mappingNb + qId);
                    $('#mapping' + mappingNb + qId).show();

                    $('#mapping' + mappingNb + qId).find('.lookupid-select').bind('change',
                        function () {
                            us.updateOneKeySelect('#mapping' + mappingNb + qId, qId);
                        }
                    );
                    
                    $('#query' + queryNb).find('.lookupid-select').find('option').remove();

                    us.updateSelects(us.queryCpt);
                }
            );
            us.fillQContractNameSelect(us.queryCpt, queryNb);
        }
    );

    $('.query-rem-btn').click(
        function () {
            $('#queries').css('margin-top', '0px');
            $('.query-rem-btn').hide();
            us.queryCpt = -1;
            queryNb = 0;
            mappingNb = 0;
            $('#queries').find('.query').remove();
        }
    );

    $('.output-add-btn').click(
        function () {
            outputNb++;
            $('#output').clone().appendTo('#outputs').attr('id', 'output' + outputNb);
            $('#output' + outputNb).show();
            $('#output' + outputNb).removeClass('output');
            $('#output' + outputNb).addClass('added-output');
            $('#output' + outputNb).find('.output-rem-btn').bind('click', function () {
                var id = this.parentElement.parentElement.id;
                $('#outputs').find('#' + id).remove();
            });

            us.updateOLookupIdSelect(outputNb, us.queryCpt);
        }
    );
});

