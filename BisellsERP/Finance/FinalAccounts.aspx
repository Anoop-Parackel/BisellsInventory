<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinalAccounts.aspx.cs" Inherits="BisellsERP.Finance.FinalAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Final Accounts</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label class="control-label">From:</label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">To:</label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">Cost Center</label>
                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control" ClientIDMode="Static">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 m-t-10">
                        <div class="checkbox-primary hidden">
                            <asp:CheckBox ID="chkPeriod" runat="server" CssClass="checkbox checkbox-circle checkbox-primary" ClientIDMode="Static" Text="Period" />
                        </div>
                    </div>
                    <div class="col-md-4 m-t-20">
                        <div class="btn-toolbar pull-right">
                            <asp:Button ID="btnShowPL" runat="server" CssClass="btn btn-primary" Text="Profit and Loss" OnClick="btnShowPL_Click" />
                            <asp:Button ID="btnBalanceSheet" runat="server" CssClass="btn btn-primary" Text="Balance Sheet" OnClick="btnBalanceSheet_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <asp:Literal ID="grdLiteral" runat="server"></asp:Literal>
            </div>
        </div>
    </div>
    <div class="row hidden">
        <asp:HiddenField ID="hiddenCompanyId" runat="server" />
        <asp:HiddenField ID="hiddenDataSQL" runat="server" />
        <asp:HiddenField ID="hiddenDataSQLName" runat="server" />
        <asp:HiddenField ID="hiddenDataSQLId" runat="server" />
        <asp:HiddenField ID="hiddenDataSQLTable" runat="server" />
        <asp:HiddenField ID="hiddenFobId" runat="server" />
        <asp:HiddenField ID="hiddenReportId" runat="server" />
    </div>
    <script>
        $(document).ready(function () {
            $('#txtFromDate,#txtToDate').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd-M-yyyy',
                todayBtn: "linked"
            })
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
