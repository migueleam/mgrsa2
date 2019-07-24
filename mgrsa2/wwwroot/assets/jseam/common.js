//
// Common
//
//<!====================    mensaje =============================!>

"use strict";

var c, CommonG = {

    settings: {
        directorySearch: $('#searchDir'),
        btnDirRefresh: $('#btnDirRefresh'),
        panorama: ''
    },

    Message: function (message) {

        if (message !== "" || message !== undefined) {
            var msg = message.split('|');
            var typeMsg = msg[0];

            if (typeMsg === "ok") {
                toastr.success(msg[1], msg[2]);
            }
            else if (typeMsg === 'su') {
                toastr.success(msg[1], msg[2]);
            }
            else if (typeMsg === 'er') {
                toastr.error(msg[1], msg[2]);
            }
            else if (typeMsg === 'wn') {
                toastr.warning(msg[1], msg[2]);
            }
            else if (typeMsg === 'in') {
                toastr.info(msg[1], msg[2]);
            }
        }              
    },

    MessageNifty: function (message) {
        if (message !== "" || message !== undefined) {
            var msg = message.split('|');
            var typeMsg = msg[0];
            var xtimer = 3000;

            var ctype = "";

            if (typeMsg === "ok") {              
                ctype = 'mint';
            }
            else if (typeMsg === 'su') {
                ctype = 'success';
            }
            else if (typeMsg === 'er') {
                ctype = 'pink';
            }
            else if (typeMsg === 'wn') {
                ctype = 'warning';               
            }
            else if (typeMsg === 'in') {
                ctype = 'info';                
            }
            if (msg[3] !== undefined)
                xtimer = msg[3];

            $.niftyNoty({
                type: ctype,
                title: msg[2],
                message: msg[1],
                container: 'floating',
                timer: xtimer
            });

        }
        CommonG.Loading();

    },
    

    //sample
    //Confirmar("Are you sure?", "Are you sure?", "warning", temp, 'xxxxx');
    Confirmarv1: function (vtitle, vtext, vtype, vfunction, pmt) {
        swal({
            title: vtitle,
            text: vtext,
            type: vtype,
            showCancelButton: true,
            confirmButtonColor: "#1D8177",
            confirmButtonText: "Si, Proceder!",
            cancelButtonText: "Cancelar!",
            closeOnConfirm: true,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    swal("Cargando!", "Actualizando", "success");
                    eval(vfunction);
                } else {
                    swal("Proceso cancelado", "Cancelado", "error");
                }
            }
        );
        return false;
    },


    GetString: function (data, pos) {
        const lista = data.split("~"); //array rows
        const msg = lista[pos];
        return msg;
    },

    CrearMatriz: function (data, msgHeader) {

        let matriz = [];

        if (data === undefined || data === '') {
            return matriz;
        }

        var lista = data.split("~"); //array rows

        //si contiene msg como header                 
        if (msgHeader || msgHeader === undefined) {
            lista.splice(0, 1);  //quitar msg
        }

        var nrows = lista.length;


        if (nrows === 0) {
            return matriz;
        }

        let cols; //array
        let ncols;
        let c;


        //alert('crearMatriz nrows:' + nrows);

        for (let i = 0; i < nrows; i++) {
            matriz[i] = [];
            cols = lista[i].split("|"); //array
            ncols = cols.length;
            for (let j = 0; j < ncols; j++) {
                if (cols[j] !== '') {
                    if (isNaN(cols[j])) {
                        matriz[i][j] = cols[j];
                    }
                    else if (cols[j] !== undefined) {
                        matriz[i][j] = cols[j];
                    }
                    else {
                        matriz[i][j] = cols[j] * 1; //numeric
                    }
                }
                else
                    matriz[i][j] = '';
            }
        }

        return matriz;
    },

    formatDec: function (x) {
        if (isNaN(x)) return "";
        const n = x.toString().split('.');

        if (n.length > 1) {
            return n[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",") + (n.length > 1 ? "." + n[1] : "");
        }
        else {
            return x.toString() + ".00";
        }
    },

    RequestAjax: function (url, type, callbackfunction, frmdata, pmts, okmsg, ermsg) {

        http(url
            , type
            , frmdata
            , true
            , callbackfunction            
        );

        //$.ajax({
        //    type: type,
        //    url: url,
        //    data: frmdata,
        //    success: function (result) {
        //        if (callbackfunction !== '')
        //            eval(callbackfunction);

        //        if (okmsg !== '')
        //            var pm = CommonG.Message(okmsg);
        //        else
        //            var pm2 = CommonG.Message(result);
        //    },
        //    fail: function (result) {
        //        if (ermsg !== '')
        //            var pme = CommonG.Message(ermsg);
        //        else
        //            var pme2 = CommonG.Message(result);
        //    }
        //});
    },

    RequestAjaxSelect: function (url, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange) {
        
        http(
            url
            , 'POST'
            , vdata
            , true
            , 'CommonG.populateSelect'
            , 'RP'
            , { proceso, divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange }
        );


        //let dataSelect = [];

        //if (vdata === '') {
        
        //    $.ajax({
        //        type: 'POST',
        //        url: url,
        //        async: false,
        //        success: function (result) {
        //            if (result !== '') {
        //                dataSelect = CommonG.CrearMatriz(result, false);
        //                let px = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange);
        //            }
        //            else {
        //                let px2 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
        //            }
        //        },
        //        fail: function (result) {
        //            let px3 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
        //        }
        //    });
        //} else {


        //    $.ajax({
        //        type: 'POST',
        //        url: url,
        //        data: vdata,
        //        async: false,
        //        success: function (result) {
        //            if (result !== '') {
        //                dataSelect = CommonG.CrearMatriz(result, false);
        //                let px = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange);
        //            }
        //            else {
        //                let px2 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
        //            }
        //        },
        //        fail: function (result) {
        //            let px3 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
        //        }
        //    });
        //}
    },

    RequestAjaxSelectPmt: function (url, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial) {

        let cbkProc = 'CommonG.populateSelect';
        if (vhType === 'H')
            cbkProc += 'H';

        http(
            url
            , 'POST'
            , vdata
            , true
            , cbkProc
            , 'RP'
            , { divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial }
            , false           
        );
        
        //let dataSelect = [];
        //$.ajax({
        //    type: 'POST',
        //    url: url,
        //    data: vdata,
        //    async: false,
        //    success: function (result) {
        //        if (result !== '') {
        //            //var px = CommonG.Message('ok|Datos de ' + descdata + ' leidas|' + proceso);
        //            dataSelect = CommonG.CrearMatriz(result, false);
        //            if (vhType !== null && vhType === 'H') {
        //                let pH = CommonG.populateSelectH(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2);
        //            }
        //            else {
        //                let pV = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial);
        //            }

        //        }
        //        else {
        //            var px2 = CommonG.Message('wn|Datos de ' + descdata + ' NO leidas|' + proceso);
        //        }
        //    },
        //    fail: function (result) {
        //        let px3 = CommonG.Message('wn|Datos de ' + descdata + ' NO leidas|' + proceso);
        //    }
        //});
    },    



    HttpSelect2: function (url, bAsync, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial) {

        let cbkProc = 'CommonG.populateSelect';
        if (vhType === 'H')
            cbkProc += 'H';

        http(
            url
            , 'POST'
            , vdata
            , bAsync
            , cbkProc
            , 'RP'
            , { divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial }
            , false
        );

    },  


    HttpSelect2J: function (url, bAsync, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial) {

        let cbkProc = 'CommonG.populateSelectJ';
        if (vhType === 'H')
            cbkProc += 'H';

        http(
            url
            , 'POST'
            , vdata
            , bAsync
            , cbkProc
            , 'RP'
            , { divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2, numOptions, bDisable, vInicial }
            , false
        );

    },  


    populateSelect: function (result, pmts) {  //proceso, divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial) {
            
        if (result === undefined) {
            let px = CommonG.Message('er|' + pmts.divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (result.length === 0) {
            let px2 = CommonG.Message('er|' + pmts.divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        let adata = [];
        adata = CommonG.CrearMatriz(result, false);

        $('#' + pmts.divId).html = '';
        let hSelect = "";

        if (pmts.labelText !== '')
            hSelect = "<label class='form-label form-label-sm'>" + pmts.labelText + "</label>";

        hSelect += "<select id='" + pmts.selectId + "' class='select2 form-control form-control-sm' data-size='15' data-allow-clear='true'";

        if (pmts.bmultiple) {
            hSelect += "multiple";
        }

        if (pmts.bwidth !== undefined && pmts.bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (pmts.onchange !== undefined && pmts.onchange !== '') {
            hSelect += " onchange = '" + pmts.onchange + "' ";
        }

        hSelect += '>'; //temp 041918 <option></option>';

        let rows = adata.length;
        const cols = adata[0].length;

        if (pmts.numOptions !== null && pmts.numOptions !== undefined && pmts.numOptions !== "")
            rows = pmts.numOptions;

        //primer grupo
        let group = "";
        let opengroup = false;

        for (let i = 0; i < rows; i++) {

            if (group !== adata[i][1] && pmts.bgrupo) {
                group = adata[i][1];
                if (opengroup)
                    hSelect += '</optgroup>';
                hSelect += '<optgroup label="' + group + '"></optgroup>';
                opengroup = true;
            }

            if (pmts.oInicial !== undefined && pmts.oInicial !== "" && i === 0 ) {
                hSelect += "<option value='" + pmts.oInicial.value + "'>" + pmts.oInicial.display + "</option>";
            }

            if (adata[i][2] === '0' || pmts.oInicial === "" || pmts.oInicial === undefined)
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";
            else
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";

        }
        if (opengroup)
            hSelect += '</optgroup>';

        hSelect += "</select>";

        $('#' + pmts.divId).html(hSelect);

        let bDisable = false;
        if (pmts.bDisable !== null && pmts.bDisable !== undefined && pmts.bDisable !== '')
            bDisable = pmts.bDisable;

        if (pmts.modalId !== '') {
            $('#' + pmts.selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                dropdownParent: $('#' + pmts.modalId),
                disabled: bDisable,
                allowClear: true,
                width: '100%'
            });
        }
        else {
            $('#' + pmts.selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                disabled: bDisable,
                allowClear: true,
                width: '100%'
            });
        }

        if (pmts.vInicial !== null && pmts.vInicial !== undefined && pmts.vInicial !== "") {
            $('#' + pmts.selectId).val(pmts.vInicial);
            $('#' + pmts.selectId).trigger('change');
        }



    },

    populateSelectJ: function (data, pmts) {  //proceso, divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial) {

        if (data === undefined) {
            let px = CommonG.Message('er|' + pmts.divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (data.length === 0) {
            let px2 = CommonG.Message('er|' + pmts.divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        //let adata = [];
        //adata = CommonG.CrearMatriz(result, false);

        $('#' + pmts.divId).html = '';
        let hSelect = "";

        if (pmts.labelText !== '')
            hSelect = `<label class='form-label form-label-sm'>${pmts.labelText}</label>`;

        hSelect += `<select id='${pmts.selectId}' class='select2 form-control form-control-sm' data-size='15' data-allow-clear='true'`;

        if (pmts.bmultiple) {
            hSelect += "multiple";
        }

        if (pmts.bwidth !== undefined && pmts.bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (pmts.onchange !== undefined && pmts.onchange !== '') {
            hSelect += ` onchange = '${pmts.onchange}' `;
        }

        hSelect += '>'; //temp 041918 <option></option>';

        //let rows = adata.length;
        //const cols = adata[0].length;

        if (pmts.numOptions !== null && pmts.numOptions !== undefined && pmts.numOptions !== "")
            rows = pmts.numOptions;

        //primer grupo
        let group = "";
        let opengroup = false;

        //for (let i = 0; i < rows; i++) {
        data.forEach(function (itm){
            if (group !== itm.group.name && pmts.bgrupo) {
                group = itm.group.name;
                if (opengroup)
                    hSelect += '</optgroup>';
                hSelect += `<optgroup label="${itm.group.name}"></optgroup>`;
                opengroup = true;
            }

            if (pmts.oInicial !== undefined && pmts.oInicial !== "" && i === 0) {
                hSelect += `<option value='${pmts.oInicial.value}'>${pmts.oInicial.display}</option>`;
            }

            if (itm.value === '0' || pmts.oInicial === "" || pmts.oInicial === undefined)
                hSelect += `<option value='${itm.value}'>${itm.text}</option>`;
            else
                hSelect += `<option value='${itm.value}'>${itm.text}</option>`;

        });
        if (opengroup)
            hSelect += '</optgroup>';

        hSelect += "</select>";

        $('#' + pmts.divId).html(hSelect);

        let bDisable = false;
        if (pmts.bDisable !== null && pmts.bDisable !== undefined && pmts.bDisable !== '')
            bDisable = pmts.bDisable;

        if (pmts.modalId !== '') {
            $('#' + pmts.selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                dropdownParent: $('#' + pmts.modalId),
                disabled: bDisable,
                allowClear: true,
                width: '100%'
            });
        }
        else {
            $('#' + pmts.selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                disabled: bDisable,
                allowClear: true,
                width: '100%'
            });
        }

        if (pmts.vInicial !== null && pmts.vInicial !== undefined && pmts.vInicial !== "") {
            $('#' + pmts.selectId).val(pmts.vInicial);
            $('#' + pmts.selectId).trigger('change');
        }

    },
    
    populateSelectH: function (result, pmts) { // { divId, labelText, selectId, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2 } ) {              

        if (result === undefined) {
            let px = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (result.length === 0) {
            let px2 = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        let adata = [];
        adata = CommonG.CrearMatriz(result, false);

        
        $('#' + pmts.divId).html = '';
        let hSelect = "";

        hSelect = '<div class="form-group row">';
        hSelect += `  <label class="pt-2 form-label form-label-sm col-sm-${pmts.col1}">` + pmts.labelText + "</label>";
        hSelect += `  <div class="col-sm-${pmts.col2}">`
        hSelect += "     <select id='" + pmts.selectId + "' class='select2  form-control form-control-sm' data-size='15' data-allow-clear='true' ";

        if (pmts.bmultiple) {
            hSelect += " multiple ";
        }

        if (pmts.bwidth !== undefined && pmts.bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (pmts.onchange !== undefined && pmts.onchange !== '') {
            hSelect += " onchange = '" + pmts.onchange + "' ";
        }

        hSelect += '>'; //temp 041918 <option></option>';

    
        let rows = adata.length;
        const cols = adata[0].length;

        if (pmts.numOptions !== null && pmts.numOptions !== undefined && pmts.numOptions !== "")
            rows = pmts.numOptions;

        //primer grupo
        let group = "";
        let opengroup = false;

        for (let i = 0; i < rows; i++) {

            if (group !== adata[i][1] && pmts.bgrupo) {
                group = adata[i][1];
                if (opengroup)
                    hSelect += '</optgroup>';
                hSelect += '<optgroup label="' + group + '"></optgroup>';
                opengroup = true;
            }

            if (pmts.oInicial !== undefined && pmts.oInicial !== '' && i === 0) {
                hSelect += "<option value='" + pmts.oInicial.value + "' selected>" + pmts.oInicial.display + "</option>";
            }

            if (adata[i][2] === '0' || pmts.oInicial || "" && pmts.oInicial || undefined)
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";
            else
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";

        }
        if (opengroup)
            hSelect += '</optgroup>';

        hSelect += "    </select>";
        hSelect += "</div>";

        $('#' + pmts.divId).html(hSelect);

        let bDisable = false;
        if (pmts.bDisable !== null && pmts.bDisable !== undefined && pmts.bDisable !== "")
            bDisable = pmts.bDisable;

        if (pmts.modalId !== '') {
            $('#' + selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                dropdownParent: $('#' + pmts.modalId),
                disabled: bDisable

            });
        }
        else {
            $('#' + pmts.selectId).select2({
                placeholder: 'Select ' + pmts.labelText,
                disabled: bDisable
            });
        }

        if (pmts.vInicial !== null && pmts.vInicial !== undefined && pmts.vInicial !== "") {
            $('#' + pmts.selectId).val(pmts.vInicial);
            $('#' + pmts.selectId).trigger('change');
        }


    },

    //usar result, pmts
    populateSelectJson: function (divId, labelText, selectId, jdata, bgrupo, modalId, bmultiple, bwidth, onchange) {

        if (jdata === undefined) {
            let px = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (jdata.length === 0) {
            let px2 = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        $('#' + divId).html = '';

        let hSelect = "<label class='form-label form-label-sm'>" + labelText + "</label>";
        hSelect += "<select id='" + selectId + "' class='select2 form-control form-control-sm' data-size='15' data-allow-clear='true'";

        if (bmultiple) {
            hSelect += "multiple";
        }

        if (bwidth !== undefined && bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (onchange !== undefined && onchange !== '') {
            hSelect += " onchange = '" + onchange + "' ";
        }

        hSelect += '>'; //temp 041918 <option></option>';


        jdata.forEach(function (item) {

            if (item.value === '0')
                hSelect += "<option value='" + item.value + "' selected>" + item.display + "</option>";
            else
                hSelect += "<option value='" + item.value + "'>" + item.display + "</option>";

        });

        hSelect += "</select>";

        $('#' + divId).html(hSelect);
        $('#' + divId).select2();
    },
    
    AttributeValue: function (attrbs, attr) {
        attrbs = attrbs.replace(/"/g, '^');
        let len = attr.length;
        let ini = attrbs.indexOf(attr);
        let next = ini + len + 2;
        let fin = attrbs.indexOf('^', next);
        let vlen = fin - next;
        let xvalue = attrbs.substring(next, fin);

        return xvalue;

    },

    //DataTables

    UpdateRowTable: function (tableId, valueid, colid, colText, colpos) {
        //SAMPLE
        //common.UpdateRowTable(ProdG.settingsDesp.id, profileid, colid 5, xtitulo, 4); 
        const table = $("#" + tableId).DataTable();
        const rowId = $("#" + tableId).dataTable().fnFindCellRowIndexes(valueid, colid);

        table.cell(rowId, colpos)
            .data(colText)
            .draw(false);

    },

    AddRowTable: function (tableId, aColName, aColValue) {

        const table = $("#" + tableId).DataTable();
        table.row.add(aColValue).draw();

    },
         
    /// SELECT

    formatDate: function (date, format, utc) {
        constMMMM = ["\x00", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
        constMMM = ["\x01", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        constdddd = ["\x02", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
        constddd = ["\x03", "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

        function ii(i, len) {
            let s = i + "";
            len = len || 2;
            while (s.length < len) s = "0" + s;
            return s;
        }

        let y = utc ? date.getUTCFullYear() : date.getFullYear();
        format = format.replace(/(^|[^\\])yyyy+/g, "$1" + y);
        format = format.replace(/(^|[^\\])yy/g, "$1" + y.toString().substr(2, 2));
        format = format.replace(/(^|[^\\])y/g, "$1" + y);

        let M = (utc ? date.getUTCMonth() : date.getMonth()) + 1;
        format = format.replace(/(^|[^\\])MMMM+/g, "$1" + MMMM[0]);
        format = format.replace(/(^|[^\\])MMM/g, "$1" + MMM[0]);
        format = format.replace(/(^|[^\\])MM/g, "$1" + ii(M));
        format = format.replace(/(^|[^\\])M/g, "$1" + M);

        let d = utc ? date.getUTCDate() : date.getDate();
        format = format.replace(/(^|[^\\])dddd+/g, "$1" + dddd[0]);
        format = format.replace(/(^|[^\\])ddd/g, "$1" + ddd[0]);
        format = format.replace(/(^|[^\\])dd/g, "$1" + ii(d));
        format = format.replace(/(^|[^\\])d/g, "$1" + d);

        let H = utc ? date.getUTCHours() : date.getHours();
        format = format.replace(/(^|[^\\])HH+/g, "$1" + ii(H));
        format = format.replace(/(^|[^\\])H/g, "$1" + H);

        let h = H > 12 ? H - 12 : H === 0 ? 12 : H;
        format = format.replace(/(^|[^\\])hh+/g, "$1" + ii(h));
        format = format.replace(/(^|[^\\])h/g, "$1" + h);

        let m = utc ? date.getUTCMinutes() : date.getMinutes();
        format = format.replace(/(^|[^\\])mm+/g, "$1" + ii(m));
        format = format.replace(/(^|[^\\])m/g, "$1" + m);

        let s = utc ? date.getUTCSeconds() : date.getSeconds();
        format = format.replace(/(^|[^\\])ss+/g, "$1" + ii(s));
        format = format.replace(/(^|[^\\])s/g, "$1" + s);

        let f = utc ? date.getUTCMilliseconds() : date.getMilliseconds();
        format = format.replace(/(^|[^\\])fff+/g, "$1" + ii(f, 3));
        f = Math.round(f / 10);
        format = format.replace(/(^|[^\\])ff/g, "$1" + ii(f));
        f = Math.round(f / 10);
        format = format.replace(/(^|[^\\])f/g, "$1" + f);

        let T = H < 12 ? "AM" : "PM";
        format = format.replace(/(^|[^\\])TT+/g, "$1" + T);
        format = format.replace(/(^|[^\\])T/g, "$1" + T.charAt(0));

        let t = T.toLowerCase();
        format = format.replace(/(^|[^\\])tt+/g, "$1" + t);
        format = format.replace(/(^|[^\\])t/g, "$1" + t.charAt(0));

        let tz = -date.getTimezoneOffset();
        let K = utc || !tz ? "Z" : tz > 0 ? "+" : "-";
        if (!utc) {
            tz = Math.abs(tz);
            let tzHrs = Math.floor(tz / 60);
            let tzMin = tz % 60;
            K += ii(tzHrs) + ":" + ii(tzMin);
        }
        format = format.replace(/(^|[^\\])K/g, "$1" + K);

        let day = (utc ? date.getUTCDay() : date.getDay()) + 1;
        format = format.replace(new RegExp(dddd[0], "g"), dddd[day]);
        format = format.replace(new RegExp(ddd[0], "g"), ddd[day]);

        format = format.replace(new RegExp(MMMM[0], "g"), MMMM[M]);
        format = format.replace(new RegExp(MMM[0], "g"), MMM[M]);

        format = format.replace(/\\(.)/g, "$1");

        return format;
    },

    
    GetGeoCoord: function (address, vLat, vLong) {  
        let geocoder = new google.maps.Geocoder();
        geocoder.geocode({
                'address': address
            },

            function (results, status) {

                var lat = results[0].geometry.location.lat();
                var lng = results[0].geometry.location.lng();

                //here send to server to save it...WITH PARAMETES

                //var frm = new FormData();
                //frm.append("locationID", locationID);
                //frm.append("latitude", lat);
                //frm.append("longitude", lng);

                //var url = "/Utilities/SaveGeoCoord";
                //enviarServidor(url, "post", ShowResultSave, frm);
                document.getElementById(vLat).value = lat;
                document.getElementById(vLong).value = lng;

            });
                      
     },

    GetCoord: function(vAddress, vCity, vZip, vLatitude, vLongitude) {
        //var oID = document.getElementById("divID");      
        let oAddress = document.getElementById(vAddress);
        let oCity = document.getElementById(vCity);
        let oZipCode = document.getElementById(vZip);

        //var oLatitude = document.getElementById(vLatitude);
        //var oLongitude = document.getElementById(vLongitude);

        if (oAddress.value === '' ||
            oCity.value === '' ||
            oZipCode.value === ''
        ) {
            Message('w', 'Verify Data', 'Locations');
            return;
        }

        let sAddress = oAddress.value + ' ' + oCity.value + ' ' + 'CA' + ' ' + oZipCode.value;

        this.GetGeoCoord(sAddress, vLatitude, vLongitude);

    },

    GetCoordA: function (aData) {
        //var oID = document.getElementById("divID");      
        let oAddress = document.getElementById(aData[0]);
        let oCity = document.getElementById(aData[1]);
        let oZipCode = document.getElementById(aData[2]);

        //let oLatitude = document.getElementById(vLatitude);
        //let oLongitude = document.getElementById(vLongitude);

        if (oAddress.value === '' ||
            oCity.value === '' ||
            oZipCode.value === ''
        ) {
            Message('w', 'Verify Data', 'Locations');
            return;
        }

        let sAddress = oAddress.value + ' ' + oCity.value + ' ' + 'CA' + ' ' + oZipCode.value;

        this.GetGeoCoord(sAddress, aData[3], aData[4]);

    },
    

    ExtractFromMatriz: function (data, col) {

        let nRow = data.length;
        let rdata = [];
        for (let i = 0; i < nRow; i++) {
            rdata.push(data[i][col]);
        }             
        return rdata;
    },

    StaticsFromArray: function (data) {

        let rdata = {
            count: 0,
            sum: 0,
            avg: 0,
            min: 0,
            max: 0
        };

        rdata.count = data.length;

        for (let i = 0; i < rdata.count; i++) {

            if (i === 0) {
                rdata.max = data[i];
                rdata.min = data[i];
            }

            if (data[i] > rdata.max) {
                rdata.max = data[i];
            } 
            if (data[i] < rdata.min) {
                rdata.min = data[i];
            }            
            rdata.sum += data[i] * 1;
        }

        if (rdata.count > 0)
            rdata.avg = rdata.sum / rdata.count;                

        return rdata;
    }, 
    
    MaxFromArray: function (data) {
        let nRow = data.length;
        let avg = 0;
        let sum = 0;

        for (let i = 0; i < nRow; i++) {
            sum += data[i];
        }
        avg = sum / nrow;
        return avg;
    },   

    //Call for controls...

    MakeMap: function (mapId, vlat, vlng, vTipo, vtitle) {
        // Options for Google map
        // More info see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions

        let types = "RNBP-";
        let iType = "-";
        let posType = 0;

        let iconBase = 'http://intranet.elaviso.com/dist/';
        let icons = [
            'racksWire.png',
            'racksStreet.png',
            'box.png',
            'racksprivate.png',
            'other.png'
        ];

        iType = vTipo === 'NP' ? 'N' : vTipo;
        posType = types.indexOf(iType);
        

        let myCenter = new google.maps.LatLng(vlat, vlng);
        let mapCanvas = document.getElementById(mapId);
        let mapOptions = {
            center: myCenter,
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.ROADMAP 
        };

        let map = new google.maps.Map(mapCanvas, mapOptions);
        let marker = new google.maps.Marker({
            position: myCenter,
            icon: iconBase + icons[posType]

        });
        marker.setMap(map);
        let infowindow = new google.maps.InfoWindow({
            content: vtitle
        });
        infowindow.open(map, marker);
    },


    MakeMapPOnly: function (mapId, vlat, vlng, vTipo, vtitle) {
        // Options for Google map
        // More info see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions

        let types = "RNBP-";
        let iType = "-";
        let posType = 0;

        let iconBase = 'http://intranet.elaviso.com/dist/';
        let icons = [
            'racksWire.png',
            'racksStreet.png',
            'box.png',
            'racksprivate.png',
            'other.png'
        ];

        iType = vTipo === 'NP' ? 'N' : vTipo;
        posType = types.indexOf(iType);


        let myCenter = new google.maps.LatLng(vlat, vlng);
        let mapCanvas = document.getElementById(mapId);
        let mapOptions = {
            position: myCenter,
            pov: { heading: 165, pitch: 0 },
            zoom: 1 //,
            //streetViewControl: true
            //mapTypeId: google.maps.MapTypeId.RO
        };

     
        //let map = new google.maps.Map(mapCanvas, mapOptions);
        let map = new google.maps.StreetViewPanorama(mapCanvas, mapOptions);
        let marker = new google.maps.Marker({
            position: myCenter,
            icon: iconBase + icons[posType],
            title: vtitle
        });             
       
        marker.setMap(map);
        let infowindow = new google.maps.InfoWindow({
            content: vtitle
        });

        //let panorama = map.getStreetView();
        ////panorama.setPosition(astorPlace);
        //panorama.setPov(/** @type {google.maps.StreetViewPov} */({
        //    heading: 265,
        //    pitch: 0
        //}));

        infowindow.open(map, marker);

    },


    MakeMapP: function (mapId, vlat, vlng, vTipo, vtitle) {
        // Options for Google map
        // More info see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions

        let types = "RNBP-";
        let iType = "-";
        let posType = 0;

        let iconBase = 'http://intranet.elaviso.com/dist/';
        let icons = [
            'racksWire.png',
            'racksStreet.png',
            'box.png',
            'racksprivate.png',
            'other.png'
        ];

        iType = vTipo === 'NP' ? 'N' : vTipo;
        posType = types.indexOf(iType);


        let myCenter = new google.maps.LatLng(vlat, vlng);
        let mapCanvas = document.getElementById(mapId);
        let mapOptions = {
            center: myCenter,
            //position: myCenter,
            //pov: { heading: 165, pitch: 0 },
            zoom: 18,
            streetViewControl: false,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

             
        let map = new google.maps.Map(mapCanvas, mapOptions);
        let marker = new google.maps.Marker({
            position: myCenter,
            icon: iconBase + icons[posType],
            title: vtitle
        });

        marker.setMap(map);
        let infowindow = new google.maps.InfoWindow({
            content: vtitle
        });

        CommonG.settings.panorama = map.getStreetView();
        CommonG.settings.panorama.setPosition(myCenter);
        //** @type {google.maps.StreetViewPov} */
        CommonG.settings.panorama.setPov({
            heading: 90,
            pitch: 0
        });

        infowindow.open(map, marker);

    },
    
    toggleStreetView: function (id) {
               
       
        let obj = document.getElementById(id);
               
        
        let toggle = CommonG.settings.panorama.getVisible();
        if (toggle === false) {       
            obj.innerHTML= '<i class="fa fa-eye"></i> Ver Mapa';
            CommonG.settings.panorama.setVisible(true);

         } else {
            obj.innerHTML = '<i class="fa fa-eye"></i> Ver Calle';
            CommonG.settings.panorama.setVisible(false);

         }
    },

    number_format : function(number, decimals, dec_point, thousands_sep) {
        // http://kevin.vanzonneveld.net
        // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
        // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
        // +     bugfix by: Michael White (http://getsprink.com)
        // +     bugfix by: Benjamin Lupton
        // +     bugfix by: Allan Jensen (http://www.winternet.no)
        // +    revised by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
        // +     bugfix by: Howard Yeend
        // +    revised by: Luke Smith (http://lucassmith.name)
        // +     bugfix by: Diogo Resende
        // +     bugfix by: Rival
        // +      input by: Kheang Hok Chin (http://www.distantia.ca/)
        // +   improved by: davook
        // +   improved by: Brett Zamir (http://brett-zamir.me)
        // +      input by: Jay Klehr
        // +   improved by: Brett Zamir (http://brett-zamir.me)
        // +      input by: Amir Habibi (http://www.residence-mixte.com/)
        // +     bugfix by: Brett Zamir (http://brett-zamir.me)
        // +   improved by: Theriault
        // +   improved by: Drew Noakes
        // *     example 1: number_format(1234.56);
        // *     returns 1: '1,235'
        // *     example 2: number_format(1234.56, 2, ',', ' ');
        // *     returns 2: '1 234,56'
        // *     example 3: number_format(1234.5678, 2, '.', '');
        // *     returns 3: '1234.57'
        // *     example 4: number_format(67, 2, ',', '.');
        // *     returns 4: '67,00'
        // *     example 5: number_format(1000);
        // *     returns 5: '1,000'
        // *     example 6: number_format(67.311, 2);
        // *     returns 6: '67.31'
        // *     example 7: number_format(1000.55, 1);
        // *     returns 7: '1,000.6'
        // *     example 8: number_format(67000, 5, ',', '.');
        // *     returns 8: '67.000,00000'
        // *     example 9: number_format(0.9, 0);
        // *     returns 9: '1'
        // *    example 10: number_format('1.20', 2);
        // *    returns 10: '1.20'
        // *    example 11: number_format('1.20', 4);
        // *    returns 11: '1.2000'
        // *    example 12: number_format('1.2000', 3);
        // *    returns 12: '1.200'
        let n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        toFixedFix = function (n, prec) {
                // Fix for IE parseFloat(0.55).toFixed(0) = 0;
                let k = Math.pow(10, prec);
                return Math.round(n * k) / k;
            },
            s = (prec ? toFixedFix(n, prec) : Math.round(n)).toString().split('.');
            if (s[0].length > 3) {
                s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
            }
            if ((s[1] || '').length < prec) {
                s[1] = s[1] || '';
                s[1] += new Array(prec - s[1].length + 1).join('0');
            }
            return s.join(dec);
    },



    PrintContent: function(el) {
        let restorepage = $('body').html();
        let printcontent = $('#' + el).clone();
        $('body').empty().html(printcontent);
        window.print();
        $('body').html(restorepage);
    },

    SelectTr: function (tableId) {
        $('#' + tableId).on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            }
            else {
                $('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
        });
    },
          
        
    //PROMISES 

    promProcedure: function (xProcedure, aParam) {
        return new Promise(function (resolve, reject) {
            let p = xProcedure(aParam);
        });
    },

    promLoading: function (type) {
        return new Promise(function (resolve, reject) {
            $('#iboxLoading').toggleClass('hidden');            
        });   
    },      
    
    promAjax: function (object) {
        return new Promise(function (resolve, reject){
            $.ajax(object).done(resolve).fail(reject);
        })
    },

    promError: function (err) {
        return new Promise(CommonG.Message(err))  // err= 'er| Datos No encontrados| Distribucion');
    },

    promiseAll: function (ajaxObject, fnCallBack) {
        Promise.all([
            CommonG.promLoading(),
            CommonG.promAjax(ajaxObject).then(function resolve(data) {
                $.when(fnCallBack(data)).done(CommonG.promLoading);
            }, function reject(reason) {
                CommonG.Message('er| Error en lectura: ' + reason + '|Tmk Returns');
            })
        ]);
    },           
    
    ReadingTable: function (tableId) {
        let table = $('#' + tableId).DataTable();
        let aData = [];
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            let data = this.data();
            if ($('#neword_' + data[0]).val() !== '0' && $('#neword_' + data[0]).val() !== '' && $('#neword_' + data[0]).val() !== undefined) {
                aData.push([data[0], $('#neword_' + data[0]).val(), data[2], data[2]]);
            }
            //alert(data[0] + ' ' + $('#neword_' + data[0]).val());
            // ... do something with data(), or this.node(), etc
        });
        return aData;
    },

    ReadingLastRowTable: function (tableId) {

        let table = $('#' + tableId).DataTable();
        let aData = [];               
        let lrow = table.row(':last', { order: 'applied' }).data();         
        return lrow;
    },


    init: async function () {

        
        c = this.settings;      

        let data = await fetchproc('/common/GetPhoneProveedoresJ');

        CreateSelect2(data, 'PhProv', 'divPhProv', 'Phone Providers', 'Select Provider', false, 'modalProfile');

        this.bindUIActions();

    },


    RequestAjaxSelectVA: function (url, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange) {

        let dataSelect = [];

        if (vdata === '') {


            $.ajax({
                type: 'POST',
                url: url,
                async: false,
                success: function (result) {
                    if (result !== '') {
                        dataSelect = CommonG.CrearMatriz(result, false);
                        let px = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange);
                    }
                    else {
                        let px2 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
                    }
                },
                fail: function (result) {
                    let px3 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
                }
            });
        } else {


            $.ajax({
                type: 'POST',
                url: url,
                data: vdata,
                async: false,
                success: function (result) {
                    if (result !== '') {
                        dataSelect = CommonG.CrearMatriz(result, false);
                        let px = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange);
                    }
                    else {
                        let px2 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
                    }
                },
                fail: function (result) {
                    let px3 = CommonG.Message('er|Datos de ' + descdata + ' NO leidas|' + proceso);
                }
            });
        }
    },

    RequestAjaxSelectPmtVA: function (url, descdata, proceso, divId, labelText, selectId, vdata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2) {
        let dataSelect = [];
        $.ajax({
            type: 'POST',
            url: url,
            data: vdata,
            async: false,
            success: function (result) {
                if (result !== '') {
                    //var px = CommonG.Message('ok|Datos de ' + descdata + ' leidas|' + proceso);
                    dataSelect = CommonG.CrearMatriz(result, false);
                    if (vhType !== null && vhType === 'H') {
                        let pH = CommonG.populateSelectH(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2);
                    }
                    else {
                        let pV = CommonG.populateSelect(divId, labelText, selectId, dataSelect, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial);
                    }

                }
                else {
                    var px2 = CommonG.Message('wn|Datos de ' + descdata + ' NO leidas|' + proceso);
                }
            },
            fail: function (result) {
                let px3 = CommonG.Message('wn|Datos de ' + descdata + ' NO leidas|' + proceso);
            }
        });
    },



    populateSelectVA: function (divId, labelText, selectId, adata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial) {



        if (adata === undefined) {
            let px = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (adata.length === 0) {
            let px2 = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        $('#' + divId).html = '';
        let hSelect = "";

        if (labelText !== '')
            hSelect = "<label class='form-label form-label-sm'>" + labelText + "</label>";

        hSelect += "<select id='" + selectId + "' class='select2 form-control form-control-sm' data-size='15' data-allow-clear='true'";

        if (bmultiple) {
            hSelect += "multiple";
        }

        if (bwidth !== undefined && bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (onchange !== undefined && onchange !== '') {
            hSelect += " onchange = '" + onchange + "' ";
        }

        hSelect += '>'; //temp 041918 <option></option>';

        var rows = adata.length;
        var cols = adata[0].length;

        //primer grupo
        var group = "";
        var opengroup = false;

        for (var i = 0; i < rows; i++) {

            if (group !== adata[i][1] && bgrupo) {
                group = adata[i][1];
                if (opengroup)
                    hSelect += '</optgroup>';
                hSelect += '<optgroup label="' + group + '"></optgroup>';
                opengroup = true;
            }

            if (oInicial !== undefined && i === 0) {
                hSelect += "<option value='" + oInicial.value + "'>" + oInicial.display + "</option>";
            }

            if (adata[i][2] === '0' && oInicial === undefined)
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";
            else
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";

        }
        if (opengroup)
            hSelect += '</optgroup>';

        hSelect += "</select>";

        $('#' + divId).html(hSelect);

        if (modalId !== '') {
            $('#' + selectId).select2({
                placeholder: 'Select ' + labelText,
                dropdownParent: $('#' + modalId)
            });
        }
        else {
            $('#' + selectId).select2({
                placeholder: 'Select ' + labelText
            });
        }
    },

    populateSelectHVA: function (divId, labelText, selectId, adata, bgrupo, modalId, bmultiple, bwidth, onchange, oInicial, vhType, col1, col2) {

        if (adata === undefined) {
            let px = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        if (adata.length === 0) {
            let px2 = CommonG.Message('er|' + divId + ": " + " datos no encotrados| Users Mgr");
            return;
        }

        $('#' + divId).html = '';
        let hSelect = "";

        hSelect = '<div class="form-group row">';
        hSelect += `  <label class="pt-3 form-label form-label-sm col-sm-${col1}">` + labelText + "</label>";
        hSelect += `  <div class="col-sm-${col2}">`
        hSelect += "     <select id='" + selectId + "' class='select2 form-control form-control-sm' data-size='15' data-allow-clear='true'";

        if (bmultiple) {
            hSelect += "multiple";
        }

        if (bwidth !== undefined && bwidth) {
            hSelect += " style='width:100%' ";
        }

        if (onchange !== undefined && onchange !== '') {
            hSelect += " onchange = '" + onchange + "' ";
        }

        hSelect += '>'; //temp 041918 <option></option>';

        var rows = adata.length;
        var cols = adata[0].length;

        //primer grupo
        var group = "";
        var opengroup = false;

        for (var i = 0; i < rows; i++) {

            if (group !== adata[i][1] && bgrupo) {
                group = adata[i][1];
                if (opengroup)
                    hSelect += '</optgroup>';
                hSelect += '<optgroup label="' + group + '"></optgroup>';
                opengroup = true;
            }

            if (oInicial !== undefined && oInicial !== '' && i === 0) {
                hSelect += "<option value='" + oInicial.value + "'>" + oInicial.display + "</option>";
            }

            if (adata[i][2] === '0' && oInicial === undefined)
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";
            else
                hSelect += "<option value='" + adata[i][4] + "'>" + adata[i][3] + "</option>";

        }
        if (opengroup)
            hSelect += '</optgroup>';

        hSelect += "    </select>";
        hSelect += "</div>";

        $('#' + divId).html(hSelect);

        if (modalId !== '') {
            $('#' + selectId).select2({
                placeholder: 'Select ' + labelText,
                dropdownParent: $('#' + modalId)
            }); 
        }
        else {
            $('#' + selectId).select2({
                placeholder: 'Select ' + labelText
            });
        }
    },


    


    UpdateSelectPicker: function (rdata, pmts = { elemId: '', valInit: 0, txtInit: 'Seleccionar' }) {

        var contenido = "";
          
        if (rdata !== '') {

            var data = CommonG.CrearMatriz(rdata, false);
            var nRows = data.length;

            contenido = '<option value="'+ pmts.valInit +'" selected>' + pmts.txtInit +'</option>';

            for (var i = 0; i < nRows; i++) {
                contenido += '<option value="' + data[i][4] + '">' + data[i][3] + '</option>';
            }
        }
        else {
            contenido = '<option value="' + pmts.valInit + '" selected>' + pmts.txtInit + '</option>';
        }

        $("#" + pmts.elemId)
            .html(contenido)
            .selectpicker('refresh');

    },

    UpdateSelectPickerJ: function (data, pmts = { elemId: '', valInit: 0, txtInit: 'Seleccionar' }) {

        var contenido = "";

        if (data !== '') {
                   
            var nRows = data.length;

            contenido = '<option value="' + pmts.valInit + '" selected>' + pmts.txtInit + '</option>';

            for (var i = 0; i < nRows; i++) {
                contenido += '<option value="' + data[i][4] + '">' + data[i][3] + '</option>';
            }
        }
        else {
            contenido = '<option value="' + pmts.valInit + '" selected>' + pmts.txtInit + '</option>';
        }

        $("#" + pmts.elemId)
            .html(contenido)
            .selectpicker('refresh');

    },

    /****************** PROFILE **************************/



    CUpdMyProfile: function () {

        Confirmar("Actualizar su información", "Seguro?", "warning", "let pu = CommonG.UpdMyProfile()");
        return false;
    },

    UpdMyProfile: async function () {         
        
        const datav = {
            "phone": $('#Phone').val(),
            "extension": $('#Extension').val(),
            "PhoneProviderId": $('#PhProv').val(),
            "twitter": $('#twitter').val(),
            "facebook": $('#facebook').val(),
            "instagram": $('#instagram').val(),
            "linkedIn": $('#linkedIn').val()
        };

        let formdata = jsonToFormData(datav)

        let data = await fetchproc('/admin/UpdateMyProfile', formdata);

        if (data.resp.response) {
            myProfile(data);
        }
        else {
            this.Message6(data.resp.message,'wn');
        }             


    },

    CUpdAuth: function () {

        Confirmar("Actualizar su información (password)", "Seguro?", "warning", "let pu = CommonG.UpdAuth()");
        return false;

    },

    UpdAuth: async function () {
        
        event.preventDefault();
        //cambiar por solo 
        const xnewPassw = $('#NewAuth').val();
        const xconfPassw = $('#ConfAuth').val();

        if (xnewPassw === undefined || xnewPassw.length < 4) {
            let px = CommonG.Message('wn| Cheque el nuevo Password (minimo 4 caracteres) | Admin');
            return;
        }

        if (xnewPassw !== xconfPassw) {
            let px = CommonG.Message('wn| La confirmación NO COINCIDE revisela | Admin');
            return;
        }

        const datav = {
            "profileId": $('#profileId').val(),
            "loginId": $('#loginId').val(),
            "newAuth": xnewPassw
        };

        let formdata = jsonToFormData(datav)

        let data = await fetchproc('/admin/UpdateAuth', formdata);

        if (data.response) {
            this.Message6('Password Updated');
        }
        else {
            this.Message6(data.message, 'wn');
        }
        

        //http('/common/UpdateAuth'
        //    , 'POST'
        //    , datav
        //    , true
        //    , ''
        //    , 'N'
        //    , ''
        //    , true
        //    , true
        //    , {
        //        typeMsg: 'ok',
        //        message: 'Actualizacion de Contraseña realizada correctamente',
        //        title: 'Admin'
        //    }
        //);
        
    },

    bindUIActions: async function () {

        c.directorySearch.on('keyup', function (e) {
            e.preventDefault();
            const valor = $('#searchDir').val();
            //alert('texto: ' + valor);
            $.post('/home/directory', { texto: valor }, function (data) {
                //alert(data);
                $('#directoryWidget').html(data);
            });
        });

        //my profile... 
        
        let data = await fetchproc('/admin/GetMyProfile');

        if (data != null) {
            myProfile(data);
        }
        else {
            Message6('Profile NOT FOUND', 'wn');
        }              
              
    }


};
