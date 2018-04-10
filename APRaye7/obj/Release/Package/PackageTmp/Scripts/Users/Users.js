$(document).ready(function () {

    $('#UsersTable').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as CSV</b></i>',
                titleAttr: 'CSV',
                title: 'Users',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as EXCEL</b></i>',
                titleAttr: 'Excel',
                title: 'Users',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o" style="font-size:initial;color:white;"><b style="font-size:14px;font-weight:700;"> Export as PDF</b></i>',
                titleAttr: 'PDF',
                title: 'Users',
                orientation: 'landscape',
                pageSize: 'A3',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9,10]
                }
            },

        ],
        //"bServerSide": false,
        "sAjaxSource": $.UserUrls.getData,
        "bProcessing": true,
        //"bAutoWidth": true,
        //"responsive": true,
        
        //"scrollX": true,
        "columns": [
                        {
                            "data": "UserID",
                            "bSearchable": false,
                            "bSortable": false,
                            "bVisible": false,
                            "fnRender": function (oObj) {
                                return '<a href=\"Details/' +
                                oObj.aData[0] + '\">View</a>';
                            }
                        },
                        { "data": "Email" },
                        { "data": "First_Name" },
                        { "data": "Last_Name" },
                        { "data": "User_Points" },
                        { "data": "Gender" },
                           { "data": "Branch" },
                                                      { "data": "Phone_Number" },
                        { "data": "Have_Car" },
                        { "data": "Same_Gender" },
                        { "data": "Smoking" },
                      
                         { "data": "Driven_Points" },
                        
        ],
        //responsive: true,
        //
        "autoWidth": false,
        "lengthChange": true,
        "searching":true
    

    });
    var table = $('#UsersTable').DataTable();
    $('#UsersTable tbody').on('click', 'tr', function () {
        var idx = table
            .row(this)
            .index();
        var UserID = $("#UsersTable").DataTable().cell(idx, 0).data();
        window.location.href = $.UserUrls.Details + "/" + UserID;
    });
    $.ajax({
        dataType: "json",
        type: "POST",
        url: $.UserUrls.getSessionControllerData,
        data: JSON,
        success: function (data) {
            if (data.IsAdmin != null || data.IsSuperAdmin != null) {
                $("#toolbar").html('<a href=' + $.UserUrls.Create+ ' class="btn btn-block btn-success" style="display:inline" id="AddButton"><i class="fa fa-plus"></i> <b>Add User</b></a>');

            }
        },
        error: function () {

        }
    });

    $("#toolbar").after("<br/>");
});