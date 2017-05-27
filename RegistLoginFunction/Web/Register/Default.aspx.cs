using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        FunctionDemo.BLL.Users userBll = new FunctionDemo.BLL.Users();
        //利用可选参数来赋值
        if (userBll.Exists(LoginName:LoginName.Value))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('次用户名已存在');", true);
            return;
        }
        FunctionDemo.Model.Users userModel = new FunctionDemo.Model.Users();
        userModel.LoginName = LoginName.Value;
        userModel.PassWord = Loginpassword.Value;
        if (userBll.Add(userModel)>0)
        {
            Response.Redirect("../Login/LoginDemo.aspx");
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "alert('注册出现问题');", true);
            return;
        }

    }
}