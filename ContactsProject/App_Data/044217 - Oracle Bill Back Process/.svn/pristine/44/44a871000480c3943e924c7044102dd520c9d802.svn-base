<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClaim.aspx.cs" Inherits="Billback.Webforms.AddClaim" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add Claim</title>
    <base target="_self" />
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
            <asp:Label ID="lblHeader" runat="server" Text="Add Claim" Width="200px" Height="10px" ForeColor="#2464AA" Font-Size="18px"></asp:Label></h2>
    </div>
    <br />
    <div style="font-size: 12px; font-weight: bold; color: #ff3333; text-align: left">
        Required Fields to Add Claim</div>
    <br />
    <div class="verticalGap">
        <igmisc:WebGroupBox ID="WebGrp1AddClaim" runat="server" CssClass="RequiredFields">
            <Template>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblClaimNumber" runat="server" Text="Claim Number:" Width="200px"
                                Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtClaimNumber" runat="server" Width="200px" 
                                TextMode="Number"  placeholder="example: 792476">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblClientID" runat="server" Text="Client ID:" Width="200px" Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtClientID" runat="server" Width="200px" TextMode="Number" placeholder="example: 6296">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblProcessingUnit" runat="server" Text="Processing Unit:" Width="200px"
                                Height="10px"></asp:Label>
                        </td>
                        <td>
                            <ig:WebTextEditor ID="txtProcessingUnit" runat="server" Width="200px" placeholder="example: 846">
                            </ig:WebTextEditor>
                        </td>
                    </tr>
                </table>
            </Template>
        </igmisc:WebGroupBox>
        <div>
            <br />
            <br />
        </div>
        <div style="font-size: 12px; font-weight: bold; color: #ff3333; text-align: left">
            <asp:Label ID="lblStatus" runat="server" Text="Fields in Red are Required" 
                Width="420px" Height="40px"></asp:Label>
        </div>
        <div>
            <br />
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
