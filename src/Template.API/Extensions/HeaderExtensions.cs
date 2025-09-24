using System.Globalization;
using Template.Domain.Base;

namespace Template.API.Extensions;

public static class HeaderExtensions
{
    public static void AddPaginationData<T>(this IHeaderDictionary input, Paginate<T> paginate)
        where T : class
    {
        input.Add("x-page-size", paginate.PageSize.ToString(CultureInfo.InvariantCulture));
        input.Add("x-page-number", paginate.PageNumber.ToString(CultureInfo.InvariantCulture));
        input.Add("x-total-page", paginate.TotalPage.ToString(CultureInfo.InvariantCulture));
        input.Add("x-total-count", paginate.TotalCount.ToString(CultureInfo.InvariantCulture));
    }
}

