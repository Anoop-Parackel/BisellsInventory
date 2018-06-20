<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="BisellsERP.Payroll.Payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Payments</title>
 <style>
        /*tbody tr td {
            padding: 5px !important;
            font-size: larger;
        }*/

        .form-inline .checkbox input[type=checkbox], .form-inline .radio input[type=radio] {
            top: -8px;
            left: -3px;
            height: 15px;
            width: 17px;
        }
        .stat-h {
            min-height: 50vh;
            max-height: 75vh;
            overflow: auto;
        }
         input[name="daterange"] {
            border: none;
            color: #607d8b;
            font-size: 14px;
            /*background-color: #CFD8DC;*/
            background-color: white;
            padding: 6px 6px;
            border-radius: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    
  <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Pay Slip</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
              <span class="filter-span">
                                <label>Filter by Date</label>
                                <input type="text" id="txtDate" name="daterange" value="01/01/2015 - 01/31/2015" />
                            </span>
                </div>
       </div>
    </div>
        <%-- ---- List Table ---- --%>
    <div class="row">
        <div class="col-sm-12">
            <div class="panel panel-default stat-h b-r-8">
                <div class="panel-body p-t-10">
                    <div class="col-sm-12">
                        <table id="listTable" class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="hidden">Id</th>
                                    <th>
                                        <div class="checkbox checkbox-primary">
                                            <input type="checkbox" class="chk-all" /><label></label>
                                        </div>
                                    </th>
                                    <th>Employee</th>
                                    <th>Salary Type</th>
                                    <th>Basic Pay</th>
                                    <th>Hr Allowance</th>
                                    <th>Medical Allowance</th>
                                    <th>TA</th>
                                    <th>DA</th>
                                    <th>PF</th>
                                    <th>Tax Deduction</th>
                                    <th>Net Salary</th>
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
     <!-- Date Range Picker -->
     <%-- <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
       <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />--%>
    <script src="../Theme/assets/moment/moment.min.js"></script>
    <link href="../Theme/assets/daterangepicker/daterangepicker.css" rel="stylesheet" />
    <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <script>
        $(document).ready(function ()
        {
            $('input[name="daterange"]').daterangepicker({
                "opens": "left",
                "startDate": moment().startOf('month'),
                "endDate": moment().endOf('month'),
                "alwaysShowCalendars": false,
                locale: {
                    format: 'DD MMM YYYY'
                },
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            });

            var companyId = $.cookie('bsl_1');
            $.ajax
                 ({
                     url: $('#hdApiUrl').val() + '/api/PaySlip/GetDetailsForPaySlip/?CompanyId=' + companyId,
                     method: 'POST',
                     dataType: 'JSON',
                     contentType: 'application/json;charset=utf-8',
                     success: function (data) {
                         var response = data;
                         var html = '';
                         $('#listTable').DataTable().destroy();
                         $('#listTable tbody').children().remove();
                         $(response).each(function (index) {
                             html += '<tr>';
                             html += '<td class="hidden">' + this.ID + '</td>';
                             html += '<td class="no-sort"><div class="checkbox"><input type="checkbox" name="edit' + index + '"  class="checkbox checkbox-success view chk-edit" /><label></label></div></td>';
                             html += '<td>' + this.Employee + '</td>';
                             html += '<td>' + this.Grade + '</td>';
                             html += '<td contenteditable="true">' +this.BasicSalary + '</td>';
                             html += '<td contenteditable="true">' + this.HouseRentAllowance + '</td>';
                             html += '<td contenteditable="true"> ' + this.MedicalAllowance + '</td>';
                             html += '<td contenteditable="true">' + this.TravellingAllowance + '</td>';
                             html += '<td contenteditable="true">' + this.DearnessAllowance + '</td>';
                             html += '<td contenteditable="true">' + this.ProvidentFund + '</td>';
                             html += '<td contenteditable="true">' + this.TaxDeduction + '</td>';
                             html += '<td contenteditable="true">' + this.NetSalary + '</td>';
                            // html += '<td><input type="number" disabled="true" class="edit-value ins-val" value="' + this.Stock + '"/></td>';
                             //html += '<td>0</td>';
                             html += '</tr>';
                         });
                         $('#listTable tbody').append(html);
                         $('#listTable').dataTable({
                         });
                         html = '';
                     },
                     error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                 });

        });
        </script>
  <link href="/Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="/Theme/assets/datatables/datatables.min.js"></script>
    <script src="/Theme/Custom/Commons.js"></script>
    <link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
