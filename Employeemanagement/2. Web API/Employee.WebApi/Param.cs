namespace Employee.WebApi
{
    public class Param
    {
        public string? condition { get; set; }

        public string sortColumn { get; set; }

        public string sortDirection { get; set; }

        public int startIndex { get; set; }

        public int pageSize { get; set; }
    }
}
