<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountOpeningBalance.aspx.cs" Inherits="BisellsERP.Finance.AccountOpeningBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Opening Balance</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="pull-left page-title">Set Opening Balance</h3>
            <ol class="breadcrumb pull-right">
                <li><a href="#">Bisells</a></li>
                <li><a href="#">Finance</a></li>
                <li class="active">Opening Balance</li>
            </ol>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-body">
            <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
        <asp:HiddenField ID="hdSqlTable" ClientIDMode="Static" runat="server" Value="0" />
        <div class="col-md-3">
            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                <label class="form-label semibold">Account Head</label>
                <div class="form-control-wrapper form-control-icon-left">
                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountHead" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlAccountHead_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                <label class="form-label semibold">Account Child Heads</label>
                <div class="form-control-wrapper form-control-icon-left">
                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlSubAccountHead" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                <label class="form-label semibold">Opening Balance:</label>
                <div class="form-control-wrapper form-control-icon-left">
                    <asp:TextBox ID="txtOpeningBalance" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control " placeholder="Opening Balance"></asp:TextBox>
                    <i class="font-icon font-icon-burger"></i>
                </div>
            </div>
        </div>
        <div class="col-md-2">
            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                <label class="form-label semibold">Opening Date:</label>
                <div class="form-control-wrapper form-control-icon-left">
                    <asp:TextBox ID="txtOpeningDate" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control " placeholder="DD-MMM-YYYY"></asp:TextBox>
                    <i class="font-icon font-icon-burger"></i>
                    <script>
                                $(function () {
                                    $('#txtOpeningDate').datepicker({
                                        todayHighlight: true,
                                        autoclose: true,
                                        format: 'dd-M-yyyy',
                                        todayBtn: "linked"
                                    });
                                });
                            </script>
                </div>
            </div>
        </div>
            <div class="col-md-2">
            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                <label class="form-label semibold">Is Debit</label>
                <div class="form-control-wrapper form-control-icon-left">
                    <asp:DropDownList ID="ddlISDebit" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Debit</asp:ListItem>
                        <asp:ListItem Value="1">Credit</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
        <div class="col-md-12">
            <div class="btn-toolbar pull-right m-t-30">
                <button id="btnSave" type="button"data-toggle="tooltip" data-placement="bottom" title="Save"  class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>&nbsp;Save</button>
                <%--<button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>--%>
                <asp:TextBox ID="txtNumber" runat="server" CssClass="hidden" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <%-- TABLE HERE --%>
                        <table id="table" class="table table-hover table-striped table-responsive" >
                            <thead class="bg-blue-grey">
                                <tr>
                                    <th>ID</th>
                                    <th>Child Name</th>
                                    <th>Amount</th>
                                    <th>Date</th>
                                    <th>#</th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        //All functions inside document ready
        $(document).ready(function () {
            //Fetching API url
            var apirul = $('#hdApiUrl').val();
            //Date Picker
            $('#txtOpeningDate').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy',
                todayHighlight: true,
            });
            //Loading table
            RefreshTable();
            //Initialises form validation if implemented any
            $.validate();

            //new entry
            $('#btnNew').click(function () {
                reset();
            });
            //cancel entry
            $('#btnCancel').click(function () {
                swal({
                    title: "Cancel?",
                    text: "Are you sure you want to cancel?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
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

            //save functionality. 
            //This is not a asynchronous ajax call. 
            //Handled directly by code behind
            $('#btnSave').click(function () {
                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $('#btnSaveConfirmed').trigger('click');
                        }

                    });
            });

            //edit functionality
            $(document).on('click', '.edit-entry', function () {
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/AccountOpeningBalance/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtOpeningDate').val(response.Datestring);
                        $('#txtOpeningBalance').val(response.Balance.toFixed(2));
                        $('#ddlAccountHead').val(response.AccountHeadID);
                        $('#ddlSubAccountHead').val(response.ChildheadID);
                        $('#hdItemId').val(response.ID);
                        $('#hdSqlTable').val(response.SQLtable);
                        $('#ddlISDebit').val(response.isDebit);
                        $('#add-item-portlet').addClass('in');
                        $('#btnSave').html('Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/AccountOpeningBalance/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully',
                    "successFunction": RefreshTable
                });

            });
            //Open entry section
            $('#btnAdd').click(function () {
                $('#masterEntry').slideDown('slow');
            });

            //independent function to load table with data
            function RefreshTable() {
                var id = $('#ddlAccountHead :selected').val();
                $.ajax({
                    url: apirul + '/api/AccountOpeningBalance/get2/'+id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#table').dataTable({
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'OpenBalance' },
                                { data: 'OpeningDate' },
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
        });
        function CheckIfCostHeadExists() {
            var IsCostHeadExists = false;
            if (document.getElementById('ctl00_ContentPlaceHolder1_rowCostHead') != null) {
                if (document.getElementById('ctl00_ContentPlaceHolder1_listCostHead') != null && document.getElementById('ctl00_ContentPlaceHolder1_listCostHead').value != null && document.getElementById('ctl00_ContentPlaceHolder1_listCostHead').value != "") {
                    IsCostHeadExists = true;
                }
                else {
                    IsCostHeadExists = false;
                    alert('Select the Cost Head');
                }
                if (IsCostHeadExists)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
    </script>
    <script src="../Theme/assets/jquery-datatables-editable/jquery.dataTables.js"></script>
    <script src="../Theme/assets/jquery-datatables-editable/dataTables.bootstrap.js"></script>
    <link href="../Theme/assets/jquery-datatables-editable/datatables.css" rel="stylesheet" />
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>

    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
