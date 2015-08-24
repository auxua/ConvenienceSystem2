<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="WebAdminClient.AddProduct" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.AddProduct %></h2>
    
    <div class="container" style="min-width:100px; max-width:300px; ">
    <%--form role="form" runat="server"--%>
        <h2 class="form-heading"><%: WebAdminClient.StringsLocal.AddProduct %></h2>
        <div class="form-group">
            <label for="inputProduct" class="sr-only"><%: WebAdminClient.StringsLocal.Productname %></label>
            <input type="text" id="inputProduct" class="form-control" placeholder="Product name" runat="server" required autofocus />
        </div>
        <div class="form-group">    
            <label for="inputPrice" class="sr-only"><%: WebAdminClient.StringsLocal.Price %></label>
            <input type="text" id="inputPrice" class="form-control" placeholder="Price (e.g. 1.10)" runat="server" required />
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
