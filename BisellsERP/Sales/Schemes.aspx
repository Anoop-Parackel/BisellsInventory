<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Schemes.aspx.cs" Inherits="BisellsERP.Sales.Schemes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Theme/assets/ion-rangeslider/ion.rangeSlider.css" rel="stylesheet" type="text/css" class="" />
    <link href="../Theme/assets/ion-rangeslider/ion.rangeSlider.skinFlat.css" rel="stylesheet" type="text/css" />
    <style>
        /**** Styles for scheme page ****/
        .form-group {
            margin-bottom: 8px;
        }

        .panel-group .panel-heading {
            padding: 5px 10px;
            background-color: whitesmoke;
        }

        .checkbox input[type="checkbox"] {
            opacity: 1;
        }

        .ddl-chk-box + div.btn-group button.btn {
            padding: 1px 3px;
        }

        .ddl-chk-box + div.btn-group a label {
            padding: 0 25px;
        }

        .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
            padding: 15px;
        }

        .right-only-border {
            border-right: 3px solid rgba(55, 71, 79, 0.1);
        }

        .input-center {
            width: 70%;
            height: 25px;
            margin: 0 auto;
            padding: 5px;
        }

        .lr-center { /*.lr-center used for to align the add remove button to center  */
            margin-top: 83.625px;
        }

        .table-small tbody tr td {
            padding: 0px 5px !important;
            font-size: smaller;
            border-color: #ccc;
        }

        /*.table {
            margin-bottom: 10px;
            margin-top: 16px;
        }*/

        /* Adjust Tab Pane Height */
        .nav.nav-tabs > li > a, .nav.tabs-vertical > li > a {
            line-height: 30px;
        }

        #add-product .col-xs-12, #assign-customers .col-xs-12 {
            background-color: rgba(0, 69, 102, 0.04);
            padding: 10px 0;
            border-radius: 0 0 3px 3px;
            box-shadow: inset 0 1px 2px rgba(0,0,0,.2);
        }

        .add-remove-btn {
            display: block;
            background-color: transparent;
            border: 1px solid #ccc;
            border-radius: 50%;
            color: #ef5350;
            margin: 0 auto;
        }
        .apply-filter {
            border-radius: 3px;
            padding: 3px 12px;
            background-color: #607D8B;
            border: none;
            color: #ECEFF1;
            box-shadow: 0 2px 1px 0 rgba(0,0,0,.2);
            opacity: .8;
        }
            .apply-filter:hover {
                opacity: 1;
            }
        .portlet .portlet-body {
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:HiddenField ID="hdCreditCashMax" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdCreditDayMax" ClientIDMode="Static" runat="server" />
    <%-- Hidden field for to set slider dynamically --%>
<%--    <asp:HiddenField ID="hdCreditCashSliderMin" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdCreditCashSliderMax" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdCreditDaySliderMin" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdCreditDaySliderMax" ClientIDMode="Static" runat="server" />--%>
    <%--Page Title and Breadcrumb--%>
    <div class="row">
        <div class="col-sm-12">
            <h3 class="pull-left page-title">Offers and Discounts</h3>
            <ol class="breadcrumb pull-right">
                <li><a href="#">Bisells</a></li>
                <li><a href="#">Masters</a></li>
                <li class="active">Scheme</li>
            </ol>
        </div>
    </div>

    <%-- Add New Item Portlet --%>

    <div class="row">
        <div class="col-lg-12">
            <div class="portlet b-r-8">
                <div class="portlet-heading portlet-default p-b-5">
                    <h3 class="portlet-title">
                        <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Scheme </a>
                    </h3>
                    <asp:Button ID="btnSave" runat="server" Text="&#10003; Save" CssClass="btn btn-primary pull-right" OnClick="btnSave_Click" />
                    <div class="clearfix"></div>
                </div>
                <asp:HiddenField ID="hdSchemeId" ClientIDMode="Static" runat="server" Value="0" />
                <div id="add-item-portlet" class="panel-collapse collapse">
                    <div class="portlet-body b-r-8 p-t-0">
                        <ul class="nav nav-tabs tabs">
                            <li class="active tab">
                                <a href="#scheme_define" data-toggle="tab" aria-expanded="false">
                                    <span class="visible-xs"><i class="fa fa-home"></i></span>
                                    <span class="hidden-xs">Define Scheme</span>
                                </a>
                            </li>
                            <li class="tab">
                                <a href="#add-product" data-toggle="tab" aria-expanded="false">
                                    <span class="visible-xs"><i class="fa fa-user"></i></span>
                                    <span class="hidden-xs">Add Products</span>
                                </a>
                            </li>
                            <li class="tab">
                                <a href="#assign-customers" data-toggle="tab" aria-expanded="true">
                                    <span class="visible-xs"><i class="fa fa-envelope-o"></i></span>
                                    <span class="hidden-xs">Assign Customers</span>
                                </a>
                            </li>
                        </ul>
                        <div class="tab-content m-b-0 p-b-0 p-t-0">
                            <%-- SCHEME DEFINE TAB --%>
                            <div class="tab-pane active fade in p-t-25" id="scheme_define">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label text-left">Scheme Type</label>
                                                <div class="col-xs-7">
                                                    <asp:DropDownList ID="ddlSchemeType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Primary</asp:ListItem>
                                                        <asp:ListItem Value="1">Secondary</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Scheme Name</label>
                                                <div class="col-xs-7">
                                                    <asp:TextBox ID="txtSchemeName" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Start Date</label>
                                                <div class="col-xs-7">
                                                    <asp:TextBox ID="txtFromDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">End Date</label>
                                                <div class="col-xs-7">
                                                    <asp:TextBox ID="txtToDate" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Min Quantity</label>
                                                <div class="col-xs-7">
                                                    <asp:TextBox ID="txtQuantity" ClientIDMode="Static" runat="server" CssClass="form-control">0.00</asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Amount/Per</label>
                                                <div class="col-xs-7">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtAmountOrPercentage" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <asp:Label ID="btnAmtPer" ClientIDMode="Static" runat="server" class="btn waves-effect waves-light btn-primary">% &nbsp;<span class="caret"></    span></asp:Label>
                                                            <asp:CheckBox ID="chkIsPercent" Checked="true" ClientIDMode="Static" CssClass="hidden" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Mode</label>
                                                <div class="col-xs-7">
                                                    <asp:DropDownList ID="ddlMode" ClientIDMode="Static" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">Mrp based</asp:ListItem>
                                                        <asp:ListItem Value="1">Sp based</asp:ListItem>
                                                        <asp:ListItem Value="2">Lc based</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label for="inputEmail3" class="col-xs-5 text-left control-label">Status</label>
                                                <div class="col-xs-7">
                                                    <asp:DropDownList ID="ddlStatus" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="0">Active</asp:ListItem>
                                                        <asp:ListItem Value="1">Disabled</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <%-- ADD PRODUCT TAB --%>
                            <div class="tab-pane  fade" id="add-product">
                                <div class="row">
                                    <asp:UpdatePanel ID="updatePanel" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblListItem" Text="No items to list" Visible="false" CssClass="center-me" runat="server" />
                                            <div class="row">
                                                <div class="col-xs-12">
                                                    <div class=" col-md-2 right-only-border">
                                                        <label for="lstProductType" class="control-label">Type</label>
                                                        <asp:ListBox ID="lstProductType" runat="server" SelectionMode="Multiple" CssClass="ddl-chk-box"></asp:ListBox>
                                                    </div>
                                                    <div class=" col-md-2 right-only-border">
                                                        <label for="lstGroup" class="control-label">Group</label>
                                                        <asp:ListBox ID="lstGroup" runat="server" SelectionMode="Multiple" CssClass="ddl-chk-box"></asp:ListBox>
                                                    </div>
                                                    <div class=" col-md-2 right-only-border">
                                                        <label for="lstBrand" class="control-label">Brand</label>
                                                        <asp:ListBox ID="lstBrand" runat="server" SelectionMode="Multiple" CssClass="ddl-chk-box"></asp:ListBox>
                                                    </div>
                                                    <div class=" col-md-3">
                                                        <label for="lstCategory" class="control-label">Category</label>
                                                        <asp:ListBox ID="lstCategory" runat="server" SelectionMode="Multiple" CssClass="ddl-chk-box"></asp:ListBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtSearchItems" placeholder="Search Product.." style="width: 140px;float: left;height: 27px;" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:Button ID="btnProductFilter" runat="server" CssClass="apply-filter pull-right m-r-5" OnClick="btnProductFilter_Click" Text="Apply Filter" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row m-t-10">
                                                <div class="col-md-7">
                                                    <div class="form-group-sm">
                                                        <label class="control-label">All Products:</label>
                                                        <asp:GridView CssClass="table table-hover table-striped table-small" AutoGenerateColumns="False" ClientIDMode="Static" AllowSorting="True" ID="grdItems" runat="server" OnRowDataBound="grdItems_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="bg-blue-grey">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="checkAllItems" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkItems" CssClass="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Item_Id" HeaderText="Item Id">
                                                                    <HeaderStyle CssClass="hidden" />
                                                                    <ItemStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderStyle-CssClass="bg-blue-grey" HeaderStyle-ForeColor="whitesmoke" DataField="Name" HeaderText="Product"></asp:BoundField>
                                                                <asp:BoundField HeaderStyle-CssClass="bg-blue-grey" HeaderStyle-ForeColor="whitesmoke" DataField="Item_Code" HeaderText="Item Code"></asp:BoundField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                </div>
                                                <div class="col-md-1 lr-center text-center">
                                                    <asp:Button ID="btnRightProduct" runat="server" Text=">" CssClass="btn add-remove-btn" ClientIDMode="Static" OnClick="btnRightProduct_Click" />
                                                    <asp:Button ID="btnleftProduct" runat="server" Text="<" CssClass="btn m-t-10 add-remove-btn" ClientIDMode="Static" OnClick="btnleftProduct_Click" />
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group-sm">
                                                        <label class="control-label">Selected Products:</label>
                                                        <div>
                                                            <asp:ListBox ID="lbProducts" runat="server" SelectionMode="Multiple" ClientIDMode="Static" Height="233px" CssClass="w-100 form-control"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <%-- ASSIGN CUSTOMERS TAB --%>
                            <div class="tab-pane  fade" id="assign-customers">
                                <div class="row">
                                    <asp:UpdatePanel ID="updatePanel2" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <asp:Label ID="lblNoitm" Visible="false" CssClass="center-me" Text="No Items to List" runat="server" />

                                                <div class="col-xs-12 right-only-border">
                                                    <div class="panel-group panel-group-joined" id="accordion-test">
                                                        <div class="col-md-1">
                                                            <label for="inputEmail3" class="control-label p-t-15">Credit Limit</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <input type="text" id="credit-limit" />
                                                            </div>
                                                            <asp:TextBox ID="crLimitMax" CssClass="hidden" runat="server" ClientIDMode="Static"  placeholder="Min"></asp:TextBox>
                                                            <asp:TextBox ID="crLimitMin" runat="server" CssClass="hidden" ClientIDMode="Static"  placeholder="Max"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-1">
                                                            <label for="inputEmail3" class="control-label p-t-15">Credit Days</label>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <input type="text" id="credit-days" />
                                                            </div>
                                                            <asp:TextBox ID="crDaysMax" runat="server" ClientIDMode="Static" CssClass="hidden" placeholder="Min"></asp:TextBox>
                                                            <asp:TextBox ID="crDaysMin" runat="server" ClientIDMode="Static" CssClass="hidden" placeholder="Max"></asp:TextBox>
                                                        </div>
                                                        <div class="col-md-3 col-md-offset-1 m-t-20">
                                                            <asp:TextBox runat="server" ID="txtSearchCustomer" style="width: 140px;float: left;height: 27px;" CssClass="form-control" placeholder="Search Customer..."/>
                                                            <asp:Button ID="btnCustomerFilter" runat="server" Text="Apply Filter" CssClass="apply-filter pull-right m-r-5" OnClick="btnCustomerFilter_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row m-t-10">
                                                <div class="col-md-7">
                                                    <div class="form-group-sm">
                                                        <label class="control-label">All Products:</label>
                                                        <asp:GridView ID="grdCustomers" AutoGenerateColumns="false" ClientIDMode="Static"  AllowSorting="true" CssClass="table table-hover table-striped table-responsive  table-small" runat="server" OnRowDataBound="grdCustomers_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-CssClass="bg-blue-grey">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="checkAllCustomers" runat="server" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkCustomers" CssClass="" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Customer_Id" HeaderText="Customer Id">
                                                                    <ItemStyle CssClass="hidden" />
                                                                    <HeaderStyle CssClass="hidden" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Name" HeaderStyle-CssClass="bg-blue-grey" HeaderStyle-ForeColor="White" HeaderText="Customer"></asp:BoundField>
                                                                <asp:BoundField DataField="credit_amount" HeaderStyle-CssClass="bg-blue-grey" HeaderStyle-ForeColor="White" HeaderText="Credit Limit"></asp:BoundField>
                                                                <asp:BoundField DataField="credit_period" HeaderStyle-CssClass="bg-blue-grey" HeaderStyle-ForeColor="White" HeaderText="Credit Period"></asp:BoundField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-md-1 lr-center">
                                                    <asp:Button ID="btnCustomerRight" runat="server" Text=">" CssClass="btn add-remove-btn" ClientIDMode="Static" OnClick="btnCustomerRight_Click" />
                                                    <asp:Button ID="btnCustomerleft" runat="server" Text="<" CssClass="btn m-t-10 add-remove-btn" ClientIDMode="Static" OnClick="btnCustomerleft_Click" />
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group-sm">
                                                        <label class="control-label">Selected Products:</label>
                                                        <div>
                                                            <asp:ListBox ID="lbCustomer" runat="server" SelectionMode="Multiple" ClientIDMode="Static" Height="233px" CssClass="w-100 form-control"></asp:ListBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Added Item Table--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel">
                <div class="panel-body">
                    <%-- TABLE HERE --%>
                    <table id="table" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey">
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Quantity</th>
                                <th>Type</th>
                                <th>Mode</th>
                                <th>AmntOrPerc</th>
                                <th>From Date</th>
                                <th>To Date</th>
                                <th>#</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                    <%-- TABLE END --%>
                </div>
            </div>
        </div>
    </div>

    
    <link href="../Theme/assets/bootstrap-multiselect/bootstrap-multiselct.min.css" rel="stylesheet" />
    <script src="../Theme/assets/bootstrap-multiselect/bootstrap-multiselect.js"></script>
    <script src="../Theme/assets/ion-rangeslider/ion.rangeSlider.min.js"></script>
    <script>
        $(document).ready(function () {

            //$('#grdCustomers').DataTable();
            //$('#grdItems').dataTable();

            $('#txtFromDate, #txtToDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            //Fetching API url
            var apirul = $('#hdApiUrl').val();
            //Loading table
            RefreshTable();
            //Initialises form validation if implemented any


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
                            reset();
                        }

                    });
            });
            //edit functionality
            $(document).on('click', '.edit-entry', function () {
                html = '';
                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Scheme/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_2")),
                    success: function (response) {
                        reset();
                        rows = $('#grdItems tbody').children('tr');
                        for (var i = 0; i < response.Items.length; i++) {
                            for (var j = 0; j < rows.length; j++) {
                                var ItmId = Number($(rows[j]).children('td:nth-child(2)').text());
                                if (response.Items[i].ItemID == ItmId) {
                                    $(rows[j]).children('td:nth-child(1)').children('span').children('input').prop('checked', true);
                                    break;
                                }
                            }
                        }
                        rows = $('#grdCustomers tbody').children('tr');

                        for (var k = 0; k < response.Customers.length; k++) {
                            for (var l = 0; l < rows.length; l++) {
                                var CustId = Number($(rows[l]).children('td:nth-child(2)').text());

                                if (response.Customers[k].ID == CustId) {
                                    $(rows[l]).children('td:nth-child(1)').children('span').children('input').prop('checked', true);
                                    break;
                                }
                            }
                        }



                        $('#txtSchemeName').val(response.SchemeName);
                        $('#txtAmountOrPercentage').val(response.AmountOrPercentage);
                        $('#txtFromDate').val(response.StartDateString);
                        $('#txtQuantity').val(response.Quantity);
                        $('#txtToDate').val(response.EndDateString);
                        $('#ddlMode').val(response.Mode);
                        $('#ddlSchemeType').val(response.SchemeType);
                        $('#ddlStatus').val(response.Status);
                        $('#hdSchemeId').val(response.SchemeId);
                        $('#add-item-portlet').addClass('in');

                        //select items to list box while editing btnRightProduct automatically trigger btnRightCustomer
                        $('#btnRightProduct').trigger("click");

                        //used for scheme in amount or percent
                        if (item.IsPercentageBased) {

                            $('#btnAmtPer').html('% &nbsp<span class="caret"></span>').val(0);
                            $('#chkIsPercent').prop('checked', true);
                        }
                        else {

                            $('#btnAmtPer').html('AED &nbsp<span class="caret"></span>').val(1)
                            $('#chkIsPercent').prop('checked', false);
                        }


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
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Scheme/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Scheme has been deleted from Sales',
                    "successFunction": RefreshTable
                });

            });

            //independent function to load table with data
            function RefreshTable() {
                $.ajax({
                    url: apirul + '/api/Scheme/get/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie("bsl_2")),
                    success: function (response) {

                        $('#table').dataTable({
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'SchemeId' },
                                { data: 'SchemeName' },
                                { data: 'Quantity' },
                                { data: 'Types' },
                                { data: 'Modes' },
                                { data: 'AmountOrPercentage' },
                                { data: 'StartDateString' },
                                { data: 'EndDateString' },
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
    </script>
    <script type="text/javascript">

        $(function () {
            listBoxManipulate();

        });

        function listBoxManipulate() {

            //select all checkbox
            $('#checkAllItems').off().change(function () {
                if ($(this).is(':checked')) {
                    $('#grdItems input[type=checkbox]').prop('checked', true);
                }
                else {
                    $('#grdItems input[type=checkbox]').prop('checked', false);
                }
            });
            $('#checkAllCustomers').off().change(function () {
                if ($(this).is(':checked')) {
                    $('#grdCustomers input[type=checkbox]').prop('checked', true);
                }
                else {
                    $('#grdCustomers input[type=checkbox]').prop('checked', false);
                }
            });
            //amount or percentage chooser
            $('#btnAmtPer').click(function () {
                if ($(this).val() == 0) {

                    $(this).html('AED &nbsp<span class="caret"></span>').val(1)
                    $('#chkIsPercent').prop('checked', false);

                }
                else {
                    $(this).html('% &nbsp<span class="caret"></span>').val(0);
                    $('#chkIsPercent').prop('checked', true);

                }
            });
            //MultiSelect Dropdown
            $('.ddl-chk-box').multiselect();

            //Credit Limit Slider
            $("#credit-limit").ionRangeSlider({
                type: "double",
                grid: true,
                min: 0,
                max: $("#hdCreditCashMax").val(),
                from: 0,
                to: $("#hdCreditCashMax").val(),
                onStart: function (data) {
                    $('#crLimitMax').prop("value", data.from);
                    $('#crLimitMin').prop("value", data.to);
                },
                onChange: function (data) {
                    $('#crLimitMax').prop("value", data.from);
                    $('#crLimitMin').prop("value", data.to);
                }
            });


            //Credit Days Slider
            $("#credit-days").ionRangeSlider({
                type: "double",
                grid: true,
                min: 0,
                max: $("#hdCreditDayMax").val(),
                from: 0,
                to: $("#hdCreditDayMax").val(),
                onStart: function (data) {
                    $('#crDaysMax').prop("value", data.from);
                    $('#crDaysMin').prop("value", data.to);
                },
                onChange: function (data) {
                    $('#crDaysMax').prop("value", data.from);
                    $('#crDaysMin').prop("value", data.to);
                }

            });
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
