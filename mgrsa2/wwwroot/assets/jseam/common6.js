
//let msgErr = {
//    typeMsg: 'er',
//    message: 'Error en el Proceso',
//    title: 'Manager'
//};

//let msgOk = {
//    typeMsg: 'ok',
//    message: 'Proceso terminado correctamente',
//    title: 'Manager'
//};

//const settings = {
//    directorySearch: $('#searchDir'),
//    btnDirRefresh: $('#btnDirRefresh'),
//    panorama: ''
//}

function Mensaje(msg) {

    //const msg = JSON.parse(message);           
    //let typeMsg = msg.typeMsg;

    if (msg.typeMsg === "ok") {
        toastr.success(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'su') {
        toastr.success(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'er') {
        toastr.error(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'wn') {
        toastr.warning(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'in') {
        toastr.info(msg.message, msg.title);
    }

}



function Loading() {

    const loader = $('#iboxLoading');

    if (loader.hasClass('d-none'))
        loader.removeClass('d-none');
    else
        loader.addClass('d-none');
}

//***************
//
//
///   
function http(
    sUrl      //1
    , sType = 'POST'  //2
    , odata = null   //3
    , basync = true   //4
    , callBack = null //5 
    , pmtsType = null //6
    , pmts = null  //
    , bMensaje = true
    , bCloseLoader = true
    , oMsgOk = {
        typeMsg: 'ok',
        message: '', //Proceso terminado correctamente',
        title: 'Tareas e Ideas'
    },
    oMsgErr = {
        typeMsg: 'er',
        message: '', //'Error en el Proceso',
        title: 'Tareas e Ideas'
    }
) {

    const loader = document.getElementById('iboxLoading');

    loader.classList.remove('d-none');

    $.ajax({
        type: sType,
        url: sUrl,
        data: odata,
        async: basync
    })
        .done(function (result) {

            if (bCloseLoader)
                loader.classList.add('d-none');            

            if (bMensaje)
                Mensaje(oMsgOk);

            if (callBack !== null && callBack !== undefined && callBack !== '' ) {
                let fcallBack = '';
                //pmts dira si : null => pmts  = {"result": true/false, "values": "v1,v2.."}
                if (pmtsType === null || pmtsType === 'R') {
                    fcallBack = callBack + '(result)';
                }
                else if (pmtsType === 'N') {
                    fcallBack = callBack + '()';
                }
                else if (pmtsType === 'RP') {
                    fcallBack = callBack + '(result,pmts)';
                }
                else if (pmtsType === 'P') { //only pmts
                    fcallBack = callBack + '(pmts)';
                }
                else if (pmtsType === 'F') { //only pmts
                    fcallBack = callBack;
                }
                eval(fcallBack);
            }
           

        })
        .fail(function (result) {
            loader.classList.add('d-none');            
            Mensaje(oMsgErr);
        });

}


function httpFile(
    sUrl      //1
    , sType = 'POST'  //2
    , odata = null   //3
    , basync = true   //4
    , callBack = null //5 
    , pmtsType = null //6
    , pmts = null  //
    , bMensaje = true
    , bCloseLoader = true
    , oMsgOk = {
        typeMsg: 'ok',
        message: '', //Proceso terminado correctamente',
        title: 'Tareas e Ideas'
    },
    oMsgErr = {
        typeMsg: 'er',
        message: '', //'Error en el Proceso',
        title: 'Tareas e Ideas'
    }
) {

    const loader = document.getElementById('iboxLoading');

    loader.classList.remove('d-none');

    $.ajax({
        type: sType,
        url: sUrl,
        data: odata,
        async: basync,
        cache: false,
        contentType: false,
        enctype: 'multipart/form-data',
        processData: false
    })
        .done(function (result) {

            if (bCloseLoader)
                loader.classList.add('d-none');

            if (bMensaje)
                Mensaje(oMsgOk);

            if (callBack !== null && callBack !== undefined && callBack !== '') {
                let fcallBack = '';
                //pmts dira si : null => pmts  = {"result": true/false, "values": "v1,v2.."}
                if (pmtsType === null || pmtsType === 'R') {
                    fcallBack = callBack + '(result)';
                }
                else if (pmtsType === 'N') {
                    fcallBack = callBack + '()';
                }
                else if (pmtsType === 'RP') {
                    fcallBack = callBack + '(result,pmts)';
                }
                else if (pmtsType === 'P') { //only pmts
                    fcallBack = callBack + '(pmts)';
                }
                else if (pmtsType === 'F') { //only pmts
                    fcallBack = callBack;
                }
                eval(fcallBack);
            }


        })
        .fail(function (result) {
            loader.classList.add('d-none');
            Mensaje(oMsgErr);
        });

}


function zfill(num, len) { return (Array(len).join("0") + num).slice(-len); }


function myProfile(data) {

    
    $('#profileId').val(data.profileId);
    $('#loginId').val(data.loginId);

    $('#Nombre').val(data.nombre);
    $('#UserName').val(data.userName);
    $('#Email').val(data.email);


    $('#Phone').val(data.phone);
    $('#Extension').val(data.extension);
      
    $('#PhProv').val(data.phoneProviderId).trigger('change');

    $('#twitter').val(data.twitter);
    $('#facebook').val(data.facebook);
    $('#instagram').val(data.instagram);
    $('#linkedIn').val(data.linkedIn);
     

}


async function GetSelData (url, selId) {

    let data = await fetchproc(url);

    FillSelectPicker(selId, data);

}

async function SelPicker(url, selId, contId) {

    let data = await fetchproc(url);

    FillSelectPicker(selId, data);

}


function NoDataTb(divTable,cardBodyBg, cardIcon, mainTitle, numDatos, subTitle) {
    //<p class="text-semibold text-uppercase">${mainTitle}</p>
    //<p class="h5 text-thin mar-no">${numDatos}</p>
    let divH = `
                <div class="card-body">
                                <div class="text-center ${cardBodyBg} py-2">                                    
                                         <i class="${cardIcon}"></i>                               
                                </div>
                                <div class="p-3 text-center bg-light">                                                                          
                                    <h5>${subTitle}</h4>                                       
                                 </div> 
                </div>
            `;

    //$('#' + divTable).html(divH);

    return divH;
    
}

function NoDataSmall(divTable, cardBodyBg, cardIcon, mainTitle, numDatos, subTitle) {
    //<p class="text-semibold text-uppercase">${mainTitle}</p>
    //<p class="h5 text-thin mar-no">${numDatos}</p>
    let divH = `
                <div class="card-body">
                                <div class="text-center ${cardBodyBg} py-2">                                    
                                         <i class="${cardIcon}"></i>  
                                        <h6>${subTitle}</h4>                             
                                </div>                                                                             
                </div>
            `;

    //$('#' + divTable).html(divH);

    return divH;

}




function PageTitlevA(divTitle, iconTitle, mainTitle, secTitle) {

    let title = `<div class="media-body mx-3">
                    <h5 class="mb-1">
                        <a href="javascript:void(0)" class="text-white text-expanded">
                             <i class='${iconTitle}'></i>  ${mainTitle}
                        </a>
                    </h5>
                    <div class="text-white text-tiny pl-4">${secTitle}</div>
                </div>`;
    

    $('#' + divTitle).html(title);

}



function PageTitle(divTitle, iconTitle, mainTitle, secTitle) {
   
    let title = `<div class="p-3">
        <div class="d-flex">
            <span class="icon-stack display-4 mr-3 flex-shrink-0">
                <i class="base base-4 icon-stack-3x opacity-100 color-warning-200"></i>
                <i class="base base-4 icon-stack-2x opacity-100 color-warning-200"></i>
                <i class="${iconTitle} icon-stack-1x opacity-100 color-fusion-800"></i>
            </span>
            <div class="d-inline-flex flex-column">
                <a href="javascript:void(0)" class="fs-lg fw-500 d-block">
                   ${mainTitle}
                </a>
                <div class="d-block text-muted fs-sm">
                    ${secTitle}
                </div>
            </div>
        </div>
    </div>`;

    $('#' + divTitle).html(title);
}

function ConfCloseModal(omodal, question, title, type) {
    Confirmar(question, title, type, `let fn = CloseModal('${omodal}');`, '');
}

function CloseModal(omodal) {
    $('#' + omodal).modal('hide');
}

function CallDropZone(modalId) {

    if (modalId === undefined)
        modalId = "";

    let previewNode = document.querySelector("#dz-template" + modalId);
    previewNode.id = "";
    let previewTemplate = previewNode.parentNode.innerHTML;
    previewNode.parentNode.removeChild(previewNode);

    let uplodaBtn = $('#dz-upload-btn' + modalId);
    let removeBtn = $('#dz-remove-btn' + modalId);
    let myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
        url: "~/uploads", // Set the url
        thumbnailWidth: 50,
        thumbnailHeight: 50,
        parallelUploads: 20,
        previewTemplate: previewTemplate,
        autoQueue: false, // Make sure the files aren't queued until manually added
        previewsContainer: "#dz-previews" + modalId, // Define the container to display the previews
        clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
    });


    myDropzone.on("addedfile", function (file) {
        // Hookup the button
        uplodaBtn.prop('disabled', false);
        removeBtn.prop('disabled', false);
    });

    // Update the total progress bar
    myDropzone.on("totaluploadprogress", function (progress) {
        $("#dz-total-progress .progress-bar").css({ 'width': progress + "%" });
    });

    myDropzone.on("sending", function (file) {
        // Show the total progress bar when upload starts
        document.querySelector("#dz-total-progress" + modalId).style.opacity = "1";
    });

    // Hide the total progress bar when nothing's uploading anymore
    myDropzone.on("queuecomplete", function (progress) {
        document.querySelector("#dz-total-progress" + modalId).style.opacity = "0";
    });


    //// Setup the buttons for all transfers
    uplodaBtn.on('click', function () {

        const nfiles = myDropzone.files.length;

        const segId = '000001'; //document.getElementById('segId').value;   

        if (segId === undefined) {
            Mensaje({
                typeMsg: 'wn',
                message: 'Revise el Movimiento, No esta definido',
                title: 'Orden Diseño'
            });
            return;
        }

        for (let i = 0; i < nfiles; i++) {

            let fileID = segId + "_" + i;
            let formData = new FormData();

            formData.append("file", myDropzone.files[i], fileID);

            httpFile('/common/UploadFile'
                , 'POST'
                , formData
                , true
                , 'Mensaje'
                , 'P'
                , {
                    typeMsg: 'ok',
                    message: 'Archivo:' + segId + "_" + i + ' Uploaded:', //Proceso terminado correctamente',
                    title: 'Ord Diseño'
                }
                , false
            );

        }

        //after sent
        myDropzone.removeAllFiles(true);
        uplodaBtn.prop('disabled', true);
        removeBtn.prop('disabled', true);

        //myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
        //review datos notas...
        refreshGrid(1);

    });

    removeBtn.on('click', function () {
        myDropzone.removeAllFiles(true);
        uplodaBtn.prop('disabled', true);
        removeBtn.prop('disabled', true);
    });


}

function jsonToFormData(inJSON, inTestJSON, inFormData, parentKey) {
    // http://stackoverflow.com/a/22783314/260665
    // Raj: Converts any nested JSON to formData.
    var form_data = inFormData || new FormData();
    var testJSON = inTestJSON || {};
    for (var key in inJSON) {
        // 1. If it is a recursion, then key has to be constructed like "parent.child" where parent JSON contains a child JSON
        // 2. Perform append data only if the value for key is not a JSON, recurse otherwise!
        var constructedKey = key;
        if (parentKey) {
            constructedKey = parentKey + "." + key;
        }

        var value = inJSON[key];
        if (value && value.constructor === {}.constructor) {
            // This is a JSON, we now need to recurse!
            jsonToFormData(value, testJSON, form_data, constructedKey);
        } else {
            form_data.append(constructedKey, inJSON[key]);
            testJSON[constructedKey] = inJSON[key];
        }
    }
    return form_data;
}


function FillSelectPicker(selId, data) {

    if (data === null || data === undefined)
        return;

    let sel = document.getElementById(selId);

    //remove options
    if (sel !== null) {
        sel.options.length = 0;

        data.forEach(function (itm) {

            //sel.options[sel.options.length] = new Option(itm.text, itm.value, false, false);                       
            //option.data({
            //    dataContent: itm.dataContent,
            //    dataSubText: itm.dataSubText,
            //    dataIcon: itm.dataIcon
            //});

            let option = new Option(itm.text, itm.value, false, false);

            if (itm.dataSubtext !== undefined && itm.dataSubtext !== '')
                option.setAttribute('data-subtext', itm.dataSubtext);

            if (itm.dataContent !== undefined && itm.dataContent !== '')
                option.setAttribute('data-content', itm.dataContent);

            sel.options.add(option);

        })

        $(`#${selId}`).selectpicker('refresh');
    }

}

function CreateSelectPicker(data, selId, contId, label, title, style = 'btn-info') {

    //SelPicker('/common/GetPhoneProveedores', 'PhProv');
    //let data = await fetchproc(url);

    if (data === null || data === undefined)
        return;         

    let cont = document.getElementById(contId);
    let options = "";
    cont.innerHTML = "";

    data.forEach(function (itm) {
        options += `<option value='${itm.value}'>${itm.text}</option>  `;
    });

   cont.innerHTML = `<div class="form-group">
                        <label class="form-label">${label}</label>
                        <select id="${selId}"
                            class="selectpicker form-control show-tick"
                            data-size="6"
                            data-live-search="true"
                            data-allow-clear="true"
                            data-style="${style}"
                            title="${title}"
                            multiple
                            data-max-options="1"
                        >
                        ${options}        
                        </select>
                    </div>`;

    //remove options
    //if (sel !== null) {
    //    sel.options.length = 0;

    //    data.forEach(function (itm) {

    //        //sel.options[sel.options.length] = new Option(itm.text, itm.value, false, false);                       
    //        //option.data({
    //        //    dataContent: itm.dataContent,
    //        //    dataSubText: itm.dataSubText,
    //        //    dataIcon: itm.dataIcon
    //        //});

    //        let option = new Option(itm.text, itm.value, false, false);

    //        if (itm.dataSubtext !== undefined && itm.dataSubtext !== '')
    //            option.setAttribute('data-subtext', itm.dataSubtext);

    //        if (itm.dataContent !== undefined && itm.dataContent !== '')
    //            option.setAttribute('data-content', itm.dataContent);

    //        sel.options.add(option);

    //    })

    //    $(`#${selId}`).selectpicker('refresh');
    //}


    $(`#${selId}`).selectpicker('refresh');
}

function CreateSelect2(data, selId, contId, label, placeholder = 'Select...', bmultiple = false, modalParent = '') {

    if (data === null || data === undefined)
        return;

    let cont = document.getElementById(contId);
    let options = "";
    cont.innerHTML = "";

    data.forEach(function (itm) {
        options += `<option value='${itm.value}'>${itm.text}</option>  `;
    });

    cont.innerHTML = `<div class="form-group">
                        <label class="form-label">${label}</label>
                        <select class="select2 form-control w-100 data-size='15'" id="${selId}" ${bmultiple ? "multiple" : ""}>                                                                                                    
                            ${options}        
                        </select>
                    </div>`;


    //dont need modalParent
    //if (modalParent !== '') {
    //    $(`#${selId}`).select2({
    //        placeholder: `${placeholder}`,
    //        allowClear: true
    //        ,dropdownParent: $(`#${modalParent}`)
    //    });
    //} else {
        $(`#${selId}`).select2({
            placeholder: `${placeholder}`,
            allowClear: true,            
        });
    //}
  
}

///  using
///  let formData = new FormData();
///  formData.append("file", myDropzone.files[i], fileID);
///  let result = fetchFormData('/common/UploadFileJ', formData);
///  CommonG.Message(result);
/////////


async function fetchproc(uri, formdata = '', bMessage = false) {

    if (formdata === null || formdata === '' || formdata === undefined) {
        formdata = new FormData();
    }

    let response = await fetch(uri, {
        method: 'POST',
        body: formdata
    });

    let data = await response.json();

    if (bMessage) {
        Mensaje(data);
    }

    return data;

};

async function fetchFormData(url, formData, bMessage = false) {

    let response = await fetch(url, {
        method: 'POST',
        body: formData
    });

    let result = await response.json();

    if (bMessage) {
        CommonG.Message(result);
    }

}

async function uploadFile(file, bMessage = false) {

    let formData = new FormData();
    formData.append("file", file, file.fileID);

    let response = await fetch('/common/UploadFileJ', {
        method: 'POST',
        body: formData
    });

    let result = await response.json();

    if (bMessage) {
        CommonG.Message(result);
        //alert(result);
    }

}


async function sendTxt(vtoPhone, vsmsText, files, bmessage = false) {

    let formData = new FormData();
    formData.append("to", vtoPhone);
    formData.append("txtMsg", vsmsText);
    formData.append("files", files);

    let response = await fetch('/sms/sendTextJ', {
        method: 'POST',
        body: formData
    });

    let result = await response.json();

    if (bmessage) {
        CommonG.Message(result);
        //alert(result);
    }

}


async function sendTxtJ(jdata, bmessage = false) {

    let response = await fetch('/sms/sendText', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(jdata)
    });

    let result = await response.json();

    if (bmessage) {
        CommonG.Message(result); //split..
        //alert(result);
    }

}


function Message6(message = 'Proc OK', type = 'ok', title = 'Manager EAM') {

    let msg = { 'typeMsg': type, 'message': message, 'title': title };

    if (msg.typeMsg === "ok") {
        toastr.success(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'su') {
        toastr.success(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'er') {
        toastr.error(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'wn') {
        toastr.warning(msg.message, msg.title);
    }
    else if (msg.typeMsg === 'in') {
        toastr.info(msg.message, msg.title);
    }

}


//Confirmar("Are you sure?", "Are you sure?", "warning", temp, 'xxxxx');
function Confirmar(vtitle, vtext, vtype, vfunction, pmt) {
   
    var swalWithBootstrapButtons = Swal.mixin(
        {
            customClass:
            {
                confirmButton: "btn btn-primary",
                cancelButton: "btn btn-danger mr-2"
            },
            buttonsStyling: false
        });

        swalWithBootstrapButtons
        .fire(
            {
                title: vtitle,
                text: vtext,
                type: "warning",
                showCancelButton: true,
                confirmButtonText: "Yes, Procceed!",
                cancelButtonText: "No, cancel!",
                reverseButtons: true,
                closeOnConfirm: true,
                closeOnCancel: false
            })
        .then(function (result) {
            if (result.value) {

                eval(vfunction);

                swalWithBootstrapButtons.fire(
                    {
                        position: 'center',
                        type: 'success',
                        title: 'our procedure has been done.',
                        showConfirmButton: false,
                        timer: 1500
                    }
                  
                );
            }
            else if (
                    // Read more about handling dismissals
                    result.dismiss === Swal.DismissReason.cancel
                    ) {
                        swalWithBootstrapButtons.fire(
                            "Cancelled",
                            "Procedure Cancelled",
                            "error"
                    );
            }
        });

}

///////
///  using
///  let formData = new FormData();
///  formData.append("file", myDropzone.files[i], fileID);
///  let result = fetchFormData('/common/UploadFileJ', formData);
///  CommonG.Message(result);
/////////


async function fetchproc(uri, formdata = '', bMessage = false) {
    
    if (formdata === null || formdata === '' || formdata === undefined) {
        formdata = new FormData();
    }

    let response = await fetch(uri, {
        method: 'POST',
        body: formdata
    });

    let data = await response.json();

    if (bMessage) {
        Mensaje(data);
    }

    return data;     

};


/*!
 * Serialize all form data into a query string
 * (c) 2018 Chris Ferdinandi, MIT License, https://gomakethings.com
 * @param  {Node}   form The form to serialize
 * @return {String}      The serialized form data
 */
var serialize = function (form) {

    // Setup our serialized data
    var serialized = [];

    // Loop through each field in the form
    for (var i = 0; i < form.elements.length; i++) {

        var field = form.elements[i];

        // Don't serialize fields without a name, submits, buttons, file and reset inputs, and disabled fields
        if (!field.name || field.disabled || field.type === 'file' || field.type === 'reset' || field.type === 'submit' || field.type === 'button') continue;

        // If a multi-select, get all selections
        if (field.type === 'select-multiple') {
            for (var n = 0; n < field.options.length; n++) {
                if (!field.options[n].selected) continue;
                serialized.push(encodeURIComponent(field.name) + "=" + encodeURIComponent(field.options[n].value));
            }
        }

        // Convert field data to a query string
        else if ((field.type !== 'checkbox' && field.type !== 'radio') || field.checked) {
            serialized.push(encodeURIComponent(field.name) + "=" + encodeURIComponent(field.value));
        }
    }

    return serialized.join('&');

};

