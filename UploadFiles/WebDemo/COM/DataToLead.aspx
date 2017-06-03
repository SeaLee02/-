<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataToLead.aspx.cs" Inherits="WebDemo.COM.DataToLead" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>数据导入</title>
    <link href="../CSS/TableCSS.css" rel="stylesheet" />
    <link href="../CSS/dataTolead.css" rel="stylesheet" />
    <%--弹出上传框--%>
    <link href="../CSS/icon.css" rel="stylesheet" />
    <%--图标--%>
    <link href="../CSS/uploadTest.css" rel="stylesheet" />
    <%--下载模板--%>
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin-left: 100px;">
            <a class="add" href="javascript:showDiv('m1');">导入<br />
                数据</a>
        </div>
        <br />
        <br />
        <div class="tableDiv">
            <asp:Repeater ID="DemoRepeater" runat="server">
                <HeaderTemplate>
                    <%--cellpadding="0" cellspacing="0"--%>
                    <table>
                        <tbody>
                            <tr>
                                <th>序号
                                </th>
                                <th>姓名
                                </th>
                                <th>性别
                                </th>
                                <th>年龄
                                </th>
                                <th>电话号码</th>
                            </tr>                     
                  
                </HeaderTemplate>
                <ItemTemplate>
                  <tr>
                      <td><%=RowIndex++ %></td>
                      <td><%#Eval("Name") %></td>
                      <td><%#Eval("Sex") %></td>
                      <td><%#Eval("Age") %></td>
                      <td><%#Eval("Tel") %></td>
                   </tr>  
                </ItemTemplate>
                <FooterTemplate>
                       </tbody></table>
                </FooterTemplate>
            </asp:Repeater>
        </div>



        <!--弹出选项 -->
        <div id="m1" class="shade" style="display: none;">
            <div class="shade_mask">
                <span class="title"><em>导入服务对象数据</em> <a onclick="closeDiv();" href="javascript:void();">关闭</a> </span>
                <div class="sheade_data">
                    <span class="list">
                        <u>导入数据：</u>
                        <span style="width: 280px; height: 32px; float: left; position: relative;">
                            <asp:TextBox ID="txtFileUrl" runat="server" CssClass="upload-path txte" Text="" />
                            <div class="upload-img" id="upload-img"></div>
                        </span>
                    </span>
                    <span class="list"><i class="icon"></i>
                        <a class="dxz" href="../Template/上传模板.xls">点击下载模板（Excel表格）</a></span>
                </div>
                <span class="button">
                    <asp:LinkButton ID="lbSave" CssClass="bc" runat="server" OnClick="lbSave_Click" OnClientClick="return SaveLaunch();">确认上传</asp:LinkButton>
                    <a class="qx" href="javascript:closeDiv()">取消</a> </span>
            </div>
        </div>

        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../JS/webuploader/webuploader.js"></script>
        <script src="../JS/uploader.js"></script>
        <script src="../JS/jquery.tableui.js"></script>
        <%--生成下载模板的JS--%>
        <script>

            function closeDiv() {
                document.getElementById('m1').style.display = "none";
                $("#txtFileUrl").val("");
            }
            function showDiv(divid) {
                document.getElementById('m1').style.display = "none";
                document.getElementById(divid).style.display = "block";
            }
            $(function () {
                $("table").tableUI();
                $(".form_main dd:even").css({ "background": "#F0F0F0" });
                //初始化上传控件    
                $(".upload-img").InitUploader(
                    {
                        chunked: false,
                        filesize: "20480000",
                        sendurl: "../tool/upload_ajax.ashx",
                        swf: "../js/webuploader/uploader.swf",
                        filetypes: "xls,xlsx"
                        //只能上传Excel表格
                    });
            })


        </script>
    </form>
</body>
</html>
