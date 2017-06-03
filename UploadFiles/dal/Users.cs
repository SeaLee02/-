using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DBUtility;
using FunctionDemo.Model;

namespace FunctionDemo.DAL
{
    /// <summary>
    /// 数据访问类:Users
    /// </summary>
    public partial class Users
    {
        public Users()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在信息   利用可选参数来写，不用在写方法的重载
        /// </summary>
        /// <param name="ID">主键</param>
        /// <param name="LoginName">登入名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        public bool Exists(int ID,string LoginName, string PassWord)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Users");
            strSql.Append(" where ");
            SqlParameter[] parameters;
            if (ID != 0)
            {
                strSql.Append(" ID=@ID ");
                parameters = new SqlParameter[] { new SqlParameter("@ID", SqlDbType.Int, 4) };
                parameters[0].Value = ID;
            }
            else
            {
                if (PassWord == "")
                {
                    strSql.Append(" LoginName=@LoginName ");
                    parameters = new[] { new SqlParameter("@LoginName", SqlDbType.NVarChar, 50) };
                    parameters[0].Value = LoginName;
                }
                else
                {
                    strSql.Append("  LoginName=@LoginName and PassWord=@PassWord");
                    parameters = new[] {
                                    new SqlParameter("@LoginName", SqlDbType.NVarChar, 50),
                                    new SqlParameter("@PassWord", SqlDbType.NVarChar,50)
                             };
                    parameters[0].Value = LoginName;
                    parameters[1].Value = PassWord;
                }
            }
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(int ID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from Users");
        //    strSql.Append(" where ID=@ID");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4)
        //    };
        //    parameters[0].Value = ID;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

        ///// <summary>
        ///// 是否存在用户名
        ///// </summary>
        //public bool Exists(string LoginName)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from Users");
        //    strSql.Append(" where LoginName=@LoginName");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@LoginName",  SqlDbType.NVarChar,50)
        //    };
        //    parameters[0].Value = LoginName;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

        ///// <summary>
        ///// 用户名和密码是否正确
        ///// </summary>
        ///// <param name="LoginName"></param>
        ///// <returns></returns>
        //public bool Exists(string LoginName, string PassWord)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) from Users");
        //    strSql.Append(" where LoginName=@LoginName and PassWord=@PassWord");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@LoginName",  SqlDbType.NVarChar,50),
        //            new SqlParameter("@PassWord", SqlDbType.NVarChar,50)
        //    };
        //    parameters[0].Value = LoginName;
        //    parameters[1].Value = PassWord;

        //    return DbHelperSQL.Exists(strSql.ToString(), parameters);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Users(");
            strSql.Append("LoginName,UserName,PassWord)");
            strSql.Append(" values (");
            strSql.Append("@LoginName,@UserName,@PassWord)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PassWord", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.LoginName;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.PassWord;

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
        public bool Update(Model.Users model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Users set ");
            strSql.Append("LoginName=@LoginName,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("PassWord=@PassWord");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PassWord", SqlDbType.NVarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.LoginName;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.PassWord;
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
            strSql.Append("delete from Users ");
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
            strSql.Append("delete from Users ");
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
        public Model.Users GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LoginName,UserName,PassWord from Users ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            Model.Users model = new Model.Users();
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
        public Model.Users GetModel(string LoginName,string PassWord)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LoginName,UserName,PassWord from Users ");
            strSql.Append(" where LoginName=@LoginName and PassWord=@PassWord");
            SqlParameter[] parameters = {
                    new SqlParameter("@LoginName", SqlDbType.NVarChar,50),
                     new SqlParameter("@PassWord", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = LoginName;
            parameters[1].Value = PassWord;

            Model.Users model = new Model.Users();
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
        public Model.Users DataRowToModel(DataRow row)
        {
            Model.Users model = new Model.Users();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["LoginName"] != null)
                {
                    model.LoginName = row["LoginName"].ToString();
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["PassWord"] != null)
                {
                    model.PassWord = row["PassWord"].ToString();
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
            strSql.Append("select ID,LoginName,UserName,PassWord ");
            strSql.Append(" FROM Users ");
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
            strSql.Append(" ID,LoginName,UserName,PassWord ");
            strSql.Append(" FROM Users ");
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
            strSql.Append("select count(1) FROM Users ");
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
            strSql.Append(")AS Row, T.*  from Users T ");
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
			parameters[0].Value = "Users";
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
