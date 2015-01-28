<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPaySlip.aspx.cs" Inherits="PaySlipGeneration.frmPaySlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <table align="center" border="1">
                <tr>
                    <td colspan="2" height="60" width="1300" align="center" bgcolor="silver">

                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Menlologo5.png" />

                    </td>
                </tr>
                <tr>

                    <%-- <td align="center" >
                     <%--   <asp:RadioButtonList ID="rbtnlst" runat="server" RepeatDirection="Horizontal" TextAlign="Left" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="rbtnlst_SelectedIndexChanged" Font-Bold="True">
                            <asp:ListItem Selected="True" Value="E">Employee Sheet</asp:ListItem>
                            <asp:ListItem Value="T">Trainee Sheet</asp:ListItem>
                        </asp:RadioButtonList>          
                       
                    </td> --%>
                    <td align="center" colspan="4">
                        <asp:RadioButtonList ID="rbtnlst" runat="server" RepeatDirection="Horizontal" TextAlign="Left" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="rbtnlst_SelectedIndexChanged" Font-Bold="True">
                            <asp:ListItem Selected="True" Value="E">Employee Sheet</asp:ListItem>
                            <asp:ListItem Value="T">Trainee Sheet</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:Label ID="lblMsg" runat="server" Visible="False" Font-Bold="True"></asp:Label>
                        <asp:FileUpload ID="FileUploadToServer" runat="server" Font-Bold="True" />
                        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" Font-Bold="True" />
                    </td>
                </tr>
                <tr style="visibility:hidden">
                    <td align="right" colspan="">
                        <asp:Label ID="lblSender" runat="server" Visible="true" Font-Bold="True" Text="Sender Email Id:"></asp:Label>
                        <asp:TextBox ID="txtSenderMail" runat="server" Font-Bold="True" Width="200px" MaxLength="100"></asp:TextBox>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPassword" runat="server" Visible="true" Font-Bold="True" Text="Password:"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" Font-Bold="True" Width="200px" TextMode="Password" MaxLength="25"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grvBatchUpload" runat="server" CellPadding="3" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" AllowPaging="True" OnPageIndexChanging="OnPaging" AutoGenerateColumns="False" OnRowCommand="grvBatchUpload_RowCommand" PageSize="17" OnRowDataBound="grvBatchUpload_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="S# No#" HeaderText="S.No." />
                                <asp:BoundField DataField="Emp'e ID" HeaderText="Emp'e ID" />
                                <asp:BoundField DataField="Employee Name" HeaderText="Employee Name" />
                                <asp:BoundField DataField="Pay Slip for the month of" DataFormatString="{0:y}" HeaderText="Pay Slip for the month of" />
                                <asp:BoundField DataField="Basic / Stipend" HeaderText="Basic / Stipend" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="HRA" HeaderText="HRA" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Conveyance Allowance" HeaderText="Conveyance Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Medical Allowance" HeaderText="Medical Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Leave Travel Allowance" HeaderText="Leave Travel Allowance " HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Books Allowance" HeaderText="Books Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Dress Allowance" HeaderText="Dress Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Helper Allowance" HeaderText="Helper Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Special Taxable Allowance" HeaderText="Special Taxable Allowance" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Bonus" HeaderText="Bonus " HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Total Gross Amt" HeaderText="Total Gross Amt" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="TDS" HeaderText="TDS" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="ESI" HeaderText="ESI" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="PF" HeaderText="PF" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="PT" HeaderText="PT" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Loan payments" HeaderText="Loan payments" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Other deductions" HeaderText="Other deductions" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Insurance deductions" HeaderText="Insurance deductions" HtmlEncode="False" DataFormatString="{0:N0}" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Total Dedns#" HeaderText="Total Dedns." HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Net Pay" HeaderText="Net Pay" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Work days" HeaderText="Work days" />
                                <asp:BoundField DataField="Paid days" HeaderText="Paid days" />
                                <asp:BoundField DataField="Leave Bal C/F" HeaderText="Leave Bal C/F" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Leave Credited" HeaderText="Leave Cred.(days)" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Leave Taken" HeaderText="Leave Taken(days)" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                                <asp:BoundField DataField="Leave Bal EOM" HeaderText="Leave Bal EOM(days)" />

                                <asp:BoundField DataField="MailId" HeaderText="MailId" HtmlEncode="False" DataFormatString="" Visible="false" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSendMail" runat="server" CausesValidation="false" CommandName="SendMail" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Send Mail" Visible="True" Font-Bold="True" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGenPdf" runat="server" CausesValidation="false" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="Download" Visible="True" Font-Bold="True" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <%-- <asp:GridView ID="grvTrainee" runat="server" CellPadding="3" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" AllowPaging="True" OnPageIndexChanging="OnPagingTrainee" AutoGenerateColumns="False" OnRowCommand="grvTrainee_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Select" Visible="false">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Pay slip for the month of:" DataFormatString="{0:y}" HeaderText="Pay slip for the month of:" />
                                <asp:BoundField DataField="Name of trainee employee: " HeaderText="Name of trainee employee" />
                                 <asp:BoundField DataField="Trainee Employee ID: " HeaderText="Trainee Employee ID: " />
                                <asp:BoundField DataField="Vacation balance days:" HeaderText="Vacation balance days" />
                                <asp:BoundField DataField="Work days:" HeaderText="Work days" />
                                <asp:BoundField DataField="Paid days:" HeaderText="Paid days" />
                                <asp:BoundField DataField="Stipend" HeaderText="Stipend" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Bonus" HeaderText="Bonus" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="ESI" HeaderText="ESI" HtmlEncode="False" DataFormatString="{0:N0}" />
                                <asp:BoundField DataField="Provident Fund (PF)" HeaderText="Provident Fund (PF)" HtmlEncode="False" DataFormatString="{0:N0}" />
                                 <asp:BoundField DataField="MailId" HeaderText="MailId" HtmlEncode="False" DataFormatString="" Visible="false" />
                                 <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btnSendMail" runat="server" CausesValidation="false" CommandName="SendMail"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Text="Send Mail"  Visible="True"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGenPdf" runat="server" CausesValidation="false" CommandName="Select"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Text="Download"  Visible="True"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                               <%-- <asp:ButtonField ButtonType="Button"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  HeaderText="Generate PaySlip" ShowHeader="True" Text="Get Pdf"  />
                               
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                        <br />--%>
                        <asp:Button ID="btnClear" Text="Clear Gird View Data" OnClick="ClearGridView" runat="server" Font-Bold="True" />
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
