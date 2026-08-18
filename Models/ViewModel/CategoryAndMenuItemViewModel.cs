using System.Collections.Generic;

namespace FastBite.Models.ViewModel
{
    public class CategoryAndMenuItemViewModel
    {
        public IEnumerable<MenuItem> MenuItem{get;set;}
        public IEnumerable<Category> Category{get;set;}

        public Restaurant Restaurant{get;set;}
    }
}