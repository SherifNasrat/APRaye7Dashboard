//$(function () {
    
//    $('#usersTable').dataTable({
//        "processing": true,
//        "serverSide": true,
//        "ajax": {
//            "url": "Users/ReadUsers",
//            "type": "POST"
//        },
//        "columns": [

//            {"data": "First_Name" },
//            { "data": "Last_Name" },
//            { "data": "Email" },
//            { "data": "Phone_Number" },
//            { "data": "Gender" },
//            { "data": "Birth_Date" },
//            { "data": "Have_Car" },
//            { "data": "Smoking" },
//            { "data": "Same_Gender" },
//            { "data": "Provider" },
//            { "data": "Rating_Counter" },
//            { "data": "Rating" },
//            { "data": "Business_Purpose" },
//            { "data": "Referral_Points" },
//            { "data": "User_Points" },
//            { "data": "Driven_Points" },
//            { "data": "Driven_Points" },
//            { "data": "Ridden_Points" },
//            { "data": "SMS_Count" },
//            { "data": "Tokens" },
//            { "data": "Last_Seen" },
//            { "data": "Personal_Image" },
//            { "data": "Created_At" },
//            { "data": "Updated_At" }
            
//        ]
//    });
//});
$(function () {

    $('#usersTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": 'Users/ReadUsers',
        "sServerMethod": "POST",
        "aoColumns": [
                                { "sName": "First_Name"},
                                { "sName": "Last_Name" },
                                { "sName": "Email" },
                                { "sName": "Phone_Number" },    
                                { "sName": "Gender"},
                                {"sName": "Birth_Date",
                                "fnRender" : function(obj, val)
                                {
                                    var dx = new Date(parseInt(val.substr(6)));
                                    var dd = dx.getDate();
                                    var mm = dx.getMonth() + 1;
                                    var yy = dx.getFullYear();

                                    if (dd <= 9)
                                    {
                                        dd = "0" + dd;
                                    }
                                    if (mm <= 9) {
                                        mm = "0" + mm;
                                    }
                                    return dd + "." + mm + "." + yy;
                                }
                                },
                                {"sName" :"Have_Car"},
                                {"sName" : "Smoking"},
                                {"sName" : "Same_Gender"},
                                {"sName": "Provider"},
                                { "sName": "Rating_Counter" },
                                {"sName" : "Rating"},
                                {"sName" : "Business_Purpose"},
                                {"sName" : "Referral_Points"},
                                {"sName" : "User_Points"},
                                {"sName" : "Driven_Points"},
                                {"sName" : "Ridden_Points"},
                                {"sName" : "SMS_Count"},
                                {"sName" : "Tokens"},
                                {"sName" : "Last_Seen","fnRender" : function(obj, val)
                                {
                                    var dx = new Date(parseInt(val.substr(6)));
                                    var dd = dx.getDate();
                                    var mm = dx.getMonth() + 1;
                                    var yy = dx.getFullYear();

                                    if (dd <= 9)
                                    {
                                        dd = "0" + dd;
                                    }
                                    if (mm <= 9) {
                                        mm = "0" + mm;
                                    }
                                    return dd + "." + mm + "." + yy;
                                }},
                                {"sName" : "Personal_Image"},
                                {"sName" : "Created_At",
                                "fnRender" : function(obj, val)
                                {
                                    var dx = new Date(parseInt(val.substr(6)));
                                    var dd = dx.getDate();
                                    var mm = dx.getMonth() + 1;
                                    var yy = dx.getFullYear();

                                    if (dd <= 9)
                                    {
                                        dd = "0" + dd;
                                    }
                                    if (mm <= 9) {
                                        mm = "0" + mm;
                                    }
                                    return dd + "." + mm + "." + yy;
                                }
                                },
                                {"sName" : "Updated_At",
                                "fnRender" : function(obj, val)
                                {
                                    var dx = new Date(parseInt(val.substr(6)));
                                    var dd = dx.getDate();
                                    var mm = dx.getMonth() + 1;
                                    var yy = dx.getFullYear();

                                    if (dd <= 9)
                                    {
                                        dd = "0" + dd;
                                    }
                                    if (mm <= 9) {
                                        mm = "0" + mm;
                                    }
                                    return dd + "." + mm + "." + yy;
                                }
                                }
        ]

   
     
    });
});