using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RestaurantName{get;set;}
        [Required]
        public string Address{get;set;}
        public string imageurl{get;set;}
        [Display(Name="OwenerID")]
        public string OwenerID{get;set;}
        [ForeignKey("OwenerID")]
        public virtual ApplicationUser ApplicationUser{get;set;}


    }
}