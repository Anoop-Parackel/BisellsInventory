<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="BisellsERP.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Reports</title>
    <style>
        .btn-wrap {
                height: 100px;
    border: 1px solid #ccc;
    padding: 10px;
    text-align: center;
        }
        .panel {
            background-color: transparent;
            box-shadow: none;
        }
        .panel-default > .panel-heading {
            color: #fff;
            background-color: #B0BEC5;
        }



        @charset "utf-8";
/* CSS Document */

/*.hdr-fx{
	right: 0;
    background-color: #FFF;
    z-index: 99999;
    position: fixed;
    left: 0;
    top: 0;
	
	}*/

.report{ margin-top:50px;}



.top-bar{
	border-bottom: 1px solid #EEE;
    /*height: 65px;*/
	
	}


.report-title

{
	color:#222222;
	font-family: "Raleway",sans-serif;
    font-size: 24px;
    font-weight: 400;
	
	
	}		
.srch-hldr

{
	margin-top:8px;
	margin-bottom:8px;
	
	}	
	
.srch{ color:#b6b6b6;}	



.report-sec ul{
    list-style:none;
    padding:0;
}

.report-sec h3 {
color: #222222;
    font-family: 'Roboto', sans-serif;
	   font-size: 20px;
  text-align: left;
  font-weight:300;



}

.report-sec ul li a:hover, a:focus {
	text-decoration:none;
	
}


.report-sec ul li {
		 border-bottom: 1px dashed #E9E9E9;
		 font-family: 'Roboto', sans-serif;
		 /*font-family: 'Raleway', sans-serif;*/
		 padding: 6px 5px 7px 4px;
	/*	 padding: 5px 5px 5px 8px;*/
		 line-height: 24px;
		 font-size: 15px;
		 color: #428dcc;
		 font-weight:300;
		 /*  border-bottom: 1px solid #3e737b;*/
		 /*border-bottom: 1px solid #9aa1a2;*/
}
.report-sec ul li:last-child {
  border: 0;
}
.report-sec ul li .fa {
  padding-right: 10px;
  color: #777777;
}

/*.panel:hover {
  border: 1px solid #30bb57;
}*/
 .sliderbtm {
  bottom: 0;
  cursor: pointer;
}

.report-hldr{margin-bottom: 30px;
    max-width: 225px;}
	
	
	
	
.icon-small i {
  float: left;
    /* margin-right: 10px; */
    /* height: 38px; */
    /* width: 38px; */
    color: #fff;
    /* background-color: #888; */
    /* border-radius: 10%; */
    text-align: center;
    vertical-align: middle;
    padding-top: 12px;
}


.icon-small h4, {
  text-align: left;
  color: #222222;
    font-family: 'Roboto', sans-serif;
	   font-size: 20px;
  text-align: left;
  font-weight:300;
}
	
	@media screen and (max-width: 480px) {
		.select2-container{
	width:98% ! !important;
}
.report{ margin-top:75px;}		
		
		}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

<h2>Reports</h2>
<hr></hr>

<section class="report">
   <div class="container">
      <!--first row-->
      <div class="row">
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-user"></i></span>Accounts</h3>
                  <ul>
		     <li><a href="/reports/adhoc?reportuid=25"><i class="fa fa-angle-right"></i>Account Receviable</a></li>
 <li><a href="/reports/adhoc?reportuid=26"><i class="fa fa-angle-right"></i>Account Payable</a></li>
                     <li><a href="/Finance/FinancialTransactions"><i class="fa fa-angle-right"></i>Transactions</a></li>
                     <li><a href="/Finance/DailyStatement"><i class="fa fa-angle-right"></i>Daily Statement</a></li>
                     <li><a href="/Finance/Ledger"><i class="fa fa-angle-right"></i>Ledger</a></li>
                     <li><a href="/Finance/FinalAccounts"><i class="fa fa-angle-right"></i>Trial Balance</a></li>
                     <li><a href="/Finance/FinalAccounts"><i class="fa fa-angle-right"></i>Final Accounts</a></li>
                     <li><a href="/Finance/FinalAccounts"><i class="fa fa-angle-right"></i>Finance Reports</a></li>

                  </ul>
               </div>
            </div>
         </div>
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-calendar-o"></i></span>Product</h3>
                  <ul>
                     <li><a href="/reports/adhoc?reportuid=13"><i class="fa fa-angle-right"></i>Price List</a></li>
                     <li><a href="/reports/adhoc?reportuid=13"><i class="fa fa-angle-right"></i>Stock Report</a></li>
                     <li><a href="/reports/adhoc?reportuid=11"><i class="fa fa-angle-right"></i>Product Details</a></li>
                  </ul>
               </div>
            </div>
         </div>
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-cart-plus"></i></span>Sales</h3>
                  <ul>

                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>Sales Summary</a></li>
                     <li><a href="/reports/adhoc?reportuid=8"><i class="fa fa-angle-right"></i>Sales Return</a></li>
                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>Customer Sales</a></li>
                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>Despatch Report</a></li>
                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>DSR</a></li>
                  </ul>
               </div>
            </div>
         </div>
      </div>
      <!--first row end-->
      <!--second row start-->
      <div class="row">
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-clock-o"></i></span>Ageing</h3>
                  <ul>
                     <li><a href="/reports/adhoc?reportuid=24"><i class="fa fa-angle-right"></i>Customer Ageing</a></li>
                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>Supplier Ageing</a></li>
                     <li><a href="/reports/adhoc?reportuid=6"><i class="fa fa-angle-right"></i>Product Ageing</a></li>
                  </ul>
               </div>
            </div>
         </div>
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-shopping-cart"></i></span>Purchase</h3>
                  <ul>
                     <li><a href="/reports/adhoc?reportuid=2"><i class="fa fa-angle-right"></i>Purchase Summary</a></li>
                     <li><a href="/reports/adhoc?reportuid=2"><i class="fa fa-angle-right"></i>Purchase Analysis</a></li>
                     <li><a href="/reports/adhoc?reportuid=4"><i class="fa fa-angle-right"></i>Purchase Return</a></li>
                     <li><a href="/reports/adhoc?reportuid=2"><i class="fa fa-angle-right"></i>Purchase Comparison</a></li>
        
                  </ul>
               </div>
            </div>
         </div>
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-user-plus"></i></span>Admin</h3>
                  <ul>
                     <li><a href="/Party/Suppliers"><i class="fa fa-angle-right"></i>Supplier</a></li>
                     <li><a href="/reports/adhoc?reportuid=20"><i class="fa fa-angle-right"></i>Customer Vise Profit</a></li>
                     <li><a href="/reports/adhoc?reportuid=22"><i class="fa fa-angle-right"></i>Bill Vise Profit</a></li>
                     <li><a href="/reports/adhoc?reportuid=23"><i class="fa fa-angle-right"></i>Product Vise Profit</a></li>
                     <li><a href="/party/Customers"><i class="fa fa-angle-right"></i>Customers</a></li>
                  </ul>
               </div>
            </div>
         </div>
      </div>
      <!--second row end-->
      <!--3d row start-->
      <div class="row">
         <div class="col-md-4">
            <div class="report-hldr">
               <div class="report-sec">
                  <h3><span style="margin-right:10px;"><i class="fa fa-money"></i></span>KVAT</h3>
                  <ul>
                     <li><a href="/reports/adhoc?reportuid=17"><i class="fa fa-angle-right"></i>Purchase Tax Return</a></li>
                     <li><a href="/reports/adhoc?reportuid=18"><i class="fa fa-angle-right"></i>Purchase Return Tax Reg</a></li>
                     <li><a href="/reports/adhoc?reportuid=19"><i class="fa fa-angle-right"></i>Sales Return Tax Reg</a></li>
                     <li><a href="/reports/adhoc?reportuid=16"><i class="fa fa-angle-right"></i>Sales Tax Register</a></li>
                  </ul>
               </div>
            </div>
         </div>
      </div>
      <!--3r row end-->
   </div>
</section>


<!--<script>
var catAndActs = {};
catAndActs['Classroom Instruction and Assessment'] = ['Assessment Day', 'Common Assessment Development', 'Data Team', 'Kindergarten Screening', 'Other'];
catAndActs['Curriculum Development and Alignment'] = ['Capstone Development', 'Course Of Study Development / Revision', 'Standards Alignment / Rollout', 'Other'];
catAndActs['District Committee'] = ['Curriculum Council', 'Grading & Assessment Task Force', 'Professional Development Planning Committee', 'Race To The Top Committee', 'Teacher Evaluation Committee', 'Other'];
catAndActs['Meeting'] = ['Academic Support Team', 'ELL / eKLIP Teachers', 'Gifted Intervention Specialist', 'Intervention Assistance Team', 'Intervention Teachers', 'Kindergarten Parent Conference', 'KLIP Teachers', 'Title I Teachers', 'Other'];
catAndActs['Other Category'] = ['Other'];
catAndActs['Professional Conference'] = ['Conference'];
catAndActs['Professional Workshop / Training'] = ['In-District', 'Out-Of-District'];
catAndActs['Pupil Services'] = ['IEP Meeting', 'IEP Writing'];

function ChangecatList() {
    var catList = document.getElementById("validationCustom03");
    var actList = document.getElementById("validationCustom04");
    var selCat = catList.options[catList.selectedIndex].value;
    while (actList.options.length) {
        actList.remove(0);
    }
    var cats = catAndActs[selCat];
    if (cats) {
        var i;
        for (i = 0; i < cats.length; i++) {
            var cat = new Option(cats[i], i);
            actList.options.add(cat);
        }
    }
} 
</script>-->
<script>
$(document).ready(function() {
    $('.js-example-basic-single').select2();
});
</script><!--search-->
<script src="js/select2.min.js"></script><!--search-->

    <div style="display:none" class="row">
        <div class="col-sm-6">

        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">Purchase Reports</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6">
                        <a href="/reports/adhoc?reportuid=2">
                            <div class="mini-stat clearfix bx-shadow">
                                <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                <div class="mini-stat-info text-right text-muted">
                                    <span class="counter">Purchase Report</span>
                                    Short Description
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="col-sm-6">
                        <a href="/reports/adhoc?reportuid=4">
                            <div class="mini-stat clearfix bx-shadow">
                                <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                <div class="mini-stat-info text-right text-muted">
                                    <span class="counter">Purchase Return</span>
                                    Short Description
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
        </div>

        <div class="col-sm-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Sales Reports</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=6">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Sales Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=8">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Sales Return Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-sm-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Other Reports</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=9">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Customer Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=10">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Supplier Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-sm-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Inventory Reports</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=11">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Product Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-sm-6">
                            <a href="/reports/adhoc?reportuid=13">
                                <div class="mini-stat clearfix bx-shadow">
                                    <span class="mini-stat-icon bg-info"><i class="fa fa-usd"></i></span>
                                    <div class="mini-stat-info text-right text-muted">
                                        <span class="counter">Stock Report</span>
                                        Short Description
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div></div>


</asp:Content>
