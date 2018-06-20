<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExpenseEntry.aspx.cs" Inherits="BisellsERP.Finance.ExpenseEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Finance Expense Entry</title>

</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
  
     <div class="row p-b-5">
            <div class="col-sm-4">
                <h3 class="pull-left page-title w-100">Expense Entry</h3>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group">
                <button id="btnSave" type="button"data-toggle="tooltip" data-placement="bottom" title="Save the current entry"  class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>Add Expense</button>
                </div>
            </div>
        </div>
    <div class="row">
        <div class="col-md-3">
            <div class="row">
                <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
                <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
                <asp:Button ID="hiddenButton" runat="server" ClientIDMode="Static" Style="display:none"  Text="Button" OnClick="hiddenButton_Click"/>
                <div class="panel">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <b>Date</b>
                            <asp:TextBox ID="txtDate" ClientIDMode="Static" runat="server" CssClass="form-control form-input-r-b" ></asp:TextBox>
                            <script>
                                $(function () {
                                    $('#txtDate').datepicker({
                                        todayHighlight: true,
                                        autoclose: true,
                                        format: 'dd-M-yyyy',
                                        todayBtn: "linked"
                                    });
                                });
                            </script>
                        </div>

                        <div class="col-md-12">
                            <b>Voucher Type</b>
                            <asp:DropDownList ID="ddlVoucherType" CssClass="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="col-md-6">
            <div class="panel">
                <div class="panel-body">
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <b>From Main Account</b>
                            <asp:DropDownList ID="ddlfrommain" CssClass="form-control" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12">
                            <b>From Sub Account</b>
                            <asp:DropDownList ID="ddlfromsub" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="col-md-6">
                        <div class="col-md-12">
                            <b>To Main Head</b>
                            <asp:DropDownList ID="ddltomain" CssClass="form-control" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12">
                            <b>To Sub Head</b>
                            <asp:DropDownList ID="ddltosub" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-3">
            <div class="row">
                <div class="panel">
                    <div class="panel-body">
                        <div class="col-md-12">
                            <b>Amount</b>
                            <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <b>Narration</b>
                            <asp:TextBox ID="txtnarration" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="btn-toolbar pull-right m-t-30">
                <%--<button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>--%>
                <asp:TextBox ID="txtNumber" runat="server" CssClass="hidden" ReadOnly="true"></asp:TextBox>
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
                    title: "Alert!",
                    text: "Are you sure you want to cancel?",
                    type: "warning",
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
                    title: "Alert!",
                    text: "Are you sure you want to save?",
                    type: "warning",
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

                var id = parseInt($(this).closest('tr').children('td:nth-child(8)').text());
                
                document.getElementById("<%=hdItemId.ClientID%>").value = id;
                $('#hiddenButton').trigger('click');
                
            });

            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(8)').text());
                var modifiedBy = $.cookie("bsl_3")
                deleteMaster({
                    "url": apirul + '/api/ExpenseEntry/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Deleted SuccessFully'
                });

            });

            //Open entry section
            $('#btnAdd').click(function () {
                $('#masterEntry').slideDown('slow');
            });

            //independent function to load table with data
            
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
