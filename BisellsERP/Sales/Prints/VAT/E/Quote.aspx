<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="BisellsERP.Sales.Prints.VAT.E.Quote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <style>
        body {
            background: rgb(204,204,204);
        }

        .page {
            background: white;
            display: block;
            margin: 0 auto;
            margin-bottom: 0.5cm;
            box-shadow: 0 0 0.5cm rgba(0,0,0,0.5);
            position: relative;
        }

        .page-padd {
            padding: 0 30px;
        }

        div[data-size="A4"] {
            width: 21cm;
            height: 31.7cm;
        }

        .text-underline {
            text-decoration: underline;
        }

        .fs-12 {
            font-size: 12px !important;
        }

        .fs-16 {
            font-size: 16px !important;
        }

        .list-table-wrap {
            min-height: 25cm;
        }

        .footer-block {
            height: 20px;
            background-color: #bbdefb !important;
            margin-bottom: 3px;
        }

        .invoice-footer {
            position: absolute;
            width: 21cm;
            margin-left: -20px;
            bottom: 15px;
        }

        .invoice-block {
            background-color: #212121;
            padding: 5px;
            margin-bottom: 35px;
        }

            .invoice-block p {
                color: #fff;
            }

        .text-justify {
            text-align: justify;
        }

        .table-bordered {
            border-color: #000 !important;
        }

            .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
                border-color: #000 !important;
            }

        @media print {
             body {
                font-family: 'cambria', "Helvetica Neue",Helvetica,Arial,sans-serif !important;
            }
            .cambria-font {
                font-family: 'cambria' !important;
            }
            body, .page {
                margin: 0;
                box-shadow: 0;
            }

            .page-padd {
                padding: 0;
            }

            div[data-size="A4"] {
                width: 100%;
                height: 31.7cm;
                position: relative;
            }

            .text-underline {
                text-decoration: underline;
            }

            .fs-12 {
                font-size: 12px !important;
            }

            .fs-16 {
                font-size: 16px !important;
            }

            .list-table-wrap {
                min-height: 25cm;
            }

            .page-break-before {
                page-break-before: always !important;
                break-before: always !important;
            }

            .invoice-footer {
                position: absolute;
                width: 23cm;
                margin-left: -20px;
                bottom: 15px;
            }

            .footer-block {
                height: 20px;
                background-color: #bbdefb !important;
                margin-bottom: 3px;
            }

            .invoice-block {
                background-color: #212121 !important;
                padding: 5px;
                margin-bottom: 35px;
            }

                .invoice-block p, .invoice-block small {
                    color: #fff !important;
                }

            .text-justify {
                text-align: justify;
            }

            .table-bordered {
                border-color: #000 !important;
            }

                .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > td, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > thead > tr > th {
                    border-color: #000 !important;
                }
        }
    </style>
