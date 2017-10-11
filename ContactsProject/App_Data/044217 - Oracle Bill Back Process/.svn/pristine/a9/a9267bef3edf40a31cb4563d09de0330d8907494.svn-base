<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewServiceType.aspx.cs"
    Inherits="Billback.Webforms.NewServiceType" %>

<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics4.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Service Type</title>
    <link href="../CSS/BillBackPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        function closeChildPopup() {
            window.close();
            return false;
        }

        function closeAndRefresh() {
            opener.location.reload(); // or opener.location.href = opener.location.href;
            window.close(); // or self.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" class="verticalGap">
    <ig:WebScriptManager ID="WebScriptManager1" runat="server">
    </ig:WebScriptManager>
    <div>
        <h2>
            <asp:Label ID="lblHeader" runat="server" Text="Add New Service Type" Width="200px"
                Height="10px" ForeColor="#2464AA" Font-Size="18px"></asp:Label></h2>
    </div>
    <br />
    <div style="font-size: 12px; font-weight: bold; color: #ff3333; text-align: left">
        Required Fields to Add New Service Type</div>
    <br />
    <div class="verticalGap">
        <igmisc:WebGroupBox ID="WebGrp1AddServiceType" runat="server" CssClass="RequiredFields">
            <Template>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblServiceType" runat="server" Text="Service Type:" Width="125px"
                                Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtServiceType" runat="server" Width="275px" placeholder="example: TCM">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" Width="125px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtDescription" runat="server" Width="275px" placeholder="example: TELEPHONIC CASE MANAGEMENT">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDisplay" runat="server" Text="Display:" Width="125px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtDisplay" runat="server" Width="275px" ToolTip="This is what is Displayed in Service Type Drop Down List"
                                placeholder="example: TCM (Telephonic Case Management)">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDisplayOrderID" runat="server" Text="Display Order ID:" Width="125px"
                                Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtDisplayOrdID" runat="server" Width="275px" ToolTip="Order in Service Type Drop Down List"
                                ReadOnly="true" Enabled="false">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTaxID" runat="server" Text="Tax ID:" Width="125px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtTaxID" runat="server" Width="275px" ToolTip="Tax ID for Service Type"
                                placeholder="example: 362685608" TextMode="Number">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTaxSubID" runat="server" Text="Tax Sub ID:" Width="125px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtTaxSubID" runat="server" Width="275px" ToolTip="Tax Sub ID for Service Type"
                                placeholder="example: 404" TextMode="Number">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPayCodeSuffix" runat="server" Text="PayCode Suffix:" Width="125px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtPayCodeSuffix" runat="server" Width="275px" ToolTip="Pay Code Suffix for Service Type (Example: *97)"
                                placeholder="example: *45">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <igmisc:WebGroupBox ID="GrpButtonEnable" runat="server" BorderWidth="3px" BackColor="Transparent"
                    Font-Bold="false" Font-Size="11px">
                    <Template>
                        <table>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chSearchEnable" Text="Enable Search Button" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chSearchEnable_CheckedChanged" />
                                </td>
                                <td style="width: 50px">
                                    &nbsp
                                </td>
                                <td>
                                    <asp:CheckBox ID="chImportEnable" Text="Enable Import Button:" runat="server" AutoPostBack="True"
                                        OnCheckedChanged="chImportEnable_CheckedChanged" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chAddEnable" Text="Enable Add Button:" runat="server" Checked="True" />
                                </td>
                                <td style="width: 50px">
                                    &nbsp
                                </td>
                                <td>
                                    <asp:CheckBox ID="chRemoveEnable" Text="Enable Remove Button:" runat="server" Checked="True" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chProcessEnable" Text="Enable Process Button:" runat="server" Checked="True"
                                        Enabled="False" />
                                </td>
                                <td>
                                    &nbsp
                                </td>
                                <td>
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebGroupBox>
                <br />
                <br />
                <igmisc:WebGroupBox ID="GrpPayCodeLogic" runat="server" BorderWidth="3px" BackColor="Transparent"
                    Font-Bold="false" Font-Size="11px">
                    <Template>
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButtonList ID="rdPayCodeLogic" runat="server">
                                        <asp:ListItem Value="UseSvcInteract">Use Service Interaction Logic</asp:ListItem>
                                        <asp:ListItem Value="UseCAFlipLogic">Use CA Pay Code Flip Logic</asp:ListItem>
                                        <asp:ListItem Value="UsePayCodeSupplied">Use Pay Code Supplied</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </Template>
                </igmisc:WebGroupBox>
            </Template>
        </igmisc:WebGroupBox>
        <div>
            <br />
            <br />
        </div>
        <div style="font-size: 12px; font-weight: bold; color: #ff3333; text-align: left">
            <asp:Label ID="lblStatusMsg" runat="server" Text="Fields in Red are Required" Width="420px"
                Height="40px"></asp:Label>
        </div>
        <div>
            <br />
          
        </div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btSave" CssClass="button" runat="server" Text="Save & Close" OnClick="btSave_Click" />
                </td>
                <td style="width: 25px">
                    &nbsp
                </td>
                <td>
                    <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" OnClientClick="closeChildPopup()" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
