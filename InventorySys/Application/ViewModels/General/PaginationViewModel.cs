namespace Application.ViewModels.General
{
    public class PaginationViewModel
    {
        public PaginationViewModel()
        {
            this.SortName = "Id";
            this.SortDirection = "DESC";
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
        public string SortName { get; set; }
        public string SortDirection { get; set; }
    }
}
