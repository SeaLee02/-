using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.COM
{
    public partial class OnlineEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWord();
            }
        }

        public void BindWord()
        {
                PageOfficeCtrl1.OfficeVendor = PageOffice.OfficeVendorType.AutoSelect;
                PageOfficeCtrl1.ServerPage = Request.ApplicationPath + "pageoffice/server.aspx";
                PageOfficeCtrl1.Caption = "测试Word";
                //在只读模式下工具条和菜单栏都已不起作用，不需要显示
                PageOfficeCtrl1.OfficeToolbars = false;
                PageOfficeCtrl1.CustomToolbar = false;
            //打开文件
            if (File.Exists(Server.MapPath("../files/WordDemo.doc")))
            {
                PageOfficeCtrl1.SaveFilePage = "savefile.aspx"; // 设置 savefile.aspx 用来保存文档。
                // PageOfficeCtrl1.JsFunction_AfterDocumentOpened = "AfterDocumentOpened()";
                PageOfficeCtrl1.WebOpen(Server.MapPath("../files/WordDemo.doc"), PageOffice.OpenModeType.docNormalEdit, "SeaLee");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('文件未找到');</script>");
            }
        }
    }
}