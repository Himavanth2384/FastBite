using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastBite.Models
{
    public class CartItem
    {
        public CartItem()
        {
            Count=1;
        }
        public int Id{get;set;}
        public string ApplicationUserId{get;set;}
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser{get;set;}

        public int MenuItemId{get;set;}
        
         [NotMapped]
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem{get;set;}
        [Range(1,int.MaxValue)]
        public int Count{get;set;}
    }
}