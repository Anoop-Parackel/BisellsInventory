<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Request.aspx.cs" Inherits="BisellsERP.Purchase.Print.VAT.E.Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchase Request Print</title>
      <%-- STYLE SHEETS --%>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <style>
        body {
            background-color: #FFF;
        }

        table {
            /*background-color: #f7f7f7;*/
            background-color: aliceblue;
        }

            table tbody tr th, table tbody tr td {
                font-size: 14px;
            }

        .print-wrapper {
            /*padding: 2em 4em;*/
            padding: 2em 1em;
        }

        hr {
            border-color: #ccc;
        }

        .table-responsive {
            min-height: 250px;
            height: 100%;
        }

        label {
            font-weight: 500;
        }
        .table tbody > tr > th:nth-of-type(n+4) {
                text-align:right !important;
            }
            .table tbody > tr > td:nth-of-type(n+4) {
                text-align:right !important;
            }

        body {
            font-family: 'cambria', "Helvetica Neue",Helvetica,Arial,sans-serif !important;
        }

        .cambria-font {
            font-family: 'cambria' !important;
        }

        @media print {
            body {
                font-family: 'cambria', "Helvetica Neue",Helvetica,Arial,sans-serif !important;
            }

            .cambria-font {
                font-family: 'cambria' !important;
            }
            .print-wrapper {
                padding: 0em;
            }

            .table-responsive {
                overflow-x: hidden;
            }
            .table tbody > tr > th:nth-of-type(n+4) {
                text-align:right !important;
            }
            .table tbody > tr > td:nth-of-type(n+4) {
                text-align:right !important;
            }
        }

        #lblComp {
            font-size: 20px;
        }
    </style>
</head>
<body>
    <div class="print-wrapper cambria-font">
        <form id="form1" runat="server">
            <div class="row cambria-font">
                <div class="col-md-12 cambria-font">
                    <%-- INVOICE HEAD --%>
                    <div class="clearfix cambria-font">
                        <div class="col-xs-3  cambria-font">
                            <h4>
                                <asp:Image ID="imgLogo" runat="server" Width="100px" Height="40px" />
                            </h4>
                        </div>
                        <div class="col-xs-5 text-center hidden">
                            <h2 class="text-primary m-t-0 m-b-20"><strong>Purchase Request</strong></h2>
                            <p class="m-b-0">{ Original For Recipient }</p>

                        </div>
                        <div class="col-xs-12 text-center cambria-font">
                            <address class=" cambria-font">
                                <strong>
                                <asp:Label ID="lblComp" runat="server" /></strong><br />
                                <asp:Label ID="lblCompAddr1" runat="server" /><br />
                                <asp:Label ID="lblCompAddr2" Text="trivandrum" runat="server" /><br />
                                E-mail :
                                <asp:Label ID="lblCompEmail" runat="server" /><br />
                                Ph :
                                <asp:Label ID="lblCompPh" runat="server" />
                            </address>
                        </div>

                    </div>
                    <hr class="m-0" />
                    <%-- INVOICE DETAILS --%>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-xs-4 text-center cambria-font" style="border-right: 1px solid #ececec">
                            <h2 class="text-primary m-t-0 m-b-20 cambria-font"><strong>Purchase Request</strong></h2>
                            <p class="m-b-0 cambria-font">{ Original For Recipient }</p>
                        </div>
                        <div class="col-xs-4 hidden">
                            <address style="padding-left: 25px;">
                                <strong>
                                <asp:Label ID="lblLocName" runat="server" /></strong><br />
                                <asp:Label ID="lblLocAddr1" runat="server" /><br />
                                <asp:Label ID="lblLocAddr2" runat="server" /><br />
                                <asp:Label ID="lblLocPhone" runat="server" />
                            </address>
                        </div>
                        <div class="co-xs-4 text-right cambria-font" style="padding-right: 20px;">
                            <p>
                                <strong>Date: </strong>
                                <asp:Label ID="lblDate" Text="Jun 15, 2015" runat="server" />
                            </p>
                            <p class="m-t-10 cambria-font">
                                <strong>Invoice No: </strong>
                                <asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" />
                            </p>
                            <p class="m-t-10 cambria-font">
                                <strong>VAT Registration: </strong>
                                <asp:Label ID="lblCompGst" Text="#xx" runat="server" />
                            </p>
                        </div>
                    </div>
                    <%-- INVOICE ITEMS --%>
                    <div class="row cambria-font">
                        <div class="col-md-12 cambria-font">
                            <div class="table-responsive cambria-font">
                                <asp:Table ID="listTable" CssClass="table table-bordered cambria-font" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Item Code</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Item Name</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Qty</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Mrp</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Vat %</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Rate</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Gross</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Tax </asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Net</asp:TableHeaderCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>
                    <hr class="m-b-0" />

                    <%-- INVOICE SUMMARY --%>
                    <div class="row m-t-15 cambria-font">
                        <div class="col-xs-5 text-center cambria-font" style="border-right: 1px solid #ccc;">
                            <h4 class="m-t-20 cambria-font">For&nbsp;<asp:Label ID="lblCompName" Text="xx" runat="server" /></h4>
                            <div style="height: 90px;"></div>
                            <h5><b>AUTHORISED SIGNATORY</b></h5>
                            <h5 class="hidden">Terms and Conditions</h5>
                            <%-- <ul class="hidden">
                                <li>Termination of using or accessing your website</li>
                                <li>Disclosure to inform country laws</li>
                                <li>Contact details to inform users how they contact you with questions</li>
                            </ul>--%>
                            <asp:Literal ID="tAndC" runat="server"></asp:Literal>

                        </div>
                        <div class="col-xs-3 col-xs-offset-4 m-t-20 cambria-font">
                            <p class="text-right m-b-0 cambria-font">
                                <b>Total Gross: </b>
                                <asp:Label ID="lblGross" Text="xx" runat="server" />
                            </p>
                            <p class="text-right m-0 cambria-font">
                                Total Tax:
                                <asp:Label ID="lblTax" Text="xx" runat="server" />
                            </p>
                            <p class="text-right m-b-10 cambria-font">
                                Round Off :
                                <asp:Label ID="lblroundOff" Text="xx" runat="server" />
                            </p>
                            <hr class="m-t-0" />
                            <h3 class="text-right cambria-font">
                                <asp:Label ID="lblCurrency" Text="$" runat="server" />&nbsp;
                                <asp:Label ID="lblNet" Text="xx" runat="server" /></h3>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <!-- SCRIPTS BELOW  -->
    <script src="../../../../Theme/js/bootstrap.min.js"></script>
    <script src="../../../../Theme/js/jquery.min.js"></script>
</body>
</html>
