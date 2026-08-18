using System;
using FastBite.Models;

namespace FastBite.Utility
{
    public static class StaticDefinitions
    {
     public const string defaultimage="default1.png";
     public const string Admin = "ADMIN";
     public const string Customer = "Customer";
     public const string DeliveryPerson = "DeliveryPerson";

     public const string RestaurantOwner = "RestaurantOwner";
     public const string CouponCode = "CouponCode";

     public const string PendingConfirmation = "Order Placed and  Pending Confirmation at Restaurant";
      public const string OrderConfirmed = "Order Approved and Food is being Prepared";
        public const string orderReady = "Order Ready for Pickup";
      public const string DeliveryPersonAssigned = "Delivery Person Assigned";

      public const string OrderPickedUp = "Order Picked by delivery Person";

      public const string OrderDelivered = "Order Delivered";
    public const string OrderCancelled = "Order Cancelled";


     public static double DiscountPrice(Offer offer,double amountWithoutDiscount){
         if(offer==null){
             return amountWithoutDiscount;
         }
         else{
             if(offer.MinimumAmount>amountWithoutDiscount){
                  return amountWithoutDiscount;
             }
             else{
                 if(Convert.ToInt32(offer.CouponType)==(int)Offer.ECouponType.Rupee){
                     return Math.Round(amountWithoutDiscount-offer.Discount,2);
                 }
                 else{
                       if(Convert.ToInt32(offer.CouponType)==(int)Offer.ECouponType.Percent){
                     return Math.Round(amountWithoutDiscount-(amountWithoutDiscount*offer.Discount/100),2);
                 }
                 }
             }
         }
         return amountWithoutDiscount;
     }
    }
}