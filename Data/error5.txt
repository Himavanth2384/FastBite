using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FastBite.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using XunitOrderer;
using OpenQA.Selenium.Support.UI;
using System.IO;

namespace FastBite.Tests
{
  
    [TestCaseOrderer("FastBite.Tests.PriorityOrderer", "FastBite.Tests")]
    public class FastBiteTests :  IClassFixture<WebApplicationFactory<FastBite.Startup>>,IDisposable
    {
        
        private IWebDriver _driver;
        public ApplicationDbContext _db;

        public FastBiteTests(){
        //    // _factory=factory;
             var opts = new ChromeOptions();
        opts.AddArgument("--headless");
            _driver = new ChromeDriver(opts);

        }
        [Fact, TestPriority(1)]
        public void addCustomer(){
            Console.WriteLine("test1");
              _driver.Navigate()
        .GoToUrl("http://localhost:8000/Admin/Database/deleteContext");
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Register");
 
    _driver.FindElement(By.Id("Input_Name"))
        .SendKeys("Test Employee");
 
    _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("customer1@fastbite.com");
 
    _driver.FindElement(By.Id("Input_PhoneNumber"))
        .SendKeys("77777777777");
        _driver.FindElement(By.Id("Input_Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Customer@1");
         _driver.FindElement(By.Id("Input_ConfirmPassword"))
        .SendKeys("Customer@1");
        _driver.FindElement(By.XPath("//button[contains(text(),'Register')]")).Click();
       
     var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello customer1@fastbite.com!",profilename);
         }
        [Fact,TestPriority(2)]
      public void loginCustomer(){
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("customer1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Customer@1");
          _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
           var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello customer1@fastbite.com!",profilename);

      }
        [Fact,TestPriority(3)]
        public void addAdmin(){ 
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Register");
 
    _driver.FindElement(By.Id("Input_Name"))
        .SendKeys("Test Employee");
 
    _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("admin1@fastbite.com");
 
    _driver.FindElement(By.Id("Input_PhoneNumber"))
        .SendKeys("77777777777");
        _driver.FindElement(By.Id("Input_Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Admin@123");
         _driver.FindElement(By.Id("Input_ConfirmPassword"))
        .SendKeys("Admin@123");
         _driver.FindElement(By.XPath("//input[@value='ADMIN']")).Click();
        _driver.FindElement(By.XPath("//button[contains(text(),'Register')]")).Click();
       
     var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello admin1@fastbite.com!",profilename);
        }
        [Fact,TestPriority(4)]
        public void addDeliveryPerson(){
        
             
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Register");
 
    _driver.FindElement(By.Id("Input_Name"))
        .SendKeys("Test Employee");
 
    _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("delivery1@fastbite.com");
 
    _driver.FindElement(By.Id("Input_PhoneNumber"))
        .SendKeys("77777777777");
        _driver.FindElement(By.Id("Input_Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Delivery@1");
         _driver.FindElement(By.Id("Input_ConfirmPassword"))
        .SendKeys("Delivery@1");
         _driver.FindElement(By.XPath("//input[@value='DeliveryPerson']")).Click();
        _driver.FindElement(By.XPath("//button[contains(text(),'Register')]")).Click();
       
     var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello delivery1@fastbite.com!",profilename);
        }
        [Fact,TestPriority(5)]
        public void addRestaurantOwner(){   
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Register");
 
    _driver.FindElement(By.Id("Input_Name"))
        .SendKeys("Test Employee");
 
    _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("restaurant1@fastbite.com");
 
    _driver.FindElement(By.Id("Input_PhoneNumber"))
        .SendKeys("77777777777");
        _driver.FindElement(By.Id("Input_Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Restaurant@1");
         _driver.FindElement(By.Id("Input_ConfirmPassword"))
        .SendKeys("Restaurant@1");
         _driver.FindElement(By.XPath("//input[@value='RestaurantOwner']")).Click();
        _driver.FindElement(By.XPath("//button[contains(text(),'Register')]")).Click();
       
     var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello restaurant1@fastbite.com!",profilename);
        }
        [Fact,TestPriority(6)]
      public void addCategory(){
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("admin1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Admin@123");
          _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
           var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello admin1@fastbite.com!",profilename);
       _driver.Navigate()
        .GoToUrl("http://localhost:8000/Admin/Category");
           _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
            _driver.FindElement(By.Id("Name"))
        .SendKeys("Apetizer");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
     
        string categoryName = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("Apetizer",categoryName);
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
            _driver.FindElement(By.Id("Name"))
        .SendKeys("Biryani");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
         string categoryName1 = _driver.FindElement(
		By.XPath("//table/tbody/tr[3]/td[1]")).Text;
        Assert.Equal("Biryani",categoryName1);
       IEnumerable<IWebElement> buttons= _driver.FindElements(By.XPath("//a[contains(text(),'Edit')]"));
       buttons.First().Click();
        _driver.FindElement(By.Id("Name")).Clear();
        _driver.FindElement(By.Id("Name"))
        .SendKeys("Apetizer1");
         _driver.FindElement(By.XPath("//input[@value='Update']")).Click();
          string categoryName3 = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("Apetizer1",categoryName3);
      }
      [Fact,TestPriority(7)]
      public void addSubcategory(){
             _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("admin1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Admin@123");
          _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
           var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello admin1@fastbite.com!",profilename);
     _driver.Navigate()
      .GoToUrl("http://localhost:8000/Admin/SubCategory");
           _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
             _driver.FindElement(By.Id("subCategory_Name"))
        .SendKeys("Beverages");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
     
        string subcategoryName = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("Beverages",subcategoryName);
        string categoryName = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[2]")).Text;
        Assert.Equal("Apetizer1",categoryName);
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
             _driver.FindElement(By.Id("subCategory_Name"))
        .SendKeys("Mocktails");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
     
        string subcategoryName2 = _driver.FindElement(
		By.XPath("//table/tbody/tr[3]/td[1]")).Text;
        Assert.Equal("Mocktails",subcategoryName2);
        string categoryName2 = _driver.FindElement(
		By.XPath("//table/tbody/tr[3]/td[2]")).Text;
        Assert.Equal("Apetizer1",categoryName2);
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
        new SelectElement(_driver.FindElement(By.Id("ddCategoryList"))).SelectByText("Biryani");
           _driver.FindElement(By.Id("subCategory_Name"))
        .SendKeys("Veg");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
     
        string subcategoryName3 = _driver.FindElement(
		By.XPath("//table/tbody/tr[4]/td[1]")).Text;
        Assert.Equal("Veg",subcategoryName3);
        string categoryName3 = _driver.FindElement(
		By.XPath("//table/tbody/tr[4]/td[2]")).Text;
        Assert.Equal("Biryani",categoryName3);
          IEnumerable<IWebElement> buttons= _driver.FindElements(By.XPath("//a[contains(text(),'Edit')]"));
       buttons.First().Click();
       _driver.FindElement(By.Id("subCategory_Name")).Clear();
        _driver.FindElement(By.Id("subCategory_Name"))
        .SendKeys("Beverages1");
         _driver.Navigate()
        .GoToUrl("http://localhost:8000/Admin/Offer");
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
          _driver.FindElement(By.Id("Name"))
        .SendKeys("100OFF");
         (new SelectElement(_driver.FindElement(By.Id("CouponType"))))
        .SelectByText("Rupee");
         _driver.FindElement(By.Id("Discount"))
        .SendKeys("100");
         _driver.FindElement(By.Id("MinimumAmount"))
        .SendKeys("250");
         _driver.FindElement(By.Id("isActive"))
        .Click();
         _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
          string coupon = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("100OFF",coupon);
      }
      [Fact,TestPriority(8)]
      public void addRestaurant(){
        
               _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("restaurant1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Restaurant@1");
          _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
           var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello restaurant1@fastbite.com!",profilename);
     _driver.FindElement(By.XPath("//a[contains(text(),'Restaurants')]")).Click();
      _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
         _driver.FindElement(By.Id("RestaurantName"))
        .SendKeys("Restaurant1");
         _driver.FindElement(By.Id("Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
        string restname1 = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("Restaurant1",restname1);
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
         _driver.FindElement(By.Id("RestaurantName"))
        .SendKeys("Restaurant2");
         _driver.FindElement(By.Id("Address"))
        .SendKeys("Wallstreet");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
        string restname2 = _driver.FindElement(
		By.XPath("//table/tbody/tr[3]/td[1]")).Text;
        Assert.Equal("Restaurant2",restname2);
        IEnumerable<IWebElement> buttons= _driver.FindElements(By.XPath("//a[contains(text(),'MenuItems')]"));
        buttons.ElementAt(1).Click();
        _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
         new SelectElement(_driver.FindElement(By.Id("CategoryId"))).SelectByText("Biryani");
          new SelectElement(_driver.FindElement(By.Id("SubCategoryId"))).SelectByText("Veg");
          _driver.FindElement(By.Id("MenuItem_Name"))
        .SendKeys("Dumbiryani");
          _driver.FindElement(By.Id("MenuItem_description"))
        .SendKeys("RiceItem");
        _driver.FindElement(By.Id("MenuItem_price"))
        .SendKeys("190");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
         new SelectElement(_driver.FindElement(By.Id("CategoryId"))).SelectByText("Apetizer1");
          new SelectElement(_driver.FindElement(By.Id("SubCategoryId"))).SelectByText("Beverages");
          _driver.FindElement(By.Id("MenuItem_Name"))
        .SendKeys("Lemonade");
          _driver.FindElement(By.Id("MenuItem_description"))
        .SendKeys("MadebyLemon");
        _driver.FindElement(By.Id("MenuItem_price"))
        .SendKeys("110");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
         string menu1 = _driver.FindElement(
		By.XPath("//table/tbody/tr[1]/td[1]")).Text;
        Assert.Equal("Dumbiryani",menu1);
         string menu2 = _driver.FindElement(
		By.XPath("//table/tbody/tr[2]/td[1]")).Text;
        Assert.Equal("Lemonade",menu2);
         _driver.FindElement(By.XPath("//a[contains(text(),'Restaurants')]")).Click();
        
         IEnumerable<IWebElement> buttons1= _driver.FindElements(By.XPath("//a[contains(text(),'MenuItems')]"));
        buttons1.First().Click();
         _driver.FindElement(By.XPath("//a[contains(text(),'Create New')]")).Click();
         new SelectElement(_driver.FindElement(By.Id("CategoryId"))).SelectByText("Biryani");
          new SelectElement(_driver.FindElement(By.Id("SubCategoryId"))).SelectByText("Veg");
          _driver.FindElement(By.Id("MenuItem_Name"))
        .SendKeys("vegbiryani");
          _driver.FindElement(By.Id("MenuItem_description"))
        .SendKeys("RiceItem");
        _driver.FindElement(By.Id("MenuItem_price"))
        .SendKeys("130");
        _driver.FindElement(By.XPath("//input[@value='Create']")).Click();
         string menu3 = _driver.FindElement(
		By.XPath("//table/tbody/tr[1]/td[1]")).Text;
        Assert.Equal("vegbiryani",menu3);  
      }
        [Fact,TestPriority(9)]
        public void placeOrder(){
            _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("customer1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Customer@1");
          _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
           var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
     Assert.Equal("Hello customer1@fastbite.com!",profilename);
      IEnumerable<IWebElement> buttons1= _driver.FindElements(By.XPath("//a[contains(text(),'order Now')]"));
        buttons1.First().Click();
         IEnumerable<IWebElement> cartbuttons= _driver.FindElements(By.XPath("//a[contains(text(),'Add To Cart')]"));
        cartbuttons.First().Click();
        _driver.FindElement(By.Id("Count")).Clear();
       _driver.FindElement(By.Id("Count")).SendKeys("5");
       _driver.FindElement(By.XPath("//button[contains(text(),'AddToCart')]")).Click();
      String cart =  _driver.FindElement(By.XPath("//a[@href ='/Customer/Cart/PlaceOrder']")).Text;
       Assert.Equal("Cart(1)",cart);
        IEnumerable<IWebElement> cartbuttons1= _driver.FindElements(By.XPath("//a[contains(text(),'Add To Cart')]"));
        cartbuttons1.First().Click();
         _driver.FindElement(By.Id("Count")).Clear();
         _driver.FindElement(By.Id("Count")).SendKeys("5");
       _driver.FindElement(By.XPath("//button[contains(text(),'AddToCart')]")).Click();
        String cart1 =  _driver.FindElement(By.XPath("//a[@href ='/Customer/Cart/PlaceOrder']")).Text;
       Assert.Equal("Cart(1)",cart1);
       _driver.FindElement(By.XPath("//a[contains(text(),'FastBite')]")).Click();
        IEnumerable<IWebElement> buttons= _driver.FindElements(By.XPath("//a[contains(text(),'order Now')]"));
        buttons.ElementAt(1).Click();
         IEnumerable<IWebElement> cartbuttons2= _driver.FindElements(By.XPath("//a[contains(text(),'Add To Cart')]"));
        cartbuttons2.First().Click();
         _driver.FindElement(By.Id("Count")).Clear();
          _driver.FindElement(By.Id("Count")).SendKeys("5");
       _driver.FindElement(By.XPath("//button[contains(text(),'AddToCart')]")).Click();
         String cart2 =  _driver.FindElement(By.XPath("//a[@href ='/Customer/Cart/PlaceOrder']")).Text;
       Assert.Equal("Cart(1)",cart2);
        IEnumerable<IWebElement> cartbuttons3= _driver.FindElements(By.XPath("//a[contains(text(),'Add To Cart')]"));
        cartbuttons3.ElementAt(1).Click();
        _driver.FindElement(By.Id("Count")).Clear();
        _driver.FindElement(By.Id("Count")).SendKeys("5");
       _driver.FindElement(By.XPath("//button[contains(text(),'AddToCart')]")).Click();
         String cart3 =  _driver.FindElement(By.XPath("//a[@href ='/Customer/Cart/PlaceOrder']")).Text;
       Assert.Equal("Cart(2)",cart3);
       _driver.FindElement(By.XPath("//a[contains(text(),'Cart(2)')]")).Click();
        String total = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("1500",total);
        IEnumerable<IWebElement> addbutton= _driver.FindElements(By.Id("Add"));
        addbutton.First().Click();
        IEnumerable<IWebElement> addbutton1= _driver.FindElements(By.Id("Add"));
        addbutton1.ElementAt(1).Click();
          String total1 = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("1800",total1);
         IEnumerable<IWebElement> minusbutton= _driver.FindElements(By.Id("minus"));
        minusbutton.First().Click();
        IEnumerable<IWebElement> minusbutton1= _driver.FindElements(By.Id("minus"));
        minusbutton1.ElementAt(1).Click();
         String total2 = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("1500",total2);
        _driver.FindElement(By.Id("offercode")).SendKeys("100OFF");
        _driver.FindElement(By.XPath("//button[contains(text(),'Apply')]")).Click();
        String total3 = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("1400",total3);
         _driver.FindElement(By.XPath("//button[contains(text(),'Remove')]")).Click();
          String total4 = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("1500",total4);
         _driver.FindElement(By.XPath("//button[contains(text(),'PlaceOrder')]")).Click();
         string status = _driver.FindElement(By.Id("orderstatus")).Text;
         Assert.Equal("Order Placed and Pending Confirmation at Restaurant",status);
          _driver.FindElement(By.XPath("//a[contains(text(),'FastBite')]")).Click();
        IEnumerable<IWebElement> buttons5= _driver.FindElements(By.XPath("//a[contains(text(),'order Now')]"));
        buttons5.ElementAt(1).Click();
         IEnumerable<IWebElement> cartbuttons5= _driver.FindElements(By.XPath("//a[contains(text(),'Add To Cart')]"));
        cartbuttons5.First().Click();
         _driver.FindElement(By.Id("Count")).Clear();
          _driver.FindElement(By.Id("Count")).SendKeys("5");
       _driver.FindElement(By.XPath("//button[contains(text(),'AddToCart')]")).Click();
         String cart4 =  _driver.FindElement(By.XPath("//a[@href ='/Customer/Cart/PlaceOrder']")).Text;
       Assert.Equal("Cart(1)",cart4);
        _driver.FindElement(By.XPath("//a[contains(text(),'Cart(1)')]")).Click();
        String total5 = _driver.FindElement(By.Id("Total")).Text;
        Assert.Equal("550",total5);
          _driver.FindElement(By.XPath("//button[contains(text(),'PlaceOrder')]")).Click();
         string status2 = _driver.FindElement(By.Id("orderstatus")).Text;
         Assert.Equal("Order Placed and Pending Confirmation at Restaurant",status2);
        }
        [Fact,TestPriority(10)]
        public void confirmorder(){
                _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("restaurant1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Restaurant@1");
        _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
        var profilename =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
        Assert.Equal("Hello restaurant1@fastbite.com!",profilename);
        _driver.FindElement(By.XPath("//a[contains(text(),'Restaurants')]")).Click();
        
        // IEnumerable<IWebElement> orderbuttons= _driver.FindElements(By.XPath("//a[contains(text(),'Orders')]"));
        // orderbuttons.ElementAt(1).Click();
        _driver.FindElement(By.XPath("//a[@href ='/Customer/Order/PendingRestaurantOrderDetails?restid=2']")).Click();
        // _driver.FindElement(
		// By.XPath("//table/tbody/tr[3]/td[1]"))
        
         IEnumerable<IWebElement> Accept= _driver.FindElements(By.XPath("//a[contains(text(),'Accept Order')]"));
         IEnumerable<IWebElement> cancel= _driver.FindElements(By.XPath("//a[contains(text(),'Cancel Order')]"));
         Assert.Equal(2,Accept.Count());
         Assert.Equal(2,cancel.Count());
         Accept.First().Click();
         IEnumerable<IWebElement> Accept1= _driver.FindElements(By.XPath("//a[contains(text(),'Accept Order')]"));
         Accept1.First().Click();
              _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("customer1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Customer@1");
        _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
        var profilename1 =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
        Assert.Equal("Hello customer1@fastbite.com!",profilename1);
        _driver.FindElement(By.XPath("//a[contains(text(),'OrderHistory')]"));
        IEnumerable<IWebElement> cards  = _driver.FindElements(By.ClassName("card"));
        Assert.Equal(2,cards.Count());
        // IEnumerable<IWebElement> status  = _driver.FindElements(By.ClassName("orderstatus"));
        // Assert.Equal(2,status.Count());
        // Assert.Equal("Order Approved and Food is being Prepared",status.First().Text);
        // Assert.Equal("Order Approved and Food is being Prepared",status.ElementAt(1).Text);
                _driver.Navigate()
        .GoToUrl("http://localhost:8000/Identity/Account/Login");
         _driver.FindElement(By.Id("Input_Email"))
        .SendKeys("restaurant1@fastbite.com");
         _driver.FindElement(By.Id("Input_Password"))
        .SendKeys("Restaurant@1");
        _driver.FindElement(By.XPath("//button[contains(text(),'Log in')]")).Click();
        var profilename2 =  _driver.FindElement(By.XPath("//a[@title='Manage']")).Text;
        Assert.Equal("Hello restaurant1@fastbite.com!",profilename2);
        _driver.FindElement(By.XPath("//a[contains(text(),'Restaurants')]")).Click();
     
        _driver.FindElement(By.XPath("//a[@href ='/Customer/Order/PendingRestaurantOrderDetails?restid=2']")).Click();
        
       
         IEnumerable<IWebElement> Order= _driver.FindElements(By.XPath("//a[contains(text(),'Order Prepared')]"));
         Order.First().Click();
         IEnumerable<IWebElement> cancel1= _driver.FindElements(By.XPath("//a[contains(text(),'Cancel Order')]"));
         cancel1.First().Click();
       





        }
        public void Dispose()
        {
              _driver.Quit();
        _driver.Dispose();
        }
    }
}