using Dragon.Core.Common;
using Dragon.Core.IRepository;
using Dragon.Core.IService;
using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository=baseRepository;
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _baseRepository.DeleteAsync(entity, autoSave, cancellationToken);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _baseRepository.DeleteAsync(predicate, autoSave, cancellationToken);
        }

        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _baseRepository.DeleteManyAsync(entities, autoSave, cancellationToken);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _baseRepository.FindAsync(predicate, cancellationToken);
        }

        public Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return _baseRepository.GetCountAsync(cancellationToken);
        }

        public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return _baseRepository.GetCountAsync(predicate, cancellationToken);
        }

        public Task<TEntity?> GetEntityAsync(object Id, CancellationToken cancellationToken = default)
        {
            return _baseRepository.GetEntityAsync(Id, cancellationToken);
        }

        public Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return _baseRepository.GetListAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return _baseRepository.GetListAsync(predicate, cancellationToken);
        }

        public Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default)
        {
            return _baseRepository.GetPagedListAsync(skipCount, maxResultCount, sorting, cancellationToken);
        }

        public async Task<PageModel<TEntity>> GetPageEntitiesAsync<S>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderByLambda, bool isAsc = true, int pageSize = 20, int pageIndex = 1, CancellationToken cancellationToken = default)
        {
            return await _baseRepository.GetPageEntitiesAsync(whereLambda, orderByLambda,isAsc,pageSize,pageIndex,cancellationToken);
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return await _baseRepository.InsertAsync(entity, autoSave, cancellationToken);
        }

        public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _baseRepository.InsertManyAsync(entities, autoSave, cancellationToken);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            return await _baseRepository.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default)
        {
            await _baseRepository.UpdateManyAsync(entities, autoSave, cancellationToken);
        }

        public async Task<int> UpdateNotQueryAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default, bool isIgnoreCol = false, params Expression<Func<TEntity, object>>[] properties)
        {
           return await _baseRepository.UpdateNotQueryAsync(entity,autoSave,cancellationToken,isIgnoreCol,properties);
        }
    }
}
