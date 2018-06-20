<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.G.Quote" %>

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
        .print-wrapper {
            padding: 1em 1em;
            font-family: calibiri !imporant;
            color: #000 !important;
        }
        .min-table-height {
            /*min-height: 350px;*/
        }
        address.small-font {
            font-size: 12px;
        }
        .inv-type {
            margin: 0;
        }

            .inv-type span {
                display: inline-block;
                border: 2px solid #000;
                padding: 10px 20px;
                color: #000;
                font-size: 24px;
                font-weight: 700;
            }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            border-color: transparent; 
        }

        .table tbody tr th {
            background: #323A45 !important;
            color: white !important;
            text-align: center;
            font-size: 14px;
            font-weight: 100;
            padding: 2px;
        }

        tr.inv-footer.beige {
            background-color: #f0f0f0;
        }

        tr.inv-footer > td {
            padding: 2px !important;
            font-size: 12px;
            font-weight: 100;
        }

        @media print {
            .print-wrapper {
                padding: 0em;
                font-family: calibiri !important;
                color: #000 !important;
                height: 15cm !important;
            }

            hr {
                border-top: 1px solid #000 !important;
            }

            .inv-type span {
                border: 2px solid #000 !important;
                color: #000 !important;
            }

            address.small-font {
                font-size: 12px;
            }

            .table tbody tr th {
                background: #323A45 !important;
                color: white !important;
                text-align: center;
                font-size: 14px;
                font-weight: 100;
                padding: 2px;
            }

            tr.footer.beige {
                background-color: #f0f0f0 !important;
            }
        }

        @page {
            
        }

    </style>
