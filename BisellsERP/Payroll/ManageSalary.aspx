<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageSalary.aspx.cs" Inherits="BisellsERP.Payroll.ManageSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Manage Salary</title>
    <style>
        .mini-stat.clearfix.bx-shadow {
            height: 90px;
        }

        /*#btnLoad {
            height: 30px;
            padding: 0 18px;
        }*/

        .btn-filter {
            background-color: whitesmoke;
            transition: all 300ms ease;
            padding: 4px 8px;
        }

            .btn-filter:hover, .btn-filter:active, .btn-filter:focus {
                box-shadow: none;
                border: 1.2px solid rgba(239, 83, 80, .8);
            }

        .light-font-color {
            color: #607D8B;
        }

        .panel {
            margin-bottom: 10px;
        }


        tbody tr td {
            padding: 5px !important;
        }

        .panel .panel-body {
            padding: 10px;
        }

        .edit-value {
            background-color: transparent;
            width: 40px;
            text-align: center;
        }

        .portlet .portlet-heading {
            padding: 5px;
            padding-top: 30px;
        }

        .modal-content-h-lg {
            height: 65vh;
            overflow-y: auto;
            background-color: whitesmoke;
        }

        /* Styling Radio Inline Inside Table */
        .radio.radio-inline {
            padding-left: 0;
            padding-right: 15px;
        }
        .radio input[type="radio"] {
            left: -5px;
            top: -5px;
        }
        .radio label::after {
            background-color: #6d9db5;
        }
        /* Styling Disabled Select Box Inside Table */
        .form-control[disabled] {
            background-color: #FAFAFA;
            opacity: .5;
        }
        /* Styling Table Head */
        #listTable thead tr th {
            font-weight: 600;
            background-color: #FAFAFA;
            font-size: 14px;
            color: #6d9db5;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <input type="hidden" id="hdId" value="0" />
        <asp:HiddenField runat="server" Value="0" ID="hdId" ClientIDMode="Static" />
        <%-- ---- Page Title ---- --%>
        <div class="row p-b-10">
            <div class="col-sm-3">
                <h3 class="pull-left page-title m-t-0">Manage Salary</h3>
            </div>

            <div class="col-sm-9">
                <div class="btn-toolbar pull-right" role="group">
                    <button id="btnUpdate" type="button" accesskey="n" data-toggle="tooltip" data-placement="bottom" title="Update Salary Details" class="btn btn-default waves-effect waves-light"><i class="ion ion-compose"></i>&nbsp;Update Preference</button>
                </div>
            </div>

        </div>


        <div class="panel panel-default b-r-8">
            <div class="panel-body">
                <div class="col-sm-6">
                    <div class="row">
                        <div class="col-md-3">
                            <label class="control-label text-muted m-t-5" for="ddlUOM">Select Employee: </label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control input-sm" ID="ddlEmp" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <button type="button" id="btnLoad" class="btn btn-default btn-custom waves-effect waves-light btn-filter"><i class="md md-find-in-page"></i>&nbsp;Filter</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- ---- Data Table ---- --%>
        <div class="row data-table">
            <div class="col-sm-12  p-t-5">
                <div class="panel panel-default b-r-8">
                    <div class="panel-body p-t-10">
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                <table id="listTable" class="table table-hover" style="margin-right: 10px;">
                                    <thead>
                                        <tr>
                                            <th class="hidden">EmpId</th>
                                            <th style="width: 17.66%">Employee Name</th>
                                            <th style="width: 17.66%">User Name</th>
                                            <th style="width: 18.66%">Designation</th>
                                            <th style="width: 23%">Hourly</th>
                                            <th style="width: 23%">Monthly</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
    <script>

        //documnet ready function starts here
        $(document).ready(function () {
            $('#btnUpdate').off().click(function () {
                save();
            });
            var EmpId = $('#ddlEmp').val();
            var companyId = $.cookie('bsl_1');
            $.ajax
                 ({
                     url: $('#hdApiUrl').val() + '/api/Employee/GetSalary/?CompanyId=' + companyId,
                     method: 'POST',
                     dataType: 'JSON',

                     contentType: 'application/json;charset=utf-8',
                     success: function (data) {
                         console.log(data);
                         var response = data;
                         var hour = response.HourlyTemplates;
                         var pay = response.PayrollTemplate;
                         var html = '';
                         $('#listTable').DataTable().destroy();
                         $('#listTable tbody').children().remove();
                         $(response.Employees).each(function (index) {
                             var employee = this;
                             console.log(employee);
                             if (employee.IsHourlyPaid) {
                                 var ddl1 = '<SELECT  class="form-control ddl-hour input-sm"><option value="0">--select--</       option>';
                                 var ddl2 = '<SELECT disabled="true" class="form-control ddl-month input-sm"><option value="0">--select--</option>';
                             }
                             else {

                                 var ddl1 = '<SELECT  disabled="true" class="form-control ddl-hour input-sm"><option value="0">--select--</option>';
                                 var ddl2 = '<SELECT  class="form-control ddl-month input-sm"><option value="0">--select--</option>';
                             }

                             $(hour).each(function () {
                                 if (this.ID == employee.HourlyTemplate) {
                                     ddl1 += '<option value="' + this.ID + '" selected="selected">' + this.Title + '</option>';
                                 } else {
                                     ddl1 += '<option value="' + this.ID + '">' + this.Title + '</option>';
                                 }
                             });
                             $(pay).each(function () {
                                 if (this.ID == employee.MonthlyTemplate) {
                                     ddl2 += '<option value="' + this.ID + '" selected="selected">' + this.Grade + '</option>';
                                 }
                                 else {
                                     ddl2 += '<option value="' + this.ID + '">' + this.Grade + '</option>';
                                 }

                             });
                             html += '<tr>';
                             html += '<td class="hidden">' + this.ID + '</td>';
                             html += '<td>' + this.FirstName + '</td>';
                             html += '<td>' + this.FirstName + '</td>';
                             html += '<td>' + this.Designation + '</td>';
                             if (employee.IsHourlyPaid) {
                                 html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" checked="checked" class="chk-hourly" /><label for="inlineRadio1"></label></div>' + ddl1 + '</td>';
                                 html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" class="chk-payroll"/><label for="inlineRadio1"></label></div>' + ddl2 + '</td>';

                             }
                             else {
                                 html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" class="chk-hourly"/><label for="inlineRadio1"></label></div>' + ddl1 + '</td>';
                                 html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" checked="checked"  class="chk-payroll" /><label for="inlineRadio1"></label></div>' + ddl2 + '</td>';
                             }

                             html += '</tr>';
                         });
                         $('#listTable tbody').append(html);

                         $('#listTable').dataTable();
                         html = '';
                     },
                     error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                 });

            function save() {
                var tbody = $('#listTable > tbody');
                var tr = tbody.children('tr');
                var arr = [];

                for (var i = 0; i < tr.length; i++) {
                    var id = $(tr[i]).children('td:nth-child(1)').text();
                    var hourly = $(tr[i]).children('td:nth-child(5)').children('select').val();
                    var monthly = $(tr[i]).children('td:nth-child(6)').children('select').val();
                    var isHourlyPaid = $(tr[i]).find('.chk-hourly').prop('checked');
                    var detail = {};
                    detail.ID = id;
                    detail.HourlyTemplate = hourly;
                    detail.MonthlyTemplate = monthly;
                    detail.IsHourlyPaid = isHourlyPaid;
                    arr.push(detail);
                }
                $.ajax
                ({
                    url: $('#hdApiUrl').val() + 'api/Employee/SaveSalary',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(arr),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Listing items in the table with EmployeeId
            $(document).on('click', '.btn-filter', function () {
                var EmpId = $('#ddlEmp').val();
                var companyId = $.cookie('bsl_1');
                $.ajax
                     ({
                         url: $('#hdApiUrl').val() + '/api/Employee/GetDetailsForSalary/?EmployeeId=' + EmpId,
                         method: 'POST',
                         dataType: 'JSON',
                         data: companyId,
                         contentType: 'application/json;charset=utf-8',
                         success: function (data) {
                             var response = data;
                             console.log(response)
                             var hour = response.HourlyTemplates;
                             var pay = response.PayrollTemplate;
                             var html = '';
                             $('#listTable').DataTable().destroy();
                             $('#listTable tbody').children().remove();

                             $(response.Employees).each(function (index) {
                                 var employee = this;
                                 if (employee.IsHourlyPaid) {
                                     var ddl1 = '<SELECT class="form-control ddl-hour input-sm"><option value="0">--select--</option>';
                                     var ddl2 = '<SELECT class="form-control ddl-month input-sm" disabled="true"><option value="0">--select--</option>';
                                 }
                                 else {
                                     var ddl1 = '<SELECT disabled="true" class="form-control ddl-hour input-sm"><option value="0">--select--</option>';
                                     var ddl2 = '<SELECT  class="form-control ddl-month input-sm"><option value="0">--select--</option>';
                                 }
                                 $(hour).each(function () {
                                     if (this.ID == employee.HourlyTemplate) {
                                         ddl1 += '<option value="' + this.ID + '" selected="selected">' + this.Title + '</option>';
                                     } else {
                                         ddl1 += '<option value="' + this.ID + '">' + this.Title + '</option>';
                                     }
                                 });
                                 $(pay).each(function () {
                                     if (this.ID == employee.MonthlyTemplate) {
                                         ddl2 += '<option value="' + this.ID + '" selected="selected">' + this.Grade + '</option>';
                                         console.log(ddl1)
                                     }
                                     else {
                                         ddl2 += '<option value="' + this.ID + '">' + this.Grade + '</option>';
                                     }

                                 });
                                 html += '<tr>';
                                 html += '<td class="hidden">' + this.ID + '</td>';
                                 html += '<td>' + this.FirstName + '</td>';
                                 html += '<td>' + this.FirstName + '</td>';
                                 html += '<td>' + this.Designation + '</td>';
                                 if (employee.IsHourlyPaid) {
                                     html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" checked="checked" class="chk-hourly" /><label for="inlineRadio1"></label></div>' + ddl1 + '</td>';
                                     html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" class="chk-payroll"/><label for="inlineRadio1"></label></div>' + ddl2 + '</td>';

                                 }
                                 else {
                                     html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" class="chk-hourly"/><label for="inlineRadio1"></label></div>' + ddl1 + '</td>';
                                     html += '<td><div class="radio radio-inline"><input type="radio" name="payType' + index + '" checked="checked"  class="chk-payroll" /><label for="inlineRadio1"></label></div>' + ddl2 + '</td>';
                                 }
                                 html += '</tr>';
                             });
                             $('#listTable tbody').append(html);

                             $('#listTable').dataTable();
                             html = '';
                         },
                         error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                     });
            })
            $(document).on('change', '.chk-hourly', function () {
                $(this).closest('tr').find('.ddl-hour').removeAttr('disabled');
                $(this).closest('tr').find('.ddl-month').prop('disabled', true);
            });

            $(document).on('change', '.chk-payroll', function () {
                $(this).closest('tr').find('.ddl-hour').prop('disabled', true);
                $(this).closest('tr').find('.ddl-month').removeAttr('disabled');
            });

        });

    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
