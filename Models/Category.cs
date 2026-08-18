using System.ComponentModel.DataAnnotations;

namespace FastBite.Models
{
    public class Category
    {
    
        [Key]
        public int Id{get;set;}
        [Required]
        [Display(Name="CategoryName")]
        public string Name{get;set;}

    
    }
}