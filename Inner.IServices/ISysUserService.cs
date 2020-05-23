using Inner.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Inner.IServices
{
    public interface ISysUserService
    {
        int Sum(int i, int j);
        int Add(SysUser model);
        bool Delete(SysUser model);
        bool Update(SysUser model);
        List<SysUser> Query(Expression<Func<SysUser, bool>> whereExpression);
    }
}
