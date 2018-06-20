<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Templates.aspx.cs" Inherits="BisellsERP.Settings.Invoice.Templates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.css" rel="stylesheet"/>
<script src="http://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="childContent" runat="server">
    <div class="col-sm-7">
        <div style="max-height:500px" id="summernote"></div>
    </div>
    <div class="col-sm-5">
            <div class="row">
        <asp:Literal Text="" ID="ltr" runat="server" />
    </div>
    </div>


    <script>

        $(document).ready(function () {
            $('#summernote').summernote({
                placeholder: 'Hello stand alone ui',
                height: 600,
                popover: {
                    air: [
                      ['color', ['color']],
                      ['font', ['bold', 'underline', 'clear']]
                    ]
                }
            });
        });
    </script>
</asp:Content>
