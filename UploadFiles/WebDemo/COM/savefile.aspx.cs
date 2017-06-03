using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.COM
{
    public partial class savefile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageOffice.FileSaver fs = new PageOffice.FileSaver();
            string strFileName = fs.FileName;
            string strFileExtName = fs.FileExtName;
            int iFileSize = fs.FileSize;

            // 保存当前文档到服务器文件夹。
            fs.SaveToFile(Server.MapPath("../files/") + strFileName);

            // 文档保存最后需调用 Close 方法。
            fs.Close();

        }
    }
}