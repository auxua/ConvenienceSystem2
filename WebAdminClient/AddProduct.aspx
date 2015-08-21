<%@ Page Title="Add Product" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="WebAdminClient.AddProduct" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading">Add a new Product</h2>
        <div class="form-group">
            <label for="inputProduct" class="sr-only">Product name</label>
            <input type="text" id="inputProduct" class="form-control" placeholder="Product name" runat="server" required autofocus />
        </div>
        <div class="form-group">    
            <label for="inputPrice" class="sr-only">Price (e.g. 1.10)</label>
            <input type="text" id="inputPrice" class="form-control" placeholder="Price (e.g. 1.10)" runat="server" required />
        </div>
        <div class="form-group">
            <label for="inputComment" class="sr-only">Comment</label>
            <input type="text" id="inputComment" class="form-control" placeholder="Comment" runat="server" />
        </div>
        <button class="btn btn-lg btn-primary btn-block" type="submit">Create Product</button>
    <%--/Form--%>
        <span style="color:darkred"><%: StateMessage %></span>
    </div>
</asp:Content>
