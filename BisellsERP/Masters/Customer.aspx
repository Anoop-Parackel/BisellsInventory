<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="BisellsERP.Masters.Customer" %>

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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="">

        <%--hidden fields--%>
       
        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Customer Master</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Inventory</a></li>
                    <li class="active">Customer</li>
                </ol>
            </div>
        </div>


        <%-- Add New Item Portlet --%>
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Customer</a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">

                            <%-- CREATION FORM HERE --%>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class=" ">Name:</label>
                                        <asp:TextBox data-validation="required" maxlength="20" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtCustomerName" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 1:</label>
                                        <asp:TextBox ID="txtCustomerAddrs1" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 2:</label>
                                        <asp:TextBox ID="txtCustomerAddrs2" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Phone 1:</label>
                                        <asp:TextBox  TextMode="Number" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtCustomerPhone1" ClientIDMode="Static" runat="server" class="form-control " placeholder="Phone"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Phone 2:</label>
                                        <asp:TextBox ID="txtCustomerPhone2" ClientIDMode="Static" TextMode="Number"  runat="server" class="form-control " placeholder="Phone"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Country </label>
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCustomerCountry"  runat="server" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>State </label>
                                                    <div class="form-control-wrapper ">
                                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCustomerState" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>

                                    </asp:UpdatePanel>

                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Email:</label>
                                        <asp:TextBox data-validation="email" data-validation-error-msg="<small style='color:red'>Invalid Email</small>" ID="txtCustomerEmail" ClientIDMode="Static" runat="server" class="form-control " placeholder="Email"></asp:TextBox>

                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tax No 1:</label>
                                        <asp:TextBox ID="txtCustomerTax1" ClientIDMode="Static" MaxLength="15" runat="server" class="form-control " placeholder="Tax Number"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tax No 2:</label>
                                        <asp:TextBox ID="txtCustomertax2" ClientIDMode="Static" MaxLength="15" runat="server" class="form-control " placeholder="Tax Number"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Currency </label>
                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCustomerCurrency" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Status </label>
                                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCustomerStatus" runat="server">

                                            <asp:ListItem Value="1">Active</asp:ListItem>
                                            <asp:ListItem Value="0">Inactive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="">
                                        <label class="form-label ">Credit Amount :</label>
                                        <div class="form-control-wrapper ">
                                            <asp:TextBox ID="txtCreditAmnt" ClientIDMode="Static" runat="server" TextMode="Number" class="form-control " placeholder="Credit Amount "></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="">
                                        <label class="form-label ">Credit Period (In Days) :</label>
                                        <div class="form-control-wrapper ">
                                            <asp:TextBox ID="txtCreditPeriod" TextMode="Number" ClientIDMode="Static" runat="server" class="form-control " placeholder="Credit Period"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="">
                                        <label class="form-label ">Lock Amount :</label>
                                        <div class="form-control-wrapper ">
                                            <asp:TextBox ID="txtLockAmnt" ClientIDMode="Static" runat="server" TextMode="Number" class="form-control " placeholder="Lock Amount"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="">
                                        <label class="form-label ">Lock Period (In Days):</label>
                                        <div class="form-control-wrapper ">
                                            <asp:TextBox ID="txtLockPeriod" TextMode="Number" ClientIDMode="Static" runat="server" class="form-control " placeholder="Lock Period"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="btn-toolbar pull-right m-t-10">
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




        <%--Added Item Table--%>
        <div class="row">
            <div class="col-lg-12">
                <div class="panel">
                    <div class="panel-body">
                        <table id="table" class="table table-hover table-striped table-responsive">
                            <thead class="bg-blue-grey">
                                <tr>
                                    <th>ID</th>
                                    <th>Name</th>
                                    <th>Country</th>
                                    <th>State</th>
                                    <th>Phone 1</th>
                                    <th>Email</th>
                                    <th>Currency</th>
                                    <th>Status</th>
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

            //Loading table
            RefreshTable();
            //Initialises form validation if implemented any
            $.validate();
            $('#btnSave').off().click(function () {
                save();
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

            $('#ddlCustomerCountry').change(function () {
                var CompanyId = $.cookie('bsl_1');
                var FinancialYear = $.cookie('bsl_4');
                var countryId = $('#ddlCustomerCountry').val();

                $.ajax({
                    url: apirul + '/api/Customers/getStates/' + countryId,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (data) {
                        console.log(data);
                        $('#ddlCustomerCountry').val();
                        $('#ddlCustomerState').children('option').remove();
                        $('#ddlCustomerState').append('<option value="0">--select--</option>');
                        $(data).each(function () {
                            $('#ddlCustomerState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                        });

                    }
                });
            });
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

                                data.ID = $('#hdItemId').val();
                                data.Name = $('#txtCustomerName').val();
                                data.Address1 = $('#txtCustomerAddrs1').val();
                                data.Address2 = $('#txtCustomerAddrs2').val();
                                data.CountryId = $('#ddlCustomerCountry').val();
                                data.CurrencyId = $('#ddlCustomerCurrency').val();
                                data.StateId = $('#ddlCustomerState').val();
                                data.Phone1 = $('#txtCustomerPhone1').val();
                                data.Phone2 = $('#txtCustomerPhone2').val();
                                data.Email = $('#txtCustomerEmail').val();
                                data.Taxno1 = $('#txtCustomerTax1').val();
                                data.Taxno2 = $('#txtCustomertax2').val();
                                data.LockAmount = $('#txtLockAmnt').val();
                                data.LockPeriod = $('#txtLockPeriod').val();
                                data.CreditAmount = $('#txtCreditAmnt').val();
                                data.CreditPeriod = $('#txtCreditPeriod').val();
                                data.Status = $('#ddlCustomerStatus').val();
                                data.LocationId = $.cookie('bsl_2');
                                data.CompanyId = $.cookie('bsl_1');
                                data.FinancialYear = $.cookie('bsl_4');
                                data.CreatedBy = $.cookie('bsl_3');
                                data.ModifiedBy = $.cookie('bsl_3');
                                $.ajax({
                                    url: $(hdApiUrl).val() + 'api/customers/Save',
                                    method: 'POST',
                                    data: JSON.stringify(data),
                                    contentType: 'application/json',
                                    dataType: 'JSON',
                                    success: function (response) {
                                        if (response.Success) {
                                            successAlert(response.Message);
                                            $('#add-item-portlet').removeClass('in');
                                            ResetRegister();
                                            RefreshTable();
                                        }
                                        else {
                                            errorAlert(response.Message);
                                        }
                                    },
                                    error: function (xhr) {
                                        alert(xhr.responseText);
                                        console.log(xhr);
                                        complete: loading('stop', null);
                                    },
                                    beforeSend: function () { loading('start', null) },
                                    complete: function () { loading('stop', null); },
                                });
                            }
                        
                });


            }//Save Function Ends here
            function ResetRegister() {
                $('#hdItemId').val('');
                $('#txtCustomerName').val('');
                $('#txtCustomerAddrs1').val('');
                $('#txtCustomerAddrs2').val('');
                $('#txtCustomerPhone1').val('');
                $('#txtCustomerPhone2').val('');
                $('#txtCustomerTax1').val('');
                $('#txtCustomertax2').val('');
                $('#txtCustomerEmail').val('');
                $('#txtCreditAmnt').val('');
                $('#txtCreditPeriod').val('');
                $('#txtLockAmnt').val('');
                $('#txtLockPeriod').val('');
                $('#ddlCustomerCurrency').val('0');
                $('#ddlCustomerStatus').val('0');
                $('#ddlCustomerState').val('0');
                $('#ddlCustomerCountry').val('0');
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
            }
            //edit functionality
            $(document).on('click', '.edit-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Customers/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var countryId = response.CountryId;
                        $.ajax({
                            url: apirul + '/api/Customers/getStates/' + countryId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (data) {
                                $('#ddlCustomerCountry').val(response.CountryId);
                                $('#ddlCustomerState').children('option').remove();
                                $('#ddlCustomerState').append('<option value="0">--select--</option>');
                                $(data).each(function () {
                                    $('#ddlCustomerState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                });

                                $('#ddlCustomerState').val(response.StateId);
                                $('#txtCustomerName').val(response.Name);
                                $('#txtCustomerAddrs1').val(response.Address1);
                                $('#txtCustomerAddrs2').val(response.Address2);
                                $('#txtCustomerPhone1').val(response.Phone1);
                                $('#txtCustomerPhone2').val(response.Phone2);
                                $('#txtCustomertax1').val(response.Taxno1);
                                $('#txtCustomertax2').val(response.Taxno2);
                                $('#txtCustomerEmail').val(response.Email);
                                $('#txtCustomerTax1').val(response.Taxno1);
                                $('#txtCustomertax2').val(response.Taxno2);
                                $('#txtCreditAmnt').val(response.CreditAmount);
                                $('#txtCreditPeriod').val(response.CreditPeriod);
                                $('#txtLockAmnt').val(response.LockAmount);
                                $('#txtLockPeriod').val(response.LockPeriod);
                                $('#ddlCustomerCurrency').val(response.CurrencyId);
                                $('#ddlCustomerStatus').val(response.Status);
                                $('#hdItemId').val(response.ID);
                                $('#add-item-portlet').addClass('in');
                                $('#btnSave').html('<i class="ion-checkmark-round"></i>Update');
                                
                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                                complete: loading('stop', null);
                            },
                            beforeSend: function () { loading('start', null) },
                            complete: function () { loading('stop', null); },
                        });

                    }
                });
            });
            //delete functionality
            $(document).on('click', '.delete-entry', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                var modifiedBy = $.cookie("bsl_3");
                deleteMaster({
                    "url": apirul + '/api/Customers/Delete/',
                    "id": id,
                    "modifiedBy": modifiedBy,
                    "successMessage": 'Product has been deleted from inventory',
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
                    url: apirul + '/api/Customers/get/',
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
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                 { data: 'Country' },
                                  { data: 'State' },
                                   { data: 'Phone1' },
                                     { data: 'Email' },
                                        { data: 'currency' },
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
    </script>
  <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
   <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>--%>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
