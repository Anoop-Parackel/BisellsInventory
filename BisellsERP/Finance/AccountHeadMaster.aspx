<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountHeadMaster.aspx.cs" Inherits="BisellsERP.Finance.AccountHeadMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Head</title>
    <style>
        .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
            padding: 20px;
        }
        .btn-toolbar {
            margin-top: 8px;
        }
        .tab-content .form-group {
            margin-bottom: 5px;
        }
        .tab-content .form-control {
            background-color: transparent;
            border: 1px solid #ddd;
        }
        .tab-content .control-label {
            color: #9E9E9E;
            font-size: 13px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">

        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-4">
                <h3 class="page-title m-t-0">Account Heads</h3>
            </div>
            <div class="col-sm-8">
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Finance</a></li>
                    <li class="active">Account Heads</li>
                </ol>
            </div>
        </div>

        <div class="row">

            <div class="col-md-3">
                <div class="panel" style="height: 70vh; overflow: auto">
                    <div class="panel-body">
                        <asp:Panel ID="Panel1" runat="server">
                            <asp:TreeView ID="moduleTree" runat="server" OnSelectedNodeChanged="moduleTree_SelectedNodeChanged" Font-Size="Small" ForeColor="#004767" LineImagesFolder="~/TreeLineImages">
                                <LeafNodeStyle Font-Size="Smaller" />
                                <NodeStyle BackColor="White" Font-Size="Small" ForeColor="#333333" HorizontalPadding="5px" />
                                <ParentNodeStyle Font-Bold="True" />
                                <RootNodeStyle Font-Bold="True" />
                                <SelectedNodeStyle Font-Bold="False" ForeColor="#41BC76" Font-Underline="True" />
                            </asp:TreeView>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="col-sm-9">
                <ul class="nav nav-tabs navtab-bg">
                    <li class="active">
                        <a href="#primary-tab" data-toggle="tab" aria-expanded="true">
                            <span class="visible-xs"><i class="fa fa-cog"></i></span>
                            <span class="hidden-xs">Primary</span>
                        </a>
                    </li>
                    <li class="">
                        <a href="#others-tab" data-toggle="tab" aria-expanded="false">
                            <span class="visible-xs"><i class="fa fa-envelope-o"></i></span>
                            <span class="hidden-xs">Others</span>
                        </a>
                    </li>
                    <li class="">
                        <a href="#settings-tab" data-toggle="tab" aria-expanded="false">
                            <span class="visible-xs"><i class="fa fa-user"></i></span>
                            <span class="hidden-xs">Settings</span>
                        </a>
                    </li>
                    <li class="pull-right">
                        <div class="btn-toolbar pull-right m-r-10">
                            <button id="btnSave" accesskey="s" type="button" data-toggle="tooltip" runat="server" clientIdMode="static" data-placement="bottom" title="Save or Updates the current account heads" class="btn btn-default waves-effect">Save</button>
                            <button id="btnDelete" type="button" data-toggle="tooltip" data-placement="bottom" title="Delete the current account heads" class="btn btn-default delete-entry text-danger">Delete</button>                            
                            <button id="btnCancel" type="button"data-toggle="tooltip" data-placement="bottom" title="Cancel the current account head"  class="btn btn-default waves-effect waves-light">Cancel</button>
                        </div>
                    </li>
                </ul>
                <div class="tab-content">

                    <%-- Primary Tab Contents --%>
                    <div class="tab-pane active" id="primary-tab">
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label">Name :<asp:Label ID="lblID" runat="server"></asp:Label></label>
                                    <asp:TextBox ID="txtAccountHeadName" MaxLength="50" ClientIDMode="Static" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label">Account Group :<asp:Label ID="Label1" runat="server"></asp:Label></label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlParentGroup" runat="server">
                                        <asp:ListItem Value="0">Default</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label">Opening Date :<asp:Label ID="Label2" runat="server"></asp:Label></label>
                                    <asp:TextBox ID="txtOpeningDate" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <label class="control-label">Opening Balance :<asp:Label ID="Label3" runat="server"></asp:Label></label>
                                    <asp:TextBox ID="txtOpeningBalance" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control "></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">Category</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCategory" runat="server">
                                        <asp:ListItem Value="0">Asset</asp:ListItem>
                                        <asp:ListItem Value="1">Supplier Payments</asp:ListItem>
                                        <asp:ListItem Value="2">Bank Deposit</asp:ListItem>
                                        <asp:ListItem Value="3">Purchase Head</asp:ListItem>
                                        <asp:ListItem Value="4">Customer Reciept</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Status</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="0">Enabled</asp:ListItem>
                                        <asp:ListItem Value="1">Disabled</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Is Debit</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlIsDebit" runat="server">
                                        <asp:ListItem Value="0">No</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Account Nature</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountNature" Enabled="false" runat="server">
                                        <asp:ListItem Value="0">Asset</asp:ListItem>
                                        <asp:ListItem Value="1">Income</asp:ListItem>
                                        <asp:ListItem Value="2">Expense</asp:ListItem>
                                        <asp:ListItem Value="3">Liability</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Account Type</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountType" runat="server">
                                        <asp:ListItem Value="0">Normal</asp:ListItem>
                                        <asp:ListItem Value="1">Bank</asp:ListItem>
                                        <asp:ListItem Value="2">Cash</asp:ListItem>
                                        <asp:ListItem Value="3">Stock</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>

                    <%-- Others Tab Contents --%>
                    <div class="tab-pane" id="others-tab">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Contact Person:</label>
                                    <asp:TextBox ID="txtContactPerson" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Phone:</label>
                                    <asp:TextBox ID="txtPhoneNumber" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Email:</label>
                                    <asp:TextBox ID="txtEmail" MaxLength="20" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">Address:</label>
                                    <asp:TextBox ID="txtAddress" MaxLength="20" ClientIDMode="Static" TextMode="MultiLine" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="control-label">Description:</label>
                                    <asp:TextBox ID="txtDescription" MaxLength="20" TextMode="MultiLine" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Settings Tab Contents --%>
                    <div class="tab-pane" id="settings-tab">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Default Reverse Head</label>
                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlReverseHead" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Data SQL</label>
                                    <asp:TextBox ID="txtDataSQL" MaxLength="40" TextMode="MultiLine" ClientIDMode="Static" runat="server" class="form-control " placeholder="Data SQL"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="control-label">Transaction SQL</label>
                                    <asp:TextBox ID="txtTransactionSQL" MaxLength="40" TextMode="MultiLine" ClientIDMode="Static" runat="server" class="form-control " placeholder="Transaction SQL"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label">Amount SQL</label>
                                    <asp:TextBox ID="txtAmountSQL" MaxLength="40" ClientIDMode="Static" runat="server" class="form-control " placeholder="Amount SQL"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="control-group">
                                    <label class="control-label">Ref Table Name</label>
                                    <asp:TextBox ID="txtRefenerenceTable" MaxLength="40" ClientIDMode="Static" runat="server" class="form-control " placeholder="Ref Table Name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="control-group">
                                    <label class="control-label semibold">Data ID Field</label>
                                    <asp:TextBox ID="txtDataID" MaxLength="40" ClientIDMode="Static" runat="server" class="form-control " placeholder="Data ID Field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="control-group">
                                    <label class="control-label">Data Name Field</label>
                                    <asp:TextBox ID="txtDataName" MaxLength="40" ClientIDMode="Static" runat="server" class="form-control " placeholder="Data Name Field"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div class="row">

            
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
                format: 'dd/M/yyyy',
                todayHighlight: true,
            });
            //Loading table
            //RefreshTable();
            //Initialises form validation if implemented any
            $.validate();

            $('#ddlParentGroup').change(function () {
                var id = $('#ddlParentGroup :selected').val();
                $.ajax({
                    url: apirul + '/api/AccountHeadMaster/get1/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var type = response;
                        $('#ddlAccountNature').val(type);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            });

            //new entry
            $('#btnNew').click(function () {
                reset();
            });
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

            //save functionality. 
            //This is not a asynchronous ajax call. 
            //Handled directly by code behind
            $('#btnSave').click(function () {
                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                  
                    showConfirmButton: true, closeOnConfirm: true,
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
                    url: apirul + '/api/AccountHeadMaster/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtAccountHeadName').val(response.Name);
                        $('#txtDescription').val(response.Description);
                        $('#ddlStatus').val(response.status);
                        $('#ddlAccountNature').val(response.AccountType);
                        $('#ddlParentGroup').val(response.ParentId);
                        $('#txtAddress').val(response.Address);
                        $('#txtContactPerson').val(response.ContactPerson);
                        $('#txtOpeningBalance').val(response.OpeningBalance);
                        $('#txtOpeningDate').val(response.OpeningDate);
                        $('#txtPhoneNumber').val(response.Phone);
                        $('#txtEmail').val(response.Email);
                        $('#ddlStatus').val(response.status);
                        $('#ddlIsDebit').val(response.IsDebit);
                        $('#ddlAccountNature').val(response.AccountNature);
                        $('#ddlCategory').val(response.Category);
                        $('#ddlAccountType').val(response.AccountType);
                        $('#txtOpeningDate').val(response.datestring);
                        $('#txtAmountSQL').val(response.AmountSQL);
                        $('#txtDataSQL').val(response.DataSQL);
                        $('#txtTransactionSQL').val(response.TransactionSQL);
                        $('#txtRefenerenceTable').val(response.SQLTable);
                        $('#txtDataID').val(response.SQLID);
                        $('#txtDataName').val(response.SQLName);
                        $('#ddlReverseHead').val(response.ReverseHeadId);
                        $('#hdItemId').val(response.ID);
                        $('#add-item-portlet').addClass('in');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                    }
                });
            });

            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($('#hdItemId').val());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/AccountHeadMaster/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully',
                    "successFunction": reset
                });

            });

            //Open entry section
            $('#btnAdd').click(function () {
                $('#masterEntry').slideDown('slow');
            });


        });

    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
  
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
