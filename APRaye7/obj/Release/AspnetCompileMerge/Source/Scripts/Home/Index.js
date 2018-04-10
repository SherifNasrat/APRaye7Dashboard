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
        var toDate =  $('#toPeriod_picker').val();
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "Home/MaleFemaleChart",
            data: {'fromDate':fromDate,'toDate':toDate},
            success: function (data) {
                $.DashBoardControls.MaleFemaleChart.data.datasets[0].data = data;
                $.DashBoardControls.MaleFemaleChart.update();
                $("#MalesPercentageli").text(round((data[0]) / (data[0] + data[1]) * 100,2)+"%");
                $("#FemalesPercentageli").text(round((data[1]) / (data[0] + data[1]) * 100,2)+"%");
               
            },
            error: function () {

            }
        }); //Males vs. Females Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "Home/HaveCarChart",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                $.DashBoardControls.CarsNoCarsChart.data.datasets[0].data = data;
                $.DashBoardControls.CarsNoCarsChart.update();
                $("#UsersWithCarli").text(round((data[0]) / (data[0] + data[1]) * 100, 2) + "%");
                $("#UsersWithoutCarli").text(round((data[1]) / (data[0] + data[1]) * 100, 2) + "%");

            },
            error: function () {

            }
        });//Users with Cars vs. Users without Cars 
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "Home/TotalTripsPerBranch",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {

                $.DashBoardControls.TotalTripsBranchChart.data.datasets[0].data = data[0];
                $.DashBoardControls.TotalTripsBranchChart.update();
            },
            error: function () {

            }
        }); // INCOMPLETE* Total Trips Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TripsPerBranch",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
               
                $.DashBoardControls.TripsBranchChart.data.datasets[0].data = data[0];
                $.DashBoardControls.TripsBranchChart.update();
            },
            error: function () {

            }
        }); // INCOMPLETE* Trips Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TotalKilometersMade",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                if (data === null)
                    $("#KilometersMade").text("0");
                else
                    $("#KilometersMade").text(round(data, 2) + data);


            },
            error: function () {

            }
        }); //Total Kilometers made
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/UsersPerBranch",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
              
                $.DashBoardControls.UsersBranchChart.data.datasets[0].data = data[0];
                $.DashBoardControls.UsersBranchChart.update();
            },
            error: function () {

            }
        }); // Users Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/ActiveInActiveUsers",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                $.DashBoardControls.ActiveInActiveChart.data.datasets[0].data = data;
                $.DashBoardControls.ActiveInActiveChart.update();
            },
            error: function () {

            }
        }); //Active Users  vs. In-Active Users 
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/RecurrentTrips",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
               
                $.DashBoardControls.RecurrentChart.data.datasets[0].data = data;
                $.DashBoardControls.RecurrentChart.update();
                $("#tripsli").text(round((data[0]) / (data[0] + data[1]) * 100, 2) + "%");
                $("#recurrentli").text(round((data[1]) / (data[0] + data[1]) * 100, 2) + "%");
            },
            error: function () {

            }
        }); //Trips  vs. Recurrent Users
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TopTenVisited",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                $.DashBoardControls.TopTenChart.data.datasets[0].data = data[0];
                $.DashBoardControls.TopTenChart.update();
            },
            error: function () {

            }
        }); // Top Ten Visited Branches
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TripsStatusChart",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                $.DashBoardControls.TripStatusChart.data.datasets[0].data = data;
                $.DashBoardControls.TripStatusChart.update();
                $("#acceptedtripsli").text(round((data[0]) / (data[0] + data[1] + data[2]) * 100, 2) + "%");
                $("#invitedtripsli").text(round((data[1]) / (data[0] + data[1] + data[2]) * 100, 2) + "%");
                $("#requestedtripsli").text(round((data[2]) / (data[0] + data[1] + data[2]) * 100, 2) + "%");
            },
            error: function () {

            }
        }); //Accepted,Invited,Requeste  Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/PointsSystem",
            data: JSON,
            success: function (data) {
                $.DashBoardControls.PointsSystemChart.data.datasets[0].data = data;
                $.DashBoardControls.PointsSystemChart.update();
                $("#upli").text(round(data[0], 2));
                $("#dpli").text(round(data[1], 2));
                $("#rpli").text(round(data[2], 2));

            },
            error: function () {

            }
        }); //Points System  Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/CarbonSavings",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {
                if (data === null)
                    $("#CarbonSavingsheader").text("0");
                else
                    $("#CarbonSavingsheader").text(round(data, 2) + data);


            },
            error: function () {

            }
        }); //Carbon Savings
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/PassengerTripsPerBranch",
            data: { 'fromDate': fromDate, 'toDate': toDate },
            success: function (data) {

                $.DashBoardControls.PassengerTripsBranchChart.data.datasets[0].data = data[0];
                $.DashBoardControls.PassengerTripsBranchChart.update();
            },
            error: function () {

            }
        }); // INCOMPLETE* Trips Per Branch
    });
    if ($("#fromPeriod_picker").val() === "" || $("#toPeriod_picker").val() === "")
    {
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/MaleFemaleChart",
            data: JSON,
            success: function (data) {
                var ctx = $("#MaleFemalePieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#0026ff",
                            "#FF6384"
                        ],
                        label: 'Male Users vs. Female Users' // for legend
                    }],
                    labels: [
                        "Male Users",
                        "Female Users",
                    ]
                };
                pieData.datasets[0].data = data;
               
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.MaleFemaleChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------
               
            },
            error: function () {

            }
        }); //Males vs. Females Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/HaveCarChart",
            data: JSON,
            success: function (data) {
                var ctx = $("#HaveCarPieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#ff0000",
                            "#00ff21"
                        ],
                        label: 'Users With Cars vs. Users Without Cars' // for legend
                    }],
                    labels: [
                        "Users With Cars",
                        "Users Without Cars",
                    ]
                };
                pieData.datasets[0].data = data;
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.CarsNoCarsChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------


            },
            error: function () {

            }
        }); //Users with Cars vs. Users without Cars 
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TotalTripsPerBranch",
            data: JSON,
            success: function (data) {
                var ctx = $("#TotalTripsPerBranchBarChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [],
                        //backgroundColor: [
                        //    "#ff0000",
                        //    "#00ff21"
                        //],
                        label: 'Total Number of Trips per Branch' // for legend
                    }],
                    labels: [],
                };
                pieData.datasets[0].data = data[0];
                pieData.datasets[0].backgroundColor = data[2];
                pieData.labels = data[1];
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
animation:false,
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    legend: {
                        display: false
                    },
                    categoryPercentage: 1,
                    barPercentage: 1
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.TotalTripsBranchChart = new Chart(ctx, {
                    type: 'bar',
                    data: pieData,
                    options: pieOptions
                });

                //-----------------
                //- END PIE CHART -
                //-----------------

            },
            error: function () {

            }
        }); // INCOMPLETE* Total Trips Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TripsPerBranch",
            data: JSON,
            success: function (data) {
                var ctx = $("#TripsPerBranchBarChart");

                var pieData = {
                    datasets: [
                        {
                            data: data[0],
                            backgroundColor: "green",
                        //backgroundColor: [
                        //    "#ff0000",
                        //    "#00ff21"
                        //],
                        label: 'Number of Driver Trips per Branch' // for legend
                        },
                        {
                            data: data[2],
                            backgroundColor: "red",
                          
                            label: 'Number of Deleted Driver Trips per Branch' // for legend
                        }
                    ],
                    labels: data[1],
                };
                //pieData.datasets[0].data = data[0];
                //pieData.datasets[0].backgroundColor = data[2];
                //pieData.labels = data[1];
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle

                    animation: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    legend: {
                        display: false
                    },
                    categoryPercentage: 1,
                    barPercentage: 1
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.TripsBranchChart = new Chart(ctx, {
                    type: 'bar',
                    data: pieData,
                    options: pieOptions
                });

                //-----------------
                //- END PIE CHART -
                //-----------------

            },
            error: function () {

            }
        }); // INCOMPLETE* Trips Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TotalKilometersMade",
            data: JSON,
            success: function (data) {
                if (data === null)
                    $("#KilometersMade").text("0");
                else
                    $("#KilometersMade").text(round(data, 2) + data);
               

            },
            error: function () {

            }
        }); //Total Kilometers Made 
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/UsersPerBranch",
            data: JSON,
            success: function (data) {
                var ctx = $("#UsersPerBranchBarChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [],
                        //backgroundColor: [
                        //    "#ff0000",
                        //    "#00ff21"
                        //],
                        label: 'Number of Users per Branch' // for legend
                    }],
                    labels: [],
                };
                pieData.datasets[0].data = data[0];
                pieData.datasets[0].backgroundColor = data[2];
                pieData.labels = data[1];
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle

                    //Number - Amount of animation steps
                    animation: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    legend: {
                        display: false
                    },
                    categoryPercentage: 1,
                    barPercentage: 1
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.UsersBranchChart = new Chart(ctx, {
                    type: 'bar',
                    data: pieData,
                    options: pieOptions
                });

                //-----------------
                //- END PIE CHART -
                //-----------------

            },
            error: function () {

            }
        }); // Users Per Branch
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/ActiveInActiveUsers",
            data: JSON,
            success: function (data) {
                var ctx = $("#ActiveInActiveUsersPieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#ffd800",
                            "#0026ff"
                        ],
                        label: 'Active vs. In-Active Users' // for legend
                    }],
                    labels: [
                        "Active Users",
                        "In-Active Users",
                    ]
                };
                pieData.datasets[0].data = data;
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.ActiveInActiveChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------
                $("#activeusersli").text(round((data[0]) / (data[0] + data[1]) * 100, 2) + "%");
                $("#inactiveusersli").text(round((data[1]) / (data[0] + data[1]) * 100, 2) + "%");

            },
            error: function () {

            }
        }); //Active Users  vs. In-Active Users 
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/RecurrentTrips",
            data: JSON,
            success: function (data) {
                var ctx = $("#TripsvsRecurrentPieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#000000",
                            "#2e0b0b"
                        ],
                        label: 'Trips vs. Recurrent Trips' // for legend
                    }],
                    labels: [
                        "Total Trips",
                        "Recurrent Trips",
                    ]
                };
                pieData.datasets[0].data = data;
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.RecurrentChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------
                $("#tripsli").text(round((data[0]) / (data[0] + data[1]) * 100, 2) + "%");
                $("#recurrentli").text(round((data[1]) / (data[0] + data[1]) * 100, 2) + "%");
            },
            error: function () {

            }
        }); //Trips  vs. Recurrent Users
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TopTenVisited",
            data: JSON,
            success: function (data) {
                var ctx = $("#TopTenVisitedBarChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [],
                        //backgroundColor: [
                        //    "#ff0000",
                        //    "#00ff21"
                        //],
                        label: 'Top Ten Visited Branches' // for legend
                    }],
                    labels: [],
                };
                pieData.datasets[0].data = data[0];
                pieData.datasets[0].backgroundColor = data[2];
                pieData.labels = data[1];
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle

                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    legend: {
                        display: false
                    },
                    categoryPercentage: 1,
                    barPercentage: 1
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.TopTenChart = new Chart(ctx, {
                    type: 'bar',
                    data: pieData,
                    options: pieOptions
                });

                //-----------------
                //- END PIE CHART -
                //-----------------

            },
            error: function () {

            }
        }); // Top Ten Visited Branches
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/TripsStatusChart",
            data: JSON,
            success: function (data) {
                var ctx = $("#TripsStatusPieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#9fdd45",
                            "#33d494",
                            "#554393"
                        ],
                        label: 'Accepted Trips vs. Invited Trips vs. Requested Trips' // for legend
                    }],
                    labels: [
                        "Accepted Trips",
                        "Invited Trips",
                        "Requested Trips"
                    ]
                };
                pieData.datasets[0].data = data;

                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.TripStatusChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------
                $("#acceptedtripsli").text(round((data[0]) / (data[0] + data[1]+data[2]) * 100, 2) + "%");
                $("#invitedtripsli").text(round((data[1]) / (data[0] + data[1] + data[2]) * 100, 2) + "%");
                $("#requestedtripsli").text(round((data[2]) / (data[0] + data[1] + data[2]) * 100, 2) + "%");

            },
            error: function () {

            }
        }); //Accepted,Invited,Requeste  Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/PointsSystem",
            data: JSON,
            success: function (data) {
                var ctx = $("#PointsSystemPieChart");

                var pieData = {
                    datasets: [{
                        data: [],
                        backgroundColor: [
                            "#ff0000",
                            "#0fcb2a",
                            "#0026ff"
                        ],
                        label: 'Pointing System Score' // for legend
                    }],
                    labels: [
                        "User Points",
                        "Driven Points",
                        "Ridden Points"
                    ]
                };
                pieData.datasets[0].data = data;

                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle
                    percentageInnerCutout: 50, // This is 0 for Pie charts
                    //Number - Amount of animation steps
                    animationSteps: 100,
                    //String - Animation easing effect
                    animationEasing: "easeOutBounce",
                    //Boolean - Whether we animate the rotation of the Doughnut
                    animateRotate: true,
                    //Boolean - Whether we animate scaling the Doughnut from the centre
                    animateScale: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    tooltipTemplate: "<%=value %> <%=label%>"
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.PointsSystemChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: pieData,
                    options: pieOptions
                });
                //-----------------
                //- END PIE CHART -
                //-----------------
                $("#upli").text(round(data[0],2));
                $("#dpli").text(round(data[1],2));
                $("#rpli").text(round(data[2],2));

            },
            error: function () {

            }
        }); //Points System  Pie Chart
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/CarbonSavings",
            data: JSON,
            success: function (data) {
                if (data === null)
                    $("#CarbonSavingsheader").text("0");
                else
                    $("#CarbonSavingsheader").text(round(data, 2) + data);


            },
            error: function () {

            }
        }); //Carbon Savings
        $.ajax({
            dataType: "json",
            type: "GET",
            url: "/Home/PassengerTripsPerBranch",
            data: JSON,
            success: function (data) {
                var ctx = $("#PassengerTripsPerBranchBarChart");

                var pieData = {
                    datasets: [
                        {
                            data: data[0],
                            backgroundColor: "green",
                            //backgroundColor: [
                            //    "#ff0000",
                            //    "#00ff21"
                            //],
                            label: 'Number of Passenger Trips per Branch' // for legend
                        },
                        {
                            data: data[2],
                            backgroundColor: "red",

                            label: 'Number of Deleted Passenger Trips per Branch' // for legend
                        }
                    ],
                    labels: data[1],
                };
                //pieData.datasets[0].data = data[0];
                //pieData.datasets[0].backgroundColor = data[2];
                //pieData.labels = data[1];
                var pieOptions = {
                    //Boolean - Whether we should show a stroke on each segment
                    segmentShowStroke: true,
                    //String - The colour of each segment stroke
                    segmentStrokeColor: "#fff",
                    //Number - The width of each segment stroke
                    segmentStrokeWidth: 1,
                    //Number - The percentage of the chart that we cut out of the middle

                    animation: false,
                    //Boolean - whether to make the chart responsive to window resizing
                    responsive: true,
                    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
                    maintainAspectRatio: false,
                    //String - A legend template
                    legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<segments.length; i++){%><li><span style=\"background-color:<%=segments[i].fillColor%>\"></span><%if(segments[i].label){%><%=segments[i].label%><%}%></li><%}%></ul>",
                    //String - A tooltip template
                    legend: {
                        display: false
                    },
                    categoryPercentage: 1,
                    barPercentage: 1
                };
                //Create pie or douhnut chart
                // You can switch between pie and douhnut using the method below.
                $.DashBoardControls.PassengerTripsBranchChart = new Chart(ctx, {
                    type: 'bar',
                    data: pieData,
                    options: pieOptions
                });

                //-----------------
                //- END PIE CHART -
                //-----------------

            },
            error: function () {

            }
        }); // INCOMPLETE* Passenger Trips Per Branch
    }


   
});
