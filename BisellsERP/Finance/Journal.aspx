<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="BisellsERP.Finance.Journal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Journal Entry</title>
    <style>
        .journal-table > thead tr {
            background-color: #E8EAF6;
            border-bottom: 3px solid #dfe1ec;
        }

            .journal-table > thead tr > th {
                color: #757575;
                font-weight: 100;
                font-size: 14px;
            }

        .journal-table > tbody tr > td:first-child {
            text-align: center;
            line-height: 30px;
        }

        .journal-table > tbody tr > td:last-child > i {
            font-size: 20px;
            padding: 7px;
            color: #9E9E9E;
            cursor: pointer;
        }

        .journal-table > tbody tr > td:last-child:hover > i {
            color: #e94639;
        }

        .journal-table > tfoot tr {
            background-color: #f9faff;
        }
        
            .journal-table > tfoot tr > td:not(:last-child) {
                line-height: 30px;
                color: #757575;
            }

        .form-control.borderless, .form-control.borderless:hover, .form-control.borderless:focus {
            border-color: transparent;
            box-shadow: none;
        }

        #changeType {
            cursor: pointer;
        }

            #changeType > i {
                transform: translateY(3px) rotate(180deg);
            }

        .select2-container, .select2-drop, .select2-search, .select2-search input {
            width: 100%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%-- ---- Page Title ---- --%>
    <div class="row p-b-5">
        <div class="col-sm-6">
            <h3 class="page-title m-t-0">
                <label id="lblTitle" runat="server" clientidmode="static">Journal</label><label>&nbsp;Entry</label><span id="changeType" class="p-l-5 text-muted"><i class="md md-keyboard-capslock"></i></span></h3>
            <div id="typeWrap" class="hide">
                <label>Change Type :</label>
                <asp:DropDownList ID="ddlChangeType" ClientIDMode="Static" CssClass="" runat="server"></asp:DropDownList>
                <%--<select id="ddlChangeType" class="form-control VoucherType">
                </select>--%>
                <div class="btn-toolbar pull-right m-t-10 m-b-10">
                    <button id="btnChangeType" type="button" class="btn btn-default btn-sm">Change</button>
                    <button id="btnChangeCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="btn-toolbar pull-right" role="group">
                <button type="button" accesskey="f" data-toggle="tooltip" data-placement="bottom" title="View previous journal entries" id="btnFind" class="btn btn-default waves-effect waves-light"><i class="ion-search"></i>&nbsp;</button>
                <button id="btnNew" accesskey="n" type="button" data-toggle="tooltip" data-placement="bottom" title="Start a new entry . Unsaved data will be lost " class="btn btn-default waves-effect waves-light"><i class="ion-compose"></i>&nbsp;New</button>
                <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current entry" class="btn btn-default waves-effect waves-light "><i class="ion-archive"></i>&nbsp;Save</button>
                <button id="btnSavePrint" accesskey="a" type="button" data-toggle="tooltip" data-placement="bottom" title="Save & Print the current entry" class="btn btn-default waves-effect waves-light"><i class="ion ion-printer"></i>&nbsp;Save & Print</button>
                <button type="button" accesskey="p" id="btnPrint" data-toggle="tooltip" data-placement="bottom" title="Print" class="btn btn-default waves-effect waves-light "><i class="ion ion-printer"></i></button>
            </div>
        </div>
    </div>

    <div class="panel b-r-8">
        <div class="panel-body">
            <div class="row m-b-10 m-t-10">
                <div class="col-sm-2">
                    <p class="p-t-10">Journal No :
                         <label class="text-danger" runat="server" id="VoucherNumber" ClientIDMode="Static">JL5241</label></p>
                </div>
                <div class="checkbox checkbox-primary col-sm-2">
                    <input type="checkbox" id="chkIsCheque" class="checkbox checkbox-primary"/><label>By Cheque</label>
                </div>
                <div class="hidden" id="dvCheque">
                    <div class="col-xs-2">
                        <label class="title-label">Cheque Number</label>
                        <input type="text" id="txtChequeNumber" class="form-control"/>
                    </div>
                    <div class="col-xs-2">
                        <label class="title-label">Cheque Date</label>
                        <input type="text" id="txtChequeDate" class="form-control"/>
                    </div>
                    <div class="col-xs-2">
                        <label class="title-label">Draw On</label>
                        <input type="text" id="txtDrawOn" class="form-control"/>
                    </div>
                </div>
                <div class="col-sm-2 col-lg-2 text-right pull-right">
                    <div class="input-group">
                        <input type="text" id="txtVoucherDate" class="form-control" placeholder="02-02/2018"/>
                        <span class="input-group-addon bg-none"><i class="fa fa-calendar"></i></span>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-xs-12">
                    <table id="dummy" class=" hidden">
                        <tbody>
                            <tr class="dummyRow">
                                <td class="slno">1</td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitDummyHead" ClientIDMode="Static" CssClass="account-heads" runat="server">
                                    </asp:DropDownList></td>
                                <td>
                                    <input type="text" class="form-control credit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control debit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitDummyCost" ClientIDMode="Static" class="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList CssClass="Job" runat="server" ID="ddlDebitDummyJob" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b remove-row"></i></td>
                            </tr>
                        </tbody>
                    </table>
                    <table id="VoucherTable" class="table table-bordered journal-table">
                        <thead>
                            <tr>
                                <th style="width: 40px" class="text-center">#</th>
                                <th style="width: 210px;">ACCOUNT</th>
                                <th style="width: 100px;">CREDIT</th>
                                <th style="width: 100px;">DEBIT</th>
                                <th style="width: 250px;">DESCRIPTION</th>
                                <th style="width: 200px;">COST CENTER</th>
                                <th style="width: 200px;">JOB</th>
                                <th style="width: 25px;"></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>1</td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" class="account-heads-Credit" ID="ddlCreditHead" runat="server"></asp:DropDownList></td>
                                <td>
                                    <input type="number" class="form-control credit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="number" class="form-control debit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlCreditCost" ClientIDMode="Static" class="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddlCreditJob" runat="server" CssClass="Job" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b"></i></td>
                            </tr>
                            <tr>
                                <td>2</td>
                                <td>
                                    <asp:DropDownList ID="ddlDebithead1" CssClass="account-heads" ClientIDMode="Static" runat="server"></asp:DropDownList></td>
                                <td>
                                    <input type="number" class="form-control credit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="number" class="form-control debit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitCost1" ClientIDMode="Static" CssClass="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList CssClass="Job" ID="ddlJobs1" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b remove-row"></i></td>
                            </tr>
                            <tr>
                                <td>3</td>
                                <td>
                                    <asp:DropDownList ID="ddlDebithead2" CssClass="account-heads" ClientIDMode="Static" runat="server"></asp:DropDownList></td>
                                <td>
                                    <input type="number" class="form-control credit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="number" class="form-control debit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitCost2" ClientIDMode="Static" CssClass="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList CssClass="Job" ID="ddlJobs2" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b remove-row"></i></td>
                            </tr>
                            <tr>
                                <td>4</td>
                                <td>
                                    <asp:DropDownList ID="ddlDebithead3" CssClass="account-heads" ClientIDMode="Static" runat="server"></asp:DropDownList></td>
                                <td>
                                    <input type="number" class="form-control credit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="number" class="form-control debit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitCost3" ClientIDMode="Static" CssClass="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList CssClass="Job" ID="ddlJobs3" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b remove-row"></i></td>
                            </tr>
                            <tr>
                                <td>5</td>
                                <td>
                                    <asp:DropDownList ID="ddlDebithead4" CssClass="account-heads" ClientIDMode="Static" runat="server"></asp:DropDownList></td>
                                <td>
                                    <input type="number" class="form-control credit-Amount numberonly" readonly="true" />
                                </td>
                                <td>
                                    <input type="number" class="form-control debit-Amount numberonly" />
                                </td>
                                <td>
                                    <input type="text" class="form-control entry-desc" placeholder="description" /></td>
                                <td>
                                    <asp:DropDownList ID="ddlDebitCost4" ClientIDMode="Static" CssClass="CostCenter" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList CssClass="Job" ID="ddlJobs4" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                                <td><i class="ion ion-trash-b remove-row"></i></td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2" class="text-right">Total</td>
                                <td class="text-right">
                                    <label class="credit-total">0</label></td>
                                <td class="text-right">
                                    <label class="debit-total">0</label></td>
                                <td colspan="4">
                                    <input type="text" class="form-control borderless Narration" placeholder="Enter Narration Here..." />
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                    <%--<div class="checkbox checkbox-primary col-sm-10 pull-right">
                        <input type="checkbox" id="chkIsCheque" class="checkbox checkbox-primary" /><label>Is Cheque</label>
                    </div>--%>
                    <button id="addRow" type="button" class="btn btn-success btn-custom waves-effect waves-light m-t-5">Add Row</button>
                    <input type="hidden" value="8" id="hdnVoucherType" clientidmode="static" runat="server" />
                    <input type="hidden" value="JNL" id="hdnVoucherTypeName" clientidmode="static" runat="server" />
                    <input type="hidden" value="0" id="hdnGroupID" clientidmode="static" runat="server" />
                    <input type="hidden" value="0" id="hdnVoucherNumber" clientidmode="static" runat="server" />
                </div>
                <%--<div class="col-xs-12 m-t-30 hidden" id="dvCheque">
                    <div class="col-xs-4">
                        <label class="title-label">Cheque Number</label>
                        <input type="text" id="txtChequeNumber" class="form-control" />
                    </div>
                    <div class="col-xs-4">
                        <label class="title-label">Cheque Date</label>
                        <input type="text" id="txtChequeDate" class="form-control" />
                    </div>
                    <div class="col-xs-4">
                        <label class="title-label">Draw On</label>
                        <input type="text" id="txtDrawOn" class="form-control" />
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
    <%--find list modal--%>
    <div id="findModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-dialog-w-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Previous Journal Entries</h4>
                </div>
                <div class="modal-body p-b-0">
                    <table id="tblRegister" class="table table-hover table-striped table-responsive table-scroll">
                        <thead>
                            <tr>
                                <th class="hidden">TransactionID</th>
                                <th>Particulars</th>
                                <th>Voucher Type</th>
                                <th>Voucher No</th>
                                <th>Amount</th>
                                <th>Cheque Number</th>
                                <th>Cheque Date</th>
                                <th class="hidden">Narration</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>


    <script>
        $(document).ready(function () {
            //PopOver for selecting voucher type
            $('#changeType').popover({
                placement: 'right',
                html: true,
                content: $('#typeWrap').html()
            }).on('click', function () {
                //inititalize select 2 ddl
                $("#ddlChangeType").select2({
                    width: '100%'
                });

                // Apply Filter Click
                $('#btnChangeType').click(function () {
                    var Vouchertype = $("#ddlChangeType").select2('data').id;
                    $('#hdnVoucherType').val(Vouchertype);
                    //Filter Logic Here
                    if (Vouchertype == "0") {
                        errorAlert('Select Type of voucher');
                    }
                    else {
                        var Voucherno = $("#ddlChangeType").select2('data').text;//Get text to Change Main Title
                        $('#hdnVoucherTypeName').val(Voucherno);
                        GetVoucherNo();//Get voucher Number
                        $('#lblTitle').text(Voucherno);//Changes The Main title
                    }
                    $('#changeType').popover('hide');
                    $('body').on('hidden.bs.popover', function (e) {
                        $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                    });
                })
                // Cancel Filter Click
                $('#btnChangeCancel').click(function () {
                    $('#changeType').popover('hide');
                    $('body').on('hidden.bs.popover', function (e) {
                        $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                    });
                })
            });
            $('#datepicker').datepicker();
            $('select.form-select').select2();

            //Used to add new rows to the table
            $('#addRow').click(function () {
                //Removes the select2 property of dummy table row 
                $('#dummy .dummyRow .account-heads').select2('destroy');
                $('#dummy .dummyRow .CostCenter').select2('destroy');
                $('#dummy .dummyRow .Job').select2('destroy');
                //Clones the dummyRow with normal select
                $('#dummy .dummyRow').clone(true).appendTo('.journal-table > tbody');
                //Add select2 property to the cloned Select
                $('#VoucherTable').find('.dummyRow .account-heads').select2();
                $('#VoucherTable').find('.dummyRow .CostCenter').select2();
                $('#VoucherTable').find('.dummyRow .Job').select2();
                //Removes the dummyrow class from the cloned row
                $('#VoucherTable').find('.dummyRow').removeClass('dummyRow');
                //Functionality to get serial number for the added row
                var tr = $('#VoucherTable>tbody').children('tr')
                $('.slno').text(parseInt(tr.length));
                $('#VoucherTable').find('.slno').removeClass('slno');
            });
            $('body').on('click', '.remove-row', function () {
                var tbllength = $('#VoucherTable tbody').children('tr').length;
                if (tbllength <= 2) {
                    errorAlert('Atleast Two lines should be in the table');
                }
                else {
                    $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove();calculateDebit();$('.credit-total').text($('.debit-total').text()); var tr = $('#VoucherTable>tbody').children('tr');
                        $(tr[0]).children('td:nth-child(3)').find('.credit-Amount').val($('.debit-total').text()); GetSeriallNo();
                    });
                }
            });

            //Function to add serial number in the table
            function GetSeriallNo() {
                var tr = $('#VoucherTable>tbody').children('tr');
                var length = tr.length;
                for (var i = 0; i < length; i++) {
                    $(tr[i]).eq(0).children('td:nth-child(1)').text(i+1);
                }
            }

            var apiurl = $('#hdApiUrl').val();

            //GetVoucherNo();
            $('#ddlCreditHead').select2();
            $('.account-heads').select2();
            $('.CostCenter').select2();
            $('.Job').select2();

            //This Function is used to get the URL parameters and to get the data from the database
            var Params = getUrlVars();
            if (Params.ID != undefined && !isNaN(Params.ID)) {
                reset();
                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                var GroupID = Params.ID;
                $.ajax({
                    url: apiurl + '/api/VoucherEntry/GetVoucherDataforEdit',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(GroupID),
                    success: function (response) {
                        //console.log(response);
                        var tr = $('#VoucherTable>tbody').children('tr');
                        for (var i = 0; i < response.Table.length; i++) {
                            var CostCenter = response.Table[i].CostCenter.split("`");;
                            var NewCost = CostCenter[0];
                            var selected = "";
                            $(tr[i]).find('.entry-desc').val(response.Table[i].Fve_ExpenseDesc);
                            if (i >= 5) {
                                
                                //$('#addRow').trigger('click');
                                //Adds a new row if the edit data has more than 5 rows
                                //Removes the select2 property of dummy table row 
                                $('#dummy .dummyRow .account-heads').select2('destroy');
                                $('#dummy .dummyRow .CostCenter').select2('destroy');
                                $('#dummy .dummyRow .Job').select2('destroy');
                                //Clones the dummyRow with normal select
                                $('#dummy .dummyRow').clone(true).appendTo('.journal-table > tbody');
                                //Add select2 property to the cloned Select
                                $('#VoucherTable').find('.dummyRow .account-heads').select2();
                                $('#VoucherTable').find('.dummyRow .CostCenter').select2();
                                $('#VoucherTable').find('.dummyRow .Job').select2();
                                //Removes the dummyrow class from the cloned row
                                $('#VoucherTable').find('.dummyRow').removeClass('dummyRow');
                                //Functionality to get serial number for the added row
                                var tr = $('#VoucherTable>tbody').children('tr');
                                $('.slno').text(parseInt(tr.length));
                                $('#VoucherTable').find('.slno').removeClass('slno');
                                selected = response.Table[i].ParticularsID + "|" + response.Table[i].CostHead;
                                $(tr[i]).find('.CostCenter').select2('val', NewCost);
                                if (response.Table[i].CreditOrDebit == 0) {
                                    $(tr[i]).find('.account-heads-Credit').select2('val', selected);
                                }
                                else {
                                    $(tr[i]).find('.account-heads').select2('val', selected);
                                }
                                if(response.Table[i].Fve_ExpenseDesc!=null||response.Table[i].Fve_ExpenseDesc!=""){
                                    $(tr[i]).find('.entry-desc').val(response.Table[i].Fve_ExpenseDesc);
                                }
                                alert(response.Table[i].Fve_ExpenseDesc);
                                //console.log(selected);
                                $(tr[i]).find('.Job').select2("val", response.Table[i].Job_ID);
                                if (response.Table[i].DebitAmt == 0) {
                                    $(tr[i]).find('.debit-Amount').val("");
                                }
                                else {
                                    $(tr[i]).find('.debit-Amount').val(response.Table[i].DebitAmt);
                                }
                                if (response.Table[i].creditAmt == 0) {
                                    $(tr[i]).find('.credit-Amount').val("");
                                }
                                else {
                                    $(tr[i]).find('.credit-Amount').val(response.Table[i].creditAmt);
                                }
                                calculateDebit();
                                calculateCredit();
                            }
                            else {
                                selected = response.Table[i].ParticularsID + "|" + response.Table[i].CostHead;
                                $(tr[i]).find('.CostCenter').select2('val', NewCost);
                                $(tr[i]).find('.Job').select2("val", response.Table[i].Job_ID);
                                if (response.Table[i].CreditOrDebit == 0) {
                                    $(tr[i]).find('.account-heads-Credit').select2('val', selected);
                                }
                                else {
                                    $(tr[i]).find('.account-heads').select2('val', selected);
                                }
                                //console.log(selected);
                                if (response.Table[i].DebitAmt == 0) {
                                    $(tr[i]).find('.debit-Amount').val("");
                                }
                                else {
                                    $(tr[i]).find('.debit-Amount').val(response.Table[i].DebitAmt);
                                }
                                if (response.Table[i].creditAmt == 0) {
                                    $(tr[i]).find('.credit-Amount').val("");
                                }
                                else {
                                    $(tr[i]).find('.credit-Amount').val(response.Table[i].creditAmt);
                                }
                                calculateDebit();
                                calculateCredit();
                            }
                            if (response.Table[0].Fve_IsCheque == true) {
                                var date = new Date(response.Table[0].Fve_ChequeDate);
                                var dateString = date.getDate() + '/' + getmonth(date.getMonth()+1) + '/' + date.getFullYear();
                                $('#chkIsCheque').prop('checked', true);
                                $('#dvCheque').fadeIn('slow');
                                $('#dvCheque').removeClass('hidden');
                                $('#txtChequeNumber').val(response.Table[0].Fve_ChequeNo);
                                $('#txtChequeDate').val(dateString);
                                $('#txtDrawOn').val(response.Table[0].Fve_Drawon);
                            }
                            else {
                                $('#chkIsCheque').prop('checked', false);
                                $('#dvCheque').fadeOut('slow');
                                $('#dvCheque').addClass('hidden');
                                $('#txtChequeNumber').val("");
                                $('#txtChequeDate').val("");
                                $('#txtDrawOn').val("");
                            }
                            $('#hdnVoucherNumber').val(response.Table[0].Fve_Number);
                            $('#hdnGroupID').val(response.Table[0].Fve_GroupID);
                            $('#VoucherNumber').text(response.Table[0].Voucher);
                            $('#txtVoucherDate').datepicker("update", new Date(response.Table[0].Fve_Date));
                            $('.Narration').val(response.Table[0].Fve_Description);
                            $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                            $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Voucher Save function call without print
            $('#btnSave').click(function () {
                save(false);
            });

            $('body').on('click', '#chkIsCheque', function () {
                if ($('#chkIsCheque').prop('checked')) {
                    $('#dvCheque').fadeIn('slow');
                    $('#dvCheque').removeClass('hidden');
                    $('#txtChequeDate').datepicker('setDate', today);
                }
                else {
                    $('#dvCheque').fadeOut('slow');
                    $('#dvCheque').removeClass('hidden');
                    $('#txtChequeNumber').val("");
                    $('#txtChequeDate').val("");
                    $('#txtDrawOn').val("");
                }

            });

            //Voucher Save function call with print
            $('#btnSavePrint').click(function () {
                save(true);
            });

            //Save Function Definition
            function save(print) {
                var voucher = {};
                var Heads = "";//Stores the head ID seperated by | symbol
                var child = "";//Stores the Child ID seperated by | symbol
                var amount = "";
                var costcenter = "";
                var JobString = "";
                var tempData = "";//Data Used to Store The Select2 value; 
                var creditorDebit = "";//Flag used to identify credit or debit.1 if Debit 0 if Credit.Seperated by | symbol
                var creditorDebitTemp = "";//Flag used to identify credit or debit.1 if Debit 0 if Credit.
                var EntryDesc = "";
                var tr = $('#VoucherTable>tbody').children('tr');
                var debitTotal = $('.debit-total').text();//Final debit total(Sum of debit amounts)
                var creditTotal = $('.credit-total').text();//Final Credit total(Sum of credit amounts)
                var flag = 0;
                if ($('.debit-total').text() != $('.credit-total').text()) {
                    if (isNaN($('.debit-total').text()) || isNaN($('.credit-total').text())) {
                        errorAlert('Enter valid Amount');
                    }
                    else {
                        errorAlert('Credit And Debit Amount Are Not Equal');
                    }
                }
                else if (debitTotal != "0" && creditTotal != "0") {
                    for (var i = 0; i < tr.length; i++) {
                        var debit = $(tr[i]).children('td:nth-child(4)').find('.debit-Amount').val();
                        var credit = $(tr[i]).children('td:nth-child(3)').find('.credit-Amount').val();
                        var Head = $(tr[i]).children('td').children('.account-heads').select2('data').id;
                        var ddlCostCenter = $(tr[i]).children('td').children('.CostCenter').select2('data').id; //Cost Center ddlvalue
                        if (debit == "" && credit == "" && Head != "0" || isNaN(debit)||isNaN(credit)) {
                            errorAlert('Enter Valid Amount');
                            flag = 1;
                            return false;
                        }
                        else if ($(tr[i]).children('td').children('.account-heads').select2('data').id == 0 && (debit != "" || credit != "")) {
                            errorAlert('Head is Not Selected');
                            flag = 1;
                            return false;
                        }
                        else if ($(tr[i]).children('td').children('.account-heads').select2('data').id != 0 && (debit != "" || credit != "")) {
                            tempData = $(tr[i]).children('td').children('.account-heads').select2('data').id + "|";
                            var Job = $(tr[i]).children('td').children('.Job').select2('data').id;
                            var Desc = $(tr[i]).children('td').children('.entry-desc').val();
                            if (EntryDesc == "") {
                                EntryDesc = Desc;
                            }
                            else {
                                EntryDesc += '|' + Desc;
                            }
                            if ($(tr[i]).children('td').children('.account-heads').select2('data').id == undefined) {
                                tempData = $(tr[i]).children('td').children('.account-heads-Credit').select2('data').id + "|";
                            }
                            var headstring = tempData.split("|");
                            if (Heads == "") {
                                Heads = headstring[0];
                            }
                            else {
                                Heads += "|" + headstring[0];
                            }
                            if (child == "") {
                                child = headstring[1];
                            }
                            else {
                                child += "|" + headstring[1];
                            }
                            if (JobString == "") {
                                JobString += Job;
                            }
                            else {
                                JobString += "|" + Job;
                            }
                            if (debit != "") {
                                creditorDebitTemp = "1";  //Debit then creditOrDebit is One
                                //Appends Debit Amount if Debit
                                if (amount == "") {
                                    amount = debit;
                                }
                                else {
                                    amount += "|" + debit;
                                }

                                if (costcenter == "") {
                                    if (ddlCostCenter == "0") {
                                        costcenter = "1`" + debit;
                                    }
                                    else {
                                        costcenter += ddlCostCenter + "`" + debit;
                                    }
                                }
                                else {
                                    if (ddlCostCenter == "0") {
                                        costcenter += "|1`" + debit;
                                    }
                                    else {
                                        costcenter += "|" + ddlCostCenter + "`" + debit;
                                    }
                                }
                                //if (costcenter == "") {
                                //    costcenter = ddlCostCenter + "`" + debit;
                                //}
                                //else {
                                //    costcenter += "|" + ddlCostCenter + "`" + debit;
                                //}
                            }
                            else if (credit != "") {
                                creditorDebitTemp = "0"; //Credit Then CreditorDebit is zero
                                //Appends Credit Amount if Credit
                                if (amount == "") {
                                    amount = credit;
                                }
                                else {
                                    amount += "|" + credit;
                                }
                                if (costcenter == "") {
                                    if (ddlCostCenter == "0") {
                                        costcenter = "1`" + credit;
                                    }
                                    else {
                                        costcenter += ddlCostCenter + "`" + credit;
                                    }
                                }
                                else {
                                    if (ddlCostCenter == "0") {
                                        costcenter += "|1`" + credit;
                                    }
                                    else {
                                        costcenter += "|" + ddlCostCenter + "`" + credit;
                                    }
                                }
                            }
                            //Appends Credit or Debit
                            //console.log(creditorDebitTemp);
                            if (creditorDebit == "") {
                                creditorDebit = creditorDebitTemp;
                            }
                            else {
                                creditorDebit += "|" + creditorDebitTemp;
                            }
                        }
                    }
                    if (debitTotal != "0" && creditTotal != "0" && flag != 1) {
                        voucher.AccountHead = Heads;
                        voucher.AccountChild = child;
                        voucher.Amount = amount;
                        voucher.VoucherType = creditorDebit;
                        voucher.Description = $('.Narration').val();
                        voucher.IsVoucher = 1;
                        if ($('#chkIsCheque').prop('checked')) {
                            voucher.IsCheque = 1;
                            voucher.ChequeNo = $('#txtChequeNumber').val();
                            voucher.ChequeDate = $('#txtChequeDate').val();
                            voucher.Drawon = $('#txtDrawOn').val();
                        }
                        else {
                            voucher.IsCheque = 0;
                            voucher.ChequeNo = null;
                            voucher.ChequeDate = null;
                            voucher.Drawon = null;
                        }
                        voucher.VoucherTypeID = $('#hdnVoucherType').val();
                        voucher.ReceiptNo = null;
                        voucher.CostCenter = costcenter;
                        voucher.username = $.cookie('bsl_3');
                        voucher.ID = $('#hdnGroupID').val();//groupID for updation in Entities
                        voucher.groupID = $('#hdnGroupID').val();//groupID for updation in Controllers
                        voucher.VoucherNo = $('#hdnVoucherNumber').val();
                        voucher.CreatedBy = $.cookie('bsl_3');
                        voucher.ModifiedBy = $.cookie('bsl_3');
                        voucher.Date = $('#txtVoucherDate').val();
                        voucher.Jobs = JobString;
                        voucher.EntryDesc = EntryDesc;
                        alert(voucher.EntryDesc);
                        console.log(voucher);
                        $.ajax({
                            url: apiurl + 'api/VoucherEntry/Save',
                            method: 'POST',
                            datatype: 'JSON',
                            data: JSON.stringify(voucher),
                            contentType: 'application/json;charset=utf-8',
                            success: function (response) {
                                console.log(response);
                                if (response.Success) {
                                    successAlert(response.Message);
                                    reset();
                                    GetVoucherNo();
                                    $('#hdnGroupID').val("0");
                                    $('#hdnVoucherNumber').val("0");
                                    $('.entry-desc').val("");
                                    if (print) {
                                        var url = "/Finance/Print/Voucher?id=" + response.Object;
                                        PopupCenter(url, 'VoucherPrint', 800, 700);
                                    }
                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (xhr) {
                                errorAlert("Something went wrong.Please try Again Later");
                            },
                            beforeSend: function () { miniLoading('start'); $('#btnSave').attr('disabled', 'disabled'); $('#btnSavePrint').attr('disabled', 'disabled'); },
                            complete: function () { miniLoading('stop'); $('#btnSave').removeAttr('disabled'); $('#btnSavePrint').removeAttr('disabled'); }
                        });
                    }
                    else {
                        errorAlert('Enter a valid information');
                    }
                }
                else {
                  errorAlert('Enter valid information');
                }
                

            }


            //Loads the Account heads according to the voucher Types selected
            function LoadHeads(voucher) {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/VoucherEntry/GetHeads?CompanyId=' + Company + '&Credit=0',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(voucher),
                    success: function (response) {
                        $('.account-heads').children('option').remove();
                        $('.account-heads').select2('destroy');
                        $('.account-heads').append('<option value=0>--Select--</option>');
                        for (var i = 0; i < response.length; i++) {
                            $('.account-heads').append('<option value=' + response[i].parent + '|' + response[i].ID + '>' + response[i].Name + '</option>');
                        }
                        $('.account-heads').select2();
                        $.ajax({
                            url: apiurl + '/api/VoucherEntry/GetHeads?CompanyId=' + Company + '&Credit=1',
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify(voucher),
                            success: function (response) {
                                $('.account-heads-Credit').children('option').remove();
                                $('.account-heads-Credit').select2('destroy');
                                $('.account-heads-Credit').append('<option value=0>--Select--</option>');
                                for (var i = 0; i < response.length; i++) {
                                    $('.account-heads-Credit').append('<option value=' + response[i].parent + '|' + response[i].ID + '>' + response[i].Name + '</option>');
                                }
                                $('.account-heads-Credit').select2();
                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                            }
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            //Loads CostCenter for dropdown list
            function LoadCostCenter() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/VoucherEntry/GeCostCenter',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(Company),
                    success: function (response) {
                        //console.log(response);
                        $('.CostCenter').append('<option value="0">--Select--</option>');
                        for (var i = 0; i < response.length; i++) {
                            $('.CostCenter').append('<option value=' + response[i].ID + '>' + response[i].name + '</option>');
                        }
                        $('.CostCenter').select2();
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Loads CostCenter for dropdown list
            function LoadJobs() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/Jobs/GetJobForVoucher',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(Company),
                    success: function (response) {
                        //console.log(response);
                        $('.Job').empty();
                        $('.Job').append('<option value="0">--Select--</option>');
                        for (var i = 0; i < response.length; i++) {
                            $('.Job').append('<option value=' + response[i].Job_Id + '>' + response[i].Job_Name + '</option>');
                        }
                        $('.Job').select2();
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }
            //following two event handlers are used to target the credit and debit amount any one of the exet box are allowed for one entry
            $('.credit-Amount').on('keyup', function () {
                var crAmount = $(this).closest('td').next('td').find('input').val();
                if (crAmount != "") {
                    $(this).closest('td').next('td').find('input').val("");
                    //console.log('Credit Amount key up');
                }
                calculateDebit();
                calculateCredit();
            });

            $('.debit-Amount').on('keyup', function () {
                var crAmount = $(this).closest('td').prev('td').find('input').val();
                if (crAmount != "") {
                    if (crAmount != "") {
                        var attr = $(this).closest('td').prev('td').find('input').attr('readonly');
                        //console.log(attr);
                        // attribute exists
                        //Added to reset the text box when focus to debit amount textbox(Only reset when it is not read only)
                        if (typeof attr !== undefined && attr !== false) {
                            //console.log('Debit Amount key up if');
                        }
                        else {
                            $(this).closest('td').prev('td').find('input').val("");
                        }
                    }
                }
                calculateDebit();
                //Added to Maintain the debit and credit amount equal
                var tr = $('#VoucherTable>tbody').children('tr');
                $(tr[0]).children('td:nth-child(3)').find('.credit-Amount').val($('.debit-total').text());
                $('.credit-total').text($('.debit-total').text());
            });

            //Function to Calculate Debit Amount
            function calculateDebit() {
                var tr = $('#VoucherTable>tbody').children('tr');
                var debit = 0;
                for (var i = 0; i < tr.length; i++) {
                    var amount = $(tr[i]).children('td:nth-child(4)').find('.debit-Amount').val();
                    if (amount == "") {
                        amount = 0;
                    }
                    debit += parseFloat(amount);
                }
                $('.debit-total').text(debit);
            }

            //Function to Calculate Credit Amount
            function calculateCredit() {
                var tr = $('#VoucherTable>tbody').children('tr');
                var credit = 0;
                for (var i = 0; i < tr.length; i++) {
                    var amount = $(tr[i]).children('td:nth-child(3)').find('.credit-Amount').val();
                    if (amount == "") {
                        amount = 0;
                    }
                    credit += parseFloat(amount);
                }
                $('.credit-total').text(credit);
            }

            //Datepicker Functions
            $('#txtVoucherDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });

            $('#txtChequeDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });

            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtVoucherDate').datepicker('setDate', today);
            $('#txtVoucherDate').datepicker()
                      .on('changeDate', function (ev) {
                          $('#txtVoucherDate').datepicker('hide');
                      });
            $('#txtChequeDate').datepicker()
                      .on('changeDate', function (ev) {
                          $('#txtChequeDate').datepicker('hide');
                      });
            //Loads the voucher Types for dropdown list
            function LoadVoucherTypes() {
                var Company = $.cookie("bsl_1");
                $.ajax({
                    url: apiurl + '/api/VoucherSettings/GetVoucherTypes',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('.VoucherType').append('<option value="0">--Select--</option>');
                        $(response).each(function () {
                            $('.VoucherType').append('<option value=' + response[i].ID + '>' + response[i].Name + '</option>');
                        });
                        $("#ddlChangeType").select2({
                            width: '100%'
                        });
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Function is used to get the voucher number
            function GetVoucherNo() {
                var voucher = $('#hdnVoucherType').val();
                $.ajax({
                    url: apiurl + '/api/VoucherEntry/GeVoucherNumber?voucher=' + voucher,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var VoucherType = response.Table1[0].Fvt_TypeName;
                        var Number = response.Table[0].Column1;
                        $('#VoucherNumber').html(VoucherType + ':' + Number);
                        $('#hdnVoucherTypeName').val(VoucherType);
                        $('#lblTitle').text(VoucherType);
                        var vouchertypeid = $('#hdnVoucherType').val();
                        LoadHeads(vouchertypeid);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Reset Function

            function reset() {
                var Voucher = $('#hdnVoucherType').val();
                $('.debit-Amount').val("");
                $('.credit-Amount').val("");
                $('.debit-total').text("0");
                $('.credit-total').text("0");
                $('.account-heads').select2('data', { id: "0", text: '--Select--' });
                $('.account-heads-Credit').select2('data', { id: "0", text: '--Select--' });
                $('.CostCenter').select2('data', { id: "0", text: '--Select--' });
                $('.Job').select2('data', { id: "0", text: '--Select--' });
                //$('.CostCenter').select2("val", "1");
                $('#txtVoucherDate').datepicker('setDate', today);
                $('#txtChequeNumber').val("");
                $('#txtChequeDate').val("");
                $('#txtDrawOn').val("");
                $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Save');
                $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Save & Print');
                var tr = $('#VoucherTable>tbody').children('tr');
                for (var i = tr.length; i > 5; i--) {
                    $(tr[i - 1]).remove();
                }
                $('#chkIsCheque').prop('checked', false);
                $('#dvCheque').fadeOut('slow');
                $('#dvCheque').addClass('hidden');
                $('.Narration').val("");
            }

            function getmonth(month) {
                if (month == 0) {
                    month = 12;
                }
                switch (month) {
                    case 1:
                        return 'Jan';
                    case 2:
                        return 'Feb';
                    case 3:
                        return 'Mar';
                    case 4:
                        return 'Apr';
                    case 5:
                        return 'May';
                    case 6:
                        return 'June';
                    case 7:
                        return 'July';
                    case 8:
                        return 'Aug';
                    case 9:
                        return 'Sep';
                    case 10:
                        return 'Oct';
                    case 11:
                        return 'Nov';
                    case 12:
                        return 'Dec';
                }
            }

            //Find Button Click event handler
            $('#btnFind').click(function () {
                $('#findModal').modal('show');
                refreshTable();
                function refreshTable() {
                    reset();
                    var html = '';
                    var fin = {};
                    var date = new Date();
                    var toDate = date.getMonth();
                    var FromdateString = date.getDate() + "/" + getmonth(toDate) + "/" + (date.getFullYear() - 1);
                    fin.filterType = 0;
                    fin.fromdatestring = FromdateString;
                    fin.todatestring = date.getDate() + "/" + getmonth(toDate) + "/" + (date.getFullYear() + 1);
                    fin.VoucherType = $('#hdnVoucherType').val();
                    fin.CompanyId = $.cookie('bsl_1');
                    fin.FromAccount = 0;
                    //console.log(fin);
                    $.ajax({
                        url: apiurl + '/api/VoucherEntry/GetVoucherData',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'Json',
                        data: JSON.stringify(fin),
                        success: function (response) {
                            console.log(response);
                            $(response).each(function (index) {
                                html += '<tr>';
                                html += '<td style="display:none">' + this.TrnID + '</td>';
                                html += '<td>' + this.TrnDesc + '</td>';
                                html += '<td>' + this.TrnVchType + '</td>';
                                html += '<td>' + this.TrnVchNo + '</td>';
                                html += '<td>' + this.TrnAmount + '</td>';
                                html += '<td>' + this.TrnChequeNo + '</td>';
                                html += '<td>' + this.TrnChequeDate + '</td>';
                                html += '<td style="display:none">' + this.TrnGroupID + '</td>';
                                html += '<td><a class="edit-register" title="edit" href="#"><i class="fa fa-edit"></i></a></td>'
                                html += '</tr>';
                            });
                            $('#tblRegister').DataTable().destroy();
                            $('#tblRegister tbody').children().remove();
                            $('#tblRegister tbody').append(html);
                            $('#tblRegister').dataTable({ destroy: true, aaSorting: [] });
                            //binding event to row
                            $('#tblRegister').off().on('click', '.edit-register', function () {
                                var GroupID = $(this).closest('tr').children('td').eq(7).text();
                                $.ajax({
                                    url: apiurl + '/api/VoucherEntry/GetVoucherDataforEdit',
                                    method: 'POST',
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'Json',
                                    data: JSON.stringify(GroupID),
                                    success: function (response) {
                                        console.log(response);
                                        var tr = $('#VoucherTable>tbody').children('tr');
                                        if (response.Table[0].Fve_IsCheque == true) {
                                            var date = new Date(response.Table[0].Fve_ChequeDate);
                                            var dateString = date.getDate() + '/' + getmonth(date.getMonth() + 1) + '/' + date.getFullYear();
                                            $('#chkIsCheque').prop('checked', true);
                                            $('#dvCheque').fadeIn('slow');
                                            $('#dvCheque').removeClass('hidden');
                                            $('#txtChequeNumber').val(response.Table[0].Fve_ChequeNo);
                                            $('#txtChequeDate').val(dateString);
                                            $('#txtDrawOn').val(response.Table[0].Fve_Drawon);
                                        }
                                        else {
                                            $('#chkIsCheque').prop('checked', false);
                                            $('#dvCheque').fadeOut('slow');
                                            $('#dvCheque').addClass('hidden');
                                            $('#txtChequeNumber').val("");
                                            $('#txtChequeDate').val("");
                                            $('#txtDrawOn').val("");
                                        }
                                        for (var i = 0; i < response.Table.length; i++) {
                                            var CostCenter = response.Table[i].CostCenter.split("`");;
                                            var NewCost = CostCenter[0];
                                            var selected = "";
                                            $(tr[i]).find('.entry-desc').val(response.Table[i].Fve_ExpenseDesc);
                                            if (i >= parseInt($('#VoucherTable>tbody').children('tr').length)) {
                                                //$('#addRow').trigger('click');
                                                //Adds a new row if the edit data has more than 5 rows
                                                //Removes the select2 property of dummy table row 
                                                $('#dummy .dummyRow .account-heads').select2('destroy');
                                                $('#dummy .dummyRow .CostCenter').select2('destroy');
                                                $('#dummy .dummyRow .Job').select2('destroy');
                                                //Clones the dummyRow with normal select
                                                $('#dummy .dummyRow').clone(true).appendTo('.journal-table > tbody');
                                                //Add select2 property to the cloned Select
                                                $('#VoucherTable').find('.dummyRow .account-heads').select2();
                                                $('#VoucherTable').find('.dummyRow .CostCenter').select2();
                                                $('#VoucherTable').find('.dummyRow .Job').select2();
                                                //Removes the dummyrow class from the cloned row
                                                $('#VoucherTable').find('.dummyRow').removeClass('dummyRow');
                                                //Functionality to get serial number for the added row
                                                var tr = $('#VoucherTable>tbody').children('tr');
                                                $('.slno').text(parseInt(tr.length));
                                                $('#VoucherTable').find('.slno').removeClass('slno');
                                                selected = response.Table[i].ParticularsID + "|" + response.Table[i].CostHead;
                                                if (response.Table[i].CreditOrDebit == 0) {
                                                    $(tr[i]).find('.account-heads-Credit').select2('val', selected).trigger("change");
                                                }
                                                else {
                                                    //$(tr[i]).find('.account-heads').select2('val', selected);
                                                    $(tr[i]).find('.account-heads').val(selected).trigger("change");
                                                    console.log(selected);
                                                }
                                                $(tr[i]).find('.CostCenter').select2("val", NewCost);
                                                $(tr[i]).find('.Job').select2("val", response.Table[i].Job_ID);
                                                $(tr[i]).find('.entry-desc').val(response.Table[i].Fve_ExpenseDesc);
                                                //console.log(response.Table[i].DebitAmt == 0);
                                                if (response.Table[i].DebitAmt == 0) {
                                                    $(tr[i]).find('.debit-Amount').val("");
                                                }
                                                else {
                                                    $(tr[i]).find('.debit-Amount').val(response.Table[i].DebitAmt);
                                                }
                                                if (response.Table[i].creditAmt == 0) {
                                                    $(tr[i]).find('.credit-Amount').val("");
                                                }
                                                else {
                                                    $(tr[i]).find('.credit-Amount').val(response.Table[i].creditAmt);
                                                }


                                                calculateDebit();
                                                calculateCredit();
                                            }
                                            else {
                                                selected = response.Table[i].ParticularsID + "|" + response.Table[i].CostHead;
                                                if (response.Table[i].CreditOrDebit == 0) {
                                                    $(tr[i]).find('.account-heads-Credit').select2('val', selected);
                                                }
                                                else {
                                                    $(tr[i]).find('.account-heads').select2('val', selected);
                                                }
                                                $(tr[i]).find('.CostCenter').select2("val", NewCost);
                                                $(tr[i]).find('.Job').select2("val", response.Table[i].Job_ID);
                                                if (response.Table[i].DebitAmt == 0) {
                                                    $(tr[i]).find('.debit-Amount').val("");
                                                }
                                                else {
                                                    $(tr[i]).find('.debit-Amount').val(response.Table[i].DebitAmt);
                                                }
                                                if (response.Table[i].creditAmt == 0) {
                                                    $(tr[i]).find('.credit-Amount').val("");
                                                }
                                                else {
                                                    $(tr[i]).find('.credit-Amount').val(response.Table[i].creditAmt);
                                                }

                                                calculateDebit();
                                                calculateCredit();
                                            }
                                            $('#hdnVoucherNumber').val(response.Table[0].Fve_Number);
                                            $('#hdnGroupID').val(response.Table[0].Fve_GroupID);
                                            $('#VoucherNumber').text(response.Table[0].Voucher);
                                            //$('#txtVoucherDate').val(response.Table[0].Fve_Date);
                                            $('#txtVoucherDate').datepicker("update", new Date(response.Table[0].Fve_Date));
                                            $('#btnSave').html('<i class="ion-archive"></i>&nbsp;Update');
                                            $('#btnSavePrint').html('<i class="ion ion-printer"></i>&nbsp;Update & Print');
                                            $('.Narration').val(response.Table[0].Fve_Description);
                                        }

                                    },
                                    error: function (xhr) {
                                        alert(xhr.responseText);
                                        console.log(xhr);
                                    }
                                });
                                $('#findModal').modal('hide');
                                //binding delete
                                $('.delete-row').click(function () {
                                    $(this).closest('tr').hide('slow', function () { $(this).closest('tr').remove(); });
                                });
                            });
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });

                }
            });


            //Print Functionality Requires GroupID of the voucher.
            $('#btnPrint').click(function () {
                var id = $('#hdnGroupID').val();
                if (id == "0") {
                    errorAlert('Select a voucher to print');
                }
                else {
                    var url = "/finance/Print/Voucher?id=" + id;
                    PopupCenter(url, 'VoucherPrint', 800, 700);
                }
            });

            //Added for generate new row when focus comes to last row
            var tr = $('#VoucherTable>tbody').children('tr');
            $('body').on('blur', '.credit-Amount', function () {
                if ($(this).parent().parent().next('tr').html() == undefined) {
                    $('#addRow').trigger('click');
                }

            });

            //Added for generate new row when focus comes to last row
            $('body').on('blur', '.debit-Amount', function () {
                if ($(this).parent().parent().next('tr').html() == undefined) {
                    $('#addRow').trigger('click');
                }
            });

            $('#btnNew').click(function () {
                reset();
                GetVoucherNo();
            });

        });//Document Ready function end
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
</asp:Content>
