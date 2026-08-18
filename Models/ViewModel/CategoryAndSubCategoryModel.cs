using System.Collections.Generic;

namespace FastBite.Models.Viewmodels
{
    public class CategoryAndSubCategoryModel
    {
        public IEnumerable<Category> categoryList{get;set;}
        public SubCategory subCategory{get;set;}
        public List<string> subCategoryList{get;set;}
        public string errorMessage{get;set;}
    }
}