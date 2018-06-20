Date: <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.A.Entry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Print</title>
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
            background-color: transparent
        }
        table tbody tr th, table tbody tr td {
            font-size: 10px;
        }

        .print-wrapper {
            /*padding: 2em 4em;*/
            padding: 2em 1em;
font-family: calibiri !imporant;
color : #000 !important;
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
.table-bordered>tbody>tr>td, .table-bordered>tbody>tr>th, .table-bordered>tfoot>tr>td, .table-bordered>tfoot>tr>th, .table-bordered>thead>tr>td, .table-bordered>thead>tr>th {
    border: 1px solid #000;
    color: #000;
}
.inv-head {
    border: 1px solid #000;
    padding: 0;
    margin: 0px 10px 0;}
.table-bordered {
    border: 1px solid #000;
}
.table tbody tr td {border-color:transparent;padding:2px 4px}
.table tbody tr th {border-left-color:transparent !important;border-right-color:transparent !important}
.table tbody tr > td:nth-of-type(n+4) {text-align: right}
.table tbody tr > th:nth-of-type(n+4) {text-align: right}

        @media print {
            .print-wrapper {
                padding: 0em;
font-family: calibiri !imporant;
color : #000 !important;
height: 15cm !important;
            }
.table-bordered th,
.table-bordered td {
  border: 1px solid #000 !important;
}
.table tbody tr td {border-color:transparent !important;padding:2px 4px}
.table tbody tr th {border-left-color:transparent !important;border-right-color:transparent !important}
.table tbody tr > td:nth-of-type(n+4) {text-align: right}
.table tbody tr > th:nth-of-type(n+4) {text-align: right}
  table tbody tr th, table tbody tr td {
            font-size: 10px;
        }
.table {
    border: 1px solid #000 !imporant;
}

.table-bordered>tbody>tr>td, .table-bordered>tbody>tr>th, .table-bordered>tfoot>tr>td, .table-bordered>tfoot>tr>th, .table-bordered>thead>tr>td, .table-bordered>thead>tr>th {
    border: 1px solid #000;
    color: #000;
}
.inv-head {
    border: 1px solid #000;
    padding: 0;
    margin: 0px 10px 0;}

        hr {
            border-color: #000;
        }
.right-dark-border {
border-right: 1px solid #000; height:100px}
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
                <div class="col-md-12 inv-head">
                    <%-- INVOICE HEAD --%>
                    <div class="clearfix" style="display:none">
                        <div class="col-xs-3">
                            <h4>
                                 <asp:Image ID="imgLogo" runat="server" width ="100px" Height="40px"/>
                            </h4>
                        </div>
                        <div class="col-xs-5 text-center hidden">
                            <h2 class="text-primary m-t-30"><strong>Sales Invoice</strong></h2>
                            
                        </div>
                        <div class="col-xs-12 text-center">
                            <address class="">
                                <strong>  
                                    <asp:Label ID="lblComp" runat="server" /></strong><br />
                               <asp:Label ID="lblCompAddr1" runat="server" /><br />
                                <asp:Label ID="lblCompAddr2" Text="trivandrum" runat="server" /><br />
                                E-mail :
                                <asp:Label ID="lblCompEmail"  runat="server" /><br />
                                Ph :
                                <asp:Label ID="lblCompPh" runat="server" />
                            </address>
                        </div>

                    </div>
                    <hr class="m-0" style="display:none" />
                 <%-- INVOICE DETAILS --%>
                    <div class="row">
                        <div class="col-xs-4" style="height:90px; font-size: 10px; margin-left: 5px;">
                            <label class="control-label m-t-5" style="font-size: 10px"><strong>To</strong></label>
                            <address style="padding-left: 25px;">
                                 <asp:Label ID="lblCustName" runat="server" /><br />
                                      <asp:Label ID="lblAddress" runat="server" /><br />
                                 <asp:Label ID="lblCustPhone" runat="server" /><br />
                                 <strong>
                                TRN No : 
                                      </strong>
                               <asp:Label ID="lblTfn" runat="server" /><br />
                             
                             
                             </address>
                        </div>
                        <div class="col-xs-4 text-center" style="height:90px; font-size: 10px">
                            <h3 class="text-primary m-t-30"><strong>Sales Invoice</strong></h3>

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
                        <div class="col-xs-4" style="padding-right: 20px; font-size: 12px">
                         <p style="margin: 0 !important;"><strong>Date: </strong>
                                <asp:Label ID="lblDate" Text="" runat="server" /></p>
                            <p class="" style="margin: 0 !important;"><strong>Invoice No: </strong>
                                <asp:Label ID="lblInvoiceNo" Text="" runat="server" /></p>
               
                            <asp:Label ID="lblTrn" Text="" runat="server" />
                            <p class="" style="margin: 0 !important;"><strong>TRN No: </strong>
                                  <p class="" style="margin: 0 !important;"><strong>Project Name : </strong>
                                   <asp:Label ID="lblprojectName" Text="" runat="server" /></p> 
                            <p class="" style="margin: 0 !important;"><strong>LPO : </strong>
                                   <asp:Label ID="lblLpo" Text="" runat="server" /></p>

                        </div>
                    </div>
                     <%-- INVOICE ITEMS --%>
                     <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Table ID="listTable" CssClass="table table-bordered" style="border-color: #000; margin-bottom: 0;" runat="server">
                                    <asp:TableRow>
                                        <asp:TableHeaderCell>#</asp:TableHeaderCell>                            
                                        
                                        <asp:TableHeaderCell>Item Code</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Description</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Qty</asp:TableHeaderCell>
                                       <%-- <asp:TableHeaderCell>Mrp</asp:TableHeaderCell>--%>
                                        <asp:TableHeaderCell>Rate</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>VAT %</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Gross</asp:TableHeaderCell>
                                        <asp:TableHeaderCell>Tax </asp:TableHeaderCell>                                      
                                        <asp:TableHeaderCell>Net</asp:TableHeaderCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </div>
                        </div>
                    </div>

                    <%-- INVOICE SUMMARY --%>
                    <div class="row" style="border-top: 1px solid #000; margin: 0 3px;">
                        <div class="col-xs-5 text-center" style="border-right: 1px solid #000;">
                            <h6 class="m-t-40" style="text-transform: uppercase;">For&nbsp;<asp:Label ID="lblCompName" Text="" runat="server" /></h6>
                            <div style="height: 25px;"></div>
                            <h6><b>CUSTOMER SIGNATURE</b></h6>
                            <%--<h5 class="hidden">Terms and Conditions</h5>
                            <ul class="hidden">
                                <li>Termination of using or accessing your website</li>
                                <li>Disclosure to inform country laws</li>
                                <li>Contact details to inform users how they contact you with questions</li>
                            </ul>--%>
                            <asp:Literal ID="tAndC" runat="server"></asp:Literal>
                        </div>
                        <div class="col-xs-7 p-0" style="font-size: 12px;">
                            <p class="text-right m-b-0 m-t-5">
                                <b> Total Gross : </b>
                                <asp:Label ID="lblGross" Text="xx" runat="server" />
                            </p>
                            <p class="text-right m-0">
                              <b>Vat 5% :</b>
                                <asp:Label ID="lblTax" Text="xx" runat="server" />
                            </p>
                           <p class="text-right m-b-10">
                               <b>Round Off :</b>
                                <asp:Label ID="lblroundOff" Text="xx" runat="server" />
                            </p>
                            <hr class="m-0" />
                            <h4 class="text-right m-b-0 m-t-5" style="font-weight: 700; color :#000">
                                 <asp:Label ID="lblCurrency" Text="$" runat="server" style="color :#000" />&nbsp;
                                <asp:Label ID="lblNet" Text="xx" runat="server" style="color :#000" /></h4>
                             <p class="text-right m-0">
                            <asp:label ID="lblnumber" style="text-transform :capitalize;" Text="xx"  runat="server"/>
                             </p>
                        </div>

                        
                    </div>
                </div>
            </div>
        </form>
    </div>

    <!-- SCRIPTS BELOW  -->

    <script src="../../../../Theme/js/jquery.min.js"></script>
    <script src="../../../../Theme/js/jquery.app.js"></script>
    <script src="../../../../Theme/js/bootstrap.min.js"></script>

<script>
$(document).ready(function(){

});
</script>
</body>
</html>
