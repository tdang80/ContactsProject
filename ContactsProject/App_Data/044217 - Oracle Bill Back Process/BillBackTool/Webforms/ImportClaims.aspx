<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportClaims.aspx.cs" Inherits="Billback.Webforms.ImportClaims" %>

<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Import Claims</title>
    <base target="_self" />
    <link href="../CSS/BillBackPopup.css" rel="stylesheet" />
    <script type="text/javascript">
        function closeChildPopup() {
            window.close();
            return false;
        }

        function refreshParent() {
          //  window.opener.location.reload(true);
//            window.parent.refreshParent();
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
            <asp:Label ID="lblHeader" runat="server" Text="Import Claim" Width="200px" Height="10px"
                ForeColor="#2464AA" Font-Size="18px"></asp:Label></h2>
    </div>
    <br />
    <div style="font-size: 12px; font-weight: bold; color: #ff3333; text-align: left">
        Required Fields to Add Claim</div>
    <br />
    <div class="verticalGap">
        <igmisc:WebGroupBox ID="WebGrp1ImportClaim" runat="server" CssClass="RequiredFields">
            <Template>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblFileUpload" runat="server" Text="Choose File To Upload:" Width="200px"
                                Height="10px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUploader" runat="server" Width="400px" />
                        </td>
                    </tr>
                    <br></br>
                    <tr>
                        <td>
                            <asp:Button ID="btnFileUpload" runat="server" CssClass="button" OnClick="btnFileUpload_Click" Enabled="true"
                                Text="Import Excel File" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSelectTab" runat="server" Height="10px" Text="Select Tab to Import:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <ig:WebDropDown ID="ddlExcelSheets" runat="server" Width="200px">
                            </ig:WebDropDown>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnImportSheet" runat="server" CssClass="button" OnClick="btnImportSheet_Click"  Enabled="false"
                                Text="Import Sheet" />
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
            <asp:Label ID="lblStatus" runat="server" Text="Select Excel File to Import" Width="420px"
                Height="40px"></asp:Label>
        </div>
        <div>
            <br />
            <br />
        </div>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width: 25px">
                    &nbsp
                </td>
                <td>
                    <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" OnClientClick="closeChildPopup()"
                        OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
