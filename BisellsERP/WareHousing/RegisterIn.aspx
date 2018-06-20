<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterIn.aspx.cs" Inherits="BisellsERP.WareHousing.RegisterIn" %>

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
            <h3 class="page-title">Inward Register</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="v"id="btnFind" data-toggle="tooltip" data-placement="bottom" title="View previous register in"  class="btn btn-default waves-effect waves-light"><i class="ion-search"></i></button>
                <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new inward register . Unsaved data will be lost" class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current register in" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
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
                <div class="col-sm-10">
                    <div class="panel b-r-8">
                        <div class="panel-body">
                            <div class="col-sm-5">

                                <label class="">
                                    <asp:RadioButton ID="rdbCustomer" ClientIDMode="Static" GroupName="toWho" runat="server" />
                                    From Customer</label>
                                <label class="">
                                    <asp:RadioButton ID="rdbEmployee" GroupName="toWho" ClientIDMode="Static" runat="server" />
                                    From Employee</label>
                                <label class="">
                                    <asp:RadioButton ID="rdbOther" GroupName="toWho" ClientIDMode="Static" runat="server" />
                                    Others</label>

                            </div>
                            <div class="col-sm-6">

                                <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown hidden" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="ddlEmployee" ClientIDMode="Static" CssClass="searchDropdown hidden" runat="server"></asp:DropDownList>
                                <asp:TextBox ID="txtOthers" ClientIDMode="Static" CssClass="form-control round-no-border hidden " runat="server"></asp:TextBox>

                            </div>
                            <div class="col-sm-1">
                                <div class="btn-group" data-toggle="tooltip" title="Link Register Out">
                                    <button type="button" id="btnLink" class="btn btn-icon waves-effect waves-light btn-warning">
                                        <i class="ion-pull-request text-bold"></i>
                                    </button>
                                </div>
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
                                        <th>Qty&nbsp;<sup><i style="color: #ccc;" class="fa fa-pencil-square-o"></i></sup></th>
                                        <th class="hidden">InstanceId</th>
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
                    <h4 class="modal-title">Previous Register In &nbsp;<a id="findFilter"><i class="fa fa-align-justify"></i></a></h4>
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

    <%-- ---- Modal for Quote Building ---- --%>
    <%-- ---- Modal for Quote Building ---- --%>
    <div id="myModal" class="modal animated fadeIn" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" style="display: none;">
        <div class="modal-dialog modal-dialog-w-lg">
            <div class="modal-content modal-content-h-lg">
                <div class="modal-header">
                    <div class="modal-title" id="myModalLabel">
                    </div>
                </div>
                <div class="modal-body modal-body-lg">
                    <table id="poTable" class="table table-hover table-striped table-responsive">
                        <thead class="bg-blue-grey ">
                            <tr>
                                <th class="hidden">ID</th>
                                <th class="tect-white">Location</th>
                                <th class="text-white">Order No</th>
                                <th class="text-white">Tax</th>
                                <th class="text-white">Gross</th>
                                <th class="text-white">Net</th>
                                <th>
                                    <input type="checkbox" class="checkbox checkbox-primary chk-all" /></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer modal-foo-lg">
                    <div class="row">
                        <div class="col-sm-3 col-md-5 text-left">
                            <h4 class="m-b-0">Total : &nbsp<label class="text-success" id="noOfItems"></label></h4>
                        </div>

                        <div class="col-sm-5 col-md-7">
                            <div class="btn-toolbar pull-right">
                                <button id="btnMerge" class="btn btn-primary waves-effect waves-light" aria-expanded="true" type="button"><i class="ion ion-steam"></i>Merge</button>
                                <button type="button" class="btn btn-inverse waves-effect waves-light" data-dismiss="modal" aria-hidden="true">x</button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <script>
        
    </script>

    <script>

        $(document).ready(function () {

            //Getting Entry for edit purpose if user asked for it
            var Params = getUrlVars();
            if (Params.UID != undefined && !isNaN(Params.UID)) {
                resetRegister();

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Registerin/get/' + Params.UID,
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify($.cookie('bsl_2')),
                    contentType: 'application/json;charset=utf-8',
                    success: function (response) {
                        try
                        {
                            var html = '';
                            var register = response;
                            //else if ladder used for to display users basis of available data

                            if (register.CustomerId != null && register.CustomerId != '' && register.CustomerId !== '0') {
                                $('#ddlCustomer').select2('val', register.CustomerId);
                                $('#ddlCustomer').val(register.CustomerId);
                                $('#rdbCustomer').prop('checked', true);
                                $('#ddlCustomer').removeClass('hidden');

                            }
                            else if (register.EmployeeId != null && register.EmployeeId != '' && register.EmployeeId !== '0') {
                                $('#ddlEmployee').select2('val', register.EmployeeId);
                                $('#ddlEmployee').val(register.EmployeeId);
                                $('#rdbEmployee').prop('checked', true);
                                $('#ddlEmployee').removeClass('hidden');
                            }
                            else if (register.Others != null && register.Others != '' && register.Others !== '0') {
                                $('#txtOthers').val(register.Others);
                                $('#rdbOther').prop('checked', true);
                                $('#txtOthers').removeClass('hidden');
                            }
                            else {
                                $('#ddlCustomer').addClass('hidden');
                                $('#txtOthers').addClass('hidden');
                                $('#ddlEmployee').addClass('hidden');
                            }
                            $('#txtEntryDate').val(register.EntryDateString);
                            $('#txtNarration').val(register.Narration);
                            $('#ddlCostCenter').select2('val', register.CostCenterId);
                            $('#ddlJob').select2('val', register.JobId);

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
                                html += '<td style="display:none">' + this.RoId + '</td>';
                                html += '<td>' + this.Name + '</td>';
                                html += '<td>' + this.ItemCode + '</td>';
                                html += '<td>' + this.TaxPercentage + '</td>';
                                html += '<td>' + this.MRP + '</td>';
                                html += '<td><input type="number" class="edit-value" value="' + this.Quantity + '"?></td>';
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
            //date picker intialization
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
            //  date picker ends here

           

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
                            var qty = $(tr[i]).children('td').eq(6).children('input').val();
                            var roId = $(tr[i]).children('td:nth-child(2)').text();
                            var instanceId = $(tr[i]).children('td:nth-child(8)').text();
                            var detail = { "Quantity": qty };
                            detail.InstanceId = instanceId;
                            detail.RoId = roId;
                            detail.ItemID = id;
                            arr.push(detail);
                        }
                        data.ID = $('#hdId').val();
                        data.LocationId = $.cookie('bsl_2');
                        data.CostCenterId = $('#ddlCostCenter').val();
                        data.JobId = $('#ddlJob').val();
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
                        data.Products = arr;
                        data.Narration = narration;
                        data.ModifiedBy = $.cookie('bsl_3');
                        data.CompanyId = CompanyId;
                        data.FinancialYear = $.cookie('bsl_4');
                        data.CreatedBy = $.cookie('bsl_3');
                        data.UserId = $.cookie('bsl_3');
                        data.Status = 1;
                        $.ajax({
                            url: $(hdApiUrl).val() + 'api/RegisterIn/Save',
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
                                alert(xhr.responseText); console.log(xhr);
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
                refreshTable(null, null);
                function refreshTable(fromDate, toDate) {
                    resetRegister();
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Registerin/Get?from=' + fromDate + '&to=' + toDate,
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
                                //else if ladder used for to display users basis of available data

                                if (register.CustomerId != null && register.CustomerId != '' && register.CustomerId !== '0') {
                                    $('#ddlCustomer').select2('val', register.CustomerId);
                                    $('#ddlCustomer').val(register.CustomerId);
                                    $('#rdbCustomer').prop('checked', true);
                                    $('#ddlCustomer').removeClass('hidden');

                                }
                                else if (register.EmployeeId != null && register.EmployeeId != '' && register.EmployeeId !== '0') {
                                    $('#ddlEmployee').select2('val', register.EmployeeId);
                                    $('#ddlEmployee').val(register.EmployeeId);
                                    $('#rdbEmployee').prop('checked', true);
                                    $('#ddlEmployee').removeClass('hidden');
                                }
                                else if (register.Others != null && register.Others != '' && register.Others !== '0') {
                                    $('#txtOthers').val(register.Others);
                                    $('#rdbOther').prop('checked', true);
                                    $('#txtOthers').removeClass('hidden');
                                }
                                else {
                                    $('#ddlCustomer').addClass('hidden');
                                    $('#txtOthers').addClass('hidden');
                                    $('#ddlEmployee').addClass('hidden');
                                }
                                $('#txtEntryDate').val(register.EntryDateString);
                                $('#txtNarration').val(register.Narration);


                                $('#hdId').val(register.ID);
                                html = '';

                                $(register.Products).each(function () {
                                    html += '<tr>';
                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                    html += '<td style="display:none">' + this.RoId + '</td>';
                                    html += '<td>' + this.Name + '</td>';
                                    html += '<td>' + this.ItemCode + '</td>';
                                    html += '<td>' + this.TaxPercentage + '</td>';
                                    html += '<td>' + this.MRP + '</td>';
                                    html += '<td><input type="number" class="edit-value" value="' + this.Quantity + '"?></td>';
                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                    html += '<td> <i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                    html += '</tr>';
                                });
                                $('#listTable tbody').append(html);
                                $('#lblOrderNo').text(register.OrderNo);
                                $('#ddlCostCenter').select2('val', register.CostCenterId);
                                $('#ddlJob').select2('val', register.JobId);
                                $('#btnSave').html('<i class="ion-archive"></i>Update');
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
                $('#txtOthers').attr('disabled', false);
                $('#ddlEmployee').attr('disabled', false);
                $('#ddlCustomer').attr('disabled', false);
            });

            //Reset This Register
            function resetRegister() {
                reset();
                $('#listTable tbody').children().remove();
                $('#lookup').children().remove();
                $('#tblRegister tbody').children().remove();
                $('#hdId').val('');
                $('#txtEntryDate').datepicker('setDate', today);
                $('#ddlCustomer').addClass('hidden');
                $('#txtOthers').addClass('hidden');
                $('#ddlEmployee').addClass('hidden');
                $('#btnSave').html('<i class=\"ion-archive"\></i>Save');
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
                                url: $('#hdApiUrl').val() + 'api/Registerin/delete/' + id,
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
            //delete function ends here\

            // binding event to select All checkbox
            $('#poTable').on('change', '.chk-all', function () {

                if ($(this).is(':checked')) {
                    var rows = $('#poTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        if ($(rows[i]).children('td:last-child').children('input').prop('checked') != undefined) {
                            $('.chk-single').prop('checked', true);
                            $(rows[i]).addClass('selected-row');
                        }
                    }
                }
                else {
                    var rows = $('#poTable tbody').children('tr');
                    for (var i = 0; i < rows.length; i++) {
                        $('.chk-single').prop('checked', false);
                        $(rows[i]).removeClass('selected-row');
                    }
                }

            });

            //binding event to single checkbox
            $('#poTable').on('click', '.chk-single', function () {

                if ($(this).is(':checked')) {
                    $(this).closest('tr').children('td').eq(6).children('input').focus().select();
                    $(this).closest('tr').addClass('selected-row');
                }
                else {
                    $(this).closest('tr').removeClass('selected-row');
                }
            });


            setInterval(function () {
                var rows = $('#poTable tbody').children('tr');
                var noOfItems = 0.00;
                rows.each(function (index) {
                    if ($(this).find('.chk-single').is(':checked')) {

                        noOfItems++;
                    }

                });
                $('#noOfItems').text(noOfItems);
            }, 500);

            //link  button click
            $('#btnLink').click(function () {
                var type = 0;
                var id = 0;
                if ($('#rdbCustomer').is(':checked')) {

                    type = 1;
                    id = $('#ddlCustomer').val();
                }
                else if ($('#rdbEmployee').is(':checked')) {

                    type = 2;
                    id = $('#ddlEmployee').val();
                }
                else if ($('#rdbOther').is(':checked')) {

                    type = 3;
                    if ($('#txtOthers').val() != '') {
                        id = $('#txtOthers').val();
                    }
                    else {
                        id = 'other';
                    }
                }

                $('#poTable tbody').children().remove();

                var locationId = $.cookie('bsl_2');
                var companyId = $.cookie('bsl_1');
                var financialYear = $.cookie('bsl_4');

                if (type != 0 && id != 0) {
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/RegisterIn/GetUserwise/?id=' + id + '&type=' + type,
                        method: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        dataType: 'JSON',
                        data: (locationId),
                        success: function (response) {
                            var html = '';

                            $(response).each(function () {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.ID + '</td>';
                                html += '<td>' + this.Location + '</td>';
                                html += '<td>' + this.OrderNo + '</td>';
                                html += '<td>' + this.TaxAmount + '</td>';
                                html += '<td>' + this.Gross + '</td>';
                                html += '<td>' + this.NetAmount + '</td>';
                                html += '<td><input type="checkbox" class="checkbox chk-single"></td>';
                                html += '</tr>';
                            });
                            $('#txtOthers').attr('disabled', false);
                            $('#ddlEmployee').attr('disabled', false);
                            $('#ddlCustomer').attr('disabled', false);

                            $('#poTable tbody').append(html);
                            $('#myModal').modal('show');
                            //merge  button 
                            $('#btnMerge').click(function () {

                                rows = $('#poTable tbody').children('tr');
                                //else if ladder used for to display users basis of available data [type is used for identify user type]
                                if (type == 1) {
                                    if (response[0].CustomerId !== 0 && response[0].CustomerId != '') {
                                        $('#ddlCustomer').val(response[0].CustomerId);
                                        $('#ddlCustomer').removeClass('hidden');
                                        $('#rdbCustomer').prop('checked', true);
                                        $('#rdbEmployee').prop('checked', false);
                                        $('#rdbOther').prop('checked', false);
                                        $('#txtOthers').addClass('hidden');
                                        $('#ddlEmployee').addClass('hidden');
                                        $('#txtOthers').val('');
                                        $('#ddlEmployee').val(0);

                                        $('#txtOthers').attr('disabled', true);
                                        $('#ddlEmployee').attr('disabled', true);
                                        $('#ddlCustomer').attr('disabled', true);
                                    }
                                }
                                else if (type == 2) {
                                    if (response[0].EmployeeId !== 0 && response[0].EmployeeId != '') {
                                        $('#ddlEmployee').val(response[0].EmployeeId);
                                        $('#ddlEmployee').removeClass('hidden');
                                        $('#rdbCustomer').prop('checked', false);
                                        $('#rdbEmployee').prop('checked', true);
                                        $('#rdbOther').prop('checked', false);
                                        $('#ddlCustomer').addClass('hidden');
                                        $('#txtOthers').addClass('hidden');
                                        $('#ddlCustomer').val(0);
                                        $('#txtOthers').val('');
                                        $('#txtOthers').attr('disabled', true);
                                        $('#ddlEmployee').attr('disabled', true);
                                        $('#ddlCustomer').attr('disabled', true);

                                    }
                                }
                                else if (type == 3) {
                                    if (response[0].Others != null && response[0].Others != '') {
                                        $('#txtOthers').val(response[0].Others);
                                        $('#txtOthers').removeClass('hidden');
                                        $('#rdbCustomer').prop('checked', false);
                                        $('#rdbEmployee').prop('checked', false);
                                        $('#rdbOther').prop('checked', true);
                                        $('#ddlCustomer').addClass('hidden');
                                        $('#ddlCustomer').val(0);
                                        $('#ddlEmployee').val(0);
                                        $('#ddlEmployee').addClass('hidden');
                                        $('#txtOthers').attr('disabled', true);
                                        $('#ddlEmployee').attr('disabled', true);
                                        $('#ddlCustomer').attr('disabled', true);
                                    }
                                }

                                else {
                                    $('#ddlCustomer').addClass('hidden');
                                    $('#txtOthers').addClass('hidden');
                                    $('#ddlEmployee').addClass('hidden');
                                }



                                html = '';
                                $(rows).each(function () {
                                    if ($(this).children('td').eq(6).children('input').is(':checked')) {
                                        var RoId = $(this).children('td').eq(0).text();
                                        for (var i = 0; i < response.length; i++) {
                                            if (response[i].ID == RoId) {
                                                $(response[i].Products).each(function () {
                                                    html += '<tr>';
                                                    html += '<td style="display:none">' + this.ItemID + '</td>';
                                                    html += '<td style="display:none">' + this.DetailsID + '</td>';
                                                    html += '<td>' + this.Name + '</td>';
                                                    html += '<td>' + this.ItemCode + '</td>';
                                                    html += '<td>' + this.TaxPercentage + '</td>';
                                                    html += '<td>' + this.MRP + '</td>';
                                                    html += '<td><input type="number" class="edit-value" value="' + this.Quantity + '"/>';
                                                    html += '<td style="display:none">' + this.InstanceId + '</td>';
                                                    html += '<td><i class="glyphicon glyphicon-minus-sign text-danger delete-row"></i></td>';
                                                    html += '</tr>'
                                                });
                                                $('#listTable tbody').children().remove();
                                                $('#listTable tbody').append(html);
                                                $('#poTable tbody').children().remove();
                                                $('#myModal').modal('hide');

                                                break;
                                            }
                                        }
                                    }
                                });
                                //Binding Event to remove button
                                $('#listTable > tbody').off().on('click', '.delete-row', function () { $(this).closest('tr').fadeOut('slow', function () { $(this).remove(); }) });
                            });//Merge link button end here
                        },
                        error: function (err) {
                            alert(err.responseText)
                        },
                        beforeSend: function () { loading('start', null) },
                        complete: function () { loading('stop', null); },
                    });
                }

                else {

                    errorField('#rdbEmployee');

                }


            });//link button ends here



        });
        //document ready function ends here
    </script>
        <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
                    <!-- Date Range Picker -->
    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
</asp:Content>
