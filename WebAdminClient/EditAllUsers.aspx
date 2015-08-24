<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAllUsers.aspx.cs" Inherits="WebAdminClient.EditAllUsers" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.EditAllUsers %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <span><%: EntryCount %> <%: WebAdminClient.StringsLocal.Entries %></span>
    <table class="table table-striped table-bordered">
        <tr><th>#</th><th><%: WebAdminClient.StringsLocal.Username %></th><th><%: WebAdminClient.StringsLocal.Debt %></th><th>Status</th><th><%: WebAdminClient.StringsLocal.Comment %></th><th><%: WebAdminClient.StringsLocal.Delete %>?</th></tr>
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
                            <%: WebAdminClient.StringsLocal.Delete %>
                        </label>
                    </td>
                </tr>
                </ItemTemplate>
            </asp:Repeater>
    </table>
    <input type="hidden" runat="server" id="entryCount" />
    <button class="btn btn-lg btn-primary btn-block" type="submit"><%: WebAdminClient.StringsLocal.Confirm %></button>
</asp:Content>
