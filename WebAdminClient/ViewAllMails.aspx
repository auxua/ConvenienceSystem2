<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAllMails.aspx.cs" Inherits="WebAdminClient.ViewAllMails" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.ViewMails %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <table class="table table-striped table-bordered">
        <tr><th><%: WebAdminClient.StringsLocal.ForUser %></th><th><%: WebAdminClient.StringsLocal.Adress %></th><%--<th>Active?</th>--%></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelName" runat="server" Text='<%# Eval("username") %>' /></td>
                    <td><asp:Label ID="labelAdress" runat="server" Text='<%# Eval("adress") %>' /></td>
                    <%-- <td><asp:Label ID="labelActive" runat="server" Text='<%# Eval("active") %>' /></td> --%>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
</asp:Content>
