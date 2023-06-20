
using APISP.Models;
using Microsoft.Data.SqlClient;

namespace APISP
{
   public class Program
   {
      public static void Main(string[] args)
      {
         var builder = WebApplication.CreateBuilder(args);
         builder.Services.AddControllers();
         builder.Services.AddEndpointsApiExplorer();
         builder.Services.AddSwaggerGen();         
         builder.Services.AddScoped(_ => new SqlConnection(builder.Configuration.GetConnectionString("DatabaseSP")));
         builder.Services.AddScoped<DALPeople>();
         builder.Services.Configure<RouteOptions>(o =>
         {
            o.LowercaseUrls = true;
            o.LowercaseQueryStrings = true;
         });

         var app = builder.Build();

         if (app.Environment.IsDevelopment())
         {
            app.UseSwagger();
            app.UseSwaggerUI();
         }

         app.UseHttpsRedirection();
         app.UseAuthorization();
         app.MapControllers();
         app.Run();
      }
   }
}