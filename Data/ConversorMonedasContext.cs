using ConversorMonedas.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConversorMonedas.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor con opciones que se inyectarán desde Program.cs
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed de monedas
            modelBuilder.Entity<Coin>().HasData(
                new Coin { Id = 1, Code = "USD", Legend = "Dólar estadounidense", Symbol = "$", IC = 1.0m },
                new Coin { Id = 2, Code = "ARS", Legend = "Peso argentino", Symbol = "$", IC = 0.002m },
                new Coin { Id = 3, Code = "EUR", Legend = "Euro", Symbol = "€", IC = 1.09m },
                new Coin { Id = 4, Code = "KC", Legend = "Corona Checa", Symbol = "Kč", IC = 0.043m }
            );

            // Seed de suscripciones
            modelBuilder.Entity<Subscription>().HasData(
                new Subscription { Id = 1, Type = "Free", MaxConversions = 10 },
                new Subscription { Id = 2, Type = "Trial", MaxConversions = 100 },
                new Subscription { Id = 3, Type = "Pro", MaxConversions = -1 }
            );

        }



        public DbSet<User> Users => Set<User>();
        public DbSet<Coin> Coins => Set<Coin>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

    }
}
