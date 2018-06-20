<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Entry.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.C.Entry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Invoice Print</title>
    <%-- STYLE SHEETS --%>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <style>
        body {
            margin: 10px auto;
        }

        .a4-page {
            width: 24.5cm;
            width: 1053px;
            margin: 0 auto;
            /*padding: 0 2cm;*/
        }

        pre {
            padding: 0;
            font-size: 14px;
            background-color: #f5f5f5;
            border: 0;
            border-radius: 0;
            text-align: left;
        }

        .bbc {
            border-bottom-color: transparent;
        }

        @media print {
            .a4-page {
                margin: 0;
                margin-top: 5cm;
            }
        }
    </style>
</head>
<body>

    <div class="a4-page">

        <table width="1053" height="299" border="1">
            <tr>
                <td style="width: 449px; height: 89px;">
                    <h1 class="p-l-5">Invoice</h1>
                </td>
                <td>
                    <p class="p-l-5 w-lg">
                        Date:
                        <asp:Label ID="lblDate" runat="server" class="p-l-10"></asp:Label>
                    </p>
                    <p class="p-l-5 w-lg">
                        Inv No:
                        <asp:Label ID="lblInvno" runat="server" class="p-l-10"></asp:Label>
                    </p>
                </td>
            </tr>
            <tr>
                <td height="28" class="p-l-5"><b>Invoice To</b></td>
                <td class="p-l-5"><b>Ship To</b></td>
            </tr>
            <tr>
                <td height="172">
                    <p class="m-l-30">
                        <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblCountry" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblPhoneNo1" runat="server"></asp:Label><br />

                    </p>
                </td>
                <td width="588">
                    <p class="m-l-30">
                        <asp:Label ID="lblCustomer" runat="server"></asp:Label><br />
                        <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblAddress3" runat="server"></asp:Label><br />
                        <asp:Label ID="lblCountry1" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblPhoneno" runat="server"></asp:Label><br />

                    </p>
                </td>
            </tr>
        </table>

        <table width="1053" height="267" border="1">
            <tr>
                <td width="291" style="text-align: center;"><b>Customer P.O./Ref</b></td>
                <td width="303" height="37" style="text-align: center;"><b>Terms of Payment</b></td>
                <td style="text-align: center;" colspan="2"><b>Delivery Destination</b></td>
            </tr>
            <tr>
                <td style="text-align: center;">Email Confirmation dtd 4/8/15</td>
                <td style="text-align: center;">50% ADV &amp; Balance upon delivery</td>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;"><b>Country of Origin</b></td>
                <td style="text-align: center;"><b>Shipped Via</b></td>
                <td style="text-align: center;" colspan="2"><b>Delivery Period</b></td>
            </tr>
            <tr>
                <td style="text-align: center;">UK</td>
                <td id="lblDespatch" runat="server" style="text-align: center;">Despatch</td>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;"><b>Account No</b></td>
                <td style="text-align: center;"><b>A/C Name</b></td>
                <td style="text-align: center;" width="212"><b>Bank</b></td>
                <td style="text-align: center;" width="215"><b>Swift Code</b></td>
            </tr>
            <tr>
                <td style="text-align: center;"></td>
                <td>&nbsp;</td>
                <td style="text-align: center;">Emirates NBD</td>
                <td style="text-align: center;">EBILAEAD</td>
            </tr>
        </table>
        <asp:Table runat="server" ID="productTable" Width="1053"  border="1">
            <asp:TableHeaderRow>
                <asp:TableCell Width="95" Height="40" Style="text-align: center;">Item</asp:TableCell>
                <asp:TableCell Width="450" Style="text-align: center;">Description</asp:TableCell>
                <asp:TableCell Width="80" Style="text-align:center ;">Qty</asp:TableCell>
                <asp:TableCell Width="102" Style="text-align: center;">Unit Price </asp:TableCell>
                <asp:TableCell Width="107" Style="text-align: center;">Tax Amount</asp:TableCell>
                <asp:TableCell Width="213" Style="text-align: center;">Total Price</asp:TableCell>
            </asp:TableHeaderRow>
           
        </asp:Table>



        <table width="1053" height="347" border="1">
            <tr>
                <td height="40" colspan="3">&nbsp;</td>
                <td  class="p-l-5">Sub Total</td>
                <td style="text-align:right" class="p-l-5">&nbsp;<asp:label ID="lblSubTotal" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td style="text-align: center;" height="30" colspan="3"><b>Packing Details</b></td>
                <td class="p-l-5">Tax Amount</td>
                <td style="text-align:right" class="p-l-5">&nbsp;<asp:label ID="lblTaxAmount" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td id="lblCartoon" runat="server" width="102" height="30" class="text-center">Cartoon No.</td>
                <td style="text-align: center;" width="240">Weight in KGS</td>
                <td style="text-align: center;" width="252">Case Dimensions</td>
                <td width="212" class="p-l-5"><b>Net Total CFR</b></td>
                <td width="216" style="text-align:right" class="p-l-5">&nbsp;<asp:label ID="lblNetCfr" runat="server"></asp:label></td>
            </tr>
            <tr>
                <td height="55" class="text-center">2</td>
                <td>&nbsp;</td>
                <td style="text-align: center;">&nbsp;</td>
                <td style="text-align: center; padding-top: 4.9cm;" colspan="2" rowspan="3">Authorized Signatory</td>
            </tr>
            <tr>
                <td height="40" class="text-center">Type of Package</td>
                <td style="text-align: center;" colspan="2">Shipping Marks:</td>
            </tr>
            <tr>
                <td height="72" class="text-center">Wooden Box</td>
                <td colspan="2">&nbsp;</td>
            </tr>
        </table>


    </div>

</body>
</html>

