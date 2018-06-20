   <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Adhoc.aspx.cs" Inherits="BisellsERP.Settings.Reporting.Adhoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .portlet .portlet-body {
            padding: 20px;
        }

        .form-control {
            background-color: #fafafa;
        }
        .form-horizontal .control-label {
            color: #55879e;
            text-align: left;
        }
        .portlet-transparent {
            background-color: transparent;
            box-shadow: none;
            border: 0px solid #ececec;
             margin-bottom: 0;
        }
        .right-side .portlet-heading, .right-side .portlet-body {
            padding: 0 20px;
        }
        .right-side .table-scroll tbody {
            height: 125px;
        }
        .right-side table {
           border: 1px solid #f1f1f1;
        }
            .right-side table thead th {
                color: #717171;
                font-size: 12px;
                background-color: #F5F5F5;
                border-bottom: 0;
            }
        .table-scroll tbody {
            height: 60vh;
        }
            
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="">
        <%--Page Title and Breadcrumb--%>
        <div class="row">
            <div class="col-sm-12">
                <h3 class="pull-left page-title">Create Report</h3>
                <ol class="breadcrumb pull-right">
                    <li><a href="#">Bisells</a></li>
                    <li><a href="#">Settings</a></li>
                    <li><a href="#">Reporting</a></li>
                    <li class="active">Adhoc</li>
                </ol>
            </div>
        </div>
        <input type="hidden" value="0" id="hdReport" />
        <div class="row">
            <div class="col-lg-12">
                <div class="portlet b-r-8">
                    <div class="portlet-heading portlet-default">

                        <h3 class="portlet-title">
                            <a id="btnAdd" data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary"><i class="ion-ios7-plus-outline"></i>&nbsp;Add Report Settings</a>
                        </h3>
                        <div class="clearfix"></div>

                        <div id="add-item-portlet" class="panel-collapse collapse">
                            <div class="container p-r-0">
                                <div class="col-sm-4">
                                        <div class="form-horizontal m-t-10">
                                            <div class="form-group">
                                                <label class="control-label col-sm-5">Report Name</label>
                                                <div class="col-sm-7">
                                                    <input type="text" id="txtReportName" name="example-input1-group1" class="form-control" />
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
                                             
                                            <div class="btn-toolbar pull-right ">
                                                <button type="button" id="btnSave" class="btn btn-success">Save</button>
                                                <button type="button" id="btnDelete" class="btn btn-danger">Delete</button>
                                            </div>
                                        </div>
                                </div>

                                <div class="col-sm-8 right-side">
                                    <div class="portlet portlet-transparent">
                                        <div class="portlet-heading portlet-default">
                                            <h3 class="portlet-title w-100">
                                                <a id="btnAddColumn" data-toggle="modal" data-target="#columnModal" href="#add-item-portlet" class="pull-right" style="color: #55879e;">Add&nbsp;<i class="ion-ios7-plus-outline"></i></a>
                                                <a data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary">Columns<small>&nbsp;Drag & Drop one over another for reordering</small></a>
                                            </h3>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="portlet-body">
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
                                    <div class="portlet portlet-transparent">
                                        <div class="portlet-heading portlet-default">
                                            <h3 class="portlet-title w-100">
                                                <a id="btnAddFilter" data-toggle="modal" data-target="#filterModal" href="#add-item-portlet" class="pull-right" style="color: #55879e;">Add&nbsp;<i class="ion-ios7-plus-outline"></i></a>
                                                <a data-toggle="collapse" data-parent="#accordion1" href="#add-item-portlet" class="text-primary">Filters</a>
                                            </h3>
                                            <div class="clearfix"></div>
                                        </div>
                                        <div class="portlet-body">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <table class="table table-hover table-scroll" id="tblFilters">
                                                        <thead>
                                                            <tr>
                                                                <th>Filter Name</th>
                                                                <th>Label</th>
                                                                <th>Applicable To</th>
                                                                <th>Data Text Field</th>
                                                                <th>Data Value Field</th>
                                                                <th>Type</th>
                                                                <th>Render As</th>
                                                                <th>#</th>
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
                        </div>
                        </div>
                    </div>
                   </div>
                </div>
            </div>
        
 
    <div class="row data-table">
        <div class="col-sm-12  p-t-5">
            <div class="panel panel-default b-r-8">
                <div class="panel-body p-t-10">
                    <div class="row">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <table id="listTable" class="table table-hover table-scroll">
                                <thead>
                                    <tr>
                                        <th>Report Id</th>
                                        <th>Report</th>
                                        <th>Detail Report</th>
                                        <th>Primary Key </th>
                                        <th>Details Key </th>
                                        <th>Type</th>
                                        <th>Status</th>
                                        <th></th>

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

    </div>
    <%--Filter Modal--%>
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

    <%--Columns Modal--%>
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
                        <div class="col-sm-12">
                            <label class="control-label">Select Field</label>
                            <select id="ddlField" class="form-control">
                            </select>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px">

                        <div class="col-sm-6">
                            <label class="control-label">Display Name</label>
                            <input id="columnDisplayName" type="text" class="form-control" />
                        </div>
                        <div class="col-sm-6">
                            <div class="checkbox checkbox-inline checkbox-primary" style="padding:40px;">
                        <asp:CheckBox ID="chkSummable" ClientIDMode="Static" runat="server" Text="IsSummable" />
                                                    </div>
                        </div>
                    </div>


                    <div class="row" style="padding-top: 20px">
                        <div class="col-sm-12">
                            <button type="button" id="btnSaveColumn" class="btn btn-danger pull-right"><i class="md md-add-box"></i>&nbsp;Add Field</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            //list reports
            listItems();
            function listItems() {
                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/adhoc/GetDetails/',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        var response =data;
                        console.log(response);
                        $('#listTable').dataTable({
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ReportId' },
                                { data: 'ReportName' },
                                { data: 'DetailReportId' },
                                { data: 'PrimaryKeyColumn' },
                                { data: 'DetailKeyColumn' },
                                { data: 'TypeString' },
                                { data: 'Status', 'render': function (status) { if (status == 1) return '<label class="label label-default">&nbspActive&nbsp </label>'; else return '<label class="label label-danger">Inactive</label>' } },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();




                    },
                    error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                });
            }

            //Edit functionality
            $(document).on('click', '.edit-entry', function () {
                resetAll();

                var id = parseInt(($(this).closest('tr').children('td:nth-child(1)').text()));
                $('#hdReport').val(id);

                $.ajax({
                    url: $('#hdApiUrl').val() + 'api/adhoc/GetDetails/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        var itemEdit = response;


                        //
                        $.ajax({
                            url: $('#hdApiUrl').val() + 'api/adhoc/GetFields',
                            method: 'POST',
                            dataType: 'JSON',
                            contentType: 'application/json;charset=utf-8',
                            data: JSON.stringify(itemEdit.ReportViewObjectId),
                            success: function (response) {
                                var data = response;

                                LoadDropDownChanged(data, 'ddlKeyColumn');
                                LoadDropDownChanged(data, 'ddlDetailKey');
                                LoadDropDownChanged(data, 'ddlField');
                                LoadDropDownChanged(data, 'ddlApplicableTo');
                                LoadDropDownChanged(data, 'ddlDataValueColumn');
                                LoadDropDownChanged(data, 'ddlDataTextColumn');

                                $('#txtReportName').val(itemEdit.ReportName);
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
            });

            var DragObj;
            $('#tblColumns').on('dragstart', 'tr', function (evt) {

                //evt.originalEvent.dataTransfer.setData("text", JSON.stringify(evt.target));
                DragObj = evt.target;
                console.log(DragObj);
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

            $('#btnSaveColumn').click(function () {
                if ($('#ddlField').val() != null && $('#ddlField').val() !== '0' && $('#columnDisplayName').val() !== '') {
                    var columnName = $('#ddlField').children('option:selected').text();
                    var columnDispName = $('#columnDisplayName').val();
                    var summable=$('#chkSummable').is(':checked')
                       
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
            });
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

            $('#btnSave').click(function () {                          
                var Report = {};
                Report.ReportId = $('#hdReport').val();
                Report.ReportName = $('#txtReportName').val();
                Report.ReportView = $('#ddlViews').children('option:selected').text();
                Report.DefaultRows = $('#txtDefaultRows').val();
                Report.FilterAPI = $('#hdApiUrl').val() + 'api/AdHoc/FilterReport/';
                Report.DetailKeyColumn = $('#ddlDetailKey').children('option:selected').text();
                Report.DetailReportId = $('#ddlDetailViews').val();
                Report.PrimaryKeyColumn = $('#ddlKeyColumn').children('option:selected').text();
                Report.Type = $('#ddlReportType').val();
                Report.CreatedBy = $.cookie('bsl_3');
                Report.ModifiedBy = $.cookie('bsl_3');
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
            }

            //Delete functionality
            $('#btnDelete').click(function () {
                var id = $('#hdReport').val();
                swal({
                    title: "Delete?",
                    text: "Are you sure you want to delete?",
               
                    showConfirmButton: true,closeOnConfirm:true,
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
                                }
                            },
                            error: function (xhr) { alert(xhr.responseText); console.log(xhr); }
                        });
                    }

                });


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
