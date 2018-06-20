<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserEntities.aspx.cs" Inherits="BisellsERP.Application.UserEntities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #wrapper, body {
            overflow: hidden;
        }

        .masters-wrap > div {
            height: calc(100vh - 83px);
        }

            .masters-wrap > div .nav {
                height: calc(100vh - 135px);
                background-color: #fff;
                box-shadow: 0 2px 1px 0 #ccc;
            }

        .masters-wrap .nav > li {
            border-bottom: 1px solid #f3f3f3;
        }

            .masters-wrap .nav > li > a {
                color: #90A4AE !important;
            }

                .masters-wrap .nav > li > a:hover {
                    color: #757575 !important;
                }

            .masters-wrap .nav > li.active > a {
                color: #3cba9f !important;
            }

            .masters-wrap .nav > li.active {
                border-left: 3px solid #3cba9f;
            }

            .masters-wrap .nav > li > a:focus, .nav > li > a:hover {
                background-color: transparent;
            }

            .masters-wrap .nav > li > a {
                line-height: 35px;
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

        .tab-pane label {
            /*color: #78909c;*/
            font-weight: 100;
            text-align: left !important;
            font-size: 12px;
        }

            .tab-pane label > i {
                margin-left: 2px;
                color: #B0BEC5;
            }

        .tab-pane .form-group {
            margin-bottom: 5px;
        }

        li.list-break > p {
            margin-bottom: 0;
            background-color: #FAFAFA;
            padding: 8px;
        }

        /* User Group Styling */
        .popver label {
            color: #78909c;
        }

        .user-portlet {
            border: 1px solid #eee;
            border-radius: 5px;
        }

            .user-portlet .portlet-heading {
                padding: 5px 20px;
            }

            .user-portlet .portlet-body {
                padding: 0px 15px;
            }

        .group-users {
            width: 100%;
        }

            .group-users li {
                display: inline-block;
                width: 19.64%;
                padding: 15px 5px;
                border: 1px dashed #eee;
                border-radius: 5px;
            }

                .group-users li:hover {
                    border: 1px dashed #ccc;
                }

            .group-users a, .group-users a:active {
                color: #90A4AE;
                outline: none;
            }

                .group-users a:hover {
                    color: #E57373;
                }

            .group-users .fullname {
                font-weight: 700;
                color: #78909C;
                font-size: 12px;
            }

            .group-users .username {
                color: #90A4AE;
                font-size: 12px;
            }

        .btn-upload {
            background-color: transparent;
            border: 1px solid #4abfa6;
            border-radius: 20px;
            opacity: .8;
            padding: 3px 12px;
            transition: all .3s ease-in-out;
        }
            .btn-upload:hover {
                opacity: .9;
                background-color: #eefffb;
                color: #4abfa6;
                box-shadow: none;
            }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="masters-wrap">
        <div class="col-sm-2 p-0">
            <h3 class="m-t-0">Accounts</h3>
            <div class="tabs-vertical-env">
                <ul class="nav" id="menu">
                    <li class="active" id="User">
                        <a href="#v-1" data-toggle="tab" aria-expanded="true">User</a>
                    </li>
                    <li class="" id="Groups">
                        <a href="#v-2" data-toggle="tab" aria-expanded="false">Groups</a>
                    </li>
                    <li class="" id="Roles">
                        <a href="#v-3" data-toggle="tab" aria-expanded="true">Roles</a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="col-sm-10 p-0">
            <div class="col-md-12 m-t-40">
                <div class="tab-content">
                    <%-- User MASTER --%>
                    <div class="tab-pane active" id="v-1">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">

                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New User</h5>

                                        <div class="row">
                                            <label class="control-label col-sm-4 m-l--5 m-t-20"></label>
                                            <div class="col-sm-8 m-b-15">
                                                <div class="col-sm-2">
                                                    <img id="imgPhoto" src="../Theme/images/profile-pic.jpg" style="width:100%" class="img-circle profile-pic" />
                                                </div>
                                                <div class="col-sm-5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10">
                                                        <span><i class="ion-upload m-r-5"></i>Upload</span>
                                                        <input type="file" class="upload" id="btnProfileUpload"/>
                                                    </div>
                                                </div>
                                                <div class="col-sm-5 m-r--5">
                                                    <div class="fileUpload btn btn-upload btn-block btn-sm m-t-10 remove-pic">
                                                        <span id="btnRemoveProfile"><i class="ion-trash-b m-r-5"></i>Remove</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Full Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtUserFullName" class="form-control input-sm" placeholder="Name" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Email</label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtUserEmail" required="true" data-validation="email" data-validation-error-msg="<small style='color:red'>Invalid Email Format</small>" runat="server" CssClass="form-control input-sm" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Location</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" class="form-control input-sm" ID="ddlUserLocation" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Associated Employee</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList runat="server" class="form-control input-sm" ID="ddlEmpolyee" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group password">
                                                        <label class="control-label col-sm-4">Password</label>
                                                        <div class="col-sm-8">
                                                            <input type="password" id="txtUserPassword" required="required" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group password">
                                                        <label class="control-label col-sm-4">Confirm Password</label>
                                                        <div class="col-sm-8">
                                                            <input type="password" id="txtConfirmPassword" required="required" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div style="display:none" class="form-group">
                                                        <div class="col-sm-6">
                                                            <div class="checkbox checkbox-primary">
                                                                <asp:CheckBox ID="chkDisableUser" runat="server" CssClass="checkbox checkbox-primary" ClientIDMode="Static" Text="Disable user" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="checkbox checkbox-primary">
                                                                <asp:CheckBox ID="chkChangeNext" runat="server" CssClass="checkbox checkbox-primary" ClientIDMode="Static" Text="Password Change at next time" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="display:none" class="form-group">
                                                        <div class="col-sm-6">
                                                            <div class="radio radio-primary radio-inline">
                                                                <asp:RadioButton ID="rdExpirepass" runat="server" CssClass="radio radio-primary" Text="Password Expires" GroupName="PasswordExpires" ClientIDMode="Static" />
                                                            <select id="ddlSelectDays" class="form-control hidden">
                                                                <option value="0">--Select--</option>
                                                                <option value="30">After 30 Days</option>
                                                                <option value="40">After 40 Days</option>
                                                                <option value="60">After 60 Days</option>
                                                            </select>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="radio radio-primary radio-inline">
                                                                <asp:RadioButton ID="rdneverExpires" runat="server" CssClass="radio radio-primary" Text="Password Never Expires" GroupName="PasswordExpires" ClientIDMode="Static" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs hidden" id="btnResetPassword">Reset Password</button>
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddUser">Save</button>
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs hidden" id="btnUpdateUser"><i class=""></i>&nbsp;<span>Update</span></button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">User List</h5>
                                        <table id="tableUser" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>User Name</th>
                                                    <th>Full Name</th>
                                                    <th>Employee Name</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- Groups Master --%>
                    <div class="tab-pane" id="v-2">
                        <div class="panel">
                            <div class="panel-body">

                                <div class="row">
                                    <div class="col-sm-5">
                                        <h5 class="sett-title p-t-0">Add New Group</h5>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-horizontal">
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Name</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtGroupName" placeholder="Name" class="form-control input-sm" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-sm-4">Description</label>
                                                        <div class="col-sm-8">
                                                            <textarea id="txtGroupDescritpion" rows="3" cols="50" placeholder="Description" class="form-control input-sm"></textarea>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddnewGroup">Save</button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7">
                                        <h5 class="sett-title p-t-0">Group List</h5>
                                        <table id="tableuserGroup" class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>Group Name</th>
                                                    <th>Description</th>
                                                    <th>#</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <%--Roles--%>
                    <div class="tab-pane" id="v-3">
                        <div class="panel">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <h5 class="sett-title p-t-0">Add New Roles</h5>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="portlet user-portlet">
                                                    <div class="portlet-heading portlet-default">
                                                        <div class="col-sm-6">
                                                            <h3 class="portlet-title text-dark">
                                                                <label id="lblGroupName" style="font-size: 18px;">No Group Selected</label>&nbsp;<a href="#" id="changeGroupPopover"><i class="fa fa-edit"></i></a><label class="hidden" id="lblGroupId">0</label></h3>
                                                            <div id="rolesWrap" class="hide">
                                                                <label>Choose Group :</label>
                                                                <asp:DropDownList ID="ddlGroup" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                                <div class="btn-toolbar pull-right m-t-10 m-b-10">
                                                                    <button id="btnSelectGroup" type="button" class="btn btn-default btn-sm hidden">Select</button>
                                                                    <button id="roleCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 text-right">
                                                            <div class="m-t-5">
                                                                <a href="#" id="addUserPopover">Add User&nbsp;<i class="ion-plus"></i></a>
                                                                <div id="userWrap" class="hide">
                                                                    <label>Select User :</label>
                                                                    <asp:DropDownList ID="ddlUser" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                                                    <div class="btn-toolbar pull-right m-t-10 m-b-10">
                                                                        <button id="addUser" type="button" class="btn btn-default btn-sm">Add</button>
                                                                        <button id="userCancel" type="button" class="btn btn-inverse btn-sm">x</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div id="bg-default" class="panel-collapse collapse in" aria-expanded="true" style="">
                                                        <div class="portlet-body">
                                                            <ul class="group-users list-unstyled">
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="btn-toolbar m-t-20">
                                                    <button type="button" class="btn btn-green waves-effect waves-light btn-xs" id="btnAddnewRole">Update</button>
                                                    <button type="button" class="btn btn-default waves-effect waves-light btn-xs refresh-data" id="btnResetRole"><i class="fa fa-refresh"></i>&nbsp;<span>Reset</span></button>
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
        <input type="hidden" id="hdnGroupID" value="0" />
        <input type="hidden" id="hdnUserID" value="0" />
        <div id="PasswordModal" class="modal animated fadeIn" role="dialog">
            <div class="modal-dialog modal-dialog-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close text-inverse" style="opacity: .5" data-dismiss="modal" aria-hidden="true"><i class="ion-close-circled"></i></button>
                        <h4 class="modal-title">Reset Password</h4>
                    </div>
                    <div class="modal-body p-b-0">
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="control-label">Password</label>
                                    <input type="password" id="txtNewPassword" class="form-control" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label class="control-label">Confirm Password</label>
                                    <input type="password" id="txtNewPassword1" class="form-control" />
                                </div>
                            </div>
                            <input type="hidden" id="hdnUserIDforReset" value="0" />
                            <div class="col-md-12 pull-right">
                                <div class="btn-toolbar pull-right m-t-15">
                                    <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                    <button id="btnUpdatePassword" type="button" class="btn btn-blue btn-job">Update Password</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#btnProfileUpload').change(function () {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgPhoto').attr('src', e.target.result);
                }
                reader.readAsDataURL(this.files[0]);
            });

            $(".tab-content > .tab-pane > .panel").niceScroll({
                cursorcolor: "#90A4AE",
                cursorwidth: "8px",
                horizrailenabled: false
            });

            $('.remove-pic').click(function () {
                $('#imgPhoto').attr('src', '../Theme/images/profile-pic.jpg');
            });

            //Change Role Popover Function
            $(function () {
                $('#changeGroupPopover').popover({
                    placement: 'right',
                    html: true,
                    content: $('#rolesWrap').html()
                }).on('click', function () {
                    //inititalize select 2 ddl
                    $("#ddlGroup").select2({
                        width: '100%'
                    });

                    // Apply Group Filter Click
                    $('#ddlGroup').change(function () {
                        $('#changeGroupPopover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                        //Change Role Logic Below
                        console.log($('#ddlGroup').val());
                        var groupId = $('#ddlGroup').val();
                        var groupName = $('#ddlGroup').find("option:selected").text();
                        console.log(groupName);
                        $.ajax({
                            url: apirul + '/api/Users/GetUserInGroup?GroupId=' + groupId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (response) {
                                console.log(response);
                                var html = '';
                                $('.group-users.list-unstyled').children().remove();
                                $(response).each(function () {
                                    html += "<li userId='" + this.ID + "' groupId='" + groupId + "'> <div class='col-sm-10'>";
                                    html += "<div class='fullname'>" + this.FullName + "</div>";
                                    html += "<div class='username'>" + this.UserName + "</div></div>";
                                    html += "<div class='col-sm-2'><a href='#' class='remove-user'><i class='md-remove-circle m-t-10'></i></a></div></li>";
                                });
                                $('#lblGroupName').text(groupName);
                                $('#lblGroupId').text(groupId);
                                $('.group-users.list-unstyled').append(html)
                            }
                        });
                    });
                    // Cancel Filter Click
                    $('#roleCancel').click(function () {
                        $('#changeGroupPopover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                })
            });
            //Reset in Role
            $('#btnResetRole').click(function () {
                $('.group-users.list-unstyled').children().remove();
                $('#lblGroupName').text('No Group Selected');
                $('#lblGroupId').text('0');
            })
            //Save in Roles
            $('#btnAddnewRole').click(function () {
                var data = {};
                var arr = [];
               
                var userGroupId = $('#lblGroupId').text();
                var userList = $('.group-users.list-unstyled li');

                userList.each(function (index, li) {
                    var detail = {};
                    var userId = $(this).attr('userId');
                    console.log(userId);
                    detail.ID = userId;
                    arr.push(detail);

                });
                data.ID = userGroupId;
                data.CreatedBy = $.cookie('bsl_3')
                data.AssisgnedUsers = arr;
                console.log(data);
             
                $.ajax({
                    url: apirul + '/api/UserGroup/SaveRoles',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify(data),
                    success: function (response) {
                        if (response.Success) {
                            successAlert(response.Message)
                        }
                        else {
                            errorAlert(response.Message);
                        }
                    }
                });

            })
            //End of Save roles
            //Add User Popover Function
            $(function () {
                $('#addUserPopover').popover({
                    placement: 'bottom',
                    html: true,
                    content: $('#userWrap').html()
                }).on('click', function () {
                    //inititalize select 2 ddl
                    $("#ddlUser").select2({
                        width: '100%'
                    });

                    // Apply Filter Click
                    $('#addUser').click(function () {
                        $('#addUserPopover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                        //Change User Logic Below
                        var groupId = $('#lblGroupId').text();
                        var userId = $('#ddlUser').val();
                        $.ajax({
                            url: apirul + '/api/Users/GetUser?UserId=' + userId,
                            method: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'Json',
                            data: JSON.stringify($.cookie("bsl_1")),
                            success: function (response) {
                                console.log(response);
                                //check for user is already exists in the group
                                var userIsExist = $('.group-users.list-unstyled').children('li[userId="' + userId + '"][groupId="' + groupId + '"]').length;
                                if (userIsExist > 0) {
                                    errorAlert('User is already exists in this group');
                                }
                                else {


                                    var html = '';
                                    $(response).each(function () {
                                        html += "<li userId='" + this.ID + "' groupId='" + groupId + "'> <div class='col-sm-10'>";
                                        html += "<div class='fullname'>" + this.FullName + "</div>";
                                        html += "<div class='username'>" + this.UserName + "</div></div>";
                                        html += "<div class='col-sm-2'><a href='#' class='remove-user'><i class='md-remove-circle m-t-10'></i></a></div></li>";
                                    });
                                    $('.group-users.list-unstyled').append(html)
                                }
                            }
                        });
              
                    })
                    // Cancel Filter Click
                    $('#userCancel').click(function () {
                        $('#addUserPopover').popover('hide');
                        $('body').on('hidden.bs.popover', function (e) {
                            $(e.target).data("bs.popover").inState = { click: false, hover: false, focus: false };
                        });
                    })
                })
            });

            //Remove Added User from Roles
           
            $('.group-users.list-unstyled').on('click','.remove-user',function () {
                $(this).closest('li').fadeOut('slow', function () {
                    $(this).remove().sli;
                });
            });
          

            $('#rdExpirepass').click(function () {
                $('#ddlSelectDays').removeClass('hidden');
            });
            $('#rdneverExpires').click(function () {
                $('#ddlSelectDays').addClass('hidden');
            });

            $('#rdneverExpires').attr("checked", "checked");


            //Fetching API url
            var apirul = $('#hdApiUrl').val();

            $('#menu > li').click(function () {
                var select = $(this).attr('id');
                //$('#v-1').removeClass('active');
                switch (select) {
                    case 'User':
                        //$('#menu >li').removeClass('active');
                        //$('#User').addClass('active');
                        //$('#v-1').addClass('active');
                        RefeshTableUser();
                        break;
                    case 'Groups':
                        //$('#menu >li').removeClass('active');
                        //$('#Groups').addClass('active');
                        //$('#v-2').addClass('active');
                        RefreshTableGroups();
                        break;
                    case 'Roles':                      
                        //$('#menu >li').removeClass('active');
                        //$('#Roles').addClass('active');
                        //$('#v-3').addClass('active');                    
                        break;
                    default:
                        break;

                }

            });

            RefeshTableUser();


            //Tables for Users
            function RefeshTableUser() {
                $.ajax({
                    url: apirul + '/api/Users/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableUser').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'UserName' },
                                { data: 'FullName' },
                                { data: 'EmployeeId' },
                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-user"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-user"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    }
                });
            }


            //Table for User Groups
            function RefreshTableGroups() {
                $.ajax({
                    url: apirul + '/api/UserGroup/get',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        $('#tableuserGroup').dataTable({
                            responsive: true,
                            dom: 'Blfrtip',
                            lengthMenu: [[10, 25, 50, -1], ['10 rows', '25 rows', '50 rows', 'Show all']],
                            buttons: ['excel', 'csv', 'print'],
                            data: response,
                            destroy: true,
                            columns: [
                                { data: 'ID', className: 'hidden-td' },
                                { data: 'Name' },
                                { data: 'Description' },

                                {
                                    data: function () {
                                        return '<a href="#" data-toggle="tooltip" data-placement="auto left" title="Edit" class="edit-entry-group"><i class="fa fa-edit"></i></a><span class="divider">&nbsp&nbsp&nbsp</span><a data-toggle="tooltip" data-placement="auto left" title="Delete" href="#" class="delete-entry-group"><i class="fa fa-times" style="color:red"></i></a>'
                                    },
                                    sorting: false
                                }
                            ]
                        });
                        $('[data-toggle="tooltip"]').tooltip();

                    }
                });
            }

            //Saves User
            $('#btnAddUser').click(function () {
                var User = {};
                User.UserName = $('#txtUserEmail').val();
                User.Password = $('#txtUserPassword').val();
                User.FullName = $('#txtUserFullName').val();
                User.EmployeeId = $('#ddlEmpolyee').val();
                if ($('#chkChangeNext').prop("checked") == true) {
                    User.ForcePasswordChange = true;
                }
                else {
                    User.ForcePasswordChange = false;
                }
                if ($('#rdExpirepass').prop("checked") == true) {
                    User.ExpiryPeriod = $('#ddlSelectDays').val();
                }
                else {
                    User.ExpiryPeriod = 0;
                }
                User.LocationId = $('#ddlUserLocation').val();
                User.ID = $('#hdnUserID').val();
                if ($('#chkDisableUser').prop("checked") == true) {
                    User.Disable = true;
                }
                else {
                    User.Disable = false;
                }
                User.ProfileImageB64 = $('#imgPhoto').attr('src').split(',')[1];
                User.CompanyId = $.cookie("bsl_1");
                User.CreatedBy = $.cookie('bsl_3');
                User.ModifiedBy = $.cookie('bsl_3');
                console.log(User);
                if ($('#txtUserPassword').val().length>6 ||User.ID!=0) {//User.ID!=0 is used to update the user.Otherwise always shows password length is less
                    if ($('#txtUserPassword').val() == $('#txtConfirmPassword').val()) {
                        $.ajax({
                            url: apirul + 'api/Users/save',
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify(User),
                            contentType: 'application/json;charset=utf-8',
                            success: function (data) {
                                if (data.Success) {
                                    $('#btnAddUser').html('Save');
                                    $('#hdnUserID').val("0");
                                    successAlert(data.Message);
                                    reset();
                                    RefreshRegister()
                                    RefeshTableUser();
                                }
                                else {
                                    errorAlert(data.Message);
                                }
                            },
                            error: function (xhr) {
                                errorAlert(data.Message);
                            }
                        });
                    }
                    else {
                        errorAlert("Password doesn't match");
                    }
                }
                else {
                    errorAlert('Atleast 6 charcters required');
                }
            });

            //Saves Group
            $('#btnAddnewGroup').click(function () {
                var Group = {};
                Group.Name = $('#txtGroupName').val();
                Group.Description = $('#txtGroupDescritpion').val();
                Group.CompanyId = $.cookie("bsl_1");
                Group.CreatedBy = $.cookie('bsl_3');
                Group.ModifiedBy = $.cookie('bsl_3');
                Group.ID = $('#hdnGroupID').val();
                $.ajax({
                    url: apirul + 'api/UserGroup/save',
                    method: 'POST',
                    dataType: 'JSON',
                    data: JSON.stringify(Group),
                    contentType: 'application/json;charset=utf-8',
                    success: function (data) {
                        if (data.Success) {
                            $('#btnAddnewGroup').html('Save');
                            $('#hdnGroupID').val("0");
                            successAlert(data.Message);
                            reset();
                            RefreshTableGroups();
                            RefreshRegister();
                        }
                        else {
                            errorAlert(data.Message);
                        }
                    },
                    error: function (xhr) {
                        errorAlert(data.Message);
                    }
                });
            });

            //User Edit
            $(document).on('click', '.edit-entry-user', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/Users/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtUserEmail').val(response.UserName);
                        $('#txtUserFullName').val(response.FullName);
                        $('#ddlUserLocation').val(response.LocationId);
                        $('#ddlEmpolyee').val(response.EmployeeId);
                        if (response.ExpiryPeriod > 0) {
                            $('#rdExpirepass').prop("checked", true);
                            $('#ddlSelectDays').removeClass('hidden');
                        }
                        else {
                            $('#rdneverExpires').prop("checked", true);
                            $('#ddlSelectDays').addClass('hidden');
                        }
                        $('#ddlSelectDays').val(response.ExpiryPeriod);
                        $('#chkChangeNext').val(response.ForcePasswordChange);
                        $('#hdnUserID').val(response.ID);
                        $('#btnAddUser').html('Update');
                        $('#btnResetPassword').removeClass('hidden');   
                        $('.password').addClass('hidden');
                        
                        $('#imgPhoto').attr('src', response.ProfileImagePath != '' ? response.ProfileImagePath : '../Theme/images/profile-pic.jpg');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //Group Edit
            $(document).on('click', '.edit-entry-group', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());
                $.ajax({
                    url: apirul + '/api/UserGroup/get/' + id,
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'Json',
                    data: JSON.stringify($.cookie("bsl_1")),
                    success: function (response) {
                        reset();
                        $('#txtGroupName').val(response.Name);
                        $('#txtGroupDescritpion').val(response.Description)
                        $('#hdnGroupID').val(response.ID);
                        $('#btnAddnewGroup').html('Update');
                    },
                    error: function (xhr) {
                        alert(xhr.responseText);
                        console.log(xhr);
                    }
                });
            });

            //User Delete
            $(document).on('click', '.delete-entry-user', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                deleteMaster({
                    "url": apirul + '/api/Users/Delete/',
                    "id": id,
                    "successMessage": 'User has been deleted ',
                    "successFunction": RefeshTableUser

                });

            });

            //Group Delete
            $(document).on('click', '.delete-entry-group', function () {

                var id = parseInt($(this).closest('tr').children('td:nth-child(1)').text());

                deleteMaster({
                    "url": apirul + '/api/UserGroup/Delete/',
                    "id": id,
                    "successMessage": 'User has been deleted ',
                    "successFunction": RefreshTableGroups
                });

            });

            $('#btnResetPassword').click(function () {
                $('#PasswordModal').modal('show');
            });

            $('#btnUpdatePassword').click(function(){
                var newPassword=$('#txtNewPassword').val();
                var RePassword=$('#txtNewPassword1').val();
                //console.log($('#hdnUserID').val()+ ' '+newPassword +' '+RePassword);
                var User = {};
                User.Password = $('#txtNewPassword').val();
                User.ID = $('#hdnUserID').val();
                User.ModifiedBy = $.cookie('bsl_3');
                if ($('#txtNewPassword').val().length >= 8 && $('#txtNewPassword').val().length <= 16) {
                    if (newPassword == RePassword) {
                        $.ajax({
                            url: apirul + 'api/Users/Updatepassword',
                            method: 'POST',
                            dataType: 'JSON',
                            data: JSON.stringify(User),
                            contentType: 'application/json;charset=utf-8',
                            success: function (data) {
                                if (data.Success) {
                                    $('#PasswordModal').modal('hide');
                                    $('#btnResetPassword').addClass('hidden');
                                    $('#btnAddUser').html('Save');
                                    $('#hdnUserID').val("0");
                                    $('.password').removeClass('hidden');
                                    successAlert(data.Message);
                                    reset();
                                    RefeshTableUser();
                                }
                                else {
                                    errorAlert(data.Message);
                                }
                            },
                            error: function (xhr) {
                                errorAlert(data.Message);
                            }
                        });
                    }
                    else {
                        errorAlert("Passwords doesn't match");
                    }
                }
                else {
                    errorAlert('Password should be in between 8 and 16 characters length');
                }
            });

            $('.refresh-data').click(function () {
                RefreshRegister();
            });

            function RefreshRegister() {
                reset();
                $('#btnResetPassword').addClass('hidden');
                $('#btnAddUser').html('Save');
                $('#hdnUserID').val("0");
                $('.password').removeClass('hidden');
                $('#btnAddnewGroup').html('Save');
                $('#hdnGroupID').val("0");
                $('#imgPhoto').attr('src','../Theme/images/profile-pic.jpg');
            }
        });
    </script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/assets/sweet-alert/sweet-alert.init.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
   <%-- <script src="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/jquery.form-validator.min.js"></script>--%>
    <script src="../Theme/assets/jquery-form-validator/jquery.form.validator.js"></script>
</asp:Content>
