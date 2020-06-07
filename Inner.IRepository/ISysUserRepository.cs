using Inner.Model.Models;
using Inner.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Inner.IRepository.BASE;
namespace Inner.IRepository
{
    public interface ISysUserRepository :IBaseRepository<SysUser>
    {
        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <param name="id">患者id</param>
        ///// <returns>患者信息</returns>
        //SysUserDto GetUserInfoById(string id);

        //int Sum(int i, int j);

        //int Add(SysUser model);
        //bool Delete(SysUser model);
        //bool Update(SysUser model);
        //List<SysUser> Query(Expression<Func<SysUser, bool>> whereExpression);
    }
}
