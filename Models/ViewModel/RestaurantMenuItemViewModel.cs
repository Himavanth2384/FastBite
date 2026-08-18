using System.Collections.Generic;

namespace FastBite.Models.ViewModel
{
    public class RestaurantMenuItemViewModel
    {
        public Restaurant Restaurant{get;set;}
        public MenuItem MenuItem{get;set;}
        public IEnumerable<MenuItem> MenuItemList{get;set;}
    }
}