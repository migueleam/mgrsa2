
// Form-File-Upload.js
// ====================================================================
// This file should not be included in your project.
// This is just a sample how to initialize plugins or components.
//
// - ThemeOn.net -

let DropZoneG = {

    CallDropZone: function(container) {

        Dropzone.autoDiscover = false;

        //let dropzonex = Dropzone.forElement("#" + container);
        //if (!dropzonex)
        //    DropZoneG.CallDropZone(container);
        
        let previewNode = document.querySelector(".dz-template");
        previewNode.id = "";
        let previewTemplate = previewNode.parentNode.innerHTML;
        previewNode.parentNode.removeChild(previewNode);

        let uplodaBtn = $('.dz-upload-btn');
        let removeBtn = $('.dz-remove-btn');

        let myDropzone = new Dropzone("#" + container, { // Make the whole body a dropzone
            url: "../task", // Set the url
            thumbnailWidth: 50,
            thumbnailHeight: 50,
            parallelUploads: 20,
            previewTemplate: previewTemplate,
            autoQueue: false, // Make sure the files aren't queued until manually added
            previewsContainer: ".dz-previews", // Define the container to display the previews
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
            document.querySelector("#dz-total-progress").style.opacity = "1";
        });

        // Hide the total progress bar when nothing's uploading anymore
        myDropzone.on("queuecomplete", function (progress) {
            document.querySelector("#dz-total-progress").style.opacity = "0";
        });


        removeBtn.on('click', function () {
            myDropzone.removeAllFiles(true);
            uplodaBtn.prop('disabled', true);
            removeBtn.prop('disabled', true);
        });

        uplodaBtn.on('click', function () {

            let vId = $('#taskId').val();
            let vTipo = "T";           

            if (vId === undefined) {
                vId = $('#ideaId').val();
                vTipo = "I";
            }

            if (vId === null|| vId === undefined || vId === "0") {                          
                return Mensaje({
                    "typeMsg": 'wn',
                    "message": 'Guardar Attachment',
                    "title": 'Task-Ideas'
                });
            }

            
            let vnotas = $('#axNotas').val();                      
            if (vnotas === null || vnotas === undefined)
                vnotas = "-";
           
            myDropzone.files.forEach(function (itm) {
                //let fileID = myDropzone.files[i].name;     //segId + "_" + i;
                let formData = new FormData();
                let vfileDate = moment(itm.lastModifiedDate).format("YYYY-MM-DD hh:mm A");

                formData.append("file", itm, itm.name);
                formData.append("fileDate", vfileDate);
                formData.append("notas", vnotas);
                formData.append("id", vId);
                formData.append("tipo", vTipo);

                //, `AnexosG.GetAnexos(${taskId})`
                httpFile('/task/UploadFile'
                    , 'POST'
                    , formData                 
                    , true                                        
                    , 'AnexosG.RefreshTb'
                    , 'R'
                   
                );                               

            });
            
            //after sent
            myDropzone.removeAllFiles(true);
            uplodaBtn.prop('disabled', true);
            removeBtn.prop('disabled', true);
            

        });

        removeBtn.on('click', function () {
            myDropzone.removeAllFiles(true);
            uplodaBtn.prop('disabled', true);
            removeBtn.prop('disabled', true);
        });
                
    },
         
    InitHtml: function () {
        return  `  <div class="border-top py-2">
                        <!-- The fileinput-button span is used to style the file input field as button -->
                        <span class="btn btn-success btn-sm fileinput-button dz-clickable" id='btnAddAtt'>
                            <i class="fa fa-plus"></i>
                            <span>Anexar</span>
                        </span>

                        <div class="btn-group pull-right">
                            <button class="btn btn-primary btn-sm dz-upload-btn" type="submit" disabled>
                                <i class="ion ion-ios-cloud-upload"></i> Subir
                            </button>
                            <button class="btn btn-danger btn-sm dz-remove-btn" type="reset" disabled>
                                <i class="ion ion-ios-trash"></i> Remover
                            </button>
                        </div>
                    </div>

                        <div  class="dz-previews">
                            <div class="pt-2 border-top dz-template">
                                <div class="card">
                                    <div class="car-body">
                                        <!--This is used as the file preview template-->
                                        <div class="row mx-auto">
                                            <div class="col-sm-4 py-2">
                                                <img class="dz-img" data-dz-thumbnail>
                                            </div>
                                            <div class="col-sm-6  py-2">
                                                <p class="text-bold" data-dz-name></p>
                                                <span class="dz-error text-danger text-tiny" data-dz-errormessage></span>
                                                <p class="text-tiny" data-dz-size></p>
                                                <div id="dz-total-progress" style="opacity:0">
                                                    <div class="progress active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0">
                                                        <div class="progress-bar bg-success" style="width:0%;" data-dz-uploadprogress></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-2 my-auto">
                                                <button class="btn btn-sm btn-danger" data-dz-remove><i class="ion ion-ios-close"></i></button>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
            `;
     
     
    },

    Init: function (container) {

        let oDZ = document.getElementById(container);
        oDZ.innerHTML= DropZoneG.InitHtml();
        //init dropzone...
        let dz = DropZoneG.CallDropZone(container);        
    },

    CloseDropZone: function (container) {

        let uplodaBtn = $('.dz-upload-btn');
        let removeBtn = $('.dz-remove-btn');
        uplodaBtn.prop('disabled', true);
        removeBtn.prop('disabled', true);

        Dropzone.forElement("#" + container).removeAllFiles(true);

        //let oDZ = document.getElementById(container);            
        //oDZ.innerHTML = '';
        
    }

}
