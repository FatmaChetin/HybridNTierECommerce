using PagedList;
using Project.VM.PureVMs;

namespace HybridNTierECommerce.Models.PageVMs
{
    public class PaginationVM
    {
        // IpagedList kütüphanesinden emin değilim araştır!!
        //todo core önemli kütüphanleri öğren!!!
        public IPagedList<ProductVM> PagedProducts  { get; set; }
        public List<CategoryVM> Categories { get; set; }
        public ProductVM Product { get; set; }
    }
}
