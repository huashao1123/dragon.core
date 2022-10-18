using Dragon.Core.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Dragon.Core.Common;

namespace Dragon.Core.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly IUintOfWork _uow;
        internal readonly DbContext _dbContext;
        //private readonly DbSet<TEntity> _dbSet;
        //private DbSet<TEntity> dbSet;

        public IQueryable<TEntity> Table => _dbSet;

        public IQueryable<TEntity> TableNoTracking => _dbSet.AsNoTracking();

        protected virtual DbSet<TEntity> _dbSet =>  _dbContext.Set<TEntity>();

        public BaseRepository(IUintOfWork uintOfWork)
        {
            _uow = uintOfWork;
            _dbContext=_uow.GetDbContext();
           // _dbSet=_dbContext.Set<TEntity>();

        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities=await _dbSet.Where(predicate).ToListAsync(cancellationToken);
            await DeleteManyAsync(entities,autoSave,cancellationToken);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
        }

        public  Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbSet.Where(predicate).SingleOrDefaultAsync(cancellationToken);
        }

        public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
           return  _dbSet.LongCountAsync(cancellationToken);
        }

        public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _dbSet.Where(predicate).LongCountAsync(cancellationToken);
        }

        public Task<TEntity?> GetEntityAsync(object Id, CancellationToken cancellationToken = default)
        {
            return _dbSet.FindAsync(new object[] { Id },cancellationToken).AsTask();
        }
        public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return _dbSet.ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
        {
           return _dbSet.OrderBy(sorting).Skip(skipCount).Take(maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<PageModel<TEntity>> GetPageEntitiesAsync<S>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderByLambda, bool isAsc = true, int pageSize = 20, int pageIndex = 1, CancellationToken cancellationToken = default)
        {
            int total = await _dbSet.Where(whereLambda).CountAsync(cancellationToken);
            List<TEntity> temp = null;
            if (isAsc)
            {
                temp = await _dbSet.Where(whereLambda)
                             .OrderBy<TEntity, S>(orderByLambda)
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize).ToListAsync(cancellationToken);
            }
            else
            {
                temp = await _dbSet.Where(whereLambda)
                               .OrderByDescending<TEntity, S>(orderByLambda)
                               .Skip(pageSize * (pageIndex - 1))
                               .Take(pageSize).ToListAsync(cancellationToken);
            }
            int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(total) / Convert.ToDecimal(pageSize)));
            return new PageModel<TEntity>()
            { Total = total, page = pageIndex, PageSize = pageSize, TotalPages = pageCount, Items = temp, HasNextPage = pageIndex < pageCount, HasPrevPage = pageIndex - 1 > 0 };
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var saveEntity= (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
            return saveEntity;
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities,cancellationToken);
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.Attach(entity);
            var updateEntity=_dbSet.Update(entity).Entity;
            
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
            return updateEntity;
        }
        /// <summary>
        /// 更新部分列或者忽略部分列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="isIgnoreCol">当properties有值时，isIgnoreCol为true为忽略更新</param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public async Task<int> UpdateNotQueryAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default,bool isIgnoreCol= false, params Expression<Func<TEntity, object>>[] properties)
        {
            var dbEntityEntry = _dbContext.Entry<TEntity>(entity);
            if (properties.Any())
            {
                if (isIgnoreCol)
                {
                    dbEntityEntry.State = EntityState.Modified;
                }
                foreach (var property in properties)
                {
                    dbEntityEntry.Property(property).IsModified = !isIgnoreCol;
                }
            }
            else
            {
                dbEntityEntry.State=EntityState.Modified;
            }
            int updateCount = 0;
            if (autoSave)
            {
                updateCount=await SaveAsync(cancellationToken);
            }
            return updateCount;
        }



        public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            _dbSet.AttachRange(entities);
            _dbSet.UpdateRange(entities);
            if (autoSave)
            {
                await SaveAsync(cancellationToken);
            }
        }
    }
}
