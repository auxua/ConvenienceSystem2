<%@ Page Title="All Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllUsers.aspx.cs" Inherits="WebAdminClient.ViewAllUsers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th>Name</th><th>Status</th><th>Comment</th></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' /></td>
                    <td><asp:Label ID="labelName" runat="server" Text='<%# Eval("username") %>' /></td>
                    <td><asp:Label ID="labelState" runat="server" Text='<%# Eval("status") %>' /></td>
                    <td><asp:Label ID="labelComment" runat="server" Text='<%# Eval("comment") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
</asp:Content>
