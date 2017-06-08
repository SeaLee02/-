<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDemo.aspx.cs" Inherits="WebDemo.UpControl2.EditDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>编辑器第二种写法</title>
    <link href="../CSS/UpControl2/UploadDemo.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="group">
            <label class="title">类别说明</label>
            <div class="controls">
                <asp:TextBox runat="server" ID="txtExplain" TextMode="MultiLine" Width="1000" Height="500"></asp:TextBox>
            </div>
        </div>
          <asp:LinkButton ID="lnkBtnSave" runat="server" OnClick="lnkBtnSave_Click">保存</asp:LinkButton>
         <br />
         <asp:LinkButton ID="lnkBtnClear" runat="server" OnClick="lnkBtnClear_Click">清除Session</asp:LinkButton>

        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../editor/kindeditor-min.js"></script>
        <%--<script src="../editor/lang/zh-CN.js"></script>--%>
        <script>
            $(function () {
                KindEditor.ready(function (K) {
                    var editor1 = K.create('#txtExplain', {
                        cssPath: '/editor/plugins/code/prettify.css',
                        uploadJson: '/Tools/upload_json.ashx',
                        fileManagerJson: '/Tools/file_manager_json.ashx',
                        allowFileManager: true,
                        afterBlur: function () {
                            this.sync();  //这个方法必须，不加这个我们的textarea是取不到值的
                        }
                    });
                });
            })

        </script>

    </form>
</body>
</html>
