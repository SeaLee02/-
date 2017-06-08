<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadDemo.aspx.cs" Inherits="WebDemo.UpControl2.UploadDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>利用表单文件上传</title>
    <link href="../CSS/UpControl2/UploadDemo.css" rel="stylesheet" />
</head>
<body>
    <%--   enctype="multipart/form-data"   可以上传多种格式--%>
    <form id="myfrom" runat="server" enctype="multipart/form-data">
        <h5>利用表单文件上传</h5>
        <div class="group">
            <label class="title">上传</label>
            <div class="controls ">
                <ul class="filebox">
                    <li class="txtfilename">
                       <asp:TextBox runat="server" ID="txtTypeImg" CssClass="input02"></asp:TextBox>
                    </li>
                    <li class="btnfilea"><a href="javascript:void(0)" onclick="btnfileaclick($(this))" class="btnfilebtn">上传图片</a>
                    </li>
                    <li class="fileinput">
                        <input type="file" name="fileuploadico" id="fileuploadico" onchange="fileonchange($(this),'myfrom')" />
                    </li>
                </ul>
            </div>
        </div>
        <script src="../JS/jquery-1.11.0.min.js"></script>
        <script src="../JS/UpControl2/jquery.form.min.js"></script>  <%--form表单提交，不能少--%>
        <script src="../JS/UpControl2/jquery.leadinupload.js"></script> <%--脚本处理 --%>
    </form>
</body>
</html>
