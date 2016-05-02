<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebAdminClient.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.About %></h2>
    <h3><%: WebAdminClient.StringsLocal.Convenience %> - WebClient</h3>
    <p><a href="https://github.com/auxua/ConvenienceSystem2" target="_blank">https://github.com/auxua/ConvenienceSystem2</a></p>
</asp:Content>
