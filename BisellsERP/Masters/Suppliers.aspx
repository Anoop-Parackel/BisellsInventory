<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="BisellsERP.Masters.Suppliers" %>

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
    <asp:ScriptManager runat="server" />        
    <div class="">
        <%--hidden fields--%>
       <asp:HiddenField ID="hdSupplierId" ClientIDMode="Static" runat="server" Value="0" />
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Suppliers</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Inventory</a></li>
                    <li class="active">Suppliers</li>
                </ol>
            </div>
        </div>
        <%--new master form--%>
        <div class="row">

            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">
                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add New Supplier </a>
                        </h3>
                        <div class="clearfix"></div>
                    </div>
                    <div id="add-item-portlet" class="panel-collapse collapse">
                        <div class="portlet-body b-r-8">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label">Name:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox data-validation="required" MaxLength="30" data-validation-error-msg="<small style='color:red'>This field is required</small>" ID="txtName" ClientIDMode="Static" runat="server" class="form-control " placeholder="Name"></asp:TextBox>
                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Address1:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtAddress1" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address1"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Address2:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtAddress2" ClientIDMode="Static" runat="server" class="form-control " placeholder="Address2"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="form-label ">Country</label>
                                                <div class="form-control-wrapper">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlCountry" AutoPostBack="false" runat="server">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="form-label ">State</label>
                                                <div class="form-control-wrapper">
                                                    <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlState" runat="server">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Phone 1:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtPhone1"   ClientIDMode="Static" TextMode="Number" runat="server" class="form-control" onkeypress="return this.value.length<=15" placeholder="Phone 1"></asp:TextBox>
                                       
                                             </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Phone 2:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtPhone2" ClientIDMode="Static" runat="server" class="form-control" TextMode="Number" onkeypress="return this.value.length<=15" placeholder="Phone 2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Email:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox data-validation="email" data-validation-error-msg="<small style='color:red'>Invalid Email Format</small>" ID="txtEmail" ClientIDMode="Static" runat="server" class="form-control " placeholder="Email"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Tax No 1:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtTaxNo1" MaxLength="15" ClientIDMode="Static" runat="server" class="form-control " placeholder="TaxNo1"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Tax No 2:</label>
                                        <div class="form-control-wrapper">
                                            <asp:TextBox ID="txtTaxNo2" maxlength="15" ClientIDMode="Static" runat="server" class="form-control " placeholder="TaxNo2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="form-label ">Currency</label>
                                        <div class="form-control-wrapper">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="searchDropdown" ID="ddlCurrency" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="padding-left: 10px; padding-right: 10px">
                                        <label class="form-label ">Status</label>
                                        <div class="form-control-wrapper">
                                            <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlStatus" runat="server">
                                           
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">Inactive</asp:ListItem>
                                            </asp:DropDownList>

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
                                        <th>Name</th>
                                        <th>Country</th>
                                        <th>State</th>
                                        <th>Phone1</th>
                                        <th>Email</th>
                                        <th>TaxNo1</th>
                                        <th>TaxNo2</th>
                                        <th>Currency</th>
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

                $('#ddlCountry').change(function () {
                    var CompanyId = $.cookie('bsl_1');
                    var FinancialYear = $.cookie('bsl_4');
                    var countryId = $('#ddlCountry').val();

                    $.ajax({
                        url: apirul + '/api/Customers/getStates/' + countryId,
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify($.cookie("bsl_1")),
                        success: function (data) {
                            console.log(data);
                            $('#ddlCountry').val();
                            $('#ddlState').children('option').remove();
                            $('#ddlState').append('<option value="0">--select--</option>');
                            $(data).each(function () {
                                $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                            });

                        }
                    });
                });
                function ResetRegister()
                {
                    $('#hdSupplierId').val('');
                    $('#txtName').val('');
                    $('#txtAddress1').val('');
                    $('#txtAddress2').val('');
                    $('#ddlState').val('0');
                    $('#ddlCountry').val('0');
                    $('#ddlCurrency').val('0');
                    $('#txtPhone1').val('');
                    $('#txtPhone2').val('');
                    $('#txtEmail').val('');
                    $('#txtTaxNo1').val('');
                    $('#txtTaxNo2').val('');
                    $('#ddlStatus').val('0');
                    $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                }

                function save()
                {
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

                        data.ID = $('#hdSupplierId').val();
                        data.Name=$('#txtName').val();
                        data.Address1=$('#txtAddress1').val();
                        data.Address2=$('#txtAddress2').val();
                        data.StateId=$('#ddlState').val();
                        data.CountryId=$('#ddlCountry').val();
                        data.CurrencyId=$('#ddlCurrency').val();
                        data.Phone1=$('#txtPhone1').val();
                        data.Phone2=$('#txtPhone2').val();
                        data.Email=$('#txtEmail').val();
                        data.Taxno1=$('#txtTaxNo1').val();
                        data.Taxno2=$('#txtTaxNo2').val();
                        data.Status=$('#ddlStatus').val();
                        data.LocationId = $.cookie('bsl_2');
                        data.CompanyId = $.cookie('bsl_1');
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.ModifiedBy = $.cookie('bsl_3');
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/Suppliers/Save',
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
                 //edit functionality
                $(document).on('click', '.edit-entry', function () {

                    var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                    $.ajax({
                        url: apirul + '/api/Suppliers/get/' + id,
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
                                    $('#ddlCountry').val(response.CountryId);
                                    $('#ddlState').children('option').remove();
                                    $('#ddlState').append('<option value="0">--select--</option>');
                                    $(data).each(function () {
                                        $('#ddlState').append('<option value="' + this.StateId + '">' + this.State + '</option>');
                                    });

                                    $('#txtName').val(response.Name);
                                    $('#txtAddress1').val(response.Address1);
                                    $('#txtAddress2').val(response.Address2);
                                    $('#txtPhone1').val(response.Phone1);
                                    $('#txtPhone2').val(response.Phone2);
                                    $('#txtEmail').val(response.Email);
                                    $('#txtTaxNo1').val(response.Taxno1);
                                    $('#txtTaxNo2').val(response.Taxno2);
                                    $('#ddlStatus').val(response.Status);
                                    $('#ddlState').val(response.StateId);
                                    $('#ddlCountry').val(response.CountryId);
                                    $('#ddlCurrency').val(response.CurrencyId);
                                    $('#ddlCurrency').select2('val', response.CurrencyId);
                                    $('#hdSupplierId').val(response.ID);
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
                        "url": apirul + '/api/Suppliers/Delete/',
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
                        url: apirul + '/api/Suppliers/get/',
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
                                    { data: 'Taxno1' },
                                    { data: 'Taxno2' },
                                    { data: 'Currency' },
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
        <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>
 
</asp:Content>
