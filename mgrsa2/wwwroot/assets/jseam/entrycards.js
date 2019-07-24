"use strict";

var ec, ecr, EntryCardsG = {

    settings: {
        id: "data-tableEC",
        container: "divEntryCards",
        config: {
            "order": [0, "desc"],
            //"pagingType": ["full_numbers"],
            //"pageLength": 5,
            //"lengthMenu": [[5, -1], [5, "All"]],
            //"select": true,
            "responsive": true,
            //"scrollY": 180,
            //"scrollX": true,
            //"scrollCollapse": true,
            "paging": false,
            "fixedColumns": true,
            "bFilter": false

        }
    },

    InitEC: function (vprofileId) {

        http('/users/GetEntryCards'
            , 'POST'
            , { profileId: vprofileId }
            , true
            , 'EntryCardsG.RefreshECTb'
        );

        //let vprofileId = $('#profileIdPf').val();
        //$.ajax({
        //    type: 'POST',
        //    url: '/users/GetEntryCards',
        //    data: { profileId: vprofileId },
        //    success: function (result) {
        //        if (result.substring(0, 2).toLowerCase() !== 'wn') {
        //            let s = EntryCardsG.RefreshECTb(result);
        //            $('#divLoading').hide();
        //        }
        //        else {
        //            let mx = CommonG.Message(result);
        //        }
               
        //    },
        //    fail: function (result) {
        //        px = CommonG.Message('wn|Problemas con Servidor| Admin Entry Cards');
        //        //return result;              
        //    }

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


    },

    NewCard: function () {

        const vLoginId = $('#loginId').val();
        const vProfileId = $('#ProfileIdPf').val();
    
        const vCard = $('#card').val();

        if (vCard === undefined || vCard.length !== 10) {
            CommonG.Message('wn|Verifique información|ENTRY CARDS', 0);
            return;
        }

        http('/users/AddEntryCard'
            , 'POST'
            , { profileId: vProfileId, loginId: vLoginId, card: vCard }
            , true
            , 'EntryCardsG.RefreshECTb'
            , 'P'
            , vProfileId
        );


        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/users/AddEntryCard',
        //    data: { profileId: vProfileId, loginId: vLoginId, card: vCard },
        //    success: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString(result, 0));
        //        EntryCardsG.InitEC(vProfileId);               
        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString(result, 0));
        //    }

        //});

    },

    RefreshECTb: function (rdata) {

        let contenido = "";
        let odivContainer = document.getElementById(this.settings.container);

        if (rdata !== '' && ('weo').indexOf(rdata.substring(0, 1)) === -1) {
            let data = CommonG.CrearMatriz(rdata, false);
            let nRows = data.length;
            contenido =
                `<table id="${this.settings.id}"  class="table table-hover table-striped w-100 table-no-bordered" style="width:100%">
                      <thead class="bg-primary-50"> 
                          <tr> 
                              <td>Id</td> 
                              <td>Tarjeta</td> 
                              <td>Activación</td> 
                              <td>Creación</td> 
                          </tr> 
                      </thead> 
                      <tbody>`;

            for (let i = 0; i < nRows; i++) {
                let bactive = (data[i][4] === 'True') ? "checked" : "";
                contenido += `<tr>
                         <td> ${data[i][0]}  </td> 
                         <td> ${data[i][2]}  </td> 
                         <td> <input class="activeCard" type="checkbox"   ${bactive}  data-cardid="${data[i][0]}" data-card="${data[i][2]}" onclick="let r = EntryCardsG.ActiveCard(this);"/> </td> 
                         <td> ${data[i][6]}  </td> 
                       </tr>`;
            }
            contenido += '     </tbody>';
            contenido += '  </table>';

            odivContainer.innerHTML = contenido;

            $("#" + this.settings.id).DataTable(this.settings.config);

        }
        else {
            contenido =
                `<div class="card">
                    <div class="card-body text-center bg-light">
                        <i class="far fa-map d-block display-4"></i>
                    </div>
                    <div class="p-1 text-center">
                        <p class="text-semibold text-lg text-main text-uppercase">TARJETAS DE ENTRADA</p>
                        <p class="h1 text-thin mar-no">0</p>
                        <p class="text-sm text-overflow pad-top">
                            <span class="text-bold">NO</span> datos encontrados
                        </p>
                    </div>
                </div>`;
            
            odivContainer.innerHTML = contenido;
        }
    },

    CancelNewCard: function (obj) {

        $('#card').val('');
        CommonG.Message('ok|Cancelación de Adición de Tarjeta', "ENTRY CARDS", 0);

    },

    ActiveCard: function (obj) {

        //e.preventDefault();

        const vLoginId = $('#loginId').val();
        const vProfileId = $('#ProfileIdPf').val();
        const Id = $(obj).data('cardid');
        const card = $(obj).data('card');

        //alert($(obj).prop('checked'));
        //alert($(obj).is(':checked'));

        const vActive = $(obj).prop('checked');

        http('/users/ActiveEntryCard'
            , 'POST'
            , { profileId: vProfileId, loginId: vLoginId, cardId: Id, active: vActive, card: card }
            , true           
        );

        //CommonG.Loading();
        //$.ajax({
        //    type: 'POST',
        //    url: '/users/ActiveEntryCard',
        //    data: { profileId: vProfileId, loginId: vLoginId, cardId: Id, active: vActive, card: card },
        //    success: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString(result, 0));
        //        //let e =  EntryCardsG.InitEC();    //no necesario           

        //    },
        //    fail: function (result) {
        //        CommonG.Loading();
        //        CommonG.Message(CommonG.GetString(result, 0));     
        //    }

        //});

    },

    init: function () {
        ec = this.settings;
        ecr = this.InitEC();
    }


};
