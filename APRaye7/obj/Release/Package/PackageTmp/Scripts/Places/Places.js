$(document).ready(function () {
    $('#BranchesTable').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as CSV</b></i>',
                titleAttr: 'CSV',
                title:'Branches',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as EXCEL</b></i>',
                titleAttr: 'Excel',
                title: 'Branches',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;;"> Export as PDF</b></i>',
                titleAttr: 'PDF',
                title: 'Branches',
                exportOptions: {
                    columns: [ 1,2,3,4]
                }
            },
           
        ],

        "sAjaxSource": $.BranchUrls.getData,
        "bProcessing": true,
        //responsive:true,

        "columns": [
                        { "data": "id",
                            "searchable": false,
                            "sortable": false,
                            "visible": false
                            
                        },
                        { "data": "name" },
                        { "data": "longitude" },
                        { "data": "latitude" },
                        { "data": "description" }
        ],
        //responsive: true,
        //"autoWidth": true,
        "autoWidth": false,
        "lengthChange": true,
        "searching":true
        
    });
    var table = $('#BranchesTable').DataTable();
    $('#BranchesTable tbody').on('click', 'tr', function () {
        var idx = table
            .row(this)
            .index();
        var PlaceID = $("#BranchesTable").DataTable().cell(idx, 0).data();
        window.location.href = $.BranchUrls.Details + "/" + PlaceID;
    });
    $.ajax({
        dataType: "json",
        type: "POST",
        url: $.BranchUrls.getSessionControllerData,
        data: JSON,
        success: function (data) {
            if(data.IsAdmin != null || data.IsSuperAdmin != null)
            {
                $("#toolbar").html('<a href=' + $.BranchUrls.Create + ' class="btn btn-block btn-success" style="display:inline" id="AddButton"><i class="fa fa-plus"></i> <b>Add Branch</b></a>');

            }

        }
        
         

    ,
        error: function () {

        }
    });
    $("#toolbar").after("<br/>");

});