$(document).ready(function () {
    
    var LogID = $("#ChangeLogId").val();

    $('#TripsEditLog').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as CSV</b></i>',
                titleAttr: 'CSV',
                title: 'Trips Edit Log',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as EXCEL</b></i>',
                titleAttr: 'Excel',
                title: 'Trips Edit Log',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as PDF</b></i>',
                titleAttr: 'PDF',
                title: 'Trips Edit Log',
                exportOptions: {
                    columns: [1, 2, 3]
                }
            },

        ],
        "bServerSide": false,
        "sAjaxSource": dataURL,
        "bProcessing": true,
        "bAutoWidth": false,
        //responsive:true,
        "scrollX": true,
        "columns": [
                        {
                            "data": "ChangeLogDetailId",
                            "searchable": false,
                            "sortable": false,
                            "visible": false

                        },
                        { "data": "Field" },
                        { "data": "PreviousValue" },
                        { "data": "NewValue" },
                        { "data": "DetailDate" }
        ],
        //responsive: true,
        //"autoWidth": true,

        "lengthChange": true,
        "searching": true

    });
   
   


});