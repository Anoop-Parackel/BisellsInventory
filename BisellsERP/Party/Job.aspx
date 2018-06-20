<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Job.aspx.cs" Inherits="BisellsERP.Party.Job" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .task-post .post-options {
            margin-top: 15px;
        }

            .task-post .post-options a:first-child {
                margin-left: 3px;
            }

            .task-post .post-options a {
                display: inline-block;
                margin-left: 20px;
            }

                .task-post .post-options a:hover i {
                    color: #3CBA9F;
                }

                .task-post .post-options a i {
                    font-size: 22px;
                    color: #BDBDBD;
                }

        .task-post > input {
            border-bottom: none;
            border-radius: 3px 3px 0 0;
        }

        .task-post > textarea {
            border-bottom: none;
            resize: vertical;
            border-radius: 0;
            border-top: 1px dashed #eee;
        }

            .task-post > textarea:hover, .task-post > textarea:focus, .task-post > input:hover, .task-post > input:focus {
                box-shadow: none;
                border-color: #e4e4e4;
            }

        .attachment-container {
            height: 30px;
            padding: 2px;
            border: 1px solid #e4e4e4;
            border-top-color: #eee;
            border-radius: 0 0 3px 3px;
            background-color: #FAFAFA;
        }

            .attachment-container .attachment-holder {
                display: inline-block;
                height: 24px;
                width: 24px;
                margin-right: 3px;
                border-radius: 2px;
                overflow: hidden;
                position: relative;
            }

                .attachment-container .attachment-holder .remove-attachment {
                    position: absolute;
                    top: 0;
                    left: 0;
                    display: inline-block;
                    height: 100%;
                    width: 100%;
                    background-color: hsla(0, 0%, 0%, 0.5);
                    color: #F44336;
                    text-align: center;
                    cursor: pointer;
                    opacity: 0;
                    transform: scale(0);
                    transition: all .3s ease;
                }

                .attachment-container .attachment-holder:hover .remove-attachment {
                    opacity: 1;
                    transform: scale(1);
                }

                .attachment-container .attachment-holder img {
                    height: 100%;
                    width: 100%;
                    object-fit: cover;
                }

        .tag-employee-input-wrap {
            display: inline;
            position: relative;
            left: 10px;
            top: -2px;
            visibility: hidden;
            opacity: 0;
            transition: all .3s ease;
        }

            .tag-employee-input-wrap.in {
                visibility: visible;
                opacity: 1;
            }

            .tag-employee-input-wrap input {
                width: 130px;
                border-radius: 3px 0 0 3px;
                border: 1px solid #e4e4e4;
                border-right: 0;
                padding: 3px 6px;
            }

            .tag-employee-input-wrap span {
                padding: 0 5px;
            }

                .tag-employee-input-wrap span:hover {
                    background-color: #efefef;
                }
            .tag-employee-input-wrap .selectize-control.multi .selectize-input > div {
                -webkit-border-radius: 10px;
                -moz-border-radius: 10px;
                border-radius: 10px;
            }

        .mini-stat {
            padding: 10px 20px;
        }


        .profile-timeline ul li .timeline-item-header {
            width: 100%;
            /*overflow: hidden;*/
        }

            .profile-timeline ul li .timeline-item-header img {
                width: 40px;
                height: 40px;
                float: left;
                margin-right: 10px;
                border-radius: 50%;
                border: 1px solid #eee;
                background-color: #F5F5F5;
            }

            .profile-timeline ul li .timeline-item-header p {
                margin: 0;
                color: #000;
                font-weight: 500;
            }

            .profile-timeline ul li .timeline-item-header > p.timeline-post-info {
                display: inline-block;
                width: calc(100% - 50px - 60px);
            }

            .profile-timeline ul li .timeline-item-header p span {
                margin: 0;
                color: #8E8E8E;
                font-weight: 400;
            }

            .profile-timeline ul li .timeline-item-header small {
                margin: 0;
                color: #8E8E8E;
            }

            .profile-timeline ul li .timeline-item-header .action-menu {
                font-size: 120%;
            }

            .profile-timeline ul li .timeline-item-header .dropdown {
                display: inline-block;
                width: 15px;
            }

            .profile-timeline ul li .timeline-item-header .open > .dropdown-menu {
                display: block;
                left: calc(-160px + 10px);
            }

        .profile-timeline ul li .timeline-item-post {
            padding: 15px 0 0;
            position: relative;
        }

            .profile-timeline ul li .timeline-item-post > p {
                color: #757575;
                text-align: justify;
            }


            .profile-timeline ul li .timeline-item-post .image-wrap {
                width: 100%;
            }

                .profile-timeline ul li .timeline-item-post .image-wrap img {
                    width: calc(16.2% - 3px);
                    margin-top: 5px;
                    margin-right: 3px;
                }

            .profile-timeline ul li .timeline-item-post p {
                margin-bottom: 15px;
            }

        .profile-timeline ul li.timeline-item .panel-body {
            padding: 15px 20px 5px;
        }

        .profile-timeline .assign-wrap {
            border-top: 1px dashed #E0E0E0;
            margin-top: 10px;
        }

            .profile-timeline .assign-wrap ul li {
                display: inline-block;
                height: 20px;
                margin-right: 5px;
                border-right: 1px solid #eee;
                padding: 0 5px;
                margin-top: 5px;
                font-size: 12px;
                color: #9E9E9E;
            }

                .profile-timeline .assign-wrap ul li img {
                    height: 15px;
                    width: 15px;
                    border-radius: 50%;
                    margin-right: 5px;
                }

        /* mini-stat new style */
        .mini-stat-info {
            font-size: 12px;
        }

            .mini-stat-info span {
                font-size: 18px;
            }
        /* Donut Progress Styling*/
        .mini-stat.progress-donut {
            padding: 13px 20px;
        }

        .mini-stat-info.job-progress {
            display: inline-block;
            float: right;
        }

        .donut-size {
            font-size: 2em;
            display: inline-block;
        }

        .pie-wrapper {
            position: relative;
            width: 6rem;
            height: 6rem;
            margin: 0 0 0 auto;
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
                top: 50%;
                left: 50%;
                transform: translate(-43%,-50%);
                display: block;
                background: none;
                border-radius: 50%;
                color: #7F8C8D;
                font-size: 0.45em;
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
                border: 0.2em solid #ccc;
                border-radius: 50%;
            }

        .panel-default > .panel-heading {
            background-color: #eff7f5;
        }

        .job-action-grp .btn.btn-default {
            color: #455A64;
        }

        .content-page {
            min-height: 0;
        }

        .task-scroll {
            max-height: calc(100vh - 135px);
        }

        .multi-steps-wrap .multi-steps > li.is-active:before, .multi-steps-wrap .multi-steps > li.is-active ~ li:before {
            content: counter(stepNum);
            font-family: inherit;
            font-weight: 700;
        }

        .multi-steps-wrap .multi-steps > li.is-active:after, .multi-steps-wrap .multi-steps > li.is-active ~ li:after {
            background-color: #d4d4d4;
        }

        .multi-steps-wrap {
            background-color: #fff;
            padding: 15px 0 0;
            margin: 0;
        }

        .date-range-text {
            font-size: 13px;
            margin-left: 50px !important;
            font-weight: 600 !important;
        }

        @media (max-width: 576px) {
            .multi-steps-wrap {
                margin: 0;
            }
        }

        .multi-steps-wrap .multi-steps {
            display: table;
            table-layout: fixed;
            width: 100%;
        }

            .multi-steps-wrap .multi-steps > li {
                counter-increment: stepNum;
                text-align: center;
                display: table-cell;
                position: relative;
                color: #3cba9f;
                font-size: 12px;
                z-index: 1;
            }

                .multi-steps-wrap .multi-steps > li:before {
                    content: '✓';
                    display: block;
                    margin: 0 auto 4px;
                    background-color: #fff;
                    width: 46px;
                    height: 46px;
                    line-height: 40px;
                    text-align: center;
                    font-size: 18px;
                    border-width: 2px;
                    border-style: solid;
                    border-color: #3cba9f;
                    border-radius: 50%;
                }

                .multi-steps-wrap .multi-steps > li:after {
                    content: '';
                    height: 2px;
                    width: 100%;
                    background-color: #3cba9f;
                    position: absolute;
                    top: 20px;
                    left: 50%;
                    z-index: -1;
                }

                .multi-steps-wrap .multi-steps > li:last-child:after {
                    display: none;
                }

                .multi-steps-wrap .multi-steps > li:last-child.is-active:before {
                    content: '✓';
                }

                .multi-steps-wrap .multi-steps > li:first-child.is-active:before {
                    content: '✓';
                }

                .multi-steps-wrap .multi-steps > li.is-active:before {
                    background-color: #fff;
                    border-color: #3cba9f;
                }

                .multi-steps-wrap .multi-steps > li.is-active ~ li {
                    color: #808080;
                }

                    .multi-steps-wrap .multi-steps > li.is-active ~ li:before {
                        background-color: #eee;
                        border-color: #fff;
                    }

        .fileUpload {
            display: inline-block;
            cursor: pointer;
            overflow: hidden;
            height: 22px;
        }

            .fileUpload:hover i {
                color: #3CBA9F;
            }

            .fileUpload input {
                position: absolute;
                top: 0;
                left: 0;
                width: 0;
                padding-left: 16px;
                height: 16px;
                margin: 6px 0 0 6px;
                cursor: pointer;
            }

            .fileUpload i {
                font-size: 20px;
                color: #bdbdbd;
                margin-right: 0;
                margin-left: 3px;
            }


        .panel-body {
            position: relative;
        }

        .label.custom {
            margin-left: 50px !important;
            color: #fff !important;
            letter-spacing: .5px;
            font-size: 10px;
            padding: .1em .4em .2em;
        }

        .modal .modal-dialog .modal-content {
            padding: 20px 30px;
        }

        .address-panel h5 {
            font-size: 16px;
            color: #9a9a9a;
        }

        .address-panel p {
            font-size: 12px;
        }

        .job-dates {
            padding: 5px 0;
            background-color: #FAFAFA;
            border-radius: 20px;
            text-align: center;
            color: #9E9E9E;
            text-transform: uppercase;
            border: 1px solid #eee;
        }

            .job-dates > span:last-child {
                font-weight: 700;
                color: #3cba9f;
            }

        .panel-title {
            font-weight: 100;
        }

            .panel-title > span {
                font-weight: 700;
                color: slategray;
            }

        .selectize-control {
            left: -10px;
            top: 10px;
        }

        .selectize-input {
            border-radius: 0 !important;
            border: 1px solid #e4e4e4 !important;
        }

            .selectize-input.focus {
                box-shadow: none !important;
            }

        .selectize-control img {
            height: 18px;
            width: 18px;
            border-radius: 50%;
            margin-right: 5px;
        }

        .edit-task, .delete-task {
            opacity: .7;
        }

            .edit-task:hover, .delete-task:hover {
                opacity: 1;
            }

        #txtTaskStartDate, #txtTaskEndDate {
            display: inline;
            width: 100px;
        }

        #btnAddTask {
            padding: 5px 12px;
        }
        .task-add-image {
            position: relative;
            overflow: hidden;
        }
        .add-more-image {
            position: absolute;
            top: 0;
            opacity: 0;
            height: 100%;
            cursor: pointer;
        }
        [contenteditable="true"] {
            background-color: #FAFAFA;
            border: 1px solid #eee;
            padding: 0 6px;
            border-radius: 3px;
        }
        .profile-timeline ul li .timeline-item-post .image-wrap {
            
        }

        .profile-timeline ul li .timeline-item-post .image-wrap > div {
            display: inline;
            position: relative;
        }

            .profile-timeline ul li .timeline-item-post .image-wrap > div > span {
                position: absolute;
                left: 0;
                right: 0;
                top: 0;
                display: inline-block;
                width: 20px;
                background-color: rgba(244, 67, 54, 0.7);
                text-align: center;
                margin: auto;
                border-radius: 50px;
                color: #fff;
                cursor: pointer;
                opacity: 0;
            }
