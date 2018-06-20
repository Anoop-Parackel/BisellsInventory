<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinancialTransactions.aspx.cs" Inherits="BisellsERP.Finance.FinancialTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Financial Transactions</title>
    <style>
        .checkbox-inline + .checkbox-inline, .radio-inline + .radio-inline {
            margin-top: 0;
            margin-left: 5px;
        }

        .panel .panel-body {
            padding: 15px 20px 0;
        }

        .custom-row .control-label {
            color: #9E9E9E;
            font-size: 13px;
        }

        .filter-toggle {
            font-size: 20px;
            margin-top: 5px;
        }

        .transaction-table table > thead > tr > th {
            color: #5b9ebf;
            font-weight: 100;
            font-size: 14px;
            background-color: #E3F2FD;
            padding: 4px 8px;
        }

        .transaction-table table > tbody > tr > td {
            font-size: 14px;
            padding: 4px 8px;
        }

        .edit-tran {
            cursor: pointer;
        }

        .edit-tran:hover {
            color:blue;
            cursor: pointer;
        }

        .delete-tran {
            color: red;
            cursor: pointer;
        }

        .pageNumber {
            cursor: pointer;
        }

            .pageNumber:hover {
                background-color: #e0dbdb;
                font-size: 15px;
                border-radius: 10px;
            }

        .focus {
            background-color: black !important;
            color: white;
            border-radius: 10px;
            font-size: 15px;
        }

        .div-pages {
            margin-bottom: 5px;
        }

        .transaction-table .btn-group.dropdown {
            width: 100%;
            height: 100%;
        }

            .transaction-table .btn-group.dropdown > button {
                /*margin-top: 8px;*/
                padding: 1px 5px;
                background-color: transparent;
                border: none;
                box-shadow: none;
            }

        .transaction-table .open > .dropdown-menu {
            top: 134%;
            left: -8px;
            min-width: calc(100% + 16px);
            padding: 0;
            border-radius: 0 0 3px 3px;
        }

        .filter-body {
            margin-top: 20px;
            margin-bottom: 10px;
        }

        .btn-filter {
            margin-bottom: 10px;
            background-color: transparent;
            color: black !important;
            border-radius: 18px;
            width: 120px;
            border: 1px solid;
        }

            .btn-filter:hover {
                margin-bottom: 10px;
                background-color: transparent;
                color: black !important;
                border-radius: 18px;
                width: 120px;
            }

        .empty-data {
            margin-bottom: 20px;
            font-weight: 100;
            font-size: 18px;
            font-style: oblique;
            font-family: cursive;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
        <div class="col-sm-5">
            <h3 class="page-title m-t-0">Financial Transactions</h3>
        </div>
        <div class="col-sm-7">
        </div>
    </div>

    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="row">
                <div class="col-sm-9">
                    <h3 class="panel-title">Filters</h3>
                </div>
                <div class="col-sm-3 text-right"><i class="fa fa-plus-circle filter-toggle"></i></div>
            </div>
        </div>
        <div class="panel-body filter-content">
            <div class="filter-body col-sm-5">
                <div class="col-sm-12 m-b-10">
                    <div class="radio radio-primary radio-inline">
                        <input type="radio" checked="checked" class="filter-type" name="filter" value="0" id="rdTodaynew" /><label>Today</label>
                    </div>
                    <div class="radio radio-primary radio-inline">
                        <input type="radio" name="filter" class="filter-type" value="1" id="rdLastWeeknew" /><label>Last Week</label>
                    </div>
                    <div class="radio radio-primary radio-inline">
                        <input type="radio" name="filter" class="filter-type" value="2" id="rdLastMonthnew" /><label>Last Month</label>
                    </div>
                    <div class="radio radio-primary radio-inline">
                        <input type="radio" name="filter" class="filter-type" value="3" id="rdAllnew" /><label>All</label>
                    </div>
                    <div class="radio radio-primary radio-inline">
                        <input type="radio" name="filter" class="filter-type" value="4" id="rdCustomnew" /><label>Custom</label>
                    </div>
                </div>
            </div>
            <div class="col-sm-3 hidden custom-div">
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="control-label">From</label>
                        <asp:TextBox ID="txtFromDate" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="control-label">To</label>
                        <asp:TextBox ID="txtTo" ClientIDMode="Static" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 hidden custom-div">
                <div class="col-sm-7">
                    <div class="form-group">
                        <label class="control-label">All Accounts</label>
                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlAccountTypes" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-5">
                    <div class="form-group">
                        <label class="control-label">Voucher Types</label>
                        <asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlVoucherTypes" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="btn-toolbar pull-right m-t-0">
                    <button type="button" id="btnSearch" class="btn btn-block btn-filter">Search</button>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default transaction-table">
        <div class="panel-body">
            <table class="table table-bordered transaction-content" id="tbltransaction" style="width: 100%">
                <thead>
                    <tr>
                        <th class="hidden">Voucher ID</th>
                        <th class="hidden">Group ID</th>
                        <th>Date</th>
                        <th>Voucher Type</th>
                        <th>Voucher No</th>
                        <th>Amount</th>
                        <th>Cheque No</th>
                        <th>Cheque Date</th>
                        <th>Description</th>
                        <th>Narration</th>
                        <th class="hidden">TrnIsDebit</th>
                        <th class="hidden">TrnFrmID</th>
                        <th class="hidden">TrnFrmCldID</th>
                        <th class="hidden">TrnToID</th>
                        <th class="hidden">TrnToCldID</th>
                        <th class="hidden">TrnFve_IsVoucher</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div id="page"></div>
        </div>
    </div>
    <input type="hidden" value="0" id="hdnFiltertype" />
    <script>
        $(function () {

            $('#rdCustom').change(function () {
                $(this).prop('checked') ? $('.custom-row').show : $('.custom-row').hide;
            });

            $('#txtFromDate, #txtTo').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd/M/yyyy',
                todayBtn: "linked"
            });

            InitialLoad();

            $('.filter-type').click(function () {
                $('#hdnFiltertype').val($(this).attr('value'));
                if ($(this).attr('value') == '4') {
                    $('.custom-div').removeClass('hidden');
                    $('.custom-div').fadeIn("slow");
                }
                else {
                    $('.custom-div').addClass('hidden');
                    $('.custom-div').fadeOut("slow");
                    $('#btnSearch').trigger('click');
                }
            })

            $.fn.animateRotate = function (angle, duration, easing, complete) {
                var args = $.speed(duration, easing, complete);
                var step = args.step;
                return this.each(function (i, e) {
                    args.complete = $.proxy(args.complete, e);
                    args.step = function (now) {
                        $.style(e, 'transform', 'rotate(' + now + 'deg)');
                        if (step) return step.apply(e, arguments);
                    };

                    $({ deg: 0 }).animate({ deg: angle }, args);
                });
            };

            $('.filter-toggle').click(function () {
                $('.filter-content').toggle();
                $(".filter-toggle").animateRotate(90, {
                    duration: 500,
                    easing: 'linear',
                    complete: function () { },
                    step: function () { }
                });
            });

            $('#btnSearch').click(function () {
                $('.transaction-table').removeClass('hidden');
                var typeid = $('#hdnFiltertype').val();
                var date = new Date();
                var toDate = date.getMonth();
                var filterType = 0;
                var FromdateString = '';
                var todatestring = '';
                if (typeid == '0') {
                    FromdateString = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                    filterType = 0;
                    date.getDate() + "/" + getmonth(date.getMonth()) + "/" + date.getFullYear();
                    todatestring = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                }
                else if (typeid == '1') {
                    FromdateString = (date.getDate() - 7) + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                    filterType = 0;
                    todatestring = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();

                }
                else if (typeid == '2') {
                    FromdateString = (date.getDate()) + "/" + getmonth((date.getMonth())) + "/" + date.getFullYear();
                    filterType = 0;
                    todatestring = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                }
                else if (typeid == '3') {
                    FromdateString = (date.getDate() - 7) + "/" + getmonth((date.getMonth() - 1)) + "/" + date.getFullYear();
                    filterType = 1;
                    todatestring = date.getDate() + "/" + getmonth(date.getMonth()) + "/" + date.getFullYear();
                }
                else if (typeid == '4') {
                    FromdateString = $('#txtFromDate').val();
                    filterType = 0;
                    todatestring = $('#txtTo').val();
                }
                LoadTableContent(FromdateString, todatestring, filterType);
            });

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

            function LoadTableContent(fromdate, todate, filtertype) {
                var fin = {};
                var date = new Date();
                var toDate = date.getMonth();
                fin.filterType = filtertype;
                if (fromdate == null && todate == null) {
                    var FromdateString = date.getDate() + "/" + getmonth(date.getMonth()) + "/" + (date.getFullYear() - 1);
                    fin.fromdatestring = FromdateString;
                    fin.todatestring = date.getDate() + "/" + getmonth(date.getMonth()) + "/" + (date.getFullYear() + 1);
                }
                else {
                    var FromdateString = fromdate;
                    fin.fromdatestring = FromdateString;
                    fin.todatestring = todate;
                }
                fin.VoucherType = $('#ddlVoucherTypes').val();
                fin.FromAccount = $('#ddlAccountTypes').val();
                fin.CompanyId = $.cookie('bsl_1');
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/FinancialTransactions/GetAccountTransactions',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(fin),
                    success: function (response) {
                        console.log(response);
                        if (response.length > 0) {
                            var html = '';
                            $('#tbltransaction').removeClass('hidden');
                            $('#tbltransaction >tbody').empty();
                            $('.empty-state').empty();
                            $('.pageNumber').remove();
                            $(response).each(function () {
                                html += '<tr>';
                                html += '<td class="hidden Voucher-ID">' + this.TrnVchID + '</td>';
                                html += '<td class="hidden Group-ID">' + this.TrnGroupID + '</td>';
                                var date = new Date(this.TrnDate);
                                var dateString = date.getDate() + '/' + getmonth(date.getMonth() + 1) + '/' + date.getFullYear();
                                html += '<td>' + dateString + '</td>';
                                html += '<td>' + this.TrnVchType + '</td>';
                                html += '<td><a herf="#" class="edit-tran" title="clik to edit the transaction">' + this.TrnVchNo + '</a></td>';
                                html += '<td>' + this.TrnAmount + '</td>';
                                if (this.TrnChequeNo != null) {
                                    html += '<td>' + this.TrnChequeNo + '</td>';
                                }
                                else {
                                    html += '<td></td>';
                                }
                                if (this.TrnChequeDate != null) {
                                    var Chequedate = new Date(this.TrnChequeDate);
                                    var ChequedateString = date.getDate() + '/' + getmonth(date.getMonth() + 1) + '/' + date.getFullYear();
                                    html += '<td>' + ChequedateString + '</td>';
                                }
                                else {
                                    html += '<td></td>';
                                }
                                if (this.TrnDesc != null) {
                                    html += '<td>' + this.TrnDesc + '</td>';
                                }
                                else {
                                    html += '<td></td>';
                                }
                                if (this.TrnNarration != null) {
                                    html += '<td>' + this.TrnNarration + '</td>';
                                }
                                else {
                                    html += '<td></td>';
                                }
                                html += '<td class="hidden">' + this.TrnIsDebit + '</td>';
                                html += '<td class="hidden">' + this.TrnFrmID + '</td>';
                                html += '<td class="hidden">' + this.TrnFrmCldID + '</td>';
                                html += '<td class="hidden">' + this.TrnToID + '</td>';
                                html += '<td class="hidden">' + this.TrnToCldID + '</td>';
                                html += '<td class="hidden">' + this.TrnFve_IsVoucher + '</td>';
                                html += '<td><a href="#" class="delete-tran"><i class="fa fa-trash"></i></a></td>';
                            });
                            html += '<div id="pageination"></div>';
                            $('#tbltransaction >tbody').append(html);
                            Pagination(20);
                        }
                        else {
                            $('#tbltransaction').addClass('hidden');
                            $('.empty-state').empty();
                            $('.pageNumber').remove();
                            var html = '';
                            html += '<div class="text-center empty-state"><img style="width: 20%;" src="../Theme/images/empty_state.png" /><br/><label class="empty-data">No transactions to show</label><div>';
                            $('.transaction-table').append(html);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            }

            //Loads the current day's transaction.
            function InitialLoad() {
                var date = new Date();
                FromdateString = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                filterType = 0;
                date.getDate() + "/" + getmonth(date.getMonth()) + "/" + date.getFullYear();
                todatestring = date.getDate() + "/" + getmonth(date.getMonth() + 1) + "/" + date.getFullYear();
                LoadTableContent(FromdateString, todatestring, filterType);
            }

            //$('.edit-tran').click(function () {
            //    alert($(this).parent('tr').html());

            //});

            $(document).on('click', '.edit-tran', function () {
                var VoucherID = $(this).closest('tr').children('.Voucher-ID').html();
                var GroupID = $(this).closest('tr').children('.Group-ID').html();
                window.open('../Finance/Journal?ID=' + GroupID + '&VOUCHER=' + VoucherID + '', '_blank');
            });

            $(document).on('click', '.delete-tran', function () {
                var GroupID = $(this).closest('tr').children('.Group-ID').html();
                var modifiedBy = $.cookie('bsl_3');
                //alert(GroupID);
                swal({
                    title: "Delete?",
                    text: "Are you sure to delete transaction?",
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Cancel",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Delete"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            url: $('#hdApiUrl').val() + '/api/FinancialTransactions/Delete?id=' + GroupID,
                            method: 'DELETE',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify(modifiedBy),
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    $('#btnSearch').trigger('click');
                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (xhr, err, status) {
                                alert(xhr.responseText);
                                console.log(xhr);
                            }
                        });
                    }
                });
            });

            function Pagination(PerPage) {
                var totalRows = $('#tbltransaction').find('tbody tr:has(td)').length;
                var recordPerPage = 20;
                var totalPages = Math.ceil(totalRows / recordPerPage);
                var $pages = $('<div id="pages" class="div-pages"></div>');
                $pages.children().remove();
                for (i = 0; i < totalPages; i++) {
                    $('<span class="pageNumber">&nbsp;' + (i + 1) + '&nbsp;</span>').appendTo($pages);
                }
                $pages.appendTo('#page');

                $('.pageNumber').click(
                  function () {
                      alert($(this).parent("div").html());
                      $(this).addClass('focus');
                  },
                  function () {
                      $(this).parent("div").find('.focus').removeClass('focus');
                      $(this).removeClass('focus');
                  }
                );

                $('table').find('tbody tr:has(td)').hide();
                var tr = $('table tbody tr:has(td)');
                for (var i = 0; i <= recordPerPage - 1; i++) {
                    $(tr[i]).show();
                }
                $('span').click(function (event) {
                    $(this).toggleClass('focus');
                    $('#tbltransaction').find('tbody tr:has(td)').hide();
                    var nBegin = ($(this).text() - 1) * recordPerPage;
                    var nEnd = $(this).text() * recordPerPage - 1;
                    for (var i = nBegin; i <= nEnd; i++) {
                        $(tr[i]).show();
                    }
                });
            }
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>

    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
