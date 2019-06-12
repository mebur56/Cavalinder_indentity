using Caalinder.Models;
using Caalinder.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Caalinder.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("sqlconnection")
        {

        }

        public DbSet<HorseModel> Horses { get; set; }
        public DbSet<MatchModel> Matches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           /* modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<String>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
            modelBuilder.Configurations.Add(new HorseConfiguration());
            modelBuilder.Configurations.Add(new MatchConfiguration());
            */
        }
    }
}