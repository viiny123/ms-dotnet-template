namespace Template.Domain.Base
{
    public class Paginate<T> where T : class
    {
        private readonly int DefaultPageSize;

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long TotalCount { get; set; }
        public long TotalPage { get; set; }
        public IEnumerable<T> PageData { get; set; }

        public Paginate(int defaultSize = 10)
        {
            PageSize = defaultSize;
            PageNumber = 1;
            PageData = new List<T>();
            DefaultPageSize = defaultSize;
        }

        public Paginate(int pageSize,
            int pageNumber,
            long totalCount,
            long totalPage)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalCount = totalCount;
            TotalPage = totalPage;
        }

        public int GetSkip()
        {
            return (PageNumber - 1) * PageSize;
        }

        public long GetTotalPage()
        {
            var totalPages = (double)TotalCount / PageSize;
            var roundedTotalPages = (long)Math.Ceiling(totalPages);

            return roundedTotalPages;
        }
    }
}
