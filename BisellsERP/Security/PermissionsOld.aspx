<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PermissionsOld.aspx.cs" Inherits="BisellsERP.Application.PermissionsOld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .toggle-centre {
            padding: 0 25%;
        }
        .vh-60 {
            min-height: 44vh;
            height: 100%;
            overflow: auto;
            max-height: 70vh;
        }
        .p-t-10p {
            padding-top: 5% !important;
            position: relative;
        }
        .module-label {
            position: absolute;
            top: -25px;
            background-color: rgb(0, 71, 103);
            padding: 5px 15px;
            border-radius: 4px;
            color: whitesmoke;
        }
        .panel {
            border-radius: 5px;
        }
        /*@import url(..\Theme\assets\font-awesome\css\font-awesome.min.css);*/
        div.divTogglePerm label {
            position: relative;
            padding-left: 30px;
            font-size: 14px;
            cursor: pointer;
            margin-left: 4px;   
        }

            div.divTogglePerm label:before, div.divTogglePerm label:after {
                font-family: FontAwesome;
                font-size: 28px;
                position: absolute;
                top: 0;
                left: 0;
                color: #41bd76;
                /*font-weight: 100;*/
            }

            div.divTogglePerm label:before {
                content: '\f096'; /*unchecked*/
                color: #d0d5d8;
            }

            div.divTogglePerm label:after {
                content: '\f00c'; /*checked*/
                max-width: 0;
                overflow: hidden;
                opacity: 0.5;
                top: -3px;
                left: 1px;
                font-weight: 100;
                transition: all 0.35s cubic-bezier(1, 0.32, 1, 1);
            }

            div.divTogglePerm label.clicked:after {
                content: '\f00c'; /*checked*/
                max-width: 60px;
                top: -3px;
                left: 1px;
                font-weight: 100;
                opacity: 1;
                transition: all 0.35s cubic-bezier(1, 0.32, 1, 1);
            }

        div.divTogglePerm input[type="checkbox"] {
            display: none;
        }
            div.divTogglePerm input[type="checkbox"]:checked > span + label:after {
                max-width: 25px; /*an arbitratry number more than the icon's width*/
                opacity: .5; /*for fade in effect*/
            }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="">

        <%--hidden fields--%>
        <%--Page Title and Buttons--%>
        <div class="row">
            <div class="col-sm-4">
                <h4 class="pull-left page-title">Set Permission</h4>
            </div>
            <div class="col-sm-8">
                <div class="btn-toolbar pull-right" role="group" aria-label="...">
                    <button type="button" class="btn btn-success waves-effect waves-light"><i class="md md-assignment-turned-in"></i>&nbsp Save</button>
                    <button type="button" class="btn btn-success waves-effect waves-light"><i class="md md-print"></i>&nbsp Print</button>
                    <button type="button" class="btn btn-danger waves-effect waves-light"><i class="md md-assignment-returned"></i>&nbsp Export</button>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%-- LEFT SIDE --%>
                <div class="col-sm-3">
                    <div class="panel">
                        <div class="panel-heading">
                            <h4>Select Module</h4>
                        </div>
                        <div class="panel-body vh-60 p-t-0">
                            <asp:Panel ID="Panel1" runat="server">
                                <asp:TreeView ID="moduleTree" runat="server" CssClass="nunito-font" OnSelectedNodeChanged="moduleTree_SelectedNodeChanged" Font-Size="Small" ForeColor="#004767" LineImagesFolder="~/TreeLineImages">
                                    <LeafNodeStyle HorizontalPadding="5px" />
                                    <NodeStyle HorizontalPadding="5px" />
                                    <ParentNodeStyle Font-Bold="True" />
                                    <RootNodeStyle Font-Bold="True" />
                                    <SelectedNodeStyle Font-Bold="True" ForeColor="#41BC76" />
                                </asp:TreeView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>

                <%-- RIGHT SIDE --%>
                <div class="col-sm-9">

                    <%-- Search User/Group --%>
                    <div class="panel">
                        <div class="panel-heading">
                            <h4>Select Permission Type</h4>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-6 col-md-4">
                                    <div class="radio radio-primary radio-inline p-t-5">
                                        <asp:RadioButton ID="rdGroup" AutoPostBack="true" GroupName="permissionLevel" runat="server" OnCheckedChanged="rdGroup_CheckedChanged" />
                                        <label for="inlineRadio1">Group </label>
                                    </div>
                                    <div class="radio radio-primary radio-inline p-t-5">
                                        <asp:RadioButton ID="rdUser" GroupName="permissionLevel" AutoPostBack="true" runat="server" OnCheckedChanged="rdUser_CheckedChanged" />
                                        <label for="inlineRadio2">Users </label>
                                    </div>
                                </div>

                                <div class="col-sm-6 col-md-4">
                                    <div class="row">
                                        <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="false" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlUser" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <%-- Permission Tick Panel --%>
                    <div class="panel m-t-30">

                        <div class="panel-body p-t-10p" style="padding-bottom: 40px;">
                            <h4 class="module-label">
                                <asp:Label ID="lblModule" runat="server" Text="Set Permission"></asp:Label>
                            </h4>

                            <div class="row">
                                <%-- VIEW --%>
                                <div class="col-sm-2 text-center">
                                    <div class="col-sm-12">
                                        <label>View</label>
                                    </div>
                                    <div class="col-sm-12 toggle-centre">
                                        <div class="divTogglePerm">
                                            <asp:CheckBox type="checkbox" ID="chkView" runat="server" />
                                            <asp:Label AssociatedControlID="chkView" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%-- CREATE --%>
                                <div class="col-sm-2 text-center">
                                    <div class="col-sm-12">
                                        <label>Create</label>
                                    </div>
                                    <div class="col-sm-12 toggle-centre">
                                        <div class="divTogglePerm">
                                            <asp:CheckBox type="checkbox" ID="chkCreate" runat="server" />
                                            <asp:Label AssociatedControlID="chkCreate" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%-- UPDATE --%>
                                <div class="col-sm-2 text-center">
                                    <div class="col-sm-12">
                                        <label>Update</label>
                                    </div>
                                    <div class="col-sm-12 toggle-centre">
                                        <div class="divTogglePerm">
                                            <asp:CheckBox type="checkbox" ID="chkUpdate" runat="server" />
                                            <asp:Label AssociatedControlID="chkUpdate"  runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%-- DELETE --%>
                                <div class="col-sm-2 text-center">
                                    <div class="col-sm-12">
                                        <label>Delete</label>
                                    </div>
                                    <div class="col-sm-12 toggle-centre">
                                        <div class="divTogglePerm">
                                            <asp:CheckBox type="checkbox" ID="chkDelete" runat="server" />
                                            <asp:Label AssociatedControlID="chkDelete" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%-- ALL --%>
                                <div class="col-sm-2 text-center">
                                    <div class="col-sm-12">
                                        <label>All</label>
                                    </div>
                                    <div class="col-sm-12 toggle-centre">
                                        <div class="divTogglePerm">
                                            <asp:CheckBox type="checkbox" ID="chkAll" runat="server" />
                                            <asp:Label AssociatedControlID="chkAll" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <%-- SET BUTTON --%>
                                <div class="col-sm-2 text-center">
                                    <div style="padding-top: 37px;">
                                        <asp:LinkButton ID="btnSave" CssClass="btn btn-success w-100" runat="server" OnClick="btnSave_Click"><i class="ion ion-ios7-checkmark"></i> Set</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <%--User/Group Dropdown --%>
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>

    </div>

    <script>
        $(document).ready(function () {
            $.ajax({

                url: 'http://localhost:56078/api/permissions/GetModules',
                method: 'Get',
                dataType: 'JSON',
                success: function (data) {
                    console.log(JSON.parse(data))
                }
            });


            $('#chk01').click(function () {
                if ($(this).is(":checked")) {
                    $('.user-dd').addClass('hidden');
                }
                else {
                    $('.user-dd').removeClass('hidden');
                }
            });


            $('body').on('click', 'div.divTogglePerm input[type="checkbox"]', function () {
                if ($(this).is(":checked")) {
                    $(this).parent().next().addClass('clicked');
                }
                else {
                    $(this).parent().next().removeClass('clicked');
                }
            });


            setInterval(x, 500);

            function x() {
                $('div.divTogglePerm input[type="checkbox"]').each(function () {
                    if ($(this).is(':checked')) {
                        $(this).parent().next().addClass('clicked');
                    }
                    else {
                        $(this).parent().next().removeClass('clicked');
                    }
                });
            }

        });
    </script>

</asp:Content>
