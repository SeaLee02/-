using System;
using System.Data;
using System.Collections.Generic;

namespace FunctionDemo.BLL
{
    /// <summary>
    /// Category
    /// </summary>
    public partial class Category
    {
        private readonly FunctionDemo.DAL.Category dal = new FunctionDemo.DAL.Category();
        public Category()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FunctionDemo.Model.Category model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FunctionDemo.Model.Category model)
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
        public FunctionDemo.Model.Category GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

    
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获取全部类别
        /// </summary>
        /// <param name="PID">父类ID</param>
        /// <param name="isState">是否加载禁用</param>
        /// <returns></returns>
        public DataTable GetListChild(int PID, bool isState)
        {
            return dal.GetListChild(PID, isState);
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
        public List<FunctionDemo.Model.Category> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FunctionDemo.Model.Category> DataTableToList(DataTable dt)
        {
            List<FunctionDemo.Model.Category> modelList = new List<FunctionDemo.Model.Category>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FunctionDemo.Model.Category model;
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
        public DataSet GetPageList(string strWhere, int PageSize, int PageIndex)
        {
            return dal.GetPageList(strWhere,PageSize,PageIndex);
        }
            #endregion  BasicMethod
            #region  ExtensionMethod

            #endregion  ExtensionMethod
        }
}

