<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="BisellsERP.Purchase.Print.VAT.E.Quote" %>

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

        .amount-word {
            text-transform: uppercase;
            font-size: 15px;
            font-weight: 500;
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
            /*border-color: transparent;*/
            padding: 2px 4px;
        }

        .table tbody tr th {
            /*border-left-color: transparent !important;
            border-right-color: transparent !important;*/
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
                /*border-color: transparent !important;*/
                padding: 2px 4px;
            }

            .table tbody tr th {
                /*border-left-color: transparent !important;
                border-right-color: transparent !important;*/
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
                /*border: 1px solid #000;*/
                /*color: #000;*/
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

        .table-new {
            margin-bottom: 0px;
        }

        #lblComp {
            font-size: 20px;
        }

        .table-bordered-address {
            padding-left: 0px;
            border: 2px solid #c4bebe;
            padding-right: 0;
        }

        .table-responsive {
            min-height: 200px;
            height: 100%;
            border-left: 1px solid #000;
            border-right: 1px solid #000;
        }

        @page {
            /*margin-top: 1in;*/
        }
    </style>
</head>
<body>
    <div class="print-wrapper">
        <form id="form1" runat="server">
            <div class="row cambria-font">
                <div class="col-md-12 cambria-font">
                    <%-- INVOICE HEAD --%>
                    <div class="clearfix cambria-font">
                        <div class="col-xs-3 p-l-10 cambria-font">
                            <h4>
                                <asp:Image ID="imgLogo"  runat="server" Height="75px" />
                            </h4>

                        </div>
                        <div class="col-xs-9 text-right cambria-font">
                            <h4 class="m-b-0 m-t-40 cambria-font" style="color: #0e4679;">
                                <strong>
                                    <asp:Label ID="lblMainHead" runat="server">LOCAL PURCHASE ORDER</asp:Label>
                                </strong>

                            </h4>
                            <%--<p class="m-b-0 m-t-10 pull-right"><asp:Label ID="lblPaymentStatus" runat="server">Paid</asp:Label></p>--%>
                        </div>
                        <div class="col-xs-12 p-b-10 cambria-font">
                            <div class="col-xs-7 cambria-font">
                                <address class=" cambria-font">
                                    <strong>
                                        <asp:Label ID="lblComp" Text="Company1" runat="server" /></strong><br />
                                    <label>Address:</label>
                                    <asp:Label ID="lblCompAddr1" runat="server" /><br />
                                    <asp:Label ID="lblCompAddr2" runat="server" /><br />
                                    Ph :
                                <asp:Label ID="lblCompPh" runat="server" /><br />
                                    E-mail :
                                <asp:Label ID="lblCompEmail" CssClass="text-primary" runat="server" /><br />

                                </address>

                            </div>
                            <div class="col-xs-3 text-right cambria-font">
                                <div>
                                    <label><strong>DATE:</strong></label>
                                    <br />
                                    <label><strong>LPO NO: </strong></label>
                                    <br />
                                    <label><strong>LPO Ref: </strong></label>
                                    <br />
                                    <label><strong>TRN: </strong></label>

                                </div>


                            </div>
                            <div class="col-xs-2 table-bordered-address text-center cambria-font">
                                <label>
                                    <strong>
                                        <asp:Label Text="25-01-2018" ID="lblDate" runat="server" /></strong></label><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" /></strong></><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="lblEmail" Text="#xx" runat="server" /></strong></><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="lblTRN" runat="server"></asp:Label>
                                </strong></><br />


                            </div>
                        </div>
                        <%--    <div class="col-xs-12">
                            <h5 class="text-primary m-t-0 m-b-20 m-r-10 pull-right ">
                                <asp:Label ID="lblPaymentStatus" runat="server">asdasd</asp:Label></h5>
                        </div>--%>
                    </div>
                    <%-- INVOICE DETAILS --%>
                    <div class="row hidden" style="margin-top: 15px;">
                        <div class="col-xs-6" style="border-right: 1px solid #ececec">
                            <strong>Project :</strong><asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            <br />

                            <strong>TRN# :</strong>
                        </div>
                        <div class="col-xs-4 text-center hidden" style="border-right: 1px solid #ececec">
                            <h2 class="text-primary m-t-0 m-b-20"><strong>QUOTATION</strong></h2>
                            <p class="m-b-0">{ Original For Recipient }</p>

                        </div>
                        <div class="col-xs-3" style="border-right: 1px solid #ececec; height: 120px;">
                            <strong>Quotation No: </strong>
                            <%--<asp:Label ID="lblInvoiceNo" Text="#xx" runat="server" />--%>
                            <br />
                            <strong class="hidden">LPO No :</strong>
                            <asp:Label ID="lblLPONumber" CssClass="hidden" runat="server"></asp:Label><br />
                            <strong>Quote Reference:</strong>
                            <asp:Label ID="lblQuoteRef" runat="server"></asp:Label>
                            <hr class="m-b-0 m-t-5" />
                            <address style="padding-left: 25px;" class="hidden">
                                <strong>
                                    <%--<asp:Label ID="lblLocName" runat="server" />location</strong><br />--%>
                                    <asp:Label ID="label1" runat="server" /><br />
                                    <asp:Label ID="label2" runat="server" /><br />
                                    <asp:Label ID="lblLocPhone" runat="server" />
                            </address>
                            <div class="col-xs-12">
                                <div class="col-xs-6 text-center" style="border-right: 1px solid #ececec; height: 55px;">
                                    <strong>Validity</strong>
                                    <br />
                                    <asp:Label ID="lblValidity" runat="server"></asp:Label>
                                </div>
                                <div class="col-xs-6 text-center">
                                    <strong>Completion</strong>
                                    <br />
                                    <asp:Label ID="lblCompletion" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="co-xs-3 text-right" style="padding-right: 20px;">
                            <div style="border-left: 1px solid black">
                                <p>
                                    <strong>Date: </strong>
                                    <%--<asp:Label ID="lblDate" Text="Jun 15, 2015" runat="server" />--%>
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

                                </p>
                            </div>
                        </div>
                    </div>


                    <%-- INVOICE ITEMS --%>
                    <div class="row cambria-font">
                        <div class="col-md-12 cambria-font">
                            <table class="table table-bordered table-new cambria-font">
                                <thead>
                                    <tr>
                                        <td class="text-center">Supplier Details</td>
                                        <td class="text-center">Delivery Address</td>
                                        <td class="text-center">Delivery Date</td>
                                        <td class="text-center">Payment Terms</td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="text-center">


                                            <address style="padding-left: 35px;">
                                                <asp:Label Text="" ID="lblSupName" runat="server" /><br />
                                                <asp:Label Text="" ID="lblSupplierAddress" runat="server" /><br />
                                                <asp:Label ID="lblSupPhone" runat="server" /><br />
                                                TRN No:<asp:Label ID="lblSupTRN" runat="server" /><br />
                                            </address>
                                        </td>
                                        <td class="text-center">
                                            <asp:Label Text="text" ID="lblLocName" runat="server" /><br />
                                            <asp:Label ID="lblLocAddr1" runat="server" /><br />
                                            <asp:Label ID="lblLocAddr2" runat="server" /><br />
                                        </td>
                                        <td class="text-center">
                                            <asp:Label Text="text" ID="lblDueDate" runat="server" />
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblPaymentTerms" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="table-responsive cambria-font">
                                <asp:Table ID="listTable" CssClass="table table-bordered cambria-font" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell CssClass="text-center" style="width: 12.4%">Item Code</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell CssClass="hidden">Item Code</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell CssClass="text-center" style="width: 40.5%">Description</asp:TableHeaderCell>
                                        <asp:TableHeaderCell CssClass="text-center" style="width: 15.2%">Unit</asp:TableHeaderCell>
                                        <asp:TableHeaderCell CssClass="text-center" style="width: 7%">Qty</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>Mrp</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell CssClass="text-center" style="width: 10.5%">Rate</asp:TableHeaderCell>
                                        <%--<asp:TableHeaderCell>VAT %</asp:TableHeaderCell>--%>
                                        <%--<asp:TableHeaderCell>Gross</asp:TableHeaderCell>--%>
                                        <%--<asp:TableHeaderCell>Tax </asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell CssClass="text-center">Amount</asp:TableHeaderCell>
                                    </asp:TableRow>

                                </asp:Table>
                            </div>
                            <table class="table table-bordered cambria-font">
                                <tbody>
                                    <tr>
                                        <td class="text-right" style="width: 85%">Total&nbsp;(&nbsp;<asp:Label ID="lblCurrency2" Text="AED" runat="server" />&nbsp;)
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblGross" Text="0.00" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right">5% VAT
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblTax" Text="0.00" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="text-right">GRAND TOTAL&nbsp;(&nbsp;<asp:Label ID="lblCurrency" Text="AED" runat="server" />&nbsp;)
                                        </td>
                                        <td class="text-center">
                                            <asp:Label ID="lblNet" Text="0.00" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="col-xs-6 text-center">
                                                <span style="margin-left: 4em">Approved signatory</span>
                                            </div>
                                            <div class="col-xs-6 text-center">
                                                <span class="" style="margin-right: 10em">Procurement Officer</span>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>


                    <%-- INVOICE SUMMARY --%>
                    <div class="row cambria-font">
                        <div class="col-xs-6 text-center cambria-font">
                            <img height="145" src="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/4RDsRXhpZgAATU0AKgAAAAgABAE7AAIAAAALAAAISodpAAQAAAABAAAIVpydAAEAAAAWAAAQzuocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE1hY2xpbmtQQzIAAAAFkAMAAgAAABQAABCkkAQAAgAAABQAABC4kpEAAgAAAAM3OQAAkpIAAgAAAAM3OQAA6hwABwAACAwAAAiYAAAAABzqAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMjAxODowMzowMSAxMzo1Nzo1MwAyMDE4OjAzOjAxIDEzOjU3OjUzAAAATQBhAGMAbABpAG4AawBQAEMAMgAAAP/hCx1odHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+DQo8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIj48cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSJ1dWlkOmZhZjViZGQ1LWJhM2QtMTFkYS1hZDMxLWQzM2Q3NTE4MmYxYiIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIi8+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPjx4bXA6Q3JlYXRlRGF0ZT4yMDE4LTAzLTAxVDEzOjU3OjUzLjc4NzwveG1wOkNyZWF0ZURhdGU+PC9yZGY6RGVzY3JpcHRpb24+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iPjxkYzpjcmVhdG9yPjxyZGY6U2VxIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpsaT5NYWNsaW5rUEMyPC9yZGY6bGk+PC9yZGY6U2VxPg0KCQkJPC9kYzpjcmVhdG9yPjwvcmRmOkRlc2NyaXB0aW9uPjwvcmRmOlJERj48L3g6eG1wbWV0YT4NCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgPD94cGFja2V0IGVuZD0ndyc/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ACiiigAooooAKKKKACuJ+LxJ+G13GSVjmubWKY448triMNn2wTXbVkeKtBh8UeFNR0W4bYl5AYw+M7G6q34MAfwq4O0k2BrDpxS1yHgXxZJqsEmia+Ba+JdLUR31s3HmgcCaP+8jdcjoTg119S007MAooopAFcv491ltO8OtZWXz6pqrfYrGIAEtI4xuI/uqMsT2ArS8R+JdN8L6S19qs2xc7YokG6Sdz0RF6sxPYVieGNEv9Q1k+LPFMSx6hJD5VlZgcWEJ5IPrI3G4+2B3ropRUf3stlt5v+txPsRXfhfXr6S00KPUV03wxZ2kcLtasftV5hdpQsR+7XA6rknPUdur0vS7HRdPisNKtYrS1iGEiiXaB/8AX96t4orKU5S0YwoooqACiiigAooooAKKKKACiiigAooooAKKKKACiiigAooooAKKKKAOe8UeCtK8VrDLeCW1v7Xm11G0fy7i3P8AsuO3scg+lYqp8StB2xxto/iq2TgPMzWNyfrgNGfrgfSu7oquZ7AcaviTxuY/m8BIJPQa1EV/Pb/SoG1D4k6swhtdE0jw9Hkh7m8vTePj1WNAoz9WruaKFK3QDktD8A21jqq61r19Pr+trkJeXYAWAHtFGPljH0yfeutooolJy3AKKKKkAooooAKKKKACiiigAooooAKKKKACiiigD//Z" />
                        </div>
                        <div class="col-xs-6 text-center cambria-font">
                            <img height="145" src="data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/4RCURXhpZgAATU0AKgAAAAgABAE7AAIAAAALAAAISodpAAQAAAABAAAIVpydAAEAAAAWAAAQduocAAcAAAgMAAAAPgAAAAAc6gAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAE1hY2xpbmtQQzIAAAAB6hwABwAACAwAAAhoAAAAABzqAAAACAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABNAGEAYwBsAGkAbgBrAFAAQwAyAAAA/+EKY2h0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8APD94cGFja2V0IGJlZ2luPSfvu78nIGlkPSdXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQnPz4NCjx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iPjxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+PHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9InV1aWQ6ZmFmNWJkZDUtYmEzZC0xMWRhLWFkMzEtZDMzZDc1MTgyZjFiIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iLz48cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0idXVpZDpmYWY1YmRkNS1iYTNkLTExZGEtYWQzMS1kMzNkNzUxODJmMWIiIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyI+PGRjOmNyZWF0b3I+PHJkZjpTZXEgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj48cmRmOmxpPk1hY2xpbmtQQzI8L3JkZjpsaT48L3JkZjpTZXE+DQoJCQk8L2RjOmNyZWF0b3I+PC9yZGY6RGVzY3JpcHRpb24+PC9yZGY6UkRGPjwveDp4bXBtZXRhPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICA8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAHBQUGBQQHBgUGCAcHCAoRCwoJCQoVDxAMERgVGhkYFRgXGx4nIRsdJR0XGCIuIiUoKSssKxogLzMvKjInKisq/9sAQwEHCAgKCQoUCwsUKhwYHCoqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioqKioq/8AAEQgAwADLAwEiAAIRAQMRAf/EAB8AAAEFAQEBAQEBAAAAAAAAAAABAgMEBQYHCAkKC//EALUQAAIBAwMCBAMFBQQEAAABfQECAwAEEQUSITFBBhNRYQcicRQygZGhCCNCscEVUtHwJDNicoIJChYXGBkaJSYnKCkqNDU2Nzg5OkNERUZH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" />
                        </div>

                        <div class="col-xs-12 cambria-font">
                            <table class="table  table-bordered cambria-font">
                                <tr>
                                    <td class="text-center cambria-font">
                                        <u>Terms & Conditions</u>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Literal ID="tAndC" runat="server"> 
                                <ol>
                                    <li>sdfasdfasdf</li>
                                    <li>asdfsfd</li>
                                    <li>asdfsfd</li>
                                    <li>asdfsfd</li>
                                    <li>asfdsdf</li>
                                    <li>asfdsdf</li>
                                </ol>
                                        </asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="row m-t-15 hidden">

                        <div class="col-xs-8">
                            <p style="margin-top: 85px" class="text-center">
                                <asp:Label ID="lblAmountinWords" runat="server" CssClass="amount-word">Eight Thousand One Hundred Ninety only</asp:Label>
                            </p>
                            <hr style="margin-top: 37px;" />
                        </div>
                        <div class="col-xs-4 pull-right" style="border-left: 1px solid black">
                            <p class="text-right m-b-0">
                                <b>Total Gross: </b>

                            </p>
                            <p class="text-right m-0">
                                Total Tax:
                               
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


                                <hr class="m-b-0" />
                        </div>
                        <div class="col-xs-12">
                            <div class="col-xs-6">
                            </div>
                            <div class="col-xs-6 text-center pull-right">
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

                </div>
            </div>
        </form>
    </div>
</body>
</html>
