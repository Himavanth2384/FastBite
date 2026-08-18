using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id{get;set;}
        [Required]
        public int OrderId{get;set;}
        [ForeignKey("OrderId")]
        public virtual Cart Cart{get;set;}

        public int MenuItemId{get;set;}
        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem{get;set;}

        public int Count{get;set;}

        public string Name{get;set;}
        public string Description{get;set;}
    }
}