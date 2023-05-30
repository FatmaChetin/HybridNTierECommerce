using HybridNTierECommerce.Models.OutherRequestMadel;
using Project.VM.PureVMs;

namespace HybridNTierECommerce.Models.PageVMs
{
    public class OrderPageVM
    {
        public OrderVM  Order { get; set; }
        public List<OrderVM> Orders { get; set; }
        public PaymentRequestModel PaymentRM { get; set; }
    }
}
