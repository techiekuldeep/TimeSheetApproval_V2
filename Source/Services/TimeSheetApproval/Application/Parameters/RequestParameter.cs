namespace TimeSheetApproval.Application.Parameters
{
    public class RequestParameter
    {
        private int _pageNumber;
        private int _pagesize;
        public int PageNumber { get { return _pageNumber; } set { _pageNumber = value < 1 ? 1 : value; } }
        public int PageSize { get { return _pagesize; } set {_pagesize = value == 0 ? 10 : value; } }
        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
    }
}
