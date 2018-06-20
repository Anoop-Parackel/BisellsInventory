<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Attendance.aspx.cs" Inherits="BisellsERP.Payroll.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Attendance</title>
    <style>
        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            vertical-align: middle;
        }

        input[name="daterange"] {
            display: inline-block;
            font-size: 14px;
            background-color: transparent;
            border: none;
        }

        .attendance-marker {
            position: absolute;
            top: 8px;
            padding: 0 5px;
        }

        .attendance-box {
            position: relative;
            padding: 0 !important;
            height: 40px;
            vertical-align: middle !important;
        }

        small {
            font-size: 80%;
            font-weight: 100;
        }

        [data-attendance='1'] {
            color: #33b86c;
        }

        [data-attendance='2'] {
            color: #ef5350;
        }
        .today-highlight {
            background-color: #E3F2FD;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-8">
            <h3 class="page-title m-t-0">Attendance&nbsp;<input type="text" name="daterange" id="dateRange" value="01/01/2015 - 01/31/2015" /></h3>
        </div>
        <div class="col-sm-4">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" id="btnSave" accesskey="s" data-toggle="tooltip" data-placement="bottom" title="Mark Attendance" class="btn btn-default waves-effect waves-light "><i class=" md-done"></i>&nbsp;Mark Attendance</button>
            </div>
        </div>
    </div>

    <%-- ---- Attendance Panel ---- --%>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel b-r-8">
                <div class="panel-body p-10 p-b-0">
                    <table id="tblAttendance" class="table m-b-0">
                        <thead>
                            <tr>
                                <th style="width: 35px">
                                    <div class="checkbox mark-all-employee">
                                        <input type="checkbox" />
                                        <label for=""></label>
                                    </div>
                                </th>
                                <th></th>
                                <th class="text-center">
                                    <p><small class="attendance-day">MON</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">TUE</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">WED</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">THU</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">FRI</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">SAT</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                                <th class="text-center">
                                    <p><small class="attendance-day">SUN</small></p>
                                    <p class="m-t-0"><small class="text-muted attendance-date" data-attendance-date="">Feb 13</small></p>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="ltrEmpList" runat="server"></asp:Literal>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <!-- Date Range Picker -->
    
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
     <script src="../Theme/Custom/Commons.js"></script>
    <script>

        //Setting This Week Dates as default function starts here 
        function LoadAttendanceDetails(startDate, endDate)
        {
            //Reset data on the page 
            $('.attendance').html('-').attr('data-attendance', '0');

            var companyId = $.cookie('bsl_1');
            $.ajax
                ({
                    url: $('#hdApiUrl').val() + '/api/Attendance/Get?CompanyId=' + companyId + '&From=' + startDate + '&To=' + endDate,
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        console.log(data);
                        $(data).each(function (index) {
                            var row = $('#tblAttendance tbody').find('[data-empid="' + this.EmployeeId + '"]').closest('tr').index();

                            var column = $('#tblAttendance thead').find('[data-attendance-date="' + this.DateString + '"]').closest('th').index();

                            var element = $('#tblAttendance tbody').children('tr').eq(row).children('td').eq(column).children('span.attendance');
                            element.attr("data-attendance", this.AttendanceStatus);
                            element.trigger('change');

                        })
                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); loading('stop', null); },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
        }
        //Setting this week dates as default function ends here

        //function for finding dates
        function loadingdates(type)
        {
            var startDate;
            var endDate;
           
            type ? endDate = moment().endOf('week').add(1, 'day').format('DD-MMM-YYYY') : endDate = $('input[name="daterange"]').data('daterangepicker').endDate.format('DD-MMM-YYYY');
            type ? startDate = moment().startOf('week').add(1, 'day').format('DD-MMM-YYYY') : startDate =  $('input[name="daterange"]').data('daterangepicker').startDate.format('DD-MMM-YYYY');
            for (i = 0; i < $('.attendance-date').length; i++) {
                var displayDate = moment(moment(startDate).add(i, 'day')._d).format('MMM DD');
                var displayDay = moment(moment(startDate).add(i, 'day')._d).format('ddd');
                var attrDate = moment(moment(startDate).add(i, 'day')._d).format('DD-MMM-YYYY');
                $('.attendance-date').eq(i).html(moment(displayDate).format('MMM DD')).attr('data-attendance-date', attrDate);
                $('.attendance-day').eq(i).html(displayDay);
            }
            var todayPos = $('#tblAttendance').find('[data-attendance-date="' + moment().format("DD-MMM-YYYY") + '"]').closest('th').index();
            if (todayPos > 1) {
                $('td').removeClass('today-highlight');
                $('#tblAttendance tbody tr').find('td:nth-of-type(' + (todayPos + 1) + ')').addClass('today-highlight');
            }
            else {
                $('td').removeClass('today-highlight');
            }
            //function call for loading attendance details
            LoadAttendanceDetails(startDate,endDate);
        };
        

        // DOCUMENT READY EVENT STARTS HERE
        $(document).ready(function ()
        {

            loadingdates(true);

            // Adding buttons for present or absent marking
            $(document).on('click', '.attendance-box', function ()
            {
                var markerAvailable = $(this).children('div').hasClass('attendance-marker');
                var attendanceMarker = '<div class="attendance-marker btn-group btn-group-justified"><a href="#" class="btn btn-default btn-xs btn-attendance">-</a><a href="#" class="btn btn-default btn-xs text-success btn-attendance">P</a><a href="#" class="btn btn-default btn-xs text-danger btn-attendance">A</a></div>';
                if (!markerAvailable) { $(this).append(attendanceMarker); }
            });

            //Function call for saving the attendance details
            $('#btnSave').off().click(function () {
                Save();
            });

            //Save function for marking attendance starts here
            function Save()
            {
                var arr = [];
                var tbody = $('#tblAttendance > tbody');
                var tr = tbody.children('tr');
                var column = $("#tblAttendance  > thead > tr:first > th");
                for (var i = 0; i < tr.length; i++)
                {
                    for (var j = 2; j < 9; j++)
                    {
                        var attendance = {};
                        attendance.EmployeeId = $(tr[i]).children('td:nth-child(2)').attr('data-empid');
                        attendance.Date = $(column).eq(j).find('.attendance-date').attr('data-attendance-date');
                        attendance.AttendanceStatus = $(tr[i]).children('td').eq(j).find('.attendance').attr('data-attendance');
                        attendance.CreatedBy = $.cookie('bsl_3');
                        attendance.ModifiedBy = $.cookie('bsl_3');
                        attendance.CompanyId = $.cookie('bsl_1');
                        arr.push(attendance);
                    }
                }
                console.log(arr);
                $.ajax
               ({
                   url: $('#hdApiUrl').val() + 'api/Attendance/Save',
                   method: 'POST',
                   contentType: 'application/json; charset=utf-8',
                   dataType: 'Json',
                   data: JSON.stringify(arr),
                   success: function (data) {
                       var response = data;
                       if (response.Success) {
                           successAlert(response.Message);
                          // $('.attendance').html('-').attr('data-attendance', '0');
                       }
                       else {
                           errorAlert(response.Message);
                       }
                   },
                   error: function (xhr) {
                       alert(xhr.responseText);
                       console.log(xhr);
                       miniLoading('stop');
                   },
                   beforeSend: function () { miniLoading('start') },
                   complete: function () { miniLoading('stop') },
               });
            }
            //Save function ends here

            $(document).on('change', 'span.attendance', function () {
                
                var status = 0;
                status =Number( $(this).attr('data-attendance'));
                console.log(status);
                switch (status) {
                    case 1:
                        $(this).html('P');
                        break;
                    case 2:
                        $(this).html('A');
                        break;
                    case 0:
                        $(this).html('-');
                        break;
                }
            });

            // Marking All Employees
            $('body').on('change', '.mark-all-employee', function () {
                var checked = $(this).find('input[type="checkbox"]').prop('checked');
                var employeeMarker = $('#tblAttendance').find('.mark-employee').find('input[type="checkbox"]');
                checked ? employeeMarker.prop('checked', true) : employeeMarker.prop('checked', false);
            });

            // [data-attendance] attribute value def
            // 0 -  Attendance not marked
            // 1 -  Present Attendance
            // 2 -  Absent Attendance

            // Marking Employee Attendance
            $('body').on('click', '.btn-attendance', function () {
                var attendanceText = $(this).html();
                var attendanceStatus = 0;
                switch (attendanceText) {
                    case '-':
                        attendanceStatus = 0;
                        break;
                    case 'P':
                        attendanceStatus = 1;
                        break;
                    case 'A':
                        attendanceStatus = 2;
                        break;
                    default:
                        break;
                }
                var columnPos = $(this).closest('td').index();
                var markedEmployees = $('#tblAttendance').find('.mark-employee').find('input[type="checkbox"]:checked');
                $(markedEmployees).each(function () {
                    $(this).closest('tr').children('td').eq(columnPos).find('.attendance').attr('data-attendance', attendanceStatus).html(attendanceText);
                });

                $(this).closest('.attendance-box').find('.attendance').attr('data-attendance', attendanceStatus).html(attendanceText);
                $('.attendance-marker').remove();
            });

            // Daterangepicker Init
            $('input[name="daterange"]').daterangepicker({
                "opens": "right",
                "startDate": moment().startOf('week').add(1, 'day'),
                "endDate": moment().endOf('week').add(1, 'day'),
                "alwaysShowCalendars": false,
                "dateLimit": {
                    "days": 7
                },
                locale: {
                    format: 'DD MMM YYYY'
                },
                ranges: {
                    'This Week': [moment().startOf('week').add(1, 'day'), moment().endOf('week').add(1, 'day')],
                    'Last Week': [moment().startOf('week').add(1, 'day').subtract(1, 'week'), moment().endOf('week').add(1, 'day').subtract(1, 'week')],
                    //'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            });

            // Applying Daterangepicker Dates to Attendance Table
            $('input[name="daterange"]').on('apply.daterangepicker', function (ev, picker) {
                loadingdates(false);
            });

        });
        //Document ready function ends here
    </script>
</asp:Content>
