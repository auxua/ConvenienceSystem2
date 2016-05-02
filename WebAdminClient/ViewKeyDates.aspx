<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewKeyDates.aspx.cs" Inherits="WebAdminClient.ViewKeyDates" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.ViewKeydates %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <table class="table table-striped table-bordered">
        <tr><th><%: WebAdminClient.StringsLocal.Keydate %></th><th><%: WebAdminClient.StringsLocal.Comment %></th></tr>
            <asp:Repeater ID="repDates" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelDate" runat="server" Text='<%# Eval("keydate") %>' /></td>
                    <td><asp:Label ID="labelComment" runat="server" Text='<%# Eval("comment") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
</asp:Content>
