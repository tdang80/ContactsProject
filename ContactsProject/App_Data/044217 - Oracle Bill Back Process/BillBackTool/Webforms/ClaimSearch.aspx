<%@ Page Title="" Language="C#" MasterPageFile="~/LayoutTemplates/Billback.Master"
    AutoEventWireup="true" CodeBehind="ClaimSearch.aspx.cs" Inherits="Billback.Webforms.ClaimSearch" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/BillBack.css" rel="stylesheet" />
    <link href="../Scripts/jquery-ui.structure.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jquery-ui.theme.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.2.1.js" type="text/javascript"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        function getGrid() {
            return $find("<%= this.grdClaimSelect.ClientID %>");

        }

        function AddRow() {
            var grid = getGrid();
            //var rows = grid.get_rows();
            var newRowData = ["", "", "4054", "16944243.338", "S", "Velu", "", "PU33", "331", ""];
            //var row = new Array(grid.get_rows().get_length() + 1, "Item 1");
            grid.get_rows().add(newRowData);
            return false;
        }



        function openChildNewTab() {
            window.open("childGridPage.aspx", "ZoomGrid");
            return false;
        }


        function ShowAjaxIndicator() {
            var wdg = $find("<%= this.grdClaimSelect.ClientID %>");
            wdg.get_ajaxIndicator().show(wdg);
        }

        function HideAjaxIndicator() {
            var wdg = $find("<%= this.grdClaimSelect.ClientID %>");
            wdg.get_ajaxIndicator().hide();
        }

        function openChildPopup() {
            //OLD CODE NOT WORKING ON CHROME
            // myWindow = window.showModalDialog("AddClaim.aspx", "AddClaim", "dialogWidth=500px,dialogHeight=300px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes,left=200,top=200");


            //*** work around for Chrome  window.showModalDialog not working 1/25/2017 ***
            var w = 500;
            var h = 500;
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            myWindow = window.open("AddClaim.aspx", "AddClaim", "Width=" + w + "px,Height=" + h + "px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes, left="
                + left + ",top=" + top);


            return false;
        }

        function openChildPopupImport() {

            //OLD CODE NOT WORKING ON CHROME
            //   myWindow = window.showModalDialog("ImportClaims.aspx", "AddClaim", "dialogWidth=500px,dialogHeight=300px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes,left=200,top=200");


            //*** work around for Chrome  window.showModalDialog not working 1/25/2017 ***
            var w = 500;
            var h = 500;
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            myWindow = window.open("ImportClaims.aspx", "ImportClaim", "Width=" + w + "px,Height=" + h + "px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes, left="
             + left + ",top=" + top);
            return false;
        }


        function openChildNewServiceType() {

            var w = 500;
            var h = 700;
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            myWindow = window.open("NewServiceType.aspx", "NewServiceType", "Width=" + w + "px,Height=" + h + "px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes, left="
             + left + ",top=" + top);
            return false;
        }

        function openChildClientSearch() {

            var w = 680;
            var h = 550;
            var left = window.screenX + (window.outerWidth / 2) - (w / 2);
            var top = window.screenY + (window.outerHeight / 2) - (h / 2);
            myWindow = window.open("ClientSearch.aspx", "Client Search", "Width=" + w + "px,Height=" + h + "px, scrollbars=no, modal=yes,resizable=no, AlwaysRaised=yes, left="
             + left + ",top=" + top);
            return false;
        }

      
                       
    </script>
    <style type="text/css">
        tbody.Igg_Item > tr > td
        {
            /* style */
            font-size: 10px;
        }
    </style>
    <script type="text/javascript" language="javascript">
    
 
    </script>
    <script type="text/javascript">
        //        //On Page Load.
        //        $(function () {
        //            SetAutoComplete();
        //        });

        //        //On UpdatePanel Refresh.
        //        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //        if (prm != null) {
        //            prm.add_endRequest(function (sender, e) {
        //                if (sender._postBackSettings.panelsToUpdate != null) {
        //                    SetAutoComplete();
        //                }
        //            });
        //        };

        //        function SetAutoComplete() {
        //            $('#<%= txtClientID.ClientID %>').autocomplete({
        //                source: 'ClientHandler.ashx'
        //            });
        //        }

        //        $(document).ready(function () {
        //            $('#<%= txtClientID.ClientID %>').autocomplete({
        //                source: 'ClientHandler.ashx'
        //            });
        //        });
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <ig:WebScriptManager ID="WebScriptManager1" runat="server" AsyncPostBackTimeout="300">
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

        function DeleteRow() {
            var wdg = $find('<%= this.grdClaimSelect.ClientID  %>');
            var gridRows = wdg.get_rows()

            var selectedRows = wdg.get_behaviors().get_selection().get_selectedRows();
            for (var i = selectedRows.get_length() - 1; i >= 0; i--) {
                var row = selectedRows.getItem(i);
                gridRows.remove(row);
            }
        }

        function PreventSubmitOnEnter() {

           // if (event.keyCode == 13) return false;
            $(document).ready(function () {
                $(window).keydown(function (event) {
                    if (event.keyCode == 13) {
                        event.preventDefault();
                        return false;
                    }
                });
            });
        }
    </script>
    <div style="margin: 10px 10px 10px 10px;">
        <igmisc:WebGroupBox ID="WebGrp1" runat="server" BorderWidth="3px" Font-Bold="True"
            CssClass="OuterWebGrp" StyleSetName="" Text="" TitleAlignment="Left">
            <Template>
                <asp:UpdatePanel ID="updSearchParameter" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="verticalGap">
                            <igmisc:WebGroupBox ID="WebGrp0ServiceType" runat="server" CssClass="RequiredFields">
                                <Template>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblServiceType" runat="server" Text="Service Type:" Height="18px"
                                                    Width="150px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDropDown ID="ddlServiceType" runat="server" Width="350px" DisplayMode="DropDownList"
                                                    CssClass="ddl" AutoPostBack="True" OnSelectionChanged="ddlServiceType_SelectionChanged">
                                                </ig:WebDropDown>
                                            </td>
                                            <td style="width: 50px">
                                                &nbsp
                                            </td>
                                            <td style="text-align: right" colspan="4">
                                                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Add New Service Type"
                                                    Width="25px" Height="25px" ImageUrl="~/Images/Settings4.jpg" BackColor="Transparent"
                                                    OnClientClick="openChildNewServiceType()"  />
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </div>
                        <div style="font-size: 12px; font-weight: bold; text-align: left">
                            Required For Search</div>
                        <div class="verticalGap">
                            <igmisc:WebGroupBox ID="WebGrp1ReqFields" runat="server" CssClass="RequiredFields">
                                <Template>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblClientID" runat="server" Text="Client ID:" Width="150px" Height="10px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtClientID" runat="server" Width="150px" TextMode="Number"
                                                    Height="18px" placeholder="e.g. 1954" ToolTip="Enter Client ID and hit TAB or Select Search Icon to Search for Client" OnTextChanged="txtClientID_TextChanged" 
                                                    AutoPostBackFlags-ValueChanged="On" ClientEvents-KeyDown="PreventSubmitOnEnter">
                                                </ig:WebTextEditor>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgSearchClient" runat="server" ToolTip="Search for Client"
                                                    Width="25px" Height="25px" ImageUrl="~/Images/searchImg.jpg" BackColor="Transparent"
                                                    OnClientClick="openChildClientSearch()" />
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDataSet" runat="server" Text="Data Set:" Width="150px" Height="10px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDropDown ID="ddlDataSet" runat="server" Width="150px" DisplayMode="DropDownList"
                                                    CssClass="ddl" placeholder="e.g. WC">
                                                    <Items>
                                                        <ig:DropDownItem Selected="False" Text="WC" Value="0" ToolTip="Required Data Set"
                                                            Key="WC">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Key="DS" Selected="False" Text="DS" Value="1">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="GL" Value="2" Key="GL">
                                                        </ig:DropDownItem>
                                                    </Items>
                                                </ig:WebDropDown>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIncludeClaimStatus" runat="server" Text="Include Claim Status:"
                                                    Height="10px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDropDown ID="ddlIncludeClaimStatus" runat="server" Width="150px" DisplayMode="DropDownList"
                                                    CssClass="ddl" placeholder="example: OPEN">
                                                </ig:WebDropDown>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblClientName" runat="server" Text="Selected Client Name:" Width="150px" Height="10px" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td colspan="2" >
                                                <asp:Label ID="lblSelectedClientName" runat="server" Text="Not Selected" Width="180px" ForeColor="Black"
                                                    Height="14px"></asp:Label>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                           
                                            <td>
                                                <asp:Label ID="lblValuationDateFrom" runat="server" Text="Valuation Date From:" Width="150px"
                                                    Height="10px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDatePicker ID="dtValuationDateFrom" runat="server" Width="150px" DisplayModeFormat="d">
                                                    <Buttons>
                                                        <CustomButton ImageUrl="~/Images/calendar-img-black.png" AltText="CalendarImg" />
                                                    </Buttons>
                                                </ig:WebDatePicker>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblValuationDateTo" runat="server" Text="Valuation Date To:" Width="150px"
                                                    Height="10px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDatePicker ID="dtValuationDateTo" runat="server" Width="150px" DisplayModeFormat="d">
                                                    <Buttons>
                                                        <CustomButton ImageUrl="~/Images/calendar-img-black.png" AltText="CalendarImg" />
                                                    </Buttons>
                                                </ig:WebDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </div>
                        <br />
                        <div style="font-size: 12px; font-weight: bold; text-align: left">
                            Optional For Search</div>
                        <div class="verticalGap">
                            <igmisc:WebGroupBox ID="WebGrp2OptionalFields" runat="server" CssClass="OptionalFields">
                                <Template>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblExcludeClaimSubTypes" runat="server" Text="Exclude Claim Sub Types:"
                                                    Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtExcludeClaimSubTypes" Width="148px" runat="server" ToolTip="Separate multiple values with comma"
                                                    placeholder="example: AC,CB,CC " CssClass="txtFields">
                                                </ig:WebTextEditor>
                                            </td>
                                            <td style="width: 63px">
                                            </td>
                                            <td >
                                            </td>
                                            <td>
                                                <asp:Label ID="lblIncludeClaimTypes" runat="server" Text="Include Claim Types:" Width="150px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtIncludeClaimTypes" Width="148px" runat="server" ToolTip="Separate multiple values with comma"
                                                    placeholder="example: IN,MO " CssClass="txtFields">
                                                </ig:WebTextEditor>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblExcludeLineCodes" runat="server" Text="Exclude Line Codes:" Width="150px"
                                                    Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtExcludeLineCodes" Width="148px" runat="server" ToolTip="Separate multiple values with comma"
                                                    placeholder="example: AD,AI,AL " CssClass="txtFields">
                                                </ig:WebTextEditor>
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </div>
                        <br />
                        <div style="font-size: 12px; font-weight: bold; text-align: left">
                            Fee Allocation Details</div>
                        <div class="verticalGap">
                            <igmisc:WebGroupBox ID="WebGrp3FeeAllocFields" runat="server" CssClass="FeeAllocFields">
                                <Template>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMonthlyFee" runat="server" Text="Monthly Fee:" Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebCurrencyEditor ID="txtCurMonthlyFee" runat="server" Width="150px" AutoPostBackFlags-CustomButtonClick="On"
                                                    CssClass="txtFields" DataMode="Uint" OnCustomButtonClick="txtCurMonthlyFee_CustomButtonClick1"
                                                    MinDecimalPlaces="2">
                                                    <Buttons CustomButton-ToolTip="Update Allocation Amount" CustomButton-AltText="Update Alloc Amt"
                                                        CustomButtonDisplay="OnRight">
                                                        <CustomButton AltText="Update Alloc Amt" ToolTip="Update Allocation Amount" ImageUrl="../Images/MoneyImg.jpg" />
                                                    </Buttons>
                                                </ig:WebCurrencyEditor>
                                            </td>
                                            <td style="width: 65px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTaxID" runat="server" Text="Tax ID:" Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtTaxID" runat="server" Width="150px" TextMode="Number" placeholder="example: 4525 "
                                                    CssClass="txtFields">
                                                </ig:WebTextEditor>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblBInvoice" runat="server" Text="BInvoice:" Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebTextEditor ID="txtBInvoice" runat="server" Width="150px" TextMode="Number"
                                                    placeholder="example: B1090099" CssClass="txtFields">
                                                </ig:WebTextEditor>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblDtPayFrom" runat="server" Text="Date Pay From" Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDatePicker ID="dtpDatePayFrom" runat="server" Width="150px" DisplayModeFormat="d">
                                                    <Buttons>
                                                        <CustomButton ImageUrl="~/Images/calendar-img-black.png" AltText="CalendarImg" />
                                                    </Buttons>
                                                </ig:WebDatePicker>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDtPayThru" runat="server" Text="Date Pay Thru" Width="150px" Height="18px"></asp:Label>
                                            </td>
                                            <td>
                                                <ig:WebDatePicker ID="dtpDatePayThru" runat="server" Width="154px" DisplayModeFormat="d">
                                                    <Buttons>
                                                        <CustomButton ImageUrl="~/Images/calendar-img-black.png" AltText="CalendarImg" />
                                                    </Buttons>
                                                </ig:WebDatePicker>
                                            </td>
                                            <td style="width: 35px">
                                            </td>
                                           <td>
                                               
                                            </td>
                                            <td >
                                                <asp:Button ID="btnUpdateFeeAllocDetails" CssClass="button" runat="server" Text="Batch Update" style="margin-left:0px"
                                                    Width="150px" ToolTip="Update Fee, Dates, BInvoice and Tax ID" OnClick="btnUpdateFeeAllocDetails_Click"
                                                    OnClientClick="ShowAjaxIndicator()" />
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="updPanelTab0" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="padding: 5px">
                            <igmisc:WebGroupBox ID="WebGrpMainButtons" runat="server" CssClass="OptionalFields">
                                <Template>
                                    <table>
                                        <tr>
                                            <td colspan="8">
                                                <asp:Label ID="lblStatusMsg" runat="server" Text="Select Service Type to Continue"
                                                    Height="16px" Width="100%" CssClass="statusMessage"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSearchClaims" CssClass="button" runat="server" Text="Search" CommandName="SearchClaim"
                                                    OnClick="btnSearchClaims_Click" OnClientClick="ShowAjaxIndicator()" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAddClaims" CssClass="button" runat="server" Text="Add Claims"
                                                    OnClientClick="openChildPopup()" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnImportClaims" CssClass="button" runat="server" Text="Import Claims"
                                                    OnClientClick="openChildPopupImport()" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnRemoveClaims" CssClass="button" runat="server" Text="Remove Claims"
                                                    Visible="false" OnClick="btnRemoveClaims_Click" OnClientClick="ShowAjaxIndicator()" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnTestAddRow" Visible="false" CssClass="button" runat="server" Text="Test Add Claims"
                                                    OnClientClick="AddRow()" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnValidateProcClaims" CssClass="button" runat="server" Text="Validate / Process"
                                                    OnClick="btnValidateProcClaims_Click" OnClientClick="ShowAjaxIndicator()" />
                                            </td>
                                            <td style="width: 300px">
                                            </td>
                                            <td align="right" style="width: 600px">
                                                <table>
                                                    <tr align="right">
                                                        <td align="right">
                                                            <igmisc:WebGroupBox ID="WebGrpGridBtns" runat="server" BorderWidth="3px" BackColor="Transparent">
                                                                <Template>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnZoomGrid" runat="server" OnClientClick="openChildNewTab()"
                                                                                    ToolTip="Edit / View Grid in New Web Tab" Width="25px" Height="25px" ImageUrl="~/Images/grid2.jpg"
                                                                                    OnClick="btnZoomGrid_Click" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnExportToExcel" runat="server" ToolTip="Export Grid Data to Excel"
                                                                                    Width="65px" Height="25px" ImageUrl="~/Images/ExcelExport.jpg" OnClick="btnExportToExcel_Click" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:CheckBox ID="chkExportAllColumns" runat="server" Checked="true" Text="Export All Columns"
                                                                                    Visible="false" ForeColor="Blue" ToolTip="Check to Export Hidden Columns (e.g. Claim UID)" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnExportLog" runat="server" ImageUrl="~/Images/log-Img_200.png"
                                                                                    ToolTip="Export Log To Excel" Visible="false" Width="25px" Height="25px" Enabled="false"
                                                                                    OnClick="btnLogImage_Click" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnSaveImage" runat="server" ImageUrl="~/Images/SaveImg.jpg"
                                                                                    ToolTip="Save Grid Data" Visible="false" Width="25px" Height="25px" Enabled="false"
                                                                                    OnClick="btnSaveImage_Click" OnClientClick="ShowAjaxIndicator()" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnRefreshImg" runat="server" ToolTip="Refresh Grid Data" Width="25px"
                                                                                    Height="25px" ImageUrl="~/Images/RefreshImg2.jpg" OnClick="btnRefreshImg_Click"
                                                                                    OnClientClick="ShowAjaxIndicator()" />
                                                                            </td>
                                                                            <td style="width: 25px">
                                                                                &nbsp
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </Template>
                                                            </igmisc:WebGroupBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </Template>
                            </igmisc:WebGroupBox>
                        </div>
                        <div style="padding: 5px">
                            <ig:WebDataGrid ID="grdClaimSelect" runat="server" Height="600px" Width="100%" AutoGenerateColumns="False"
                                ItemCssClass="borderClass" AltItemCssClass="grdAltRow" OnInitializeRow="grdClaimSelect_InitializeRow"
                                HeaderCaptionCssClass="HeaderCaptionClass" DataKeyFields="GUID" OnRowsDeleting="grdClaimSelect_RowDeleting"
                                EnableDataViewState="True">
                                <EditorProviders>
                                    <ig:DatePickerProvider ID="DateInputProvider">
                                        <EditorControl runat="server" ClientIDMode="Predictable">
                                        </EditorControl>
                                    </ig:DatePickerProvider>
                                </EditorProviders>
                                <Columns>
                                    <ig:TemplateDataField Key="TemplateField_0" Width="20px" CssClass="ImgCenter">
                                        <ItemTemplate>
                                            <asp:Image ID="imgValidationError" runat="server" Width="20px"></asp:Image>
                                        </ItemTemplate>
                                    </ig:TemplateDataField>
                                    <ig:UnboundCheckBoxField HeaderChecked="False" Key="ClaimSelect" Width="10px" CssClass="chbox">
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
                                    <ig:BoundDataField DataFieldName="BINVOICE" Key="BINVOICE" DataType="System.String">
                                        <Header Text="BInvoice"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DATEFROM" Key="DATEFROM" DataType="System.DateTime">
                                        <Header Text="DateFrom"></Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DATETHRU" Key="DATETHRU" DataType="System.DateTime">
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
                                                    <ig:EditingColumnSetting ColumnKey="DOL" EditorID="DateInputProvider" />
                                                    <ig:EditingColumnSetting ColumnKey="DATEFROM" EditorID="DateInputProvider" />
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
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </Template>
        </igmisc:WebGroupBox>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
