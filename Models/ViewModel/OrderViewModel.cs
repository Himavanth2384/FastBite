using System.Collections.Generic;

namespace FastBite.Models.ViewModel
{
    public class OrderViewModel
    {
        public Cart cart{get;set;}
        public IEnumerable<OrderDetails> OrderDetailsList{get;set;}

        public Restaurant Restaurant{get;set;}
    }
}