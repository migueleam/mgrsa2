
"use strict";

let AnexosG = {
    
    settings: {
        index: 0,
        id: "tbAnexos",
        container: "divAnexos",
        config: {
            "order": [0, "desc"],
            "pagingType": ["full_numbers"],
            "pageLength": 5,
            "lengthMenu": [[5, 10, 25, -1], [5, 10, 25, "All"]],
            "responsive": true,
            "select": true,        
            "paging": true,            
            "bFilter": false,
            "bInfo": true           
        }
    },

    RefreshTb: function (data) {          

       
        let contenido = "";
        let rows = "";
        if (data === null || data.length === 0) {

            contenido = NoDataSmall('divAnexos', 'bg-ca-yellow-transparent-4', 'fa-2x ion-ios-attach d-block', 'Anexos', '0', 'NO Datos encontrados');         

            //odivContainer.innerHTML = contenido;
        }
        else {
            let oTable = AnexosG.settings.id;
            contenido = `<div class="card-datatable table-responsive">
                <table id="${oTable}" class="table table-striped table-bordered table-hover font60" cellspacing="0" style="width:100%">
                    <thead>
                        <tr>
                            <th>Id</th>                                                      
                            <th>Notas</th>   
                            <th>Nombre</th>                         
                            <th>Fecha</th>
                            <th>Hora</th>                              
                            <th>Ext</th>  
                            <th>Dn</th>    
                        </tr>
                    </thead>`;

                    contenido += `<tbody id="tbRows">`;

                    data.forEach(function (itm) {                                            
                       
                        contenido += `<tr>
                                    <td>${itm.attachmentId}</td>
                                    <td>${itm.notes}</td>
                                    <td>${itm.fileName}</td>
                                    <td>${moment(itm.attachmentDate).format('MM/DD/YYYY')}</td>                                   
                                    <td>${moment(itm.attachmentDate).format('HH:hh A')}</td>
                                    <td>${itm.extension}</td>                                               
                                    <td>
                                        <button  
                                            data-backdrop="static"
                                            data-keyboard="false"
                                            class="btn btn-info btn-xs"
                                            target="_blank"
                                            onclick="AnexosG.Href('${itm.attachmentId}${itm.extension}')"
                                    >
                                    <i class="ion ion-md-cloud-download"></i></button>                                                  
                                    </td>                                               
                                </tr>`;  
                    });
                  
                    contenido += `</tbody>
                            </table>
                        </div >`;

                    //odivContainer.innerHTML = contenido;
                    $('#' + AnexosG.settings.container).html(contenido);
                    $("#" + AnexosG.settings.id).DataTable(AnexosG.settings.config);
                             
             }           

    },       

    Href: function (fileName) {

        window.open(`//intranet.elaviso.com/task/${fileName}`);

    },

    //Download: function (fileName) {
        
    //    http('/common/DownloadDocument'
    //        , 'GET'
    //        , {
    //            "fileName": fileName
    //        }
    //        , true
    //        , ''
            

    //    );

    //},

    GetWebRootPath: function () {

        http('/common/GetWebRootPath'
            , 'POST'
            , null
            , true
            , 'AnexosG.PutPath'
            , 'R'
        );

    },

    PutPath(data) {

        $('#axNotas').val(data);

    },


    GetAnexos: function (id, tipo) {

     
        if (id === undefined || id === null)
            return;

        let vtaskId = 0;
        let videaId = 0;
        
        if (tipo === 'T')
            vtaskId = id;
        else
            videaId = id;
        

        http('/task/GetAttachments'
            , 'POST'
            , {
                "taskId": vtaskId,
                "ideaId": videaId,
                "attId": ""
            }
            , true
            , 'AnexosG.RefreshTb'            
            , 'R'           

        );
    },

    GetAnexo: function (attId) {

        if (taskId === undefined || taskId === null)
            return;

        http('/task/GetAttachments'
            , 'POST'
            , {
                "taskId": 0,
                "attId": attId
            }
            , true
            , 'AnexosG.RefreshTb'
            , 'R'

        );
    },

    
    //Populate: function (id) {
    //    //AnexosG.Inicializar();
    //    //get Info...
    //    //alert('Polate');
    //},

    //Init: function () {
    //    //alert('Inicializar');
    //},

    bindUIActions: function () {    
                    
        $('#axNotas').val('');
        //AnexosG.GetAnexos();  
    }
};
