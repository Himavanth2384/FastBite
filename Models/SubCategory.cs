using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class SubCategory
    {
         [Key]
        public int Id{get;set;}
        [Required]
        [Display(Name="SubCategoryName")]
        public string Name{get;set;}

        [Required]
        [Display(Name="CategoryId")]
        public int CategoryId{get;set;}

        [ForeignKey("CategoryId")]
        [Display(Name="Category")]
        public virtual Category category{get;set;}
    }
}