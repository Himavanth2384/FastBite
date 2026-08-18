using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class Cart
    {
        [Key]
        public int Id{get;set;}
        [Required]
        public string userId{get;set;}

        [ForeignKey("userId")]
        public virtual ApplicationUser ApplicationUser{get;set;}
        [Required]
        public double AmountWithoutDiscount{get;set;}

        [Required]
        [Display(Name="Order Total")]
        public double OrderTotal{get;set;}
         [Display(Name="Offer Code")]
        public string offercode{get;set;}

        public double discount{get;set;}

        public string orderStatus{get;set;}

        public string Address{get;set;}

        public string email{get;set;}
        public string name{get;set;}
        public string mobilenumber{get;set;}
    }
}