using HybridNTierECommerce.Areas.Admin.Data.AdminPureVms;

namespace HybridNTierECommerce.Areas.Admin.Data.AdminPageVMs
{
    public class AdminAddUpdateProductPageVM
    {
        public AdminProductPureVM Product { get; set; }
        public List<AdminCategoryPureVM> Categories { get; set; }
    }
}
