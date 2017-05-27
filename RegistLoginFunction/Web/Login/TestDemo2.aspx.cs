using System;
using System.Web.UI;
using FunctionDemo.Common;

namespace FunctionDemo.Web.Login
{
    public partial class Login_TestDemo2:Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //是否登入
                if (PageBase.GetUseInfo() == null)
                {
                    Response.Redirect("LoginDemo.aspx");
                }
            }
        }
    }
}