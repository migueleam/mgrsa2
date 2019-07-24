"use strict";

const IdeaG = {

    settingsTable: {
        id: "tbIdeas",
        container: "divIdeas",
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
            //"createdRow": function (row, data, dataIndex) {
            //  if (data[7] === "True") {
            //    $(row).addClass("text-danger");
            //  }
            //},
            //"columnDefs": [
            //    {
            //      "targets": [7],
            //      "visible": false
            //    },
            //    {
            //      "className": "dt-center",
            //      "targets": "_all"
            //    }
            //]
        }
    },
    
    RefreshTb: function (data) {

        //here
        let contenido = "";

        if (data !== null && data.length > 0) {

            contenido =
                `<div class="card">
                    <div class="card-datatable table-responsive">
                        <table id="${IdeaG.settingsTable.id}" class="table table-striped table-bordered table-hover font70" cellspacing="0" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Descripcion</th>
                                    <th>Advance</th>
                                    <th>Req by</th>
                                    <th>Prioridad</th>

                                    <th>Status</th>
                                    <th>Dept</th>
                                    <th class="text-center">Fecha Plan</th>                                                                       
                                    <th class="text-center">ed/vw</th>
                                </tr>
                            </thead>

                            <tbody id="tbRows">`;

            data.forEach(function (itm) {
                let bgAdv = itm.advance === 100 ? "bg-success" : (itm.advance > 50 ? "bg-info" : "bg-warning");
                contenido += `<tr>
                                                        <td>${itm.ideaId}</td>

                                                        <td>${itm.description}</td>
                                                        <td>
                                                            <div class ="progress">
                                                              <div class ="progress-bar ${bgAdv}" style="width:${itm.advance}%; color:#000 !important">${itm.advance}%</div>
                                                            </div>
                                                        </td>
                                                        <td>${itm.userReq}</td>
                                                        <td>${itm.priority}</td>

                                                        <td>${itm.status}</td>
                                                        <td>${itm.dept}</td>
                                                        <td  class="text-center">${moment(itm.dueDate).format('L')}</td>                                                                                                         

                                                        <td class="text-center">
                                                            <button class="btn btn-xs btn-info"
                                                                data-toggle="modal" href="#modalItm"
                                                                data-id="${itm.ideaId}"
                                                                data-duedate="${moment(itm.dueDate).format('L')}"
                                                                data-description = "${itm.description}"
                                                                >
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

        $('#' + IdeaG.settingsTable.container).html(contenido);
        $("#" + IdeaG.settingsTable.id).DataTable(IdeaG.settingsTable.config);

    },

    Search: function () {


        let vsearch = $('#search').val();


        http('/task/IdeaJ'
            , 'POST'
            , {
                'search': vsearch
            }
            , true
            , 'IdeaG.RefreshTb'
            , 'R'
            , ''
            , false
        );
        
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
        $('#modalItm').on('show.bs.modal', function (e) {

            event.preventDefault();

            const id = $(e.relatedTarget).data('id');
            const desc = $(e.relatedTarget).data('description');
            const duedate = $(e.relatedTarget).data('duedate');

            let modaltitle = document.getElementById('modalTitle');

            if (id !== 0) {
                modalTitle.innerHTML = `<span id="divideaDescr" class="text-ca-green">${desc}</span><br/><span id="divideaduedate" class ="text-tiny text-secondary">${duedate}</span>`;
            } else {
                modalTitle.innerHTML = `<span id="divideaDescr" class="text-info">Nueva Idea</span>`;
            }

            CleanForm();
            
            if (id !== 0) {
                GetIdea(id);
                GetFUs(id);
                AnexosG.GetAnexos(id, "I");
            }
            else {
                IdeaFUG.RefreshTb('');
                AnexosG.RefreshTb('');
            }
            //HERE
            CleanFU();
            CleanAttachment();

        });

        $('#modalItm').on('hidden.bs.modal', function () {

            const vsearch = $('.dataTables_filter input').val();

            //cambiar por solo                      
            http('/task/idea'
                , 'POST'
                , {
                    vsearch: vsearch
                }
                , true
            );

        });

        $('.fecha').datepicker({
            todayHighlight: true,
            autoclose: true
        });

        $('.tiempo').timepicker({
            template: 'modalEdi'
        });                 

    }

};

function CleanForm(data) {

    const oideaId = document.getElementById('ideaId');
    const oDueDate = document.getElementById('dueDate'); 
    const oDescription = document.getElementById('description');
    const oAdvance = document.getElementById('advance');
    const oNotas = document.getElementById('notas');

    oideaId.value = "0";
    oDescription.value = "";
    oAdvance.value = "0";
    oNotas.value = "";
    oDueDate.value = moment(new Date()).format('L');

    CommonG.HttpSelect2J('/task/GetPriority', false, 'Prioridad', 'Prioridad', 'divPriority', 'Prioridad', 'priorityId', '', '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');
    CommonG.HttpSelect2J('/task/GetStatusTask', false, 'StatusTask', 'StatusTask', 'divStatusTask', 'StatusTask', 'statusTaskId', '', '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');
    CommonG.HttpSelect2J('/task/GetUserReq', false, 'UserReq', 'UserReq', 'divUserReq', 'UserReq', 'userReqId', '', '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');
    CommonG.HttpSelect2J('/task/GetDepartment', false, 'Departamento', 'Departamento', 'divDepartment', 'Departamento', 'deptId', '', '', 'modalItm', false, true); //, '', '', '', '', '', '', false, '1');

    $('#priorityId').val('').trigger('change');
    $('#statusTaskId').val('').trigger('change');
    $('#deptId').val('').trigger('change');
    $('#userReqId').val('').trigger('change');



}

function GetIdea(ideaId) {

    http('/task/getIdea'
        , 'POST'
        , {
            'ideaId': ideaId
        }
        , true
        , 'FillForm'
        , 'R'
    );
}

function FillForm(data) {

    //let aidea = [];
    //aidea = data.split('|');
    let oIdeaId = document.getElementById('ideaId');
    let oDueDate = document.getElementById('dueDate');
    let oDescription = document.getElementById('description');
    let oAdvance = document.getElementById('advance');
    let oNotas = document.getElementById('notas');
  
    let oProgress = document.getElementById('divProgress');
    let bgAdv =  data.advance === 100 ? "bg-success" : data.advance > 50 ? "bg-info" : "bg-warning";
    oProgress.innerHTML = `<div class="progress"><div class="progress-bar ${bgAdv}" style="width:${data.advance}%; color:#000 !important">${data.advance}%</div></div>`;
    oAdvance.disabled = true;

    oIdeaId.value = data.ideaId;
    oDueDate.value = moment(data.dueDate).format('L');
    oDescription.value = data.description;
    oAdvance.value = data.advance;
    oNotas.value = data.notes;

    $('#priorityId').val(data.priorityId).trigger('change');
    $('#statusTaskId').val(data.statusTaskId).trigger('change');
    $('#deptId').val(data.departmentId).trigger('change');
    $('#userReqId').val(data.userId).trigger('change');  
   

}

//function RefreshTask(id) {

//    getInfoGral(id);
//    refreshGrid(0);
//    refreshGrid(1);

//}

function RefreshNewIdea(id) {

    //Message('s', 'Nuevo Proyecto adicionado', 'Proyecto');

    obtn = document.getElementById('btnSave').disabled = true;
    obtn = document.getElementById('btnSaveFU').disabled = false;
    
    IdeaG.Search(); // refreshGrid();

}

function SaveIdea() {

     
      let videaid = document.getElementById('ideaId').value;

      const vDueDate = document.getElementById('dueDate').value;
      const vDescription = document.getElementById('description').value;
      const vAdvance = document.getElementById('advance').value;

      const vstatusidea = document.getElementById('statusTaskId').value;
      const vuserreq = document.getElementById('userReqId').value;
      const vpriority = document.getElementById('priorityId').value;
      const vdept = document.getElementById('deptId').value;

      const vNotas = document.getElementById('notas').value;

      const url = '/task/GuardarIdea';
      const type = 'POST';
      let callBack = "CBSaveIdea";

      //let callback = 'IdeaG.Search';                  
      //if (oideaid !== "0") {
      //      IdeaG.RefreshNewTask(oideaid);
      //}
      

      http(url
          , type
          , {
              "ideaid": videaid,
              "duedate": vDueDate,
              "description": vDescription,
              "advance": vAdvance,
              "statusidea": vstatusidea,
              "userreq": vuserreq,
              "priority": vpriority,
              "dept": vdept,
              "notas": vNotas
          }
          , true
          , callBack
          , 'R'
      );

}

function CBSaveIdea(data) {

    if (data !== null) {
        if (data.identity !== 0) {

            $('#ideaId').val(data.identity);
            IdeaG.Search();

        }
    } else {
        Mensaje({
            "typeMsg": "wn",
            "message": "Tarea NO Adicionada",
            "title": "Tareas e Ideas"
        });
    }

}


/////////////////
// 
//    Avance...
//
////////////////

const IdeaFUG = {

    settingsTable: {
        id: "tbIdeasFU",
        container: "divIdeasFU",
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
            //"createdRow": function (row, data, dataIndex) {
            //  if (data[7] === "True") {
            //    $(row).addClass("text-danger");
            //  }
            //},
            //"columnDefs": [
            //    {
            //      "targets": [7],
            //      "visible": false
            //    },
            //    {
            //      "className": "dt-center",
            //      "targets": "_all"
            //    }
            //]
        }
    },

    RefreshTb: function (data) {

        //here
        let contenido = "";

        if (data !== null && data.length > 0) {

            contenido =
                `<div class="card">
                    <div class="card-datatable table-responsive">
                        <table id="${IdeaG.settingsTable.id}" class="table table-striped table-bordered table-hover font70" cellspacing="0" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th class="text-center">Fecha Plan</th>                                   
                                    <th>Advance</th>
                                    <th>Notas</th>     
                                    <th>Completa</th>    
                                    <th class="text-center">Vw</th>
                                </tr>
                            </thead>

                            <tbody id="tbRows">`;

            data.forEach(function (itm) {
                let bgAdv = itm.advance === 100 ? "bg-success" : (itm.advance > 50 ? "bg-info" : "bg-warning");
                contenido += `<tr>
                                                        <td>${itm.fuId}</td>
                                                        <td  class="text-center">${moment(itm.fuDate).format('L')}</td>                                                                                                                                                                
                                                        <td>
                                                            <div class ="progress">
                                                              <div class ="progress-bar ${bgAdv}" style="width:${itm.advance}%; color:#000 !important">${itm.advance}%</div>
                                                            </div>
                                                        </td>
                                                        <td>${itm.notes}</td>    
                                                        <td>
                                                            <label class="custom-control custom-checkbox d-block">
                                                                <input type="checkbox" class="custom-control-input" ${itm.complete == 1 ? "checked" : ""} id="complete_${itm.fuId}" onClick="CComplete(${itm.fuId},'I');">
                                                                <span class="custom-control-label"></span>
                                                            </label>
                                                            <span class="d-none">${itm.complete}</span>
                                                        </td>

                                                        <td class="text-center">
                                                            <button class="btn btn-xs btn-info"                                                              
                                                                data-fuid="${itm.fuId}"
                                                                data-fuadvance= "${itm.advance}"
                                                                data-fudate="${moment(itm.fuDate).format('L')}"
                                                                data-funotes = "${itm.notes}"                     
                                                                onclick="let px = FillFormFU(this);"
                                                                >
                                                                <i class="fa fa-edit"></i>
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
                         <i class="fas fa-info fa-2x text-info"></i>
                         <div class="p-sm text-center">
                             <h6 class="pt-4 text-uppercase">Avances</h6>
                             <p class="text-thin mar-no">No data</p>                           
                         </div>
                     </div>
                 <div class="col-lg-4"></div>
            </div>`;
        }

        $('#' + IdeaFUG.settingsTable.container).html(contenido);
        $("#" + IdeaFUG.settingsTable.id).DataTable(IdeaG.settingsTable.config);

    },

  
    Search: function () {


        let vsearch = $('#search').val();


        http('/task/IdeaJ'
            , 'POST'
            , {
                'search': vsearch
            }
            , true
            , 'IdeaG.RefreshTb'
            , 'R'
            , ''
            , false
        );


    },

    bindUIActions: function () {

        $('#' + IdeaFUG.settingsTable.id).on('click', 'tr', function () {
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



function CleanFU() {

    $('#fuId').val('');
    $('#fuDate').value = moment(new Date()).format('L');
    $('#fuAdvance').val('');
    $('#fuNotes').val('');   
    $('#btnSaveFU').prop("disabled", false); 

}

function GetFUs(ideaId, advance) {

    if (advance !== null && advance !== undefined && advance > 0)
        $('#advance').val(advance);

    http('/task/GetIdeaFU'
        , 'POST'
        , {
            'ideaId': ideaId
        }
        , true
        , 'IdeaFUG.RefreshTb'
        , 'R'
    );
}

function UpdateAll(ideaId, advance) {

    if (advance !== null && advance !== undefined && advance > 0)
        $('#advance').val(advance);

    IdeaG.Search();
    GetFUs(ideaId, advance);

}


function FillFormFU(obj) {

    //event.preventDefault();
    let oId = obj.id;
        
    let fuId = $(obj).data('fuid');
    let fuAdvance = $(obj).data('fuadvance');
    let fuDate = $(obj).data('fudate');
    let funotes = $(obj).data('funotes');
    
    $('#fuId').val(fuId);
    $('#fuDate').val(fuDate);
    $('#fuAdvance').val(fuAdvance);
    $('#fuNotes').val(funotes);
    
    let obtnSaveFU = document.getElementById('btnSaveFU');
    obtnSaveFU.disabled = true;
       
}

function NewFUAdvance() {

    CleanFU();
    document.getElementById('fuDate').focus();
  
}


function SaveFU() {

    let videaid = document.getElementById('ideaId').value;
    let vfuId = document.getElementById('fuId').value;   
    let vfuDate = document.getElementById('fuDate').value;
    let vfuAdvance = document.getElementById('fuAdvance').value;
    let vfuNotes = document.getElementById('fuNotes').value;

    const url = '/task/GuardarFUI';
    const type = 'POST';
    const callBack = `GetFUs(${videaid},${vfuAdvance})`;
      
    http(url
        , type
        , {
            "ideaId": videaid,
            'fuId': vfuId,
            "fuDate": vfuDate,
            "fuAdvance": vfuAdvance,
            "fuNotes": vfuNotes
        }
        , true
        , callBack
        , 'F'         
    );
    
}

function CComplete(fuId, tipo) {
  
    if (fuId !== "" || fuId !== undefined) {
        Confirmar("Tarea e Idea (in)Completa", "Seguro?", "warning", `SaveComplete(${fuId},"${tipo}");`);
    }
    else {
        Message("er|No se encontro el Id| Tarea e Ideas |2000");
    }

    return false;
}

function SaveComplete(fuId, tipo) {

    http('/task/CompleteTI'
        , 'POST'
        , {
            'fuId': fuId,
            'tipo': tipo
        }
        , true     
        
    );

}


/////////////////
// 
//    Attachement
//
////////////////


function SaveAttachment() {

    //Upload all files

    //const nfiles = myDropzone.files.length;
    const oideaid = document.getElementById('ideaId').value;
    const ofecha = document.getElementById('attachmentDate').value;
    const onotas = document.getElementById('attNotas').value;
    const url = '/task/uploadFiles';
    const callBack = 'IdeaG.RefreshAtt';

    myDropzone.files.forEach(function (itm) {

        //alert("Filename " + myDropzone.files[i].name);                      

        const frm = new FormData();
        frm.append("ideaId", oideaid);
        frm.append("attachmentDate", ofecha);
        frm.append("attNotas", onotas);
        frm.append("file", itm);

        //enviarServidorPmt(url, type, ShowResultSaveFuncPmts, data, refreshGrid, 0);

        http(url
            , 'POST'
            , frm
            , true
            , callBack
            , 'R'
        );

    });


}

function CleanAttachment() {

  $('#axDate').datepicker('setDate', new Date());
  $('#axNotas').val('');

}



