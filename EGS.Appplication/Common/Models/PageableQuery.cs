namespace EGS.Application.Common.Models
{
    public abstract class PageableQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
