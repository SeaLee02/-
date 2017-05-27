using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionDemo.Model;
using FunctionDemo.BLL;
using System.Web;


namespace FunctionDemo.Common
{
  public static class PageBase
    {
        public static Model.Users GetUseInfo()
        {
            if (IsLogin())
            {
                Model.Users userModel =HttpContext.Current.Session["UserModel"] as Model.Users;
                if (userModel != null)
                {
                    return userModel;
                }
            }
            return null;
        }

        public static bool IsLogin()
        {
            if (HttpContext.Current.Session["UserModel"] != null)
            {
                return true;
            }
            else
            {
                string name = Utils.GetCookie("UserName", "FunctionDemo");
                string pwd = Utils.GetCookie("UserPwd", "FunctionDemo");
                Model.Users userModel = new BLL.Users().GetModel(name, pwd);
                if (userModel != null)
                {
                    HttpContext.Current.Session["UserModel"] = userModel;
                    return true;
                }
            }
            return false;
        }
    }
}
