<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddKeyDate.aspx.cs" Inherits="WebAdminClient.AddKeyDate" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.AddKeyDate %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading"><%: WebAdminClient.StringsLocal.AddKeyDate %></h2>
        <div class="form-group">
            <label for="inputComment" class="sr-only"><%: WebAdminClient.StringsLocal.Comment %></label>
            <input type="text" id="inputComment" class="form-control" placeholder="Comment" runat="server" />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.Confirm %></button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>
    </div>
</asp:Content>
