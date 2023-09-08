using Application.Common.Models;
using System.Linq.Expressions;

namespace Application.Common.Interfaces.EntityRepositories
{
    public interface IRepository<T>  where T : class
    {
        Task<RespDto<bool>> CreateAsync(T entity);
        Task<RespDto<bool>> CreateListAsync(IEnumerable<T> entities);
        Task<RespDto<bool>> DeleteAsync(int id);
        Task<RespDto<bool>> DeleteRangeAsync(IEnumerable<T> entities);
        Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter);
        Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter, string[] includedList = null);
        Task<RespDto<IEnumerable<T>>> FindAllAsync(Expression<Func<T, bool>> filter, string[] includedList = null, int take = 1000, int offset = 0);
        Task<RespDto<T>> FindByIdAsync(int id);
        Task<RespDto<List<T>>> GetAllAsync();
        Task<T?> FindFirstOrDefaultAsync(Expression<Func<T, bool>> filter);
        Task<RespDto<bool>> UpdateAsync(object id, T entity);
        Task<bool> HasAny();
        Task<bool> HasAny(Expression<Func<T, bool>> filter);
        Task<RespDto<bool>> HasAnyWithMessage(Expression<Func<T, bool>> filter);
    }
}