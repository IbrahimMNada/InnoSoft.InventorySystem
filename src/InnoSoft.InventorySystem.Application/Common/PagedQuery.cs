namespace InnoSoft.InventorySystem.Application.Common
{
    public class PagedQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}