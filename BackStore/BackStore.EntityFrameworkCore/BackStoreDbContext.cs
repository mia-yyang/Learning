using BackStore.Domain.UserInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace BackStore.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class BackStoreDbContext : AbpDbContext<BackStoreDbContext>
    {
        public DbSet<Users> Users { get; set; }


        public BackStoreDbContext(DbContextOptions<BackStoreDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
