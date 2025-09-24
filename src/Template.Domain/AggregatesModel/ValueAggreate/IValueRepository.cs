using System.Linq.Expressions;
using Template.Domain.Base;

namespace Template.Domain.AggregatesModel.ValueAggreate
{
    public interface IValueRepository : IRepositoryBase<Value>
    {
        Task<bool> IsCodeDuplicate(string code);
        Task<Value> GetValueByIdAsync(Guid id);
        Task<IEnumerable<Value>> GetValuesAsync(string code, string description);

        Task<Paginate<Value>> GetValuePaginated(IEnumerable<Expression<Func<Value, bool>>> predicates,
                                                Paginate<Value> paginate);
    }
}
