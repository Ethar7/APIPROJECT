namespace Ecommerence.Shared
{
    public class ProductQueryParams
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        public string? Search { get; set; }

        public ProductSortingObjects? Sort{get; set;}

        private int _pageIndex = 1;
        public int pageIndex
        {
            get {return _pageIndex = 1;}
            set {_pageIndex = (value <= 0) ? 1 : value;}
        }
        private const int DefaultPageSize = 5;
        private const int MaxPageSize = 10;
        private int _pageSize = 5;

        public int PageSize
        {
            get {return _pageSize;}
            set {
                if (value <= 0)
                    _pageSize = DefaultPageSize; 
                else if (value > MaxPageSize)
                    _pageSize = MaxPageSize;

                else
                    _pageSize = value;
            }
        }
    }
}