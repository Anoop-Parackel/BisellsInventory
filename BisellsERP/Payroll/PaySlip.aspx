<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PaySlip.aspx.cs" Inherits="BisellsERP.Payroll.PaySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PaySlip</title>
    <style>
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
            display: inline-block;
            font-size: 16px;
            background-color: transparent;
            border: none;
        }

        #editModal .control-label {
            color: #546E7A;
            font-weight: 100;
        }

        .left-side {
            height: calc(100vh - 198px);
            overflow-y: auto;
            overflow-x: hidden;
        }

            .left-side table tbody td {
                /*padding: 15px 10px;*/
                cursor: pointer;
            }

            .left-side table thead td {
                padding: 4px 10px;
            }

            .left-side table tbody tr.active td, .left-side table tbody tr.active th {
                background-color: rgba(30, 136, 229, 0.1);
            }

            .left-side table tbody tr.active, .left-side table tbody tr.active {
                border-left: 10px solid #1E88E5;
            }

        .form-inline .checkbox input[type=checkbox], .form-inline .radio input[type=radio] {
            top: -8px;
            left: -3px;
            height: 15px;
            width: 17px;
        }
        .btn-save{
            width:60px;
            height:30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-5">
            <h3 class="page-title m-t-0">Generate Payslip&nbsp;<input type="text" name="daterange" id="daterange" value="Feb-2018" /></h3>
        </div>
        <div class="col-sm-7">
            <div class="btn-toolbar pull-right" role="group">
                <button id="btnPost" accesskey="s" type="button" data-toggle="modal" data-target="#modalSave" class="btn btn-default waves-effect waves-light"><i class="ion-checkmark-round btn-save"></i>&nbsp;Post Salary</button>
            </div>
        </div>
    </div>
    <%-- ---- List Table ---- --%>
    <div class="left-side" style="background-color: white;">
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <table id="listTable" class="table left-table invoice-list">
                        <thead>
                            <tr>
                                <th class="hidden">Id</th>
                                <th>
                                    <div class="checkbox checkbox-primary">
                                        <input type="checkbox" class="chk-all" /><label></label>
                                    </div>
                                </th>
                                <th>Employee</th>
                                <th>Salary Template</th>
                                <th>Basic Salary</th>
                                <th>Total Deduction</th>
                                <th class="hidden">HR</th>
                                <th class="hidden">Medical</th>
                                <th class="hidden">Travelling</th>
                                <th class="hidden">Dearness</th>
                                <th class="hidden">SecurityDep</th>
                                <th class="hidden">PF</th>
                                <th class="hidden">Tax Deduction</th>
                                <th class="hidden">Total Allowance</th>
                                <th class="hidden">Incentives</th>
                                <th class="hidden">Gross</th>
                                <th class="hidden">LeaveDeduction</th>
                                <th>Net Salary</th>
                                <th class="hidden">NoOfLeave</th>
                                <th class="hidden">TotalWorkingDays</th>
                                <th class="hidden">TotalAttendance</th>
                                <th class="hidden">TotalHolidays</th>
                                <th class="hidden">PaymentStatus</th>
                                <th>#</th>
                                <th> </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal for updating salary-->
    <div id="editModal" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row">
                        <div class="col-xs-8">
                            <h4 class="modal-title" id="myModalLabel">Edit Details of <b>
                                <label id="lblEmployee"></label></b></h4>
                        </div>
                         <div class="col-xs-4 checkbox checkbox-inline checkbox-primary">
                                <asp:CheckBox ID="chkPayment" ClientIDMode="Static" runat="server" Text="Payment" />
                            </div>
                    </div>
                </div>
                <div class="modal-body p-0">
                    <div class="row before-send p-t-25">
                        <input id="hdEmployeeId" type="hidden" value="0" />
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Basic Pay</label>
                                    <input type="text" class="form-control" id="txtBp" value="" placeholder="Basic Pay" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">HR Allowance</label>
                                    <input type="text" class="form-control" id="txtHr" value="" placeholder="Hr Allowance" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Medical Allowance</label>
                                    <input type="text" class="form-control" id="txtMdecical" value="" placeholder="Medical Allowance" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Travelling Allowance</label>
                                    <input type="text" class="form-control" id="txtTa" value="" placeholder="Travelling Allowance" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Dearness Allowance</label>
                                    <input type="text" class="form-control" id="txtDa" value="" placeholder="Dearness Allowance" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Incentives</label>
                                    <input type="text" class="form-control" id="txtInc" value="" placeholder="Incentives" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Provident Fund</label>
                                    <input type="text" class="form-control" id="txtPf" value="" placeholder="Provident Fund" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Security Deposit</label>
                                    <input type="text" class="form-control" id="txtSecDep" value="" placeholder="Security Deposit" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Tax Deduction</label>
                                    <input type="text" class="form-control" id="txtTax" value="" placeholder="Tax Deduction" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Leave Deduction</label>
                                    <input type="text" class="form-control" id="txtLeave" value="" placeholder="Leave Deduction" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Total Allowance</label>
                                    <input type="text" class="form-control" id="txtTotalAllow" value="" placeholder="Total Allowance" />
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group">
                                    <label class="control-label">Total Deduction</label>
                                    <input type="text" class="form-control" id="txtTotDeduc" value="" placeholder="Total Deduction" />
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="control-label">Gross Salary</label>
                                    <input type="text" class="form-control" id="txtGross" value="" placeholder="Gross Salary" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="control-label">Net Salary</label>
                                    <input type="text" class="form-control" id="txtNet" value="" placeholder="Net Salary" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-15">
                            <div class="btn-toolbar pull-right">
                                <button id="btnUpdate" type="button" class="btn btn-default m-t-5"><i class="md-done"></i>&nbsp;Update</button>
                                <button type="button" id="btnCancel" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--  Modal for Save--%>
    <div id="modalSave" class="modal fade-scale" role="dialog">
        <div class="modal-dialog center-me m-0">

            <!-- Modal content-->
            <div class="modal-content m-10">
                <div class="modal-body p-0">
                    <div class="row">
                        <div class="before-send">
                            <h2 class="text-center">Save ?</h2>
                            <p class="text-center">Are you sure you want to Save?</p>
                           
                            <div class="col-sm-12 text-center m-t-20">
                                <button id="btnSave" type="button" class="btn btn-default m-t-5">Save&nbsp;<i class="md-done"></i></button>
                                <button type="button" class="btn btn-inverse m-t-5" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <script>
        //Document ready function starts here
        $(document).ready(function ()
        {
            //date initialization
            $("#daterange").datepicker({
                format: "M-yyyy",
                viewMode: "months",
                minViewMode: "months",
                autoclose: true
            });

            //Function call for Load details from payslip
            LoadDetails(new Date());

            //function call for load payslip details while changing date
            $(document).on('changeDate', '#daterange', function () {
                LoadDetails(new Date(Date.parse('01-' + $('#daterange').val())));
            });

            //Load details from payslip
            function LoadDetails(Initdate) {
                var companyId = $.cookie('bsl_1');
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', ];
                var firstDate = new Date(Date.parse('01/' + months[Initdate.getMonth()] + '/' + Initdate.getFullYear()));
                var lastDay = new Date(firstDate.getFullYear(), firstDate.getMonth() + 1, 0);
                var lastDate = lastDay.getDate() + '/' + months[lastDay.getMonth()] + '/' + lastDay.getFullYear();
                var firstDay = firstDate.getDate() + '/' + months[firstDate.getMonth()] + '/' + firstDate.getFullYear();
                $.ajax
                  ({
                      url: $('#hdApiUrl').val() + '/api/PaySlip/GetDetailsForPaySlip/?CompanyId=' + companyId + '&From=' + firstDay + '&To=' + lastDate,
                      method: 'POST',
                      dataType: 'JSON',
                      contentType: 'application/json;charset=utf-8',
                      success: function (data) {
                          var response = data;
                          console.log(response);
                          if (response != null && response.length > 0) {
                              var html = '';
                              $(response).each(function (index) {
                                  
                                  html += '<tr>';
                                  html += '<td class="hidden">' + this.EmployeeId + '</td>';
                                  html += '<td class="no-sort"><div class="checkbox"><input type="checkbox" name="edit' + index + '"  class="checkbox checkbox-success view chk-edit" /><label></label></div></td>';
                                  html += '<td>' + this.Employee + '&nbsp;' + this.EmployeeLastName + '</td>';
                                  html += '<td>' + this.Grade + '</td>';
                                  html += '<td>' + this.BasicSalary + '</td>';
                                  html += '<td>' + this.TotalDeduction + '</td>';
                                  html += '<td class="hidden">' + this.HouseRentAllowance + '</td>';
                                  html += '<td class="hidden">' + this.MedicalAllowance + '</td>';
                                  html += '<td class="hidden">' + this.TravellingAllowance + '</td>';
                                  html += '<td class="hidden">' + this.DearnessAllowance + '</td>';
                                  html += '<td class="hidden">' + this.SecurityDeposit + '</td>';
                                  html += '<td class="hidden">' + this.ProvidentFund + '</td>';
                                  html += '<td class="hidden">' + this.TaxDeduction + '</td>';
                                  html += '<td class="hidden">' + this.TotalAllowance + '</td>';
                                  html += '<td class="hidden">' + this.Incentives + '</td>';
                                  html += '<td class="hidden">' + this.GrossSalary + '</td>';
                                  html += '<td class="hidden">' + this.LeaveDeduction + '</td>';
                                  html += '<td>' + this.NetSalary + '</td>';
                                  html += '<td class="hidden">' + this.TotalLeave + '</td>';
                                  html += '<td class="hidden">' + this.TotalWorkingDays + '</td>';
                                  html += '<td class="hidden">' + this.TotalAttendance + '</td>';
                                  html += '<td class="hidden">' + this.TotalHolidays + '</td>';
                                  html += '<td class="hidden">' + this.PaymentStatus + '</td>';
                                  html += '<td <a href="#" data-toggle="modal" data-placement="auto left" title="Edit" data-target="#editModal" class="edit-entry"><i class="fa fa-edit"></i></a></td>';
                                  html += '<td><button type="button" class="btn btn-default btn-sm btn-save">Print</button></td>';
                                  html += '</tr>';
                              });
                              $('#listTable tbody').children().remove();
                              $('#listTable tbody').append(html);
                              html = '';
                              //Edit salaries of a single employee
                              $('#listTable').off().on('click', '.edit-entry', function () {
                                  //Find current row
                                  var tr = $(this).closest('tr');
                                  var id = $(this).closest('tr').children('td').eq(0).text();
                                  var employee = $(this).closest('tr').children('td').eq(2).text();
                                  var bp = $(this).closest('tr').children('td').eq(4).text();
                                  var hr = $(this).closest('tr').children('td').eq(6).text();
                                  var ma = $(this).closest('tr').children('td').eq(7).text();
                                  var ta = $(this).closest('tr').children('td').eq(8).text();
                                  var da = $(this).closest('tr').children('td').eq(9).text();
                                  var secDep = $(this).closest('tr').children('td').eq(10).text();
                                  var pf = $(this).closest('tr').children('td').eq(11).text();
                                  var tax = $(this).closest('tr').children('td').eq(12).text();
                                  var gross = $(this).closest('tr').children('td').eq(15).text();
                                  var totAllow = $(this).closest('tr').children('td').eq(13).text();
                                  var totDeduc = $(this).closest('tr').children('td').eq(5).text();
                                  var inc = $(this).closest('tr').children('td').eq(14).text();
                                  var net = $(this).closest('tr').children('td').eq(17).text();
                                  var leave = $(this).closest('tr').children('td').eq(16).text();
                                  var status = $(this).closest('tr').children('td').eq(22).text();

                                  $('#hdEmployeeId').val(id);
                                  $('#lblEmployee').text(employee);
                                  $('#txtBp').val(bp);
                                  $('#txtHr').val(hr);
                                  $('#txtMdecical').val(ma);
                                  $('#txtTa').val(ta);
                                  $('#txtDa').val(da);
                                  $('#txtSecDep').val(secDep);
                                  $('#txtPf').val(pf);
                                  $('#txtTax').val(tax);
                                  $('#txtGross').val(gross);
                                  $('#txtTotalAllow').val(totAllow);
                                  $('#txtTotDeduc').val(totDeduc);
                                  $('#txtInc').val(inc);
                                  $('#txtLeave').val(leave);
                                  $('#txtNet').val(net);
                                  if (status) {
                                      $('#chkPayment').prop('checked', true);
                                  }
                                  else {
                                      $('#chkPayment').prop('checked', false);
                                  }
                                 
                                  //Updating salary on update button click
                                  $('#btnUpdate').off().click(function () {
                                      tr.children('td:nth-child(5)').text($('#txtTotDeduc').val());
                                      tr.children('td:nth-child(18)').text($('#txtNet').val());
                                      tr.children('td:nth-child(7)').text($('#txtHr').val());
                                      tr.children('td:nth-child(8)').text($('#txtMdecical').val());
                                      tr.children('td:nth-child(9)').text($('#txtTa').val());
                                      tr.children('td:nth-child(10)').text($('#txtDa').val());
                                      tr.children('td:nth-child(11)').text($('#txtSecDep').val());
                                      tr.children('td:nth-child(12)').text($('#txtPf').val());
                                      tr.children('td:nth-child(13)').text($('#txtTax').val());
                                      tr.children('td:nth-child(14)').text($('#txtTotalAllow').val());
                                      tr.children('td:nth-child(6)').text($('#txtTotDeduc').val());
                                      tr.children('td:nth-child(15)').text($('#txtInc').val());
                                      tr.children('td:nth-child(16)').text($('#txtGross').val());
                                      tr.children('td:nth-child(17)').text($('#txtLeave').val());
                                      tr.children('td:nth-child(18)').text($('#txtNet').val());
                                      $('#editModal').modal("hide");
                                  });
                              });
                               $('#btnPost').hide();
                          }
                          else {
                              LoadFromTemplate(firstDate);
                          }
                      },
                      error: function (xhr) { alert(xhr.responseText); console.log(xhr); loading('stop', null); },
                      beforeSend: function () { loading('start', null) },
                      complete: function () { loading('stop', null); }
                  });
            }

            //Function for loading salary details of employee from salary template
            function LoadFromTemplate(date) {
                var companyId = $.cookie('bsl_1');
                var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', ];
                var firstDate = firstDay.getDate() + '/' + months[firstDay.getMonth()] + '/' + firstDay.getFullYear();
                var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                var lastDate = lastDay.getDate() + '/' + months[lastDay.getMonth()] + '/' + lastDay.getFullYear();

                $.ajax
                    ({
                        url: $('#hdApiUrl').val() + '/api/PaySlip/GetDetailsForPayment/?CompanyId=' + companyId + '&From=' + firstDate + '&To=' + lastDate,
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        success: function (data) {
                            var response = data;
                            console.log(response);
                            var html = '';
                            $(response).each(function (index)
                            {
                                var totalWorkindDay = lastDay.getDate() - this.TotalHolidays;
                                var totalAttendance = totalWorkindDay - this.TotalLeave;
                                var netSalaryPerDay = this.NetSalary / totalWorkindDay;
                                var deductionAmount = netSalaryPerDay * this.TotalLeave;
                                var ldeductionAmount = parseFloat(deductionAmount.toFixed(2));
                                var netSalaryToPay = this.NetSalary - deductionAmount;
                                var lnetSalaryToPay = parseFloat(netSalaryToPay.toFixed(2));
                                var totalDeduction = this.TotalDeduction + ldeductionAmount;

                                html += '<tr>';
                                html += '<td class="hidden">' + this.EmployeeId + '</td>';
                                html += '<td class="no-sort"><div class="checkbox"><input type="checkbox" name="edit' + index + '"  class="checkbox checkbox-success view chk-edit" /><label></label></div></td>';
                                html += '<td>' + this.Employee + '&nbsp;' + this.EmployeeLastName + '</td>';
                                html += '<td>' + this.Grade + '</td>';
                                html += '<td>' + this.BasicSalary + '</td>';
                                html += '<td>' + totalDeduction + '</td>';
                                html += '<td class="hidden">' + this.HouseRentAllowance + '</td>';
                                html += '<td class="hidden">' + this.MedicalAllowance + '</td>';
                                html += '<td class="hidden">' + this.TravellingAllowance + '</td>';
                                html += '<td class="hidden">' + this.DearnessAllowance + '</td>';
                                html += '<td class="hidden">' + this.SecurityDeposit + '</td>';
                                html += '<td class="hidden">' + this.ProvidentFund + '</td>';
                                html += '<td class="hidden">' + this.TaxDeduction + '</td>';
                                html += '<td class="hidden">' + this.TotalAllowance + '</td>';
                                html += '<td class="hidden">' + this.Incentives + '</td>';
                                html += '<td class="hidden">' + lnetSalaryToPay + '</td>';
                                html += '<td class="hidden">' + ldeductionAmount + '</td>';
                                html += '<td>' + lnetSalaryToPay + '</td>';
                                html += '<td class="hidden">' + this.TotalLeave + '</td>';
                                html += '<td class="hidden">' + totalWorkindDay + '</td>';
                                html += '<td class="hidden">' + totalAttendance + '</td>';
                                html += '<td class="hidden">' + this.TotalHolidays + '</td>';
                                html += '<td <a href="#" data-toggle="modal" data-placement="auto left" title="Edit" data-target="#editModal" class="edit-entry"><i class="fa fa-edit"></i></a></td>'
                                html += '</tr>';
                            });
                            $('#listTable tbody').children().remove();
                            $('#listTable tbody').append(html);
                            html = '';
                            //Edit salaries of a single employee
                            $('#listTable').off().on('click', '.edit-entry', function () {
                                //Find current row
                                var tr = $(this).closest('tr');
                                var id = $(this).closest('tr').children('td').eq(0).text();
                                var employee = $(this).closest('tr').children('td').eq(2).text();
                                var bp = $(this).closest('tr').children('td').eq(4).text();
                                var hr = $(this).closest('tr').children('td').eq(6).text();
                                var ma = $(this).closest('tr').children('td').eq(7).text();
                                var ta = $(this).closest('tr').children('td').eq(8).text();
                                var da = $(this).closest('tr').children('td').eq(9).text();
                                var secDep = $(this).closest('tr').children('td').eq(10).text();
                                var pf = $(this).closest('tr').children('td').eq(11).text();
                                var tax = $(this).closest('tr').children('td').eq(12).text();
                                var gross = $(this).closest('tr').children('td').eq(15).text();
                                var totAllow = $(this).closest('tr').children('td').eq(13).text();
                                var totDeduc = $(this).closest('tr').children('td').eq(5).text();
                                var inc = $(this).closest('tr').children('td').eq(14).text();
                                var net = $(this).closest('tr').children('td').eq(17).text();
                                var leave = $(this).closest('tr').children('td').eq(16).text();

                                $('#hdEmployeeId').val(id);
                                $('#lblEmployee').text(employee);
                                $('#txtBp').val(bp);
                                $('#txtHr').val(hr);
                                $('#txtMdecical').val(ma);
                                $('#txtTa').val(ta);
                                $('#txtDa').val(da);
                                $('#txtSecDep').val(secDep);
                                $('#txtPf').val(pf);
                                $('#txtTax').val(tax);
                                $('#txtGross').val(gross);
                                $('#txtTotalAllow').val(totAllow);
                                $('#txtTotDeduc').val(totDeduc);
                                $('#txtInc').val(inc);
                                $('#txtLeave').val(leave);
                                $('#txtNet').val(net);
                               
                                $('#btnUpdate').off().click(function () {
                                    tr.children('td:nth-child(5)').text($('#txtTotDeduc').val());
                                    tr.children('td:nth-child(18)').text($('#txtNet').val());
                                    tr.children('td:nth-child(7)').text($('#txtHr').val());
                                    tr.children('td:nth-child(8)').text($('#txtMdecical').val());
                                    tr.children('td:nth-child(9)').text($('#txtTa').val());
                                    tr.children('td:nth-child(10)').text($('#txtDa').val());
                                    tr.children('td:nth-child(11)').text($('#txtSecDep').val());
                                    tr.children('td:nth-child(12)').text($('#txtPf').val());
                                    tr.children('td:nth-child(13)').text($('#txtTax').val());
                                    tr.children('td:nth-child(14)').text($('#txtTotalAllow').val());
                                    tr.children('td:nth-child(6)').text($('#txtTotDeduc').val());
                                    tr.children('td:nth-child(15)').text($('#txtInc').val());
                                    tr.children('td:nth-child(16)').text($('#txtGross').val());
                                    tr.children('td:nth-child(17)').text($('#txtLeave').val());
                                    tr.children('td:nth-child(18)').text($('#txtNet').val());
                                    $('#editModal').modal("hide");
                                });
                            });
                            $('#btnPost').show();
                        },

                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); loading('stop', null); },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); }
                    });
            }

            //Mark all checkboxes in the table
            $('body').on('change', '.chk-all', function () {
                $('#listTable').find('.chk-edit').prop('checked', $(this).prop('checked'))
            });

            //reseting the table
            function resetRegister() {
                $('#listTable > tbody').find('.chk-edit').prop('checked', false);
                $('#listTable > thead').find('.chk-all').prop('checked', false);
                $('#chkPayment').prop('checked', false);
            }

            //calculation inside the modal starts here
            $('input').keyup(function () {
                var bp = $('#txtBp').val();
                if (bp == '' || isNaN(bp)) {
                    bp = 0;
                }
                else {
                    bp = parseFloat(bp);
                }
                var hra = $('#txtHr').val();
                if (hra == '' || isNaN(hra)) {
                    hra = 0;
                }
                else {
                    hra = parseFloat(hra);
                }
                var ta = $('#txtTa').val();
                if (ta == '' || isNaN(ta)) {
                    ta = 0;
                }
                else {
                    ta = parseFloat(ta);
                }
                var da = $('#txtDa').val();
                if (da == '' || isNaN(da)) {
                    da = 0;
                }
                else {
                    da = parseFloat(da);
                }
                var incent = $('#txtInc').val();
                if (incent == '' || isNaN(incent)) {
                    incent = 0;
                }
                else {
                    incent = parseFloat(incent);
                }
                var pf = $('#txtPf').val();
                if (pf == '' || isNaN(pf)) {
                    pf = 0;
                }
                else {
                    pf = parseFloat(pf);
                }
                var ma = $('#txtMdecical').val();
                if (ma == '' || isNaN(ma)) {
                    ma = 0;
                }
                else {
                    ma = parseFloat(ma);
                }
                var taxDeduc = $('#txtTax').val();
                if (taxDeduc == '' || isNaN(taxDeduc)) {
                    taxDeduc = 0
                }
                else {
                    taxDeduc = parseFloat(taxDeduc);
                }
                var secDep = $('#txtSecDep').val();
                if (secDep == '' || isNaN(secDep)) {
                    secDep = 0;
                }
                else {
                    secDep = parseFloat(secDep);
                }
                var leave = $('#txtLeave').val();
                if (leave == '' || isNaN(leave)) {
                    leave = 0;
                }
                else {
                    leave = parseFloat(leave);
                }

                var totalAllowance = hra + ma + ta + da + incent;
                if (totalAllowance == '' || isNaN(totalAllowance)) {
                    totalAllowance = 0;
                }
                else {
                    totalAllowance = parseFloat(totalAllowance);
                }
                var totDeduc = pf + taxDeduc + leave + secDep;
                if (totDeduc == '' || isNaN(totDeduc)) {
                    totDeduc = 0;
                }
                else {
                    totDeduc = parseFloat(totDeduc);
                }
                var totSal = bp + totalAllowance - totDeduc;
                if (totSal == '' || isNaN(totSal)) {
                    totSal = 0;
                }
                else {
                    totSal = parseFloat(totSal);

                }
                $('#txtTotalAllow').val(totalAllowance);
                $('#txtTotDeduc').val(totDeduc);
                $('#txtGross').val(totSal);
                $('#txtNet').val(totSal);
            });
            //Calculation inside the modal ends here

            //function call for save
            $('#btnSave').off().click(function () {
                if ($('.chk-edit').prop('checked')) {
                    savePaySlip();
                }
                else {
                    errorField('.chk-edit');
                }
            })

            //Save payslip details
            function savePaySlip() {
                var date = new Date();
                var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec', ];
                var firstDate = firstDay.getDate() + '/' + months[firstDay.getMonth()] + '/' + firstDay.getFullYear();
                var tbody = $('#listTable > tbody');
                var tr = tbody.children('tr');
                var arr = [];
                for (var i = 0; i < tr.length; i++) {
                    if ($(tr[i]).children('td:nth-child(2)').find('.chk-edit').prop('checked')) {
                        var empId = $(tr[i]).children('td:nth-child(1)').text();
                        var template = $(tr[i]).children('td:nth-child(4)').text();
                        var bp = $(tr[i]).children('td:nth-child(5)').text();
                        var totDeduc = $(tr[i]).children('td:nth-child(6)').text();
                        var hr = $(tr[i]).children('td:nth-child(7)').text();
                        var medAll = $(tr[i]).children('td:nth-child(8)').text();
                        var ta = $(tr[i]).children('td:nth-child(9)').text();
                        var da = $(tr[i]).children('td:nth-child(10)').text();
                        var secDep = $(tr[i]).children('td:nth-child(11)').text();
                        var pf = $(tr[i]).children('td:nth-child(12)').text();
                        var taxDeduc = $(tr[i]).children('td:nth-child(13)').text();
                        var totAll = $(tr[i]).children('td:nth-child(14)').text();
                        var ince = $(tr[i]).children('td:nth-child(15)').text();
                        var gross = $(tr[i]).children('td:nth-child(16)').text();
                        var leaveDeduc = $(tr[i]).children('td:nth-child(17)').text();
                        var net = $(tr[i]).children('td:nth-child(18)').text();
                        var noOfLeave = $(tr[i]).children('td:nth-child(19)').text();
                        var totWorkingDays = $(tr[i]).children('td:nth-child(20)').text();
                        var totAtt = $(tr[i]).children('td:nth-child(21)').text();
                        var totHolidays = $(tr[i]).children('td:nth-child(22)').text();

                        var pay = {};
                        pay.EmployeeId = empId;
                        if ($('#chkPayment').is(':checked')) {
                            pay.PaymentStatus = 1;
                        }
                        else {
                            pay.PaymentStatus = 0;
                        }
                        pay.Grade = template;
                        pay.BasicSalary = bp;
                        pay.HouseRentAllowance = hr;
                        pay.MedicalAllowance = medAll;
                        pay.TravellingAllowance = ta;
                        pay.DearnessAllowance = da;
                        pay.SecurityDeposit = secDep;
                        pay.ProvidentFund = pf;
                        pay.TaxDeduction = taxDeduc;
                        pay.TotalAllowance = totAll;
                        pay.Incentives = ince;
                        pay.GrossSalary = gross;
                        pay.LeaveDeduction = leaveDeduc;
                        pay.NetSalary = net;
                        pay.TotalLeave = noOfLeave;
                        pay.TotalWorkingDays = totWorkingDays;
                        pay.TotalHolidays = totHolidays;
                        pay.TotalAttendance = totAtt;
                        pay.CreatedBy = $.cookie('bsl_3');
                        pay.CompanyId = $.cookie('bsl_1');
                        pay.Date = firstDate;
                        pay.TotalDeduction = totDeduc;
                        arr.push(pay);
                        console.log(arr)
                    }
                }
                $.ajax
                ({
                    url: $('#hdApiUrl').val() + 'api/PaySlip/Save',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(arr),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            successAlert(response.Message);
                            resetRegister();
                            $("#modalSave").modal('hide');
                        }
                        else {
                            errorAlert(response.Message);
                            $("#modalSave").modal('hide');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start') },
                    complete: function () { miniLoading('stop'); },
                });
            }

        });
        //Document ready function ends here

    </script>
    <link href="/Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="/Theme/assets/datatables/datatables.min.js"></script>
    <script src="/Theme/Custom/Commons.js"></script>
    <link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
