<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="VoucherEdit.aspx.cs" Inherits="BisellsERP.Finance.VoucherEdit" %>

<html>
    <head>
        <script src="/Theme/js/jquery.min.js"></script>
    <!-- Base Css Files -->
    <link href="/Theme/css/bootstrap.min.css" rel="stylesheet" />
	 <link href="/Theme/assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
	  <!-- Custom Files -->
    <link href="/Theme/css/helper.css" rel="stylesheet" type="text/css" />
    <link href="/Theme/css/style.css" rel="stylesheet" type="text/css" />

    </head>
<body>
    <form runat="server">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-group">
                    <b>Voucher Number :</b>
                    <asp:Label ID="lblVoucherNumber" runat="server" Text=""></asp:Label>
                </div>
                <div class="form-group">
                    <b>Voucher Type</b>
                    <asp:DropDownList ID="ddlVoucherType" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <b>Voucher Date</b>
                    <asp:TextBox ID="txtVoucherDate" ClientIDMode="static" runat="server" CssClass="form-control"></asp:TextBox>
                    <script>
                        $(function () {
                            $('#txtVoucherDate').datepicker({
                                todayHighlight: true,
                                autoclose: true,
                                format: 'dd-M-yyyy',
                                todayBtn: "linked"
                            });
                        });
                    </script>
                </div>
                <div class="form-group">
                    <b>Cash/Bank:</b>
                    <div class="radio radio-primary">
                        <asp:RadioButtonList ID="rblIsCheque" runat="server" AutoPostBack="True"
                            RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="rblIsCheque_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Cash A/C&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="1">Bank A/C</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="form-group">
                    <b>Cheque Number:</b>
                    <asp:TextBox ID="txtChequeNumber" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <b>Cheque Date</b>
                    <asp:TextBox ID="txtChequeDate" runat="server" ClientIDMode="static" CssClass="form-control"></asp:TextBox>
                    <script>
                        $(function () {
                            $('#txtChequeDate').datepicker({
                                todayHighlight: true,
                                autoclose: true,
                                format: 'dd-M-yyyy',
                                todayBtn: "linked"
                            });
                        });
                    </script>
                </div>
                <div class="form-group">
                    <b>Narration</b>
                    <asp:TextBox ID="txtNarration" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="cmdUpdateVoucher" runat="server" CssClass="btn btn-primary"
                        Text="Update" OnClick="cmdUpdateVoucher_Click" />
                    <asp:Button ID="cmdDelete" runat="server" CssClass="btn btn-primary"
                        Text="Delete" OnClick="cmdDelete_Click" Visible="False" />
                </div>
                <div class="form-group">
                    <b>Accounts Details</b>
                    <asp:GridView ID="grdVoucher" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                        OnRowDataBound="grdVoucher_RowDataBound" OnRowEditing="grdVoucher_RowEditing" OnRowCancelingEdit="grdVoucher_RowCancelingEdit"
                        OnRowUpdating="grdVoucher_RowUpdating"
                        CssClass="table table-striped table-bordered table-hover">


                        <Columns>
                            <asp:CommandField ShowEditButton="True" />
                            <asp:BoundField ReadOnly="True" HeaderText="Sl.no" />
                            <asp:TemplateField HeaderText="Particulars">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlAccHead" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlChildHead" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                    <%-- <asp:Label ID="lblChildName" runat="server" Text='<%# Bind("ChildName") %>'></asp:Label>--%>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAccHead" runat="server" Text='<%# Bind("AccountName") %>'></asp:Label>
                                    <asp:DropDownList ID="ddlChildHead" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr. Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDr" Text='<%# String.Format("{0:0.00}",Eval("DrAmount"))  %>' runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblDr" Text='<%# String.Format("{0:0.00}",Eval("DrAmount"))  %>' runat="server" CssClass="form-control"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDr" Text='<%# String.Format("{0:0.00}",Eval("DrAmount"))  %>' runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblDr" Text='<%# String.Format("{0:0.00}",Eval("DrAmount"))    %>' runat="server" CssClass="form-control"></asp:Label>
                                </EditItemTemplate>
                                <FooterStyle BackColor="#C0C0FF" />
                                <FooterTemplate>
                                    <asp:Label ID="lblDrAmount" Text='<%#GetDrSum.ToString("N2")%>' runat="server" CssClass="txt"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr. Amount">
                                <EditItemTemplate>
                                    <asp:Label ID="lblCr" Text='<%# String.Format("{0:0.00}",Eval("CrAmount"))    %>' runat="server" CssClass="form-control"></asp:Label>
                                    <asp:TextBox ID="txtCr" Text='<%# String.Format("{0:0.00}",Eval("CrAmount"))    %>' runat="server" CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCr" Text='<%# String.Format("{0:0.00}",Eval("CrAmount"))    %>' runat="server" CssClass="form-control"></asp:Label>
                                    <asp:TextBox ID="txtCr" Text='<%# String.Format("{0:0.00}",Eval("CrAmount"))    %>' runat="server" CssClass="form-control"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle BackColor="#C0C0FF" />
                                <FooterTemplate>
                                    <asp:Label ID="lblCrAmount" Text='<%#GetCrSum.ToString("N2")%>' runat="server" CssClass="txt"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dr/Cr">
                                <EditItemTemplate>
                                    <asp:Label ID="PreFixID" runat="server" Text='<%# Bind("PreFix") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="PreFixID" runat="server" Text='<%# Bind("PreFix") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    &nbsp;<asp:Button ID="cmdUpdate" runat="server" CssClass="btn btn-primary"
                                        Text="Update" UseSubmitBehavior="False" OnClick="cmdUpdate_Click" Visible="True" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ObjectID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="ObjectID" runat="server" Text='<%# Bind("ObjectID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AccountID" Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="AccountID" runat="server" Text='<%# Bind("AccountNO") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="AccountID" runat="server" Text='<%# Bind("AccountNO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ChildID" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="ChildID" runat="server" Text='<%# Bind("ChildID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HasChild" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="HasChild" runat="server" Text='<%# Bind("HasChild") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HasTrans" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="HasTrans" runat="server" Text='<%# Bind("HasTrans") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GroupID" Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="GroupID" runat="server" Text='<%# Eval("GroupID") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="GroupID" runat="server" Text='<%# Bind("GroupID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HasGeneral" Visible="False">
                                <EditItemTemplate>
                                    <asp:Label ID="HasGeneral" runat="server" Text='<%# Bind("HasGeneral") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="HasGeneral" runat="server" Text='<%# Bind("HasGeneral") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="columnColorStyle leftColumnLink" />
                        <AlternatingRowStyle CssClass="leftColumnStyle txt" />
                    </asp:GridView>
                </div>
            </div>
            <asp:HiddenField ID="hdnVoucherID" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="hdnIsVoucher" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="hdnGroupID" runat="server" Value="0"></asp:HiddenField>
        </div>
    </form>
</body>
    <script src="/Theme/js/bootstrap.min.js" type="text/javascript"></script>
	<!-- CUSTOM JS -->
    <script src="/Theme/js/jquery.app.js" type="text/javascript"></script>
    <script src="/Theme/assets/timepicker/bootstrap-datepicker.js" type="text/javascript"></script>
</html>
