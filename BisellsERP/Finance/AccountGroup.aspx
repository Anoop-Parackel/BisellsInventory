<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountGroup.aspx.cs" Inherits="BisellsERP.Finance.AccountGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Group</title>
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
    <div>

        <%--hidden fields--%>
        <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Group</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Finance</a></li>
                    <li class="active">Account Group</li>
                </ol>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="portlet b-r-8">
                            <div class="portlet-heading portlet-default">
                                <h3 class="portlet-title">
                                    <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New AccountGroup </a>
                                </h3>
                                <div class="clearfix"></div>
                            </div>
                            <div id="add-item-portlet" class="panel-collapse collapse">
                                <div class="portlet-body b-r-8">

                                    <%-- CREATION FORM HERE --%>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Name:</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:TextBox ID="txtAccountGroupName" MaxLength="100" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name"></asp:TextBox>
                                                    <i class="font-icon font-icon-burger"></i>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Parent Group</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlParentGroup" runat="server">
                                                        <asp:ListItem Value="0">Default</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Description:</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:TextBox ID="txtDescription" MaxLength="20" TextMode="MultiLine" Height="101px" ClientIDMode="Static" runat="server" class="form-control " placeholder="Description"></asp:TextBox>
                                                    <i class="font-icon font-icon-burger"></i>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Status</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlStatus" runat="server">
                                                        <asp:ListItem Value="0">Enabled</asp:ListItem>
                                                        <asp:ListItem Value="1">Disabled</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Is Affect Gross Profit</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlIsAffectGrossProfit" runat="server">
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                                <label class="form-label semibold">Account Nature</label>
                                                <div class="form-control-wrapper form-control-icon-left">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountNature" runat="server">
                                                        <asp:ListItem Value="0">Asset</asp:ListItem>
                                                        <asp:ListItem Value="1">Income</asp:ListItem>
                                                        <asp:ListItem Value="3">Liability</asp:ListItem>
                                                        <asp:ListItem Value="2">Expense</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="btn-toolbar pull-right m-t-30">
                                                <button id="btnSave" accesskey="s" type="button" runat="server" clientIDmode="static" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>&nbsp;Save</button>
                                                <button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>&nbsp;Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel" style="height:430px;overflow-y:auto">
                            <div class="panel-body vh-60 p-t-0">
                                <asp:Panel ID="Panel1" runat="server">
                                    <div class="col-md-12 m-t-10">
                                        <asp:TreeView ID="moduleTree" runat="server" CssClass="nunito-font" OnSelectedNodeChanged="moduleTree_SelectedNodeChanged" Font-Size="medium" ForeColor="#004767" LineImagesFolder="~/TreeLineImages">
                                        <LeafNodeStyle HorizontalPadding="5px" VerticalPadding="3px" />
                                        <ParentNodeStyle Font-Bold="True" />
                                        <RootNodeStyle Font-Bold="True" />
                                        <SelectedNodeStyle Font-Bold="True" CssClass="edit-entry" ForeColor="#41BC76" />
                                    </asp:TreeView>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
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
            $('#add-item-portlet').addClass('in');
            //Loading table
            //RefreshTable();
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
                $('#add-item-portlet').slideDown('fast');
                $('#add-item-portlet').addClass('in');
                $('#btnSave').html('Update');
            });

            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/AccountGroup/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully'
                });

            });

            //Open entry section
            $('#btnAdd').click(function () {
                $('#masterEntry').slideDown('slow');
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
