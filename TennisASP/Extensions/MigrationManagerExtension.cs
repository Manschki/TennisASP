using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TennisDB
{
    public static class MigrationExtentionManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TennisContext>();
                try
                {
                    db.Database.EnsureDeleted();
                    db.Database.Migrate();
                    int nr = db.Persons.Count();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error migrating DB: " + e.Message);
                    throw;
                }
            }
            return host;
        }
    }
}
