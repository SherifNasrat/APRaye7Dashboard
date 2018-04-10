$(document).ready(function () {
    $('#TripsTable').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as CSV</b></i>',
                titleAttr: 'CSV',
                title:'Trips',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as EXCEL</b></i>',
                titleAttr: 'Excel',
                title: 'Trips',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as PDF</b></i>',
                titleAttr: 'PDF',
                title: 'Trips',
                exportOptions: {
                    columns: [ 1,2,3,4,5,6,7,8,9]
                }
            },
           
        ],

        "sAjaxSource": $.TripsUrls.getData,
        "bProcessing": true,


        "columns": [
                        { "data": "TripID",
                            "searchable": false,
                            "sortable": false,
                            "visible": false
                           
        },
                        { "data": "Departure_Time_String" },
                        { "data": "Seats" },
                        { "data": "Points" },
                        { "data": "FK_SourceName" },
                        { "data": "FK_DestinationName" },
                        { "data": "FK_DriverName" },
                        { "data": "Cancelled" },
                        { "data": "Created_At_String" },
                        { "data": "Updated_At_String" }
    ],
    //responsive: true,
    //"autoWidth": true,
       
        "autoWidth": false,
        "lengthChange": true,
        "searching": true
        
});
var table = $('#TripsTable').DataTable();
$('#TripsTable tbody').on('click', 'tr', function () {
    var idx = table
        .row(this)
        .index();
    var TripID = $("#TripsTable").DataTable().cell(idx, 0).data();
    window.location.href = $.TripsUrls.Details+"/" + TripID;
});
$.ajax({
    dataType: "json",
    type: "POST",
    url: $.TripsUrls.getSessionControllerData,
    data: JSON,
    success: function (data) {
        if(data.IsAdmin != null || data.IsSuperAdmin != null)
        {
            $("#toolbar").html('<a href=' + $.TripsUrls.Create + ' class="btn btn-block btn-success" style="display:inline" id="AddButton"><i class="fa fa-plus"></i> <b>Add Trip</b></a>');

        }
    }
        
         

,
    error: function () {

    }
});
$("#toolbar").after("<br/>");
});