.profile-timeline ul li .timeline-item-post .image-wrap > div:hover > span {
    opacity: 1;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <input type="hidden" value="0" />
    <%-- ---- Page Title ---- --%>
    <div class="row p-b-10">
        <div class="col-sm-3">
            <h3 class="page-title m-t-0 job-name">xxx</h3>
        </div>
    </div>

    <div class="row">
        <div class="col-md-5 task-scroll">
            <%-- ---- Job Timeline ---- --%>
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-white">
                        <div class="panel-body p-b-10">
                            <div class="task-post">
                                <input id="txtTaskTitle" type="text" class="form-control input-sm" placeholder="Title" />
                                <textarea id="txtTaskDesc" class="form-control" placeholder="Description" rows="4"></textarea>
                                <div class="attachment-container">
                                </div>
                                <div class="post-options">
                                    <div class="fileUpload btn-upload">
                                        <span><i class="ion-image m-r-5"></i></span>
                                        <input id="imageUploader" type="file" accept="image/*" />
                                    </div>
                                     <input type="hidden" id="hdTaskId" value="0" />
                                    <a href="#"><i class="ion ion-paperclip"></i></a>
                                    <%--<a href="#"><i class="ion ion-location"></i></a>--%>
                                    <a href="#" class="tag-employee"><i class="ion ion-person-add"></i></a>
                                    <button type="button" id="btnAddTask" class="btn btn-default btn-custom btn-sm waves-effect pull-right">Add Task</button>
                                    <div class="pull-right">
                                        <input id="txtTaskStartDate" type="text" class="form-control input-sm" placeholder="Start Date" />
                                        <input id="txtTaskEndDate" type="text" class="form-control input-sm m-r-5" placeholder="End Date" />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="tag-employee-input-wrap">
                                    <input id="empSearch" type="text" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="profile-timeline">
                        <ul id="taskList" class="list-unstyled">
                        </ul>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-7">
            <%-- ---- Job Widgets ---- --%>
            <div class="row">
                <input type="hidden" id="hdId" value="0" />
                <div class="col-md-4">
                    <div class="mini-stat clearfix bx-shadow">
                        <span class="mini-stat-icon bg-green"><i class="ion-social-usd"></i></span>
                        <div class="mini-stat-info text-right text-muted">
                            <span id="lblCard1Value" class="counter">xxx</span>
                            Total Invoices
                        </div>
                        <div class="tiles-progress">
                            <div class="m-t-20">
                                <h6 class="text-uppercase p-t-10 m-b-0">Total Invoiced Amt <span class="pull-right" id="lblCard4Value"></span></h6>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="mini-stat clearfix bx-shadow" style="padding-bottom: 23px;">
                        <span class="mini-stat-icon bg-green"><i class="ion-ios7-cart"></i></span>
                        <div class="mini-stat-info text-right text-muted">
                            <span id="lblCard2Value" class="counter">xxx</span>
                            Estimate Amt
                        </div>
                        <div class="tiles-progress">
                            <div class="m-t-20">
                                <h6 class="text-uppercase p-t-10 m-b-0"><span class="pull-right"></span></h6>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="mini-stat clearfix bx-shadow style="padding-bottom: 23px;"">

                        <div class="donut-size">
                            <div class="pie-wrapper">
                                <span class="label">
                                    <span id="lblCard3Value" class="num">xx</span><span class="smaller">%</span>
                                </span>
                                <div class="pie">
                                    <div class="left-side half-circle"></div>
                                    <div class="right-side half-circle"></div>
                                </div>
                                <div class="shadow"></div>
                            </div>
                        </div>

                        <div class="mini-stat-info text-right text-muted job-progress">
                            <span class="counter">Job</span>
                            Progress
                        </div>
                        <div class="tiles-progress">
                            <h6 class="text-uppercase m-b-0"><span class="pull-right"></span></h6>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="row multi-steps-wrap">
                        <ul class="list-unstyled multi-steps">
                            <li class="job-status job-created">Created</li>
                            <li class="job-status job-inprogress">Inprogress</li>
                            <li class="job-status job-completed">Completed</li>
                            <li class="job-status job-closed">Closed</li>
                            
                        </ul>
                    </div>
                </div>
            </div>


            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default m-b-0">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-sm-6">
                                    <%--<h3 class="panel-title job-name">xxx</h3>--%>
                                    <h3 class="panel-title">Customer : <span id="lblCustName"></span></h3>
                                </div>
                                <div class="col-sm-6">
                                    <div class="btn-toolbar pull-right job-action-grp">
                                        <button type="button" id="btnAddJob" class="btn btn-default waves-effect btn-xs">Create</button>
                                        <button type="button" id="btnEditJob" class="btn btn-default waves-effect btn-xs">Edit</button>
                                        <button type="button" id="btnDeleteJob" class="btn btn-default waves-effect btn-xs">Delete</button>
                                        <button type="button" class="btn btn-default waves-effect btn-xs"><i class="md md-mail"></i></button>
                                        <button type="button" class="btn btn-default waves-effect btn-xs"><i class="md md-local-print-shop"></i></button>
                                        <button type="button" class="btn btn-default btn-xs waves-effect waves-light dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><span class="caret"></span></button>
                                        <ul class="dropdown-menu" id="jobstatus">
                                            <li data-status="0" class="job-status-change"><a href="#">Open</a></li>
                                            <li class="job-status-change" data-status="1"><a href="#">In Progress</a></li>
                                            <li class="job-status-change" data-status="2"><a href="#">Completed</a></li>
                                            <li class="job-status-change" data-status="3"><a href="#">Closed</a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="panel-body address-panel">

                            <div class="row m-b-10">
                                <div class="col-sm-6">
                                    <p class="m-t-0 job-dates"><span>Commencing Date</span> : <span id="lblDate">&nbsp;</span></p>
                                </div>

                                <div class="col-sm-6 text-right">
                                    <p class="m-t-0 job-dates"><span>Completed Date</span> : <span id="lblCompletedDate">&nbsp;</span></p>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-6 dashed-b-r">
                                    <h5><i class="md md-home text-muted"></i>&nbsp;Contact Address</h5>
                                    <p class="m-t-0 text-green">
                                        <input type="hidden" id="hdCustomerId" value="0" />
                                        <span id="lblSalutation"></span> <span id="lblJobContact">&nbsp;</span>
                                    </p>
                                    <p class="m-t-0"><span id="lblJobAddr1">Address Line 1</span></p>
                                    <p class="m-t-0"><span id="lblJobAddr2">Address Line 2</span></p>
                                    <p class="m-t-0"><span id="lblJobCity">City</span> <span id="lblJobState"></span></p>
                                    <p class="m-t-0"><span id="lblJobCountry">Country</span> | <span id="lblJobZip">Zip Code</span></p>
                                    <p class="m-t-0"><b>Ph: </b><span id="lblJobPh1"></span> <span id="lblJobPh2"></span></p>
                                </div>

                                <div class="col-sm-6 text-right">
                                    <h5>Site Address&nbsp;<i class="md md-store text-muted"></i></h5>
                                    <p class="m-t-0 text-green">
                                        <input type="hidden" value="0" />
                                        <span id="lblSiteSalutation"></span> <span id="lblSiteContactName"></span>
                                    </p>
                                    <p class="m-t-0"><span id="lblSiteAddr1"></span></p>
                                    <p class="m-t-0"><span id="lblSiteAddr2"></span></p>
                                    <p class="m-t-0"><span id="lblSiteCity"></span> <span id="lblSiteState"></span></p>
                                    <p class="m-t-0"><span id="lblSiteCountry">Country</span> | <span id="lblSiteZip">Zip Code</span></p>
                                    <p class="m-t-0"><b>Ph: </b><span id="lblSitePh1"></span> <span id="lblSitePh2"></span></p>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>

        </div>


    </div>
    
    <%-- Job creation modal--%>
    <div id="jobModal" class="modal animated fadeIn" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                    <h4 class="modal-title">Job Creation&nbsp;</h4>
                </div>
                <div class="modal-body p-b-0 p-t-5">
                    <div class="row cust-form">

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Job</label>
                                <input type="text" id="txtJob" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Start Date</label>
                                <input type="text" id="txtStartDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Estimated End Date</label>
                                <input type="text" id="txtEndDate" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">Estimate Amount</label>
                                <input type="text" id="txtEstAmt" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Customer</label>
                                <asp:DropDownList ID="ddlCustomer" ClientIDMode="Static" CssClass="searchDropdown" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group hidden">
                                <label class="control-label">Contact Address</label>
                                <textarea id="txtContactAddress" class="form-control"></textarea>
                            </div>

                            <div class="row dashed-b-r m-t-15">
                                <div class="col-xs-12">
                                    <h4 class="m-t-0"><i class="md md-home text-muted"></i>&nbsp;Contact Address</h4>
                                </div>
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="control-label">Salutation</label>
                                        <select id="ddlSalutation" class="form-control input-sm">
                                            <option value="Mr">Mr </option>
                                            <option value="Mrs">Mrs </option>
                                            <option value="Ms">Ms </option>
                                            <option value="Miss">Miss </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <div class="form-group">
                                        <label class="control-label">Contact Person</label>
                                        <input type="text" id="txtContactPers" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 1</label>
                                        <textarea rows="2" id="txtContAddr1" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 2</label>
                                        <textarea rows="2" id="txtContAddr2" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="text" id="txtCity" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Zip</label>
                                        <input type="number" id="txtZip" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlCountry" CssClass="form-control input-sm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">State</label>
                                        <select class="form-control input-sm" id="ddlState">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 1</label>
                                        <input type="number" id="txtPhone1" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 2</label>
                                        <input type="number" id="txtPhone2" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="text" id="txtEmail" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>


                        </div>
                        <div class="col-sm-6">
                            <div class="form-group hidden">
                                <label class="control-label">Site Address</label>
                                <textarea id="txtSiteAddress" class="form-control"></textarea>
                            </div>


                            <div class="row m-t-15">
                                <div class="col-xs-12">
                                    <h4 class="m-t-0"><i class="md md-store text-muted"></i>&nbsp;Site Address</h4>
                                </div>
                                <div class="col-xs-3">
                                    <div class="form-group">
                                        <label class="control-label">Salutation</label>
                                        <select id="ddlSiteSalutation" class="form-control input-sm">
                                            <option value="Mr">Mr </option>
                                            <option value="Mrs">Mrs </option>
                                            <option value="Ms">Ms </option>
                                            <option value="Miss">Miss </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-9">
                                    <div class="form-group">
                                        <label class="control-label">Contact Person</label>
                                        <input type="text" id="txtSiteContPer" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 1</label>
                                        <textarea rows="2" id="txtSiteAddr1" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Address Line 2</label>
                                        <textarea rows="2" id="txtSiteAddr2" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">City</label>
                                        <input type="text" id="txtSiteCity" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Zip</label>
                                        <input type="number" id="txtSiteZip" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Country</label>
                                        <asp:DropDownList ID="ddlSiteCountry" CssClass="form-control input-sm" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">State</label>
                                        <select class="form-control input-sm" id="ddlSiteState">
                                            <option value="0">--Select--</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 1</label>
                                        <input type="number" id="txtSitePh1" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Phone 2</label>
                                        <input type="number" id="txtSitePh2" class="form-control input-sm" />
                                    </div>
                                </div>
                                <div class="col-xs-6">
                                    <div class="form-group">
                                        <label class="control-label">Email</label>
                                        <input type="text" id="txtSiteEmail" class="form-control input-sm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 pull-right">
                            <div class="btn-toolbar pull-right m-t-15">
                                <button id="btnSaveJob" type="button" class="btn btn-default btn-sm btn-job">Create</button>
                                <button type="button" class="btn btn-inverse btn-sm" data-dismiss="modal" aria-hidden="true">x</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="../Theme/assets/selectize.js"></script>
    <script src="../Theme/assets/selectize.min.js"></script>
    <link href="../Theme/css/selectize.bootstrap3.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {

            /* Date and Due Date */
            $('#txtTaskStartDate, #txtTaskEndDate').datepicker({
                autoclose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });



            $('.tag-employee').click(function () {
                $('.tag-employee-input-wrap').toggleClass('in');
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Jobs/GetEmployeeDetails/',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        console.log(response);
                        var options = [];
                        for (var i = 0; i < response.length; i++) {
                            options.push({ id: response[i].ID, title: response[i].FirstName, img: response[i].PhotoPath })
                        }
                        $('#empSearch').selectize({
                            maxItems: null,
                            valueField: 'id',
                            labelField: 'title',
                            searchField: 'title',
                            create: false,
                            options: options,
                            render: {
                                option: function (item, escape) {
                                    return '<div class="option">' +
                                      '<img src="' + item.img + '" height="42" width="42">' +
                                      '<span>' + escape(item.title) + '</span>' +
                                      '</div>';
                                },
                                item: function (item, escape) {
                                    return '<div class="item">' +
                                    '<img src="' + item.img + '" height="42" width="42">' + '<span>' + escape(item.title) + '</span>' +
                                    '</div>';
                                }
                            }
                        });
                    }
                });
            });

            $(".task-scroll").niceScroll({
                cursorcolor: "#6d7993",
                cursorwidth: "6px",
                horizrailenabled: false
            });

            //Date initialization
            $('#txtStartDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',

                todayHighlight: true
            });
            $('#txtEndDate').datepicker({
                autoClose: true,
                format: 'dd/M/yyyy',
                todayHighlight: true
            });

            //Set Request Date to current date
            var date = new Date();
            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#txtStartDate').datepicker('setDate', today);
            $('#txtEndDate').datepicker('setDate', today);
            // Below script used for to close the date picker (auto close is not working properly)
            $('#txtStartDate').datepicker()
           .on('changeDate', function (ev) {
               $('#txtStartDate').datepicker('hide');
           });
            $('#txtEndDate').datepicker()
                    .on('changeDate', function (ev) {
                        $('#txtEndDate').datepicker('hide');
                    });

            //load State
            function LoadStates(stateId, country, state) {
                var selected = country.val();
                if (selected == 0) {
                    state.empty();
                    state.append("<option value='0'>--Select States--</option>")
                }
                else {
                    state.empty();
                    state.append("<option value='0'>--Select States--</option>")
                    $.ajax({
                        type: "POST",
                        url: $('#hdApiUrl').val() + "api/customers/GetStates?id=" + selected,
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify($.cookie("bsl_1")),
                        dataType: "json",
                        success: function (data) {
                            state.empty;
                            var html = '';
                            $.each(data, function () {
                                html += '<option value="' + this.StateId + '">' + this.State + '</option>';
                            });
                            state.append(html);
                            state.val(stateId);
                        },
                        failure: function () {
                            console.log("Error");
                        }
                    });
                }
            }

            //Load state function call when contact address country changed
            $('#ddlCountry').change(function () {
                LoadStates(0, $(this), $('#ddlState'));
            });

            //Load state function call when site address country changed
            $('#ddlSiteCountry').change(function () {
                LoadStates(0, $(this), $('#ddlSiteState'));
            });

            //reset modal
            $('#btnAddJob').click(function () {
                $('#txtJob').val('');
                $('#txtEstAmt').val('');
                $('#txtStartDate').val('');
                $('#txtEndDate').val('');
                $('#ddlSalutation').val('0');
                $('#ddlCustomer').select2('val','0');
                $('#txtContactPers').val('');
                $('#ddlSiteSalutation').val('0');
                $('#txtSiteContPer').val('');
                $('#txtContAddr1').val('');
                $('#txtContAddr2').val('');
                $('#txtSiteAddr1').val('');
                $('#txtSiteAddr2').val('');
                $('#txtCity').val('');
                $('#txtZip').val('');
                $('#txtSiteCity').val('');
                $('#txtSiteZip').val('');
                $('#ddlCountry').val('0');
                $('#ddlState').val('0');
                $('#ddlSiteCountry').val('0');
                $('#ddlSiteState').val('0');
                $('#txtPhone1').val('');
                $('#txtPhone2').val('');
                $('#txtSitePh1').val('');
                $('#txtSitePh2').val('');
                $('#txtEmail').val('');
                $('#txtSiteEmail').val('');
                $('#jobModal').modal({ backdrop: 'static', keyboard: false, show: true });
            });

            //Change task status
            $('#taskList').on('click', '.task-status-change', function () {
                var li = $(this).closest('li.timeline-item');
                var taskId = $(li).attr('data-instance');
                var status = $(this).closest('li').attr('data-status');
                var data = {};
                data.TaskId = taskId;
                data.Status = status;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Jobs/UpateTaskStatus',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            var label = $(li).find('.lbl-status');
                            label.removeClass('label-success').removeClass('label-info').removeClass('label-warning');
                            if (status == 0) {
                                label.addClass('label-info').html('Open Task');
                            }
                            else if (status == 1) {
                                label.addClass('label-warning').html('In Progress');
                            }
                            else {
                                label.addClass('label-success').html('Completed');
                            }
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        complete: miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            })

            //Load job details
            var settings = JSON.parse($('#hdSettings').val());
            var qsData = getUrlVars();
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/Jobs/GetJob?job_id=' + qsData.UID,
                method: 'POST',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    $('.job-name').text(data.JobName);
                    $('#lblCard1Value').text(data.TotalInvoices);
                    $('#lblCard2Value').text(settings.CurrencySymbol + ' ' + data.EstimatedAmount);
                    $('#lblCard3Value').text(((data.TotalCompletedTasks / data.TotalTasks) * 100).toFixed(0));
                    $('#lblCard4Value').text(settings.CurrencySymbol + ' ' + data.TotalInvoiceAmount);
                    $('#lblCustomer').text(data.Customer);
                    $('#hdCustomerId').val(data.CustomerId);
                    $('#ddlCustomer').select2('val',data.CustomerId);
                    $('#lblCustName').text(data.Customer);
                    $('#lblSalutation').text(data.Salutation);
                    $('#lblSiteSalutation').text(data.SiteSalutation);
                    $('#lblJobContact').text(data.ContactName);
                    $('#lblSiteContactName').text(data.SiteContactName);
                    $('#lblJobAddr1').text(data.ContactAddress);
                    $('#lblSiteAddr1').text(data.SiteAddress);
                    $('#lblJobAddr2').text(data.ContactAddress2);
                    $('#lblSiteAddr2').text(data.SiteContactAddress2);
                    if (data.ContactCity != null) {
                        $('#lblJobCity').text(data.ContactCity); +"," + $('#lblJobState').text(data.State);
                    }
                    else {
                        $('#lblJobState').text(data.State);
                    }
                    if (data.SiteContactCity != null) {
                        $('#lblSiteCity').text(data.SiteContactCity); +',' + $('#lblSiteState').text(data.SiteState);
                    }
                    else {
                        $('#lblSiteState').text(data.SiteState);
                    }
                    if (data.ContactPhone1 != null) {
                        $('#lblJobPh1').text(data.ContactPhone1); +',' + $('#lblJobPh2').text(data.ContactPhone2);
                    }
                    else {
                        $('#lblJobPh2').text(data.ContactPhone2);
                    }
                    if (data.SiteContactPhone1 != null) {
                        $('#lblSitePh1').text(data.SiteContactPhone1); +',' + $('#lblSitePh2').text(data.SiteContactPhone2);
                    }
                    else {
                        $('#lblSitePh2').text(data.SiteContactPhone2);
                    }
                    $('#lblJobCountry').text(data.Country);
                    $('#lblSiteCountry').text(data.SiteCountry);
                    $('#lblJobZip').text(data.ZipCode);
                    $('#lblSiteZip').text(data.SiteZipCode);
                    //$('#lblJobPh1').text(data.ContactPhone1);
                    //$('#lblSitePh1').text(data.SiteContactPhone1);
                    //$('#lblJobPh2').text(data.ContactPhone2);
                    //$('#lblSitePh2').text(data.SiteContactPhone2);
                    //$('#lblJobState').text(data.State);
                    //$('#lblSiteState').text(data.SiteState);
                    $('#lblDate').text(data.StartDateString);
                    $('#lblCompletedDate').text(data.CompletedDateString);
                    $('.job-status').removeClass('is-active');
                    //Status For Job
                    //0 - Open
                    //1 - In Progress
                    //2 - Completed
                    //3 - Closed
                    switch (data.Status) {
                        case 0:
                            $('.job-created').addClass('is-active');
                            break;
                        case 1:
                            $('.job-inprogress').addClass('is-active');

                            break;
                        case 2:
                            $('.job-completed').addClass('is-active');
                            break;
                        case 3:
                            $('.job-closed').addClass('is-active');
                            break;
                    }
                }
            });

            //Get all tasks related to the current job
            $.ajax({
                url: $('#hdApiUrl').val() + 'api/Jobs/GetTasks?JobId=' + qsData.UID,
                method: 'POST',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    $(data).each(function () {
                        var taskId = this.TaskId;
                        var title = this.Title;
                        var desc = this.Description;
                        var userName = this.CreatedBy.FullName;
                        var userImage = this.CreatedBy.ProfileImagePath;
                        var timeLeft = this.TimeLeft;
                        var imageshtml = '';
                        if (this.ImagesPath.length > 0) {
                            imageshtml += '<div class="image-wrap">';
                            for (var i = 0; i < this.ImagesPath.length; i++) {
                                imageshtml += '<div class="image-holder"><img src="' + this.ImagesPath[i] + '" /><span class="delete-image">X</span></div>'
                            }
                            imageshtml += '</div>';
                        }
                        var Assigned = '';
                        if (this.EmployeeList.length > 0) {
                            Assigned += '<div class="assign-wrap"><ul class="list-unstyled">';
                            for (var i = 0; i < this.EmployeeList.length; i++) {
                                if (this.EmployeeList[i].PhotoPath != '' || this.EmployeeList[i].FirstName != '') {
                                    Assigned += '<li><img src="' + this.EmployeeList[i].PhotoPath + '" />' + this.EmployeeList[i].FirstName + '</li>'
                                }
                            }
                            Assigned += '</ul></div>'
                        }
                        var FromDate = this.StartDateString;
                        var EndDate = this.EndDateString;
                        if (FromDate != null && EndDate != null) {
                            var DateString = '<p class="date-range-text"><small>' + FromDate + ' to ' + EndDate + '</small></p>';
                        }
                        else if (FromDate == null && EndDate == null) {
                            var DateString = '';
                        }

                        //Status For Task
                        //0 - Open Task
                        //1 - In Progress
                        //2 - Completed
                        var status = 0;
                        var statusClass = '';
                        switch (this.Status) {
                            case 0:
                                status = 'Open Task';
                                statusClass = 'label-info';
                                break;
                            case 1:
                                status = 'In Progress';
                                statusClass = 'label-warning';
                                break;
                            case 2:
                                status = 'Completed';
                                statusClass = 'label-success';
                                break;
                            default:
                                break;
                        }
                        var html = '<li data-instance="' + taskId + '" class="timeline-item"><div class="panel panel-white"><div class="panel-body"><div class="timeline-item-header"> <img src="' + userImage + '" alt="" /><p class="timeline-post-info">' + title + '</p><a href="#" class="text-green m-l-5 m-r-5 edit-task"><i class="md md-create"></i></a><a href="#" class="text-danger m-r-5 delete-task"><i class="md md-delete"></i></a><div class="dropdown"> <a href="#" data-toggle="dropdown" role="button"><i class="md md-more-vert"></i></a><ul class="dropdown-menu"><li class="task-status-change" data-status="0"><a href="#">Open</a></li><li class="task-status-change" data-status="2"><a href="#">Completed</a></li><li class="task-status-change" data-status="1"><a href="#">InProgress</a></li><li class="divider"></li><li class="task-add-image"><a href="#">Add Image</a><input class="add-more-image" type="file" accept="image/*"></li><li class="task-add-employee"><a href="#">Add Employee</a></li></ul></div> <p><small>' + timeLeft + ' ago by <i>' + userName + '</i></small></p>' + DateString + '<p><span class="label custom ' + statusClass + ' lbl-status">' + status + '</span></p></div><div class="timeline-item-post"><p>' + desc + '</p>' + imageshtml + '</div>' + Assigned + '</div></div></li>';
                        $('#taskList').append(html);
                        html = '';
                    });
                }
            });

            //Save jobs
            $('#btnSaveJob').click(function () {
                var data = {};
                var CustomerId = $('#ddlCustomer').val();
                var jobName = $('#txtJob').val();
                var salutation = $('#ddlSalutation').val();
                var completedDate = $('#txtEndDate').val();
                var contPer = $('#txtContactPers').val();
                var siteSalut = $('#ddlSiteSalutation').val();
                var siteContPer = $('#txtSiteContPer').val();
                var addr1 = $('#txtContAddr1').val();
                var addr2 = $('#txtContAddr2').val();
                var siteAddr1 = $('#txtSiteAddr1').val();
                var siteAddr2 = $('#txtSiteAddr2').val();
                var city = $('#txtCity').val();
                var zip = $('#txtZip').val();
                var siteCity = $('#txtSiteCity').val();
                var siteZip = $('#txtSiteZip').val();
                var countryId = $('#ddlCountry').val();
                var stateId = $('#ddlState').val();
                var siteCountryId = $('#ddlSiteCountry').val();
                var siteStateId = $('#ddlSiteState').val();
                var ph1 = $('#txtPhone1').val();
                var ph2 = $('#txtPhone2').val();
                var Siteph1 = $('#txtSitePh1').val();
                var Siteph2 = $('#txtSitePh2').val();
                var email = $('#txtEmail').val();
                var siteEmail = $('#txtSiteEmail').val();
                var startDate = $('#txtStartDate').val();
                var EstimatedAmount = $('#txtEstAmt').val();
                var Id = $('#hdId').val();
                data.ID = Id;
                data.JobName = jobName;
                data.CustomerId = CustomerId;
                data.Status = 0;
                data.CompanyId = $.cookie("bsl_1");
                data.CreatedBy = $.cookie('bsl_3');
                data.ModifiedBy = $.cookie('bsl_3');
                data.StartDate = startDate;
                data.StartDateString = startDate;
                data.EstimatedAmount = EstimatedAmount;
                data.CompletedDate = completedDate;
                data.ContactName = contPer;
                data.SiteContactName = siteContPer;
                data.SiteContactPhone1 = Siteph1;
                data.ContactPhone1 = ph1;
                data.ContactPhone2 = ph2;
                data.SiteContactPhone2 = Siteph2;
                data.ContactAddress = addr1;
                data.ContactAddress2 = addr2;
                data.SiteAddress = siteAddr1;
                data.SiteContactAddress2 = siteAddr2;
                data.ContactCity = city;
                data.SiteContactCity = siteCity;
                data.StateId = stateId;
                data.SiteStateId = siteStateId;
                data.CountryId = countryId;
                data.SiteCountryId = siteCountryId;
                data.ZipCode = zip;
                data.SiteZipCode = siteZip;
                data.Email = email;
                data.SiteEmail = siteEmail;
                data.Salutation = salutation;
                data.SiteSalutation = siteSalut;
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/save',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(data),
                    success: function (response) {
                        console.log(response)
                        if (response.Success) {
                            successAlert(response.Message);
                            $('#txtJob').val('');
                            $('#txtEstAmt').val('');
                            $('#txtStartDate').val('');
                            $('#txtEndDate').val('');
                            $('#ddlSalutation').val('0');
                            $('#ddlSiteSalutation').val('0');
                            $('#txtContactPers').val('');
                            $('#txtSiteContPer').val('');
                            $('#txtContAddr1').val('');
                            $('#txtContAddr2').val('');
                            $('#txtSiteAddr1').val('');
                            $('#txtSiteAddr2').val('');
                            $('#txtCity').val('');
                            $('#txtZip').val('');
                            $('#txtSiteCity').val('');
                            $('#txtSiteZip').val('');
                            $('#ddlCountry').val('0');
                            $('#ddlState').val('0');
                            $('#ddlSiteCountry').val('0');
                            $('#ddlSiteState').val('0');
                            $('#txtPhone1').val('');
                            $('#txtPhone2').val('');
                            $('#txtSitePh1').val('');
                            $('#txtSitePh2').val('');
                            $('#txtEmail').val('');
                            $('#txtSiteEmail').val('');
                            $('#jobModal').modal('hide');
                            $('#btnSaveJob').html('<i class="fa fa-plus"></i>&nbsp;Save ');
                            if (response.Object != null && response.Object.Id != 0) {
                                window.open('../party/job?UID=' + response.Object.Id, '_self');
                            }
                            else {
                                window.open('../party/job?UID=' + qsData.UID, '_self');
                              }
                           // window.open('../party/job?UID=' + qsData.UID, '_self');
                            $('#hdId').val(0);
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });

            })

            //Delete job
            $('#btnDeleteJob').click(function () {
                var data = {};
                var jobId = qsData.UID;
                data.ID = jobId;
                data.ModifiedBy = $.cookie('bsl_3');
                swal({
                    title: "Delete?",
                    text: "Are you sure you want to delete?",
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Delete"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/jobs/Delete',
                            method: 'DELETE',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify(data),
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    window.open('../Masters/Inventories?MODE=NEW&SECTION=CUSTOMER');
                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (err) {
                                alert(err.responseText);
                                miniLoading('stop', null);
                            },
                            beforeSend: function () { miniLoading('start', null) },
                            complete: function () { miniLoading('stop', null); },
                        });
                    }
                });
            });

            //$('#taskList').on('click', '.edit-task', function () {
            //    var li = $(this).closest('li.timeline-item');
            //    var taskId = $(li).attr('data-instance');
            //    $.ajax({
            //        url: $('#hdApiUrl').val() + 'api/Jobs/GetTask?TaskId=' + taskId,
            //        method: 'POST',
            //        contentType: 'application/json;charset=utf-8',
            //        dataType: 'JSON',
            //        success: function (response) {
            //            //console.log(response);
            //            $('#txtTaskDesc').val(response.Description);
            //            $('#txtTaskTitle').val(response.Title);
            //            $('#txtTaskStartDate').val(response.StartDateString);
            //            $('#txtTaskEndDate').val(response.EndDateString);
            //            $('#hdTaskId').val(response.TaskId);
            //        },
            //        error: function (err) {
            //            alert(err.responseText);
            //            miniLoading('stop', null);
            //        },
            //        beforeSend: function () { miniLoading('start', null) },
            //        complete: function () { miniLoading('stop', null); },
            //    });
            //})

            //Edit Individual task functions
            $('#taskList').on('click', '.edit-task', function () {
                let li = $(this).closest('li.timeline-item');
                let taskId = $(li).attr('data-instance');
                let postTitle = $(li).find('.timeline-post-info');
                let postDesc = $(li).find('.timeline-item-post').children('p');
                let dropdownMenu = $(li).find('.dropdown-menu');
                let editTask = $(li).find('.edit-task');
                editTask.removeClass('edit-task').addClass('update-task');
                editTask.children('i').removeClass('md-create').addClass('md-save');
                //dropdownMenu.append('<li class="divider"></li><li class="task-add-image"><a href="#">Add Image</a><input class="add-more-image" type="file" accept="image/*"></li><li class="task-add-employee"><a href="#">Add Employee</a></li>')
                postTitle.attr('contenteditable', true);
                postDesc.attr('contenteditable', true);
            });
            //Add image in edit task
            $('#taskList').on('change', '.add-more-image', function (e) {
                var fileReader = new FileReader();
                let li = $(this).closest('li.timeline-item');
                    var image = li.find('.image-wrap');
                    fileReader.onload = function () {
                        var task = {};
                        var taskId = $(li).attr('data-instance');
                    $(image).append('<img src="' + fileReader.result + '" />');
                    task.ImagesAsB64 = [];
                    task.TaskId = taskId;
                    task.ImagesAsB64.push(fileReader.result.split(',')[1]);
                    
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/jobs/AddImageToTask',
                        method: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        dataType: 'JSON',
                        data: JSON.stringify(task),
                        success: function (response) {
                            console.log(response)
                            if (response.Success) {
                                //successAlert(response.Message);
                                $('.dropdown-menu').hide();
                                window.open('../party/job?UID=' + qsData.UID, '_self');
                            }
                            else {
                                errorAlert(response.Message);
                            }
                        },
                        error: function (err) {
                            alert(err.responseText);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }
                fileReader.readAsDataURL(e.target.files[0]);
            });

            //Add employee in edit task
            $('#taskList').on('click', '.task-add-employee', function () {
                console.log('add employee here');
            });

            //Update Funcionality Here
            $('#taskList').on('click', '.update-task', function () {
                let li = $(this).closest('li.timeline-item');
                alert(li.html());
                let postTitle = $(li).find('.timeline-post-info');
                let postDesc = $(li).find('.timeline-item-post').children('p');
                let dropdownMenu = $(li).find('.dropdown-menu');
                let updateTask = $(li).find('.update-task');
                updateTask.removeClass('update-task').addClass('edit-task');
                updateTask.children('i').removeClass('md-save').addClass('md-create');
                dropdownMenu.children('li').slice(-3).remove();
                postTitle.removeAttr('contenteditable');
                postDesc.removeAttr('contenteditable');

                var taskId = $(li).attr('data-instance');

                var title = $(li).find('.timeline-post-info').text()
                var desc = $(li).find('.timeline-item-post').children('p').text()
                var jobId = qsData.UID;
                //var Participants = $('#empSearch').val();
                //var FromDate = $('#txtTaskStartDate').val();
                //var ToDate = $('#txtTaskEndDate').val();
                var task = { Title: title, Description: desc ,TaskId:taskId};
                task.CreatedBy = { ID: $.cookie('bsl_3') };
                task.Job = { ID: jobId.replace(/#/g, '') };
                
                console.log(task)
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/UpdateTask',
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    data: JSON.stringify(task),
                    success: function (response) {
                        console.log(response)
                        if (response.Success) {
                          //  successAlert(response.Message);
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (err) {
                        alert(err.responseText);
                        miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            });

            //Load jobs into the modal
            $('#btnEditJob').click(function () {
                var jobId = qsData.UID;
                var cusId = $('#hdCustomerId').val();
                $('#hdId').val(jobId);
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/jobs/GetJob/?job_id=' + jobId,
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'JSON',
                    success: function (response) {
                        //console.log(response);
                        $('#jobModal').modal({ backdrop: 'static', keyboard: false, show: true });
                        $('#txtJob').val(response.JobName);
                        $('#txtStartDate').val(response.StartDateString);
                        $('#txtEndDate').val(response.CompletedDateString);
                        $('#ddlSalutation').val(response.Salutation);
                        $('#ddlSiteSalutation').val(response.SiteSalutation);
                        $('#txtContactPers').val(response.ContactName);
                        $('#txtSiteContPer').val(response.SiteContactName);
                        $('#txtContAddr1').val(response.ContactAddress);
                        $('#txtContAddr2').val(response.ContactAddress2);
                        $('#txtSiteAddr1').val(response.SiteAddress);
                        $('#txtSiteAddr2').val(response.SiteContactAddress2);
                        $('#txtCity').val(response.ContactCity);
                        $('#txtZip').val(response.ZipCode);
                        $('#txtSiteCity').val(response.SiteContactCity);
                        $('#txtSiteZip').val(response.SiteZipCode);
                        $('#ddlCountry').val(response.CountryId);
                        $('#ddlSiteCountry').val(response.SiteCountryId);
                        LoadStates(response.SiteStateId, $('#ddlSiteCountry'), $('#ddlSiteState'));
                        LoadStates(response.SiteStateId, $('#ddlSiteCountry'), $('#ddlSiteState'));
                        LoadStates(response.StateId, $('#ddlCountry'), $('#ddlState'));
                        $('#txtPhone1').val(response.ContactPhone1);
                        $('#txtPhone2').val(response.ContactPhone2);
                        $('#txtSitePh1').val(response.SiteContactPhone1);
                        $('#txtSitePh2').val(response.SiteContactPhone2);
                        $('#txtEmail').val(response.Email);
                        $('#txtSiteEmail').val(response.SiteEmail);
                        $('#txtEstAmt').val(response.EstimatedAmount);
                        $('#btnSaveJob').html('Update');
                    },
                    error: function (err) {
                        alert(err.responseText);
                        miniLoading('stop', null);
                    },
                    beforeSend: function () { miniLoading('start', null) },
                    complete: function () { miniLoading('stop', null); },
                });
            })

            //Change job status
            $('#jobstatus').on('click', '.job-status-change', function () {
                var jobId = qsData.UID.replace(/#/g, "");
                var status = $(this).closest('li').attr('data-status');
                var data = {};
                data.ID = jobId;
                data.Status = status;
                data.ModifiedBy = $.cookie('bsl_3');
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/Jobs/UpdateStatus',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        var response = data;
                        if (response.Success) {
                            $('.job-status').removeClass('is-active');
                            switch (Number(status)) {
                                case 0:
                                    $('.job-created').addClass('is-active');
                                    break;
                                case 1:
                                    $('.job-inprogress').addClass('is-active');

                                    break;
                                case 2:
                                    $('.job-completed').addClass('is-active');
                                    break;
                                case 3:
                                    $('.job-closed').addClass('is-active');
                                    break;
                            }
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                        complete: miniLoading('stop');
                    },
                    beforeSend: function () { miniLoading('start'); },
                    complete: function () { miniLoading('stop'); },
                });
            })

            //Delete task
            $('#taskList').on('click', '.delete-task', function () {
                var data = {};
                var li = $(this).closest('li.timeline-item');
                var taskId = $(li).attr('data-instance');
                data.TaskId = taskId;
                data.ModifiedBy = $.cookie('bsl_3');
                swal({
                    title: "Delete?",
                    text: "Are you sure you want to delete?",
                    showConfirmButton: true, closeOnConfirm: true,
                    showCancelButton: true,
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Delete"
                },
                function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/jobs/DeleteTask',
                            method: 'DELETE',
                            contentType: 'application/json;charset=utf-8',
                            dataType: 'JSON',
                            data: JSON.stringify(data),
                            success: function (response) {
                                if (response.Success) {
                                    successAlert(response.Message);
                                    $(li).remove();
                                }
                                else {
                                    errorAlert(response.Message);
                                }
                            },
                            error: function (err) {
                                alert(err.responseText);
                                miniLoading('stop', null);
                            },
                            beforeSend: function () { miniLoading('start', null) },
                            complete: function () { miniLoading('stop', null); },
                        });
                    }
                });
            })

            //Function for add task
            $('#btnAddTask').click(function (e) {
                var title = $('#txtTaskTitle').val();
                var desc = $('#txtTaskDesc').val();
                var jobId = qsData.UID;
                var Participants = $('#empSearch').val();
                var FromDate = $('#txtTaskStartDate').val();
                var ToDate = $('#txtTaskEndDate').val();
                var task = { Title: title, Description: desc };
                task.CreatedBy = { ID: $.cookie('bsl_3') };
                task.Job = { ID: jobId.replace(/#/g, '') };
                task.ImagesAsB64 = [];
                if (FromDate == '') {
                    task.StartDate = null;
                }
                else {
                    task.StartDate = FromDate;
                }
                if (ToDate == '') {
                    task.EndDate = null;
                }
                else {
                    task.EndDate = ToDate;
                }
                var images = $('.attachment-container').find('img');
                for (var i = 0; i < images.length; i++) {
                    task.ImagesAsB64.push($(images[i]).attr('src').split(',')[1]);
                }
                task.Participants = Participants;
                var loadingTimer;
                console.log(task);
                $.ajax({
                    url: $('#hdApiUrl').val() + '/api/Jobs/AddTask?companyId=' + $.cookie('bsl_1'),
                    method: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(task),
                    beforeSend: function () { loadingTimer = setTimeout(function () { $('#btnAddTask').html('Adding...'); }, 1000) },
                    complete: function () { clearTimeout(loadingTimer); $('#btnAddTask').html('Add Task') },
                    success: function (data) {
                        if (data.Success) {
                            console.log(data);
                            taskId = data.Object.TaskId;
                            //console.log($('.item').children('img').prop('src'));
                            $('.tag-employee-input-wrap').removeClass('in');
                            var userName = $('#txtUserNameMst').text();
                            var userImage = $('#UserProfileImageMSt').prop('src');
                            var imagesHtml = '';
                            var images = $('.attachment-container').find('img');
                                imagesHtml += '<div class="image-wrap">';
                                for (var j = 0; j < images.length; j++) {
                                    imagesHtml += '<img src="' + $(images[j]).attr('src') + '" />';
                                }
                                imagesHtml += '</div>';
                            
                            var Assign = $('.selectize-input').find('img');
                            var AssignList = '';
                            if (Assign.length > 0) {
                                AssignList += '<div class="assign-wrap"><ul class="list-unstyled">';
                                for (var k = 0; k < Assign.length; k++) {
                                    AssignList += '<li><img src="' + $(Assign[k]).attr('src') + '" /><span>' + $(Assign[k]).next("span").html() + '</span></li>';
                                }
                                AssignList += '</ul></div>';
                            }
                            var FromDate = $('#txtTaskStartDate').val();
                            var EndDate = $('#txtTaskEndDate').val();
                            var DateString = '';
                            if (FromDate!=''&&EndDate!='') {
                                DateString = '<p class="date-range-text"><small>' + FromDate + ' to ' + EndDate + '</small></p>';
                            }
                            else {
                                DateString = '';
                            }
                            var html = '<li data-instance="' + taskId + '" style="display:none" class="timeline-item" ><div class="panel panel-white"><div class="panel-body"><div class="timeline-item-header"> <img src="' + userImage + '" alt="" /><p class="timeline-post-info">' + title + '</p><a href="#" class="text-green m-l-5 m-r-5 edit-task"><i class="md md-create"></i></a><a href="#" class="text-danger m-r-5 delete-task"><i class="md md-delete"></i></a><div class="dropdown"> <a href="#" data-toggle="dropdown" role="button"><i class="md md-more-vert"></i></a><ul class="dropdown-menu"><li class="task-status-change" data-status="0"><a href="#">Open</a></li><li class="task-status-change" data-status="2"><a href="#">Completed</a></li><li class="task-status-change" data-status="1"><a href="#">InProgress</a></li><li class="divider"></li><li class="task-add-image"><a href="#">Add Image</a><input class="add-more-image" type="file" accept="image/*"></li><li class="task-add-employee"><a href="#">Add Employee</a></li></ul></div> <p><small>just now by <i>' + userName + '</i></small></p>' + DateString + '<p><span class="label custom label-info lbl-status">Open Task</span></p></div><div class="timeline-item-post"><p>' + desc + '</p>' + imagesHtml + '</div>' + AssignList + '</div></div></li>';
                            $('#taskList').prepend(html);
                            var empImg = $()
                            $('#taskList').children('li').eq(0).slideDown('slow');
                            html = '';
                            $('#txtTaskTitle').val('');
                            $('#txtTaskDesc').val('');
                            $('#txtTaskStartDate').val('');
                            $('#txtTaskEndDate').val('');
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (err) { console.log(err); }
                });
            });

            //Image upload with previewing
            $('#imageUploader').change(function (e) {
                var fileReader = new FileReader();

                fileReader.onload = function () {
                    $('.attachment-container').append('<div class="attachment-holder"><span class="remove-attachment">x</span><img src="' + fileReader.result + '" /></div>');
                }
                fileReader.readAsDataURL(e.target.files[0]);

            });

            //Remove selected image
            $('body').on('click', 'span.remove-attachment', function () {
                $(this).closest('.attachment-holder').remove();
            });


            $('body').on('click', 'span.delete-image', function () {
               var img= $(this).closest('.image-holder').children('img').attr('src');
               let li = $(this).closest('li.timeline-item');
               var taskId = $(li).attr('data-instance');
               var data = {};
               data.TaskId = taskId;
               data.ImagePath = img;
               data.ModifiedBy = $.cookie('bsl_3');
               $.ajax({
                   url: $('#hdApiUrl').val() + 'api/jobs/DeleteImage',
                   method: 'DELETE',
                   contentType: 'application/json;charset=utf-8',
                   dataType: 'JSON',
                   data: JSON.stringify(data),
                   success: function (response) {
                       if (response.Success) {
                           //successAlert(response.Message);
                       }
                       else {
                           errorAlert(response.Message);
                       }
                   },
                   error: function (err) {
                       alert(err.responseText);
                       miniLoading('stop', null);
                   },
                   beforeSend: function () { miniLoading('start', null) },
                   complete: function () { miniLoading('stop', null); },
               });
                $(this).closest('.image-holder').remove();
            })

        });
        //Document ready ends here
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
