using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections;


namespace WebDemo.tool
{
    public class Upload
    {
        /// <summary>
        /// 无参构成函数 ctor在按两次Table
        /// </summary>
        public Upload()
        {

        }
        /// <summary>
        /// 文件上传方法
        /// </summary>
        /// <param name="postedFile">上传的文件</param>
        /// <param name="isthumbnail">是否生成缩略图</param>
        /// <param name="iswater">是否带水印</param>
        /// <returns>上传后文件信息</returns>
        public string fileSaveAs(HttpPostedFile postedFile, bool isthumbnail, bool iswater)
        {
            //我们的一般会对上传的文件进行处理，比如：判断类型，改名字

            try
            {
                //postedFile.FileName 得到的是不包含路径的名字，无法用Path类去获取名字
                string fileExt = FileHelp.GetFileExt(postedFile.FileName); //得到扩展名
                int fileSize = postedFile.ContentLength;//文件大小 字节单位
                string FileName = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf(@"\") + 1).Replace("." + fileExt, "");//原文件
                string randomCode = FileHelp.GetRamCode(); //随机数
                string newFileName = randomCode + "." + fileExt; //新文件名
                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名
                string uploadPath = GetUploadPath(); //上传相对路径
                string NewFilePath = uploadPath.TrimStart('~') + newFileName;//上传后的路径

                string newThumbnailPath = uploadPath + newThumbnailFileName;//缩略图路径
                //绝对路径，需要判断项目中是否存在
                string fullUpLoadPath = FileHelp.GetMapPath(uploadPath); //HttpContext.Current.Server.MapPath(uploadPath); 

                //检查文件扩展是否合法
                if (!CheckFileExt(_fileExt:fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, fileSize))
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小！\"}";
                }

                //检查上传的物理路径是否存在，不存在则创建
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }
                //保存的时候需要绝对路径
                postedFile.SaveAs(fullUpLoadPath + newFileName);

                if (IsVedio(fileExt.ToLower()))
                {
                    string mpegfile = NewFilePath;
                    string flvfile = uploadPath + randomCode + ".mp4";
                    try
                    {
                        string Extension = CheckExtension(fileExt);
                        if (Extension == "ffmpeg")
                        {
                            ChangeFilePhy(mpegfile, flvfile);
                        }
                        else if (Extension == "mencoder")
                        {
                            MChangeFilePhy(mpegfile, flvfile);
                        }
                    }
                    catch
                    {

                    }

                    //处理完毕，返回JOSN格式的文件信息
                    return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                        + FileName + "\", \"path\": \"" + NewFilePath + "\", \"thumb\": \""
                        + newThumbnailPath + "\", \"size\": " + fileSize + ", \"ext\": \"" + "flv" + "\"}";
                }
                else
                {
                    return "{\"status\":1,\"msg\":\"上传文件成功！\",\"name\":\"" + FileName
                   + "\",\"path\":\"" + NewFilePath + "\",\"thumb\":\"" + newThumbnailPath
                   + "\",\"size\":\"" + fileSize + "\",\"ext\":\"" + fileExt + "\"}";
                }   
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        #region 是否需要打水印
        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string _fileExt)
        {
            //判断是否开启水印
            if (0 > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList();
                al.Add("bmp");
                al.Add("jpeg");
                al.Add("jpg");
                al.Add("png");
                if (al.Contains(_fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 是否为图片类型
        /// <summary>
        /// 是否为图片类型
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        public bool IsImage(string _fileExt)
        {
            List<string> al = new List<string>() { "bmp", "jpeg", "jpg", "gif", "png" };
            if (al.Any(x => x == _fileExt))
            {
                return true;
            }
            return false;
        } 
        #endregion

        #region 是否为视频类型
        /// <summary>
        /// 是否为视频类型
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private bool IsVedio(string _fileExt)
        {
            List<string> al = new List<string>() { "mpeg", "avi", "mov", "wmv", "3gp", "rmvb", "mp4" };
            if (al.Contains(_fileExt))
            {
                return true;
            }
            return false;
        } 
        #endregion


        #region 检查扩展名是否合法
        /// <summary>
        /// 检查文件扩展名是否合法
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <returns></returns>
        private bool CheckFileExt(string _fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "ashx", "asa", "asmx", "asax", "php", "jsp", "htm", "html" };
            bool isFlag = excExt.Any(x => x.ToLower() == _fileExt.ToLower());
            if (isFlag)
            {
                return false;
            }
            //检查合法文件  这里可以进行过滤，把你不需要的类型可以不写在其中
            string[] allowExt = ("rar,zip,doc,docx,ppt,pptx,xls,xlsx" + "," + "flv,mp3,mp4,avi,rmvb,jpg,png,jpeg,ai,gif,bmp").Split(',');
            bool isF = allowExt.Any(x => x.ToLower() == _fileExt);
            if (isF)
            {
                return true;
            }
            return false;
        } 
        #endregion

        #region 检查文件大小是否合适
        /// <summary>
        /// 检查文件大小是否合适
        /// </summary>
        /// <param name="_fileExt"></param>
        /// <param name="_fileSize"></param>
        /// <returns></returns>
        private bool CheckFileSize(string _fileExt, int _fileSize)
        {
            //视频扩展名
            List<string> lsVideoExt = new List<string>() { "flv", "mp3", "mp4", "avi", "rmvb" };
            //判断是否为图片类型
            if (IsImage(_fileExt.ToLower()))
            {
                if (10240 > 0 && _fileSize > 10240 * 1024)
                {
                    return false;
                }
            }
            else if (lsVideoExt.Contains(_fileExt.ToLower()))
            {
                if (102400 > 0 && _fileSize > 1024000 * 1024)
                {
                    return false;
                }
            }
            else
            {
                if (51200 > 0 && _fileSize > 102400 * 1024)
                {
                    return false;
                }
            }
            return true;
        } 
        #endregion

        string[] strArrMencoder = new string[] { "wmv", "rmvb", "rm", "mp4" };
        string[] strArrFfmpeg = new string[] { "asf", "avi", "mpg", "3gp", "mov" };

        #region 获取文件类型
        /// <summary>
        /// 获取文件类型
        /// </summary>
        public string CheckExtension(string extension)
        {
            string m_strReturn = "";
            if (strArrFfmpeg.Contains(extension))
            {
                m_strReturn = "ffmpeg"; 
            }
            if (m_strReturn=="")
            {
                if (strArrMencoder.Contains(extension))
                {
                    m_strReturn = "mencoder";
                }
            }
            return m_strReturn;

            //foreach (string var in this.strArrFfmpeg)
            //{
            //    if (var == extension)
            //    {
            //        m_strReturn = "ffmpeg"; break;
            //    }
            //}
            //if (m_strReturn == "")
            //{
            //    foreach (string var in strArrMencoder)
            //    {
            //        if (var == extension)
            //        {
            //            m_strReturn = "mencoder"; break;
            //        }
            //    }
            //}
            //return m_strReturn;
        }
        #endregion


        #region 配置
        //public static string ffmpegtool = ConfigurationManager.AppSettings["ffmpeg"];
        //public static string mencodertool = ConfigurationManager.AppSettings["mencoder"];

        //public static string sizeOfImg = ConfigurationManager.AppSettings["CatchFlvImgSize"];
        //public static string widthOfFile = ConfigurationManager.AppSettings["widthSize"];
        //public static string heightOfFile = ConfigurationManager.AppSettings["heightSize"];



        public static string sizeOfImg = "1024*720";
        public static string widthOfFile = "1024";
        public static string heightOfFile = "720";
        #endregion


        #region 运行FFMpeg的视频解码(绝对路径)
        /// <summary>
        /// 转换文件并保存在指定文件夹下
        /// </summary>
        /// <param name="fileName">上传视频文件的路径（原文件）</param>
        /// <param name="playFile">转换后的文件的路径（网络播放文件）</param>
        /// <param name="imgFile">从视频文件中抓取的图片路径</param>
        /// <returns>成功:返回图片虚拟地址;失败:返回空字符串</returns>
        public string ChangeFilePhy(string fileName, string playFile)
        {
            string ffmpeg = HttpContext.Current.Server.MapPath("/ffmpeg/ffmpeg.exe");
            string vFileName = HttpContext.Current.Server.MapPath(fileName);
            string flv_file = HttpContext.Current.Server.MapPath(playFile);
            if ((!System.IO.File.Exists(ffmpeg)) || (!System.IO.File.Exists(vFileName)))
            {
                return "";
            }
            string FlvImgSize = sizeOfImg;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(ffmpeg);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //FilestartInfo.Arguments = " -i " + vFileName + " -ab 56 -ar 22050 -b 500 -r 15 -s " + widthOfFile + "x" + heightOfFile + " " + flv_file;
            FilestartInfo.Arguments = " -i \"" + vFileName + "\" -y -ab 32 -ar 22050 -b 800000 -s  480*360 \"" + flv_file + "\""; //Flv格式   
            try
            {
                System.Diagnostics.Process.Start(FilestartInfo);//转换
            }
            catch
            {
                return "";
            }
            return "";
        }

        #endregion


        #region 运行mencoder的视频解码器转换(绝对路径)
        /// <summary>
        /// 运行mencoder的视频解码器转换
        /// </summary>
        public string MChangeFilePhy(string fileName, string playFile)
        {
            string tool = HttpContext.Current.Server.MapPath("/ffmpeg/mencoder.exe");
            string vFileName = HttpContext.Current.Server.MapPath(fileName);
            string flv_file = HttpContext.Current.Server.MapPath(playFile);
            if ((!System.IO.File.Exists(tool)) || (!System.IO.File.Exists(vFileName)))
            {
                return "";
            }

            string FlvImgSize = sizeOfImg;
            System.Diagnostics.ProcessStartInfo FilestartInfo = new System.Diagnostics.ProcessStartInfo(tool);
            FilestartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            FilestartInfo.Arguments = " " + vFileName + " -o " + flv_file + " -of lavf -oac mp3lame -lameopts abr:br=56 -ovc lavc -lavcopts vcodec=flv:vbitrate=200:mbd=2:mv0:trell:v4mv:cbp:last_pred=1:dia=-1:cmp=0:vb_strategy=1 -vf scale=" + widthOfFile + ":" + heightOfFile + " -ofps 12 -srate 22050";
            try
            {
                System.Diagnostics.Process.Start(FilestartInfo);
            }
            catch
            {
                return "";
            }
            return "";
        }
        #endregion


        #region 相对路径
        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <returns></returns>
        private string GetUploadPath()
        {
            string path = "~/upload" + "/";//站点目录+上传目录
            switch (2)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default://按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        #endregion

        #region 获取文件的名字
        /// <summary>
        /// 获取文件的名字
        /// </summary>
        public static string GetFileName(string fileName)
        {
            int i = fileName.LastIndexOf("\\") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }
        #endregion

        #region 获取文件扩展名
        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        public static string GetExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".") + 1;
            string Name = fileName.Substring(i);
            return Name;
        }
        #endregion


    }



}
