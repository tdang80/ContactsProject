<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientSearch.aspx.cs" Inherits="Billback.Webforms.ClientSearch" %>

<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Search Client ID</title>
    <link href="../CSS/BillBackPopup.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.2.1.js" type="text/javascript"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <link href="../Scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtName').autocomplete({
                source: 'ClientHandler.ashx'
            });
        });

        function filterClientID() {
            $('#txtClientID').autocomplete({
                source: 'ClientHandler.ashx'
            });
        }

        function closeAndRefresh() {
            opener.location.reload(); // or opener.location.href = opener.location.href;
            window.close(); // or self.close();
        }

        function btnClientNameClick() {
            document.getElementById("btnSearchClientName2").click();

        }

       
    </script>
</head>
<body class="verticalGap">
    <form id="form1" runat="server">
    <ig:WebScriptManager ID="WebScriptManager1" runat="server">
    </ig:WebScriptManager>
    <div >
        <h2>
            <asp:Label ID="lblHeader" runat="server" Text="Search Client ID" Width="200px" Height="10px"
                ForeColor="#2464AA" Font-Size="18px"></asp:Label></h2>
    </div>
    <div >
        <igmisc:WebGroupBox ID="WebGrpSearchClientID" runat="server" CssClass="OptionalFields">
            <Template>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblClientName" class="w3-label w3-text-black " runat="server" Text="Client Name:"
                               style="font-size:14px" ></asp:Label>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtClientName" CssClass="form-control"  placeholder="Search Client Name"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSearchClientName"
                                        OnClick="btnSearchClientName_Click">
                                        <i class="glyphicon glyphicon-search" style="height:20px; width:20px"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblClientID" runat="server" Text="Client ID:" style="font-size:14px"></asp:Label>
                        </td>
                        <td>
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtClientID" CssClass="form-control" placeholder="Search Client ID"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton class="btn btn-default" runat="server" ID="btnSearchClientID" OnClick="btnSearchClientID_Click">
                                        <i class="glyphicon glyphicon-search" style="height:20px; width:20px"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </Template>
        </igmisc:WebGroupBox>
    </div>
    <div  style=" margin-left: 5px; margin-right: 5px">
        <asp:UpdatePanel runat="server" ID="updClientSearch" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="padding: 5px; width: 90%">
                    <asp:Label ID="lblStatusMsg" runat="server" ForeColor="Red" Width="90%" Height="20px"></asp:Label>
                </div>
                <div style="padding: 5px">
                    <asp:Label ID="lblSelectedClient" runat="server" ForeColor="Green" Width="90%" Height="20px"></asp:Label>
                </div>
                <div style="padding: 5px; overflow: auto; height: 300px; width: 100%">
                    <asp:GridView ID="grdClientSelect2" runat="server" OnSelectedIndexChanged="grdClientSelect2_SelectedIndexChanged"
                        CssClass="GridData" DataKeyFields="CONT_NUM" AutoGenerateColumns="false" HorizontalAlign="Center"
                        Width="100%">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select Client">
                                <ItemStyle Width="100px" ForeColor="Black"></ItemStyle>
                            </asp:CommandField>
                            <asp:BoundField DataField="CONT_NUM" HeaderText="Client ID">
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="NAME" HeaderText="Client Name">
                                <HeaderStyle Width="300px"></HeaderStyle>
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="CONT_STAT" HeaderText="Client Status">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <HeaderStyle Font-Bold="true" CssClass="GridHeader"/>
                        <AlternatingRowStyle BackColor="#E6F7FF" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
    <div style="padding:5px; margin-left: 25px; margin-right: 25px">
        <table >
            <tr align="center">
                <td >
                    <asp:Button ID="btnClose" CssClass="button" runat="server" Text="Use Selected Client"
                        OnClick="btnClose_Click" />
                </td>
                <td style="width:50px"></td>
                <td >
                    <asp:Button ID="btnCancel" CssClass="button" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
