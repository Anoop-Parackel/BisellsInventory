<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Details.aspx.cs" Inherits="BisellsERP.Details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Theme/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../Theme/js/jquery.min.js"></script>
    <script src="../Theme/js/bootstrap.min.js"></script>
    <link href="../Theme/assets/timepicker/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="../Theme/assets/timepicker/bootstrap-datepicker.js"></script>
    <link href="../Theme/assets/datatables/datatables.min.css" rel="stylesheet" />
    <script src="../Theme/assets/datatables/datatables.min.js"></script>
    <link href="../Theme/css/material-design-iconic-font.min.css" rel="stylesheet" />
    <link href="../Theme/assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <script src="../Theme/Custom/Commons.js"></script>
    <link href="../Theme/css/style.css" rel="stylesheet" />
    
    <style>
        div.dataTables_wrapper div.dataTables_length select {
    width: 75px!important;

}
        
    </style>
</head>

<body>
        <div class="loading-overlay">
		<div class="loading-spinner">
		  <div></div>
		  <div></div>
		  <div></div>
		  <div></div>
		  <div></div>
		</div>
        <p></p>
	</div>
    <form id="form1" runat="server">
            <%-- Loading Spinner --%>

        <script>
            loading('start');
        </script>
        <div>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
    </form>
</body>
<script>
    $(document).ready(function () {
        loading('stop');
    });
</script>
</html>
