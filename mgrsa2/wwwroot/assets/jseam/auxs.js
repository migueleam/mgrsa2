//
// Auxiliares: alertas noticias
//
//<!====================    mensaje =============================!>


"use strict";

var AuxG = {

    settingsNA: {
        index: 0,
        id: "tableX",
        container: "divTable",
        tbRows: "tbRows",
        showHeader: true,
        config: {
            "order": [5, "desc"],
            "pagingType": ["full_numbers"],
            "pageLength": 12,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            //responsive: true,
            "select": true,
            "scrollCollapse": true,
            "paging": true,
            "fixedColumns": false,
            "bFilter": true,
            "bInfo": true
        }
    },

    GetNewAlert: function (naid) {

        http("/ord/GetNewAlert"
            ,'POST'
            ,{
                "id": naid
            }
            , true
            , 'AuxG.PopulateNewAlertH'
        );
    },

    PopulateNewAlertH: function (data) {
        $('#naActual').html(data.memo); //aResult[2]
    },       

    PopulateNewAlert: function (data) {

        $('#id').val(data.id);
        $('#xtitulo').val(data.title);
        $('#xfromdate').val(data.fromdate);
        $('#xtodate').val(data.todate);
        $('#xorden').val(data.orden);
        if (data.publish)
            $('#xpublish').prop("checked", true);
        else
            $('#xpublish').prop("checked", false);
        //? document.getElementById("xpublish").checked = true : document.getElementById("xpublish").checked = false;          


        $('#xmemo').summernote('code', data.memo);

        //CKEDITOR.instances['xmemo'].setData(aData[2]);
        //var vmemo = CKEDITOR.instances.xmemo.getData();
        //$('#xmemo').html(aData[2]);
    },

    UpdateNewAlerts: function (xtipo) {

        var vid = $('#id').val();
        var vtitulo = $('#xtitulo').val();
        var vfromdate = $('#xfromdate').val();
        var vtodate = $('#xtodate').val();
        var vorden = $('#xorden').val();
        var vpublish = $('#xpublish').prop('checked');
        //var vmemo = CKEDITOR.instances.xmemo.getData();
        //vmemo = vmemo.replace('"', "''");
        var vmemo = $('#xmemo').summernote('code');

        //crea Area inicial...for Schedules
        var url = "/admin/UpdateNewAlerts";
        
        http(
            url
            , 'POST'
            , {
                "id": vid,
                "titulo": vtitulo,
                "fromdate": vfromdate,
                "todate": vtodate,
                "orden": vorden,
                "publish": vpublish,
                "memo": vmemo,
                "tipo": xtipo
            }
            , true
            , 'AuxG.OpenPage'
            , 'P'
            , {xtipo}
        );               

    },
    
    OpenPage: function (vtipo) {
        if (vtipo === 'N') {
            window.location.href = '/ord/news/';
        }
        else {
            window.location.href = '/ord/alerts/';
        }
    },

};