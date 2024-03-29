﻿using Common.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ExperianCore
{
    public class ExperianDBContext : DbContext
    {
        //Constructor sin parametros
        public ExperianDBContext()
        {
        }

        //Constructor con parametros para la configuracion
        public ExperianDBContext(DbContextOptions options) : base(options)
        {
        }

        //Sobreescribimos el metodo OnConfiguring para hacer los ajustes que queramos en caso de
        //llamar al constructor sin parametros
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //En caso de que el contexto no este configurado, lo configuramos mediante la cadena de conexion
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                optionsBuilder.UseMySql(connectionString);
            }
        }

        //Tablas de datos
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Parametro> Parametro { get; set; }
    }
}