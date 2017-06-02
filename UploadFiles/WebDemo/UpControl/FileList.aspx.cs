using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;

namespace WebDemo.UpControl
{
    public partial class FileList : System.Web.UI.Page
    {
        public int RowIndex
        {
            get
            {
                object o = ViewState["RowIndex"];
                return o == null ? 0 : int.Parse(o.ToString());
            }
            set { ViewState["RowIndex"] = value; }
        }

        public int FileTypeID
        {
            get
            {
                object o = ViewState["FileTypeID"];
                return o == null ? 0 : int.Parse(o.ToString());
            }
            set { ViewState["FileTypeID"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepater();
            }
        }

        void BindRepater()
        {
            FunctionDemo.BLL.Files fs = new FunctionDemo.BLL.Files();
            DemoRepeater.DataSource = fs.GetList("FileType=0").Tables[0];
            DemoRepeater.DataBind();
        }

        /// <summary>
        /// 普通下载
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void DemoRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName== "download")
            {
                string filePath = e.CommandArgument.ToString();//路径
                try
                {
                    string FullFileName = Server.MapPath(filePath);//需要下载的文件名
                    FileInfo DownLoadFile = new FileInfo(FullFileName);
                    if (DownLoadFile.Exists)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.Buffer = false;
                        Response.ContentType = "application/octet-stream";//二进制流（常见下载）
                        //添加头部，做什么处理  如果下载的出现乱码就编下码
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" + DownLoadFile.Name);// HttpUtility.UrlEncode(DownLoadFile.Name,System.Text.Encoding.ASCII));
                        Response.AppendHeader("Content-Length", DownLoadFile.Length.ToString());
                        Response.WriteFile(DownLoadFile.FullName);
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        
                    }
                }
                catch 
                {

                }
            }
        }

        /// <summary>
        ///测试下载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_DownLoad_Click(object sender, EventArgs e)
        {
            //以文件下载
            string fileName = Server.MapPath("/files/登入模块.doc");
            //FileInfo fn = new FileInfo(fileName);
            //Response.Clear();          
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.AppendHeader("Content-Disposition", "attachment;filename="+HttpUtility.UrlEncode(fn.Name,System.Text.Encoding.UTF8));
            //Response.AppendHeader("Content-Length",fileName.Length.ToString());
            //Response.WriteFile(fileName);
            //Response.Flush();
            //Response.End();

            //以二进制字符下载
            FileStream fs = new FileStream(fileName, FileMode.Open);         
            byte[] buff = new byte[fs.Length];
            fs.Read(buff, 0, buff.Length);
            fs.Close();
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("登入模块.doc", System.Text.Encoding.UTF8));
            Response.BinaryWrite(buff);
            Response.Flush();
            Response.End();

        }

        /// <summary>
        /// 解压形式的下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lb_YS_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/zip";//压缩类型
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            Response.AppendHeader("Content-Disposition","filename="+fileName+".zip");
            using (ZipFile zf=new ZipFile(System.Text.Encoding.Default)) //默认给个编码，不然会出现乱码
            {
                foreach (RepeaterItem item in DemoRepeater.Items)
                {
                    CheckBox cbID = item.FindControl("chkId") as CheckBox;
                    //这是强势转化，如果确定类型可以这么用,不确实就上面的方法
                    HiddenField hidFilePath = (HiddenField)item.FindControl("hidFilePath");
      
                    if (cbID.Checked)
                    {
                        // 同一个zip下面不能有两个相同名字的文件夹  这里的文件名可以给个动态的
                        //zf.AddDirectoryByName("测试文件夹");//添加文件夹 
                        if (!zf.EntryFileNames.Contains(hidFilePath.Value.Split('/')[hidFilePath.Value.Split('/').Length - 1].Trim()))
                        {
                            zf.AddFile(Server.MapPath(hidFilePath.Value), "");
                            //第一个参数是路径,需要在项目中找到的路劲，第二个参数是文件夹，没有就给个""
                            //zf.AddFile(Server.MapPath(hidFilePath.Value), "测试文件夹");
                        }
                    }
                }
                zf.Save(Response.OutputStream);
            }
            Response.End();
        }
    }
}