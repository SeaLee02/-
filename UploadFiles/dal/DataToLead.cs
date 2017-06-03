
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using System.Collections.Generic;

namespace FunctionDemo.dal
{
    /// <summary>
    /// 数据访问类:DataToLead
    /// </summary>
    public partial class DataToLead
    {
        public DataToLead()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DataToLead");
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
        public int Add(Model.DataToLead model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DataToLead(");
            strSql.Append("Name,Sex,Age,Tel,Remark)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Sex,@Age,@Tel,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sex", SqlDbType.VarChar,10),
                    new SqlParameter("@Age", SqlDbType.Int,4),
                    new SqlParameter("@Tel", SqlDbType.NVarChar,200),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.Remark;

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
        /// 批量添加数据数据
        /// </summary>
        public int Add(List<Model.DataToLead> model)
        {

            List<CommandInfo> strsql = new List<CommandInfo>();
            foreach (Model.DataToLead item in model)
            {
                CommandInfo info = new CommandInfo();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into DataToLead(");
                strSql.Append("Name,Sex,Age,Tel,Remark)");
                strSql.Append(" values (");
                strSql.Append("@Name,@Sex,@Age,@Tel,@Remark)");
                strSql.Append(";select @@IDENTITY");
                info.CommandText = strSql.ToString();
                SqlParameter[] parameters = {
                        new SqlParameter("@Name", SqlDbType.NVarChar,50),
                        new SqlParameter("@Sex", SqlDbType.VarChar,10),
                        new SqlParameter("@Age", SqlDbType.Int,4),
                        new SqlParameter("@Tel", SqlDbType.NVarChar,200),
                        new SqlParameter("@Remark", SqlDbType.NVarChar,200)};
                parameters[0].Value = item.Name;
                parameters[1].Value = item.Sex;
                parameters[2].Value = item.Age;
                parameters[3].Value = item.Tel;
                parameters[4].Value = item.Remark;
                info.Parameters = parameters;
                strsql.Add(info);
            }
            int num = DbHelperSQL.ExecuteSqlTran(strsql);
            if (num > 0)
            {
                return num;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.DataToLead model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DataToLead set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("Age=@Age,");
            strSql.Append("Tel=@Tel,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Sex", SqlDbType.VarChar,10),
                    new SqlParameter("@Age", SqlDbType.Int,4),
                    new SqlParameter("@Tel", SqlDbType.NVarChar,200),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,200),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Sex;
            parameters[2].Value = model.Age;
            parameters[3].Value = model.Tel;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.ID;

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
            strSql.Append("delete from DataToLead ");
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
            strSql.Append("delete from DataToLead ");
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
        public Model.DataToLead GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,Name,Sex,Age,Tel,Remark from DataToLead ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Model.DataToLead model = new Model.DataToLead();
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
        public Model.DataToLead DataRowToModel(DataRow row)
        {
            Model.DataToLead model = new Model.DataToLead();
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
                if (row["Sex"] != null)
                {
                    model.Sex = row["Sex"].ToString();
                }
                if (row["Age"] != null && row["Age"].ToString() != "")
                {
                    model.Age = int.Parse(row["Age"].ToString());
                }
                if (row["Tel"] != null)
                {
                    model.Tel = row["Tel"].ToString();
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
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
            strSql.Append("select ID,Name,Sex,Age,Tel,Remark ");
            strSql.Append(" FROM DataToLead ");
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
            strSql.Append(" ID,Name,Sex,Age,Tel,Remark ");
            strSql.Append(" FROM DataToLead ");
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
            strSql.Append("select count(1) FROM DataToLead ");
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
            strSql.Append(")AS Row, T.*  from DataToLead T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "DataToLead";
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

