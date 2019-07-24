"use strict";

var LocsG = {

    settingsTable: {
        id: "tbLocs",
        container: "divLocs",
        config: {
            "order": [[3, "asc"], [2, "asc"]],
            "pagingType": ["full_numbers"],
            "pageLength": 10,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "scrollCollapse": true,
            "paging": true,
            "fixedColumns": false,
            "bFilter": true,
            "bInfo": true //,
            //"columnDefs": [
            //    //{
            //    //    "targets": [0],
            //    //    "visible": false
            //    //},
            //    {
            //        "className": "dt-center",
            //        "targets": "_all"
            //    }
            //]
        }
    },

    GetLoc: function (locid) {

        http('/admin/GetLoc'
            , 'POST'
            , { "locid": locid }
            , true
            , 'LocsG.PopulateLoc'
            , 'R'
        );
        
        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/admin/GetLoc',
        //    data: {
        //        "locid": locid
        //    },
        //    success: function (result) {
        //        //CommonG.Message(CommonG.GetString(result, 0));
        //        CommonG.Loading();
        //        var pop = LocsG.PopulateLoc(result);
        //        $('#divLoading').hide();
        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString('er| Punto de Acceso NO encontrado| Puntos de Acceso Mgr', 0));
        //    }
        //});

    },

    PopulateLoc: function (data) {

        //var aData = data.split('~');
        const itm = data.split('|');

        this.PrepareLoc();

        $('#Id').val(itm[0]);
        $('#Description').val(itm[2]);  
        $('#IP').val(itm[1]);
           

        if (itm[3] === 'True') {
            $('#Active').prop('checked', true);           
        } else {
            $('#Active').prop('checked', false);        
        }   

    },


    PrepareLoc: function (id) {
        
        $('#Id').val('');
        $('#Description').val('');     
        $('#IP').val('');
       
        $('#Active').prop('checked', true);

        if (id !== undefined && id !== ""){
            $('#Id').val(id);
        }

    },


    PopulateResults: function (data) {

        const rdata = data.split("^");
        const amsg = rdata[0].split("|");
        const amodel = rdata[1].split("|");

        if (amsg[0].toLowerCase() !== "er") {

            $('#Id').val(amodel[0]);
            $('#Description').val(amodel[2]);    
            $('#IP').val(amodel[1]);
          
            
            if (amodel[3] === 'True') {                         
                $('#Active' + amodel[0]).prop('checked', true);           
            } else {                
                $('#Active' + amodel[0]).prop('checked', false);                   
            }   

            if (amsg[3] === 'u') {
                //update              
                      
                CommonG.UpdateRowTable(LocsG.settingsTable.id, amodel[0], 0, amodel[1], 2); //pos 2 ip
                CommonG.UpdateRowTable(LocsG.settingsTable.id, amodel[0], 0, amodel[2], 1); //pos 1 localizacion        

                if (amodel[3] === "True") {
                    $('#active' + amodel[0]).prop('checked', true);
                }
                else {
                    $('#active' + amodel[0]).prop('checked', false);
                }  
                
            } else {
                //insert
                const t = $("#" + LocsG.settingsTable.id).DataTable();  
                const bActive = (amodel[3] === 'True'? "checked" : "");
                              
                t.row.add([
                    amodel[0],
                    amodel[2],
                    amodel[1],
                    `<label class="custom-control custom-checkbox d-block">
                        <input type="checkbox" class="custom-control-input" ${bActive} disabled id='active${amodel[0]}' />
                        <span class="custom-control-label"></span>
                    </label>
                    <span class="d-none">${amodel[3]}</span>`
                ]).draw(false);

                if (amodel[3] === "True") {                   
                    $('#active' + amodel[0]).prop('checked', true);
                }
                else {
                    $('#active' + amodel[0]).prop('checked', false);
                }  
            }

        }
        //que haya un refresh de table al cerrar
        CommonG.Message(rdata[0]);

    },

    bindUIActions: function () {

        //triggered when modal is about to be shown
        $('#modalLoc').on('show.bs.modal', function (e) {

            const locid = $(e.relatedTarget).data('locid');
            
            if (locid !== 0) {                
                let shw = LocsG.GetLoc(locid);
            } else {
                $("#disTitle").html('Nuevo Punto de Acceso');                
                let px = LocsG.PrepareLoc(0);
            }
                        
        });

        $('#modalLoc').on('hidden.bs.modal', function () {
                      
            const vsearch = $('.dataTables_filter input').val();


            http('/admin/locs'
                , 'POST'
                , { vsearch: vsearch }
                , true               
            );

            //cambiar por solo                      

            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/locs',
            //    data: {
            //        vsearch: vsearch
            //    },
            //    success: function (result) {
            //        var upx = CommonG.Message('ok|Info Actualizada|Puntos de Acceso Mgr');
            //    },
            //    fail: function (result) {
            //        var upx = CommonG.Message('er|Error en Actualización|Puntos de Acceso Mgr');
            //    }

            //});

        });


        $("#btnUpdate").on('click', function (event) {

            event.preventDefault();

            const busq = $('.dataTables_filter input').val();
            //var x = $("#Active").is(":checked");

            var datav = {
                "Id": $('#Id').val(),
                "IP": $('#IP').val(),                        
                "Description": $('#Description').val(),
                "Active": $("#Active").is(":checked")          
            };

            if (datav.IP === "" || datav.Description === "") {
                let px = CommonG.Message('wn|Verifique Datos de Puntos de acceso | Puntos de acceso Mgr');
                return;
            }
            
            http('/admin/UpdateLoc'
                , 'POST'
                , datav
                , true
                , 'LocsG.PopulateResults'
                , 'R'
            );

            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/UpdateLoc',
            //    data: datav,
            //    success: function (result) {
            //        var upr = LocsG.PopulateResults(result);
            //    },
            //    fail: function (result) {
            //        var upx = CommonG.Message('er|Error en Actualización|Puntos de acceso Mgr');
            //    }
            //});

        });


        //$('#IP').mask('999.999.999.999');

        $('#' + this.settingsTable.id).on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });     

    }

};