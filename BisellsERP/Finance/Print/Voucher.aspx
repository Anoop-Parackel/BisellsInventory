<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Voucher.aspx.cs" Inherits="BisellsERP.Finance.Print.Voucher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Voucher</title>
    <%-- STYLE SHEETS --%>
    <link href="../../../../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../../Theme/css/helper.css" rel="stylesheet" />
    <link href="../../../../Theme/css/style.css" rel="stylesheet" />
    <!-- jQuery  -->
    <script src="/Theme/js/jquery.min.js"></script>
    <!-- Base Css Files -->
    <link href="/Theme/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Icons -->
    <link href="/Theme/assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="/Theme/assets/ionicon/css/ionicons.min.css" rel="stylesheet" />
    <link href="/Theme/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <!-- animate css -->
    <link href="/Theme/css/animate.css" rel="stylesheet" />
    <!-- Waves-effect -->
    <link href="/Theme/css/waves-effect.css" rel="stylesheet" />
    <%-- Plugins css --%>
    <link href="/Theme/assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <link href="/Theme/assets/modal-effect/css/component.css" rel="stylesheet" />
    <!-- X-editable css -->
    <%--<link type="text/css" href="/Theme/assets/bootstrap3-editable/bootstrap-editable.css" rel="stylesheet" />--%>


    <link href="/Theme/assets/notifications/notification.css" rel="stylesheet" />
    <!-- Select 2 CSS -->
    <link href="/Theme/assets/select2/select2.css" rel="stylesheet" type="text/css" />
    <!-- Custom Files -->
    <link href="/Theme/css/helper.css" rel="stylesheet" type="text/css" />
    <link href="/Theme/css/style.css" rel="stylesheet" type="text/css" />

    <style>
        .voucher-wrap {
            width: 700px;
            margin: 0 auto;
            border: 1px solid #ccc;
            padding: 10px;
        }

        .voucher-details, .voucher-entries {
            margin-top: 1.5em;
        }

        .debit > td:nth-of-type(1) {
            padding-left: 20px !important;
        }

        .voucher-type {
            text-decoration: underline;
            color: #607D8B;
            text-transform: uppercase;
            font-weight: 600;
        }

        .total, thead > tr {
            background-color: #eee;
        }

            .total > td {
                font-weight: 600;
            }

        .voucher-seals, .voucher-seals > div {
            height: 125px;
        }

        .seal {
            position: absolute;
            bottom: 0;
        }

        @media print {
            .voucher-wrap {
                width: 100%;
                margin: auto;
            }
        }
    </style>
    <script>
        $(document).ready(function () {

            function getDateFormat(Fve_Date) {
                var newDate = new Date(Fve_Date);
                var stringDate = newDate.getDate() + '/' + getmonth(newDate.getMonth()) + '/' + newDate.getFullYear();
                return stringDate;
            }

            function getmonth(month) {
                if (month == 0) {
                    month = 12;
                }
                switch (month) {
                    case 1:
                        return 'Jan';
                    case 2:
                        return 'Feb';
                    case 3:
                        return 'Mar';
                    case 4:
                        return 'Apr';
                    case 5:
                        return 'May';
                    case 6:
                        return 'June';
                    case 7:
                        return 'July';
                    case 8:
                        return 'Aug';
                    case 9:
                        return 'Sep';
                    case 10:
                        return 'Oct';
                    case 11:
                        return 'Nov';
                    case 12:
                        return 'Dec';
                }
            }


            //Load data for Print
            var Parameter = getUrlVars();

            var apiurl = $('#hdApiUrl').val();

            var Vouchertype = Parameter.id;
            var credittotal = 0.0;
            var debitTotal = 0.0;
            $.ajax({
                url: $('#hdApiUrl').val() + '/api/VoucherEntry/GetVoucherDataforPrint?GroupID=' + Vouchertype,
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'Json',
                data: JSON.stringify($.cookie("bsl_1")),
                success: function (response) {
                    console.log(response);
                    $('#lblVoucherNumber').text(response[0].Voucher);
                    $('#lblCompanyName').text(response[0].Company);
                    $('#lblCompanyAddress1').text(response[0].Address1);
                    $('#lblCompanyAddress2').text(response[0].Address2);
                    $('#lblState').text(response[0].State + ',' + response[0].Country);
                    $('#lblVoucherDate').text(getDateFormat(response[0].Fve_Date));
                    $('#lblCompanyPhone').text(response[0].Mobile_No1);
                    $('#lblTaxNumber').text(response[0].Reg_Id1);
                    $('#lblMainTitle').text(response[0].Heading);
                    var html = '';
                    for (var i = 0; i < response.length; i++) {
                        if (response[i].CreditOrDebit == 0) {
                            debitTotal += parseFloat(response[i].creditAmt);
                            html += '<tr class="credit"><td>' + response[i].particulars + '</td><td></td><td>' + response[i].creditAmt + '</td>';
                            $('#lblNarration').text(response[i].particulars);
                        }
                        else if (response[i].CreditOrDebit == 1) {
                            credittotal += parseFloat(response[i].DebitAmt);
                            html += '<tr class="debit"><td>' + response[i].particulars + '</td><td>' + response[i].DebitAmt + '</td><td></td></tr>';
                        }
                    }
                    html += '<tr class="total"><td></td><td>' + debitTotal + '</td><td>' + credittotal + '</td></tr>';
                    $('#tblVoucherDetails >tbody').empty();
                    $('#tblVoucherDetails >tbody').append(html);

                },
                error: function (xhr) {
                    alert(xhr.responseText);
                    console.log(xhr);
                }
            });
        });
    </script>
