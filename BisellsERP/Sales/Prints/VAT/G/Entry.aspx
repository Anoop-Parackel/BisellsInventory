<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.G.Entry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />

    <style>
        body {background-color: rgb(204,204,204);border: 1px solid #000;}

        .print-wrap {
            width: 21cm;
            min-height: 29.7cm;
            margin: 10px auto;
            padding: 15px;
            background-color: #fff;
            
        }

            .print-wrap p {margin: 0}
            address {margin:0}

        .bb-dashed {border-bottom: 1px dashed #ccc}
        .br-dashed {border-right: 1px dashed #ccc}
        .b-all-dashed {border: 1px dashed #ccc}
        span.inv-info-title {
            display: inline-block;
            width: 75px;
            margin-right: 5px;
        }
        .inv-comp-title {
            display: inline-block;
            width: 50px;
            margin-right: 5px;
        }
        .fs-12 {
            font-size: 12px;
        }
        tr.inv-footer td {
            border-color: #fff !important;
            padding: 0 4px !important;
        }
        tr.inv-footer.first td {
            border-top: 1px dashed #ddd !important;
        }
        #listTable > tbody > tr > th {
            border: none ;
            border-bottom: 1px dashed #ddd;
            font-size: 12px;
            padding: 2px 4px;
        }
         #listTable > tbody > tr > td {
            border: none ;
            border-right: 1px dashed #ddd;
            font-size: 12px;
        }
         #listTable > tbody > tr > td:last-child {
            border: none ;
        }


        @media print {
            body {background-color: #fff;border-color}
            .print-wrap{padding: 0}
        }

        @page {
            
        }

    </style>
</head>
<body>
    <div class="print-wrap">
        <form id="form1" runat="server">

            <div class="row bb-dashed fs-12">
                <div class="col-xs-4">
                    <address class="small-font">
                        <p><strong><asp:Label ID="lblComp" runat="server" /></strong></p>
                        <asp:Label ID="lblCompAddr1" runat="server" /><br />
                        <asp:Label ID="lblCompAddr2" runat="server" /><br />
                        <p><strong>CIN : </strong><asp:Label ID="lblCompanyTaxNo1" runat="server">74859685265</asp:Label></p>
                    </address>
                </div>
                <div class="col-xs-4">

                </div>
                <div class="col-xs-4">
                    <p><span class="inv-comp-title">Tel<span class="pull-right">:</span></span><asp:Label runat="server" ID="lblCompanyPhone1" /></p>
                    <p><span class="inv-comp-title">Mob<span class="pull-right">:</span></span><asp:Label ID="lblCompanyPhone2" runat="server" /></p>
                    <p><strong class="inv-comp-title">GST<span class="pull-right">:</span></strong><asp:Label runat="server" ID="lblCompanyTaxNo2" /></p>
                    <p><span class="inv-comp-title">Email<span class="pull-right">:</span></span><asp:Label ID="lblCompEmail" CssClass="text-primary" runat="server" /></p>
                    <p><span class="inv-comp-title">Web<span class="pull-right">:</span></span><asp:Label runat="server" ></asp:Label></p>
                </div>
            </div>

            <div class="row bb-dashed fs-12">
                <div class="col-xs-6">
                    <p><strong>To</strong></p>
                    <address class="m-l-15">
                        <p><asp:Label ID="lblCustomer" runat="server" /></p>
                        <p><asp:Label ID="lblCustomerAddress1" runat="server" /></p>
                        <p><asp:Label ID="lblCustomerAddress2" runat="server" /></p>
                        <p>Ph :<asp:Label runat="server" ID="lblCustPh" /></p>
                        <div>E-mail :<asp:Label CssClass="text-primary" runat="server" ID="lblCustEmail" /><br />
                        </div>
                        <p>Customer TRN :<asp:Label ID="lblCustTRN" runat="server" ClientIDMode="Static"></asp:Label></p>
                    </address>
                </div>
                <div class="col-xs-6">
                    <p><strong>Billing Address</strong></p>
                    <address class="m-l-15">
                    </address>
                </div>
            </div>

            <div class="row fs-12">
                <div class="col-xs-12">
                    <h5 class="text-center"><strong>TAX INVOICE-(Credit)  B-B</strong></h5>
                </div>
                <div class="col-xs-3">
                    <p><span class="inv-info-title">S.Id<span class="pull-right">:</span></span><strong><asp:Label runat="server" ClientIDMode="Static" ID="lblSalesID">11,059</asp:Label></strong></p>
                    <p><span class="inv-info-title">SR No<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static">24,344</asp:Label></p>
                    <p><span class="inv-info-title">SR Date<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static"></asp:Label></p>
                </div>
                <div class="col-xs-3">
                    <p><span class="inv-info-title">Rep<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblSalesRep">CO</asp:Label></p>
                    <p><span class="inv-info-title">Payment<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblPayment">30 Days</asp:Label></p>
                    <p><span class="inv-info-title">Despatch<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblDespatch">Auto Althaf</asp:Label></p>
                </div>
                <div class="col-xs-3">
                    <p><span class="inv-info-title">Desp Date<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblDespatchDate">29/03/2018</asp:Label></p>
                    <p><span class="inv-info-title">Cartons<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblCartons">0</asp:Label></p>
                    <p><span class="inv-info-title">Freight<span class="pull-right">:</span></span><asp:Label runat="server" ClientIDMode="Static" ID="lblFrieghtAmount">None</asp:Label></p>
                </div>
                <div class="col-xs-3">
                    <p><span class="inv-info-title">DIVISION<span class="pull-right">:</span></span><strong><asp:Label runat="server" ClientIDMode="Static" ID="lblDivision">TS</asp:Label></strong></p>
                    <p><span class="inv-info-title">Invoice No<span class="pull-right">:</span></span><strong><asp:Label ID="lblInvoiceNo" runat="server" ClientIDMode="Static"></asp:Label></strong></p>
                    <p><span class="inv-info-title">Date<span class="pull-right">:</span></span><strong><asp:Label ID="lblDate" runat="server" ClientIDMode="Static"></asp:Label></strong></p>
                </div>
            </div>

            <div class="row bb-dashed b-all-dashed">
                <div class="col-xs-12">
                    <div class="min-table-height">
                        <asp:Table ID="listTable" CssClass="table" runat="server">
                            <asp:TableRow>
                                <asp:TableHeaderCell>#</asp:TableHeaderCell>
                                <asp:TableHeaderCell Style="width: 50%">Description</asp:TableHeaderCell>
                                <asp:TableHeaderCell Style="text-align: right">Qty</asp:TableHeaderCell>
                                <asp:TableHeaderCell Style="text-align: right">Unit</asp:TableHeaderCell>
                                <asp:TableHeaderCell Style="text-align: right" ID="thRate">Rate</asp:TableHeaderCell>
                                <asp:TableHeaderCell Style="text-align: right" ID="thTotal">Total</asp:TableHeaderCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </div>
            </div>

            <div class="row bb-dashed p-t-5 p-b-5">
                <div class="col-xs-8 br-dashed">
                    <p>Rupees : <asp:Label ID="lblWords" runat="server" ClientIDMode="Static"></asp:Label></p>
                </div>
                <div class="col-xs-4">
                    <p><strong>Total</strong><asp:Label runat="server" ClientIDMode="Static" CssClass="pull-right" ID="lblNetTotal">12,400.00</asp:Label></p>
                </div>
            </div>
            <div class="row ">
                <div class="col-xs-8 br-dashed">
                    <p><b>Terms and Conditions</b></p>
                    <asp:Label ID="lblTerms" runat="server" ClientIDMode="Static"></asp:Label>
                </div>
                <div class="col-xs-4">
                    <h5 class="text-center">For <asp:Label ID="lblCompanySign" runat="server">Company Name</asp:Label></h5>
                    <h5 class="text-center m-t-40">Authorised Signatory</h5>
                </div>
            </div>


            <%-- END --%>




            <div class="hidden">

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
                        <h5 class="m-t-25"><strong>Job : <asp:Label id="lblJob" runat="server" ClientIDMode="Static">Translation Agreement Process</asp:Label></strong></h5>
                    </div>
                    <div class="col-xs-6">
                        <h5 class="text-right m-b-0"><strong>Date : </strong></h5>
                        <h5 class="text-right m-t-0"><strong>Invoice No : </strong></h5>
                    </div>
                </div>


                <%-- INVOICE ITEMS --%>


                <div class="row">
                    <div class="col-xs-12">
                        <h5 class="text-right m-t-0">In Words: </h5>
                    </div>
                </div>

                <hr class="m-0" />



                <div class="row m-t-10">
                    <div class="col-xs-12">
                        <p class="m-0"><strong>Terms & Conditions</strong></p>
                        <p>
                        </p>
                    </div>
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
