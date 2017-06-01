using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.tool
{
    /// <summary>
    /// upload_ajax 的摘要说明
    /// </summary>
    public class upload_ajax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = HttpContext.Current.Request.QueryString["action"];

            switch (action)
            {
                case "EditorFile": //编辑器文件
                    EditorFile(context);
                    break;
                case "ManagerFile": //管理文件
                    ManagerFile(context);
                    break;
                default: //普通上传
                    UpLoadFile(context);
                    break;
            }
        }

        #region 普通上传
        private void UpLoadFile(HttpContext context)
        {
            HttpPostedFile _upfile = context.Request.Files["Filedata"]; //得到上传的文件
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (_upfile == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            Upload upFiles = new Upload();
            string msg = upFiles.fileSaveAs(_upfile, _isthumbnail, _iswater);

            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        } 
        #endregion

        #region 编辑器上传
        private void EditorFile(HttpContext context)
        {

        }
        #endregion

        #region 浏览器方法
        private void ManagerFile(HttpContext context)
        { }
        #endregion
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}