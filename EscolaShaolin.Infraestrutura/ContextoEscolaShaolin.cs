using EscolaShaolin.Dominio.Academia.Entidade;
using EscolaShaolin.Framework.Persistence.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EscolaShaolin.Infraestrutura
{
    public class ContextoEscolaShaolin : BaseEFContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ContextoEscolaShaolin() : base("name=EscolaShaolinContext")
        {
            Init();
        }

        public ContextoEscolaShaolin(string connString)
            : base(connString)
        {
            Init();
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<LocalTreinamento> LocaisTreinamento { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<QuantidadeModalidade> QuantidadesModalidades { get; set; }
        public DbSet<Modalidade> Modalidades { get; set; }

        public override void SetInitializer()
        {
            Database.SetInitializer<ContextoEscolaShaolin>(new MigrateDatabaseToLatestVersion<ContextoEscolaShaolin, EscolaShaolin.Infraestrutura.Migrations.Configuration>());
        }

        public override void AddMappingConfigurations(object modelBuilder)
        {
            base.AddMappingConfigurations(modelBuilder);
            ((DbModelBuilder)modelBuilder).Entity<Aluno>().ToTable("Aluno", "Academia");
            ((DbModelBuilder)modelBuilder).Entity<Plano>().ToTable("Plano", "Academia");
            ((DbModelBuilder)modelBuilder).Entity<LocalTreinamento>().ToTable("LocalTreinamento", "Academia");
            ((DbModelBuilder)modelBuilder).Entity<Profissional>().ToTable("Profissional", "Academia");
            ((DbModelBuilder)modelBuilder).Entity<QuantidadeModalidade>().ToTable("QuantidadeModalidade", "Academia");            
            ((DbModelBuilder)modelBuilder).Entity<Modalidade>().ToTable("Modalidade", "Academia");                        
        }        

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }        
        
    
    }
}
