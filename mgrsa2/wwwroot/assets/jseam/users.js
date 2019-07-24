
"use strict";

var u, UsersG = {

    settingsTable: {

        index: 0,
        id: "tbUsers",
        container: "divUsers",
        tbRows: "tbRows",
        showHeader: true,
        config: {
            "order": [1,'asc'],
            "pagingType": ["full_numbers"],
            "pageLength": 10,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "scrollCollapse": true,
            "paging": true,
            "fixedColumns": false,
            "bFilter": true,
            "bInfo": true
            ,"columnDefs": [
                {
                    "targets": [8],
                    "render": function (data, type, row) {
                        return data.split(",").join("<br/>");
                    }
                   
                },
                {
                    "targets": [10],
                    "render": function (data, type, row) {
                        return data.split(",").join("<br/>");
                    }

                }
                //,
                //{
                //    "className": "th dt-center",
                //    "targets": "_all"
                //}
            ]
                       

        }
        
    },


    RefreshTb: function (data) {

        let odivContainer = document.getElementById("divUsers");
        let contenido = "";
        let rows = "";
        if (data.length === 0) {

            contenido = NoDataTb('divUsers', 'bg-warning-darker', 'ion-ios-create d-block', 'Usuarios', '0', 'NO Datos encontrados');

            odivContainer.innerHTML = contenido;
        }
        else {
            let oTable = UsersG.settingsTable.id;
            //<th>Hora</th>
            //<th>HLSt</th>
            contenido = `
                <table id="${oTable}"  class="table table-hover table-striped w-100 table-no-bordered" style="width:100%">
                    <thead class="bg-primary-50">
                        <tr>
                            <th>Id</th>

                            <th>Nombre</th>
                            <th>Usuario</th>
                            <th>Email</th>
                            <th>Codigo</th>
                            <th>Telefono</th>

                            <th>Ext</th>
                            <th>Activo</th>
                            <th>Nivel</th>
                            <th>Area</th>
                            <th>Dept</th>

                            <th>Ofic</th>
                            <th>Producto</th>
                                      
                            <th>Auth</th>
                            <th>Prof</th>

                        </tr>
                    </thead>`;

            contenido += `<tbody id="tbRows">`;

            data.forEach(function (itm) {               
                let xrow = "";
                     xrow =    `
                                    <tr>                                      
                                        <td>${itm.profileId}</td>
                                        <td>${itm.nombre}</td>
                                        <td>${itm.userName}</td>
                                        <td>${itm.email}</td>
                                        <td>${itm.codigo}</td>

                                        <td>${itm.phoneFmt}</td>
                                        <td>${itm.extension}</td>
                                        <td>
                                            <div class="custom-control custom-checkbox custom-checkbox-circle custom-control-inline">
                                                            <input type="checkbox" class="custom-control-input" ${itm.activo ? "checked" : ""} disabled id='Active${itm.profileId}'/> 
                                                            <label class="custom-control-label" for="ActivePf"></label>
                                            </div>                                               
                                            <span class="d-none">${itm.activo}</span>
                                        </td>
                                        <td>${itm.roles[2]}</td>
                                        <td>${itm.roles[0]}</td>
                                        <td>${itm.roles[1]}</td>
                                        <td>${itm.roles[3]}</td>
                                        <td>${itm.roles[4]}</td>                                     

                                        <td data-title="Auth">
                                            <button class="btn btn-xs btn-danger" data-toggle="modal" data-target="#modalAuth" data-userid="${itm.id}"><i class="fal fa-key"></i></button>
                                        </td>
                                        <td data-title="Prof">
                                            <button class="btn btn-xs btn-success" data-toggle="modal" data-target="#modalProf" data-userid="${itm.id}" data-nombre="${itm.nombre}"><i class="fal fa-cog"></i></button>
                                        </td>
                                    </tr >`
                contenido += xrow;
            });

            contenido += `</tbody>
                            </table>
                        `;

            odivContainer.innerHTML = contenido;
            $("#" + UsersG.settingsTable.id).DataTable(UsersG.settingsTable.config);
            
        }
    },    

   
    GetAuth: async function (userid, divContent) {

        //let fmd = new FormData();
        //fmd.append('formdata', userid);

        //let data = await fetchproc('/users/GetAuth', fmd);

        //UsersG.PopulateAuth(data);

        http(
            '/users/GetAuth'
            , 'POST'
            , { "userid": userid }
            , true
            , 'UsersG.PopulateAuth'
            , 'R'
        );              

    },
    
    PopulateAuth: function (data) {

        const aData = data.split('^');
        //HERE ver prod.js in intra17sa
    },

    SaveProfile: async function () {


        //let fmd = new FormData();

        //let form = document.querySelector('#editprof');
        //fmd = serialize(form);
        //let data = await fetchproc('/users/EditProfJ', fmd, true);             

        http('/users/EditProfJ'
            , 'POST'
            , datav
            , true
            );      
        
  
        //});
    },

    GetUser: async function (userid) {
        
        //let fmd = new FormData();
        //fmd.append( "userid", userid );
        //let data = await fetchproc('/users/GetUser', fmd);

        //UsersG.PopulateUser(data);


        http('/users/GetUser'
            , 'POST'
            , { "userid": userid }
            , true
            , 'UsersG.PopulateUser'
            , 'R'
        );       

       

    },

    RefreshTableUsers: async function () {

        const busq = $('.dataTables_filter input').val();

        //let fmd = new FormData();
        //fmd.append("vsearch", busq);            
        //fmd.append("brefreshTable", true);

        //let data = await fetchproc('/users/index', fmd, true);               

        http('/users/index'
            , 'POST'
            , {
                "vsearch": busq,
                "brefreshTable": true
            }
            , true            
        );
      
    },

   

    PopulateUser: function (data) {

        const aData = data.split('~');
        const item = aData[0].split('|');

        this.CleanUser();

        $('#Id').val(item[0]);
        $('#Codigo').val(item[3]);
        $('#NombreA').val(item[4]);
        $('#UserNameA').val(item[10]);
        $('#EmailA').val(item[12]);
        //if (item[5] === 'True')
        //    $('#Activo').prop('checked', true);
        //else
        //    $('#Activo').prop('checked', false);
        //$('#Password').val(item[11]);

    },

    CleanUser: function () {

        $('#Id').val('');
        $('#Codigo').val('');
        $('#NombreA').val('');
        $('#UserNameA').val('');
        $('#EmailA').val('');            
        $('#Password').val('');
        $('#ActivePf').prop('checked', false);
    },

    PrepareNew: function () {

        $('#CodigoNew').val('');
        $('#NombreNew').val('');
        $('#UserNameNew').val('');
        $('#PasswordNew').val('');      

    },

    PrepareProf: function () {

        $('#loginIdPf').val('');
        $('#userIdPf').val('');

        $('#ProfileIdPf').val('');
        $('#CodigoPf').val('');
        $('#UserNamePf').val('');
        $('#EmailPf').val('');

        $('#NombrePf').val('');
        $('#divSupervisorId').html('');
        $('#PhonePf').val('');
        $('#ExtensionPf').val('');
        $('#divPhoneProveedores').html('');
        $('#ContactosPermitidosPf').html('');
        $('#card').val('');

        $('#twitterPf').val('');
        $('#facebookPf').val('');
        $('#instagramPf').val('');
        $('#linkedInPf').val('');

                       
        //populate Selects...               
                     
        CommonG.RequestAjaxSelectPmt('/common/GetUsersByRole', 'Supervisores', 'User MGR', 'divSupervisor', 'Supervisor', 'SupervisorId', { 'role': 'supervisor' }, '', 'modalProf', false,true, '');

        //HERE add common/...
        CommonG.RequestAjaxSelectPmt('/common/GetPhoneProveedores', 'ProveedoresTel', 'User MGR', 'divPhProveedores', 'Tel Proveedores', 'PhProveedoresId', '', '', 'modalProf', false, true, '');
        CommonG.RequestAjaxSelectPmt('/common/GetContactosPermitidos', 'Contactos Permitidos', 'User MGR', 'divContactosPerm', 'Contactos Permitidos', 'ContactosPermId','', '', 'modalProf', false, true, '');

        //HERE add EntryCards, Roles...     
        CommonG.RequestAjaxSelectPmt('/common/GetRoles', 'Roles', 'User MGR', 'divRolesId', 'Roles', 'RoleId', '', true, 'modalProf',true, true, '');       

    },
        

    GetProfile: async function (userid) {


        //let fmd = new FormData();
        //fmd.append("userid", userid);
        //let data = await fetchproc('/users/GetProfile', fmd);           
        //UsersG.PopulateProfile(data);

        http('/users/GetProfile'
            , 'POST'
            , { "userid": userid }
            , true
            , 'UsersG.PopulateProfile'
            , 'R'
        );       
       

    },

    PopulateProfile: function (data) {

        //data: profile, entrycards, roles...

        if (data.substring(0, 2).toLowerCase() === 'er') {
            let px = CommonG.Message(data);
            return;
        }

        const alist = data.split('^'); 
        const aprof = alist[0].split('|');  

        $('#loginId').val(aprof[2]);
        $('#userId').val(aprof[0]);

        $('#ProfileIdPf').val(aprof[1]);
        $('#CodigoPf').val(aprof[3]);
        $('#UserNamePf').val(aprof[4]);
        $('#EmailPf').val(aprof[5]);

        if (aprof[7] === 'True')
            $('#ActivePf').prop('checked', true);            
        else
            $('#ActivePf').prop('checked', false);
        
     
        $('#NombrePf').val(aprof[8]);

        $('#SupervisorId').val(aprof[9]);
        $('#SupervisorId').trigger('change');

        $('#PhonePf').val(aprof[10]);
        $('#ExtensionPf').val(aprof[11]);
        $('#PhProveedoresId').val(aprof[12]);
        $('#PhProveedoresId').trigger('change'); 
        $('#ContactosPermId').val(aprof[13]);
        $('#ContactosPermId').trigger('change');   


        $('#twitterPf').val(aprof[14]);
        $('#facebookPf').val(aprof[15]);
        $('#instagramPf').val(aprof[16]);
        $('#linkedInPf').val(aprof[17]);
        

        let ecx = EntryCardsG.InitEC(aprof[1]);
        //var pex = EntryCardsG.RefreshECTb(alist[1]);
        //alert(alist[2]);
        let prx = RolesG.RefreshRolesTb(alist[2]);
        
    },


    PopulateResults: function (data) {

        if (data.substring(0, 2).toLowerCase() === 'er') {
            let px = CommonG.Message(data);
            return;
        }
        if (data.substring(0, 2).toLowerCase() === 'ok') {
            let px = CommonG.Message(data);        
        }
        //var datav = {
        //    "profileId": $('#ProfileIdPf').val(),
        //    "ActivePf": $("#ActivePf").is(":checked"),
        //    "nombrepf": $('#NombrePf').val(),
        //    "supervidorId": $('#SupervisorId').val(),
        //    "phonepf": $('#PhonePf').val(),
        //    "extensionpf": $('#ExtensionPf').val(),
        //    "phproveedorId": $('#PhProveedoresId').val(),
        //    "contactosPerm": $('#ContactosPermId').val()
        //};
                
        var vprofId = $('#ProfileIdPf').val(); //7
        var activeId = 'Active' + vprofId; //6
        var vnombre = $('#NombrePf').val(); //0
        var vPhone = $('#PhonePf').val(); //4
        var vExt = $('#ExtensionPf').val();  //5           

        //CommonG.UpdateRowTable(ProdG.settingsDesp.id, profileid, colid 5, xtitulo, 4); 

        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vnombre, 1);
        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vPhone, 5);
        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vExt, 6);            
                       
        if ($('#ActivePf').is(':checked')) {
            $('#' + activeId).prop('checked', true);
        }
        else {
            $('#' + activeId).prop('checked', false);
        }
    },
    

    bindUIActions: function () {

        $("#CodigoNew").on('keyup', function (e) {

            e.preventDefault();

            $('#divcodigos').html('');
            var valor = $('#CodigoNew').val();
            //alert('texto: ' + valor);
            if (valor !== undefined && valor.length > 3) {

                $.post(
                    '/home/GetCodigos',
                    { texto: valor },
                    function (data) {
                        if (data.toLowerCase().indexOf('select') > 0) {
                            if (valor.length === 7) {
                                let px = CommonG.Message('wn|Codigo Existente|Codigos', 0);
                            }
                            $('#btnSaveNew').attr('disabled', true);
                        }
                        else {
                            if (valor.length === 7) {
                                let px2 = CommonG.Message('ok|Codigo ACEPTABLE|Codigos', 0);
                            }
                            $('#btnSaveNew').attr('disabled', false);
                        }
                        $('#divcodigos').html(data);
                  
                        $('#codigos').select2({
                            placeholder: 'Codigo Existentes',
                            dropdownParent: $('#modalAuthNew')
                        });
                    //$('.selectpicker').selectpicker('render');

                });
            }
        });


        $("#btnRefresh").on('click', function (event) {

            event.preventDefault();
            //cambiar por solo                      

            const busq = $('.dataTables_filter input').val();

            http('/users/index'
                , 'POST'
                , {
                    vsearch: busq,
                    brefreshTable: false
                }
                , true
                
            );                        
          
        });


        $("#btnUpdatePf").on('click', function (event) {

            event.preventDefault();
            //cambiar por solo                      

            const datav = {
                "profileId": $('#ProfileIdPf').val(),
                "ActivoPf":  $("#ActivePf").is(":checked"),    
                "nombrepf": $('#NombrePf').val(),
                "supervidorId": $('#SupervisorId').val(),
                "phonepf": $('#PhonePf').val(),
                "extensionpf": $('#ExtensionPf').val(),
                "phproveedorId": $('#PhProveedoresId').val(),
                "contactosPerm": $('#ContactosPermId').val(),
                "twitter": $('#twitterPf').val(),
                "facebook": $('#facebookPf').val(),
                "instagram": $('#instagramPf').val(),
                "linkedIn": $('#linkedInPf').val(),
            };
          
            if (datav.nombrepf === undefined || datav.nombrepf.length < 4 || datav.profileId === undefined || datav.profileId === "") {
                let px = CommonG.Message('wn|Verifique Nombre del Usuario | Users Mgr');
                return;
            }


            http('/users/UpdateProf'
                , 'POST'
                , datav
                , true
                , 'UsersG.PopulateResults'
                , 'R'
            );


            //CommonG.Loading();
            //$.ajax({
            //    type: 'POST',
            //    url: '/users/UpdateProf',
            //    data: datav,
            //    success: function (result) {    
            //        CommonG.Loading();
            //        var rpf = UsersG.PopulateResults(result);
            //    },
            //    fail: function (result) {
            //        CommonG.Loading();
            //        var upx = CommonG.Message('er|Error en Actualización|Users Mgr');
            //    }

            //});
        });

        
        $("#btnUpdate").on('click', function (event) {

            event.preventDefault();
            //cambiar por solo 
            const xid = $('#Id').val();
            const xcodigo = $('#Codigo').val();
            const xpassword = $('#Password').val();

            if (xpassword === undefined || xpassword.length < 4) {
                let px = CommonG.Message('wn|Cheque el Password, es obligatorio | Users Mgr');
                return;
            }

            http('/users/UpdateUser'
                , 'POST'
                , {
                    Id: xid,
                    Password: xpassword
                }
                , true
                , `function(){ $('#btnUpdate').attr('disabled', 'disabled') }`
                , 'NONE'
            );
            //CommonG.Loading();
            //$.ajax({
            //    type: 'POST',
            //    url: '/users/UpdateUser',
            //    data: {
            //        Id: xid,
            //        Password: xpassword
            //    },
            //    success: function (result) {
            //        CommonG.Loading();
            //        $('#btnUpdate').attr('disabled', 'disabled');
            //        var px = CommonG.Message(result);
            //    },
            //    fail: function (result) {
            //        CommonG.Loading();
            //        var px2 = CommonG.Message(result);
            //    }
            //});
        });

        //metodo directo para crear Usuario

        $("#btnSaveNew").on('click', function (event) {

            event.preventDefault();

            //var xId = $('#Id').val();
            const xCodigo = $('#CodigoNew').val();
            const xNombre = $('#NombreNew').val();
            const xUserName = $('#UserNameNew').val();
            const xPassword = $('#PasswordNew').val();
            const xSearch = $('.dataTables_filter input').val() + '';
            const xSales = $("#chkSales").is(":checked");
            const xOfAdcr = $('#OfAdcr').val();

               
            if (xCodigo === undefined || xNombre === undefined || xUserName === undefined || xPassword === undefined
                || xCodigo.length < 7 || xNombre.length < 7 || xUserName.length < 5 || xPassword.length < 4) {

                const msg = ' Revise:';
                if (xCodigo.length < 7)
                    msg += " Codigo ";

                if (xNombre.length < 7)
                    msg += " Nombre ";

                if (xUserName.length < 7)
                    msg += " Nombre de Usuario ";

                if (xPassword.length < 4)
                    msg += " Password ";

                let px = CommonG.Message('wn|' + msg + '| Users Mgr');

                return;
            }



            if (xPassword === undefined || xPassword.length < 4) {
                let px2 = CommonG.Message('wn|Cheque el Password, es obligatorio | Users Mgr');
                return;
            }
            const xEmail = $('#Email').val();     

            http('/users/AddUser'
                , 'POST'
                , {
                    nombre: xNombre,
                    usuario: xUserName,
                    codigo: xCodigo,
                    passw: xPassword,
                    search: xSearch,
                    sales: xSales,
                    ofAdcr: xOfAdcr
                }
                , true
                , 'UsersG.RefreshTb'
                , 'R'
                
            );

            //function() {
            //    $('#btnCreateNew').attr('disabled', 'disabled');
            //    UsersG.RefreshTableUsers();
           // }
         
            //CommonG.Loading();
            //$.ajax({
            //    type: 'POST',
            //    url: '/users/AddUser',
            //    data: {
            //        nombre: xNombre,
            //        usuario: xUserName,
            //        codigo: xCodigo,
            //        passw: xPassword,
            //        search: xSearch
            //    },

            //    success: function (result) {

            //        CommonG.Loading();

            //        if (result !== "" || result !== undefined) {

            //            var aData = result.split('^');

            //            if (aData[0].substring(0, 2).toLowerCase() === 'ok') {

            //                //userprofiles
            //                //var aUsers = aData[1].split('~');    
            //                //var item = aUsers[0].split('|');
            //                //var aColName = [
            //                //    "Nombre",
            //                //    "Usuario",
            //                //    "Email",
            //                //    "ProfileId",
            //                //    "LoginId",
            //                //    "Codigo"
            //                //];
            //                //var aColValue = [
            //                //    item[6],
            //                //    item[3],
            //                //    item[1],
            //                //    item[4],
            //                //    item[5],
            //                //    item[7]
            //                //];

            //                //add row
            //                //var px = CommonG.AddRowTable(UsersG.settingsTable.id, aColName, aColValue);
            //                //$("#" + UsersG.settingsTable.id).DataTable(UsersG.settingsTable.config);
                          

            //                $('#btnCreateNew').attr('disabled', 'disabled');

            //                //routes
            //                //var aRoutes = aData[1].split('~');     
            //                //var route = aRoutes[0].split('|');
            //                //if (route[1] !== "") {
            //                //    $('.dataTables_filter input').val(route[1]);
            //                //}
            //                UsersG.RefreshTableUsers();
            //            }
            //            var px2 = CommonG.Message(aData[0]);
            //        }                                               
                    
            //    },
            //    fail: function (result) {
            //        CommonG.Loading();
            //        var px3 = CommonG.Message(result);
            //    }
            //});                   


        });


        //triggered when modal is about to be shown
        $('#modalAuth').on('show.bs.modal', function (e) {

            //get data-id attribute of the clicked element
            const userId = $(e.relatedTarget).data('userid');

            if (userId !== 0) {
                UsersG.GetUser(userId); //id, divContent                                                                 
            }
            else {
                CommonG.Message("ok|Usuario No Encontrado| Usuarios Mgr");
            }
            $('#btnUpdate').removeAttr('disabled');

        });

        //triggered when modal is about to be shown
        $('#modalAuthNew').on('show.bs.modal', function (e) {
            $("#disTitle").html('Nuevo Usuario');
            let px = UsersG.PrepareNew();
        });

        //triggered when modal is about to be shown
        $('#modalProf').on('show.bs.modal', function (e) {  
            
            const nombre = $(e.relatedTarget).data('nombre');          
            const userId = $(e.relatedTarget).data('userid');

            //populate...
            $("#profTitleP").html(nombre);
            UsersG.PrepareProf();    
            
            if (userId !== 0) {
                UsersG.GetProfile(userId); //id, divContent                                                                                
            }
            else {
                CommonG.Message("ok|Usuario No Encontrado| Usuarios Mgr");
            }
            //HERE ADICIONAR BUTTONES

        });               

        $('#modalProf').on('hidden.bs.modal', function () {

            //var vprofId = $('#ProfileIdPf').val(); //7
            //var activeId = 'Active' + vprofId; //6
            //var vnombre = $('#NombrePf').val(); //0
            //var vPhone = $('#PhonePf').val(); //4
            //var vExt = $('#ExtensionPf').val();  //5           

            ////CommonG.UpdateRowTable(ProdG.settingsDesp.id, profileid, colid 5, xtitulo, 4); 

            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vnombre, 1);
            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vPhone, 5);
            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, vExt, 6);          
            
            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, "", 8);      
            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, "", 9);      
            //CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, "", 10);      
            ////col 6
            
            //if ($('#'+ activeId).is(':checked')) {
            //    $('#' + activeId).prop('checked', true);
            //}
            //else {
            //    $('#' + activeId).prop('checked', false);
            //}
            
            //var rtable = $("#" + RolesG.settings.id).DataTable();
            //var rdata = rtable.rows().data();
            //var acols = [];
            //var nivel = "";
            
            //rdata.each(function (value, index) {               
            //    //alert('Data in index: ' + index + ' is: ' + value);
            //    var nivel = value[0].substring(0, 1);             
            //    if (nivel === 'D') {
            //        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, value[1], 8);      
            //    }
            //    else if (nivel === 'N') {
            //        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, value[1], 9);      
            //    }
            //    else if (nivel === 'O') {
            //        CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 0, value[1], 10);     
            //    }                
            //});




            ////CommonG.UpdateRowTable(UsersG.settingsTable.id, vprofId, 7, vExt, 5);         



            ////var busq = $('.dataTables_filter input').val();
            ////refreshtable
            ////if (busq !== "") {
            ////    $('.dataTables_filter input').val(busq);
            ////}
          
        });
        
        //$('#PhonePf').mask('(999) 999-9999');
        //$('#ExtensionPf').mask('999');
        //$('#card').mask('9999999999');           
               
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
