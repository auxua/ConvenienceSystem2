<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAdminClient._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <h2><%: WebAdminClient.StringsLocal.Welcome %></h2>
        <h3><%: WebAdminClient.StringsLocal.Welcome_Sub %></h3>
    </div>

</asp:Content>
