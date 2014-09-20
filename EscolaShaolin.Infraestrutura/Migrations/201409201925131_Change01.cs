namespace EscolaShaolin.Infraestrutura.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Change01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Academia.Aluno",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        StatusOutros = c.String(),
                        Sexo = c.Int(nullable: false),
                        EstadoCivil = c.Int(),
                        DataNascimento = c.DateTime(nullable: false),
                        Identidade = c.String(),
                        ContatoEmergencia = c.String(),
                        ComoConheceuEscola = c.Int(),
                        AlunoQueIndicouId = c.Guid(),
                        ComoConheceuEscolaOutros = c.String(),
                        NomePai = c.String(),
                        NomeMae = c.String(),
                        NomeResponsavel = c.String(),
                        Email = c.String(),
                        ValidadeCarteira = c.DateTime(),
                        DataMatricula = c.DateTime(),
                        DataCancelamento = c.DateTime(),
                        PlanoId = c.Guid(),
                        Nome = c.String(),
                        CnpjCpf = c.String(),
                        Telefone = c.String(),
                        Telefone2 = c.String(),
                        Endereco = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        CEP = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Academia.Aluno", t => t.AlunoQueIndicouId)
                .ForeignKey("Academia.Plano", t => t.PlanoId)
                .Index(t => t.AlunoQueIndicouId)
                .Index(t => t.PlanoId);
            
            CreateTable(
                "Academia.Plano",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Valor = c.Single(nullable: false),
                        LocalTrainamentoCodigo = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        LocalTrainamento_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Academia.LocalTreinamento", t => t.LocalTrainamento_Id)
                .Index(t => t.LocalTrainamento_Id);
            
            CreateTable(
                "Academia.LocalTreinamento",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        Endereco = c.String(),
                        Telefone = c.String(),
                        ProfissionalCodigo = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        Profissional_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Academia.Profissional", t => t.Profissional_Id)
                .Index(t => t.Profissional_Id);
            
            CreateTable(
                "Academia.Profissional",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        Email = c.String(),
                        TipoProfissional = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Academia.QuantidadeModalidade",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuantidadeAulasNaSemana = c.Int(nullable: false),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        Modalidade_Id = c.Guid(),
                        Plano_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Academia.Modalidade", t => t.Modalidade_Id)
                .ForeignKey("Academia.Plano", t => t.Plano_Id)
                .Index(t => t.Modalidade_Id)
                .Index(t => t.Plano_Id);
            
            CreateTable(
                "Academia.Modalidade",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(),
                        InsertDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Academia.Aluno", "PlanoId", "Academia.Plano");
            DropForeignKey("Academia.QuantidadeModalidade", "Plano_Id", "Academia.Plano");
            DropForeignKey("Academia.QuantidadeModalidade", "Modalidade_Id", "Academia.Modalidade");
            DropForeignKey("Academia.Plano", "LocalTrainamento_Id", "Academia.LocalTreinamento");
            DropForeignKey("Academia.LocalTreinamento", "Profissional_Id", "Academia.Profissional");
            DropForeignKey("Academia.Aluno", "AlunoQueIndicouId", "Academia.Aluno");
            DropIndex("Academia.QuantidadeModalidade", new[] { "Plano_Id" });
            DropIndex("Academia.QuantidadeModalidade", new[] { "Modalidade_Id" });
            DropIndex("Academia.LocalTreinamento", new[] { "Profissional_Id" });
            DropIndex("Academia.Plano", new[] { "LocalTrainamento_Id" });
            DropIndex("Academia.Aluno", new[] { "PlanoId" });
            DropIndex("Academia.Aluno", new[] { "AlunoQueIndicouId" });
            DropTable("Academia.Modalidade");
            DropTable("Academia.QuantidadeModalidade");
            DropTable("Academia.Profissional");
            DropTable("Academia.LocalTreinamento");
            DropTable("Academia.Plano");
            DropTable("Academia.Aluno");
        }
    }
}
