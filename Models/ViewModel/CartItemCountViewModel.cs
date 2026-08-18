using System.Collections.Generic;

namespace FastBite.Models.ViewModel
{
    public class CartItemCountViewModel
    {
          public IEnumerable<MenuItem> MenuItem{get;set;}
          public int count{get;set;}
    }
}