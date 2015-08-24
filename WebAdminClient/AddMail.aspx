﻿<%@ Page Title="Add Mail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddMail.aspx.cs" Inherits="WebAdminClient.AddMail" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading">Add new Mail</h2>
        <div class="form-group">
            <label for="inputUser" class="sr-only">Username</label>
            <input type="text" id="inputUser" class="form-control" placeholder="Username" runat="server" required autofocus />
        </div>
        <div class="form-group">    
            <label for="inputAdress" class="sr-only">adress (email)</label>
            <input type="email" id="inputAdress" class="form-control" placeholder="test@me.com" runat="server" required />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit">Create Mail</button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>
    </div>
</asp:Content>
