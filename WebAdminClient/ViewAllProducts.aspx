<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllProducts.aspx.cs" Inherits="WebAdminClient.ViewAllProducts" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.ViewAllProducts %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th><%: WebAdminClient.StringsLocal.Productname %></th><th><%: WebAdminClient.StringsLocal.Price %></th><th><%: WebAdminClient.StringsLocal.Comment %></th></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' /></td>
                    <td><asp:Label ID="labelProduct" runat="server" Text='<%# Eval("product") %>' /></td>
                    <td><asp:Label ID="labelPrice" runat="server" Text='<%# Eval("price") %>' /></td>
                    <td><asp:Label ID="labelComment" runat="server" Text='<%# Eval("comment") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
</asp:Content>
