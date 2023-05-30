namespace HybridNTierECommerce.Models.ShoppingTools
{
    public class Cart
    {
        Dictionary<int, CartItem> _sepetim;
        public Cart()
        {
            _sepetim = new Dictionary<int, CartItem>();
        }
        public List<CartItem> Sepetim 
        {
            get 
            {
                return _sepetim.Values.ToList();
            }
        }
        public void SepetEkle(CartItem item)
        {
            if (_sepetim.ContainsKey(item.ID))
            {
                _sepetim[item.ID].Amount++;
                return;
            }
            _sepetim.Add(item.ID, item);
        
           
        }
        public void SepettenCikar(int id)// çıkarma işlemi çıkardığın için id ile ile olacak unutma
        {
            if (_sepetim[id].Amount>0)
            {
                _sepetim[id].Amount--;
                return;
            }
            _sepetim.Remove(id);
        }
        public decimal TotalPrice
        {
            get 
            {
                return _sepetim.Sum(x => x.Value.SubTotal);
            }
        
        }

    }
}
