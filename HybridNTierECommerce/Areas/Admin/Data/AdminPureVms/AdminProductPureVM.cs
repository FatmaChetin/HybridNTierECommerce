namespace HybridNTierECommerce.Areas.Admin.Data.AdminPureVms
{
    public class AdminProductPureVM
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string DataStatus { get; set; }
    }
}
