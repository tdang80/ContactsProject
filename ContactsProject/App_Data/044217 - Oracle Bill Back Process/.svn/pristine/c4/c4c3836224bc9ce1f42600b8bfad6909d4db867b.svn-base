<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChildGridPage.aspx.cs"
    Inherits="Billback.Webforms.ChildGridPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics4.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.Misc" TagPrefix="igmisc" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v11.1,Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Claim Edit / View Screen</title>
    <link href="../CSS/BillBack.css" rel="stylesheet" type="text/css" />
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.1.0.js"></script>--%>
    <script src="../Scripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <script>
        window.jQuery || document.write("<script src='jquery-1.12.4.js'><\/script>");
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <ig:WebScriptManager ID="WebScriptManager1" runat="server">
    </ig:WebScriptManager>
     <script type="text/javascript" language="javascript">
       
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
       
        function EndRequestHandler(sender, args) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(pageReloaded);
        }

        function pageReloaded() {
            var wdg = $find("<%= this.grdClaimSelect.ClientID %>");
            wdg.get_ajaxIndicator().hide();
        }
       
        }
    </script>
    
    <div style="margin: 10px 10px 10px 10px;">
        <div style="padding: 15px 10px 15px 10px">
            <asp:Button ID="btnSaveTop" runat="server" Text="Save and Close" OnClick="btnSaveClose_Click"
                CssClass="button" />
        </div>
        <div>
            <table>
          
               
                <tr>
                    <td >
                        <ig:WebDataGrid ID="grdClaimSelect" runat="server" Height="825px" Width="100%" AutoGenerateColumns="False"
                        ItemCssClass="borderClass" AltItemCssClass="grdAltRow" EnableDataViewState ="True" 
                            HeaderCaptionCssClass="HeaderCaptionClass" DataKeyFields="GUID" 
                            oninitializerow="grdClaimSelect_InitializeRow">
                             <EditorProviders>
                                    <ig:DatePickerProvider ID="DateInputProvider" >
                                        <EditorControl  runat="server" ClientIDMode="Predictable">
                                        </EditorControl>
                                     </ig:DatePickerProvider>
                                </EditorProviders>
                           <Columns>
                                    <ig:TemplateDataField Key="TemplateField_0" Width="10px">
                                        <ItemTemplate>
                                            <asp:Image ID="imgValidationError" runat="server"></asp:Image>
                                        </ItemTemplate>
                                    </ig:TemplateDataField>
                                   
                                    <ig:UnboundCheckBoxField HeaderChecked="False" Key="ClaimSelect" Width="10px">
                                    </ig:UnboundCheckBoxField>
                                    <ig:BoundDataField DataFieldName="CLIENTID" Key="CLIENTID" Width="35px">
                                        <Header Text="Client ID"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="CLAIM#" Key="CLAIM#">
                                        <Header Text="Claim #"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="CLMNTFNAME" Key="CLMNTFNAME">
                                        <Header Text="First Name"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="CLMNTLNAME" Key="CLMNTLNAME">
                                        <Header Text="Last Name"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DOL" Key="DOL" DataType="System.DateTime">
                                        <Header Text="DOL"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="BINVOICE" Key="BINVOICE">
                                        <Header Text="BInvoice"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DATEFROM" Key="DATEFROM"  DataType="System.DateTime">
                                        <Header Text="DateFrom"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DATETHRU" Key="DATETHRU"  DataType="System.DateTime">
                                        <Header Text="DateThru"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="PROCESSINGUNIT" Key="PROCESSINGUNIT" Width="35px">
                                        <Header Text="Proc Unit"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="PAYCODE" Key="PAYCODE">
                                        <Header Text="Pay Code"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="ALLOCATIONAMOUNT" Key="ALLOCATIONAMOUNT">
                                        <Header Text="Allocation Amount"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="CLAIM_UID" Key="CLAIM_UID" Hidden="true">
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="VENDOR_ID" Key="VENDOR_ID" Hidden="true">
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DATA_SET" Key="DATA_SET" Hidden="true">
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="CLAIM_TYPE" Key="CLAIM_TYPE" Hidden="true">
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="STATE_PAYROLL" Key="STATE_PAYROLL" Hidden="true">
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="STATE_A" Key="STATE_A" Hidden="true">
                                    </ig:BoundDataField>
                                      <ig:BoundDataField DataFieldName="SUB_TYPE" Key="SUB_TYPE" Hidden="true">
                                    </ig:BoundDataField>
                                     <ig:BoundDataField DataFieldName="LINE_CODE" Key="LINE_CODE" Hidden="true">
                                    </ig:BoundDataField>
                                     <ig:BoundDataField DataFieldName="GUID" Key="GUID" Hidden="true">
                                    </ig:BoundDataField>
                                </Columns>
                               
                                <Behaviors>
                                    <ig:ColumnFixing>
                                    </ig:ColumnFixing>
                                    <ig:Sorting>
                                    </ig:Sorting>
                                    <ig:Filtering>
                                    </ig:Filtering>
                                    <ig:EditingCore>
                                        <Behaviors>
                                            <ig:RowEditingTemplate>
                                            </ig:RowEditingTemplate>
                                            <ig:CellEditing Enabled="true">
                                                <ColumnSettings>
                                                <ig:EditingColumnSetting ColumnKey="DOL" EditorID="DateInputProvider"  />
                                                    <ig:EditingColumnSetting ColumnKey="DATEFROM" EditorID="DateInputProvider"  />
                                                    <ig:EditingColumnSetting ColumnKey="DATETHRU" EditorID="DateInputProvider" />
                                                     <ig:EditingColumnSetting ColumnKey="CLAIM#" ReadOnly="true" />
                                                    <ig:EditingColumnSetting ColumnKey="CLMNTFNAME" ReadOnly="true" />
                                                    <ig:EditingColumnSetting ColumnKey="CLMNTLNAME" ReadOnly="true" />
                                                    <ig:EditingColumnSetting ColumnKey="DOL" ReadOnly="true" />
                                                    <ig:EditingColumnSetting ColumnKey="CLIENTID" ReadOnly="true" />
                                                </ColumnSettings>
                                            </ig:CellEditing>
                                            <ig:RowDeleting Enabled="true" />
                                        </Behaviors>
                                    </ig:EditingCore>
                                    <ig:RowSelectors RowNumbering="True">
                                    </ig:RowSelectors>
                                    <ig:Selection RowSelectType="Single" CellClickAction="Row" CellSelectType="None"
                                        SelectedCellCssClass="SelectedCellClass">
                                    </ig:Selection>
                                    <ig:ColumnResizing>
                                    </ig:ColumnResizing>
                                    <ig:Activation ActiveCellCssClass="ActiveCellClass" ActiveRowCssClass="ActiveRowClass">
                                    </ig:Activation>
                                </Behaviors>
                        </ig:WebDataGrid>
                    </td>
                </tr>
               
            </table>
        </div>
    </div>
    </form>
</body>
</html>
