using Dragon.Core.Data;
using Dragon.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Repository
{
    public class UintOfWork : IUintOfWork
    {
        /// <summary>
        /// 定义数据库上下文访问对象
        /// </summary>
        private readonly DbContext _dbContext;
        /// <summary>
        ///
        /// </summary>
        private IDbContextTransaction _currenTtransaction { get; set; }

        public bool HasCommited { get; private set; }
        public UintOfWork(EfDbContext dbContext)
        {
            _dbContext = dbContext;
            HasCommited = false;
        }

        public DatabaseFacade Database()
        {
            return this.GetDbContext().Database;
        }

        public IDbContextTransaction GetTransaction()
        {
            return this.Database().BeginTransaction();
        }

        public void BeginTransaction()
        {
            _currenTtransaction = GetTransaction();
        }

        public void Commit()
        {
            if (HasCommited)
            {
                return;
            }

            if (_currenTtransaction != null)
            {
                try
                {
                    _currenTtransaction.Commit();
                }
                catch (Exception e)
                {
                    _currenTtransaction.Rollback();

                    HasCommited = true;
                    throw e;
                }

            }
            HasCommited = true;
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public void RollbackTran()
        {
            if (HasCommited)
            {
                return;
            }
            _currenTtransaction.Rollback();
        }
    }
}
