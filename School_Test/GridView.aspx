<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GridView1.aspx.vb" Inherits="School_Test.GridView1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }
        table {
            border: 1px solid #ccc;
            border-collapse: collapse;
            background-color: #fff;
        }
        table th {
            background-color: #B8DBFD;
            color: #333;
            font-weight: bold;
        }
        table th, table td {
            padding: 5px;
            border: 1px solid #ccc;
        }
        table, table table td {
            border: 0px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="dvGrid" style="padding: 10px; width: 450px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="width: 150px">
                            BL_NUMBER:<br />
                            <asp:TextBox ID="TextBox1" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">
                            CONSIGNEE:<br />
                            <asp:TextBox ID="TextBox2" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">
                            BL_TYPE:<br />
                            <asp:TextBox ID="TextBox3" runat="server" Width="140" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                    DataKeyNames="BL_ID" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" PageSize="3"
                    AllowPaging="true" OnPageIndexChanging="OnPaging" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting"
                    EmptyDataText="No records has been added." Width="450">
                    <Columns>
                        <asp:BoundField DataField="BL_ID" HeaderText="BL ID" />
                        <asp:TemplateField HeaderText="BL Number" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblBLNumber" runat="server" Text='<%# Eval("BL_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBLNumber" runat="server" Text='<%# Eval("BL_NUMBER") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consignee" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblConsignee" runat="server" Text='<%# Eval("CONSIGNEE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtConsignee" runat="server" Text='<%# Eval("CONSIGNEE") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BL Type" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="lblBLType" runat="server" Text='<%# Eval("BL_TYPE") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtBLType" runat="server" Text='<%# Eval("BL_TYPE") %>' Width="140"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
                <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="width: 150px">
                            BL Number:<br />
                            <asp:TextBox ID="txtBLNumberInsert" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">
                            Consignee:<br />
                            <asp:TextBox ID="txtConsigneeInsert" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">
                            BL Type:<br />
                            <asp:TextBox ID="txtBLTypeInsert" runat="server" Width="140" />
                        </td>
                        <td style="width: 150px">
                            <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="scripts/jquery.blockUI.js"></script>
    <script type="text/javascript">
        $(function () {
            BlockUI("dvGrid");
            $.blockUI.defaults.css = {};
        });
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<div align="center">' + '<img src="images/loadingAnim.gif"/></div>',
                    css: {},
                    overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB' }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        };
    </script>
</body>
</html>




