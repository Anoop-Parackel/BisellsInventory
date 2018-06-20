<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.D.Quote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <style>
        body {
            background-color: #FFF;
        }

        table {
            /*background-color: #f7f7f7;*/
            background-color: transparent;
        }

            table tbody tr th, table tbody tr td {
                font-size: 10px;
            }

        .amount-word {
            text-transform: uppercase;
            font-size: 11px;
            font-weight: 600;
        }

        .print-wrapper {
            /*padding: 2em 4em;*/
            padding: 1em 1em;
            font-family: calibiri !important;
            color: #000 !important;
        }

        hr {
            border-color: #000;
        }

        .table-responsive {
            min-height: 150px;
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

        .table tbody tr:nth-child(1) th {
            font-size: 12px;
            padding: 0;
        }

        .text-blue {
            color: #42A5F5 !important;
        }

        .text-grey-light {
            color: #9E9E9E !important;
        }

        .text-green {
            color: #8BC34A !important;
        }

        .bg-blue {
            background-color: #42A5F5 !important;
            color: #fff !important;
        }

        .bg-green {
            background-color: #8BC34A !important;
            color: #fff !important;
        }

        .literal-control {
            font-size: 12px !important;
        }

            .literal-control p, .literal-control li {
                margin-bottom: 0 !important;
            }

            .literal-control ul, .literal-control ol {
                padding-left: 10px;
            }

        .underline {
            text-decoration: underline !important;
        }

        .p-foot {
            font-family: cursive;
            font-size: 10px !important;
            margin-bottom: 0;
        }

        @media print {
            .print-wrapper {
                padding: 0em;
                font-family: calibiri !imporant;
                color: #000 !important;
                height: 15cm !important;
            }

            .p-foot {
                font-family: cursive;
                font-size: 10px !important;
                margin-bottom: 0;
            }

            .literal-control {
                font-size: 12px !important;
            }

                .literal-control p, .literal-control li {
                    margin-bottom: 0 !important;
                }

                .literal-control ul, .literal-control ol {
                    padding-left: 10px;
                }

            .text-blue {
                color: #42A5F5 !important;
            }

            .text-grey-light {
                color: #9E9E9E !important;
            }

            .text-green {
                color: #8BC34A !important;
            }

            .bg-blue {
                background-color: #42A5F5 !important;
                color: #fff !important;
            }

            .bg-green {
                background-color: #8BC34A !important;
                color: #fff !important;
            }

            .amount-word {
                text-transform: uppercase;
                font-size: 11px;
                font-weight: 600;
            }

            .table tbody tr:nth-child(1) th {
                font-size: 12px;
                padding: 0;
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
                min-height: 150px;
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
                        <div class="col-xs-6">
                            <asp:Image ID="imgLogo" ImageUrl="~/Theme/images/image004.jpg" runat="server" Height="120px" />
                            <div class="col-xs-12 text-center hidden">
                                <address class="m-b-5">
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
                        <div class="col-xs-6 text-right">
                            <h2 class="text-greeen m-t-40">
                                <strong>
                                    <asp:Label ID="lblMainHead" runat="server" CssClass="underline text-green">QUOTATION</asp:Label></strong>
                            </h2>
                            <br />
                            <%--<p class="m-b-0 m-t-10 pull-right"><asp:Label ID="lblPaymentStatus" runat="server">Paid</asp:Label></p>--%>
                        </div>
                        <h5 class="text-primary m-t-0 m-b-20 m-r-10 pull-right hidden">
                            <asp:Label ID="lblPaymentStatus" runat="server"></asp:Label></h5>
                    </div>
                    <hr class="m-b-0" />
                    <%-- INVOICE DETAILS --%>
                    <div class="row">

                        <div class="col-xs-6" style="border-right: 1px solid #000; font-size: 12px; height: 145px;">

                            <div class="row">
                                <div class="col-xs-3">
                                    <strong>Project<span class="pull-right">: </span></strong>
                                </div>
                                <div class="col-xs-9" style="padding:0">
                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-3">
                                    <strong>Contact<span class="pull-right">: </span></strong>
                                </div>
                                <div class="col-xs-9" style="padding: 0">
                                    <address class="m-b-5" style="min-height: 45px;">
                                        <strong>
                                        <asp:Label ID="lblCustName" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCustCompanyName" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCustAddress" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCustAddress2" runat="server"></asp:Label><br />
                                        <asp:Label ID="lblCustPhone" runat="server" /><br /></strong>
                                    </address>
                                </div>
                                <div class="col-xs-3">
                                    <strong>Email<span class="pull-right">: </span></strong>
                                </div>
                                <div class="col-xs-9" style="padding:0">
                                    <strong><asp:Label ID="lblCustEmail" runat="server"></asp:Label></strong>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-3">
                                    <strong>TRN#<span class="pull-right">: </span></strong>
                                </div>
                                <div class="col-xs-9" style="padding:0">
                                    <strong><asp:Label ID="lblTRN" runat="server"></asp:Label></strong>
                                </div>
                            </div>


                        </div>

                        <div class="col-xs-4 text-center hidden" style="border-right: 1px solid #ececec">
                            <h2 class="text-primary m-t-0 m-b-20"><strong>Sales Order</strong></h2>
                            <p class="m-b-0">{ Original For Recipient }</p>

                        </div>

                        <div class="col-xs-3" style="border-right: 1px solid #000; padding: 0; font-size: 12px; height: 145px;">
                            <p class="text-center m-b-0" style="min-height: 20px;">Quotation No: </p>
                            <p class="text-center m-b-0" style="border-bottom: 1px solid #000; min-height: 20px;">
                                <b>
                                    <asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" /></b>
                            </p>
                            <p class="text-center m-b-0" style="border-bottom: 1px solid #000; min-height: 20px;">Quotation Reference</p>
                            <div class="row">
                                <div class="col-xs-6 text-center" style="border-right: 1px solid #000; height: 85px;">
                                    <p class="text-center m-0">Validity: </p>
                                    <asp:Label ID="lblValidity" Text="#xx" runat="server" />
                                </div>
                                <div class="col-xs-6 text-center">
                                    <p class="text-center m-0">Completion: </p>
                                    <asp:Label ID="lblCompletion" Text="#xx" runat="server" />
                                </div>
                            </div>
                            <address style="padding-left: 25px;" class="hidden">
                                <strong>
                                    <asp:Label ID="lblLocName" runat="server" />location</strong><br />
                                <asp:Label ID="lblLocAddr1" runat="server" />address<br />
                                <asp:Label ID="lblLocAddr2" runat="server" />address<br />
                                <asp:Label ID="lblLocPhone" runat="server" />8888888
                            </address>
                        </div>

                        <div class="col-xs-3" style="padding-right: 10px; padding-left: 0; font-size: 12px; height: 145px;">
                            <div>
                                <p class="text-center m-b-0" style="min-height: 20px;">Date: </p>
                                <p class="text-center m-b-0" style="border-bottom: 1px solid #000; min-height: 20px;">
                                    <b>
                                        <asp:Label ID="lblDate" Text="Jun 15, 2015" runat="server" /></b>
                                </p>
                                <p class="m-t-10 hidden">
                                    <strong>Registration Id : </strong>
                                    <asp:Label ID="lblCompGst" Text="#xx" runat="server" />
                                </p>
                                <p class="text-center m-b-0">Payment Terms: </p>
                                <p class="text-center">
                                    <b>
                                        <asp:Label ID="lblPaymentTerms" runat="server"></asp:Label></b>
                                </p>
                            </div>
                        </div>
                    </div>
                    <%-- INVOICE ITEMS --%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Table ID="listTable" CssClass="table table-bordered" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>S.No</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell CssClass="hidden">Item Code</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Qty</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>Mrp</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Rate</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>VAT %</asp:TableHeaderCell>--%>
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
                    <div class="row">
                        <div class="col-xs-9 text-center" style="padding-right: 0">
                            <p style="margin-top: 71px; margin-bottom: 0; padding-bottom: 7px; border-bottom: 1px solid #000; border-top: 1px solid #000;">
                                <asp:Label ID="lblAmountinWords" runat="server" CssClass="amount-word">Eight Thousand One Hundred Ninety only</asp:Label>
                            </p>
                        </div>

                        <div class="col-xs-3" style="border-left: 1px solid black; font-size: 12px">

                            <div class="row" style="border-bottom: 1px solid #000">
                                <div class="col-xs-6 text-right">
                                    <span>Total :</span>
                                </div>
                                <div class="col-xs-6 text-right">
                                    <strong>
                                        <asp:Label ID="lblGross" Text="0.00" runat="server" /></strong>
                                </div>
                            </div>

                            <div class="row" style="border-bottom: 1px solid #000">
                                <div class="col-xs-6 text-right">
                                    <span>VAT 5% :</span>
                                </div>
                                <div class="col-xs-6 text-right">
                                    <strong>
                                        <asp:Label ID="lblTax" Text="0.00" runat="server" /></strong>
                                </div>
                            </div>

                            <div class="row" style="border-bottom: 1px solid #000">
                                <div class="col-xs-6 text-right">
                                    <span>Round Off :</span>
                                </div>
                                <div class="col-xs-6 text-right">
                                    <strong>
                                        <asp:Label ID="lblroundOff" Text="0.00" runat="server" /></strong>
                                </div>
                            </div>

                            <div class="row" style="border-bottom: 1px solid #000">
                                <div class="col-xs-6 text-right">
                                    <span>Deduction :</span>
                                </div>
                                <div class="col-xs-6 text-right">
                                    <strong>
                                        <asp:Label ID="lblDeduction" runat="server">0.00</asp:Label></strong>
                                </div>
                            </div>

                            <h4 class="text-right" style="border-bottom: 1px solid #000; margin: 0 0 0 -10px;">
                                <asp:Label ID="lblCurrency" Text="AED" runat="server" />&nbsp;
                                <asp:Label ID="lblNet" Text="0.00" runat="server" />
                            </h4>
                        </div>
                        <div class="col-xs-12 literal-control">
                            <h5 class="underline">Terms & Conditions</h5>
                            <asp:Literal ID="tAndC" runat="server"> 

                            </asp:Literal>
                        </div>
                        <div class="col-xs-6 text-center" style="border-right: 1px solid #ccc;">
                            <h4 class="m-t-20 hidden">For&nbsp;<asp:Label ID="lblCustomerName" Text="xx" runat="server" /></h4>
                            <div style="height: 65px;"></div>
                            <h6><b>RECEIVED BY</b></h6>
                        </div>
                        <div class="col-xs-6 text-center">
                            <h4 class="m-t-20 hidden">For&nbsp;<asp:Label ID="lblCompName" Text="xx" runat="server" /></h4>
                            <div style="height: 65px;"></div>
                            <h6><b>AUTHORISED SIGNATORY</b></h6>
                        </div>
                    </div>
                    <div class="row text-center">
                        <asp:Literal ID="lblPageFooter" runat="server">
                            <p class="bg-green m-t-10" style="margin-bottom: 3px;">Any query regarding this quotation should contact 0556203985 or 0551650330</p>
                            <p class="bg-blue" style="font-weight: 500;font-size: 20px;letter-spacing: 0.5px;background-color: #42A5F5;color: #fff;font-family: initial; margin-bottom: 0;">Thank You For Your Business With US!!!</p>
                            <p class="text-grey-light p-foot">* Exhibitions Stands * Singage * Printing * Acrylic * Joinery * Building Maintenance *</p>
                            <p class="text-grey-light p-foot">P.O. Box: 28138 ~ Ind.# 17, Sharjah, UAE ~ 06-5346707 ~ accounts@idesign-me.com</p>
                        </asp:Literal>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
