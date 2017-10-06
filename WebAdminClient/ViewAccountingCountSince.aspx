<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewAccountingCountSince.aspx.cs" Inherits="WebAdminClient.ViewAccountingCountSince" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.Accounting %></h2>
    
    <div class="container" style="min-width:200px; max-width:500px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading"><%: WebAdminClient.StringsLocal.ProductsBoughtSince %></h2>
        <div class="form-group">
            <a href="ViewAccountingCountSince?mode=yesterday" class="btn btn-info" role="button"><%: WebAdminClient.StringsLocal.Yesterday %></a>&nbsp;
            <a href="ViewAccountingCountSince?mode=week" class="btn btn-info" role="button"><%: WebAdminClient.StringsLocal.LastWeek %></a>&nbsp;
            <a href="ViewAccountingCountSince?mode=month" class="btn btn-info" role="button"><%: WebAdminClient.StringsLocal.LastMonth %></a>&nbsp;
            <a href="ViewAccountingCountSince?mode=keydate" class="btn btn-info" role="button"><%: WebAdminClient.StringsLocal.LastKeyDate %></a>&nbsp;<br /><br />
            <label for="inputDate" class="sr-only"><%: WebAdminClient.StringsLocal.Since %></label>
            <input type="text" id="inputDate" style="width:100%" class="form-control" placeholder="yyyy-MM-dd" runat="server" required autofocus />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.Confirm %></button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>

        <div id="AccountingTable" runat="server">
        <h2><%: WebAdminClient.StringsLocal.RecentActivities %></h2>
        <table class="table table-striped table-bordered">
        <tr><th><%: WebAdminClient.StringsLocal.Productname %></th><th>#</th></tr>
            <asp:Repeater ID="repAccounting" runat="server">
                <ItemTemplate>
                <tr>
                    <td><asp:Label ID="labelID" runat="server" Text='<%# Eval("product") %>' /></td>
                    <td><asp:Label ID="labelDate" runat="server" Text='<%# Eval("amount") %>' /></td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    </div>

    </div>
</asp:Content>