</head>
<body>
    <div class="voucher-wrap">
        <div class="col-xs-4">
            <%--<asp:HiddenField ID="hdApiUrl" Value="0" runat="server" />--%>
            <input type="hidden" id="hdApiUrl" value="0" runat="server" />
            <asp:Image ID="imgLogo" ImageUrl="~/Theme/images/logobisellsjpg.jpg" runat="server" Height="70px" />
            <div class="col-xs-12 m-t-20">
                <p class="m-0">Voucher No : <span id="lblVoucherNumber" runat="server">1425</span></p>
                <p>Date : <span id="lblVoucherDate">11/01/2018</span></p>
            </div>
        </div>
        <div class="col-xs-4 text-center">
            <h4 class="m-t-30 voucher-type">
                <asp:Label runat="server" ID="lblMainTitle" ClientIDMode="Static" Text="Cash Voucher"></asp:Label></h4>
        </div>
        <div class="col-xs-4 text-right">
            <b>
                <asp:Label runat="server" class="m-b-0" ID="lblCompanyName" ClientIDMode="Static" Text="Urban Scape"></asp:Label></b>
            <address>
                <small>
                    <asp:Label runat="server" ID="lblCompanyAddress1" Text="Address Line 1" ClientIDMode="Static" /><br />
                    <asp:Label runat="server" ID="lblCompanyAddress2" Text="Address Line 2" ClientIDMode="Static" /><br />
                    <asp:Label runat="server" ID="lblState" Text="State, Country" ClientIDMode="Static" /><br />
                    <b>Ph : </b>
                    <asp:Label runat="server" ID="lblCompanyPhone" Text="949456789" ClientIDMode="Static" /><br />
                    <b>Tax No:</b><asp:Label runat="server" ID="lblTaxNumber" Text="85857475" ClientIDMode="Static" /><br />

                </small>
            </address>
        </div>
        <div class="voucher-entries">
            <div class="row">
                <div class="col-xs-12">
                    <table class="table table-bordered" id="tblVoucherDetails">
                        <thead>
                            <tr>
                                <th style="width: 70%">Particulars</th>
                                <th style="width: 15%">Debit</th>
                                <th style="width: 15%">Credit</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="credit">
                                <td>Debit Account</td>
                                <td></td>
                                <td>4000</td>
                            </tr>
                            <tr class="debit">
                                <td>Credit Account</td>
                                <td>1000</td>
                                <td></td>
                            </tr>
                            <tr class="debit">
                                <td>Credit Account</td>
                                <td>1000</td>
                                <td></td>
                            </tr>
                            <tr class="debit">
                                <td>Credit Account</td>
                                <td>1000</td>
                                <td></td>
                            </tr>
                            <tr class="debit">
                                <td>Credit Account</td>
                                <td>1000</td>
                                <td></td>
                            </tr>
                            <tr class="total">
                                <td></td>
                                <td>4000</td>
                                <td>4000</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="voucher-seals">
            <div class="col-xs-6">
                <p>For :</p>
                <p class="m-l-20 m-t-10" id="lblNarration" runat="server">Debit Account</p>
            </div>
            <div class="col-xs-6 text-center">
                <span class="text-muted seal">Authorised Signature</span>
            </div>
        </div>
    </div>
</body>
<%-- Bootstrap --%>
<script src="/Theme/js/bootstrap.min.js" type="text/javascript"></script>
<script src="/Theme/assets/jquery-detectmobile/detect.js" type="text/javascript"></script>
<script src="/Theme/assets/fastclick/fastclick.js" type="text/javascript"></script>
<!-- CUSTOM JS -->
<script src="/Theme/js/jquery.nicescroll.js" type="text/javascript"></script>
<script src="/Theme/js/jquery.scrollTo.min.js" type="text/javascript"></script>
<script src="../../Theme/Custom/Commons.js"></script>
<script src="../../Theme/js/jquery.app.js" type="text/javascript"></script>
<script src="../../Plugins/Cookies/jquery.cookie.js"></script>

</html>
