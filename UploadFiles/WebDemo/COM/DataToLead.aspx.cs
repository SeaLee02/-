using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Aspose.Cells;
using System.Data;


namespace WebDemo.COM
{
    public partial class DataToLead : System.Web.UI.Page
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
                BindDemo();
            }
        }

        private void BindDemo()
        {
            DemoRepeater.DataSource = new FunctionDemo.BLL.DataToLead().GetList("").Tables[0];
            DemoRepeater.DataBind();

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSave_Click(object sender, EventArgs e)
        {
            string errorInfo = "";
            int errorCount = 0;
            List<FunctionDemo.Model.DataToLead> dtlList = new List<FunctionDemo.Model.DataToLead>();
            string FullName = Server.MapPath(txtFileUrl.Text);
            FileInfo fo = new FileInfo(FullName);
            using (FileStream fs = fo.OpenRead())
            {
                Workbook workbook = new Workbook(fs);//构造函数
                //workbook.Open(fs);  无法智能调用次方法，除非智能写  
                Cells cells = workbook.Worksheets[0].Cells;//取到所有的列
                //cells.MaxRow,这是所有的行。而cells.MaxDataRow是数据行，不包括头                        cells.MaxColumn  列无所谓
                DataTable dt = cells.ExportDataTable(0, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1, true);//把列输出成表格 
                if (dt.Rows.Count > 0)//证明有数据
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //如果需要做判断可以在这里做，比如某些字段不能为空
                        if (dt.Rows[i]["姓名"] != null)
                        {
                            FunctionDemo.Model.DataToLead dtlModel = new FunctionDemo.Model.DataToLead();
                            dtlModel.Name = dt.Rows[i]["姓名"].ToString();
                            dtlModel.Sex = dt.Rows[i]["性别"].ToString();
                            dtlModel.Age = Convert.ToInt32(dt.Rows[i]["年龄"]);
                            dtlModel.Tel = dt.Rows[i]["电话号码"].ToString();
                            dtlList.Add(dtlModel);
                        }
                        else
                        {
                            errorCount++;
                            errorInfo += "\r\n第" + (i + 1) + "行提交失败，因为名称为空或为空！";
                        }
                    }
                }
            }
            if (dtlList.Count > 0)
            {
                if (new FunctionDemo.BLL.DataToLead().Add(dtlList) > 0)
                {
                    string msg = "导入成功！";
                    if (errorCount > 0)
                    {
                        msg = msg + errorInfo;
                    }
                    Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('" + msg + "'); </script>");
                }
                else
                {
                    string msg = "导入失败！";
                    if (errorCount > 0)
                    {
                        msg = msg + errorInfo;
                    }
                    Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('" + msg + "'); </script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('导入失败，因为模板中无数据！'); </script>");
            }
            BindDemo();
        }
    }
}