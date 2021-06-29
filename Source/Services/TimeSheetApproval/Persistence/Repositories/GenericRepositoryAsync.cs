using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Persistence.Contexts;
namespace TimeSheetApproval.Persistence.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        public GenericRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get Entity Details by passing Id(Primary Key Column value)
        /// Finds an entity with the given primary key values. 
        /// If an entity with the given primary key values is being 
        /// tracked by the context, then it is returned immediately 
        /// without making a request to the database. Otherwise, a 
        /// query is made to the database for an entity with the 
        /// given primary key values and this entity, if found, is 
        /// attached to the context and returned. If no entity is 
        /// found, then null is returned
        /// </summary>
        /// <param name="id"></param>
        /// <returns>returns entity</returns>
        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        /// <summary>
        /// Method for applying filters dynamically for an 
        /// entity using single or multiple properties of an entity.
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Entity as PaginatedResult</returns>
        public async Task<PaginatedResult<T>> GetPaginatedResultsByFiltersAsync(SearchFilters filter)
        {
            var entity = _dbContext.Set<T>().ToFilterView(filter);
            var totalRecords = filter.TotalCount;
            PaginatedResult<T> pagedresponse = new PaginatedResult<T>(entity, totalRecords, filter.PageSize);
            return await Task.FromResult(pagedresponse);
        }
        /// <summary>
        /// To save entity
        /// Begins tracking the given entity, and any other reachable 
        /// entities that are not already being tracked, in the Added 
        /// state such that they will be inserted into the database 
        /// when SaveChanges() is called.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>returns the saved entity</returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// To update entity
        /// This method will automatically call DetectChanges() 
        /// to discover any changes to entity instances before 
        /// saving to the underlying database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>returns number of effected records</returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Entry(entity).Property("CreatedDateTime").IsModified = false;
            _dbContext.Entry(entity).Property("CreatedBy").IsModified = false;
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Delete entity by passing entity details
        /// This method will automatically call DetectChanges() 
        /// to discover any changes to entity instances before 
        /// saving to the underlying database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>returns number of effected records</returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Method to check whether record exist with for 
        /// a column with specific value dynamically for any entity.
        /// </summary>
        /// <param name="ColName"></param>
        /// <param name="colValue"></param>
        /// <returns>returns true if record exists</returns>
        public async Task<bool> IsRecordExistForString(string ColName, string colValue)
        {
            var result = await _dbContext
                  .Set<T>()
                .AsNoTracking().AnyAsync(CompiledLambdaExpressionForString(ColName, colValue), default);
            return result;
        }
        /// <summary>
        /// Method to create dynamic expression for an entity and column
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <returns>returns lamda expression to pass for where condition</returns>
        public Expression<Func<T, bool>> CompiledLambdaExpressionForString(string PropertyName, string PropertyValue)
        {
            var param = Expression.Parameter(typeof(T));
            var member = Expression.Property(param, PropertyName);
            var constant = Expression.Constant(PropertyValue);
            var body = Expression.Equal(member, constant);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
        /// <summary>
        /// Method to check whether record exist or not for 
        /// an entity based on specific column and columnvalue
        /// </summary>
        /// <param name="ColName"></param>
        /// <param name="colValue"></param>
        /// <returns>returns entity</returns>
        public async Task<T> GetRecordByColumn(string ColName, string colValue)
        {
            var result = _dbContext
                  .Set<T>()
                .AsNoTracking().Where(CompiledLambdaExpressionForString(ColName, colValue)).FirstOrDefault();
            return await Task.FromResult(result);
        }
    }
}