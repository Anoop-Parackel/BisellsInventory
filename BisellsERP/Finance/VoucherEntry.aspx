<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VoucherEntry.aspx.cs" Inherits="BisellsERP.Finance.VoucherEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Voucher Entry</title>
    <style>
        #wrapper {
            overflow: hidden;
        }
        .trans-form-control, .trans-form-control:focus, .trans-form-control:active {
            background-color: transparent;
            border: none;
            box-shadow: none;
        }
        #txtDate {
            border-bottom: 1px dashed #ccc;
        }
        .panel {
            border-radius: 6px 6px 0 0;
        }
        .amt-form-control {
            background-color: transparent;
            font-size: 20px;
            text-align: right;
            color: #33b86c;
        }
            .amt-form-control:focus {
                background-color: transparent;
            }
        .narration-form-control {
            background-color: transparent;
            border: none;
            box-shadow: none;
            border: 1px solid #dcdcdc;
        }
        .voucher-wrap {
            width: 800px;
        }
        .voucher-head-wrap {
            background-color: #eaedf2;
            display: inline-block;
            width: calc(100% + 40px);
            margin-left: -20px;
            margin-right: -20px;
            margin-top: -20px;
            padding: 5px;
            border-radius: 6px 6px 0 0;
        }
        #ddlVoucherType {
             border: 1px solid rgba(0, 71, 103, 0.11);
        }
        .vh-40 {
            height: 40vh;
        }
        .table-scroll tbody {
            height: 35vh;
        }
        .left-side {
            padding-right: 30px;
            border-right: 1px dashed #e6e6e6;
        }
        .right-side {
            padding-left: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <%-- Page Title & Buttons --%>
    <div class="row p-b-5">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">
                <asp:Label ID="lblType" runat="server">Voucher</asp:Label>
                Entry</h3>
        </div>
        <div class="col-sm-8">
            <div class="btn-toolbar pull-right">
                <asp:Button ID="btnNew" runat="server" AccessKey="n" data-toggle="tooltip" data-placement="bottom" title="Start a new entry . Unsaved data will be lost" CssClass="btn btn-default" Text="New" OnClick="btnNew_Click" />
                <button id="btnSaveConfirmed1" style="display: none" type="button" data-toggle="tooltip" data-placement="bottom" title="Save the current entry" class="btn btn-success waves-effect"><i class="md-receipt"></i>&nbsp;Save</button>
                <asp:Button ID="btnSaveConfirmed" runat="server" style="display: none" type="button" data-toggle="tooltip" data-placement="bottom" title="Confirm the entry" CssClass="btn btn-default" OnClick="btnSaveConfirmed_Click" Text="Save" />
                <button type="reset" data-toggle="tooltip" data-placement="bottom" title="Reset" class="btn btn-default waves-effect hidden"><i class="md-replay"></i></button>
                <asp:HiddenField ID="hdnGroupID" runat="server" Value="0" />
            </div>
        </div>
    </div>

    <div class="row">
        <div class="">
            <div class="panel">
                <div class="panel-body">
                    <div class="col-sm-5 left-side">

                        <div class="">
                            <div class="col-sm-7">
                                <div class="row">
                                    <label class="control-label col-xs-4 m-t-10">Type</label>
                                    <div class="col-xs-8">
                                        <asp:DropDownList ID="ddlVoucherType" AutoPostBack="true" ClientIDMode="Static" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlVoucherType_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 text-right">
                                <div class="row">
                                    <label class="control-label col-xs-4 m-t-10">Date</label>
                                    <div class="col-xs-8">
                                        <asp:TextBox ID="txtDate" ClientIDMode="Static" runat="server" CssClass="form-control text-right text-info trans-form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-40">
                            <div class="form-group m-b-5">
                                <div class="control-label">
                                    From <span class="pull-right text-muted">Balance :
                                    <asp:Label ID="lblOpeningBalance" runat="server">0</asp:Label></span>
                                </div>
                                <asp:DropDownList ID="ddlfrommain" CssClass="searchDropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlfrommain_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-15">
                            <div class="form-group">
                                <div class="control-label">To <span class="pull-right text-muted">Balance :<asp:Label ID="lblToBalance" runat="server">0</asp:Label></span></div>
                                <asp:DropDownList ID="ddltomain" AutoPostBack="true" CssClass="searchDropdown" runat="server" OnSelectedIndexChanged="ddltomain_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-10 m-t-40">
                            <label class="col-xs-4 m-t-5">Amount : </label>
                            <div class="col-xs-8">
                                <asp:TextBox ID="txtamount" Type="text" runat="server" AutoComplete="false" CssClass="form-control amt-form-control text-success"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2 text-right m-t-40">
                            <asp:Button ID="AddNewHead" runat="server" CssClass="btn btn-default" Text="+" OnClick="AddNewHead_Click" />
                        </div>
                    </div>

                    <div class="col-sm-7 right-side">
                        <div class="vh-40 m-b-20">
                            <table id="tableVouchEntry" class="table table-hover table-scroll">
                                <thead>
                                    <tr>
                                        <th>Description</th>
                                        <th>From</th>
                                        <th>To</th>
                                        <th>Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Literal ID="tableContent" runat="server"></asp:Literal>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-sm-3">
                            <label>Debit Total</label>
                            <br />
                            <label id="drTotal" style="margin-left:30px">0</label>
                        </div>
                        <div class="col-sm-3">
                            <label>Credit Total</label>
                            <br />
                            <label id="crTotal" style="margin-left:30px">0</label>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox ID="txtnarration" TextMode="multiline" Columns="50" Rows="2" runat="server" CssClass="form-control narration-form-control" placeholder="Enter Narration..."></asp:TextBox>
                        </div>
                        <asp:TextBox ID="txtNumber" runat="server" CssClass="hidden" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField ID="hdItemId" ClientIDMode="Static" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnAmount" runat="server" Value="0" />
                        <asp:Button ID="hiddenButton" runat="server" ClientIDMode="Static" Style="display: none" Text="Button" OnClick="hiddenButton_Click" />
                    </div>
                    <div class="hidden">
                        <div></div>
                        <asp:DropDownList ID="ddlfromsub" CssClass="form-control" AutoPostBack="false" runat="server" />
                        <asp:DropDownList ID="ddltosub" AutoPostBack="false" CssClass="form-control" runat="server" />
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="hidden" ReadOnly="true"></asp:TextBox>
                    </div>

                </div>
            </div>

        </div>
    </div>

    <script>
        //All functions inside document ready
        $(document).ready(function () {
            //Date Picker
            $('#txtDate').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd-M-yyyy',
                todayBtn: "linked"
            });

            

        });

        setInterval(calculateSummary, 100);

        //calculation function ends here
        function calculateSummary() {
            var tr = $('#tableVouchEntry > tbody').children('tr');
            var From = 0;
            var To = 0;
            for (var i = 0; i < tr.length; i++) {
                From += parseFloat($(tr[i]).children('td:nth-child(2)').text());
                To += parseFloat($(tr[i]).children('td:nth-child(3)').text());
            }
            //console.log($('#txtDiscount').val());
            $('#drTotal').text(From);
            $('#crTotal').text(To);
        }


        function CheckIfCostHeadExists() {
            var IsCostHeadExists = false;
            if (document.getElementById('ctl00_ContentPlaceHolder1_rowCostHead') != null) {
                if (document.getElementById('ctl00_ContentPlaceHolder1_listCostHead') != null && document.getElementById('ctl00_ContentPlaceHolder1_listCostHead').value != null && document.getElementById('ctl00_ContentPlaceHolder1_listCostHead').value != "") {
                    IsCostHeadExists = true;
                }
                else {
                    IsCostHeadExists = false;
                    alert('Select the Cost Head');
                }
                if (IsCostHeadExists)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
  
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
