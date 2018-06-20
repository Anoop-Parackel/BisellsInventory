<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="BisellsERP.Application.Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Multi Select CSS --%>
    <link href="../Theme/assets/jquery-multi-select/multi-select.css" rel="stylesheet" type="text/css" />
    <style>
        .lr-center {
            height: 30vh;
            margin-top: 25px;
            line-height: 17em;
            text-align: center;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">

    <div class="">
        <%--hidden fields--%>
        <input type="hidden" id="hdUserId" value="0" />
        <%-- ---- Page Title, Button---- --%>
        <div class="row">
            <div class="col-md-4">
                <h3 class="pull-left page-title">Group Roles</h3>
            </div>
            <div class="col-md-8">
                <div class="btn-group pull-right">
                    <asp:Button ID="btnSave" runat="server" Style="display: none" ClientIDMode="Static" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" />
                    <input id="btnSaveHtml" type="button" value="Save"data-toggle="tooltip" data-placement="bottom" title="Save the current roles"  class="btn btn-success" />
                </div>
            </div>
        </div>

        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-horizontal">
                                    <label class="control-label">Select Group </label>
                                    <div>
                                        <asp:DropDownList ClientIDMode="Static" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm" ID="ddlGroup" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-8"></div>
                        </div>

                       <hr />

                        <div class="row">
                            <div class="col-md-5">
                                <div class="form-group-sm">
                                    <label class="control-label">Select User:</label>
                                    <div>
                                        <asp:ListBox ID="lbUnAsUser" runat="server" SelectionMode="Multiple" ClientIDMode="Static" style="height: 30vh" CssClass="w-100 form-control"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2 lr-center">
                                <asp:Button ID="btnLeft" runat="server" Text="<" CssClass="btn btn-primary btn-rounded" ClientIDMode="Static" OnClick="btnLeft_Click" />
                                <asp:Button ID="btnRight" runat="server" Text=">" CssClass="btn btn-primary btn-rounded" ClientIDMode="Static" OnClick="btnRight_Click" />
                            </div>
                            <div class="col-md-5">
                                <div class="form-group-sm">
                                    <label class="form-label semibold">User in Group:</label>
                                    <div>
                                        <asp:ListBox ID="lbAsUser" runat="server" SelectionMode="Multiple" ClientIDMode="Static" style="height: 30vh" CssClass="w-100 form-control"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>


    <script>
        //All funcrtions inside document ready
        $(document).ready(function () {
        
            //save functionality. 
            //This is not a asynchronous ajax call. 
            //Handled directly by code behind
            $('#btnSaveHtml').click(function () {
                swal({
                    title: "Save?",
                    text: "Are you sure you want to save?",
                    
                    showConfirmButton: true,closeOnConfirm:true,
                    showCancelButton: true,
                    
                    cancelButtonText: "Back to Entry",
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Save"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            $('#btnSave').trigger('click');
                        }
                    });
            });//Save function ends here
        });
    </script>
    <link href="../Theme/assets/sweet-alert/sweet-alert.min.css" rel="stylesheet" />
    <script src="../Theme/assets/sweet-alert/sweet-alert.min.js"></script>
    <script src="../Theme/Custom/Commons.js"></script>
</asp:Content>
