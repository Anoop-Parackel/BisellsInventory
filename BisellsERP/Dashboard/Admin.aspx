<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="BisellsERP.Dashboard.admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #wrapper {
            overflow: hidden;
            height: auto;
        }

        .nav.nav-tabs > li > a, .nav.tabs-vertical > li > a {
            line-height: 40px;
            border-radius: 8px 8px 0 0;
            color: #9E9E9E !important;
        }

            .nav.nav-tabs > li > a:hover, .nav.tabs-vertical > li > a:hover {
                background-color: rgba(255, 255, 255, 0.5);
            }

        .nav.nav-tabs > li.active > a, .tabs-vertical-env .nav.tabs-vertical li.active > a {
            border: 0;
            background-color: #fff;
            color: #004566 !important;
        }

        .nav-tabs > li > a {
            margin-right: 5px;
        }

        .nav.nav-tabs + .tab-content, .tabs-vertical-env .tab-content {
            background-color: transparent;
            box-shadow: none;
        }

        .navtab-bg {
            background-color: transparent;
        }


        .portlet {
            border-radius: 5px;
        }

        .mini-stat-info span {
            font-size: 18px;
        }

            .mini-stat-info span.lf {
                font-size: 18px;
            }

        input[name="daterange"] {
            border: none;
            color: #607d8b;
            font-size: 12px;
        }

        .bg-dash-blue {
            background-color: #3e95cd;
        }

        .bg-dash-green {
            background-color: #3cba9f;
        }

        .bg-dash-red {
            background-color: #c45850;
        }

        .bg-dash-ccc {
            background-color: #ccc;
        }

        .bg-dash-bluegrey {
            background-color: #607d8b;
        }

        .bg-dash-purple {
            background-color: #8e5ea2;
        }

        .dash-height {
            min-height: 107px;
        }

        /* PROGRESS BAR STYLE */
        .progress {
            width: 100%;
            height: 100%;
            line-height: 150px;
            background: none;
            margin: 0 auto;
            box-shadow: none;
            position: relative;
        }

            .progress:after {
                content: "";
                width: 100%;
                height: 100%;
                border-radius: 50%;
                border: 12px solid #fff;
                position: absolute;
                top: 0;
                left: 0;
            }

            .progress > span {
                width: 50%;
                height: 100%;
                overflow: hidden;
                position: absolute;
                top: 0;
                z-index: 1;
            }

            .progress .progress-left {
                left: 0;
            }

            .progress .progress-bar {
                width: 100%;
                height: 100%;
                background: none;
                border-width: 12px;
                border-style: solid;
                position: absolute;
                top: 0;
            }

            .progress .progress-left .progress-bar {
                left: 100%;
                border-top-right-radius: 80px;
                border-bottom-right-radius: 80px;
                border-left: 0;
                -webkit-transform-origin: center left;
                transform-origin: center left;
            }

            .progress .progress-right {
                right: 0;
            }

                .progress .progress-right .progress-bar {
                    left: -100%;
                    border-top-left-radius: 80px;
                    border-bottom-left-radius: 80px;
                    border-right: 0;
                    -webkit-transform-origin: center right;
                    transform-origin: center right;
                }

        .progress-1 .progress-left .progress-bar {
            animation: loading-1-left 1.8s linear forwards;
        }

        .progress-1 .progress-right .progress-bar {
            animation: loading-1-right 1.8s linear forwards;
        }

        .progress .progress-value {
            width: 90%;
            height: 90%;
            border-radius: 50%;
            background: #f5f5f5;
            font-size: 14px;
            color: #455a64;
            line-height: 56px;
            text-align: center;
            position: absolute;
            top: 5%;
            left: 5%;
        }

        .progress.green .progress-bar {
            border-color: #3cba9f;
        }

        .progress-2 .progress-left .progress-bar {
            animation: loading-2-left 1.2s linear forwards 1.8s;
        }

        .progress-2 .progress-right .progress-bar {
            /*animation: loading-2-right 1.2s linear forwards 1.8s;*/
        }



        @keyframes loading-1-right {
            0% {
                -webkit-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(180deg);
                transform: rotate(180deg);
            }
        }

        @keyframes loading-1-left {
            0% {
                -webkit-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(45deg);
                transform: rotate(45deg);
            }
        }

        @keyframes loading-2-right {
            0% {
                -webkit-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(180deg);
                transform: rotate(180deg);
            }
        }

        @keyframes loading-2-left {
            0% {
                -webkit-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            100% {
                -webkit-transform: rotate(45deg);
                transform: rotate(45deg);
            }
        }

        @media only screen and (max-width: 990px) {
            .progress {
                margin-bottom: 20px;
            }
        }

        /* List Card Styles */

        .addon {
            background: #fff;
            border-radius: 3px;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            padding-bottom: 10px;
        }

            .addon > ul {
                overflow-y: scroll;
            }

            .addon li {
                padding: 10px;
                border-top: 1px dashed #E0E0E0;
                overflow: hidden;
            }

            .addon li {
                list-style: none;
            }

        .clearfix {
            display: block;
        }

            .clearfix:after {
                content: " ";
                display: block;
                height: 0;
                clear: both;
                visibility: hidden;
                overflow: hidden;
            }

        li {
            display: list-item;
            text-align: -webkit-match-parent;
        }

        ul {
            margin: 0;
            padding: 0;
            border: 0;
            font-size: 100%;
            font: inherit;
            vertical-align: baseline;
        }

        ul {
            list-style: none;
        }

        .activity-round {
            border-radius: 100%;
            display: block;
            padding: 8px;
            float: left;
            font-size: 14px;
            color: whitesmoke;
        }

        .addon p {
            padding: 10px 15px;
            margin: 0;
            font-weight: 600;
            font-size: 16px;
            color: #797979;
        }

        p {
            display: block;
            -webkit-margin-before: 1em;
            -webkit-margin-after: 1em;
            -webkit-margin-start: 0px;
            -webkit-margin-end: 0px;
        }

        .addon li .legend-info {
            float: left;
            margin-left: 10px;
            width: 65%;
            color: #607d8b;
        }

        .legend-info small, .legend-info a {
            margin-right: 5px;
            padding-right: 5px;
            color: #3e95cd;
            font-size: 11px;
            border-right: 1px solid #e6e6e6;
        }

            .legend-info small:last-child {
                font-weight: bold;
                margin-right: 0;
                padding-right: 0;
                border: none;
            }

            .legend-info small i, .legend-info a i {
                color: #607d8b;
            }

        .time-stamp {
            float: right;
            margin-right: 5px;
            color: #ccc;
        }

        .inline-block {
            display: inline-block;
        }

        .pr-br-mr {
            padding-right: 15px;
            border-right: 1px dashed #ccc;
            margin-right: 10px;
        }



        .donut-size {
            font-size: 2em;
        }

        .pie-wrapper {
            position: relative;
            width: 1em;
            height: 1em;
            margin: 0px auto;
        }

            .pie-wrapper .pie {
                position: absolute;
                top: 0px;
                left: 0px;
                width: 100%;
                height: 100%;
                clip: rect(0, 1em, 1em, 0.5em);
            }

            .pie-wrapper .half-circle {
                position: absolute;
                top: 0px;
                left: 0px;
                width: 100%;
                height: 100%;
                border: 0.1em solid #3cba9f;
                border-radius: 50%;
                clip: rect(0em, 0.5em, 1em, 0em);
            }

            .pie-wrapper .right-side {
                -webkit-transform: rotate(0deg);
                transform: rotate(0deg);
            }

            .pie-wrapper .label {
                position: absolute;
                /*top: 0.52em;*/
                right: 0.4em;
                bottom: 0.4em;
                left: 0.4em;
                display: block;
                background: none;
                border-radius: 50%;
                color: #7F8C8D;
                font-size: 0.25em;
                line-height: 2.6em;
                text-align: center;
                cursor: default;
                z-index: 2;
            }

            .pie-wrapper .smaller {
                padding-bottom: 20px;
                color: #BDC3C7;
                font-size: 0.45em;
                vertical-align: super;
            }

            .pie-wrapper .shadow {
                width: 100%;
                height: 100%;
                border: 0.1em solid #ccc;
                border-radius: 50%;
            }

        .today-cash-credit .mini-stat,
        .week-cash-credit .mini-stat,
        .month-cash-credit .mini-stat {
            padding: 10px 20px;
            margin-bottom: 10px;
        }

        /*.widget-title {
            margin-top: 0;
            font-weight: bold;
            color: #90A4AE;
            text-align: right;
        }*/

        .widget-title {
            margin-top: 10px;
            font-weight: bold;
            color: #3cba9f;
            text-align: right;
            background-color: #FAFAFA;
            margin-bottom: 0;
            padding: 10px;
            border-bottom: 1px solid #EEEEEE;
            border-radius: 3px;
        }
        .widget-title:nth-of-type(1) {
            margin-top: 0;
        }
            
    </style>
    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <script>
        loading('start');
    </script>
    <div class="row">

        <%-- CHART SIDE --%>
        <div class="col-md-8 col-lg-8">
            <ul class="nav nav-tabs navtab-bg hidden">
                <li class="active">
                    <a href="#dash-summary" data-toggle="tab" aria-expanded="true">
                        <span class="visible-xs"><i class="fa fa-home"></i></span>
                        <span class="hidden-xs">Summary</span>
                    </a>
                </li>
                <%--                <li class="">
                    <a href="#dash-purchase" data-toggle="tab" aria-expanded="false">
                        <span class="visible-xs"><i class="fa fa-user"></i></span>
                        <span class="hidden-xs">Purchase</span>
                    </a>
                </li>
                <li class="">
                    <a href="#dash-sales" data-toggle="tab" aria-expanded="false">
                        <span class="visible-xs"><i class="fa fa-envelope-o"></i></span>
                        <span class="hidden-xs">Sales</span>
                    </a>
                </li>--%>
            </ul>
            <div class="tab-content p-0">
                <div class="tab-pane active fade in" id="dash-summary">

                    <div class="row">
                        <div class="col-sm-12">
                            <div class="portlet">
                                <div class="portlet-heading portlet-default">
                                    <h3 class="portlet-title text-dark">Cash Vs Credit Vs Expenses</h3>
                                    <div class="portlet-widgets">
                                        <input type="text" name="daterange" id="dateSummaryChart1" value="01/01/2015 - 01/31/2015" />
                                        <span class="divider"></span>
                                        <a href="javascript:;" id="refreshSummaryChart1" data-toggle="reload"><i class="ion-refresh"></i></a>
                                        <span class="divider"></span>
                                        <a href="#" id="downloadSummaryChart1"><i class="md md-file-download"></i></a>
                                        <span class="divider"></span>
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#summary-chart1"><i class="ion-minus-round"></i></a>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div id="summary-chart1" class="panel-collapse collapse in">
                                    <div class="portlet-body p-t-0">
                                        <div class="summary-chart1">
                                            <canvas id="summaryChart1" width="400" height="100"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="portlet">
                                <div class="portlet-heading portlet-default">
                                    <h3 class="portlet-title text-dark">Purchase Bifurcation</h3>
                                    <div class="portlet-widgets">
                                        <a href="javascript:;" data-toggle="reload"><i class="ion-refresh"></i></a>
                                        <span class="divider"></span>
                                        <a href="#" id="downloadSummaryChart2"><i class="md md-file-download"></i></a>
                                        <span class="divider"></span>
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#summary-chart2"><i class="ion-minus-round"></i></a>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div id="summary-chart2" class="panel-collapse collapse in">
                                    <div class="portlet-body p-t-0">
                                        <div class="summary-chart2">
                                            <canvas id="summaryChart2" width="400" height="180"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-6 today-cash-credit">
                            <h6 class="widget-title">Today's Cash/Credit Sales</h6>
                            <%--                            <div class="col-md-4">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardTotalSales" class="counter">00</span>
                                            Total Sales
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCashSales" class="counter">00</span>
                                            Cash Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCreditSales" class="counter">00</span>
                                            Credit Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-6 week-cash-credit">
                            <h6 class="widget-title">This Week's Cash/Credit Sales</h6>
                            <%--                            <div class="col-md-4">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardTotalSales" class="counter">00</span>
                                            Total Sales
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCashSalesWeek" class="counter">00</span>
                                            Cash Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCreditSalesWeek" class="counter">00</span>
                                            Credit Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-6 month-cash-credit">
                            <h6 class="widget-title">This Month's Cash/Credit Sales</h6>
                            <%--                            <div class="col-md-4">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardTotalSales" class="counter">00</span>
                                            Total Sales
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCashSalesMonth" class="counter">00</span>
                                            Cash Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="mini-stat clearfix bx-shadow">
                                        <div class="mini-stat-info text-center text-muted">
                                            <span id="cardCreditSalesMonth" class="counter">00</span>
                                            Credit Sales
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-12">
                            <div class="portlet sale-purchase-charts">
                                <div class="portlet-heading portlet-default">
                                    <h3 class="portlet-title text-dark">Sale v/s Purchase</h3>
                                    <div class="portlet-widgets">
                                        <input type="text" name="daterange" id="dateSummaryChart3" value="01/01/2015 - 01/31/2015" />
                                        <span class="divider"></span>
                                        <a href="javascript:;" data-toggle="reload" id="refreshSummaryChart3"><i class="ion-refresh"></i></a>
                                        <span class="divider"></span>
                                        <a href="#" id="downloadSummaryChart3"><i class="md md-file-download"></i></a>
                                        <span class="divider"></span>
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#summary-chart3"><i class="ion-minus-round"></i></a>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div id="summary-chart3" class="panel-collapse collapse in">
                                    <div class="portlet-body p-t-0">
                                        <div class="summary-chart3">
                                            <canvas id="summaryChart3" width="400" height="100"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="dash-purchase">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="portlet">
                                <div class="portlet-heading portlet-default">
                                    <h3 class="portlet-title text-dark">Purchase Charts</h3>
                                    <div class="portlet-widgets">
                                        <a href="javascript:;" data-toggle="reload"><i class="ion-refresh"></i></a>
                                        <span class="divider"></span>
                                        <a href="#"><i class="md md-file-download"></i></a>
                                        <span class="divider"></span>
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#purchase-chart1"><i class="ion-minus-round"></i></a>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div id="purchase-chart1" class="panel-collapse collapse in">
                                    <div class="portlet-body p-t-0">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="dash-sales">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="portlet">
                                <div class="portlet-heading portlet-default">
                                    <h3 class="portlet-title text-dark">Sales Charts</h3>
                                    <div class="portlet-widgets">
                                        <a href="javascript:;" data-toggle="reload"><i class="ion-refresh"></i></a>
                                        <span class="divider"></span>
                                        <a href="#"><i class="md md-file-download"></i></a>
                                        <span class="divider"></span>
                                        <a data-toggle="collapse" data-parent="#accordion1" href="#purchase-chart2"><i class="ion-minus-round"></i></a>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                                <div id="purchase-chart2" class="panel-collapse collapse in">
                                    <div class="portlet-body p-t-0">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <%-- WIDGET SIDE --%>
        <div class="col-md-4 col-lg-4">
            <div class="col-md-12 p-r-0">
                <div class="mini-stat clearfix bx-shadow">
                    <span class="mini-stat-icon bg-dash-blue b-r-5"><i class="ion-trophy"></i></span>
                    <div class="text-right">
                        <div class="mini-stat-info text-muted inline-block pr-br-mr">
                            <span class="counter lf" id="lblTotalReceivable">158k</span>
                            Total Receivables
                        </div>
                        <div class="mini-stat-info text-muted inline-block">
                            <span class="counter lf" id="lblTotalPayable">352k</span>
                            Total Payables
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 p-r-0">
                <div class="col-md-6">
                    <div class="row">
                        <div class="mini-stat clearfix bx-shadow dash-height">
                            <div class="mini-stat-icon m-t-5">
                                <div id="newCustomerProgress" class="donut-size">
                                    <div class="pie-wrapper">
                                        <span class="label">
                                            <span class="num">0</span><span class="smaller">%</span>
                                        </span>
                                        <div class="pie">
                                            <div class="left-side half-circle"></div>
                                            <div class="right-side half-circle"></div>
                                        </div>
                                        <div class="shadow"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="mini-stat-info text-right text-muted">
                                <span id="cardNewCustomers" class="counter lf m-t-10">x</span>
                                New Customers
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="row">
                        <div class="mini-stat clearfix bx-shadow dash-height">
                            <div class="mini-stat-icon m-t-5">

                                <div id="returningCustomerProgress" class="donut-size">
                                    <div class="pie-wrapper">
                                        <span class="label">
                                            <span class="num">0</span><span class="smaller">%</span>
                                        </span>
                                        <div class="pie">
                                            <div class="left-side half-circle"></div>
                                            <div class="right-side half-circle"></div>
                                        </div>
                                        <div class="shadow"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="mini-stat-info text-right text-muted">
                                <span id="cardReturningCustomers" class="counter lf m-t-10">00</span>
                                Returning Customers
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 p-r-0">
                <div class="addon">
                    <p>Activity</p>
                    <ul id="activitySection">
                    </ul>
                </div>
            </div>

        </div>
    </div>

    <%-- Scripts Below --%>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.js"></script>--%>
    <script src="../Theme/assets/chartjs/chart.min.js"></script>
    <!-- Date Range Picker -->
    <script type="text/javascript" src="//cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.js"></script>
    <link rel="stylesheet" type="text/css" href="//cdn.jsdelivr.net/bootstrap.daterangepicker/2/daterangepicker.css" />

    <script>

        //Set Height between Widget and Chart
        $(window).resize(function () {
            var chartHeight = Math.abs($('.today-cash-credit').offset().top - $('.month-cash-credit').offset().top - $('.month-cash-credit').height()) - 64;
            if (chartHeight) {
                $('#summary-chart2').css('min-height', chartHeight);
            }
        });

        $(function () {

            //var dataHeight = Math.abs($('.today-cash-credit').offset().top - $('.month-cash-credit').offset().top - $('.month-cash-credit').height()) - 54 + 20;
            //if (dataHeight) {
            //    $('#summary-chart2').css('min-height', dataHeight);
            //}

            $(".addon > ul").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px"
            });

            $('input[name="daterange"]').daterangepicker({
                "opens": "left",
                "startDate": moment().subtract(6, 'days'),
                "endDate": moment(),
                "alwaysShowCalendars": false,
                locale: {
                    format: 'DD MMM YYYY'
                },
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            });

            //Initialise all charts
            var summaryChart1 = $("#summaryChart1");
            var summaryChart2 = $("#summaryChart2");
            var summaryChart3 = $("#summaryChart3");

            $.ajax({
                url: $('#hdApiUrl').val() + 'api/AdminDashBoard/InitialiseAllCharts?From=&To=',
                method: 'GET',
                dataType: 'JSON',
                success: function (response) {
                    summaryChart1 = new Chart(summaryChart1, {
                        type: 'bar',
                        data: response[1],
                        options: {
                            legend: {
                                position: 'top',
                                labels: {
                                    usePointStyle: true
                                }
                            },
                            scales: {
                                yAxes: [{
                                    gridLines: {
                                        color: 'rgba(0,0,0,0.05)',
                                    }
                                }],
                                xAxes: [{
                                    ticks: {
                                        beginAtZero: true,
                                        autoSkip: true,
                                    },
                                    gridLines: {
                                        display: false,
                                        drawOnChartArea: false
                                    }
                                }]
                            }
                        }
                    });

                    summaryChart2 = new Chart(summaryChart2, {
                        type: 'doughnut',
                        data: response[2],
                        options: {
                            legend: {
                                position: 'right',
                                labels: {
                                    usePointStyle: true
                                }
                            }
                        }
                    });

                    summaryChart3 = new Chart(summaryChart3, {
                        type: 'line',
                        data: response[0],
                        options: {
                            legend: {
                                position: 'top',
                                labels: {
                                    usePointStyle: true
                                }
                            },
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                            }
                        }
                    });


                    //putting data to cards
                    if (response[3] != null) {
                        var currencySymbol = JSON.parse($('#hdSettings').val()).CurrencySymbol;
                        $('#cardTotalSales').text(currencySymbol + ' ' + parseInt(response[3].TotalSales));
                        $('#cardCashSales').text(currencySymbol + ' ' + parseInt(response[3].CashSales));
                        $('#cardCreditSales').text(currencySymbol + ' ' + parseInt(response[3].CreditSales));
                        $('#cardCashSalesWeek').text(currencySymbol + ' ' + parseInt(response[3].CashSalesLastLast7Days));
                        $('#cardCreditSalesWeek').text(currencySymbol + ' ' + parseInt(response[3].CreditSalesLast7Days));
                        $('#cardCashSalesMonth').text(currencySymbol + ' ' + parseInt(response[3].CashSalesLast30Days));
                        $('#cardCreditSalesMonth').text(currencySymbol + ' ' + parseInt(response[3].CreditSalesLast30Days));
                        $('#cardNewCustomersP').text(response[3].NewCustomerPercentage + '%');
                        $('#cardNewCustomers').text(response[3].NewCustomer);
                        $('#cardReturningCustomersP').text(response[3].ReturningCustomerPercentage + '%');
                        $('#cardReturningCustomers').text(response[3].ReturningCustomer);
                        $('#lblTotalReceivable').text(currencySymbol + ' ' + parseInt(response[3].TotalReceivable));
                        $('#lblTotalPayable').text(currencySymbol + ' ' + parseInt(response[3].TotalPayable));
                        updateDonutProgress('#newCustomerProgress', response[3].NewCustomerPercentage, true);
                        updateDonutProgress('#returningCustomerProgress', response[3].ReturningCustomerPercentage, true);
                    }
                },
                beforeSend: function () { loading('start') },
                complete: function () { loading('stop') },
                error(xhr) {
                    alert(xhr.responseText);
                    console.log(xhr);
                }
            });
              
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/AdminDashBoard/ActivityLog',
                method: 'GET',
                dataType: 'JSON',
                success: function (activities) {

                    var html = '';
                    $(activities).each(function (index) {
                        html += '<li class="clearfix">';
                        html += '<div class="activity-round" style="background-color:' + this.CodeColor + '">' + this.Code + '</div>';
                        html += '<div class="legend-info">' + this.Activity + '<br/>';
                        html += '<a href="' + this.Url + '"><small><i>#</i>&nbsp;' + this.Reference + '</small></a>';
                        html += '<small><i class="md md-today"></i>&nbsp;' + this.Date + '</small>';
                        html += '<small><i class="md md-face-unlock"></i>&nbsp;' + this.User + '</small>';
                        html += '</div>';
                        html += '<span class="time-stamp">' + this.Time + '</span>';
                        html += '</li>';
                    });

                    $('#activitySection').children('li').remove();
                    $('#activitySection').append(html);
                },
                beforeSend: function () { loading('start') },
                complete: function () { loading('stop') },
                error: function (xhr) {
                    alert(xhr.responseText);
                    console.log(xhr);
                }
            });

            //Refresh Cash Vs Credit Vs Expenses
            $('#refreshSummaryChart1').off().click(function () {
                var fromDate = $('#dateSummaryChart1').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                var toDate = $('#dateSummaryChart1').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/AdminDashBoard/CashVsCreditVsExpense?From=' + fromDate + '&To=' + toDate,
                    method: 'GET',
                    dataType: 'JSON',
                    success: function (response) {
                        summaryChart1.data = response;
                        summaryChart1.update();
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });
            $('#dateSummaryChart1').on('apply.daterangepicker', function (ev, picker) {
                $('#refreshSummaryChart1').trigger('click');
            });
            //Refresh Purchase VS Sales
            $('#refreshSummaryChart3').off().click(function () {
                var fromDate = $('#dateSummaryChart3').data('daterangepicker').startDate.format('YYYY-MMM-DD');
                var toDate = $('#dateSummaryChart3').data('daterangepicker').endDate.format('YYYY-MMM-DD');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/AdminDashBoard/SalesVsPurchase?From=' + fromDate + '&To=' + toDate,
                    method: 'GET',
                    dataType: 'JSON',
                    success: function (response) {
                        summaryChart3.data = response;
                        summaryChart3.update();
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });

            });
            $('#dateSummaryChart3').on('apply.daterangepicker', function (ev, picker) {
                $('#refreshSummaryChart3').trigger('click');
            });
            //Download Options
            $('#downloadSummaryChart3').off().click(function () {
                download(document.getElementById('summaryChart3').toDataURL('image/png'), 'Cash Vs Credit Vs Expenses', 'image/png')
            });
            $('#downloadSummaryChart2').off().click(function () {
                download(document.getElementById('summaryChart2').toDataURL('image/png'), 'Purchase VS Sales', 'image/png')
            });
            $('#downloadSummaryChart1').off().click(function () {
                download(document.getElementById('summaryChart1').toDataURL('image/png'), 'Purchase Bifurcation', 'image/png')
            });
        });


        //Setting Dynamic Activity Height
        setInterval(function () {
            var activityHeight = Math.abs($('.addon').offset().top - $('.sale-purchase-charts').offset().top - $('.sale-purchase-charts').height()) - 52;
            if (activityHeight) {
                //$('#activitySection').css('min-height', activityHeight);
                $('#activitySection').css('max-height', activityHeight);
            }
        }, 500);

    </script>
    <script src="../Plugins/Download/download.js"></script>
</asp:Content>
