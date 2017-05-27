<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDemo.aspx.cs" Inherits="FunctionDemo.Web.Login.Login_LoginDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登入页面</title>
    <link href="../CSS/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login">
            <dl class="main">
                <dd>
                    <u>用户名：</u>
                    <input placeholder="用户名" type="text" id="LoginName" runat="server" />
                </dd>
                <dd>
                    <u>密码：</u>
                    <input placeholder="登录密码" type="password" id="Loginpassword" runat="server" />
                </dd>
                <dd>
                    <u>验证码：</u>
                    <input class="code" placeholder="验证码" type="text" id="code" runat="server" />
                     <%-- 路径给的一般处理程序，就会去找这个，而这个处理程序放回的就是一张图片--%>
                    <img src="verify_code.ashx" />  
                </dd>
                <dt>
                    <asp:Button ID="bt_Login" runat="server" Text="登 录" OnClick="bt_Login_Click" />
                    <a href="../Register/Default.aspx">用户注册</a>
                </dt>
            </dl>
        </div>
        <script src="../JS/jquery.min.js"></script>
        <script>
            'use strict';

            //点击图片获取新的验证码
            $("img").click(function () {
                var dd = $(this).attr("src");
                //访问一次verify_code就会得到一个随机数  如果不设置src点击图片并不会去访问这个处理程序，就随便加一个参数就可以
                $(this).attr("src", dd + "?time=" + +Math.random());                
            })


        </script>
    </form>
</body>
</html>
