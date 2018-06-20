<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdvanceSalary.aspx.cs" Inherits="BisellsERP.Payroll.AdvanceSalary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Advance Salary</title>
    <style>
        #wrapper {
            /*overflow: hidden;*/
        }

        /* Styling Table Head */
        #table thead tr th {
            font-weight: 600;
            background-color: #FAFAFA;
            font-size: 14px;
            color: #63aace;
        }

        .form-horizontal .form-label {
            font-weight: 100;
            color: #63aace;
            margin-top: 5px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="panel">
        <div class="panel-body">
            <div class="row">
                <div class="col-sm-4">
                    <h3 class="page-title m-t-0">Advance Salary</h3>
                    <br />
                    <div class="row">
                        <input type="hidden" id="hdAdvSalId" value="0" />
                        <div class="col-sm-12">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="form-label col-md-5">Select Employee</label>
                                    <div class="col-md-7">
                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlEmployee" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="form-label col-md-5">Month And Year</label>
                                    <div class="col-md-7">
                                        <input type="text" id="txtDate" class="form-control" maxlength="10" required="required" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="form-label col-md-5">Amount</label>
                                    <div class="col-md-7">
                                        <input type="text" id="txtAmount" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12 text-right m-t-20">
                            <button type="button" class="btn btn-default btn-sm" id="btnSave"><i class="ion-checkmark-round"></i>&nbsp;Save</button>
                            <button type="button" data-toggle="tooltip" id="btnReset" data-placement="bottom" title="Reset" class="btn btn-inverse light btn-sm"><i class="md md-replay"></i>&nbsp;Reset</button>
                        </div>
                    </div>
                </div>
                <div class="col-sm-8">
                    <table id="table" class="table table-hover">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Employee</th>
                                <th>Amount</th>
                                <th>Month And Year</th>
                                <th>#</th>
                            </tr>
                        </thead>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script>
        //Document ready function starts here
        $(document).ready(function ()
        {
            //Datepicker initialization
            $(function () {
                $("#txtDate").datepicker({
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months"
                });
            });

            var apirul = $('#hdApiUrl').val();
            //Loading table
            RefreshTable();
            //Initialises form validation if implemented any
            $.validate();

            ResetRegister();

            //cancel entry
            $('#btnCancel').click(function () {
                swal({
                    title: "Cancel?",
                    text: "Are you sure you want to cancel?",
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    closeOnConfirm: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Cancel this Entry"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        reset();
                        $('#add-item-portlet').removeClass('in');
                    }
                    else {

                    }

                });

            });

            $('#btnSave').off().click(function () {
                save();
            });
            $('#btnReset').off().click(function () {
                ResetRegister();
            });

            //function for saving the details
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
                        data.Id = $('#hdAdvSalId').val();
                        data.Amount = $('#txtAmount').val();
                        data.MonthAndYear = $('#txtDate').val();
                        data.EmployeeId = $('#ddlEmployee').val();
                        data.Status = $('#ddlStatus').val();
                        data.CreatedBy = $.cookie("bsl_3");
                        data.ModifiedBy = $.cookie("bsl_3");
                        data.CompanyId = $.cookie("bsl_1");
                        console.log(data);
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/AdvanceSalary/Save',
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
            //Save Function Ends here

            //Function for reset the details
            function ResetRegister() {
                $('#txtAmount').val('');
                $('#ddlEmployee').val('0');
                $('#hdAdvSalId').val('');
                $('#txtDate').val('');
                $('#btnSave').html('<i class="ion-checkmark-round"></i>&nbsp;Save');
            }

            //edit functionality
            $(document).on('click', '.edit-entry', function ()
            {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/AdvanceSalary/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#ddlEmployee').val(response.EmployeeId);
                        $('#txtAmount').val(response.Amount);
                        $('#txtDate').val(response.MonthString);
                        $('#ddlStatus').val(response.Status);
                        $('#hdAdvSalId').val(response.Id);
                        $('#add-item-portlet').addClass('in');
                        $('#btnSave').html('<i class="ion-checkmark-round"></i>');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //independent function to load table with data
            function RefreshTable() {
                $.ajax({
                    url: apirul + '/api/AdvanceSalary/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#table').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['copy', 'excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'Id', className: 'hidden-td' },
                                { data: 'Employee' },
                                { data: 'Amount' },
                                { data: 'MonthString' },
                                {
                                    data: function () {
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
            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                var modifiedBy = ($.cookie("bsl_3"));

                deleteMaster({
                    "url": apirul + '/api/AdvanceSalary/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Advance Salary has been deleted from inventory',
                    "successFunction": RefreshTable
                });

            });
        });
    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
