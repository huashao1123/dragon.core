using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IRepository
{
    public interface IUintOfWork
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 得到实体上下文
        /// </summary>
        /// <returns></returns>
        DbContext GetDbContext();
        /// <summary>
        /// 开启事务
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTran();
    }
}
