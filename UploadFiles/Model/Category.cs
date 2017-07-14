using System;
namespace FunctionDemo.Model
{
    /// <summary>
    /// Category:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Category
    {
        public Category()
        { }
        #region Model
        private int _id;
        private string _name;
        private int _pid;
        private int _levalnum;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LevalNum
        {
            set { _levalnum = value; }
            get { return _levalnum; }
        }
        #endregion Model

    }
}

