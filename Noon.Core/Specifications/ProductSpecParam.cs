namespace Noon.Core.Specifications
{
    public class ProductSpecParam
    {

        private const int MaxPageSize = 5;
        private string _search = string.Empty;
        private int _pageSize = MaxPageSize;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public int PageIndex { get; set; } = 1;

        public string? Sort { get; set; }

        public int? TypeId { get; set; }

        public int? BrandId { get; set; }




    }
}
