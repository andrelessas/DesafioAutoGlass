using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DesafioAutoGlass.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioAutoGlass.INFRASTRUCTURE.Context
{
    public class DesafioAutoGlassContext:DbContext
    {
        public DesafioAutoGlassContext(DbContextOptions options)
            :base(options)
        {
            
        }   

        public DbSet<Produto> Produto { get; set; } = null;
        public DbSet<Fornecedor> Fornecedor { get; set; } = null;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}