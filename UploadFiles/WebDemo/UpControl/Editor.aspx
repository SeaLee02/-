<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editor.aspx.cs" Inherits="WebDemo.UpControl.Editor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>编辑器第一种写法</title>
    <link href="../CSS/edit.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">       
    <div class="edit">
       <textarea class="editor"  id="txt_Analysis" runat="server"></textarea>
    </div>

        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">保存</asp:LinkButton>
         <br />
         <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">清除Session</asp:LinkButton>
        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../editor/kindeditor-min.js"></script>
        <script>
            'use strict';  
            $(document).ready(function () {
                editor = KindEditor.create('.editor', {
                    width: '99.5%',
                    height: '350px',
                    resizeType: 1,
                    uploadJson: '../tool/upload_ajax.ashx?action=EditorFile&IsWater=1',
                    fileManagerJson: '../tool/upload_ajax.ashx?action=ManagerFile',
                    allowFileManager: true,
                    afterBlur: function () {     //这个方法必须，在4.1中不加这个我们的textarea是取不到值的
                        this.sync();
                    }
                });
            });
        </script>
       
    </form>
</body>
</html>
