<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupedLedger.aspx.cs" Inherits="BisellsERP.Finance.GroupedLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .Row-class {
            background-color: #403e92;
            color: white;
            font-weight: 200;
        }

        .pageNumber {
            cursor: pointer;
        }

        .focus {
            background-color: black;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row p-b-5">
        <div class="col-sm-12">
            <h3 class="pull-left page-title text-default">Grouped Ledger</h3>
        </div>
    </div>
    <div class="panel b-r-8">
        <div class="panel-body">
            <div class="row m-b-10 m-t-10">
                <div class="col-sm-2 col-lg-2">
                    <label class="text-bold">Account Nature</label>
                    <asp:DropDownList ID="ddlAccountNature" CssClass="form-control col-sm-2" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="-1">All</asp:ListItem>
                        <asp:ListItem Value="0">Asset</asp:ListItem>
                        <asp:ListItem Value="1">Income</asp:ListItem>
                        <asp:ListItem Value="2">Expense</asp:ListItem>
                        <asp:ListItem Value="3">Liability</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-lg-3">
                    <label class="text-bold">Account Groups</label>
                    <asp:DropDownList ID="ddlAccountGroups" CssClass="form-control col-sm-2" runat="server" ClientIDMode="Static">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-lg-4">
                    <label class="text-bold">Account Heads</label>
                    <asp:DropDownList ID="ddlTransHead" CssClass="form-control col-sm-2" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-lg-3">
                    <label class="text-bold">Child Head</label>
                    <asp:DropDownList ID="ddlChild" CssClass="form-control col-sm-2" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="-1">--Select--</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-lg-3 m-t-10">
                    <label class="text-bold">From</label>
                    <input type="text" id="txtFromDate" class="form-control" />
                </div>
                <div class="col-sm-3 col-lg-3 m-t-10">
                    <label class="text-bold">To</label>
                    <input type="text" id="txtToDate" class="form-control" />
                </div>
                <div class="col-sm-2 m-t-40 pull-right">
                    <button type="button" accesskey="f" data-toggle="tooltip" data-placement="bottom" title="Show Report" id="btnSearch" class="btn btn-info waves-effect waves-light pull-right"><i class="ion-search"></i>&nbsp;Show Report</button>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-title">
            <h4 class="title-heading">Report<div class="col-sm-2 pull-right hidden">
                <input type="text" id="txtSearch" class="form-control m-t-30" placeholder="Type to search" />
            </div>
            </h4>
        </div>
        <div class="panel-body">
            <div id="dvSearchResult">
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {

            var apiurl = $('#hdApiUrl').val();

            $('#txtFromDate,#txtToDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });
            $('#txtFromDate').datepicker()
                      .on('changeDate', function (ev) {
                          $('#txtFromDate').datepicker('hide');
                      });
            $('#txtToDate').datepicker()
                      .on('changeDate', function (ev) {
                          $('#txtToDate').datepicker('hide');
                      });

            $('#ddlAccountNature').change(function () {
                var AccountNature = $('#ddlAccountNature').val();
                var company = $.cookie('bsl_1');
                $.ajax({
                    url: apiurl + '/api/FinancialTransactions/GetAccountGroup?Nature=' + AccountNature,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(company),
                    success: function (response) {
                        $('#ddlAccountGroups').children('option').remove();
                        $('#ddlAccountGroups').append('<option value="-1">--Select--</option>')
                        for (var i = 0; i < response.length; i++) {
                            $('#ddlAccountGroups').append('<option value=' + response[i].Fag_ID + '>' + response[i].Fag_Name + '</option>')
                            $('#ddlAccountGroups').trigger('change');
                        }

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });

            $('#ddlAccountGroups').change(function () {
                var AccountGroup = $('#ddlAccountGroups').val();
                var company = $.cookie('bsl_1');
                $.ajax({
                    url: apiurl + '/api/FinancialTransactions/GetAccountHeadsByGroup?Group=' + AccountGroup,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(company),
                    success: function (response) {
                        $('#ddlTransHead').children('option').remove();
                        $('#ddlTransHead').append('<option value="-1">--Select--</option>');
                        for (var i = 0; i < response.length; i++) {
                            $('#ddlTransHead').append('<option value=' + response[i].Fah_ID + '>' + response[i].Fah_Name + '</option>');
                            $('#ddlTransHead').trigger('change');
                        }

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });

            $('#ddlTransHead').change(function () {
                var AccountHead = $('#ddlTransHead').val();
                var company = $.cookie('bsl_1');
                $.ajax({
                    url: apiurl + '/api/FinancialTransactions/GetAccountHeadsChild?HeadID=' + AccountHead,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(company),
                    success: function (response) {
                        $('#ddlChild').children('option').remove();
                        $('#ddlChild').append('<option value="-1">--Select--</option>')
                        for (var i = 0; i < response.length; i++) {
                            $('#ddlChild').append('<option value=' + response[i].parent + '>' + response[i].name + '</option>')
                        }

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });

            $('#btnSearch').click(function () {
                var details = [];
                var AccID = $('#ddlTransHead').val();
                var Child = $('#ddlChild').val();
                if (Child == "-1") {
                    var ChildText = "";
                }
                else {
                    var ChildText = $('#ddlChild :selected').text();
                }
                var fromdate = $('#txtFromDate').val();
                var toDate = $('#txtToDate').val();
                var GrpID = $('#ddlAccountGroups').val();
                var company = $.cookie('bsl_1');
                var data = {};
                data.AccountID = AccID;
                data.ChildID = Child;
                data.ChildText = ChildText;
                data.fromdatestring = fromdate;
                data.todatestring = toDate;
                data.GroupID = GrpID;
                data.CompanyId = $.cookie('bsl_1');
                console.log(data);
                var chkDate = "";
                var drTotal = 0;
                var crTotal = 0;
                var html = '';
                var current = 0.0;
                $.ajax({
                    url: apiurl + '/api/FinancialTransactions/GetReport',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (response) {
                        console.log(response);
                        $('#dvSearchResult').children().remove();
                        html += '<table class="table table-bordered" id="tblData">'
                        html += '<thead><tr><td>Head</td><td>Child</td><td>Narration</td><td>Debit</td><td>Credit</td><td>Cheque No</td></tr></thead>';
                        html += '<tbody>';
                        for (var i = 0; i < response.length; i++) {
                            if (response[i].chkDate == null) {
                                chkDate = "";
                            }
                            else {
                                chkDate = response[i].chkDate;
                            }
                            html += '<tr class="normal-row"><td>' + response[i].AcccHead + '</td><td>' + response[i].AccChild + '</td><td>' + response[i].Fte_Desc + '</td><td>' + response[i].Dr + '</td><td>' + response[i].Cr + '</td><td>' + chkDate + '</td></tr>'
                            drTotal += parseFloat(response[i].Dr);
                            crTotal += parseFloat(response[i].Cr);
                            if (i != (response.length - 1)) {
                                if ((response[i + 1].AccChild != response[i].AccChild)) {
                                    if (response[i].OpeningBalType == 'Dr') {
                                        //html += '<tr><td>' + response[i].AcccHead + '</td><td>' + response[i].AccChild + '</td><td>' + response[i].Fte_Desc + '</td><td>' + response[i].Dr + '</td><td>' + response[i].Cr + '</td><td>' + response[i].chkDate + '</td></tr>'
                                        html += '<tr class="Row-class"><td></td><td></td><td>Opening Balance</td><td>0</td><td></td><td></td></tr>';
                                        html += '<tr class="Row-class"><td></td><td></td><td>Curent total</td><td>' + Math.round(drTotal) + '</td><td>' + Math.round(crTotal) + '</td><td></td></tr>';
                                        if (drTotal >= crTotal) {
                                            current = drTotal - crTotal;
                                        }
                                        else {
                                            current = crTotal - drTotal;
                                        }
                                        html += '<tr class="Row-class"><td></td><td></td><td>Closing Balance</td><td>' + Math.round(current) + '</td><td></td><td></td></tr>';
                                        drTotal = 0;
                                        crTotal = 0;
                                    }
                                    else {
                                        html += '<tr class="Row-class"><td></td><td></td><td>Opening Balance</td><td>0</td><td></td><td></td></tr>';
                                        html += '<tr class="Row-class"><td></td><td></td><td>Curent total</td><td>' + Math.round(drTotal) + '</td><td>' + Math.round(crTotal) + '</td><td></td></tr>';
                                        if (drTotal >= crTotal) {
                                            current = drTotal - crTotal;
                                        }
                                        else {
                                            current = crTotal - drTotal;
                                        }
                                        html += '<tr class="Row-class"><td></td><td></td><td>Closing Balance</td><td>' + Math.round(current) + '</td><td></td><td></td></tr>';
                                        drTotal = 0;
                                        crTotal = 0;
                                    }
                                }
                            }
                            if ((i + 1) == response.length) {
                                if (response[i].OpeningBalType == 'Dr') {
                                    //html += '<tr><td>' + response[i].AcccHead + '</td><td>' + response[i].AccChild + '</td><td>' + response[i].Fte_Desc + '</td><td>' + response[i].Dr + '</td><td>' + response[i].Cr + '</td><td>' + response[i].chkDate + '</td></tr>'
                                    html += '<tr class="Row-class"><td></td><td></td><td>Opening Balance</td><td>0</td><td></td><td></td></tr>';
                                    html += '<tr class="Row-class"><td></td><td></td><td>Curent total</td><td>' + Math.round(drTotal) + '</td><td>' + Math.round(crTotal) + '</td><td></td></tr>';
                                    if (drTotal >= crTotal) {
                                        current = drTotal - crTotal;
                                    }
                                    else {
                                        current = crTotal - drTotal;
                                    }
                                    html += '<tr class="Row-class"><td></td><td></td><td>Closing Balance</td><td>' + Math.round(current) + '</td><td></td><td></td></tr>';
                                    drTotal = 0;
                                    crTotal = 0;
                                }
                                else {
                                    html += '<tr class="Row-class"><td></td><td></td><td>Opening Balance</td><td>0</td><td></td><td></td></tr>';
                                    html += '<tr class="Row-class"><td></td><td></td><td>Curent total</td><td>' + Math.round(drTotal) + '</td><td>' + Math.round(crTotal) + '</td><td></td></tr>';
                                    if (drTotal >= crTotal) {
                                        current = drTotal - crTotal;
                                    }
                                    else {
                                        current = crTotal - drTotal;
                                    }
                                    html += '<tr class="Row-class"><td></td><td></td><td>Closing Balance</td><td>' + Math.round(current) + '</td><td></td><td></td></tr>';
                                    drTotal = 0;
                                    crTotal = 0;
                                }
                            }
                        }
                        html += '</tbody>';
                        $('#dvSearchResult').append(html);
                        var TotalRows = $('#tblData').find('tbody tr:has(td)').length;
                        Pagination(TotalRows, 15);

                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });

            $('body').on('keyup', '#txtSearch', function () {
                var searchText = $(this).val().toLowerCase();
                $.each($(".normal-row"), function () {
                    if ($(this).text().toLowerCase().indexOf(searchText) === -1) {
                        $(this).hide();
                        $(this).siblings().not('.normal-row').hide();
                    }
                    else {
                        $(this).show();
                        $(this).siblings().not('.normal-row').show();
                    }
                });
            });

            function Pagination(TotalRows, PerPage) {
                var totalRows = $('#tblData').find('tbody tr:has(td)').length;
                var recordPerPage = 20;
                var totalPages = Math.ceil(totalRows / recordPerPage);
                var $pages = $('<div id="pages"></div>');
                $pages.children().remove();
                for (i = 0; i < totalPages; i++) {
                    $('<span class="pageNumber">&nbsp;' + (i + 1) + '&nbsp;&nbsp;</span>').appendTo($pages);
                }
                $pages.appendTo('#tblData');

                $('.pageNumber').hover(
                  function () {
                      $(this).addClass('focus');
                  },
                  function () {
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
                    $('#tblData').find('tbody tr:has(td)').hide();
                    var nBegin = ($(this).text() - 1) * recordPerPage;
                    var nEnd = $(this).text() * recordPerPage - 1;
                    for (var i = nBegin; i <= nEnd; i++) {
                        $(tr[i]).show();
                    }
                });
            }
        });
    </script>
</asp:Content>
