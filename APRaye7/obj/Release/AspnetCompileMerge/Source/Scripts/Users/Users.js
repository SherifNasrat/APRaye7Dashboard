$(document).ready(function () {

    $('#UsersTable').DataTable({
        "dom": '<"col-lg-push-0" lf><"#toolbar">rti<"col-lg-pull-0" p>B',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o btn-primary"><b>CSV</b></i>',
                titleAttr: 'CSV',
                title: 'Users',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o btn-success">EXCEL</i>',
                titleAttr: 'Excel',
                title: 'Users',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                extend: 'pdfHtml5',

                text: '<i class="fa fa-file-pdf-o btn-danger"><b>PDF</b></i>',
                titleAttr: 'PDF',
                title: 'Users',
                orientation: 'landscape',
                pageSize: 'A3',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12,13,14,15]
                }
            },

        ],
        "bServerSide": false,
        "sAjaxSource": "Users/getUsers",
        "bProcessing": true,
        "bAutoWidth": true,
        responsive: true,
        
        //"scrollX": true,
        "columns": [
                        {
                            "sName": "UserID",
                            "bSearchable": false,
                            "bSortable": false,
                            "bVisible": false,
                            "fnRender": function (oObj) {
                                return '<a href=\"Details/' +
                                oObj.aData[0] + '\">View</a>';
                            }
                        },
                        { "sName": "Email" },
                        { "sName": "First Name" },
                        { "sName": "Last Name" },
                        { "sName": "User Points" },
                        { "sName": "Gender" },
                           { "sName": "Business Purprose" },
                                                      { "sName": "Phone Number" },
                        { "sName": "Have a Car" },
                        { "sName": "Same Gender" },
                        { "sName": "Smoking" },
                        { "sName": "Last Seen" },
                         { "sName": "Driven Points" },
                          { "sName": "Current Sign In At" },
                          { "sName": "Sign In Count" },
                          { "sName": "Creation Date" }
        ],
        //responsive: true,
        //
        "autoWidth": true,
        "lengthChange": true,
        "searching":true
    

    });
    var table = $('#UsersTable').DataTable();
    $('#UsersTable tbody').on('click', 'tr', function () {
        var idx = table
            .row(this)
            .index();
        var UserID = $("#UsersTable").DataTable().cell(idx, 0).data();
        window.location.href = "Users/Details/" + UserID;
    });
    $.ajax({
        dataType: "json",
        type: "POST",
        url: "Account/getSessionControllerUser",
        data: JSON,
        success: function (data) {
            if (data.IsAdmin != null || data.IsSuperAdmin != null) {
                $("#toolbar").html('<a href="Users/Create" class="btn btn-block btn-success" style="display:inline" id="AddButton"><i class="fa fa-plus"></i> <b>Add User</b></a>');

            }
        }



   ,
        error: function () {

        }
    });

});