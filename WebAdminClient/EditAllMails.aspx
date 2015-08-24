<%@ Page Title="Edit Mails" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAllMails.aspx.cs" Inherits="WebAdminClient.EditAllMails" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <span><%: EntryCount %> Entries</span>
    <table class="table table-striped table-bordered">
        <tr><th>For User</th><th>Adress</th><th>Delete?</th></tr>
            <asp:Repeater ID="repMails" runat="server">
                <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="labelName" runat="server" Text='<%# Eval("username") %>' />
                        <input type="hidden" runat="server" id="textName" value='<%# Eval("username") %>' />

                    </td>
                    <td>
                        <input type="text" style="width:100%" runat="server" id="textAdress" value='<%# Eval("adress") %>' required />
                        <input type="hidden" runat="server" id="textAdressOld" value='<%# Eval("adress") %>' />
                    </td>
                    <td>
                        <label runat="server">
                            <input type="checkbox" runat="server" id="checkDelete" value='<%# Eval("username") %>' />
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
