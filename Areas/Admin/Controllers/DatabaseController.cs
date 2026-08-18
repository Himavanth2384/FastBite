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
using Microsoft.Extensions.DependencyInjection;

namespace FastBite.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class DatabaseController : Controller
    {
      //  SqlConnection connection = new SqlConnection("Server=(LocalDB)\\MSSQLLocalDB;Database=FastBite;Trusted_Connection=True;MultipleActiveResultSets=true");

         public readonly ApplicationDbContext _db;
         public readonly IServiceProvider _serviceProvider;
       //  public IDbContextTransaction _transaction;



         public DatabaseController( ApplicationDbContext db, IServiceProvider serviceProvider){
             _db=db;
             _serviceProvider=serviceProvider;
            // _transaction=transaction;
         }
         public void deleteContext(){
           _db.Database.EnsureDeleted();

           Console.WriteLine("hello");
           using (var scope = _serviceProvider.CreateScope())
           {
               var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
               IdentitySeeder.SeedRolesAsync(roleManager).GetAwaiter().GetResult();
           }
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
