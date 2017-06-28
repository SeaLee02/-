using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Cells;
using System.Data;

namespace WebDemo.COM
{
    public partial class DataToOut : System.Web.UI.Page
    {
        public int RowIndex
        {
            get
            {
                object o = ViewState["RowIndex"];
                return o == null ? 1 : Convert.ToInt32(o);
            }
            set { ViewState["RowIndex"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bings();
            }
        }

        private void Bings()
        {
            DemoRepeater.DataSource = new FunctionDemo.BLL.DataToLead().GetList("").Tables[0];
            DemoRepeater.DataBind();
        }

        protected void lnkData_Click(object sender, EventArgs e)
        {
            DataTable ds = new FunctionDemo.BLL.DataToLead().GetList(" ID in (" + hfID.Value + ")").Tables[0];
            if (ds.Rows.Count > 0)
            {
                //创建一个excel表格
                Workbook workbook = new Workbook();
                Worksheet sheet = workbook.Worksheets[0] as Worksheet; //工作薄
                Cells cells = sheet.Cells;//取到所以的列
                //然后往cell里面插入数据  
                sheet.FreezePanes(1, 1, 1, 0);//冻结第一行
                cells[0, 0].PutValue("姓名");
                cells[0, 1].PutValue("性别");
                cells[0, 2].PutValue("年龄");
                cells[0, 3].PutValue("电话号码");
                cells[0, 4].PutValue("标记");

                Aspose.Cells.Style style1 = workbook.Styles[workbook.Styles.Add()]; //设置样式  然后进行设置
                style1.HorizontalAlignment = Aspose.Cells.TextAlignmentType.Center;//居中
                //列宽
                cells.SetColumnWidth(0, 20);
                cells.SetColumnWidth(1, 20);
                cells.SetColumnWidth(2, 20);
                cells.SetColumnWidth(3, 50);
                cells.SetColumnWidth(4, 20);
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    cells.SetRowHeight(i, 20); //行高
                    //赋值和样式
                    cells[1 + i,0].PutValue(ds.Rows[i]["Name"].ToString());
                    cells[1 + i,0].SetStyle(style1);
                    cells[1 + i,1].PutValue(ds.Rows[i]["Sex"].ToString());
                    cells[1 + i,1].SetStyle(style1);
                    cells[1 + i,2].PutValue(ds.Rows[i]["Age"].ToString());
                    cells[1 + i,2].SetStyle(style1);
                    cells[1 + i,3].PutValue(ds.Rows[i]["Tel"].ToString());
                    cells[1 + i,3].SetStyle(style1);
                    cells[1 + i,4].PutValue(ds.Rows[i]["Remark"].ToString());
                    cells[1 + i,4].SetStyle(style1);
                }
                Aspose.Cells.Style style = workbook.Styles[workbook.Styles.Add()]; //设置样式
                style.HorizontalAlignment = TextAlignmentType.Center;//居中
                style.ForegroundColor = System.Drawing.Color.Khaki;//背景样式
                style.Pattern = BackgroundType.Solid; //枚举类型  模式 实线
                cells[0,0].SetStyle(style); //第一行设计样式  头
                cells[0, 1].SetStyle(style); 
                cells[0, 2].SetStyle(style); 
                cells[0, 3].SetStyle(style); 
                cells[0, 4].SetStyle(style); 
                System.IO.MemoryStream ms = workbook.SaveToStream();//生成流
                byte[] by = ms.ToArray();//生成字节好下载

                string fileName = "导出数据" + DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";//文件名
                Response.ContentType = "application/octet-stream";//文件下载
                //以字节流形式下载excel   上面下面两种类型都可以
                //Response.ContentType = "application/vnd.ms-excel";
                //编码 
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName,System.Text.Encoding.UTF8));
                Response.BinaryWrite(by);
                Response.Flush();
                Response.End();
            }
        }
    }
}