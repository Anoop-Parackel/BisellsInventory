<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoucherSettings.aspx.cs" Inherits="BisellsERP.Finance.VoucherSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Voucher Settings</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Voucher Settings</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Finance</a></li>
                    <li class="active">Voucher Settings</li>
                </ol>
            </div>
        </div>
    <div class="container-fluid">
        <div class="panel panel-body">
            <div class="row">
            <asp:Button ID="btnSaveConfirmed" ClientIDMode="Static" Style="display: none" runat="server" Text="Save" OnClick="btnSaveConfirmed_Click" />
            <div class="col-md-8">
                <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                    <label class="form-label semibold">Voucher Type</label>
                    <div class="form-control-wrapper form-control-icon-left">
                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlVoucherType" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Value="0">Default</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-5" style="display:none">
                <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                    <label class="form-label semibold">Voucher Type</label>
                    <div class="form-control-wrapper form-control-icon-left">
                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlgroupBy" AutoPostBack="true" OnSelectedIndexChanged="ddlgroupBy_SelectedIndexChanged" runat="server">
                            <asp:ListItem Value="1">Account Group</asp:ListItem>
                            <asp:ListItem Value="0" Selected="True">Account Heads</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-2">
                <div class="btn-toolbar pull-right m-t-25">
                    <button id="btnSave" accesskey="s" type="button" class="btn btn-primary waves-effect"><i class="ion-checkmark-round"></i>&nbsp;Save</button>
                    <%--<button id="btnCancel" type="button" class="btn btn-danger waves-effect waves-light"><i class="ion-close-round"></i>Close</button>--%>
                </div>
            </div>
        </div>
        </div>
        <div class="panel-body">
            <div class="table table-bordered table-hover table-striped">
            <asp:GridView ID="gvAccounts" runat="server" AutoGenerateColumns="false"
                OnRowDataBound="gvAccounts_RowDataBound" CssClass="table table-bordered table-hover table-striped">
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%#Bind("ID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Account Head" />
                    <asp:TemplateField HeaderText="Dr">
                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="left" />
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkSelectAllDr" onclick='SelectAllDr(this)' CssClass="txt"
                                Text="Dr" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:CheckBox runat="server" ID="chkSelectDr" CssClass="txt" value='<%#Eval("Fah_ID")%>' />--%>
                            <input type="checkbox" id="chkSelectDr" name="chkSelectDr" value='<%#Eval("ID")%>' class="txt" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cr">

                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="chkSelectAllCr" onclick='SelectAllCr(this)' CssClass="txt"
                                Text="Cr" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<asp:CheckBox runat="server" ID="chkSelectCr" CssClass="txt" value='<%#Eval("Fah_ID")%>' />--%>
                            <input type="checkbox" id="chkSelectCr" name="chkSelectDr" value='<%#Eval("ID")%>' class="txt" runat="server" />
                            <%--<asp:CheckBox runat="server" id="chkSelect" value='<%#Eval("Amount")%>' onclick='CalculateTotal(this)'
                                                                        class="txt" />--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDr" runat="server" Text='<%#Bind("AllowDr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCr" runat="server" Text='<%#Bind("AllowCr")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalHead" runat="server" Text='<%#Bind("TotalHead")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDrHead" runat="server" Text='<%#Bind("DrHead")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCrHead" runat="server" Text='<%#Bind("CrHead")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        </div>
    </div>
    <script>
        //All functions inside document ready
        $(document).ready(function () {
            //Fetching API url
            var apirul = $('#hdApiUrl').val();

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
                    url: apirul + '/api/CostCenter/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtCostCenterName').val(response.Name);
                        $('#ddlDisable').val(response.Status);
                        $('#hdItemId').val(response.ID);
                        $('#add-item-portlet').addClass('in');
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
                    "url": apirul + '/api/CostCenter/Delete/',
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
                $.ajax({
                    url: apirul + '/api/CostCenter/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#table').dataTable({
                            data:response,
                            destroy: true,
                            columns: [
                                { data: 'ID' },
                                { data: 'Name' },                          
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },
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

        function SelectAllDr(id) {

            var frm = document.forms[0];
            var nameCheck = id.name;
            for (i = 0; i < frm.elements.length; i++) {

                if (frm.elements[i].type == "checkbox" && nameCheck.search('Cr') == -1) {
                    if (id.checked == true && frm.elements[i].name.search('Dr') != -1)
                        frm.elements[i].checked = true;
                    else if (frm.elements[i].name.search('Dr') != -1)
                        frm.elements[i].checked = false;
                }
            }
        }
        function SelectAllCr(id) {

            var frm = document.forms[0];
            var nameCheck = id.name;
            for (i = 0; i < frm.elements.length; i++) {

                if (frm.elements[i].type == "checkbox" && nameCheck.search('Dr') == -1) {
                    if (id.checked == true && frm.elements[i].name.search('Cr') != -1)
                        frm.elements[i].checked = true;
                    else if (frm.elements[i].name.search('Cr') != -1)
                        frm.elements[i].checked = false;
                }
            }
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
