using Microsoft.EntityFrameworkCore;
using RegistroPedidos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace RegistroPedidos.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Ordenes> Ordenes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Suplidores> Suplidores { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite(@"Data Source=Data/Pedidos.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Productos>().HasData(new List<Productos>()
                { 
                    new Productos(1, "Jugo de naranja", 50M, 100),
                    new Productos(2, "Galletas saladas", 30M, 80),
                    new Productos(3, "Funda de pan sobao", 50M, 30)
                }
            );

            modelBuilder.Entity<Suplidores>().HasData(new List<Suplidores>()
                {
                    new Suplidores(1, "Rica"),
                    new Suplidores(2, "Guarina"),
                    new Suplidores(3, "Yoma")
                }
            );
        }
    }
}
