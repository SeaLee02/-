<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Register_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <title>注册</title>
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
                    <u>确认密码：</u>
                    <input placeholder="确认密码" type="password" id="RPassWord" runat="server" />
                </dd>
                <dd>
                    <u>手机号码：</u>
                    <input placeholder="手机号码" type="text" id="Tel" runat="server" />
                </dd>
                <dd>
                    <u>验证码：</u>
                    <asp:HiddenField ID="hfCode" runat="server" />
                    <input class="code" placeholder="验证码" type="text" id="code" runat="server" />
                   
                    <input type="text" id="GCode" data-flag="60" data-base="获取验证码" runat="server" style="width: 80px; height: 16px; margin-left: 10px;" value="获取验证码" readonly="readonoly" />
                </dd>
                <dt>
                    <asp:Button ID="btnRegister" runat="server" Text="注册" OnClick="btnRegister_Click" OnClientClick="return Confirm();" />

                </dt>
            </dl>
        </div>
        <script src="../JS/jquery.min.js"></script>
        <script>
            'use strict';
            function Confirm() {
                var LoginName = $("#LoginName").val();
                if (LoginName === "" || LoginName === null) {
                    alert("请填写用户名！");
                    $("#LoginName").focus();
                    return false;
                }

                var Loginpassword = $("#Loginpassword").val();
                if (Loginpassword === "" || Loginpassword === null) {
                    alert("请填写密码！");
                    $("#Loginpassword").focus();
                    return false;
                }
                var RPassWord = $("#RPassWord").val();
                if (RPassWord === "" || RPassWord === null) {
                    alert("请填写确认密码！");
                    $("#RPassWord").focus();
                    return false;
                }

                if (RPassWord !== Loginpassword) {
                    alert("两次密码不一样！");
                    $("#RPassWord").focus();
                    return false;
                }

                var Tel = $("#Tel").val();
                var verify_tel = /^1[34578]\d{9}$/;
                if (!verify_tel.test(Tel)) {
                    alert("请填写正确的手机号码！");
                    $("#Tel").focus();
                    return false;
                }

                var code = $("#code").val();
                if (code === "" || code === null) {
                    alert("请填写验证码！");
                    $("#code").focus();
                    return false;
                }

                if (code!=$("#hfCode").val()) {
                    alert("您输入的验证码跟系统的不匹配");
                    $("#code").focus();
                    return false;
                }

            }


            var result = true;
            function GetCode() {
                var Tel = $("#Tel").val();
                var verify_tel = /^1[34578]\d{9}$/;
                if (!verify_tel.test(Tel)) {
                    result = false;
                    alert("请填写正确的手机号码！");
                    $("#Tel").focus();
                    return false;
                }
                result = true;
                $.ajax({
                    type: "post",
                    url: "RegisterHandler.ashx",
                    datatype: "json",
                    data: "type=GetCode&Phone="+Tel,
                    async: false,
                    success: function (data) {
                        console.log(data);
                        var GetData = data.toString().split('-')[0];
                        //alert(GetData);
                        console.log(GetData);
                        if (GetData == 0) {
                            alert("该号码已注册！");
                        } else if (GetData == 1) {
                            $("#hfCode").val(data.toString().split('-')[1]);
                            alert("发送成功！");
                        } else if (GetData == 2) {
                            alert("发送失败，请重新获取！");
                        }
                    }
                })
            }




            //计时器
            var bb;
            $("#GCode").bind("click", function () {
                GetCode();
                if (!result) {
                    return result;
                }
                $(this).attr("disabled", "disabled");
                $(this).attr("data-flag", "60");                    
                Test();
                bb = setInterval("Test()", 1000);
            });

            function Test() {
                $("#GCode").css("background-color", "#808080");
                var BeginValue = $("#GCode").attr("data-base");
                var BeginFlag = $("#GCode").attr("data-flag");
                $("#GCode").val(BeginValue + $("#GCode").attr("data-flag"));
                BeginFlag = Number(BeginFlag) - 1;
                if (BeginFlag == 0) {
                    BeginFlag = "";
                }
                $("#GCode").attr("data-flag", BeginFlag);
                if (BeginFlag == "-1") {
                    $("#GCode").removeAttr("disabled");
                    $("#GCode").css("background-color", "#fff");
                    clearInterval(bb);
                }
            }


        </script>
    </form>
</body>
</html>
