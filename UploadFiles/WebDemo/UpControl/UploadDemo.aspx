<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadDemo.aspx.cs" Inherits="WebDemo.UpControl.UploadDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>上传文件测试</title>
    <link href="../CSS/upload.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <%--使用了绝对定位 #upload-img里面的都会根据这个来定位 可以通过这个来设置宽度（.txte 的宽度跟着一起设置）  --%>
        <div style="width: 357px; height: 32px; float: left; position: relative;">
            <div style="float: left; width: 50px; height: 16px; line-height: 16px; padding: 6px 0 6px 10px">上传：</div>
            <%--第一个这个div的宽度需要比下面的文本框宽度大--%>
            <asp:TextBox ID="txtFileUrl" runat="server" CssClass="upload-path txte" Text="" />
            <%--浏览的文字宽度是84，也就是说文本框的宽度+84=上面div的宽度--%>
            <div class="upload-img" id="upload-img">

               <%-- 这个div里面会加载一些标签，这个是在js里面手动加的，上传成功了就会显示值，也是在js中完成的--%>

            </div>

            <%--用来存储信息--%>
            <div style="display: none;">
                <u>文件名称：</u>
                <input class="upload-name" type="text" id="txtTitle" value="" runat="server" />
            </div>
            <div style="display: none;">
                <u>文件大小：</u>
                <input class="upload-size" type="text" id="txtFileSize" value="" runat="server" />
            </div>
        </div>

        <asp:LinkButton ID="lbSave" runat="server" OnClick="lbSave_Click" OnClientClick="return Comfirm();">确认上传</asp:LinkButton>

        <div>
            <a href="FileList.aspx">查看文件</a>
        </div>


        <script src="../JS/jquery-1.11.0.min.js"></script>
        <%--uploader初始化create需要这个引用里面来调用--%>
        <script src="../JS/webuploader/webuploader.js"></script>
        <script src="../JS/uploader.js"></script>
        <script>
            //先执行
            $(function () {
                $(".upload-img").InitUploader({
                    chunked: false,
                    filesize: 102400,
                    sendurl: "/tool/upload_ajax.ashx",
                    swf: "/js/webuploader/uploader.swf",
                    filetypes: "jpg,jpeg,gif,png,flv,mp4,mp3,doc,xls,xlsx"
                    //可以上传的类型，根据你的需求，第一次过滤，如果上传的类型不在其中，就会提示错误   在代码中还会过滤一次
                })
            });


            function Comfirm() {
               var dd=$("#txtFileUrl").val();
               if (dd == "") {
                   alert("您还没有选择上传的文件");
                   return false;
               }
            }

        </script>
    </form>
</body>
</html>
