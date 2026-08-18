using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class MenuItem
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public string description{get;set;}

        public string imageUrl{get;set;}
    
        [Display(Name="CategoryId")]
        public int CategoryId{get;set;}
        [Display(Name="SubCategoryId")]
        public int SubCategoryId{get;set;}
        [ForeignKey("CategoryId")]
        [Display(Name="Category")]
        public virtual Category Category{get;set;}
         [ForeignKey("SubCategoryId")]
         [Display(Name="SubCategory")]
        public virtual SubCategory SubCategory{get;set;}
        [Required]
        public double price{get;set;}
        [Display(Name="RestaurantId")]
        public int RestaurantId{get;set;}
        [ForeignKey("RestaurantId")]
        [Display(Name="Restaurant")]
        public virtual Restaurant Restaurant{get;set;}
    }
}