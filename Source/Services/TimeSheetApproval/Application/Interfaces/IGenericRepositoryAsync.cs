using System;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;

namespace TimeSheetApproval.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<PaginatedResult<T>> GetPaginatedResultsByFiltersAsync(SearchFilters filter);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> IsRecordExistForString(string ColName, string colValue);
        Task<T> GetRecordByColumn(string ColName, string colValue);

    }
    public class PaginatedResult<T>
    {
        public int TotalPages { get; private set; }
        public int TotalRecordsCount { get; private set; }
        public IQueryable<T> PagedList { get; private set; }
        public PaginatedResult(IQueryable<T> items, int count, int pageSize)
        {
            TotalRecordsCount = count;
            TotalPages = (pageSize == -1) ? 1 : (int)Math.Ceiling(count / (double)pageSize);
            this.PagedList = items;
        }

    }
}
