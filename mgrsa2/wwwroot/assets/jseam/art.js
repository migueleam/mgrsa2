"use strict";

var ArtG = {

    settingsTable: {
        id: "tbArts",
        container: "divArts",
        config: {
            "order": [
                [2,"asc"],
                [4,"asc"],
                [6,"asc"]
            ],
            "pagingType": ["full_numbers"],
            "pageLength": 12,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "paging": true,
            "bFilter": true
            ,
            "bInfo": true
            , "columnDefs": [
                //{
                //    "targets": [0],
                //    "visible": false,
                //    "searchable": false
                //},
                {
                    "targets": [2],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [4],
                    "visible": false,
                    "searchable": false
                },
                {
                    "targets": [6],
                    "visible": false,
                    "searchable": false
                }
            ]
        }
    },

    PrepareArt: function () {


        $('#divSeccion').html('');
        $('#divSscTipo').html('');

        CommonG.HttpSelect2('/admin/GetSecciones', false, 'Secciones', 'Secciones', 'divSeccion', 'Seccion', 'seccionId', '', '', 'modalItm', false, true, 'let px = ArtG.GetSubSeccionSel(this);'); //, '', '', '', '', '', '', false, '1');                             
        CommonG.HttpSelect2('/admin/GetSscTipos', false, 'Tipo SubSecc', 'TipoSubSec', 'divSscTipo', 'Tipo Sub-Seccion', 'sscTipoId', '', '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');

        $('#seccionId').val('');
        $('#seccionId').trigger('change');
        $('#sscTipoId').val('');
        $('#sscTipoId').trigger('change');
        
        $('#edicionIngreso').val($('#edActual').val());
                
        $('#revisado').prop('checked', false);
        $('#publicar').prop('checked', false);

        $('#seccionId').val('0');
        $('#seccionId').trigger('change');
        $('#subSeccionId').val('0');
        $('#subSeccionId').trigger('change');
        $('#sscTipoId').val('0');
        $('#sscTipoId').trigger('change');


        $('#titulo').val('');
        $('#subTitulo').val('');
        $('#contenido').val('');
        $('#relevancia').val('A');
        $('#relevancia').trigger('change');
        $('#notas').val('');
        $('#palabrasClaves').val('');
    },


    GetEdActual: function () {

        http('/common/GetEdActual'
            , 'POST'
            , null
            , true
            , 'ArtG.PopulateEdActual'
            , 'R'
        );

    },


    PopulateEdActual: function (itm) {

        $('#edActual').val(itm.edicion);

    },

    GetItm: function (id) {
        
        http('/art/GetArticulo'
            , 'POST'
            , {
                "articuloId": id //notaFUId
            }
            , true           
            , 'ArtG.PopulateArt'
            , 'R'
        );

    },

    GetSubSeccionSSel: function (obj) {
        $('#divSubSeccionS').html('');
        let vSeccionId = $(obj).val();
        let vsscTipoId = $('#sscTipoId').val();
        CommonG.HttpSelect2('/admin/GetSubSecciones', false, 'Sub-Secciones', 'Sub-Secciones', 'divSubSeccionS', 'Sub-Seccion', 'subSeccionIdS', { "seccionId": vSeccionId, "sscTipoS": vsscTipoId }, '', '', false, true); //, '', '', '', '', '', '', false, '1');           

        $('#subSeccionIdS').val('0');
        $('#subSeccionIdS').trigger('change');
    },

    GetSubSeccionSel: function (obj) {
        $('#divSubSeccion').html('');
        let vSeccionId = $(obj).val();
        let vsscTipoId = $('#sscTipoId').val();
        CommonG.HttpSelect2('/admin/GetSubSecciones', false, 'Sub-Secciones', 'Sub-Secciones', 'divSubSeccion', 'Sub-Seccion', 'subSeccionId', { "seccionId": vSeccionId, "sscTipoS": vsscTipoId }, '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');           

        $('#subSeccionId').val('0');
        $('#subSeccionId').trigger('change');
    },


    PrepareSearch: function () {

        $('#divSeccionS').html('');     
        $('#divSscTipoS').html('');

        //let pxe = CommonG.RequestAjaxSelectPmt('/common/GetEdicionesPmt', 'Edicion', 'Edicion Dist', 'divEdiciones', 'Edicion', 'EdicionId', { "total": 10, "future": 1 }, false, '', false, true, '');                          

        CommonG.HttpSelect2('/common/GetEdicionesPmt', false, 'Ediciones', 'Ediciones', 'divEdicionIngresosS', 'Edicion Ingreso', 'edicionIngresoS', { "total": 10, "future": 2 }, '', '', false, true); //, '', '', '', '', '', '', false, '1');
        CommonG.HttpSelect2('/admin/GetSecciones', false, 'Secciones', 'Secciones', 'divSeccionS', 'Seccion', 'seccionIdS', '', '', '', false, true, 'let px = ArtG.GetSubSeccionSSel(this);');                             
        CommonG.HttpSelect2('/admin/GetSscTipos', false, 'Tipo SubSecc', 'TipoSubSec', 'divSscTipoS', 'Tipo Sub-Seccion', 'sscTipoIdS', '', '', '', false, true); //, '', '', '', '', '', '', false, '1');

        $('#seccionIdS').val('');
        $('#seccionIdS').trigger('change');    
        $('#sscTipoIdS').val('');
        $('#sscTipoIdS').trigger('change');    

    },
    

    PopulateArt: function (data) {

        $('#articuloId').val(data.articuloId);
        $('#edicionIngreso').val(data.edicionIngreso);
        $('#revisado').prop('checked', data.revisado);
        $('#publicar').prop('checked', data.publicar);
    
        $('#seccionId').val(data.seccionId);
        $('#seccionId').trigger('change');
        $('#subSeccionId').val(data.subSeccionId);
        $('#subSeccionId').trigger('change');
        $('#sscTipoId').val(data.sscTipoId);
        $('#sscTipoId').trigger('change');    
      
        $('#titulo').val(data.titulo);
        $('#subTitulo').val(data.subtitulo);
        $('#contenido').val(data.contenido);
        $('#relevancia').val(data.relevancia);
        $('#relevancia').trigger('change');    
        $('#notas').val(data.notas);
        $('#palabrasClaves').val(data.palabrasClaves);

        AnexosG.GetAnexos(data.articuloId);

    },

    Search: function () {

        let vedicion = $('#edicionIngresoS').val();
        let vseccionId = $('#seccionIdS').val();
        let vsubSeccionId = $('#subSeccionIdS').val();
        let vsscTipoId = $('#sscTipoIdS').val();
        let vsearch = $('#search').val();
        let vrevisado = $('#revisadoS').val();
        let vpublicar = $('#publicarS').val();


        http('/art/ArtJ'
            , 'POST'
            , {
                'edicion': vedicion,
                'seccionId': vseccionId,
                'subSeccion': vsubSeccionId,
                'sscTipoId': vsscTipoId,
                'search': vsearch,
                'revisado': vrevisado,
                'publicar': vpublicar

            }
            , true
            , 'ArtG.RefreshTb'
            , 'R'
        );


    },

    RefreshTb: function (data) {

        //here
        let contenido = "";
      
        if (data !== null && data.length > 0) {

            contenido =
                `<div class="card">
                    <div class="card-datatable table-responsive">
                        <table id="tbArts" class="table table-striped table-bordered table-hover font80" cellspacing="0" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Id</th>

                                    <th>Seccion</th>
                                    <th>Sec Ord</th>

                                    <th>Sub-Seccion</th> 
                                    <th>SSec Ord</th>

                                    <th>Tipo</th>  
                                    <th>Ssct Ord</th>
            
                                    <th class="text-center">Relevancia</th>
                                    <th>Titulo-SubTitulo</th>  
                                    <th>Revisado</th>
                                    <th>Publicado</th>                                   

                                    <th class="text-center">ed/vw</th>
                                </tr>
                            </thead>

                            <tbody id="tbRows">`;

            data.forEach(function (itm) {
                contenido += `<tr>
                                                        <td>${itm.articuloId}</td>

                                                        <td>${itm.seccion}</td>  
                                                        <td>${itm.secOrden}</td>

                                                        <td>${itm.subSeccion}</td>                                                        
                                                        <td>${itm.ssecOrden}</td>

                                                        <td>${itm.sscTipo}</td>  
                                                        <td>${itm.ssctOrden}</td>   

                                                        <td class="text-center">${itm.relevancia}</td>   

                                                        <td><span class="font-weight-bold">${itm.titulo}</span></br>
                                                            <span class="text-tiny">${itm.subtitulo}</span>
                                                        </td>  
                                                     
                                                        <td>
                                                            <label class="custom-control custom-checkbox d-block">
                                                                <input type="checkbox" class="custom-control-input" ${itm.revisado ? "checked" : ""} disabled id="revisado_${itm.articuloId}">
                                                                    <span class="custom-control-label"></span>
                                                            </label>
                                                            <span class="d-none">${itm.revisado}</span>
                                                        </td>

                                                        <td>
                                                            <label class="custom-control custom-checkbox d-block">
                                                                <input type="checkbox" class="custom-control-input" ${itm.publicar ? "checked" : ""} disabled id="publicar_${itm.articuloId}">
                                                                    <span class="custom-control-label"></span>
                                                            </label>
                                                            <span class="d-none">${itm.publicar}</span>
                                                        </td>
                                                    

                                                        <td class="text-center">
                                                            <button class="btn btn-xs btn-info"
                                                                data-toggle="modal" href="#modalItm"
                                                                data-articuloid="${itm.articuloId}">
                                                                <i class="fa fa-edit"></i> Edit
                                                            </button>
                                                        </td>
                                                    </tr>`;
            });

            contenido += `</tbody>                               
                                        </table>
                                     </div>
                                  </div>`;

        } else {

            contenido =
                `<div class="row"> 
                     <div class="col-lg-4"></div>  
                     <div class="col-lg-4 bg-ca-lime-transparent-4 text-center py-4">  
                         <i class="fas fa-map fa-4x text-info"></i>  
                         <div class="p-sm text-center">  
                             <h2 class="p-t-lg text-uppercase">Secciones</h2>  
                             <p class="h1 text-thin mar-no">0</p>  
                             <p class="text-sm text-overflow pad-top">  
                                 <span class="text-bold">Haga nueva búsqueda</span>  
                             </p>  
                         </div>  
                     </div>  
                 <div class="col-lg-4"></div>  
            </div>`;
        }

        $('#' + ArtG.settingsTable.container).html(contenido);
        $("#" + ArtG.settingsTable.id).DataTable(ArtG.settingsTable.config);             

    },
        
    bindUIActions: function () {
             
        $('#modalItm').on('show.bs.modal', function (e) {


            let id = $(e.relatedTarget).data('articuloid') === undefined ? 0 : $(e.relatedTarget).data('articuloid');

            ArtG.PrepareArt();

            if (id > 0) {               

                ArtG.GetItm(id);               
            } 
                        

        });


        $("#btnSave").on('click', function (event) {

            event.preventDefault();

            //cambiar por solo 

            let varticuloId = $('#articuloId').val();
            let vedicionIngreso = $('#edicionIngreso').val();          
            let vrevisado = $('#revisado:checked').val() ? true : false;
            let vpublicar = $('#publicar:checked').val() ? true : false;
            let vseccionId = $('#seccionId').val();
            let vsubSeccionId = $('#subSeccionId').val();
            let vsscTipoId = $('#sscTipoId').val();
            let vtitulo = $('#titulo').val();
            let vsubTitulo = $('#subTitulo').val();
            let vcontenido = $('#contenido').val();
            let vrelevancia = $('#relevancia').val();
            let vnotas = $('#notas').val();
            let vpalabrasclaves = $('#palabrasClaves').val();
            
            if (vseccionId === undefined || vseccionId === null ||
                vsubSeccionId === undefined || vsubSeccionId === null 
            ) {                
                return CommonG.Message("wn| Verifique Info | Editorial");
            }

            if (varticuloId === null || varticuloId === undefined || varticuloId === "")
                varticuloId = "0";


            http('/art/UpdateArt'
                , 'POST'
                , {
                    "articuloId": varticuloId,
                    "edicionIngreso": vedicionIngreso,
                    "revisado": vrevisado,
                    "publicar": vpublicar,

                    "seccionId": vseccionId,
                    "subSeccionId": vsubSeccionId,                    
                    "sscTipoId": vsscTipoId,                    

                    "titulo": vtitulo,
                    "subTitulo": vsubTitulo,
                    "contenido": vcontenido,
                    "relevancia": vrelevancia,
                    "notas": vnotas,
                    "palabrasClaves": vpalabrasclaves
                }
                , true
                , 'ArtG.Search'
                , null
            );
            
        });

        $("#btnRefresh").on('click', function (event) {

            event.preventDefault();
            ArtG.PrepareSearch();
        
        });   

        $("#btnSearch").on('click', function (event) {

            event.preventDefault();          
            ArtG.Search();

        });  

        ArtG.PrepareSearch();
       

    }

};