using Inner.IRepository;
using Inner.Model.Models;
using Inner.Model.ViewModels;
using Inner.Repository.sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Inner.Repository
{
    public class SysUserRepository : ISysUserRepository
    {

        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<SysUser> entityDB;

        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public SysUserRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<SysUser>(db);
        }
        public int Add(SysUser model)
        {
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            var i = db.Insertable(model).ExecuteReturnBigIdentity();
            return i.ObjToInt();
        }

        public bool Delete(SysUser model)
        {
            var i = db.Deleteable(model).ExecuteCommand();
            return i > 0;
        }

        public SysUserDto GetUserInfoById(string id)
        {
            throw new NotImplementedException();
        }

        public List<SysUser> Query(Expression<Func<SysUser, bool>> whereExpression)
        {
            return entityDB.GetList(whereExpression);
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public bool Update(SysUser model)
        {
            //这种方式会以主键为条件
            var i = db.Updateable(model).ExecuteCommand();
            return i > 0;
        }
    }
}
