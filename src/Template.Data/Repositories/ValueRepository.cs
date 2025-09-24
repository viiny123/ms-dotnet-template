using Template.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Template.Domain.AggregatesModel.ValueAggreate;
using Template.Domain.Base;

namespace Template.Data.Repositories;

public class ValueRepository : RepositoryBase<TemplateDbContext, Value>, IValueRepository
{
    public ValueRepository(TemplateDbContext context) : base(context)
    {
    }
    
    public async Task<bool> IsCodeDuplicate(string code)
    {
        return await GetDbSetQuery().AnyAsync(x => x.Code == code);
    }

    public async Task<Value> GetValueByIdAsync(Guid id)
    {
        return await GetDbSet()
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Value>> GetValuesAsync(string code, string description)
    {
        var query = GetDbSetQuery();

        if (!string.IsNullOrEmpty(code))
        {
            query = query.Where(v => v.Code == code);
        }

        if (!string.IsNullOrEmpty(description))
        {
            query = query.Where(v => v.Description == description);
        }

        return await query.ToListAsync();
    }

    public async Task<Paginate<Value>> GetValuePaginated(IEnumerable<Expression<Func<Value, bool>>> predicates,
        Paginate<Value> paginate)
    {

        var queryableValue = GetDbSetQuery()
            .AsQueryable();

        foreach (var predicate in predicates)
        {
            queryableValue = queryableValue.Where(predicate);
        }

        //total items
        paginate.TotalCount = await queryableValue.LongCountAsync();

        //Pagination and result
        var values = await queryableValue
            .OrderByDescending(x => x.Timestamp)
            .Skip(paginate.GetSkip())
            .Take(paginate.PageSize)
            .ToListAsync();

        paginate.PageData = values;
        paginate.TotalPage = paginate.GetTotalPage();

        return paginate;

    }
}
