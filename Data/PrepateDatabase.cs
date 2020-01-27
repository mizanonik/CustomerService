using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerServiceApi.Data
{
    public class PrepateDatabase
    {
        public static void populateDatabase(IApplicationBuilder app){
            using(var serviceScope = app.ApplicationServices.CreateScope()){
                var context = serviceScope.ServiceProvider.GetService<CustomerServiceContext>();
                context.Database.Migrate();
            }
        }
    }
}