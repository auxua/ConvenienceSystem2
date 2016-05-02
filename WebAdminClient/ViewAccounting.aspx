<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAccounting.aspx.cs" Inherits="WebAdminClient.ViewAccounting" Async="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.Accounting %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <div id="AggregatedAccountingTable" runat="server">
        <h2><%: WebAdminClient.StringsLocal.SinceLastkeydate %></h2>
    <table class="table table-striped table-bordered">
        <tr><!--<th>#</th>--><th><%: WebAdminClient.StringsLocal.Username %></th><th><%: WebAdminClient.StringsLocal.Debt %></th></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <!--<td><asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' /></td>-->
                    <td><asp:Label ID="labelUser" runat="server" Text='<%# Eval("username") %>' /></td>
                    <td><asp:Label ID="labelDebt" runat="server" Text='<%# Eval("debt") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    </div>

    <div id="AccountingTable" runat="server">
        <h2><%: WebAdminClient.StringsLocal.RecentActivities %></h2>
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th><%: WebAdminClient.StringsLocal.Date %></th><th><%: WebAdminClient.StringsLocal.Username %></th><th><%: WebAdminClient.StringsLocal.Price %></th><th><%: WebAdminClient.StringsLocal.Comment %></th><th>Device</th></tr>
            <asp:Repeater ID="repAccounting" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' /></td>
                    <td><asp:Label ID="labelDate" runat="server" Text='<%# Eval("date") %>' /></td>
                    <td><asp:Label ID="labelUser" runat="server" Text='<%# Eval("user") %>' /></td>
                    <td><asp:Label ID="labelPrice" runat="server" Text='<%# Eval("price") %>' /></td>
                    <td><asp:Label ID="labelComment" runat="server" Text='<%# Eval("comment") %>' /></td>
                    <td><asp:Label ID="labelDevice" runat="server" Text='<%# Eval("device") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    </div>
</asp:Content>
