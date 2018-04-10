$(document).ready(function () {
    $('#SystemUsersTable').dataTable({
        "ajax": {
            "url": "getSystemUsers",
            "type": "POST",
            "data":"JSON"

        },
        "columns": [
            {
                "data": "SystemUserID",
            "visible":false},
            { "data": "Username" },
            { "data": "FullName" },
            { "data": "Email" },
            { "data": "Role" },
            { "data": "CreationDateString" }
        ]
    });

    var table = $('#SystemUsersTable').DataTable();
    $('#SystemUsersTable tbody').on('click', 'tr', function () {
        var idx = table
            .row(this)
            .index();
        var SystemUserID = $("#SystemUsersTable").DataTable().cell(idx, 0).data();
        window.location.href = "SystemUserProfile/" + SystemUserID;})
});
