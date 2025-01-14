using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SGH2425_V3.Models;

namespace SGH2425_V3.Data
{
    public class ApplicationDbContext : IdentityDbContext<UtilizadorAplicacao>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        //public DbSet<MovimentacaoEViewModel> movimentacaoEViewModels { get; set; }
        public DbSet<UtilizadorAplicacao> Conta { get; set; }
        public DbSet<Tipo_Medicamento> TipoMedicamento { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
        public DbSet<Utente> Utente { get; set; }

        public DbSet<AlergiaUtente> AlergiaUtente { get; set; }

        public DbSet<Medico>Medicos { get; set; }

        public DbSet<Solicitar> Solicitacao { get; set; }

        public DbSet<Prescricao> Prescricoes { get; set; }

        public DbSet<Movimento> Movimento { get; set; }

        public DbSet<Farmaceutico> Farmaceuticos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);

            builder.Entity<Solicitar>()
            .HasNoKey(); // Define a entidade sem chave primária

            // Configuração da chave composta para a tabela Prescricao e Medicamento
            builder.Entity<Solicitar>()
            .HasKey(pm => new { pm.PrescricaoId, pm.MedicamentoId });

            // Configuração do relacionamento entre Movimento e Solicitar
            builder.Entity<Movimento>()
                .HasOne(m => m.Solicitar) // Relaciona Movimento com Solicitar
                .WithMany() // Muitos Movimento podem usar o mesmo Solicitar
                .HasForeignKey(m => new { m.PrescricaoId, m.MedicamentoId }) // FK composta
                .OnDelete(DeleteBehavior.Restrict); // Não permitir exclusão em cascata

            // Configuração do Relacionamento Entre a tabela Utilizadores e Prescrição
            builder.Entity<Movimento>()
            .HasOne(pc => pc.RegistradoPor)
            .WithMany()
            .HasForeignKey(pc => pc.RegistradoPorId)
            .OnDelete(DeleteBehavior.Restrict);


            // Configuração do relacionamento Muitos para Muitos entre Prescricao e MedicamentoFornecedor
            builder.Entity<Solicitar>()
                .HasOne(pm => pm.Medicamento)
                .WithMany(p => p.SolicitarMedicamento)
                .HasForeignKey(pm => pm.MedicamentoId);

            // Configuração do Relacionamento Muitos para Muitos Entre a tabela Utilizadores e Solicitação
            builder.Entity<Solicitar>()
             .HasOne(pm => pm.Prescricao)
             .WithMany(pp => pp.SolicitarMedicamento)
             .HasForeignKey(pm => pm.PrescricaoId);

            // Configuração do Relacionamento Entre a tabela Utilizadores e Prescrição
            builder.Entity<Prescricao>()
            .HasOne(pc => pc.RegistradoPor)
            .WithMany()
            .HasForeignKey(pc => pc.RegistradoPorId)
            .OnDelete(DeleteBehavior.Restrict);

            // Configuração do Relacionamento Entre a tabela Utilizadores e Solicitarção
            builder.Entity<Solicitar>()
            .HasOne(ps => ps.RegistradoPor)
            .WithMany()
            .HasForeignKey(ps => ps.RegistradoPorId)
            .OnDelete(DeleteBehavior.Restrict);

            // Configuração do Relacionamento Entre a tabela Utente e Alergias
            builder.Entity<Utente>()
             .HasOne(u => u.AlergiaUtente)
              .WithMany()
             .HasPrincipalKey(u => u.Id)
              .OnDelete(DeleteBehavior.Restrict);

            // Configuração do Relacionamento Entre a tabela Medicamento e Tipo de Medicamento
            builder.Entity<Medicamento>()
                .HasOne(t => t.TipoMedicamento)
                .WithMany()
                .HasPrincipalKey(t => t.Id)
                .OnDelete(DeleteBehavior.Restrict);


            // Configuração do Relacionamento Entre a tabela Medicamento e Utilizadores
            builder.Entity<Medicamento>()
               .HasOne(c => c.RegistradoPor)
               .WithMany()
               .HasForeignKey(c => c.RegistradoPorId)
               .OnDelete(DeleteBehavior.Restrict);



        }
     
    }
}

