<%@ Page Title="Reconcile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reconcile.aspx.cs" Inherits="BisellsERP.Finance.Reconcile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Date Range Picker CSS -->
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />
    <title>Reconcile</title>
    <style>
        .tran-head [name="daterange"], .tran-head select {
            display: inline-block;
        }

        .tran-head [name="daterange"] {
            width: 65%;
        }

        .tran-head select {
            width: 30%;
        }

        .reconcile-body .btn-group.dropdown {
            width: 100%;
            height: 100%;
        }

            .reconcile-body .btn-group.dropdown > button {
                /*margin-top: 8px;*/
                padding: 1px 5px;
                background-color: transparent;
                border: none;
                box-shadow: none;
            }

        .reconcile-body .open > .dropdown-menu {
            top: 134%;
            left: -8px;
            min-width: calc(100% + 16px);
            padding: 0;
            border-radius: 0 0 3px 3px;
        }

        .reconcile-body table > thead > tr > th {
            color: #5b9ebf;
            font-weight: 100;
            font-size: 14px;
            background-color: #E3F2FD;
            padding: 4px 8px;
        }

        .reconcile-body .label {
            display: block;
            margin-top: 3px;
        }

        .reconcile-body .cheque-action {
            position: relative;
            background-color: #fff;
            margin-top: -20px;
        }

            .reconcile-body .cheque-action textarea {
                resize: vertical;
            }

            .reconcile-body .cheque-action > .btn {
                padding: 1px 5px;
                margin-top: 4px;
            }

            .reconcile-body .cheque-action > .btn-save {
                width: 85%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <div class="panel panel-default">
        <div class="panel-heading tran-head">
            <div class="row">
                <div class="col-sm-9">
                    <h3 class="panel-title">All Transactions</h3>
                </div>
                <div class="col-sm-3 text-right">
                    <input type="text" name="daterange" value="01/01/2015 - 01/31/2015" class="form-control" />
                    <select class="form-control filtertype">
                        <option value="0">All</option>
                        <option value="1">Reconciled</option>
                        <option value="2">Bounced</option>
                        <option value="3">Pending</option>
                    </select>
                </div>
            </div>

        </div>
        <div class="panel-body reconcile-body">
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered" id="tblChequeData">
                        <thead>
                            <tr>
                                <th style="width: 100px">Cheque Date</th>
                                <th>Account</th>
                                <th>Bank</th>
                                <th style="width: 100px">Drawn On</th>
                                <th style="width: 150px">Cheque No</th>
                                <th style="width: 120px" class="text-right">Amount</th>
                                <th style="width: 100px" class="text-center">Status</th>
                                <th class="hidden">GroupID</th>
                                <th class="hidden">FromID</th>
                                <th class="hidden">ToID</th>
                                <th class="hidden">VoucherType</th>
                                <th class="hidden">CostCenter</th>
                                <th class="hidden">VoucherTypeID</th>
                                <th style="width: 150px">Action</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {

            //Daterangepicker Init
            $('input[name="daterange"]').daterangepicker({
                "opens": "left",
                "startDate": moment().subtract(6, 'days'),
                "endDate": moment(),
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

            $('input[name="daterange"]').on('apply.daterangepicker', function (ev, picker) {
                $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
                var startdate = picker.startDate.format('MM/DD/YYYY');
                var enddate = picker.endDate.format('MM/DD/YYYY');
                LoadTableContent(startdate, enddate);
            });

            LoadTableContent(null, null);

            $('.filtertype').change(function () {
                var selected = $('.filtertype').val();
                var table = $('#tblChequeData >tbody');
                if (selected == 0) {
                    table.children('tr').removeClass('hidden');
                }
                else if (selected == 1) {
                    table.children('tr').find('.bounced').parent('tr').addClass('hidden');
                    table.children('tr').find('.pending').parent('tr').addClass('hidden');
                    table.children('tr').find('.reconcile').parent('tr').removeClass('hidden');
                }
                else if (selected == 2) {
                    table.children('tr').find('.reconcile').parent('tr').addClass('hidden');
                    table.children('tr').find('.pending').parent('tr').addClass('hidden');
                    table.children('tr').find('.bounced').parent('tr').removeClass('hidden');
                }
                else if (selected == 3) {
                    table.children('tr').find('.bounced').parent('tr').addClass('hidden');
                    table.children('tr').find('.reconcile').parent('tr').addClass('hidden');
                    table.children('tr').find('.pending').parent('tr').removeClass('hidden');
                }
            });

            //Loads all cheque transactions.Fromdate and to date can be selected from the daterange picker.
            function LoadTableContent(fromdate, todate) {
                var fin = {};
                var date = new Date();
                var toDate = date.getMonth();
                fin.filter = $('.filtertype').val();
                if (fromdate == null && todate == null) {
                    var FromdateString = date.getDate() + "/" + getmonth(date.getMonth()) + "/" + (date.getFullYear() - 1);
                    fin.filterType = 0;
                    fin.fromdatestring = FromdateString;
                    fin.todatestring = date.getDate() + "/" + getmonth(date.getMonth()) + "/" + (date.getFullYear() + 1);
                }
                else {
                    var FromdateString = fromdate;
                    fin.filterType = 0;
                    fin.fromdatestring = FromdateString;
                    fin.todatestring = todate;
                }
                fin.CompanyId = $.cookie('bsl_1');
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/FinancialTransactions/GetChequeDetails',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(fin),
                    success: function (response) {
                        console.log(response);
                        var html = '';
                        $('#tblChequeData >tbody').empty();
                        $(response).each(function (index) {
                            html += '<tr>';
                            var date = new Date(this.TrnChequeDate);
                            var dateString = date.getDate() + '/' + getmonth(date.getMonth() + 1) + '/' + date.getFullYear();
                            html += '<td>' + dateString + '</td>';
                            if (this.TrmTChild != null) {
                                html += '<td>' + this.TrmTChild + '</td>';
                            }
                            else if (this.TrmFChild != null) {
                                html += '<td>' + this.TrmFChild + '</td>';
                            }
                            else {
                                html += '<td>' + this.Account + '</td>';
                            }
                            html += '<td>' + this.TrnDesc + '</td>';
                            if (this.TrnDrawon==null) {
                                html += '<td></td>';
                            }
                            else {
                                html += '<td>' + this.TrnDrawon + '</td>';
                            }
                            html += '<td class="cheque-number">' + this.TrnChequeNo + '</td>';
                            html += '<td class="text-right amount">' + this.TrnAmount + '</td>';
                            if (this.TrnFve_IsCleared == 1) {
                                html += '<td class="status reconcile"><span class="label label-success">Reconciled</span></td>'
                            }
                            else if (this.TrnFve_isBounce == 1) {
                                html += '<td class="status bounced"><span class="label label-danger status">Bounced</span></td>'
                            }
                            else {
                                html += '<td class="status pending"><span class="label label-warning status">Pending</span></td>';
                            }
                            html += '<td class="hidden group-id">' + this.TrnGroupID + '</td>';
                            html += '<td class="hidden Head-ID">' + this.TrnToID + '|' + this.TrnFrmID + ' </td>';
                            html += '<td class="hidden Child-ID">' + this.TrnToCldID + '|' + this.TrnFrmCldID + '</td>';
                            html += '<td class="hidden voucher-type">' + this.TrnVchID + '|' + this.TrnVchID + '</td>';
                            html += '<td class="hidden costcenter">1`' + this.TrnAmount + '|1`' + this.TrnAmount + '</td>';
                            html += '<td class="hidden vouchertypeId">' + this.TrnVchID + '</td>';
                            if (this.TrnFve_IsCleared != 1 && this.TrnFve_isBounce != 1) {
                                html += '<td><div class="btn-group dropdown"><button type="button" class="btn dropdown-toggle btn-xs btn-block" data-toggle="dropdown" aria-expanded="false">Action <span class="caret"></span></button><ul class="dropdown-menu" role="menu"><li><a href="#" class="btn-reconcile">Reconcile</a></li><li><a href="#" class="btn-bounced">Bounce</a></li></ul></div></td>';
                            }
                            else {
                                html += '<td></td>';
                            }
                            html += '<td class="voucher-number hidden">' + this.TrnVchType + ':' + this.TrnVchNo + '</td>'
                            html += '<td class="from-head hidden">' + this.TrnFrmID + '</td>'
                            html += '<td class="from-child hidden">' + this.TrnFrmCldID + '</td>'
                            html += '<td class="to-head hidden">' + this.TrnToID + '</td>'
                            html += '<td class="to-child hidden">' + this.TrnToCldID + '</td>'
                            html += '</tr>';
                        });
                        $('#tblChequeData >tbody').append(html);
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
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
            // Add Check Reconciled Box
            $('body').on('click', '.btn-reconcile', function () {
                $('.cheque-action').remove();
                chequeHtml = '<div class="cheque-action"><input type="hidden" class="save-type" value="2" /><textarea rows="3" placeholder="Note..." class="form-control"></textarea><input type="text" class="form-control clear-date"/><button type="button" class="btn btn-success btn-xs btn-save">Save</button><button type="button" class="btn btn-xs btn-inverse pull-right btn-cancel">x</button></div>';
                tdEml = $(this).closest('td');
                tdEml.append(chequeHtml);
                $('.clear-date').datepicker(
                    {
                        autoClose: true,
                        format: 'dd/M/yyyy',
                        todayHighlight: true
                    });
                $('.clear-date').datepicker()
                                      .on('changeDate', function (ev) {
                                          $('.clear-date').datepicker('hide');
                                      });

            });

            // Add Check Bounced Box
            $('body').on('click', '.btn-bounced', function () {
                $('.cheque-action').remove();
                chequeHtml = '<div class="cheque-action"><textarea rows="3" placeholder="Note..." class="form-control narration"></textarea><input type="text" placeholder="Bounced Day"  class="form-control bounce-day" /><input type="number" placeholder="Penalty ₹"  class="form-control penalty" /><input type="hidden" class="save-type" value="1" /><button type="button" class="btn btn-success btn-xs btn-save">Save</button><button type="button" class="btn btn-xs btn-inverse pull-right btn-cancel">x</button></div>';
                tdEml = $(this).closest('td');
                tdEml.append(chequeHtml);
                $('.bounce-day').datepicker(
                   {
                       autoClose: true,
                       format: 'dd/M/yyyy',
                       todayHighlight: true
                   });
                $('.bounce-day').datepicker()
                                      .on('changeDate', function (ev) {
                                          $('.bounce-day').datepicker('hide');
                                      });
            });

            // Close Cheque Action Box
            $(document).on('click', '.btn-cancel', function () {
                $('.cheque-action').remove();
            });

            $(document).on('click', '.btn-save', function () {
                //Cheque clearance process
                if ($(this).parent('div').find('.save-type').val() == 2) {
                    if ($('.clear-date').val() != '') {
                        var fin = {};
                        fin.ChequeClearDate = $('.clear-date').val();
                        fin.GroupID = $(this).closest('tr').children('.group-id').text();
                        console.log(fin);
                        var tr = $(this).closest('tr').find('.status');
                        swal({
                            title: "Save?",
                            text: "Are you sure to reconcile the cheque?",

                            showConfirmButton: true, closeOnConfirm: true,
                            showCancelButton: true,

                            cancelButtonText: "Cancel",
                            confirmButtonClass: "btn-danger",
                            confirmButtonText: "Save"
                        },
                        function (isConfirm) {
                            if (isConfirm) {
                                $.ajax({
                                    url: $('#hdApiUrl').val() + 'api/FinancialTransactions/ClearCheque',
                                    method: 'POST',
                                    data: JSON.stringify(fin),
                                    contentType: 'application/json',
                                    dataType: 'JSON',
                                    success: function (response) {
                                        if (response.Success) {
                                            tr.html('<span class="label label-success status">Reconciled</span>');
                                            successAlert(response.Message);
                                            $('.cheque-action').remove();
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
                                    beforeSend: function () { miniLoading('start'); },
                                    complete: function () { miniLoading('stop'); }
                                });
                            }
                        });
                    }
                    else {
                        errorAlert('Please enter a valid date');
                    }
                }
                else {
                    //Cheque bounce process.Set the current cheque as bounced and creates a new voucher entry
                    if ($('.bounce-day').val() != '' || $('.narration').val() != '') {
                        var fin = {};
                        fin.GroupID = $(this).closest('tr').children('.group-id').text();
                        var voucher = {};
                        var BounceHeadFrom = $(this).closest('tr').children('.from-head').html();
                        var BounceChildFrom = $(this).closest('tr').children('.from-child').html();
                        var BounceHeadTo = $(this).closest('tr').children('.to-head').html();
                        var BounceChildTo = $(this).closest('tr').children('.to-child').html();
                        var headID = BounceHeadFrom + '|' + BounceHeadTo;//$(this).closest('tr').children('.Head-ID').html();
                        var ChildID = BounceChildFrom + '|' + BounceChildTo;//$(this).closest('tr').children('.Child-ID').html();
                        var vouchertype = $(this).closest('tr').children('.voucher-type').html();
                        var costcenter = $(this).closest('tr').children('.costcenter').html();
                        var TypeID = $(this).closest('tr').children('.vouchertypeId').html();
                        var desc = 'bounced cheque no[' + $(this).closest('tr').children('.cheque-number').html() + '] ' + $(this).closest('tr').children('.voucher-number').html();
                        vouchertype = "1|0";
                        var amount = $(this).closest('tr').children('.amount').html() + '|' + $(this).closest('tr').children('.amount').html();
                        if ($('.penalty').val() != '') {
                            //Additionally added for bank charge
                            headID += '|' + BounceHeadFrom + '|16|' + BounceHeadTo;
                            ChildID += '|' + BounceChildFrom + '|0|' + BounceChildTo;
                            amount += '|' + $('.penalty').val() + '|' + $('.penalty').val() + '|' + $('.penalty').val();
                            costcenter += '|1`' + $('.penalty').val() + '|1`' + $('.penalty').val() + '|1`' + $('.penalty').val();
                            vouchertype += '|1|0|0';
                        }
                        voucher.VoucherType = vouchertype;
                        voucher.Date = $('.bounce-day').val();
                        voucher.username = $.cookie('bsl_3');
                        voucher.VoucherTypeID = TypeID;
                        voucher.AccountHead = headID;
                        voucher.AccountChild = ChildID;
                        voucher.Amount = amount;
                        voucher.CostCenter = costcenter;
                        voucher.Jobs = '';
                        voucher.CreatedBy = $.cookie('bsl_3');
                        voucher.Description = $('.narration').val();
                        console.log(voucher);
                        var tr = $(this).closest('tr').find('.status');
                        swal({
                            title: "Save?",
                            text: "Are you sure to bounce this cheque?",
                            showConfirmButton: true, closeOnConfirm: true,
                            showCancelButton: true,
                            cancelButtonText: "Cancel",
                            confirmButtonClass: "btn-danger",
                            confirmButtonText: "Save"
                        },
                        function (isConfirm) {
                            if (isConfirm) {
                                $.ajax({
                                    url: $('#hdApiUrl').val() + 'api/FinancialTransactions/BounceCheque',
                                    method: 'POST',
                                    data: JSON.stringify(fin),
                                    contentType: 'application/json',
                                    dataType: 'JSON',
                                    success: function (response) {
                                        if (response.Success) {
                                            $.ajax({
                                                url: $('#hdApiUrl').val() + 'api/VoucherEntry/Save',
                                                method: 'POST',
                                                datatype: 'JSON',
                                                data: JSON.stringify(voucher),
                                                contentType: 'application/json;charset=utf-8',
                                                success: function (response) {
                                                    console.log(response);
                                                    if (response.Success) {
                                                        tr.html('<span class="label label-danger status">Bounced</span>');
                                                        successAlert('The cheque has bounced');
                                                        $('.cheque-action').remove();
                                                    }
                                                    else {
                                                        errorAlert(response.Message);
                                                    }
                                                },
                                                error: function (xhr) {
                                                    errorAlert("Something went wrong.Please try Again Later");
                                                }
                                            });
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
                                    beforeSend: function () { miniLoading('start'); },
                                    complete: function () { miniLoading('stop'); }
                                });
                            }
                                                });
                    }
                    else {
                        errorAlert('Enter a valid Date');
                    }

                    //alert(amount);
                }
            });
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <!-- Date Range Picker JS -->
  
  <script src="../Theme/assets/moment/moment.min.js"></script>
  <script src="../Theme/assets/daterangepicker/daterangepicker.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>


</asp:Content>
