using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using LitJson;

namespace WebDemo.Tool2
{
    /// <summary>
    /// Update 的摘要说明
    /// </summary>
    public class Update : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //post提交
            string fileid = context.Request["fileid"].ToString();//上传控件ID
            string txtid = context.Request["txtid"].ToString();//文本框ID

            HttpPostedFile file = context.Request.Files[fileid]; //得到上传文件
            string uploadpath = HttpContext.Current.Server.MapPath("/UplaodFileds/");//绝对路径

            //获取扩展名  只有知道完全路径才能用Path来获取
            string fileExtension = System.IO.Path.GetExtension(file.FileName);
            string _NewFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;//新名字

            string _Folder = DateTime.Now.ToString("yyyyMMdd");//文件夹
            string _NewPath = uploadpath + _Folder + "\\";
            if (file != null)
            {
                if (!Directory.Exists(_NewPath)) //判断文件夹是否存在 不在就创建一个
                {
                    Directory.CreateDirectory(_NewPath);
                }
                file.SaveAs(_NewPath + _NewFileName); //保存
            }
             
            JsonData data = new JsonData();  //数据转型  需要引用ListJson

            data["txtName"] = txtid;
            data["filePath"] = "/UplaodFileds/" + _Folder + "/" + _NewFileName;

            // 如果不想调用上面的类就返回一个字符串格式的Json数据
            // "{\"txtName\": \"" + txtid + "\", \"filePath\": \"" + "/UplaodFileds/" + _Folder + "/" + _NewFileName + "\"}"


            //返回json格式 txtName:"文本框ID",filePath:"上传文件的路径"
            
            context.Response.Write(data.ToJson());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}