</head>
<body>
    <div class="print-wrapper">
        <form id="form1" runat="server">
            <div class="">

                <div class="row">
                    <div class="col-xs-6">
                        <asp:Image ID="imgLogo" runat="server" Height="60px" />
                    </div>
                    <div class="col-xs-6">
                        <h4 class="text-right inv-type">
                            <asp:Label ID="lblMainHead" runat="server">Tax Invoice</asp:Label>
                        </h4>
                        <p class="m-0 text-right"><small>TRN : <asp:label ID="lblCompRegNo" runat="server" ClientIDMode="Static"></asp:label></small></p>
                    </div> 
                </div>

                <div class="row">
                    <div class="col-xs-6">
                        <address class="small-font">
                            <h5><strong><asp:Label ID="lblComp" runat="server" /></strong></h5>
                            <asp:Label ID="lblCompAddr1" runat="server" /><br />
                            <asp:Label ID="lblCompAddr2" runat="server" /><br />
                            Ph :<asp:Label ID="lblCompPh" runat="server" /><br />
                            E-mail :<asp:Label ID="lblCompEmail" CssClass="text-primary" runat="server" /><br />
                        </address>
                    </div>
                    <div class="col-xs-6 text-right">
                        <address class="small-font">
                            <h5><strong>Bill To</strong></h5>
                            <asp:Label ID="lblCustomer" runat="server" /><br />
                            <asp:Label ID="lblCustomerAddress1" runat="server" /><br />
                            <asp:Label ID="lblCustomerAddress2" runat="server" /><br />
                            Ph :<asp:Label runat="server" id="lblCustPh"/><br />
                            E-mail :<asp:Label CssClass="text-primary" runat="server" id="lblCustEmail"/><br />
                            Customer TRN : <asp:label ID="lblCustTRN" runat="server" ClientIDMode="Static"></asp:label>
                        </address>

                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-6">
                        <h5 class="m-t-25"><strong>Job : <asp:Label id="lblJob" runat="server" ClientIDMode="Static">Translation Agreement Process</asp:Label></strong></h5>
                    </div>
                    <div class="col-xs-6">
                        <h5 class="text-right m-b-0"><strong>Date : <asp:Label CssClass="text-danger" ID="lblDate" runat="server" ClientIDMode="Static"></asp:Label></strong></h5>
                        <h5 class="text-right m-t-0"><strong>Invoice No : <asp:Label CssClass="text-danger" ID="lblInvoiceNo" runat="server" ClientIDMode="Static"></asp:Label></strong></h5>
                    </div>
                </div>


                <%-- INVOICE ITEMS --%>
                <div class="row">
                    <div class="col-md-12">
                        <div class="min-table-height">
                            <asp:Table ID="listTable" CssClass="table" runat="server">
                                <asp:TableRow>
                                    <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="width: 50%">Description</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="text-align:right">Qty</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="text-align:right">Unit</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="text-align:right" ID="thRate">Rate</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Style="text-align:right" ID="thTotal">Total</asp:TableHeaderCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-12">
                        <h4 class="text-right m-t-0">In Words: <asp:Label ID="lblWords" runat="server" ClientIDMode="Static"></asp:Label></h4>
                    </div>
                </div>

                <hr class="m-0" />



                <div class="row m-t-10">
                    <div class="col-xs-12">
                        <p class="m-0"><strong>Terms & Conditions</strong></p>
                        <p>
                           <asp:Label ID="lblTerms" runat="server" ClientIDMode="Static"></asp:Label>
                        </p>
                    </div>
                </div>

                <div class="row hidden">
                    <div class="col-xs-12">
                        <p class="m-0"><strong>Bank Details</strong></p>
                        <p>
                            Bank Details here
                        </p>
                    </div>
                </div>



                <div class="row">
                    <div class="col-xs-6"></div>
                    <div class="col-xs-6"></div>
                </div>






                <div class="col-md-12">
                    <%-- INVOICE HEAD --%>
                    <div class="clearfix hidden">
                        <div class="col-xs-8 p-l-10">
                            <div class="col-xs-12 text-left m-t-min ">
                                
                            </div>

                        </div>
                        <div class="col-xs-4 text-right">
                            <h3 class=" m-t-40 m-b-0" style="color: #0e4679;"><strong>
                                </strong>
                            </h3>
                        </div>
                        <div class="col-xs-12 p-b-10">
                            <div class="col-xs-7">
                                <div class="col-xs-4" style="padding-left: 0;">
                                    <article>
                                        <h4 class="bg-dark-blue" style="color: white; background-color: #0e4679">BILL TO</h4>
                                        <%--<strong>
                                           <%-- <asp:Label Text="Anu" ID="" runat="server" /></strong>--%>
                                      <%--  <br />
                                        <strong>--%>
                                          <%--  <asp:Label Text="India" ID="" runat="server" /></strong>--%>
                                    </article>
                                </div>

                            </div>
                            <div class="col-xs-3 text-right">
                                <div>
                                    <label><strong>DATE:</strong></label>
                                    <br />
                                    <label><strong>INVOICE# </strong></label>
                                    <br />
                                    <label><strong>CUSTOMER ID</strong></label>
                                    <label><strong>DUE DATE</strong></label>
                                </div>
                            </div>
                         <%--   <div class="col-xs-2  table-bordered-address text-center ">
                                <label>
                                    <strong>
                                        <asp:label text="25-01-2018" id="" runat="server" /></strong></label><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="" Text="#xx" runat="server" /></strong></><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="lblCustName" runat="server" /></strong></><br />
                                <hr class="m-0  hr-white" />
                                <strong>
                                    <asp:Label ID="lblDuedate" runat="server" /></strong></><br />
                            </div>--%>
                        </div>

                    </div>



                    <%-- INVOICE DETAILS --%>
                    <div class="row hidden" style="margin-top: 15px;">
                        <div class="col-xs-6" style="border-right: 1px solid #ececec">
                            <strong>Project :</strong><asp:Label ID="lblProjectName" runat="server"></asp:Label>
                            <br />
                            <strong>Contact :</strong>
                            <address style="padding-left: 35px;">
                                <strong></strong>
                                <br />
                                <asp:Label ID="lblCustPhone" runat="server" /><br />
                            </address>
                            <strong>TRN# :</strong><asp:Label ID="lblTRN" runat="server"></asp:Label>
                        </div>
                        <div class="col-xs-4 text-center hidden" style="border-right: 1px solid #ececec">
                            <h2 class="text-primary m-t-0 m-b-20"><strong>Tax Invoice</strong></h2>
                            <p class="m-b-0">{ Original For Recipient }</p>

                        </div>
                        <div class="col-xs-3" style="border-right: 1px solid #ececec; height: 120px;">
                            <strong>Invoice No: </strong>

                            <br />
                            <strong>LPO No :</strong>
                            <asp:Label ID="lblLPONumber" runat="server"></asp:Label><br />
                            <strong>Quote Ref:</strong>
                            <asp:Label ID="lblQuoteRef" runat="server"></asp:Label>
                            <address style="padding-left: 25px;" class="hidden">

                                <strong>
                                    <asp:Label ID="lblLocName" runat="server" />location</strong><br />
                                <asp:Label ID="lblLocAddr1" runat="server" />address<br />
                                <asp:Label ID="lblLocAddr2" runat="server" />address<br />
                                <asp:Label ID="lblLocPhone" runat="server" />8888888
                            </address>
                        </div>
                        <div class="co-xs-3 text-right" style="padding-right: 20px;">
                            <div>
                                <p>
                                    <strong>Date: </strong>
                                </p>
                                <p class="m-t-10">
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


                    <%-- INVOICE SUMMARY --%>
                    <div class="row m-t-15">
                        <div class="col-xs-8 text-center hidden">
                            <p style="margin-top: 85px">
                                <asp:Label ID="lblAmountinWords" runat="server" CssClass="amount-word">Eight Thousand One Hundred Ninety only</asp:Label>
                            </p>
                            <hr style="margin-top: 37px;" />
                        </div>
                        <div class="col-xs-4 pull-right hidden" style="border-left: 1px solid black">
                            <p class="text-right m-b-0">
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
                            </h3>
                            <hr class="m-b-0" />
                        </div>
                        <div class="col-xs-12 hidden">
                            <table class="table table-bordered">
                                <thead style="background: #0e4679;">
                                    <tr>
                                        <td style="color: white;">OTHER COMMENTS
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div class="col-xs-6">
                                                <asp:Literal ID="tAndC" runat="server"> 
                                <ol >
                                  <li>Termination of using or accessing your website</li>
                                  <li>Disclosure to inform country laws</li>
                                  <li>Contact details to inform users how they contact you with questions</li>
                               </ol>
                                                </asp:Literal>
                                            </div>
                                            <div class="col-md-6">
                                            </div>

                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                            <small>If you have queries about this invoice,please contact:</small><br />
                            <label class="text-primary">Mob:</label>
                            <asp:Label ID="lblCompanyPhone" runat="server" />
                            <label class="text-primary">Email:</label>
                            <asp:Label ID="lblCompanyEmail" CssClass="text-primary" runat="server" /><br />
                        </div>
                    </div>

                </div>
            </div>
        </form>
    </div>
</body>
</html>
