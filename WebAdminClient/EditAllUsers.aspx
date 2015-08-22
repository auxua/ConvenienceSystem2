<%@ Page Title="Edit Users" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAllUsers.aspx.cs" Inherits="WebAdminClient.EditAllUsers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <span><%: EntryCount %> Entries</span>
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th>Name</th><th>Debt</th><th>Status</th><th>Comment</th><th>Delete</th></tr>
            <asp:Repeater ID="repUsers" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="labelID" runat="server" Text='<%# Eval("id") %>' />
                        <input type="hidden" runat="server" id="textID" value='<%# Eval("id") %>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="textName" value='<%# Eval("username") %>' required />
                        <input type="hidden" runat="server" id="textNameOld" value='<%# Eval("username") %>' />

                    </td>
                    <td>
                        <input type="text" runat="server" id="textDebt" value='<%# Eval("debt") %>' required />
                        <input type="hidden" runat="server" id="textDebtOld" value='<%# Eval("debt") %>' />
                    </td>
                    <td>
                        <input type="text" runat="server" id="textState" value='<%# Eval("status") %>' required />
                        <input type="hidden" runat="server" id="textStateOld" value='<%# Eval("status") %>' />
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
