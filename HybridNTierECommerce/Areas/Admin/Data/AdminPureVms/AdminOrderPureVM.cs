namespace HybridNTierECommerce.Areas.Admin.Data.AdminPureVms
{
    public class AdminOrderPureVM
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public string ShippedAddress { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
