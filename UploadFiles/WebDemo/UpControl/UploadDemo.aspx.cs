using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebDemo.UpControl
{
    public partial class UploadDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string aa = "{\"status\": 0, \"msg\": \"不允许上传类型的文件！\"}";
                //Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "msg", "<script>alert("+aa+");</script>");
                //Response.Write();
            }
         
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            FunctionDemo.BLL.Files filesBLL = new FunctionDemo.BLL.Files();
            FunctionDemo.Model.Files filesModel = new FunctionDemo.Model.Files();
            filesModel.FileName = txtTitle.Value.Trim();
            filesModel.FilePath = txtFileUrl.Text.Trim();
            filesModel.FileSize = (int.Parse(txtFileSize.Value.Trim()) / 1024) + "K";
            filesModel.UpdateTime = DateTime.Now;
            int i = txtFileUrl.Text.Trim().LastIndexOf(".") + 1;
            string Name = txtFileUrl.Text.Trim().Substring(i);
            tool.Upload up = new tool.Upload();
            if (up.IsImage(Name.ToLower()))
            {
                filesModel.FileType = 0;
            }
            else
            {
                filesModel.FileType = 1;
            }
            if (filesBLL.Add(filesModel)>0)
            {
                Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> alert('上传成功');</script>");
                txtTitle.Value = "";
                txtFileUrl.Text = "";
                txtFileSize.Value = "";
                return;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> alert('上传失败');</script>");
            }
        }
    }
}