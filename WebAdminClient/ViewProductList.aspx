<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewProductList.aspx.cs" Inherits="WebAdminClient.ViewProductList" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.PriceList %></h2>
    <table class="table table-striped table-bordered">
        <tr><th><%: WebAdminClient.StringsLocal.Productname %></th><th><%: WebAdminClient.StringsLocal.Price %></th></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelProduct" runat="server" Text='<%# Eval("product") %>' /></td>
                    <td><asp:Label ID="labelPrice" runat="server" Text='<%# Eval("price") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
</asp:Content>
