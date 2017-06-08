<%@ webhandler Language="C#" class="Upload" %>

/**
 * KindEditor ASP.NET
 *
 * 本ASP.NET程序是演示程序，建议不要直接在实际项目中使用。
 * 如果您确定直接使用本程序，使用之前请仔细确认相关安全设置。
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Globalization;
using LitJson;
using System.Web.Script.Serialization;



public class Upload : IHttpHandler
{
	private HttpContext context;

	public void ProcessRequest(HttpContext context)
	{
       
        SortedList<string, object> values = new SortedList<string, object>();
        JavaScriptSerializer js = new JavaScriptSerializer();
        HttpPostedFile _upfile = context.Request.Files["imgFile"];

        if (_upfile == null)
        {
            values.Add("error", 1);
            values.Add("message", "上传失败，没有选择文件");
            context.Response.Write(js.Serialize(values));
            return;
        }
        context.Response.Write(js.Serialize(fileSaveAs(_upfile)));
       
	}



    ///<summary>
    /// 文件上传方法E
    /// </summary>
    public SortedList<string, object> fileSaveAs(HttpPostedFile _postedFile)
    {

        SortedList<string, object> values = new SortedList<string, object>();

        try
        {
            string fileType = "jpg|png|jpeg|gif";

            string _fileExt = _postedFile.FileName.Substring(_postedFile.FileName.LastIndexOf(".") + 1);
            //验证合法的文件
            if (!CheckFileExt(fileType, _fileExt))
            {
                values.Add("error", 0);
                values.Add("message", "上传失败，错误的文件类型");
                return values;
            }

            int fileSize = 3*1024;
            if (fileSize > 0 && _postedFile.ContentLength > fileSize * 1024)
            {
                values.Add("error", 0);
                values.Add("message", "上传失败，您选择的文件过大");
                return values;
            }
            string _fileName = DateTime.Now.ToString("yyyyMMddHHmmssff") + "." + _fileExt;

            string filePath = "/UplaodFileds/";

            //检查保存的路径 是否有/开头结尾
            if (filePath.StartsWith("/") == false) filePath = "/" + filePath;
            if (filePath.EndsWith("/") == false) filePath = filePath + "/";
            //按日期归类保存
            string _datePath = DateTime.Now.ToString("yyyyMMdd") + "/";
            filePath += _datePath;


            //获得要保存的文件路径
            string serverFileName = filePath + _fileName;

            //物理完整路径                    
            string toFileFullPath = HttpContext.Current.Server.MapPath(filePath);
            //检查是否有该路径没有就创建
            if (!Directory.Exists(toFileFullPath))
            {
                Directory.CreateDirectory(toFileFullPath);
            }
            //将要保存的完整文件名                
            string toFile = toFileFullPath + _fileName;


            //保存文件
            _postedFile.SaveAs(toFile);


            values.Add("error", 0);
            values.Add("message", "上传成功");
            values.Add("url", serverFileName);

            return values;

        }
        catch
        {
            return values;
        }
    }

    /// <summary>
    /// 检查是否为合法的上传文件
    /// </summary>
    /// <returns></returns>
    private bool CheckFileExt(string _fileType, string _fileExt)
    {
        string[] allowExt = _fileType.Split('|');
        for (int i = 0; i < allowExt.Length; i++)
        {
            if (allowExt[i].ToLower() == _fileExt.ToLower())
            {
                return true;
            }
        }
        return false;
    }
    
    
  

	public bool IsReusable
	{
		get
		{
			return true;
		}
	}
}
