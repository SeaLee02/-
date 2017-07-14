using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using FunctionDemo.Model;

namespace FunctionDemo.DAL
{
    /// <summary>
    /// 数据访问类:Category
    /// </summary>
    public partial class Category
    {
        public Category()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Category");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Category(");
            strSql.Append("Name,Pid,LevalNum)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Pid,@LevalNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@Pid", SqlDbType.Int,4),
                    new SqlParameter("@LevalNum", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Pid;
            parameters[2].Value = model.LevalNum;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Category set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Pid=@Pid,");
            strSql.Append("LevalNum=@LevalNum");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,100),
                    new SqlParameter("@Pid", SqlDbType.Int,4),
                    new SqlParameter("@LevalNum", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Pid;
            parameters[2].Value = model.LevalNum;
            parameters[3].Value = model.ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Category ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Category ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Category GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,Pid,LevalNum from Category ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Model.Category model = new Model.Category();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Category DataRowToModel(DataRow row)
        {
            Model.Category model = new Model.Category();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Name"] != null)
                {
                    model.Name = row["Name"].ToString();
                }
                if (row["Pid"] != null && row["Pid"].ToString() != "")
                {
                    model.Pid = int.Parse(row["Pid"].ToString());
                }
                if (row["LevalNum"] != null && row["LevalNum"].ToString() != "")
                {
                    model.LevalNum = int.Parse(row["LevalNum"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,Pid,LevalNum ");
            strSql.Append(" FROM Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,Name,Pid,LevalNum ");
            strSql.Append(" FROM Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from Category T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取全部类别
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetListChild(int PID, bool isState)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Name,Pid,LevalNum");
            strSql.Append(" FROM Category ");
            //strSql.Append(" Where ParentId=" + PId);
            if (isState)
            {
                strSql.Append("Where 1=1 ");
            }
            strSql.Append(" order by Id asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChannelChild(oldData, newData, PID);

            return newData;
        }


        /// <summary>
        /// 获取子类别
        /// </summary>
        /// <param name="parentId">父编号</param>
        /// <param name="stateInfo">状态</param>
        /// <returns></returns>
        private void GetChannelChild(DataTable oldData, DataTable newData, int PId)
        {
            //第一遍进来的时候是获取父类 pid=0
            DataRow[] dr = oldData.Select("Pid=" + PId);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["ID"] = int.Parse(dr[i]["ID"].ToString());
                row["Name"] = dr[i]["Name"].ToString();
                row["Pid"] = dr[i]["Pid"].ToString();
                row["LevalNum"] = dr[i]["LevalNum"].ToString();               
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChannelChild(oldData, newData, int.Parse(dr[i]["ID"].ToString()));
                //把父类的ID作为条件传进去，子类的数据就会出来
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetPageList(string strWhere,int PageSize,int PageIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top "+PageSize+" ID,Name,Pid,LevalNum ");
            strSql.Append(" FROM Category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere+ " and ID not in (select top "+PageSize*PageIndex+" ID from Category where "+strWhere+" )");
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Category";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

