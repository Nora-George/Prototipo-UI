using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Recorridos.Modelo
{
    class ModeloBD : DbContext
    {
        public ModeloBD()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "bdRecorridos2.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<CTipoUsuario> CTipoUsuario { get; set; }
        public virtual DbSet<Zona> Zona { get; set; }
        public virtual DbSet<Concepto> Concepto { get; set; }
        public virtual DbSet<Parte> Parte { get; set; }
        public virtual DbSet<ZonaConceptoParte> ZonaConceptoParte { get; set; }
        public virtual DbSet<Recorrido> Recorrido { get; set; }
        public virtual DbSet<Piso> Piso { get; set; }
    }
}
