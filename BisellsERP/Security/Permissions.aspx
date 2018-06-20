<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Permissions.aspx.cs" Inherits="BisellsERP.security.Permissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        tr.parent {
            background-color: #90a4ae !important;
        }

            tr.parent a {
                color: #ECEFF1;
                font-size: 15px;
                font-weight: 600;
            }

                tr.parent a > i {
                    margin-right: 5px;
                }

        tr.child > td:first-child {
            padding-left: 25px;
        }

        tr > td:not(:first-child), tr > th:not(:first-child) {
            text-align: center;
            width: 95px;
        }

        #ddlGroup, #ddlUser {
            display: inline-block;
            width: 200px;
            margin-left: 10px;
            margin-right: 10px;
        }

        .checkbox, .radio {
            margin-top: 0;
            margin-bottom: 0;
        }

            .checkbox input[type=checkbox], .checkbox-inline input[type=checkbox], .radio input[type=radio], .radio-inline input[type=radio] {
                margin-left: -20px;
                top: -4px;
                height: 17px;
                width: 17px;
            }

            .checkbox label::after {
                margin-left: -21px;
            }

            .checkbox label::before {
                border: 1px solid #ef5350;
                background-color: #ef5350;
            }

            .checkbox input[type="checkbox"] + label::after {
                font-family: 'FontAwesome';
                content: "\f067";
                transform: rotate(45deg);
                color: #FAFAFA;
            }

            .checkbox input[type="checkbox"]:checked + label::after {
                transform: rotate(0deg);
            }

        .child.again-child td:first-child {
            padding-left: 50px;
        }

        .permission-table > tbody > tr > td {
            padding-top: 6px;
            padding-left: 6px;
        }

        .checkbox label::before {
            border: none;
            background-color: #f56a67;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <%--Page Title and Buttons--%>
    <div class="col-sm-8 col-sm-offset-2" >
            <div class="row">
        <div class="col-sm-4">
            <h3 class="page-title m-t-0">Set Permission</h3>
        </div>

        <div class="col-sm-8">
            <div class="btn-toolbar pull-right">
                <button type="button" id="savePermission" data-toggle="tooltip" data-placement="bottom" title="Save the current permission" class="btn btn-default waves-effect waves-light"><i class="md md-assignment-turned-in"></i>&nbsp Save</button>
            </div>
            <div class="pull-right">
                <div class="radio radio-success radio-inline">
                    <asp:RadioButton ID="rdGroup" ClientIDMode="Static" GroupName="permissionLevel" runat="server" Checked="true" />
                    <label for="inlineRadio1">Group </label>
                </div>
                <div class="radio radio-success radio-inline">
                    <asp:RadioButton ID="rdUser" ClientIDMode="Static" GroupName="permissionLevel" runat="server" />
                    <label for="inlineRadio2">Users </label>
                </div>
                <asp:DropDownList ID="ddlGroup" ClientIDMode="Static" runat="server" CssClass="form-control">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlUser" ClientIDMode="Static" runat="server" CssClass="form-control hidden">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="panel">
            <div class="panel-body">
                <div class="col-sm-12">

                    <table id="permissionTable" class="table table-hover table-striped permission-table">
                        <thead>
                            <tr>
                                <th>Permission Modules</th>
                                <th>View</th>
                                <th>Create</th>
                                <th>Update</th>
                                <th>Delete</th>
                                <th>All</th>
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



    <script>
        $(document).ready(function () {

            $('#savePermission').click(function () {
                var partyId = 0;
                var isUser = false;
                if ($('#rdUser').prop('checked')) {
                    isUser = true;
                    partyId = Number($('#ddlUser').val());
                }
                else {
                    isUser = false;
                    partyId = Number($('#ddlGroup').val());
                }

                if (partyId != 0) {
                    var objArray = [];
                    var permission = {};
                    var tr = $('#permissionTable tbody tr');
                    for (var i = 0; i < tr.length; i++) {
                        var obj = {};
                        obj.ModuleId = Number($(tr[i]).attr('value'));
                        obj.View = $(tr[i]).find('.view > input[type="checkbox"]').prop('checked');
                        obj.Delete = $(tr[i]).find('.delete > input[type="checkbox"]').prop('checked');
                        obj.Update = $(tr[i]).find('.update > input[type="checkbox"]').prop('checked');
                        obj.Create = $(tr[i]).find('.create > input[type="checkbox"]').prop('checked');
                        obj.All = $(tr[i]).find('.all > input[type="checkbox"]').prop('checked');
                        objArray.push(obj);
                    }
                    permission.PartyId = partyId;
                    permission.UserId = Number($.cookie('bsl_3'));
                    permission.Permissions = objArray;

                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Permissions/Save?isUser=' + isUser,
                        method: 'POST',
                        dataType: 'JSON',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify(permission),
                        success: function (data) {
                            if (data.Success) {
                                successAlert(data.Message);
                            }
                            else {
                                errorAlert(data.Message);
                            }
                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }



            })



            //Get group permission
            $('#ddlGroup').change(function () {
                if ($(this).val() != '0') {
                    var groupId = $(this).val();
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Permissions/GetGroupPermission?GroupId=' + groupId,
                        method: 'POST',
                        dataType: 'JSON',
                        success: function (data) {
                            console.log(data);
                            $('#permissionTable input[type="checkbox"]').prop('checked', false);
                            for (var i = 0; i < data.length; i++) {
                                var tr = $('#permissionTable tbody').children('tr[value="' + data[i].Module_Id + '"]');
                                tr.find('.view > input[type="checkbox"]').prop('checked', data[i].View);
                                tr.find('.delete > input[type="checkbox"]').prop('checked', data[i].Delete);
                                tr.find('.update > input[type="checkbox"]').prop('checked', data[i].Update);
                                tr.find('.create > input[type="checkbox"]').prop('checked', data[i].Create);
                                tr.find('.all > input[type="checkbox"]').prop('checked', data[i].All);
                            }

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }
                else {
                    $('#permissionTable input[type="checkbox"]').prop('checked', false);
                }
            });
            //Get user permission
            $('#ddlUser').change(function () {
                if ($(this).val() != '0') {
                    var userId = $(this).val();
                    $.ajax({
                        url: $('#hdApiUrl').val() + 'api/Permissions/GetUserPermission?UserId=' + userId,
                        method: 'POST',
                        dataType: 'JSON',
                        success: function (data) {
                            console.log(data);
                            $('#permissionTable input[type="checkbox"]').prop('checked', false);
                            for (var i = 0; i < data.length; i++) {
                                var tr = $('#permissionTable tbody').children('tr[value="' + data[i].module_id + '"]');
                                tr.find('.view > input[type="checkbox"]').prop('checked', data[i].View);
                                tr.find('.delete > input[type="checkbox"]').prop('checked', data[i].Delete);
                                tr.find('.update > input[type="checkbox"]').prop('checked', data[i].Update);
                                tr.find('.create > input[type="checkbox"]').prop('checked', data[i].Create);
                                tr.find('.all > input[type="checkbox"]').prop('checked', data[i].All);
                            }

                        },
                        error: function (xhr) {
                            alert(xhr.responseText);
                            console.log(xhr);
                            miniLoading('stop');
                        },
                        beforeSend: function () { miniLoading('start'); },
                        complete: function () { miniLoading('stop'); },
                    });
                }
                else {
                    $('#permissionTable input[type="checkbox"]').prop('checked', false);
                }
            });

            //Group Dropdown Change
            $('#rdUser').change(function () {
                $('#permissionTable input[type="checkbox"]').prop('checked', false);
                if ($(this).prop('selected')) {
                    $('#ddlGroup').removeClass('hidden');
                    $('#ddlUser').addClass('hidden');
                }
                else {
                    $('#ddlGroup').addClass('hidden');
                    $('#ddlUser').removeClass('hidden');
                }
            });

            //User Dropdown Change
            $('#rdGroup').change(function () {
                if ($(this).prop('selected')) {
                    $('#ddlGroup').addClass('hidden');
                    $('#ddlUser').removeClass('hidden');
                }
                else {
                    $('#ddlGroup').removeClass('hidden');
                    $('#ddlUser').addClass('hidden');
                }
            });

            //Toggle all checkbox if all is Checked
            $(document).on('change', '.permission-table tr > td:last-child', function () {
                var checkboxStat = $(this).find('input[type="checkbox"]').is(':checked');
                var allCheckbox = $(this).closest('tr').find('td').not(':first').find('input[type="checkbox"]');
                checkboxStat ? allCheckbox.prop('checked', true) : allCheckbox.prop('checked', false);
            });
            //Toggle all checkbox if view is Unckecked
            $(document).on('change', '.view > input[type="checkbox"]', function () {
                var checkboxView = $(this).find('input[type="checkbox"]').is(':checked');
                var allCheckbox = $(this).closest('tr').find('td').slice(2, 6).find('input[type="checkbox"]');
                //if (!checkboxView) {
                //    allCheckbox.prop('checked', false);
                //}
            });
            //Untick Checkbox All if others are not checked
            $(document).on('change', '.view > input[type="checkbox"], .create > input[type="checkbox"], .update > input[type="checkbox"], .delete > input[type="checkbox"]', function () {
                //All checkbox excluding (all)
                var checkboxExceptAll = $(this).closest('tr').find('td').not(':first, :last').find('input[type="checkbox"]:checked');
                //Current Checkbox (all)
                var currentCheckboxAll = $(this).closest('tr').find('td').find('.all > input[type="checkbox"]');
                checkboxExceptAll.length != 4 ? currentCheckboxAll.prop('checked', false) : currentCheckboxAll.prop('checked', true);
            });
            //Check View Checkbox condition
            $(document).on('change', '.create > input[type="checkbox"], .update > input[type="checkbox"], .delete > input[type="checkbox"]', function () {
                //All checkbox excluding (all) and (view)
                var checkboxExceptAllView = $(this).closest('tr').find('td').slice(2, 5).find('input[type="checkbox"]:checked');
                //Current Checkbox (view)
                var currentCheckboxView = $(this).closest('tr').find('td').find('.view > input[type="checkbox"]');
                if (checkboxExceptAllView.length != 0) {
                    currentCheckboxView.prop('checked', true);
                }
            });

            //Permission Parent Collapsible
            $('body').on('click', '.parent a', function () {
                $(this).closest('.parent').nextUntil('tr.parent').slideToggle();
                $(this).closest('.parent').find('i').toggleClass('ion-ios7-plus-outline');
            });

            $.ajax({
                url: $('#hdApiUrl').val() + 'api/Permissions/GetModules',
                method: 'POST',
                dataType: 'JSON',
                success: function (response) {

                    var html = '';
                    for (var i = 0; i < response.length; i++) {
                        html += ' <tr class="parent" value="' + response[i].ModuleId + '"> <td><a href="javascript:void()"><i class="ion-ios7-minus-outline"></i>' + response[i].Name + '</a></td><td> <div class="checkbox checkbox-success view"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success create"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success update"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success delete"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success all"> <input type="checkbox"/> <label></label> </div></td></tr>';
                        if (response[i].HasChildren) {
                            for (var j = 0; j < response[i].Children.length; j++) {
                                html += '<tr class="child" value="' + response[i].Children[j].ModuleId + '"> <td>- ' + response[i].Children[j].Name + '</td><td> <div class="checkbox checkbox-success view"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success create"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success update"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success delete"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success all"> <input type="checkbox"/> <label></label> </div></td></tr>';
                                if (response[i].Children[j].HasChildren) {
                                    for (var k = 0; k < response[i].Children[j].Children.length; k++) {
                                        html += '<tr class="child again-child" value="' + response[i].Children[j].Children[k].ModuleId + '"> <td>' + response[i].Children[j].Children[k].Name + '</td><td> <div class="checkbox checkbox-success view"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success create"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success update"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success delete"> <input type="checkbox"/> <label></label> </div></td><td> <div class="checkbox checkbox-success all"> <input type="checkbox"/> <label></label> </div></td></tr>'
                                    }
                                }
                            }
                        }
                    }
                    $('#permissionTable tbody').append(html);
                }
            });

        });
    </script>
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
</asp:Content>
