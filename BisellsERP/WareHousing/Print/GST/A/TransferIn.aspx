<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferIn.aspx.cs" Inherits="BisellsERP.WareHousing.Print.GST.A.TransferIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>TransferIn Print</title>
    <%-- STYLE SHEETS --%>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <style>
        body {
            background-color: #FFF;
        }

        table {
            /*background-color: #f7f7f7;*/
            background-color: aliceblue;
        }

            table tbody tr th, table tbody tr td {
                font-size: 10px;
            }

        .print-wrapper {
            /*padding: 2em 4em;*/
            padding: 2em 1em;
        }

        hr {
            border-color: #ccc;
        }

        .table-responsive {
            min-height: 465px;
            height: 100%;
        }

        label {
            font-weight: 500;
        }

        @media print {
            .print-wrapper {
                padding: 0em;
            }

            .table-responsive {
                overflow-x: hidden;
            }
        }
    </style>
</head>
<body>
   <div class="print-wrapper">
        <form id="form1" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <%-- INVOICE HEAD --%>
                    <div class="clearfix">
                        <div class="pull-left">
                            <h4 class="text-right">
                                <asp:Image ID="imgLogo" runat="server" Width="100px" Height="40px" />
                            </h4>
                        </div>
                        <div class="col-xs-5 text-center">
                            <h2 class="text-primary m-t-0 m-b-30"><strong>Transfer In</strong></h2>
                            <p class="m-b-0">Form GST INV-1</p>
                            <p class="m-b-0">{ Original For Recipient }</p>

                        </div>
                        <div class="pull-right">
                            <address class="text-right">
                                <strong>
                                <asp:Label ID="lblComp" runat="server" /></strong><br />
                                <asp:Label ID="lblCompAddr1" runat="server" /><br />
                                <asp:Label ID="lblCompaddr2" runat="server" /><br />
                                <asp:Label ID="lblCity" runat="server" /><br />
                                <asp:Label ID="lblCompState" runat="server" /><br />
                                <asp:Label ID="lblCompCountry" runat="server" /><br />
                                <b>Ph : </b>
                                <asp:Label ID="lblCompPh" runat="server" /><br />
                                <b>RegistrationId : </b>
                                <asp:Label ID="lblCompRegId" runat="server" />
                            </address>
                        </div>
                    </div>
                    <hr class="m-0" />

                    <%-- INVOICE DETAILS --%>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-xs-4">
                            <label class="control-label">Deliver To :</label>
                            <address style="padding-left: 25px;">
                                <strong>
                                <asp:Label ID="lblLocName" runat="server" /></strong><br />
                                <asp:Label ID="lblLocAddr1" runat="server" /><br />
                                <asp:Label ID="lblLocAddr2" runat="server" /><br />
                                <b>Ph : </b>
                                <asp:Label ID="lblLocPhone" runat="server" /><br />
                                <b>RegistrationId : </b>
                                <asp:Label ID="lblLocRegId" runat="server" />
                            </address>
                        </div>
                        <div class="co-xs-4 text-right" style="padding-right: 20px;">
                            <label class="control-label">Invoice Details</label>
                            <p>
                                <strong>Date: </strong>
                                <asp:Label ID="lblDate" Text="Jun 15, 2015" runat="server" />
                            </p>
                            <p class="m-t-10">
                                <strong>Invoice No: </strong>
                                <asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" />
                            </p>
                            <p class="m-t-10">
                                <strong>GSTIN: </strong>
                                <asp:Label ID="lblCompGst" Text="#xx" runat="server" />
                            </p>
                        </div>
                    </div>

                    <%-- INVOICE ITEMS --%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Table ID="listTable" CssClass="table m-t-30" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Item Name</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Item Code</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Mrp</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Rate</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Quantity</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Gross</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>%</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>CGST</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>SGST</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>IGST</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Net</asp:TableHeaderCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>

                    <%-- INVOICE SUMMARY --%>
                    <div class="row m-t-20">
                        <div class="col-xs-9">
                            <%--<h5>Terms and Conditions</h5>
                            <ul>
                                <li>Termination of using or accessing your website</li>
                                <li>Disclosure to inform country laws</li>
                                <li>Contact details to inform users how they contact you with questions</li>
                            </ul>--%>
                            <asp:Literal ID="tAndC" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-3 m-t-30">
                            <p class="text-right m-b-0">
                                <b>Sub-total: </b>
                                <asp:Label ID="lblTotal" Text="xx" runat="server" />
                            </p>
                            <p class="text-right m-0">
                                Tax Amount:
                                <asp:Label ID="lblTax" Text="xx" runat="server" />
                            </p>
                            <p class="text-right m-0">
                                Round Off :
                                <asp:Label ID="lblroundOff" Text="xx" runat="server" />
                            </p>
                            <hr class="m-t-0" />
                            <h3 class="text-right">
                                <asp:Label ID="lblCurrency" Text="$" runat="server" />&nbsp;
                                <asp:Label ID="lblNet" Text="xx" runat="server" /></h3>
                        </div>
                    </div>
                    <hr />
                </div>
            </div>
        </form>
    </div>

    <!-- SCRIPTS BELOW  -->
    <script src="../../../../Theme/js/bootstrap.min.js"></script>
    <script src="../../../../Theme/js/jquery.min.js"></script>
</body>
</html>
