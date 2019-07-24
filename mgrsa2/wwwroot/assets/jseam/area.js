"use strict";

var AreaG = {

    settingsTable: {
        id: "tbAreas",
        container: "divAreas",
        config: {
            "order": [0, "asc"],
            "pagingType": ["full_numbers"],
            "pageLength": 20,
            "lengthMenu": [[5, 10, 15, 25, -1], [5, 10, 15, 25, "All"]],
            "responsive": true,
            "select": true,
            "paging": true,
            "bFilter": true,
            "bInfo": true
            //,
            //"language": {
            //    "paginate": {
            //        "previous": '<i class="demo-psi-arrow-left"></i>',
            //        "next": '<i class="demo-psi-arrow-right"></i>'
            //    }
            //},
            //"columnDefs": [                
            //    {
            //        "className": "dt-left", "targets": "_all"
            //    }
            //]
        }
    },
       

    bindUIActions: function () {

        //$('#' + this.settingsTable.id).on('click', 'tr', function () {
        //    if ($(this).hasClass('selected')) {
        //        $(this).removeClass('selected');
        //    }
        //    else {
        //        $('tr.selected').removeClass('selected');
        //        $(this).addClass('selected');
        //    }
        //});
             
    }

};