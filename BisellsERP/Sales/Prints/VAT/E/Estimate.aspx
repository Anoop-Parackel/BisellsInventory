<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Estimate.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.E.Estimate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Estimate Print</title>
     <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <style>
        body {
            background-color: #FFF;
        }

        .amount-word {
            text-transform: uppercase;
            font-size:15px;
            font-weight:500;
        }

        table {
            /*background-color: #f7f7f7;*/
            background-color: transparent;
        }

            table tbody tr th, table tbody tr td {
                font-size: 10px;
            }

        .print-wrapper {
            /*padding: 2em 4em;*/
            padding: 2em 1em;
            font-family: calibiri !imporant;
            color: #000 !important;
        }

        hr {
            border-color: #000;
        }

        .table-responsive {
            min-height: 200px;
            height: 100%;
        }

        label {
            font-weight: 500;
        }

        .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
            border: 1px solid #000;
            color: #000;
        }

        .inv-head {
            border: 1px solid #000;
            padding: 0;
            margin: 0px 10px 0;
        }

        .table-bordered {
            border: 1px solid #000;
        }

        .table tbody tr td {
            border-color: transparent;
            padding: 2px 4px;
        }

        .table tbody tr th {
            border-left-color: transparent !important;
            border-right-color: transparent !important;
        }

        .table tbody tr > td:nth-of-type(n+4) {
            text-align: right;
        }

        .table tbody tr > th:nth-of-type(n+4) {
            text-align: right;
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
                font-family: calibiri !imporant;
                color: #000 !important;
                height: 15cm !important;
            }

            .table-bordered th,
            .table-bordered td {
                border: 1px solid #000 !important;
            }

            .table tbody tr td {
                border-color: transparent !important;
                padding: 2px 4px;
            }

            .table tbody tr th {
                border-left-color: transparent !important;
                border-right-color: transparent !important;
            }

            .table tbody tr > td:nth-of-type(n+4) {
                text-align: right;
            }

            .table tbody tr > th:nth-of-type(n+4) {
                text-align: right;
            }

            table tbody tr th, table tbody tr td {
                font-size: 10px;
            }

            .table {
                border: 1px solid #000 !important;
            }

            .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
                border: 1px solid #000;
                color: #000;
            }

            .inv-head {
                border: 1px solid #000;
                padding: 0;
                margin: 0px 10px 0;
            }

            hr {
                border-color: #000;
            }

            .right-dark-border {
                border-right: 1px solid #000;
                height: 100px;
            }

            .table-responsive {
                min-height: 200px;
                height: 100%;
                overflow-x: hidden;
            }
        }

        #lblComp {
            font-size: 20px;
        }

        @page {
            margin-top: 1in;
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
                        <div class="col-xs-3">
                            <h4>
                                <asp:Image ID="imgLogo" ImageUrl="~/Theme/images/image004.jpg" runat="server" Width="200px" Height="120px" />

                            </h4>
                            <div class="col-xs-12 text-center hidden">
                                <address class=" cambria-font">
                                    <strong>
                                        <asp:Label ID="lblComp" runat="server" />xxx</strong><br />
                                    <asp:Label ID="lblCompAddr1" runat="server" />xxx<br />
                                    <asp:Label ID="lblCompAddr2" runat="server" />xx<br />
                                    E-mail :
                                <asp:Label ID="lblCompEmail" runat="server" />xx@xx.com<br />
                                    Ph :
                                <asp:Label ID="lblCompPh" runat="server" />888888888
                                </address>
                            </div>
                        </div>
                        <div class="col-xs-9 text-center cambria-font">
                            <h2 class="text-primary m-t-0 m-b-20 pull-right cambria-font"><strong>Estimate</strong></h2>
                            <p class="m-b-0 hidden cambria-font">{ Original For Recipient }</p>
                        </div>
                    </div>
                    <hr class="m-0" />
                    <%-- INVOICE DETAILS --%>
                    <div class="row" style="margin-top: 15px;">
                        <div class="col-xs-6 cambria-font" style="border-right: 1px solid #ececec">
                            <strong>Project :</strong><asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            <br />
                            <strong>Contact :</strong>
                            <address class=" cambria-font" style="padding-left: 35px;">
                                <strong>
                                    <asp:Label ID="lblCustName" runat="server" /></strong><br />
                                <asp:Label ID="lblCustPhone" runat="server" /><br />
                            </address>
                            <strong>TRN# :</strong><asp:Label ID="lblTRN" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-4 text-center hidden cambria-font" style="border-right: 1px solid #ececec">
                            <h2 class="text-primary m-t-0 m-b-20 cambria-font"><strong>QUOTATION</strong></h2>
                            <p class="m-b-0 cambria-font">{ Original For Recipient }</p>

                        </div>
                        <div class="col-xs-3 cambria-font" style="border-right: 1px solid #ececec; height: 120px;">
                            <strong>Quotation No: </strong>
                            <asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" />
                            <br />
                            <strong class="hidden cambria-font">LPO No :</strong>
                            <asp:Label ID="lblLPONumber" CssClass="hidden" runat="server"></asp:Label><br />
                            <strong>Quote Reference:</strong>
                            <asp:Label ID="lblQuoteRef" runat="server"></asp:Label>
                            <hr class="m-b-0 m-t-5 cambria-font" />
                            <address style="padding-left: 25px;" class="hidden cambria-font">
                                <strong>
                                    <asp:Label ID="lblLocName" runat="server" />location</strong><br />
                                <asp:Label ID="lblLocAddr1" runat="server" />address<br />
                                <asp:Label ID="lblLocAddr2" runat="server" />address<br />
                                <asp:Label ID="lblLocPhone" runat="server" />8888888
                            </address>
                            <div class="col-xs-12">
                                <div class="col-xs-6 text-center cambria-font" style="border-right: 1px solid #ececec; height: 55px;">
                                    <strong>Validity</strong>
                                    <br />
                                    <asp:Label ID="lblValidity" runat="server"></asp:Label>
                                </div>
                                <div class="col-xs-6 text-center cambria-font">
                                    <strong>Completion</strong>
                                    <br />
                                    <asp:Label ID="lblCompletion" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="co-xs-3 text-right cambria-font" style="padding-right: 20px;">
                            <div style="border-left: 1px solid black">
                                <p>
                                    <strong>Date: </strong>
                                    <asp:Label ID="lblDate" Text="Jun 15, 2015" runat="server" />
                                </p>
                                <p class="m-t-10 hidden">
                                    <strong>DO No:</strong>
                                    <asp:Label ID="lblDoNumber" runat="server"></asp:Label>
                                </p>
                                <p class="m-t-10 hidden">
                                    <strong>Registration Id : </strong>
                                    <asp:Label ID="lblCompGst" Text="#xx" runat="server" />
                                </p>
                                <p>
                                    <strong>Payment Terms:</strong>
                                    <asp:Label ID="lblPaymentTerms" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <%-- INVOICE ITEMS --%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive cambria-font">
                                <asp:Table ID="listTable" CssClass="table table-bordered cambria-font" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell CssClass="hidden">Item Code</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Item Name</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Qty</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>Mrp</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Rate</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>VAT %</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Gross</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>Tax </asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Net</asp:TableHeaderCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </div>
                        </div>
                    </div>
                    <hr class="m-b-0" />

                    <%-- INVOICE SUMMARY --%>
                    <div class="row m-t-15">
                        <div class="col-xs-8">
                            <p style="margin-top: 85px" class="text-center cambria-font">
                                <asp:Label ID="lblAmountinWords" runat="server" CssClass="amount-word cambria-font">Eight Thousand One Hundred Ninety only</asp:Label>
                            </p>
                            <hr style="margin-top: 37px;" />
                        </div>
                        <div class="col-xs-4 pull-right cambria-font" style="border-left: 1px solid black">
                            <p class="text-right m-b-0 cambria-font">
                                <b>Total Gross: </b>
                                <asp:Label ID="lblGross" Text="0.00" runat="server" />
                            </p>
                            <p class="text-right m-0">
                                Total Tax:
                                <asp:Label ID="lblTax" Text="0.00" runat="server" />
                            </p>
                            <p class="text-right m-b-0 hidden">
                                Round Off :
                                <asp:Label ID="lblroundOff" Text="0.00" runat="server" />
                            </p>
                            <p class="text-right m-b-10">
                                Deduction:
                                <asp:Label ID="lblDeduction" runat="server">0.00</asp:Label>
                            </p>
                            <hr class="m-t-0" />
                            <h3 class="text-right">
                                <asp:Label ID="lblCurrency" Text="AED" runat="server" />&nbsp;
                                <asp:Label ID="lblNet" Text="0.00" runat="server" /></h3>
                            <hr class="m-b-0" />
                        </div>
                        <div class="col-xs-12">
                            <div class="col-xs-6 cambria-font">
                                <asp:Literal ID="tAndC" runat="server"> 
                                <ul>
                                  <li>Termination of using or accessing your website</li>
                                  <li>Disclosure to inform country laws</li>
                                  <li>Contact details to inform users how they contact you with questions</li>
                               </ul>
                                </asp:Literal>
                            </div>
                            <div class="col-xs-6 text-center pull-right cambria-font">
                                <h4 class="m-t-0">For&nbsp;<asp:Label ID="lblCompName" Text="xx" runat="server" /></h4>
                                <div style="height: 90px;"></div>
                                <h5><b>AUTHORISED SIGNATURE</b></h5>
                            </div>
                        </div>
                        <div class="col-xs-6 text-center hidden" style="border-right: 1px solid #ccc;">
                            <h4 class="m-t-20">For&nbsp;<asp:Label ID="lblCustomerName" Text="xx" runat="server" /></h4>
                            <div style="height: 90px;"></div>
                            <h5><b>RECEIVED BY</b></h5>
                        </div>
                    </div>
                    <div class="row text-center cambria-font">
                        <asp:Literal ID="lblPageFooter" runat="server">
                            <p>Any query regarding this quotation should contact 0556203985 or 0551650330</p>
                            <p style="font-weight: 800; font-size: 18px;">Thank You For Your Business With US!!!</p>
                            <p>* Exhibitions Stands * Singage * Printing * Acrylic * Joinery * Building Maintenance *</p>
                            <p>P.O. Box: 28138 ~ Ind.# 17, Sharjah, UAE ~ 06-5346707 ~ accounts@idesign-me.com</p>
                        </asp:Literal>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
