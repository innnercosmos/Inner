using Inner.IRepository;
using Inner.IServices;
using Inner.Model.Models;
using Inner.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Inner.Services.BASE;
namespace Inner.Services
{
    public class SysUserService :BaseServices<SysUser> ,ISysUserService
    {
        //    ISysUserRepository dal = new SysUserRepository();

        //    public int Add(SysUser model)
        //    {
        //        return dal.Add(model);
        //    }

        //    public bool Delete(SysUser model)
        //    {
        //        return dal.Delete(model);
        //    }

        //    public List<SysUser> Query(Expression<Func<SysUser, bool>> whereExpression)
        //    {
        //        return dal.Query(whereExpression);
        //    }

        //    public int Sum(int i, int j)
        //    {
        //        return dal.Sum(i, j);
        //    }

        //    public bool Update(SysUser model)
        //    {
        //        return dal.Update(model);
        //    }

    }
}
