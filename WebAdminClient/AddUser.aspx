<%@ Page Title="Add User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WebAdminClient.AddUser" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading">Add a new User</h2>
        <div class="form-group">
            <label for="inputUser" class="sr-only">Username</label>
            <input type="text" id="inputUser" class="form-control" placeholder="Username" runat="server" required autofocus />
        </div>
        <div class="form-group">    
            <label for="inputState" class="sr-only">State (active, inactive...)</label>
            <input type="text" id="inputState" class="form-control" placeholder="active" runat="server" required />
        </div>
        <div class="form-group">
            <label for="inputComment" class="sr-only">Comment</label>
            <input type="text" id="inputComment" class="form-control" placeholder="Comment" runat="server" />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit">Create User</button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>
    </div>
</asp:Content>
