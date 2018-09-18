using BilisimProjesi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilisimProjesi.Context
{
    public class BlogContext:DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Postlar> Postlar { get; set; }
        public DbSet<Yorumlar> Yorumlar { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Kullanicilar> Kullanicilar { get; set; }
        public DbSet<Yetkiler> Yetkiler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Postlar>().ToTable("Postlar"); 
            modelBuilder.Entity<Yorumlar>().ToTable("Yorumlar"); 
            modelBuilder.Entity<Kategoriler>().ToTable("Kategoriler"); 
            modelBuilder.Entity<Kullanicilar>().ToTable("Kullanicilar"); 
            modelBuilder.Entity<Yetkiler>().ToTable("Yetkiler"); 
        }

        public DbSet<BilisimProjesi.Models.Icerikler> Icerikler { get; set; }
    }
}
