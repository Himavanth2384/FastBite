using System;
using System.IO;
using System.Linq;
using FastBite.Data;
using FastBite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace FastBite.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class DatabaseController : Controller
    {
      //  SqlConnection connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=FastBite;Trusted_Connection=True;MultipleActiveResultSets=true");

         public readonly ApplicationDbContext _db;
         public readonly RoleManager<IdentityRole> _roleManager;
       //  public IDbContextTransaction _transaction;



         public DatabaseController( ApplicationDbContext db, RoleManager<IdentityRole> roleManager){
             _db=db;
             _roleManager=roleManager;
            // _transaction=transaction;
         }
         public void deleteContext(){
           _db.Database.EnsureDeleted();

           Console.WriteLine("hello");
           IdentitySeeder.SeedRolesAsync(_roleManager).GetAwaiter().GetResult();
           DirectoryInfo di = new DirectoryInfo("./wwwroot/images/restaurant");
if(di.Exists && di.GetFiles().Count()>0){

  foreach (FileInfo file in di.GetFiles())
{

    file.Delete();
}
}
  DirectoryInfo di2 = new DirectoryInfo("./wwwroot/images/menuitems");
if(di2.Exists && di2.GetFiles().Count()>0){
  foreach (FileInfo file in di2.GetFiles())
{

    file.Delete();
}
}


         }

    }
}
