<%@ WebHandler Language="C#" Class="RegisterHandler" %>

using System;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Configuration;

public class RegisterHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        JavaScriptSerializer java = new JavaScriptSerializer();
        string type = context.Request.Form["type"] == null ? "" : context.Request.Form["type"].ToString();
        if (type == "GetCode")
        {
            string Phone = context.Request.Form["Phone"] == null ? "" : context.Request.Form["Phone"].ToString();
            Random ran = new Random();
            string strNumber = ran.Next(1000, 9999).ToString();  //你的验证码
            string a = Send(Phone, strNumber, context);
            //  s格式：sms&stat=100&message=发送成功
            string[] strsplit = a.Split('&');
            string[] s = strsplit[strsplit.Length - 2].Split('=');
            int num = Convert.ToInt32(s[s.Length - 1]);
            if (num == 100)
            {
                context.Response.Write(java.Serialize(1 + "-" + strNumber).Trim('"'));//发送成功
            }
            else
            {
                context.Response.Write(java.Serialize(2 + "-" + 000).Trim('"'));//发送失败
            }
        }

        context.Response.Write(java.Serialize(""));//发送失败
    }

    /// <summary>
    /// 发送信息    这个方法官网里面模板，自己自己去下载
    /// </summary>
    /// <param name="iPhone">电话号码</param>
    /// <param name="Num">随机数</param>
    /// <param name="context"></param>
    /// <returns></returns>
    private string Send(string iPhone, string Num, HttpContext context)
    {
        //官网-->www.sms.cn
        string sendurl = "http://api.sms.cn/sms/";
        //这个文本格式需要自己在平台里面设置模板，格式必须为一样的  
        string content = "您的验证码为："+Num+", 5分钟有效。【SeaLee】"; 
        string mobile = iPhone;
        StringBuilder sbTemp = new StringBuilder();
        //你的账号密码，去官网注册免费试用，会有10条免费的短信给你做测试
        string uid = ""; //ConfigurationManager.AppSettings["uid"].ToString();
        string pwd = "";// ConfigurationManager.AppSettings["pwd"].ToString();

        string Pass = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd + uid, "MD5");  //应要求传的pwd必须是uid和pwd利用md5加密

        // 全文变量
        sbTemp.Append("ac=send&uid=" + uid + "&pwd=" + Pass + "&mobile=" + mobile + "&content=" + content + "&format=txt");

        //json格式变量  {"code":"value"}
        //sbTemp.Append("ac=send&uid=" + uid + "&pwd=" + Pass + "&template=402086&mobile=" + mobile + "&content={\"code\":\""+Num+"\"}&format=txt");


        byte[] bTemp = Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());   //我们传递的参数编个码，不然出现中发给别人的就是乱码

        string postReturn = doPostRequest(sendurl, bTemp); //利用httprequest去访问网址来获取消息
        return postReturn;
    }

    /// <summary>
    ///访问url把参数传过去，给注册者发信息 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="bData"></param>
    /// <returns></returns>
    private static String doPostRequest(string url, byte[] bData)
    {
        HttpWebRequest hwRequest;
        HttpWebResponse hwResponse;
        string result = string.Empty;
        try
        {
            hwRequest = (HttpWebRequest)WebRequest.Create(url);
            hwRequest.Timeout = 5000;//有效时间为5分钟
            hwRequest.Method = "POST";
            hwRequest.ContentType = "application/x-www-form-urlencoded";
            hwRequest.ContentLength = bData.Length;
            Stream strem = hwRequest.GetRequestStream();
            strem.Write(bData, 0, bData.Length);  //把我们的参数以流的形式写人
            strem.Close();
        }
        catch (Exception mess)
        {
            return result;
        }
        try
        {
            hwResponse = (HttpWebResponse)hwRequest.GetResponse();
            StreamReader sr = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII); //获取返回来的流
            result = sr.ReadToEnd();  //
            sr.Close();
            hwResponse.Close();
        }
        catch (Exception err)
        {
            return result;
        }
        return result;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}