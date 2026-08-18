using System.ComponentModel.DataAnnotations;

namespace FastBite.Models
{
    public class Offer
    {
        [Key]
        public int Id{get;set;}

        [Required]
        public string Name{get;set;}
        [Required]
        public string CouponType{get;set;}

        [Required]
        public double MinimumAmount{get;set;}

        public enum ECouponType {Percent=0,Rupee=1}
        [Required]
        public double Discount{get;set;}

        public byte[] picture{get;set;}
        public bool isActive{get;set;}
    }
}