"use strict";

var EdsG = {

    settingsTable: {
        id: "tbEds",
        container: "divEds",
        config: {
            "order": [0, "desc"],
            "pagingType": ["full_numbers"],
            "pageLength": 10,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "paging": true,
            "bFilter": true,
            "bInfo": true,                       
            //"language": {
            //    "paginate": {
            //        "previous": '<i class="demo-psi-arrow-left"></i>',
            //        "next": '<i class="demo-psi-arrow-right"></i>'
            //    }
            //},
            "createdRow": function (row, data, dataIndex) {
                if (data[7] === "True") {
                    $(row).addClass("text-danger");
                }
            },
            "columnDefs": [
                {
                    "targets": [7],
                    "visible": false
                },
                {
                    "className": "dt-center",
                    "targets": "_all"
                }
            ]
        }
    },

    GetEdicion: function (edicionid) {


        http('/admin/GetEdicion'
            , 'POST'
            , {
                "edicionid": edicionid
            }
            , true
            , 'EdsG.PopulateEdicion'); 

        //$.ajax({
        //    type: 'POST',
        //    url: '/admin/GetEdicion',
        //    data: {
        //        "edicionid": edicionid
        //    },
        //    success: function (result) {
        //        //common.Message(common.GetString(result, 0));
        //        var pop = EdsG.PopulateEdicion(result);
        //        CommonG.Loading();
        //    },
        //    fail: function (result) {
        //        common.Message(common.GetString('er| Edicion NO encontrada| Edicion Mgr', 0));
        //    }
        //});

    },

    PopulateEdicion: function (data) {

      
        if (data === undefined  || data.substring(0, 1) === '0') {
            return;
        }

        const itm = data.split('|');

        $('#EdicionId').val(itm[0]);
        $('#Edicion').val(itm[1]);

        //var currentDate = new Date();
        //var dfecha = new Date(itm[2]);              

        $('#Fecha').datepicker('setDate', itm[2]);      
        $('#FechaAviso').datepicker('setDate',itm[3]);       
        //$('#HoraAviso').timepicker('setTime', itm[4]);  
        $('#HoraAviso').val(itm[4]);
        $('#FechaCierre').datepicker('setDate', itm[5]);                
        //$('#HoraCierre').timepicker('setTime', itm[6]);
        $('#HoraCierre').val(itm[6]);
    },

    PrepareEdicion: function (id) {

        $('#EdicionId').val('');
        $('#Edicion').val('');
        $('#Fecha').val('');
        $('#FechaAviso').val('');
        $('#HoraAviso').val('');
        $('#FechaCierre').val('');
        $('#HoraCierre').val('');      
        
        if (id !== undefined && id !== "") {
            $('#EdicionId').val(id);
        }

    },

    PopulateResults: function (data) {

        const rdata = data.split("^");
        const amsg = rdata[0].split("|");
        const amodel = rdata[1].split("|");

        if (amsg[0].toLowerCase() !== "er") {

            //$('#Id').val(amodel[0]);
            //$('#Description').val(amodel[1]);
            //$('#IP').val(amodel[2]);


            //if (amodel[3] === 'True') {
            //    $('#Active').prop('checked', true);
            //} else {
            //    $('#Active').prop('checked', false);
            //}

            if (amsg[3] === 'u') {
                //update
                //CommonG.UpdateRowTable(EdsG.settingsTable.id, amodel[0], 0, amodel[2], 1);
                //CommonG.UpdateRowTable(EdsG.settingsTable.id, amodel[0], 0, amodel[1], 2);


            } else {
                //insert
                const t = $("#" + EdsG.settingsTable.id).DataTable();

                //t.row.add([
                //    amodel[0],
                //    amodel[2],
                //    amodel[1],
                //    '<input type="checkbox" disabled id= "active' + amodel[0] + '" />' +
                //    '<span class="hidden">' + amodel[3] + '</span>',
                //    '<a class="btn btn-xs btn-primary" data-toggle="modal" href="#modalLoc" data-locid="' + amodel[0] + '"><i class="fa fa-edit"></i> Edit</a>'
                //]).draw(false);

            }

        }
        //que haya un refresh de table al cerrar
        CommonG.Message(rdata[0]);

    },

    UpdateRow : function(datav) {      
        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.FechaAviso, 3);
        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.HoraAviso, 4);
        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.FechaCierre, 5);
        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.HoraCierre, 6);
    }, 

    bindUIActions: function () {

        $('#' + this.settingsTable.id).on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });            

        //triggered when modal is about to be shown
        $('#modalEdi').on('show.bs.modal', function (e) {

            event.preventDefault();

            const edicionid = $(e.relatedTarget).data('edicionid');

            if (edicionid !== 0) {
                let shw = EdsG.GetEdicion(edicionid);
            } else {
                $("#disTitle").html('Nuevo Edicion');
                let px = EdsG.PrepareEdicion(0);
            }

            $('.input-group.date').datepicker({
                format: "dd/mm/yyyy",               
                todayBtn: "linked",
                autoclose: true,
                todayHighlight: true
            });


        });

        $('#modalEdi').on('hidden.bs.modal', function () {

            const vsearch = $('.dataTables_filter input').val();

            //cambiar por solo                      
            http('/admin/ediciones'
                , 'POST'
                , {
                    vsearch: vsearch
                }
                , true
               ); 


            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/ediciones',
            //    data: {
            //        vsearch: vsearch
            //    },
            //    success: function (result) {
            //        var upx = CommonG.Message('ok|Info Actualizada|Edicion Mgr|1000');                   
            //    },
            //    fail: function (result) {
            //        var upx = CommonG.Message('er|Error en Actualización|Edicion Mgr|1000');
            //    }

            //});

        });


        $("#btnUpdate").on('click', function (event) {

            event.preventDefault();

            const datav = {
                "EdicionId": $('#EdicionId').val(),
                "Edicion": $('#Edicion').val(),
                "FechaAviso": $('#FechaAviso').val(),
                "HoraAviso": $('#HoraAviso').val(),
                "FechaCierre": $('#FechaCierre').val(),
                "HoraCierre": $('#HoraCierre').val()
                //,"Active": $('#Active:checked').val() ? true : false
            };
            
            if (datav.EdicionId === "" || datav.FechaAviso === "" || datav.HoraAviso === ""
                || datav.FechaCierre === "" || datav.HoraCierre === "") {
                let px = CommonG.Message('wn|Verifique Datos de la Edicion | Edicion Mgr');
                return;
            }

            http('/admin/UpdateEdicion'
                , 'POST'
                , datav
                , true
                , 'EdsG.UpdateRow'
                , 'P'
                , datav
            ); 


            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/UpdateEdicion',
            //    data: datav,
            //    success: function (result, datav) {
            //        var up = CommonG.Message('ok|Edicion Actualizada|Edicion Mgr|1000');
            //        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.FechaAviso, 3);
            //        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.HoraAviso, 4);
            //        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.FechaCierre, 5);
            //        CommonG.UpdateRowTable(EdsG.settingsTable.id, datav.EdicionId, 0, datav.HoraCierre, 6);
            //    },
            //    fail: function (result) {
            //        var upx = CommonG.Message('er|Error en Actualización|Edicion Mgr|1000');
            //    }
            //});

        });


        //here 
        //no compatible lunes 071519              


        //https ://jdewit.github.io/bootstrap-timepicker/

        //$('.tiempo').timepicker({
        //    template: 'modalEdi'
        //});

     
      
    }

};