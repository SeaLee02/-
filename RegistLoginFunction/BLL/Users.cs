using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FunctionDemo.BLL
{
    /// <summary>
	/// Users
	/// </summary>
	public partial class Users
    {
        private readonly DAL.Users dal = new DAL.Users();
        public Users()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录  这里使用的是可选参数，  
        /// 这一个方法就可以当三个方法用，用的时候配合命名参数一起使用，不给值的时候就是默认值
        /// </summary>
        public bool Exists(int ID=0,string LoginName="",string PassWord="")
        {
            return dal.Exists(ID,LoginName,PassWord);
        }
        ///// <summary>
        ///// 是否存在用户名
        ///// </summary>
        ///// <param name="LoginName"></param>
        ///// <returns></returns>
        //public bool Exists(string LoginName)
        //{
        //    return dal.Exists(LoginName);
        //}
        ///// <summary>
        ///// 用户名和密码是否正确
        ///// </summary>
        ///// <param name="LoginName"></param>
        ///// <returns></returns>
        //public bool Exists(string LoginName,string PassWord)
        //{
        //    return Exists(LoginName,PassWord);
        //}
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Users model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Users GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Users GetModel(string LoginName,string PassWord)
        {

            return dal.GetModel(LoginName,PassWord);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Users> DataTableToList(DataTable dt)
        {
            List<Model.Users> modelList = new List<Model.Users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
