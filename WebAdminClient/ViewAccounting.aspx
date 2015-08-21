<%@ Page Title="Accounting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAccounting.aspx.cs" Inherits="WebAdminClient.ViewAccounting" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div id="AggregatedAccountingTable" runat="server">
        <h2>Debt Since Keydate</h2>
    <table class="table table-striped table-bordered">
        <tr><!--<th>#</th>--><th>User</th><th>Debt</th></tr>
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
        <h2>Recent Activities</h2>
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th>Date</th><th>User</th><th>Price</th><th>Comment</th><th>Device</th></tr>
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
