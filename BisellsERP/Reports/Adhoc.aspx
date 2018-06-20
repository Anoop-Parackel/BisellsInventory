 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Adhoc.aspx.cs" Inherits="BisellsERP.Reports.Adhoc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Adhoc Reports</title>
    <script src="../Theme/Custom/Commons.js"></script>

    <style>
        div.dataTables_wrapper div.dataTables_length select {
    width: 75px!important;

}
        
    </style>


</asp:Content>

<asp:Content ID="Content2"  ContentPlaceHolderID="childContent" runat="server">
        <script>
        loading('start');
    </script>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
   
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <script>    
        $(document).ready(function(){
            loading('stop');

        });
    </script>


</asp:Content>
