<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectAccounting.aspx.cs" Inherits="WebAdminClient.DirectAccounting" Async="true" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.Accounting %></h2>
    
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading"><%: WebAdminClient.StringsLocal.AddAccounting %></h2>
        <div class="form-group">
            <span style="color:darkred"><%: StateMessage %></span><br /><br />
            <label for="UserSelect" class="sr-only">User:</label>
            <span>User:</span><br />
            <select runat="server" id="UserSelect" name="UserSelect" class="form-control">

            </select><br />
            <label for="ReasonSelectServer" class="sr-only"><%: WebAdminClient.StringsLocal.Reason %>:</label>
            <span><%: WebAdminClient.StringsLocal.Reason %>:</span><br />
            <select runat="server" id="ReasonSelectServer" name="ReasonSelectServer" class="form-control">

            </select><br />
            <input type="text" id="inputCustomReason" name="inputCustomReason" class="form-control" runat="server" /><br />
            <input type="text" id="inputCustomPrice" name="inputCustomPrice" class="form-control" placeholder="0.20" runat="server" /><br />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.Confirm %></button>
    <%--/Form--%>
    </div>
</asp:Content>
