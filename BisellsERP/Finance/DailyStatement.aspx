<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyStatement.aspx.cs" Inherits="BisellsERP.Finance.DailyStatement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Finance Daily Statement</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="row">
        <div class="col-md-12 pull-left">
            <h3 class="pull-left text-default">Daily Statement</h3>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="col-md-12">
                    <div class="col-md-4">
                        <label class="control-label">Statements</label>
                        <asp:dropdownlist id="ddlStatements" runat="server" cssclass="form-control">
                            <asp:ListItem Value="0">Cash Book</asp:ListItem>
                        </asp:dropdownlist>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">From</label>
                        <asp:textbox id="txtFromDate" runat="server" cssclass="form-control" placeholder="DD/MMM/YYYY" clientidmode="Static"></asp:textbox>
                    </div>
                    <div class="col-md-4">
                        <label class="control-label">To</label>
                        <asp:textbox id="txtToDate" runat="server" cssclass="form-control" placeholder="DD/MMM/YYYY" clientidmode="Static"></asp:textbox>
                    </div>
                </div>
                <div class="col-md-12 m-t-10">
                    <div class="col-md-12">
                        <asp:button id="btnShow" runat="server" cssclass="btn btn-primary pull-right" text="Show" onclick="btnShow_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel panel-border panel-default">
            <div class="panel-heading">
                <h3 class="panel-title"><i class="fa fa-money">&nbsp&nbsp</i>Transactions</h3>
            </div>
            <div class="panel-body">

                <asp:gridview id="grdCash" runat="server" autogeneratecolumns="False"
                    emptydatatext="No Data Found..!!!" onrowdatabound="grdCash_RowDataBound"
                    style="width: 100%" visible="False" cssclass="table table-striped table-bordered dataTable no-footer dataTable no-footer">

                    <Columns>
                        <asp:BoundField DataField="PayDate" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderText="PayDate">
                            <ItemStyle Width="30%" CssClass="txtCenterAllign" />
                        </asp:BoundField>
                        <asp:BoundField DataField="particular" HeaderText="Particular">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VoucherType" HeaderText="VoucherType">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DebitAmount" HeaderText="DebitAmount">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreditAmount" HeaderText="CreditAmount">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Narration" HeaderText="Narration">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                </asp:gridview>
                <asp:gridview id="grdCheque" runat="server" autogeneratecolumns="False"
                    emptydatatext="No Data Found..!!!" onrowdatabound="grdCash_RowDataBound"
                    visible="False" cssclass="table table-striped table-bordered dataTable no-footer dataTable no-footer">


                    <Columns>
                        <asp:BoundField DataField="PayDate" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderText="PayDate">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="particular" HeaderText="Particular">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VoucherType" HeaderText="VoucherType">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DebitAmount" HeaderText="DebitAmount"
                            ItemStyle-CssClass="txtRightAllign">
                            <ItemStyle CssClass="txtRightAllign" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CreditAmount" HeaderText="CreditAmount"
                            ItemStyle-CssClass="txtRightAllign">
                            <ItemStyle CssClass="txtRightAllign" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Narration" HeaderText="Narration">
                            <ItemStyle Width="30%" />
                        </asp:BoundField>
                    </Columns>
                    <SelectedRowStyle BackColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                </asp:gridview>
                <asp:gridview id="grdDay" runat="server" autogeneratecolumns="False"
                    emptydatatext="No Data Found..!!!" onrowdatabound="grdDay_RowDataBound"
                    visible="False" cssclass="table table-striped table-bordered dataTable no-footer dataTable no-footer">

                    <Columns>
                        <asp:BoundField DataField="PayDate" DataFormatString="{0:dd/MM/yyyy}"
                            HeaderText="PayDate" />
                        <asp:BoundField DataField="particular" HeaderText="Particular" />
                        <asp:BoundField DataField="VoucherType" HeaderText="VoucherType" />
                        <asp:BoundField DataField="CashDebit" HeaderText="CashDebit" ItemStyle-CssClass="txtRightAllign" />
                        <asp:BoundField DataField="CashCredit" HeaderText="CashCredit" ItemStyle-CssClass="txtRightAllign" />
                        <asp:BoundField DataField="BankDebit" HeaderText="BankDebit" ItemStyle-CssClass="txtRightAllign" />
                        <asp:BoundField DataField="BankCredit" HeaderText="BankCredit" ItemStyle-CssClass="txtRightAllign" />

                        <asp:BoundField DataField="Narration" HeaderText="Narration" />
                    </Columns>
                    <SelectedRowStyle BackColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                </asp:gridview>

            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#txtFromDate,#txtToDate').datepicker({
                todayHighlight: true,
                autoclose: true,
                format: 'dd-M-yyyy',
                todayBtn: "linked"
            });

        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
