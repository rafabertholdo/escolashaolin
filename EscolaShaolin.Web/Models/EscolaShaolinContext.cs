using EscolaShaolin.Dominio.Academia.Entidade;
using EscolaShaolin.Framework.Persistence.EntityFramework;
using EscolaShaolin.Web.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EscolaShaolin.Web.Models
{
    public class EscolaShaolinContext : BaseEFContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EscolaShaolinContext() : base("name=EscolaShaolinContext")
        {
            Init();
        }

        public EscolaShaolinContext(string connString)
            : base(connString)
        {
            Init();
        }

        public System.Data.Entity.DbSet<EscolaShaolin.Dominio.Academia.Entidade.Aluno> Alunos { get; set; }


        public override void SetInitializer()
        {
            Database.SetInitializer<EscolaShaolinContext>(new MigrateDatabaseToLatestVersion<EscolaShaolinContext,Configuration>());
        }

        public override void AddMappingConfigurations(object modelBuilder)
        {
            base.AddMappingConfigurations(modelBuilder);
            ((DbModelBuilder)modelBuilder).Entity<Aluno>().ToTable("Academia", "Aluno");            
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }        
        
    
    }
}
