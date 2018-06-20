<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterOut.aspx.cs" Inherits="BisellsERP.WareHousing.RegisterOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .mini-stat.clearfix.bx-shadow {
            height: 90px;
        }

        .print-float {
            position: fixed;
            bottom: 15px;
            right: 35px;
            height: 60px;
            width: 60px;
            border-radius: 50%;
        }

            .print-float i {
                font-size: 1.6em;
                color: #263238;
            }

        .l-font {
            font-size: 1.5em;
        }

        .light-font-color {
            color: #607D8B;
        }

        .panel {
            margin-bottom: 10px;
        }

        .view-h {
            max-height: 50vh;
            overflow: auto;
        }

        tbody tr td {
            padding: 5px !important;
            font-size: smaller;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 30px;
        }

        .edit-value {
            background-color: transparent;
            width: 40px;
            text-align: center;
        }

        .panel .panel-body {
            padding: 10px;
            padding-top: 20px;
        }

        .title-label {
            font-size: 12px;
            position: absolute;
            top: -18px;
            left: 12px;
            color: #455A64;
        }
            .daterangepicker.dropdown-menu.ltr.opensleft.show-calendar {
            right: auto !important;
        }
            .combo-dropdown {
            display: inline-block;
            position: relative;
            margin-right: 10px;
        }

            .combo-dropdown p {
                position: absolute;
                top: -13px;
                font-size: 10px;
                left: 2px;
                color: #607D8B;
                text-transform: uppercase;
            }

            .combo-dropdown .select2-container {
                width: 140px !important;
                border: 1px solid #ccc;
                border-radius: 3px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%--hidden fileds--%>
    <asp:HiddenField runat="server" Value="0" ID="hdId" ClientIDMode="Static" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-4">
            <h3 class="page-title">Outward Register</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="f" id="btnFind"data-toggle="tooltip" data-placement="bottom" title="View previous register out"  class="btn btn-default waves-effect waves-light"><i class="ion-search"></i>&nbsp;Find</button>
                <button id="btnNew" type="button" accesskey="n" data-toggle="tooltip" data-placement="bottom" title="Start a new register out . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" id="btnSave" accesskey="s" data-toggle="tooltip" data-placement="bottom" title="Save the current register out" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
            </div>
             <div class="pull-right form-inline">
                <div class="combo-dropdown">
                    <div class="col-xs-6 p-l-0">
                        <p>Cost Center</p>
                        <asp:DropDownList ID="ddlCostCenter" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                    <div class="col-xs-6 p-l-0">
                        <p>Job</p>
                        <asp:DropDownList ID="ddlJob" class="searchDropdown" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ---- Search Quote Panel ---- --%>
    <div class="row search-quote-panel">
        <div class="col-sm-12">
            <div class="row">
                <div class="col-sm-5">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="col-sm-7">
                                <label class="title-label">Add to list from here..</label>
                                <asp:TextBox ID="txtChooser" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Choose Item"></asp:TextBox>
                                <div id="lookup">
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label id="lblquantityError" style="display: none; color: indianred" class="title-label">..</label>
                                <asp:TextBox ID="txtQuantity" TextMode="Number" autocomplete="off" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Qty"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 text-center">
                                <button type="button" id="btnAdd" data-toggle="tooltip" data-placement="bottom" title="Add to List" class="btn btn-icon btn-primary"><i class="ion-plus"></i></button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-5">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="col-sm-4">

                                <label class="">
                                    <asp:RadioButton ID="rdbCustomer" ClientIDMode="Static" GroupName="toWho" runat="server" />
                                    To Customer</label>
                                <label class="">
                                    <asp:RadioButton ID="rdbEmployee" GroupName="toWho" ClientIDMode="Static" runat="server" />
                                    To Employee</label>
                                <label class="">
                                    <asp:RadioButton ID="rdbOther" GroupName="toWho" ClientIDMode="Static" runat="server" />
                                    Others</label>

                            </div>
                            <div class="col-sm-6">

                                <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown hidden" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="searchDropdown hidden" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txtOthers" ClientIDMode="Static" CssClass="form-control round-no-border hidden" runat="server"></asp:TextBox>

                            </div>

                        </div>
                    </div>
                </div>

                <div class="col-sm-2">
                    <div class="panel b-r-8">
                        <div class="panel-body" style="padding-top: 7px; padding-bottom: 7px;">
                            <div class="row">
                                <div class="col-sm-12">
                                    <span>Order No :</span>
                                    <asp:Label ID="lblOrderNo" ClientIDMode="Static" runat="server" CssClass="badge badge-danger pull-right" Text="856542"></asp:Label>
                                    <div class="clearfix"></div>
                                </div>
                                <div>
                                    <div class="col-sm-12">
                                        <span>Date : </span>
                                        <input type="text" id="txtEntryDate" style="width: 60%;" class="date-info" value="01/Oct/2017" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ---- Data Table ---- --%>
    <div class="row data-table">
        <div class="col-sm-12">
            <div class="panel panel-default view-h b-r-8">
                <div class="panel-body p-t-10">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <table id="listTable" class="table table-hover">
                                <thead>
                                    <tr>
                                        <th class="hidden">item ID</th>
                                        <th>Item</th>
                                        <th>Code</th>
                                        <th>Tax%</th>
                                        <th>MRP</th>
                                        <th>Qty</th>
                                        <th style="display:none">instanceId</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ---- Summary Panel ---- --%>
    <div class="row summary-panel">
      
    </div>


    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">
            <!-- Modal content-->
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                                   <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Register Out &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
                    <div id="filterWrap" class="hide">
                      
                        <label>Filter by Date Range :</label>
                        <input type="text" name="daterange" class="form-control" id="txtDate" value="01/01/2015 - 01/31/2015" />
                        <div class="btn-toolbar pull-right m-t-10 m-b-10">
                            <button id="applyFilter" type="button" class="btn btn-default btn-sm">Apply Filter</button>
                            <button id="filterCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                        </div>
                    </div> 
                </div>
                <div class="modal-body p-b-0">
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">Id</th>
                                <th>Entry No</th>
                                <th>Date</th>
                                <th>Tax</th>
                                <th>Gross</th>
                                <th>Net Amount</th>
                                <th>Status</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <%--find list modal ends here--%>





    <script>

        $(document).ready(function () {

            //Getting Entry for edit purpose if user asked for it
            var Params = getUrlVars();
            if (Params.UID != undefined && !isNaN(Params.UID)) {
                resetRegister();
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/RegisterOut/get/' + Params.UID,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        
                        try
                        {
                            var html = '';
                            var register = response;
                            if (register.CustomerId != null && register.CustomerId != '' && register.CustomerId !== '0') {
                                $('#ddlCustomer').select2('val', register.CustomerId);
                                $('#ddlCustomer').val(register.CustomerId);
                                $('#rdbCustomer').prop('checked', true);
                                $('#ddlCustomer').removeClass('hidden');
                                $('#txtOthers').addClass('hidden');
                                $('#ddlEmployee').addClass('hidden');
                            }
                            else if (register.EmployeeId != null && register.EmployeeId != '' && register.EmployeeId !== '0') {
                                $('#ddlEmployee').select2('val', register.EmployeeId);
                                $('#ddlEmployee').val(register.EmployeeId);
                                $('#rdbEmployee').prop('checked', true);
                                $('#ddlEmployee').removeClass('hidden');
                                $('#ddlCustomer').addClass('hidden');
                                $('#txtOthers').addClass('hidden');


                            }
                            else if (register.Others != null && register.Others != '' && register.Others !== '0') {

                                $('#txtOthers').val(register.Others);
                                $('#rdbOther').prop('checked', true);
                                $('#txtOthers').removeClass('hidden');
                                $('#ddlCustomer').addClass('hidden');
                                $('#ddlEmployee').addClass('hidden');
                            }
                            else {
                                $('#ddlCustomer').addClass('hidden');
                                $('#txtOthers').addClass('hidden');
                                $('#ddlEmployee').addClass('hidden');
                            }
                            $('#txtEntryDate').val(register.EntryDateString);
                            $('#txtNarration').val(register.Narration);
                            $('#ddlCustomer').select2('val', register.CustomerId);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);
                            $('#ddlCustomer').val(register.CustomerId);
                            $('#ddlEmployee').select2('val', register.EmployeeId);
                            $('#ddlEmployee').val(register.EmployeeId);
                            $('#txtOthers').val(register.Others);
                            if (Params.MODE != null && Params.MODE != undefined) {
                                if (Params.MODE == 'clone') {
                                    $('#hdId').val('0');
                                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                }
                                else if (Params.MODE == 'edit') {
                                    $('#hdId').val(register.ID);
                                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                }
                                else if (Params.MODE == 'new') {
                                    $('#hdId').val('0');
                                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                                }
                            }
                            else {
                                $('#hdId').val(register.ID);
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                            }
                            html = '';

                            $(register.Products).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ItemID + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td>' + this.TaxPercentage + '</td>';
                                html += '<td>' + this.MRP + '</td>';
                                html += '<td>' + this.Quantity + '</td>';
                                html += '<td style="display:none">' + this.InstanceId + '</td>';
                                html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                html += '</tr>';
                            });
                            $('#listTable tbody').append(html);
                            $('#lblOrderNo').text(register.OrderNo);
                            $('#findModal').modal('hide');
                            //binding delete
                            $('.delete-row').click(function () {
                                $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                            });
                        }
                        catch(err)
                        {
                            console.log(err);
                            errorAlert('Entry not found');
                        }
                        finally
                        {
                            loading('stop', null);
                        }
                         
                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); },
                    beforeSend: function () { loading('start', null) },
                    complete: function () { loading('stop', null); },
                });
            }
            else {
                if (Params.JOB != undefined && !isNaN(Params.JOB)) {
                    var job = getUrlVars()["JOB"];
                    var jobOpts = $('#ddlJob').children('option');
                    for (var i = 0; i < jobOpts.length; i++) {
                        if ($(jobOpts[i]).val() == job) {
                            $('#ddlJob').select2('val', job);
                            break;
                        }
                        else {
                            $('#ddlJob').select2('val', '0');
                        }
                    }
                    $('#hdId').val('0');
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                }

                if (Params.COSTCENTER != undefined && !isNaN(Params.COSTCENTER)) {
                    var costCenter = getUrlVars()["COSTCENTER"];
                    var costOptn = $('#ddlCostCenter').children('option');
                    for (var i = 0; i < costOptn; i++) {
                        if ($(costOptn[i]).val() == costCenter) {
                            $('#ddlCostCenter').select2('val', costCenter);
                            break;
                        }
                        else {
                            $('#ddlCostCenter').select2('val', '0');
                        }
                    }
                    $('#ddlCostCenter').select2('val', costCenter);
                    $('#hdId').val('0');
                    $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                }
            }
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());


            // radio button click event
            $('#rdbCustomer').click(function () {
                $('#ddlCustomer').removeClass('hidden');
                $('#ddlEmployee').addClass('hidden');
                $('#txtOthers').addClass('hidden');
            })
            $('#rdbEmployee').click(function () {
                $('#ddlCustomer').addClass('hidden');
                $('#ddlEmployee').removeClass('hidden');
                $('#txtOthers').addClass('hidden');
            })
            $('#rdbOther').click(function () {
                $('#ddlCustomer').addClass('hidden');
                $('#ddlEmployee').addClass('hidden');
                $('#txtOthers').removeClass('hidden');
            })
            // radio button click event ends

            $('[data-toggle="popover"]').popover({
                content: "<textarea placeholder=\" Enter Narration Here\"></textarea>"
            });

            $('#txtEntryDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',
               
                todayHighlight: true
            });

            $('#txtEntryDate').datepicker('setDate', today);


            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtEntryDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtEntryDate').datepicker('hide');
           });

            //lookup initialization
            var locationId = $.cookie('bsl_2');
            lookup({
                textBoxId: 'txtChooser',
                url: $('#hdApiUrl').val() + 'api/Search/ItemsFromPurchaseWithStock?LocationId=' + locationId + '&keyword=',
                lookupDivId: 'lookup',
                focusToId: 'txtQuantity',
                storageKey: 'tempItem',
                heads: ['ItemID', 'InstanceId', 'Name', 'ItemCode', 'TaxPercentage', 'MRP', 'Stock'],
                visibility: [false, false, true, true, true, true, true],
                alias: ['ItemID', 'InstanceId', 'Item', 'SKU', 'Tax', 'MRP', 'Stock'],
                key: 'ItemID',
                dataToShow: 'Name',
                OnLoading: function () { miniLoading('start') },
                OnComplete: function () { miniLoading('stop') }
            });
            //lookup initialization ends here

            //Add Item to list
            $('#btnAdd').click(function () {
                AddToList();

            });
            function AddToList() {
                var tempItem = JSON.parse(sessionStorage.getItem('tempItem'));
                var taxper = parseFloat(tempItem.TaxPercentage);
                //var rate = parseFloat(tempItem.CostPrice);
                var qty = parseFloat($('#txtQuantity').val());
                if (tempItem.ItemID != '' & tempItem.ItemID != null & tempItem.ItemID != undefined & qty != '0' & qty != '' & qty != null) {
                    if (parseFloat(tempItem.Stock) < qty) {
                        $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                    }
                    else {
                        var Rows = $('#listTable > tbody').children('tr');
                        var itemExists = false;
                        var rowOfItem;
                        $(Rows).each(function () {
                            var itemId = $(this).children('td').eq(0).text();
                            var instanceId = $(this).children('td').eq(6).text();
                            if (tempItem.ItemID == itemId && tempItem.InstanceId == instanceId) {
                                itemExists = true;
                                rowOfItem = this;
                            }

                        });
                        if (itemExists) {
                            var existingQty = parseFloat($(rowOfItem).children('td').eq(5).text());
                            var newQty = existingQty + qty;
                            if (parseFloat(tempItem.Stock) < newQty) {

                                $('#lblquantityError').text('Quantity Exceeds').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                            }
                            else {
                                $(rowOfItem).children('td').eq(5).replaceWith('<td>' + newQty + '</td>');
                                sessionStorage.removeItem('tempItem');
                                $('#txtQuantity').val('');
                                $('#txtChooser').val('');
                                $('#txtChooser').focus();
                                highlightRow(rowOfItem, '#ffda4d');
                                //Binding Event to remove button
                                $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                                $('#lblquantityError').text('Quantity Updated').fadeIn(1000, function () { $('#lblquantityError').fadeOut('slow') });
                            }

                        }
                        else {
                            html = '';
                            html += '<tr><td style="display:none">' + tempItem.ItemID + '</td><td>' + tempItem.Name + '</td><td>' + tempItem.ItemCode + '</td><td>' + tempItem.TaxPercentage + '</td><td>' + tempItem.MRP + '</td><td>' + qty + ' </td><td style="display:none">' + tempItem.InstanceId + '</td><td style="display:none">0</td><td>  <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td></tr>';
                            $('#listTable > tbody').append(html);
                            sessionStorage.removeItem('tempItem');
                            $('#txtQuantity').val('');
                            $('#txtChooser').val('');
                            $('#txtChooser').focus();
                            //Binding Event to remove button
                            $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                        }
                    }
                }

            }
            //Add Item to list with enter key           
            $('#txtQuantity').keypress(function (e) {

                if (e.which == 13) {
                    e.preventDefault();
                    AddToList();
                }

            });

            //Save functionality
            $('#btnSave').off().click(function () {
                save();
            });
            //Function for Saving the register
            function save() {

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
                        var data = {};
                        var arr = [];
                        var tbody = $('#listTable > tbody');
                        var tr = tbody.children('tr');
                        var entryDate = $('#txtEntryDate').val();
                        var narration = $('#txtNarration').val();
                        var CompanyId = $.cookie('bsl_1');
                        var FinancialYear = $.cookie('bsl_4');
                        var createdBy = $.cookie('bsl_3');
                        for (var i = 0; i < tr.length; i++) {

                            var id = $(tr[i]).children('td:nth-child(1)').text();
                            var qty = $(tr[i]).children('td:nth-child(6)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(7)').text();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.ItemID = id;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.LocationId = $.cookie('bsl_2');
                        data.EntryDate = entryDate;
                        if ($('#rdbCustomer').is(':checked')) {
                            data.CustomerId = $('#ddlCustomer').val();

                        }
                        if ($('#rdbEmployee').is(':checked')) {
                            data.EmployeeId = $('#ddlEmployee').val();
                        }
                        if ($('#rdbOther').is(':checked')) {
                            data.Others = $('#txtOthers').val();
                        }
                        data.EntryDateString = entryDate;
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
                        data.Products = arr;
                        data.Narration = narration;
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.CompanyId = CompanyId;
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        data.Status = 0;

                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/RegisterOut/Save',
                            method: 'POST',
                            data: JSON.stringify(data),
                            contentType: 'application/json',
                            dataType: 'JSON',
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    resetRegister();
                                    $('#lblOrderNo').text(response.Object.OrderNo);

                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                                miniLoading('stop');
                            },
                            beforeSend: function () { miniLoading('start') },
                            complete: function () { miniLoading('stop') }
                        });
                    }

                });


            }//Save Function Ends here

            //Find function start
            $('#btnFind').click(function () {
                //Find Filter Function
                $(function () {
                    $('#findFilter').popover({
                        placement: 'bottom',
                        html: true,
                        content: $('#filterWrap').html()
                    }).on('click', function () {
                        //inititalize date range picker
                        $('input[name="daterange"]').daterangepicker({
                            "opens": "left",
                            "startDate": moment().startOf('month'),
                            "endDate": moment().endOf('month'),
                            "alwaysShowCalendars": false,
                            locale: {
                                format: 'DD MMM YYYY'
                            },
                            ranges: {
                                'Today': [moment(), moment()],
                                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                                'This Month': [moment().startOf('month'), moment().endOf('month')],
                                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                            }
                        });
                        //inititalize select 2 ddl
                        $("#ddlSupplierFilter").select2({
                            width: '100%'
                        });

                        // Apply Filter Click
                        $('#applyFilter').click(function () {
                            //Filter Logic Here
                            var fromDate = $('#txtDate').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                            var toDate = $('#txtDate').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                            refreshTable(fromDate, toDate);
                            $('#findFilter').popover('hide');
                            $('body').on('hidden.bs.popover', function (e) {
                                $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                            });
                        })
                        // Cancel Filter Click
                        $('#filterCancel').click(function () {
                            $('#findFilter').popover('hide');
                            $('body').on('hidden.bs.popover', function (e) {
                                $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                            });
                        })
                    })
                });
                $('#findModal').modal('show');
                resetRegister();
                refreshTable(null, null);
                function refreshTable(fromDate, toDate) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/RegisterOut/Get?from=' + fromDate + '&to=' + toDate,
                        method: 'POST',
                        dataType: 'JSON',
                        data: JSON.stringify($.cookie('bsl_2')),
                        contentType: 'application/json;charset=utf-8',
                        success: function (response) {
                            var html = '';
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.OrderNo + '</td>';
                                html += '<td>' + this.EntryDateString + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += this.Status == 0 ? '<td><span class="label label-danger">In Active</span></td>' : '<td><span class="label label-default">Active</span></td>';
                                html += '<td><a class="edit-register" title="edit" href="#"><i class="fa fa-edit"></i></a></td>'
                                html += '</tr>';
                            });
                            $('#tblRegister').DataTable().destroy();
                            $('#tblRegister tbody').children().remove();
                            $('#tblRegister tbody').append(html);
                            $('#tblRegister').dataTable({ destroy: true, aaSorting: [], "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]] });
                            //binding event to row
                            $('#tblRegister').off().on('click', '.edit-register', function () {
                                resetRegister();


                                var registerId = $(this).closest('tr').children('td').eq(0).text();
                                var register = {};
                                $(response).each(function () {
                                    if (this.ID == registerId) {
                                        register = this;
                                    }
                                });
                                if (register.CustomerId != null && register.CustomerId != '' && register.CustomerId !== '0') {
                                    $('#ddlCustomer').select2('val', register.CustomerId);
                                    $('#ddlCustomer').val(register.CustomerId);
                                    $('#rdbCustomer').prop('checked', true);

                                    $('#ddlCustomer').removeClass('hidden');

                                    $('#txtOthers').addClass('hidden');
                                    $('#ddlEmployee').addClass('hidden');


                                }
                                else if (register.EmployeeId != null && register.EmployeeId != '' && register.EmployeeId !== '0') {
                                    $('#ddlEmployee').select2('val', register.EmployeeId);
                                    $('#ddlEmployee').val(register.EmployeeId);
                                    $('#rdbEmployee').prop('checked', true);
                                    $('#ddlEmployee').removeClass('hidden');
                                    $('#ddlCustomer').addClass('hidden');
                                    $('#txtOthers').addClass('hidden');


                                }
                                else if (register.Others != null && register.Others != '' && register.Others !== '0') {

                                    $('#txtOthers').val(register.Others);
                                    $('#rdbOther').prop('checked', true);
                                    $('#txtOthers').removeClass('hidden');
                                    $('#ddlCustomer').addClass('hidden');
                                    $('#ddlEmployee').addClass('hidden');
                                }
                                else {
                                    $('#ddlCustomer').addClass('hidden');
                                    $('#txtOthers').addClass('hidden');
                                    $('#ddlEmployee').addClass('hidden');
                                }
                                $('#txtEntryDate').val(register.EntryDateString);
                                $('#txtNarration').val(register.Narration);
                                $('#ddlCustomer').select2('val', register.CustomerId);
                                $('#ddlCustomer').val(register.CustomerId);
                                $('#ddlEmployee').select2('val', register.EmployeeId);
                                $('#ddlEmployee').val(register.EmployeeId);
                                $('#txtOthers').val(register.Others);
                                $('#hdId').val(register.ID);
                                html = '';

                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td>' + this.Name + '</td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td>' + this.TaxPercentage + '</td>';
                                    html += '<td>' + this.MRP + '</td>';
                                    html += '<td>' + this.Quantity + '</td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '</tr>';
                                });
                                $('#listTable tbody').append(html);
                                $('#lblOrderNo').text(register.OrderNo);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                $('#ddlJob').select2('val', register.JobId);
                                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                $('#findModal').modal('hide');
                                //binding delete
                                $('.delete-row').click(function () {
                                    $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                                });
                            });
                        },
                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                }
            });
            //find function ends here
            //BtnNew starts here 
            $('#btnNew').click(function () {
                resetRegister();
            });

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#ddlCustomer').select2('val', 0);
                $('#ddlEmployee').select2('val', 0);
                $('#hdId').val('');
                $('#txtEntryDate').datepicker('setDate', today);
                $('#btnSave').html('<i class=\"ion-archive"\></i>&nbsp;Save');
                $('#ddlCostCenter').select2('val', '0');
                $('#ddlJob').select2('val', '0');
            }//Reset ends here


            //Delete functionality
            $('#btnDelete').click(function () {
                if ($('#hdId').val() != 0) {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",
                      
                        showConfirmButton: true,closeOnConfirm:true,
                        showCancelButton: true,
                        
                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    }, function (isConfirm) {

                        var id = $('#hdId').val();
                        var modifiedBy = $.cookie('bsl_3');
                        if (isConfirm) {
                            $.ajax({
                                url: $('#hdApiUrl').val() + 'api/RegisterOut/delete/' + id,
                                method: 'DELETE',
                                datatype: 'JSON',
                                contentType: 'application/json;charset=utf-8',
                                data: JSON.stringify(modifiedBy),
                                success: function (response) {
                                    if (response.Success) {
                                        successAlert(response.Message);
                                        resetRegister();
                                    }
                                    else {
                                        errorAlert(response.Message);
                                    }
                                },
                                error: function (xhr) {
                                    alert(xhr.responseText); console.log(xhr);
                                    miniLoading('stop');
                                },
                                beforeSend: function () { miniLoading('start') },
                                complete: function () { miniLoading('stop') }
                            });
                        }

                    });
                }

            });
            //delete function ends here
        });
        //document ready function ends here
    </script>
        <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
                        <!-- Date Range Picker -->
    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
</asp:Content>
