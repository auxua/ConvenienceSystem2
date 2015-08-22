<%@ Page Title="Edit Products" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAllProducts.aspx.cs" Inherits="WebAdminClient.EditAllProducts" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <span><%: EntryCount %> Entries</span>
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th>Product</th><th>Price</th><th>Comment</th><th>Delete</th></tr>
            <asp:Repeater ID="repProd" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' />
                        <input type="hidden" runat="server" id="textID" value='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="textName" value='<%# Eval("product") %>' required />
                        <input type="hidden" runat="server" id="textNameOld" value='<%# Eval("product") %>' />

                    </td>
                    <td>
                        <input type="text" runat="server" id="textPrice" value='<%# Eval("price") %>' required />
                        <input type="hidden" runat="server" id="textPriceOld" value='<%# Eval("price") %>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="textComment" value='<%# Eval("comment") %>' />
                        <input type="hidden" runat="server" id="textCommentOld" value='<%# Eval("comment") %>' />
                    </td>
                    <td>
                        <label runat="server">
                            <input type="checkbox" runat="server" id="checkDelete" value='<%# Eval("id") %>' />
                            Delete
                        </label>
                    </td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    <input type="hidden" runat="server" id="entryCount" />
    <button class="btn btn-lg btn-primary btn-block" type="submit">Commit Changes</button>
</asp:Content>
