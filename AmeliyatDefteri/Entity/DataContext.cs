using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AmeliyatDefteri.Entity
{
    public class DataContext : DbContext 
    {


         public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
        
        public DbSet<Ameliyat> Ameliyatlar => Set<Ameliyat>();
        public DbSet<Doktor> Doktorlar => Set<Doktor>();
        public DbSet<Zaman> Zamanlar => Set<Zaman>();
        public DbSet<Anestezi> Anesteziler => Set<Anestezi>();
        public DbSet<AmeliyatGun> AmeliyatGunleri => Set<AmeliyatGun>();
        public DbSet<Duyuru> Duyurular => Set<Duyuru>();
        public DbSet<Gecmis> Gecmisler => Set<Gecmis>();



    }
}