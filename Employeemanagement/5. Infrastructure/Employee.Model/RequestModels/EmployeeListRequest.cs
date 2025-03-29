namespace Employee.Model.RequestModels
{
    public class EmployeeListRequest
    {
        public string? Condition { get; set; }

        public string SortColumn { get; set; }

        public string SortDirection { get; set; }

        public int StartIndex { get; set; }

        public int PageSize { get; set; }
    }

}
