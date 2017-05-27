using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FunctionDemo;
using FunctionDemo.Common;


namespace FunctionDemo.Web.Login
{
    public partial class Login_LoginDemo :Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_Login_Click(object sender, EventArgs e)
        {
            BLL.Users usersBLL = new BLL.Users();
            String name = LoginName.Value.Trim();
            string pwd = Loginpassword.Value.Trim();
            string codes = code.Value.Trim();

            if (string.IsNullOrEmpty(name))
            {
                //两种弹框
                //Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('请输入登录名');</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('请输入登录名');", true);
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('请输入密码');", true);
                return;
            }

            #region 验证码
            if (string.IsNullOrEmpty(codes))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('请输入验证码');", true);
                return;
            }
            if (HttpContext.Current.Session["dt_session_code"]==null)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('验证码过期，请重新获取');", true);
                return;
            }          
            if (codes.Trim().ToLower()!=HttpContext.Current.Session["dt_session_code"].ToString().ToLower())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('您输入的验证码跟系统的不一样');", true);
                return;
            }
            #endregion
            //是否存在用户名  利用命名参数
            if (!usersBLL.Exists(LoginName:name))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('此用户名不存在');", true);
                return;
            }
            if (!usersBLL.Exists(LoginName:name,PassWord:pwd))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('登入名或密码不正确');", true);
                return;
            }
            //Session存一遍
            Model.Users userModel = usersBLL.GetModel(name,pwd);
            Session["UserModel"] = userModel;
            Session.Timeout = 1;
            //再用cookies写一遍  解决时间超时问题
            Utils.WriteCookie("UserName", "FunctionDemo", userModel.LoginName);
            Utils.WriteCookie("UserPwd","FunctionDemo",userModel.PassWord);
            Model.LoginLog logModel = new Model.LoginLog();
            logModel.UserID = userModel.ID;
            logModel.LoginTime = DateTime.Now;
            logModel.Remark = IPAddress; //登入IP
            if (new BLL.LoginLog().Add(logModel)>0)
            {
                Response.Redirect("TestDemo1.aspx");
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('登入日志出问题了');", true);
                return;
            }

         
        }
        /// <summary>
        /// 登入IP
        /// </summary>
        public string IPAddress
        {
            get
            {
                string userIP;
                HttpRequest Request = HttpContext.Current.Request;
                // 如果使用代理，获取真实IP 
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    userIP = Request.ServerVariables["REMOTE_ADDR"];
                else
                    userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (userIP == null || userIP == "")
                    userIP = Request.UserHostAddress;
                return userIP;
            }
        }
    }
}