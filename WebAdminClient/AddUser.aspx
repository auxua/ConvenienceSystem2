<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WebAdminClient.AddUser" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.AddUser %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading"><%: WebAdminClient.StringsLocal.AddUser %></h2>
        <div class="form-group">
            <label for="inputUser" class="sr-only"><%: WebAdminClient.StringsLocal.Username %></label>
            <input type="text" id="inputUser" class="form-control" placeholder="Username" runat="server" required autofocus />
        </div>
        <div class="form-group">    
            <!--<label for="inputState" class="sr-only"></label>
            <input type="text" id="inputState" class="form-control" placeholder="active" runat="server" required />-->
            <label runat="server">
                <input type="checkbox" runat="server" id="checkState" />
                <%: WebAdminClient.StringsLocal.Active %>
            </label>
        </div>
        <div class="form-group">
            <label for="inputComment" class="sr-only"><%: WebAdminClient.StringsLocal.Comment %></label>
            <input type="text" id="inputComment" class="form-control" placeholder="Comment" runat="server" />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.Confirm %></button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>
    </div>
</asp:Content>
