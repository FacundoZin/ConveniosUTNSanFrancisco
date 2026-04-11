namespace APIconvenios.Common
{
    public class PaginatedResult<T> : Result<T>
    {
        public int TotalItems { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedResult(bool exit, T data, string errormessage, int errorcode, int totalItems, int currentPage, int pageSize) 
            : base(exit, data, errormessage, errorcode)
        {
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize);
        }

        public static PaginatedResult<T> ExitoPaginado(T data, int totalItems, int currentPage, int pageSize)
            => new PaginatedResult<T>(true, data, string.Empty, 200, totalItems, currentPage, pageSize);
    }
}
