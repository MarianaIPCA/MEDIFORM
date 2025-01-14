//Propriedades da Classe Prescricao
using SGH2425_V3.Data.Migrations;
using SGH2425_V3.Models;

namespace SGH2425_V3.Models
{
    public class Prescricao
    {
        public int Id { get; set; }

        public string CodigoPrescricao { get; set; }

        public DateTime DataPrescricao { get; set; } = DateTime.Now;

        public DateTime ValidadePrescricao { get; set; }

        public string Estado { get; set; }

        public string Descricao { get; set; }

        public Utente Utentes { get; set; }

        public int UtenteId { get; set; }

        public string RegistradoPorId { get; set; }

        public UtilizadorAplicacao RegistradoPor { get; set; }

        public ICollection<Solicitar> SolicitarMedicamento { get; set; }

    }
}
