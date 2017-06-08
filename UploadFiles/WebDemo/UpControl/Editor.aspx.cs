using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.UpControl
{
    public partial class Editor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["test"] != null)
                {
                    txt_Analysis.Value = Session["test"].ToString();
                }
                else
                {
                    txt_Analysis.Value = "";
                }
            }
        }
    
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["test"] = txt_Analysis.Value;
            //保存的是html标签
            Response.Redirect("Editor.aspx");
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["test"] = null;
            Response.Redirect("Editor.aspx");
        }
    }
}