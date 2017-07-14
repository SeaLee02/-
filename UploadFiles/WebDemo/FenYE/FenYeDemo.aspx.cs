using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo.FenYE
{
    public partial class FenYeDemo : System.Web.UI.Page
    {
        public int RowIndex = 1;
        public int page=0;
        public int pcount=0;
        public int pagesize=2;

        public string strUrl="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = Request.QueryString["page"]==null?0: Convert.ToInt32(Request.QueryString["page"].ToString());
                pcount = new FunctionDemo.BLL.Category().GetRecordCount("1=1");
                repList.DataSource = new FunctionDemo.BLL.Category().GetPageList("1=1", pagesize, page);
                repList.DataBind();

            }
        }
    }
}