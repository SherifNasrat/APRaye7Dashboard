$(function () {
    $('#fromPeriod_picker').datetimepicker({
        format: 'DD/MM/YYYY'
    });
    $('#toPeriod_picker').datetimepicker({
        format: 'DD/MM/YYYY'

    });
    function round(value, exp) {
        if (typeof exp === 'undefined' || +exp === 0)
            return Math.round(value);

        value = +value;
        exp = +exp;

        if (isNaN(value) || !(typeof exp === 'number' && exp % 1 === 0))
            return NaN;

        // Shift
        value = value.toString().split('e');
        value = Math.round(+(value[0] + 'e' + (value[1] ? (+value[1] + exp) : exp)));

        // Shift back
        value = value.toString().split('e');
        return +(value[0] + 'e' + (value[1] ? (+value[1] - exp) : -exp));
    }
    $.DashBoardControls = {};
    
    $("#RunDashBoard").click(function () {
        var fromDate = $('#fromPeriod_picker').val();
        var toDate = $('#toPeriod_picker').val();
        if ($("#fromPeriod_picker").val() === "" || $("#toPeriod_picker").val() === "") {
            $.ajax({
                dataType: "html",
                type: "GET",
                url: $.ReportUrls.GenerateReport,
                data: JSON,
                success: function (data) {
                    $("#dvReportResult").html(data);
                },
                error: function () {

                }
            });
        }
        else
        {
            $.ajax({
                dataType: "html",
                type: "GET",
                url: $.ReportUrls.GenerateReport,
                data: { 'fromDate': fromDate, 'toDate': toDate },
                success: function (data) {
                    $("#dvReportResult").html(data);
                },
                error: function () {

                }
            }); 
        }
        
       
    });



   
});
