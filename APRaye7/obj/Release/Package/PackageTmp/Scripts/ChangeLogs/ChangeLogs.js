$(document).ready(function () {
    $('#ChangeLogsTable').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o" style="font-size:initial;"><b style="font-size:14px;font-weight:700;">Export as CSV</b></i>',
                titleAttr: 'CSV',
                title:'ChangeLogs',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:initial;"><b style="font-size:14px;font-weight:700;">Export as EXCEL</b></i>',
                titleAttr: 'Excel',
                title: 'ChangeLogs',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o" style="font-size:initial;"><b style="font-size:14px;font-weight:700;">Export as PDF</b></i>',
                titleAttr: 'PDF',
                title: 'ChangeLogs',
                exportOptions: {
                    columns: [ 1,2,3,4,5]
                }
            },
           
        ],
        "bServerSide": false,
        "sAjaxSource": $.ChangeLogsUrls.getData,
        "bProcessing": true,
        "bAutoWidth": false,
        //responsive:true,
        "scrollX":true,
        "columns": [
                        {
                            "data": "ChangeLogId",
                            "searchable": false,
                            "sortable": false,
                            "visible": false
                            
                        },
                        { "data": "UserName" },
                        { "data": "Action" },
                        { "data": "Entity" },
                        { "data": "RecordNumber" },
                        { "data": "LogDate" },
        ],
        //responsive: true,
        //"autoWidth": true,
       
        "lengthChange": true,
        "searching":true
        
    });
    var table = $('#ChangeLogsTable').DataTable();
    $('#ChangeLogsTable tbody').on('click', 'tr', function () {
        var idx = table
            .row(this)
            .index();
        var LogID = $("#ChangeLogsTable").DataTable().cell(idx, 0).data();
        var RecordID = $("#ChangeLogsTable").DataTable().cell(idx, 4).data();
        window.location.href = $.ChangeLogsUrls.Details + "/" + LogID + "/" + RecordID;
    });


});