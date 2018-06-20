<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PayrollTemplate.aspx.cs" Inherits="BisellsERP.Payroll.PayrollTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        a > small {
            background-color: #d03232;
            color: white;
            border-radius: 2px;
            padding-left: 3px;
            padding-right: 3px;
        }

        thead tr th {
            color: white;
        }

        .portlet .portlet-heading .portlet-widgets {
            line-height: 0;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <%--hidden fields--%>
       <%-- <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />--%>
        <asp:HiddenField ID="hdId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Payroll Template</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Payroll</a></li>
                    <li class="active">Payroll Template</li>
                </ol>
            </div>
        </div>
        <%--new master form--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Payroll Template </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Name Of Template:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox data-validation="required" MaxLength="35" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtName" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name Of Template"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Basic Salary:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox data-validation="required" MaxLength="35" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtBasicSal" ClientIDMode="Static" runat="server" class="form-control " placeholder="Basic Salary"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">House Rent Allowance:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox MaxLength="35" ID="txtHouseRent" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Medical Allowance:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtMedicalAllow" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Provident Fund:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtPF" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Tax Deduction:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtTax" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Travelling Allowance:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtTravelAllow" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Dearness Allowance:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtDearnessAllow" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                               
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Security Deposit:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtSecDep" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Incentives:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtIncentives" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Gross Salary:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtGross" ClientIDMode="Static" runat="server" class="form-control " placeholder="Gross Salary"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Total Allowance:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox MaxLength="35" ID="txtTotal" ClientIDMode="Static" runat="server" class="form-control " placeholder="Total Allowance"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Total Deduction:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtDeduction" ClientIDMode="Static" runat="server" class="form-control " placeholder="Total Deduction"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Net Salary:</label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:TextBox  MaxLength="35" ID="txtNet" ClientIDMode="Static" runat="server" class="form-control " placeholder="Net Salary"></asp:TextBox>
                                            <i class="font-icon font-icon-burger"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label semibold">Status  </label>
                                        <div class="form-control-wrapper form-control-icon-left">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlStatus" runat="server">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="btn-toolbar pull-right m-t-30">
                                        <button id="btnSave" accesskey="s" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>Add</button>
                                        <button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--list table--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <%-- TABLE HERE --%>
                        <table id="table" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey">
                                <tr>
                                    <th>ID</th>
                                    <th>Payroll Template</th>
                                    <th>Basic Salary</th>
                                    <th>Total Deduction</th>
                                    <th>Net Salary</th>
                                    <th>Total Allowance</th>
                                    <th>Status</th>
                                    <th>#</th>
                                </tr>
                            </thead>
                        </table>
                        <%-- TABLE END --%>
                    </div>
                </div>
            </div>
        </div>
        <script>
            //document ready function starts here

            $(document).ready(function ()
            {
                //Fetching API url
                var apirul = $('#hdApiUrl').val();
                //Loading table
                RefreshTable();
                //Initialises form validation if implemented any
                $.validate();
                //new entry
                $('#btnNew').click(function ()
                {
                    reset();
                });
                $('#btnSave').off().click(function () {
                    save();
                });

                //save details
                function save() {
                    swal({
                        title: "Save?",
                        text: "Are you sure you want to save?",
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Save",
                        closeOnConfirm: true
                    },
               function (isConfirm) {
                   if (isConfirm) {
                       var data = {};
                       data.ID = $('#hdId').val();
                       data.Grade = $('#txtName').val();
                       data.BasicSalary = $('#txtBasicSal').val();
                       data.HouseRentAllowance = $('#txtHouseRent').val();
                       data.MedicalAllowance = $('#txtMedicalAllow').val();
                       data.ProvidentFund = $('#txtPF').val();
                       data.TaxDeduction = $('#txtTax').val();
                       data.TravellingAllowance = $('#txtTravelAllow').val();
                       data.DearnessAllowance = $('#txtDearnessAllow').val();
                       data.SecurityDeposit = $('#txtSecDep').val();
                       data.Incentives = $('#txtIncentives').val();
                       data.GrossSalary = $('#txtGross').val();
                       data.TotalAllowance = $('#txtTotal').val();
                       data.TotalDeduction = $('#txtDeduction').val();
                       data.NetSalary = $('#txtNet').val();
                       data.Status = $('#ddlStatus').val();
                       data.CreatedBy = $.cookie("bsl_3");
                       data.ModifiedBy = $.cookie("bsl_3");
                       data.CompanyId = $.cookie("bsl_1");
                       console.log(data);
                       $.ajax({
                           url: $(hdApiUrl).val() + 'api/PayRollTemplate/Save',
                           method: 'POST',
                           data: JSON.stringify(data),
                           contentType: 'application/json',
                           dataType: 'JSON',
                           success: function (response) {
                               if (response.Success) {
                                   successAlert(response.Message);
                                   $('#add-item-portlet').removeClass('in');
                                   RefreshTable();
                                   ResetRegister();
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
               });
                }
                //cancel entry
                $('#btnCancel').click(function ()
                {
                    swal
                        ({
                        title: "Cancel?",
                        text: "Are you sure you want to cancel?",
                      
                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,
                        closeOnConfirm: true,
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Cancel this Entry"
                    },
                    function (isConfirm)
                    {
                        if (isConfirm)
                        {
                            reset();
                            $('#add-item-portlet').removeClass('in');
                        }
                        else
                        {

                        }

                    });

                });

                //clear the details from the form
                function ResetRegister()
                {
                    $('#hdId').val('');
                    $('#txtName').val('');
                    $('#txtBasicSal').val('');
                    $('#txtHouseRent').val('');
                    $('#txtMedicalAllow').val('');
                    $('#txtPF').val('');
                    $('#txtTax').val('');
                    $('#txtTravelAllow').val('');
                    $('#txtDearnessAllow').val('');
                    $('#txtSecDep').val('');
                    $('#txtIncentives').val('');
                    $('#txtGross').val('');
                    $('#txtTotal').val('');
                    $('#txtDeduction').val('');
                    $('#txtNet').val('');
                    $('#txtNet').val('');
                    $('#btnSave').html('<i class="ion-checkmark-round"></i>Save');
                }

                $('input').keyup(function ()
                {
                    var bp = $('#txtBasicSal').val();
                    if (bp == '' || isNaN(bp))
                    {
                        bp = 0;
                    }
                    else
                    {
                        bp = parseFloat(bp);
                    }
                    var hra = $('#txtHouseRent').val();
                    if (hra == '' || isNaN(hra))
                    {
                        hra = 0;
                    }
                    else
                    {
                      hra=parseFloat(hra);
                    }
                    var ta = $('#txtTravelAllow').val();
                    if (ta == '' || isNaN(ta))
                    {
                        ta = 0;
                    }
                    else
                    {
                       ta= parseFloat(ta);
                    }
                    var da = $('#txtDearnessAllow').val();
                    if (da == '' || isNaN(da))
                    {
                        da = 0;
                    }
                    else
                    {
                        da = parseFloat(da);
                    }
                    var incent = $('#txtIncentives').val();
                    if (incent == '' || isNaN(incent))
                    {
                        incent = 0;
                    }
                    else
                    {
                        incent = parseFloat(incent);
                    }
                    var pf = $('#txtPF').val();
                    if (pf == '' || isNaN(pf))
                    {
                        pf = 0;
                    }
                    else
                    {
                        pf = parseFloat(pf);
                    }
                    var ma = $('#txtMedicalAllow').val();
                    if (ma == '' || isNaN(ma))
                    {
                        ma = 0;
                    }
                    else
                    {
                        ma = parseFloat(ma);
                    }
                    var taxDeduc = $('#txtTax').val();
                    if (taxDeduc == '' || isNaN(taxDeduc))
                    {
                        taxDeduc=0
                    }
                    else
                    {
                        taxDeduc = parseFloat(taxDeduc);
                    }
                    var secDep = $('#txtSecDep').val();
                    if (secDep == '' || isNaN(secDep))
                    {
                        secDep = 0;
                    }
                    else
                    {
                        secDep = parseFloat(secDep);
                    }

                    var totalAllowance = hra + ma + ta + da + incent;
                    if (totalAllowance == '' || isNaN(totalAllowance))
                    {
                        totalAllowance = 0;
                    }
                    else
                    {
                        totalAllowance = parseFloat(totalAllowance);
                    }
                    var totDeduc = pf + taxDeduc + secDep;
                    if (totDeduc == '' || isNaN(totDeduc))
                    {
                        totDeduc = 0;
                    }
                    else {
                        totDeduc = parseFloat(totDeduc);
                    }
                    var totSal = bp + totalAllowance - totDeduc;
                    if (totSal == '' || isNaN(totSal))
                    {
                        totSal = 0;
                    }
                    else
                    {
                        totSal = parseFloat(totSal);

                    }
                    $('#txtTotal').val(totalAllowance);
                    $('#txtDeduction').val(totDeduc);
                    $('#txtGross').val(totSal);
                    $('#txtNet').val(totSal);
                })

                //edit functionality
                $(document).on('click', '.edit-entry', function ()
                {
                   var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                   $.ajax
                       ({
                        url: apirul + '/api/PayRollTemplate/get/' + id,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response)
                        {
                             reset();
                             $('#txtName').val(response.Grade);
                             $('#txtBasicSal').val(response.BasicSalary);
                             $('#txtOvertimeRate').val(response.OvertimeRate);
                             $('#txtHouseRent').val(response.HouseRentAllowance);
                             $('#txtMedicalAllow').val(response.MedicalAllowance);
                             $('#txtPF').val(response.ProvidentFund);
                             $('#txtTax').val(response.TaxDeduction);
                             $('#txtTravelAllow').val(response.TravellingAllowance);
                             $('#txtDearnessAllow').val(response.DearnessAllowance);
                             $('#txtSecDep').val(response.SecurityDeposit);
                             $('#txtIncentives').val(response.Incentives);
                             $('#txtGross').val(response.GrossSalary);
                             $('#txtTotal').val(response.TotalAllowance);
                             $('#txtDeduction').val(response.TotalDeduction);
                             $('#txtNet').val(response.NetSalary);
                             $('#ddlStatus').val(response.Status);
                             $('#hdId').val(response.ID);
                            $('#add-item-portlet').addClass('in');
                            $('#btnSave').html('<i class="ion-checkmark-round"></i>Update');
                        },
                        error: function (xhr)
                        {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                });

                //delete functionality
                $(document).on('click', '.delete-entry', function ()
                {
                    var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                    var modifiedBy = $.cookie("bsl_3");
                    deleteMaster({
                        "url": apirul + '/api/PayRollTemplate/Delete/',
                        "id": id,
                        "modifiedBy": modifiedBy,
                        "successMessage": 'PayRoll Template has been deleted from Payroll',
                        "successFunction": RefreshTable
                    });
                });

                //Open entry section
                $('#btnAdd').click(function ()
                {
                    $('#masterEntry').slideDown('slow');
                });

                //independent function to load table with data
                function RefreshTable()
                {
                    $.ajax
                        ({
                        url: apirul + '/api/PayRollTemplate/get/',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (response)
                        {
                            $('#table').dataTable
                                ({
                                responsive: true,
                                dom: 'Blfrtip',
                                lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                                buttons: ['copy', 'excel', 'csv', 'print'],
                                data: response,
                                destroy: true,
                                columns: [
                                    { data: 'ID', className: 'hidden-td' },
                                    { data: 'Grade' },
                                    { data: 'BasicSalary' },
                                    { data: 'TotalDeduction' },
                                    { data: 'NetSalary' },
                                    { data: 'TotalAllowance' },
                                    { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                    {
                                        data: function ()
                                        {
                                            return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry"><i class="fa fa-times" style="color:red"></i></a>'
                                        },
                                        sorting: false
                                    }
                                ]
                            });
                            $('[data-toggle="tooltip"]').tooltip();

                        }
                    });
                }
         });
        </script>
</div>
<link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
<script src="../Theme/assets/datatables/datatables.min.js"></script>
<link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
<script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
<script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
<script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>

</asp:Content>
