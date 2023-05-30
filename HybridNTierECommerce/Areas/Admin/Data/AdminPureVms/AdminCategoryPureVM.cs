namespace HybridNTierECommerce.Areas.Admin.Data.AdminPureVms
{
    public class AdminCategoryPureVM
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string DataStatus { get; set; }
    }
}
