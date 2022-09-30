using Dragon.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dragon.Core.IService
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

        Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

        Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default);

        Task DeleteAsync(TEntity entity, bool autoSave = true, CancellationToken cancellationToken = default);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = true, CancellationToken cancellationToken = default);

        Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = true, CancellationToken cancellationToken = default);

        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<TEntity?> GetEntityAsync(object Id, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, CancellationToken cancellationToken = default);

        Task<PageModel<TEntity>> GetPageEntitiesAsync<S>(
                                          Expression<Func<TEntity, bool>> whereLambda,
                                          Expression<Func<TEntity, S>> orderByLambda,
                                          bool isAsc = true, int pageSize = 20, int pageIndex = 1, CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}
