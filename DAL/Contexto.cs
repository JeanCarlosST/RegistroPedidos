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
        public DbSet<Ventas> Ventas { get; set; }
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
                    new Productos(1, "Jugo de naranja", 75M, 0),
                    new Productos(2, "Galletas saladas", 30M, 0),
                    new Productos(3, "Funda de pan sobao", 60M, 0),
                    new Productos(4, "Leche entera", 50M, 0),
                    new Productos(5, "Leche descremada", 200M, 0),
                    new Productos(6, "Hojuelitas de queso", 10M, 0),
                    new Productos(7, "Platanitos", 15M, 0),
                    new Productos(8, "Lays de queso", 25M, 0),
                    new Productos(9, "Lata de maíz", 50M, 0),
                    new Productos(10, "Lata de tomaticos", 35M, 0),
                    new Productos(11, "Botella de ketchup", 60M, 0),
                }
            );

            modelBuilder.Entity<Suplidores>().HasData(new List<Suplidores>()
                {
                    new Suplidores(1, "Rica"),
                    new Suplidores(2, "Guarina"),
                    new Suplidores(3, "Yoma"),
                    new Suplidores(4, "Frito Lays"),
                    new Suplidores(5, "La Famosa")
                }
            );
        }
    }
}
