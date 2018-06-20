<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Build.aspx.cs" Inherits="BisellsERP.Settings.Reporting.Build" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Custom Reports</title>
    <style>

        #wrapper, body {
            overflow: hidden;
        }
        .table-scroll tbody {
            height: 53vh;
        }

        .settings-wrap > div {
            height: calc(100vh - 83px);
        }

            .settings-wrap > div.left .nav {
                height: calc(100vh - 135px);
                background-color: #fff;
                box-shadow: 0 2px 1px 0 #ccc;
            }

        .settings-wrap .left .nav > li {
            border-bottom: 1px solid #f3f3f3;
            border-left: 3px solid transparent;
        }

            .settings-wrap .left .nav > li > a {
                color: #777 !important;
            }

                .settings-wrap .left .nav > li > a:hover {
                    color: #757575 !important;
                }

            .settings-wrap .left .nav > li.active {
                border-left: 3px solid #3cba9f;
            }

            .settings-wrap .left .nav > li > a:focus, .settings-wrap .left .nav > li > a:hover {
                background-color: transparent;
            }

            .settings-wrap .left .nav > li > a:focus, .settings-wrap .left .nav > li:hover {
                border-left: 3px solid #3cba9f;
            }

            .settings-wrap .left .nav > li > a {
                line-height: 30px;
                padding: 10px;
            }
            .settings-wrap .left .nav > li > a i {
                font-size: 12px;
            }
            .settings-wrap .left .nav > li > a button {
                padding: 3px 9px;
                margin-top: 5px;
                opacity: 0;
                transition: opacity .2s ease;
            }
            .settings-wrap .left .nav > li:hover > a button {
                opacity: 1;
            }

        .tab-content > .tab-pane > .panel {
            height: calc(100vh - 135px);
            box-shadow: 0 2px 1px 0 #ccc;
        }

        .sett-title {
            border-bottom: 1px dashed #ececec;
            padding-top: 20px;
            padding-bottom: 5px;
            margin-top: 0;
            margin-bottom: 15px;
            color: #3cba9f;
            margin-left: -5px;
        }

        .settings-wrap label {
            /*color: #78909c;*/
            font-weight: 100;
            text-align: left !important;
            font-size: 12px;
        }

            .settings-wrap label > i {
                margin-left: 2px;
                color: #B0BEC5;
            }

        .settings-wrap .form-group {
            margin-bottom: 5px;
        }

        li.list-break > p {
            margin-bottom: 0;
            background-color: #FAFAFA;
            padding: 8px;
        }
        .right .nav.nav-tabs > li > a, .right .nav.tabs-vertical > li > a {
            line-height: 35px;
        }
        .right .nav.nav-tabs + .tab-content, .right .tabs-vertical-env .tab-content {
            height: calc(100vh - 170px);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

            <%-- ---- Page Title ---- --%>
        <div class="row p-b-5">
            <div class="col-sm-2">
                <h3 class="page-title m-t-0">Custom Reports</h3>
            </div>
            <div class="col-sm-10">
                <div class="btn-toolbar pull-right" role="group">
                    <button type="button"  data-toggle="tooltip" data-placement="bottom" title="Add new report" class="btn btn-default AddReport waves-effect waves-light m-r-10"><i class="ion-ios7-plus-outline"></i>&nbsp;New Custom Report</button>
                    <button type="button" accesskey="s" id="btnSave" data-toggle="tooltip" data-placement="bottom" title="Save the current Report" class="btn btn-default waves-effect waves-light "><i class="ion-ios7-checkmark-outline"></i>&nbsp;Save</button>
                    <button type="button" id="btnDelete" data-toggle="tooltip" data-placement="bottom" title="Delete" class="btn btn-default waves-effect waves-light text-danger"><i class="ion ion-trash-b"></i></button>
                </div>
            </div>
        </div>


    <div class="settings-wrap">
        <div class="col-sm-4 p-l-0 left">
            <ul class="nav" id="menu">
               <%-- <li id="123" class="edit-entry">
                    <a href="#" data-toggle="tab" aria-expanded="true">
                        <p class="m-b-0">
                            Name : Purchase Details
                            <label class="label label-success m-t-5 pull-right">Active</label>
                        </p>
                        <p class="m-b-0">
                            <small>Type : <b>NESTABLE</b></small>
                            <button class="pull-right btn btn-default btn-sm">Run Report&nbsp;<i class="ion-arrow-right-c"></i></button>
                        </p>
                    </a>
                </li>
                <li id="1232" class="edit-entry">
                    <a href="#" data-toggle="tab" aria-expanded="true">
                        <p class="m-b-0">
                            Name : Purchase Summary
                            <label class="label label-danger m-t-5 pull-right">Inactive</label>
                        </p>
                        <p class="m-b-0">
                            <small>Type : <b>SUMMARISED</b></small>
                            <button class="pull-right btn btn-default btn-sm">Run Report&nbsp;<i class="ion-arrow-right-c"></i></button>
                        </p>
                    </a>
                </li>--%>
            </ul>
        </div>
        <div class="col-sm-8 right p-0">

            <ul class="nav nav-inner nav-tabs navtab-bg">
                <li class="active">
                    <a href="#generalSettings" data-toggle="tab" aria-expanded="true">
                        <span>General</span>
                    </a>
                </li>
                <li class="">
                    <a href="#reportColumns" data-toggle="tab" aria-expanded="true">
                        <span>Columns</span>
                    </a>
                </li>
                <li class="">
                    <a href="#reportFilter" data-toggle="tab" aria-expanded="true">
                        <span>Filters</span>
                    </a>
                </li>
            </ul>

            <div class="tab-content m-b-0">

                <div class="tab-pane active" id="generalSettings">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal m-t-10">
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Report Name</label>
                                    <div class="col-sm-7">
                                        <input type="text" id="txtReportName" name="example-input1-group1" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Description</label>
                                    <div class="col-sm-7">
                                        <textarea rows="2" id="txtDescription" class="form-control input-sm"></textarea>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Report View</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlViews" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Key Column</label>
                                    <div class="col-sm-7">
                                        <select id="ddlKeyColumn" class="form-control"></select>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Default Rows</label>
                                    <div class="col-sm-7">
                                        <input type="number" id="txtDefaultRows" name="example-input1-group1" class="form-control" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-5">Type</label>
                                    <div class="col-sm-7">
                                        <select id="ddlReportType" class="form-control">
                                            <option value="0">Summary</option>
                                            <option value="1">Nestable</option>
                                            <option value="2">Statistical</option>
                                        </select>
                                    </div>
                                </div>
                                <div id="divDetailsView" class="form-group">
                                    <label class="control-label col-sm-5">Detail Report View</label>
                                    <div class="col-sm-7">
                                        <asp:DropDownList ClientIDMode="Static" ID="ddlDetailViews" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div id="divDetailKeyView" class="form-group">
                                    <label class="control-label col-sm-5">Detail Key Column</label>
                                    <div class="col-sm-7">
                                        <select id="ddlDetailKey" class="form-control"></select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane " id="reportColumns">
                    <h5 class="sett-title p-t-0"><small>Drag & Drop one over another for reordering</small>
                        <a id="btnAddColumn" data-toggle="modal" data-target="#columnModal" href="#" class="pull-right" style="color: #55879e;"><i class="ion-ios7-plus-outline"></i>&nbsp;New Column</a>
                    </h5>
                    <div class="row">
                        <div class="col-sm-12">
                            <table class="table table-hover table-scroll" id="tblColumns">
                                <thead>
                                    <tr>
                                        <th>Field</th>
                                        <th>Display Name</th>
                                        <th>Is Summable</th>
                                        <th>#</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>


                <div class="tab-pane " id="reportFilter">
                    <h5 class="sett-title p-t-0"><small>&nbsp;</small>
                        <a id="btnAddFilter" data-toggle="modal" data-target="#filterModal" href="#" class="pull-right" style="color: #55879e;"><i class="ion-ios7-plus-outline"></i>&nbsp;New Filter</a>
                    </h5>

                    <div class="row">
                        <div class="col-sm-12">
                            <table class="table table-hover table-scroll" id="tblFilters">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Label</th>
                                        <th>Applicable To</th>
                                        <th>Data Text Field</th>
                                        <th>Data Value Field</th>
                                        <th>Type</th>
                                        <th>Render As</th>
                                        <th style="width: 40px;">#</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </div>


        </div>
    </div>


        <div id="filterModal" class="modal animated fadeIn" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content ">
                    <div class="modal-header">
                        <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                        <h4 class="modal-title">Report Filter</h4>
                    </div>
                    <div class="modal-body p-b-0">
                        <div class="row" style="padding-top: 10px">
                            <div class="col-sm-5">
                                <label class="control-label">Filter Name</label>
                                <input id="txtFilterName" type="text" class="form-control" />
                            </div>
                            <div class="col-sm-7">
                                <label class="control-label">Label</label>
                                <input id="txtFilterDisplayName" type="text" class="form-control" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px">
                            <div class="col-sm-12">
                                <label class="control-label">Applicable To</label>
                                <select id="ddlApplicableTo" class="form-control">
                                </select>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px">
                            <div class="col-sm-6">
                                <label class="control-label">Type</label>
                                <select id="ddlFilterType" class="form-control">
                                    <option value="0">Range</option>
                                    <option value="1">Exact Match</option>
                                    <option value="2">Contains Search</option>
                                </select>
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label">Rendering Control</label>
                                <select id="ddlControlType" class="form-control">
                                    <option value="0">List</option>
                                    <option value="1">Typable</option>
                                    <option value="2">Date</option>
                                </select>
                            </div>
                        </div>
                        <div class="row" id="divDataText" style="padding-top: 10px">
                            <div class="col-sm-6">
                                <label class="control-label">Data Text Column</label>
                                <select id="ddlDataTextColumn" class="form-control">
                                </select>
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label">Data Value Column</label>
                                <select id="ddlDataValueColumn" class="form-control">
                                </select>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 20px">
                            <div class="col-sm-12">
                                <button type="button" id="btnSaveFilter" class="btn btn-danger pull-right"><i class="md md-add-box"></i>&nbsp;Add Filter</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="columnModal" class="modal animated fadeIn" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content ">
                    <div class="modal-header">
                        <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                        <h4 class="modal-title">Report Fields</h4>
                    </div>
                    <div class="modal-body p-b-0">
                        <div class="row" style="padding-top: 10px">
                            <div class="col-sm-6">
                                <label class="control-label">Select Field</label>
                                <select id="ddlField" class="form-control">
                                </select>
                            </div>
                            <div class="col-sm-6">
                                <label class="control-label">Display Name</label>
                                <input id="columnDisplayName" type="text" class="form-control" />
                            </div>
                            <div class="col-sm-6">
                                <div class="checkbox checkbox-inline checkbox-primary m-t-15">
                                    <asp:CheckBox ID="chkSummable" ClientIDMode="Static" runat="server" Text="Is Summable" />
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px">
                            <div class="col-sm-12 text-right">
                                <button type="button" id="btnSaveColumn" class="btn btn-danger pull-right"><i class="md md-add-box"></i>&nbsp;Add Field</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ClientIDMode="Static" Value="0" ID="hdReport" runat="server" />
        <script>
            $(document).ready(function () {
                $(".tab-content > .tab-pane > .panel").niceScroll({
                    cursorcolor: "#90A4AE",
                    cursorwidth: "8px",
                    horizrailenabled: false
                });

                $("#menu").niceScroll({
                    cursorcolor: "#90A4AE",
                    cursorwidth: "8px",
                    horizrailenabled: false
                });

                listItems();
                //To load all the li.
                function listItems() {
                    var html = '';
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/adhoc/GetDetails/',
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        success: function (data) {
                            var response = data;
                            console.log(response);
                            var html = '';
                            for (var i = 0; i < response.length; i++) {
                                html +='<li id="' + response[i].ReportId + '" class="edit-entry">';
                                html +='<a href="#v-5" data-toggle="tab" aria-expanded="true">';
                                html+='<p class="m-b-0">';
                                html+='Name : ' + response[i].ReportName;
                                html+=response[i].Status == 1 ? '<label class="label label-success m-t-5 pull-right">Active</label>':'<label class="label label-danger m-t-5 pull-right">Inactive</label>';
                                html+='</p><p class="m-b-0"><small>Type :<b>' + response[i].TypeString + '</b></small>';
                                html += '<button class="ViewReport pull-right btn btn-default btn-sm">Run Report&nbsp;<i class="ion-arrow-right-c"></i></button></p></a></li>';
                            }
                            $('#menu').children().remove();
                            $('#menu').append(html);
                        },
                        error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                    });
                }


                //li click funtionality
                $(document).on('click', '.edit-entry', function () {
                    resetAll();                
                    var id = $(this).attr('id');
                    if (id != '0') {
                        $('#hdReport').val(id);

                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/adhoc/GetDetails/' + id,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (response) {
                                var itemEdit = response;
                                console.log(itemEdit);
                                $.ajax({
                                    url: $('#hdApiUrl').val() + 'api/adhoc/GetFields',
                                    method: 'POST',
                                    dataType: 'JSON',
                                    contentType: 'application/json;charset=utf-8',
                                    data: JSON.stringify(itemEdit.ReportViewObjectId),
                                    success: function (response) {
                                        console.log(response);
                                        var data = response;
                                        LoadDropDownChanged(data, 'ddlKeyColumn');
                                        LoadDropDownChanged(data, 'ddlDetailKey');
                                        LoadDropDownChanged(data, 'ddlField');
                                        LoadDropDownChanged(data, 'ddlApplicableTo');
                                        LoadDropDownChanged(data, 'ddlDataValueColumn');
                                        LoadDropDownChanged(data, 'ddlDataTextColumn');
                                        $('#txtReportName').val(itemEdit.ReportName);
                                        $('#txtDescription').val(itemEdit.Description);
                                        $('#ddlViews').val(itemEdit.ReportViewObjectId);
                                        $('#txtDefaultRows').val(itemEdit.DefaultRows);
                                        $('#ddlKeyColumn').val(itemEdit.PrimaryKeyColumn);

                                        $("#ddlReportType").val(itemEdit.Type);

                                        $('#ddlDetailViews').val(itemEdit.DetailReportId);

                                        $('#ddlDetailKey').val(itemEdit.DetailKeyColumn);
                                        $('#add-item-portlet').addClass('in');
                                        var html = '';
                                        $(itemEdit.Columns).each(function () {

                                            html += '<tr draggable="true" style="cursor: move;">';
                                            html += '<td>' + this.ColumnName + '</td>';
                                            html += '<td>' + this.DisplayName + '</td>';
                                            html += '<td>' + this.IsSummable + '</td>';
                                            html += '<td><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-item"><i class="fa fa-times" style="color:red"></i></a></td>';
                                            html += '</tr>';
                                        });



                                        $('#tblColumns tbody').append(html);

                                        html = '';
                                        $(itemEdit.Filters).each(function () {

                                            html += '<tr>';
                                            html += '<td>' + this.FilterName + '</td>';
                                            html += '<td>' + this.Label + '</td>';
                                            html += '<td>' + this.ColumnName + '</td>';
                                            html += '<td>' + this.DataTextField + '</td>';
                                            html += '<td>' + this.DataValueField + '</td>';
                                            html += '<td>' + this.TypeAsName + '</td>';
                                            html += '<td>' + this.RenderAs + '</td>';
                                            html += '<td class="hidden">' + this.Type + '</td>';
                                            html += '<td class="hidden">' + this.ControlType + '</td>';
                                            html += '<td><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-item"><i class="fa fa-times" style="color:red"></i></a></td>';
                                            html += '</tr>';
                                        });
                                        $('#tblFilters tbody').append(html);

                                        if (itemEdit.Type == '1') {
                                            $('#divDetailsView').slideDown('slow');
                                            $('#divDetailKeyView').slideDown('slow');
                                        }
                                        else {
                                            $('#divDetailsView').slideUp('slow');
                                            $('#divDetailKeyView').slideUp('slow')
                                        }
                                    },
                                    error: function (xhr) {
                                        alert(xhr.responseText);
                                        console.log(xhr);
                                    }
                                });
                                //

                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                            }
                        });
                    }
                    else {
                        resetAll();
                    }
                });

                function LoadDropDown(data, dropDownId) {
                    $('#' + dropDownId).children('option').remove();
                    $('#' + dropDownId).append('<option value="0">-select-</option>');
                    $(data).each(function () {
                        $('#' + dropDownId).append('<option value="' + this.ColumnId + '">' + this.Field + '</option>');
                    });
                }
                function LoadDropDownChanged(data, dropDownId) {
                    $('#' + dropDownId).children('option').remove();
                    $('#' + dropDownId).append('<option value="0">-select-</option>');
                    $(data).each(function () {
                        $('#' + dropDownId).append('<option value="' + this.Field + '">' + this.Field + '</option>');
                    });
                }

                function resetAll() {
                    reset();
                    $('#chkSummable').prop('checked', false);
                    $('#tblColumns tbody').children().remove();
                    $('#tblFilters tbody').children().remove();
                    $('#hdReport').val('0');
                }
                var DragObj;
                $('#tblColumns').on('dragstart', 'tr', function (evt) {

                    //evt.originalEvent.dataTransfer.setData("text", JSON.stringify(evt.target));
                    DragObj = evt.target;
                   // console.log(DragObj);
                });
                $('#tblColumns').on('dragover', 'tr', function (evt) {

                    evt.preventDefault();
                });
                $('#tblColumns').on('drop', 'tr', function (evt) {

                    evt.preventDefault();
                    var DropObj = evt.target;
                    $(DragObj).insertAfter(DropObj.closest('tr'));

                });
              

                $('#ddlViews').change(function () {
                    if ($(this).val() !== '0') {

                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/adhoc/GetFields',
                            method: 'POST',
                            dataType: 'JSON',
                            contentType: 'application/json;charset=utf-8',
                            data: JSON.stringify($(this).val()),
                            success: function (response) {
                                var data = response;

                                LoadDropDown(data, 'ddlKeyColumn');
                                LoadDropDown(data, 'ddlDetailKey');
                                LoadDropDown(data, 'ddlField');
                                LoadDropDown(data, 'ddlApplicableTo');
                                LoadDropDown(data, 'ddlDataValueColumn');
                                LoadDropDown(data, 'ddlDataTextColumn');
                            },
                            error: function (xhr) {
                                alert(xhr.responseText);
                                console.log(xhr);
                            }
                        });
                    }
                    else {
                        $('#ddlKeyColumn,#ddlDetailKey,#ddlField,#ddlApplicableTo').children('option').remove();
                    }
                });

                $('#ddlReportType').change(function () {

                    if ($(this).val() === '1') {
                        $('#divDetailsView').slideDown('slow');
                        $('#divDetailKeyView').slideDown('slow');

                    }
                    else {
                        $('#divDetailsView').slideUp('slow');
                        $('#divDetailKeyView').slideUp('slow');
                    }
                });
                $('#ddlControlType').change(function () {
                    if ($(this).val() === '0') {
                        $('#divDataText').slideDown('slow');
                    }
                    else {
                        $('#divDataText').slideUp('slow');
                    }
                });

                //Save Function for Columns of Report
                $('#btnSaveColumn').click(function () {
                    if ($('#ddlField').val() != null && $('#ddlField').val() !== '0' && $('#columnDisplayName').val() !== '') {
                        var columnName = $('#ddlField').children('option:selected').text();
                        var columnDispName = $('#columnDisplayName').val();
                        var summable = $('#chkSummable').is(':checked')

                        var html = '';
                        html += '<tr>';
                        html += '<td>' + columnName + '</td>';
                        html += '<td>' + columnDispName + '</td>';
                        html += '<td>' + summable + '</td>';
                        html += '<td><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-item"><i class="fa fa-times" style="color:red"></i></a></td>';
                        html += '</tr>';
                        $('#tblColumns tbody').append(html);
                        $('#columnDisplayName').val('');
                        $('#ddlField').val('0');
                    }
                    else {
                        errorField('#ddlField');
                        errorField('#columnDisplayName');
                    }
                });
                $('body').on('click', '.delete-item', function () {
                    $(this).closest('tr').fadeOut('slow', function () { $(this).closest('tr').remove(); })
                });//Removes The Row


                //Add Filter to the list
                $('#btnSaveFilter').click(function () {
                    var name = $('#txtFilterName').val();
                    var displayName = $('#txtFilterDisplayName').val();
                    var applicableTo = $('#ddlApplicableTo').children('option:selected').text();
                    var filterType = $('#ddlFilterType').children('option:selected').text();
                    var controlType = $('#ddlControlType').children('option:selected').text();
                    var filterTypeId = $('#ddlFilterType').val();
                    var controlTypeUId = $('#ddlControlType').val();
                    var dataTextColumn = $('#ddlDataTextColumn').children('option:selected').text();
                    var dataValueColumn = $('#ddlDataValueColumn').children('option:selected').text();
                    if ($('#ddlApplicableTo').val() !== '0' && $('#ddlApplicableTo').val() !== null) {
                        var html = '';
                        html += '<tr>';
                        html += '<td>' + name + '</td>';
                        html += '<td>' + displayName + '</td>';
                        html += '<td>' + applicableTo + '</td>';
                        html += '<td>' + dataTextColumn + '</td>';
                        html += '<td>' + dataValueColumn + '</td>';
                        html += '<td>' + filterType + '</td>';
                        html += '<td>' + controlType + '</td>';
                        html += '<td style="display:none">' + filterTypeId + '</td>';
                        html += '<td style="display:none">' + controlTypeUId + '</td>';
                        html += '<td><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-item"><i class="fa fa-times" style="color:red"></i></a></td>';
                        html += '</tr>';
                        $('#tblFilters tbody').append(html);
                        $('#txtFilterName').val('');
                        $('#txtFilterDisplayName').val('');
                        $('#ddlApplicableTo').val('');
                        $('#ddlFilterType').val('0');
                        $('#ddlControlType').val('0');
                        $('#ddlDataTextColumn').val('0');
                        $('#ddlDataValueColumn').val('0');
                    }
                    else {
                        errorField('#ddlApplicableTo');
                    }

                });


                //Save Report
                $('#btnSave').click(function () {
                    var Report = {};
                    Report.ReportId = $('#hdReport').val();
                    Report.ReportName = $('#txtReportName').val();
                    Report.Description = $('#txtDescription').val();
                    Report.ReportView = $('#ddlViews').children('option:selected').text();
                    Report.DefaultRows = $('#txtDefaultRows').val();
                    Report.FilterAPI = $('#hdApiUrl').val() + 'api/AdHoc/FilterReport/';
                    Report.DetailKeyColumn = $('#ddlDetailKey').children('option:selected').text();
                    Report.DetailReportId = $('#ddlDetailViews').val();
                    Report.PrimaryKeyColumn = $('#ddlKeyColumn').children('option:selected').text();
                    Report.Type = $('#ddlReportType').val();
                    Report.CreatedBy=Report.ModifiedBy = $.cookie('bsl_3');

                    Report.Columns = [];
                    Report.Filters = [];
                    var columnRows = $('#tblColumns tbody').children('tr');
                    $(columnRows).each(function () {
                        var column = {};
                        column.ColumnName = $(this).children('td').eq(0).text();
                        column.DisplayName = $(this).children('td').eq(1).text();
                        column.IsSummable = $(this).children('td').eq(2).text();
                        Report.Columns.push(column);
                    });

                    var filterRows = $('#tblFilters tbody').children('tr');
                    $(filterRows).each(function () {
                        var filter = {};
                        filter.FilterName = $(this).children('td').eq(0).text();
                        filter.Label = $(this).children('td').eq(1).text();
                        filter.ColumnName = $(this).children('td').eq(2).text();
                        filter.DataTextField = $(this).children('td').eq(3).text();
                        filter.DataValueField = $(this).children('td').eq(4).text();
                        filter.Type = $(this).children('td').eq(7).text();
                        filter.ControlType = $(this).children('td').eq(8).text();
                        Report.Filters.push(filter);
                    });

                    var id = $('#hdReport').val();

                    //if (Report.Type == '1' && Report.DetailReportId != '0' && (Report.DetailKeyColumn !== '-select-' && Report.DetailKeyColumn!=undefined))
                    //{

                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/adhoc/save/' + id,
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify(Report),
                        success: function (response) {

                            var data = response;
                            if (data.Success) {
                                resetAll();
                                $('#hdReport').val('0');
                                successAlert(data.Message);
                                $('#add-item-portlet').removeClass('in');
                                listItems();
                            }
                            else {
                                errorAlert(data.Message);
                            }


                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                        }
                    });
                    //}
                    //else {
                    //    errorField('#ddlDetailViews');
                    //    errorField('#ddlDetailKey');
                    //}
                });



                $(document).on('click', '.ViewReport', function () {
                    var id = $(this).closest('li').attr('id');
                    //console.log($(this).closest('li').attr('id'));
                    window.location='/reports/adhoc?reportuid=' + id;
                });

                $(document).on('click', '.AddReport', function () {
                    resetAll();
                    reset();
                    $('#txtReportName').focus();
                });
                //Delete functionality
                $('#btnDelete').click(function () {
                    var id = $('#hdReport').val();
                    if (id!='0') {
                    swal({
                        title: "Delete?",
                        text: "Are you sure you want to delete?",

                        showConfirmButton: true, closeOnConfirm: true,
                        showCancelButton: true,

                        cancelButtonText: "Back to Entry",
                        confirmButtonClass: "btn-danger",
                        confirmButtonText: "Delete"
                    }, function (isConfirm) {
                        if (isConfirm) {
                            
                                $.ajax({
                                    url: $('#hdApiUrl').val() + '/api/adhoc/Delete/' + id,
                                    method: 'DELETE',
                                    datatype: 'JSON',
                                    contentType: 'application/json;charset=utf-8',

                                    success: function (data) {
                                        var response = data;
                                        if (response.Success) {
                                            successAlert(response.Message);
                                            resetAll();
                                            $('#hdReport').val(0);
                                            listItems();
                                        }
                                        else {
                                            errorAlert(response.Message);
                                            console.log(response);
                                        }
                                    },
                                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                                });
                            
                        }

                    });

                    }
                    else {
                        errorAlert('Please select a report');
                    }
                });
            });
        </script>
        <script src="/Theme/assets/jquery-datatables-editable/jquery.dataTables.js"></script>
        <script src="/Theme/assets/jquery-datatables-editable/dataTables.bootstrap.js"></script>
        <link href="/Theme/assets/jquery-datatables-editable/datatables.css" rel="stylesheet" />
        <link href="/Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
        <script src="/Theme/assets/sweet-alert/sweet-alert.min.js"></script>
        <script src="/Theme/assets/sweet-alert/sweet-alert.init.js"></script>
        <script src="/Theme/Custom/Commons.js"></script>
</asp:Content>
