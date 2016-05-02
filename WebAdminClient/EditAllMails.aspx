<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAllMails.aspx.cs" Inherits="WebAdminClient.EditAllMails" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: WebAdminClient.StringsLocal.EditMails %></h2>
    <span style="color:darkred"><%: StateMessage %></span><br />
    <span><%: EntryCount %> <%: WebAdminClient.StringsLocal.Entries %></span>
    <table class="table table-striped table-bordered">
        <tr><th><%: WebAdminClient.StringsLocal.ForUser %></th><th><%: WebAdminClient.StringsLocal.Adress %></th><th><%: WebAdminClient.StringsLocal.Delete %>?</th></tr>
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
