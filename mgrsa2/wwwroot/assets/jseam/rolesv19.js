
"use strict";

var RolesG = {

    settings: {
        id: "tbRoles",
        container: "divRoles",
        config: {
                "order": [1, "asc"],
                "pagingType": ["full_numbers"],
                "pageLength": -1,
                "lengthMenu": [[5, 10, -1], [ 5 ,10,"All"]],
                "select": true,
                "responsive": true,
                //"scrollY": 360,
                //"scrollX": true,
                //"scrollCollapse": true,
                "paging": false,
                "fixedColumns": true,
                "bFilter": false //,
                //"oSearch": { "sSearch": "" }

        }       
    },

    InitRoles: function () {

        const vLoginId = $('#loginId').val();
        const vUserId = $('#userId').val();

        http('/users/GetRoles'
            , 'POST'
            , {
                userId: vUserId,
                loginId: vLoginId
            }
            , true
            , 'RolesG.RefreshRolesTb'
            , 'R'
        );


        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/users/GetRoles',
        //    data: { userId: vUserId, loginId: vLoginId },
        //    //async: false,
        //    success: function (result) {
        //        CommonG.Loading();
        //        var s = RolesG.RefreshRolesTb(result);
        //        $('#divLoading').hide();
        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.GetMessage('wn|Problemas con Servidor| Roles');
        //        //return result;
        //    }

        //});
    },
      
    GetRoles: function () {
        const vLoginId = $('#loginId').val();
        const vUserId = $('#userId').val();

        http('/users/GetRoles'
            , 'POST'
            , {
                userId: vUserId,
                loginId: vLoginId
            }
            , true
            , 'RET'  //pending
        );

        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/users/GetRoles',
        //    data: { userId: vUserId, loginId: vLoginId },
        //    //async: false,
        //    success: function (result) {
        //        CommonG.Loading();
        //        return result;               
        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.GetMessage('wn|Problemas con Servidor| Roles');
        //        return result;
        //    }

        //});
    },

    DeleteRole: function (obj) {        
        const vLoginId = $('#loginId').val();
        const vUserId = $('#userId').val();
        const vRoleId = $(obj).data('roleid');
        const vActive = false;

        http('/users/RemoveRole'
            , 'POST'
            , { userId: vUserId, loginId: vLoginId, roleId: vRoleId }
            , true
            , 'RolesG.RefreshRolesTb'
        );

        //$.ajax({
        //    type: 'POST',
        //    url: '/users/RemoveRole',
        //    data: { userId: vUserId, loginId: vLoginId, roleId: vRoleId },
        //    success: function (result) {               

        //        if (result.toLowerCase().substring(0, 2) === "ok") {
        //            CommonG.Message("ok|Role Eliminado|Mgr ROLES");
        //            result = "";   
        //        }              
        //        var s = RolesG.RefreshRolesTb(result);      
        //        $('#divLoading').hide();
        //    },
        //    fail: function (result) {
        //        CommonG.Message(result);
        //    }

        //});
    },
  
    RefreshRolesTb: function (data) {               

        let table = RolesG.settings.id;
        let conf = RolesG.settings.config
        let container = RolesG.settings.container;

        let contenido = "";
        let contx = rdata.substring(2, 3);

        if (data.length > 0) {
            contenido = 
                ` <table id="${table}" class="table table-hover table-striped w-100 table-no-bordered" cellspacing="0" style="width:100%">
                       <thead> 
                            <tr> 
                                <td>Group</td> 
                                <td>Role</td> 
                                <td>Activación</td> 
                            </tr> 
                        </thead> 
                        <tbody>`;                    
            data.forEach(function (itm) {
             
                contenido += `<tr> 
                                <td>${itm.Group}</td>
                                <td>${itm.Group}</td>                               
                                <td>${itm.RoleInitil}</td>
                                <td><button class="btn btn-xs btn-danger deleteRole" data-roleid="${data[i][1]}" onclick="let r = RolesG.DeleteRole(this);"><span class="fa fa-close"></span> Eliminar</button> </td>
                            </tr>`;                
            });
            contenido += `     </tbody>
                          </table>
                     </div>`;
                

            odivRoles.innerHTML = contenido;

            $("#" + RolesG.settings.id).DataTable(RolesG.settings.config);
        }
        else {

            contenido = `
                    <div class="card">
                        <div class="card-body text-center bg-light">
                            <i class="far fa-map d-block display-4"></i>
                        </div>
                        <div class="p-1 text-center">
                            <p class="text-semibold text-big text-uppercase">ROLES</p>
                            <p class="text-big m-1">0</p>
                            <p class="pt-2">
                                <span class="text-bold">NO</span> datos encontrados
                            </p>
                        </div>
                    </div>`;
                odivRoles.innerHTML = contenido;
        }
    },            

    NewRoles: function() {          

            const vLoginId = $('#loginId').val();
            const vUserId = $('#userId').val();
            const sel = $('#RoleId').select2('data');
            const rolids = [];
            //alert(sel.selected);
            //alert(sel.id);

            if (!sel.length > 0) {
                CommonG.Message("wn|No hay Roles Seleccionados|ROLES");
                return;
            }
            else {
                for (let i=0; i < sel.length; i++)
                {
                    rolids.push(sel[i].id);
                }
            }

        http('/users/AddRoles'
            , 'POST'
            , { userId: vUserId, loginId: vLoginId, roles: rolids }
            , true
            , 'RolesG.RefreshRolesTb'
            , 'R'
        );  

            //$.ajax({
            //    type: 'POST',
            //    url: '/users/AddRoles',
            //    data: { userId: vUserId, loginId: vLoginId, roles: rolids},                
            //    success: function (result) {             
            //       CommonG.Message("ok|Role(s) Adicionado(s)|Mgr ROLES");
            //       var s = RolesG.RefreshRolesTb(result);                   
            //    },
            //    fail: function (result) {
            //        CommonG.Message(result);
            //    }            
            //});

    },

    init: function () {        
            
            s = this.settings;
            sr = this.InitRoles();
            //this.bindUIActions();
            
    }, 
        
    bindUIActions: function () {

            //s.btnDeleteRole.on('click', function () {
            //    RolesG.DeActiveRole(this);               
            //});

            //s.btnSave.on('click', function () {
            //    RolesG.SaveProfile(this);
            //});

            //s.btnNewRol.on('click', function () {
            //    RolesG.NewRole(this);
            //    var rData = RolesG.GetRoles();
            //    RolesG.RefreshRolesTb(rData)
            //});
            
            //$('#'+ this.settings.id + ' tbody').on('click', 'button', function () {
            //    //var data = table.row($(this).parents('tr')).data();
            //    //alert(data[0] + "'s salary is: " + data[5]);
            //    //var vRoleId = $(this).data('roleid');
            //    //alert('click button: ' + vRoleId);
            //    RolesG.DeActiveRole(this);
            //});

         $('#' + this.settings.id).on('click', 'tr', function () {
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

var RolesAdm = {

    settingsTable: {
        id: "tbRolesAdm",
        container: "divRolesAdm",
        config: {
            "order": [[1, "asc"], [0, "asc"]],
            "pagingType": ["full_numbers"],
            "pageLength": 10,
            "lengthMenu": [[ 5, 10, 15, 25,-1], [ 5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "scrollCollapse": true,
            "paging": true,
            "fixedColumns": false,
            "bFilter": true,
            "bInfo": true
        }
              
    },

    GetRole: function (roleid) {

        http('/admin/GetRole'
            , 'POST'
            , {
                "roleid": roleid
            }
            , true
            , 'RolesAdm.PopulateRole'
            , 'R'
        );  

        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/admin/GetRole',
        //    data: {
        //        "roleid": roleid
        //    },
        //    success: function (result) {
        //        CommonG.Loading();
        //        var pop = RolesAdm.PopulateRole(result);
                
        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString('er| Role NO encontrado| Mgr Role', 0));
        //    }
        //});

    },

    NewRole: function () {

        const vLoginId = $('#loginId').val();
        const vUserId = $('#userId').val();
        const sel = $('#RoleId').select2('data');
        const rolids = [];
        //alert(sel.selected);
        //alert(sel.id);

        if (!sel.length > 0) {
            CommonG.Message("wn|No hay Roles Seleccionados|ROLES");
            return;
        } else {
            for (let i = 0; i < sel.length; i++) {
                rolids.push(sel[i].id);
            }
        }

        http('/users/AddRoles'
            , 'POST'
            , { userId: vUserId, loginId: vLoginId, roles: rolids }
            , true
            , 'RolesG.RefreshRolesTb'
            , 'R'
        );  

        //$.ajax({
        //    type: 'POST',
        //    url: '/users/AddRoles',
        //    data: { userId: vUserId, loginId: vLoginId, roles: rolids },
        //    success: function (result) {
        //        CommonG.Message("ok|Role(s) Adicionado(s)|Mgr ROLES");
        //        var s = RolesG.RefreshRolesTb(result);
        //    },
        //    fail: function (result) {
        //        CommonG.Message(result);
        //    }
        //});

    },

    PopulateRole: function (data) {

        const aData = data.split('~');
        const item = aData[0].split('|');

        this.PrepareNewRol();

        $('#roleId').val(item[7]);      
        $('#NombreRole').val(item[8]);
        $('#RoleInicial').val(item[4]);     
                   
        if (item[0] === 'True') {
            $('#ActivoRole').prop('checked', true);
        } else {
            $('#ActivoRole').prop('checked', false);
        }       
              
        $('#RoleGpsId').val(item[2]).trigger('change');
               
    },

    CleanRol: function () {

        $('#roleId').val('');
        $('#NombreRole').val('');
        $('#RoleInicial').val('');     
        $('#ActiveRole').prop('checked', false);
        $('#RoleGpsId').val('').trigger('change');
        //var px = CommonG.RequestAjaxSelect('/common/GetRolesGps', 'Grupo Roles', 'Grupos de Roles', 'divRolesGps', 'Grupo Roles', 'RoleGpsId', '', false, 'modalRol',false);
    },


    PrepareNewRol: function () {

        $('#roleId').val('');
        $('#NombreRole').val('');    
        $('#RoleInicial').val('');     
        $('#ActiveRole').prop('checked', true);
        $('#RoleGpsId').val('').trigger('change');
        //var px = CommonG.RequestAjaxSelect('/common/GetRolesGps', 'Grupo Roles', 'Grupos de Roles', 'divRolesGps', 'Grupo Roles', 'RoleGpsId', '', false, 'modalRol', false);
    },

    bindUIActions: function () {

        //triggered when modal is about to be shown
        $('#modalRol').on('show.bs.modal', function (e) {

            const roleid = $(e.relatedTarget).data('roleid');
            const px = CommonG.RequestAjaxSelectPmt('/common/GetRolesGps', 'Grupo Roles', 'Grupos de Roles', 'divRolesGps', 'Grupo Roles', 'RoleGpsId', '', false, 'modalRol', false, true, false);
            
            if (roleid !== 0) {
                let shw = RolesAdm.GetRole(roleid); 
            }
            else {
                $("#disTitle").html('Nuevo Rol');
                let px2 = RolesAdm.PrepareNewRol();
            }
            
        });

        $("#btnRefresh").on('click', function (event) {

            event.preventDefault();
            //cambiar por solo                      

            var busq = $('.dataTables_filter input').val();

            http('/admin/roles'
                , 'POST'
                , {
                    vsearch: busq,
                    brefreshTable: false
                }
                , true
                , 'RolesG.RefreshRolesTb'
                , 'R'
            ); 
                    
            //CommonG.Loading();
            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/roles',
            //    data: {
            //        vsearch: busq,
            //        brefreshTable: false
            //    },
            //    success: function (result) {
            //        CommonG.Loading();
            //        var upx = CommonG.Message(result);
            //        var s = RolesG.RefreshRolesTb(result);  
            //    },
            //    fail: function (result) {
            //        CommonG.Loading();
            //        var upx = CommonG.Message('er|Error en Actualización|Roles Mgr');
            //    }
            //});
        });

        $("#btnUpdate").on('click', function (event) {

            event.preventDefault();
            //cambiar por solo 
            const datav = {
                "roleId": $('#roleId').val(),
                "nombre": $('#NombreRole').val(),
                "inicial": $('#RoleInicial').val(),
                "grupo": $('#RoleGpsId').val(),
                "grupoTxt": $('#select2-RoleGpsId-container').text().substring(1).toUpperCase(),
                "activo": $("#ActivoRole").is(":checked")  ? "1" : "0"
            };

            if (datav.nombre === undefined || datav.nombre.length < 4 || datav.inicial === undefined || datav.inicial === ""  || datav.grupo === undefined || datav.grupo === "") {
                let px = CommonG.Message('wn|Verifique Datos del Role | Roles Mgr');
                return;
            }

            http('/admin/UpdateRole'
                , 'POST'
                , {
                    roleId: datav.roleId,
                    nombre: datav.nombre,
                    inicial: datav.inicial,
                    grupo: datav.grupo,
                    activo: datav.activo
                }
                , true
                , 'UpdateRow'); 
                   
            //CommonG.Loading();
            //$.ajax({
            //    type: 'POST',
            //    url: '/admin/UpdateRole',
            //    data: {
            //        roleId: datav.roleId,                 
            //        nombre: datav.nombre,
            //        inicial: datav.inicial,
            //        grupo: datav.grupo,
            //        activo: datav.activo
            //    },
            //    success: function (result) {
                   
            //        CommonG.Message(result);
            //        CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.nombre, 0);
            //        CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.grupoTxt, 1);
            //        CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.inicial, 4);
            //        if (datav.activo === "1") {
            //            $('#active' + datav.roleId).prop('checked', true);
            //        }
            //        else {
            //            $('#active' + datav.roleId).prop('checked', false);
            //        }
            //        CommonG.Loading();
            //    },
            //    fail: function (result) {
            //        CommonG.Loading();
            //        var upx = CommonG.Message('er|Error en Actualización|Roles Mgr');
            //    }
            //});

        });

        function UpdateRow(result) {

            CommonG.Message(result);
            CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.nombre, 0);
            CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.grupoTxt, 1);
            CommonG.UpdateRowTable(RolesAdm.settingsTable.id, datav.roleId, 3, datav.inicial, 4);
            if (datav.activo === "1") {
                $('#active' + datav.roleId).prop('checked', true);
            }
            else {
                $('#active' + datav.roleId).prop('checked', false);
            }
        }

        $("#NombreRole").on('keyup', function (e) {
            e.preventDefault();

            $('#divnombreroles').html('');
            const valor = $('#NombreRole').val();
            //alert('texto: ' + valor);
            if (valor !== undefined && valor.length > 4) {
                $.post('/admin/GetRoles'
                    , { texto: valor },
                    function (data) {
                        if (data.toLowerCase().substring(0,2) === "ok") {                       
                            var px = CommonG.Message('wn|Role Existente|Roles Mgr', 0);                        
                            $('#divnombreroles').html('<span class="bg-danger text-white">Role Existente</span>');
                            $('#btnUpdate').attr('disabled', true);
                        }
                        else {                        
                            var px2 = CommonG.Message('ok|Role ACEPTABLE|Codigos', 0);                                              
                            $('#divnombreroles').html('<span class="bg-success text-white">Role Aceptable</span>');
                            $('#btnUpdate').attr('disabled', false);
                        }                                  

                });
            }
        });

        function UpdateRolePrompt(data) {
            if (data.toLowerCase().substring(0, 2) === "ok") {
                let px = CommonG.Message('wn|Role Existente|Roles Mgr', 0);
                $('#divnombreroles').html('<span class="bg-danger text-white">Role Existente</span>');
                $('#btnUpdate').attr('disabled', true);
            }
            else {
                let px2 = CommonG.Message('ok|Role ACEPTABLE|Codigos', 0);
                $('#divnombreroles').html('<span class="bg-success text-white">Role Aceptable</span>');
                $('#btnUpdate').attr('disabled', false);
            }   
        }
        
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