</head>
<body>


    <%-- PAGE 1 --%>
    <div class="page page-padd" data-size="A4" style="font-family: 'Times New Roman'">

        <%-- INVOICE HEAD --%>
        <div class="row">
            <div class="col-xs-12">
                <h4 class="text-right m-t-0">
                    <asp:Image ID="imgLogo" ImageUrl="~/Theme/images/logobisellsjpg.jpg" runat="server" Height="70px" />
                </h4>
            </div>
        </div>

        <div class="row">
            <div class="col-xs-8 invoice-block" style="visibility: hidden">
                <p class=" text-center cambria-font" style="color: #fff">ENTERPRISE ICT SOLUTIONS FOR YOUR BUSINESS</p>
                <p class="m-b-0 text-center cambria-font" style="color: #fff"><small>We are devoted to making technology intergrate seamlessly with your business</small></p>
                <p class="m-b-0 text-center cambria-font" style="color: #fff"><small>Call today and allow us to to be your complete IT solutions provider</small></p>
            </div>
        </div>

        <%-- INVOICE DETAILS --%>
        <div class="row m-t-20 m-b-40  cambria-font">
            <div class="col-xs-8 fs-16  cambria-font">
                <div class="row  cambria-font">
                    <div class="col-xs-3  cambria-font">
                        <p class="fs-16 cambria-font"><b>Attention :</b></p>
                    </div>
                    <div class="col-xs-9 cambria-font">
                        <asp:Label ID="lblCustName" runat="server" />
                    </div>
                </div>

                <div class="row cambria-font">
                    <div class="col-xs-3 cambria-font">
                        <p><b>Company :</b></p>
                    </div>
                    <div class="col-xs-9 cambria-font">
                        <asp:Label ID="lblCustCompany" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xs-3 cambria-font">
                        <p>Emirate :</p>
                    </div>
                    <div class="col-xs-9 cambria-font">
                        <asp:Label ID="lblCustState" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row cambria-font">
                    <div class="col-xs-3 cambria-font">
                        <p>Email :</p>
                    </div>
                    <div class="col-xs-9 cambria-font">
                        <asp:Label ID="lblCustemail" runat="server"></asp:Label>
                    </div>
                </div>

            </div>
            <div class="col-xs-4 fs-16 cambria-font">
                <p>
                    <b>Date: </b>
                    <asp:Label ID="lblDate" Text="01 Feb 2018" runat="server" />
                </p>
                <p>
                    <b>Reference no: </b>
                    <asp:Label ID="lblReference" Text="ICT/NW/PPL/726" runat="server" />
                </p>

            </div>
        </div>
        <%--Invoice Decriptions--%>
        <div class="row fs-16 cambria-font">
            <div class="col-xs-12 cambria-font">
                <p class="text-underline m-b-20 cambria-font"><b>Subject: </b>Quotation for IT Work</p>

                <%--Message Content--%>

                <p class="text-justify">
                    Reference to the above-mentioned requirement,we thank you for the opportunity given to us to quote for the same.
                </p>
                <p class="text-justify">
                    At Netware,we intend to be in front of the client’s needs,deeply under standing client’s business and delivering and optimizing IT solutions from theinitialphaseofscoping therequirements,up to thefinaldelivery,maintenanceand continuous upgrade.Our partnership with industry leaders like Microsoft,HP,Dell,VMware,Veeam,Cisco, Avaya etc for Enterprise&SMBsolutions,we design smart,technology-enabled solutions to solveourclient’s toughest challenges, demonstrating a commitment to excellence and a passion for exceeding client's expectations.
                </p>
                <p class="text-justify">
                    We hope that the proposalis inline with your requirement and should you have any queries or clarifications regarding the proposal, please donot hesitate to contact the undersigned.
                </p>

                <p class="m-t-40 m-b-20">Yours Sincerely,</p>
                <div class="">
                    <strong>
                        <asp:Label ID="lblUserName" runat="server" Text="User"></asp:Label>
                    </strong>
                    <br />          
                    <asp:Label ID="lblDesignation" runat="server"></asp:Label><br />
                    <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label><br />
                    Phone :<asp:Label ID="lblCompanyContact" runat="server" Text="971-526402066"></asp:Label><br />
                    Email :<asp:Label ID="lblCompanyEmail" runat="server" Text="jismon@netwaretech.com"></asp:Label>
                </div>
            </div>
            <div class="col-xs-12 invoice-footer cambria-font">
                <div class="footer-block cambria-font"></div>
                <p class="text-center">sales@netwaretech.com, Tel: +971 4 255 5286, P.O.Box: 235485, Dubai - United Arab Emirates</p>
            </div>
        </div>

    </div>

    <%-- PAGE 2 --%>
    <div class="page page-padd" data-size="A4">

        <%-- INVOICE HEAD --%>
        <div class="row cambria-font">
            <div class="col-xs-12 cambria-font">
                <h4 class="text-right m-t-0 cambria-font">
                    <asp:Image ID="imgLogo1" ImageUrl="~/Theme/images/logobisellsjpg.jpg" runat="server" Height="80px" />
                </h4>
            </div>
        </div>
        <%-- OUR SERVICES --%>
        <div class="row m-t-0 cambria-font">
            <div class="col-xs-12 cambria-font">
                <h2 class="text-bold cambria-font">Our Services</h2>
            </div>
            <div class="col-xs-12 text-center m-t-0 cambria-font">
                <h2>
                    <img src="../../../../Theme/images/Services_Netware.png" style="width: 300px;" alt="Alternate Text" />
                </h2>
            </div>
        </div>
        <%-- OUR SOLUTIONS --%>
        <div class="row m-t-40 cambria-font">
            <div class="col-xs-12 cambria-font">
                <h2 class="text-bold cambria-font">Our Solutions</h2>
            </div>
            <div class="col-xs-12 text-center m-b-30 cambria-font">
                <h2>
                    <img src="../../../../Theme/images/Solutions_netware.png" alt="Alternate Text" />
                </h2>
            </div>
        </div>

        <div class="col-xs-12 invoice-footer cambria-font">
            <div class="footer-block cambria-font"></div>
            <p class="text-center cambria-font">sales@netwaretech.com, Tel: +971 4 255 5286, P.O.Box: 235485, Dubai - United Arab Emirates</p>
        </div>
    </div>

    <%-- PAGE 3 --%>
    <div class="page page-padd cambria-font" data-size="A4">
        <%-- INVOICE HEAD --%>
        <div class="row cambria-font">
            <div class="col-xs-12 cambria-font">
                <h4 class="text-right m-t-40 cambria-font">
                    <asp:Image ID="imgLogo2" ImageUrl="~/Theme/images/logobisellsjpg.jpg" runat="server" Height="80px" />
                </h4>
            </div>
        </div>
        <%-- INVOICE ITEMS --%>
        <div class="row cambria-font">
            <div class="col-xs-12 cambria-font">
                <h3 class="text-center text-muted m-t-30 cambria-font">BILLS OF MATERIALS</h3>
            </div>
            <div class="col-xs-12 cambria-font">
                <div class="list-table-wrap cambria-font">
                    <asp:Table ID="listTable" CssClass="table table-bordered" runat="server">
                        <asp:TableRow>
                            <asp:TableHeaderCell CssClass="hidden">#</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Part No</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Product Description</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Qty</asp:TableHeaderCell>
                            <%--<asp:TableHeaderCell>Mrp</asp:TableHeaderCell>--%>
                            <asp:TableHeaderCell>Unit Price</asp:TableHeaderCell>
                            <%--<asp:TableHeaderCell>VAT %</asp:TableHeaderCell>--%>
                            <%--<asp:TableHeaderCell>Gross</asp:TableHeaderCell>--%>
                            <%--<asp:TableHeaderCell>Tax </asp:TableHeaderCell>--%>
                            <asp:TableHeaderCell>Total Price</asp:TableHeaderCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </div>



    <%-- PAGE 4 --%>
    <div class="page page-padd" data-size="A4">
        <%-- INVOICE HEAD --%>
        <div class="row page-break-before cambria-font">
            <div class="col-xs-12 cambria-font">
                <h4 class="text-right m-t-40 cambria-font">
                    <asp:Image ID="imgLogo3" ImageUrl="~/Theme/images/logobisellsjpg.jpg" runat="server" Height="80px" />
                </h4>
            </div>
        </div>

        <div class="row cambria-font">
            <div class="col-xs-12 cambria-font">
                <%--Terms and Condition--%>
                <h3 class="text-muted text-center m-t-30 cambria-font">TERMS AND CONDITIONS</h3>
                <asp:Literal ID="tAndC" runat="server"> 
                <ul>
                    <li>Termination of using or accessing your website</li>
                    <li>Disclosure to inform country laws</li>
                    <li>Contact details to inform users how they contact you with questions</li>
                </ul>
                </asp:Literal>
            </div>
        </div>

        <div class="col-xs-12 invoice-footer cambria-font">
            <div class="footer-block cambria-font"></div>
            <p class="text-center cambria-font">sales@netwaretech.com, Tel: +971 4 255 5286, P.O.Box: 235485, Dubai - United Arab Emirates</p>
        </div>
    </div>

</body>
</html>
