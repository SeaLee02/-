using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.UpControl2
{
    public partial class EditDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["test"] != null)
                {
                    txtExplain.Text = Session["test"].ToString();
                }
                else
                {
                    txtExplain.Text = "";
                }
            }
        }

        protected void lnkBtnSave_Click(object sender, EventArgs e)
        {
            Session["test"] = txtExplain.Text;
            //保存的是html标签
            Response.Redirect("EditDemo.aspx");
        }

        protected void lnkBtnClear_Click(object sender, EventArgs e)
        {
            Session["test"] = null;
            Response.Redirect("EditDemo.aspx");
        }
    }
}