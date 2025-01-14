
//Relacionamento Muitos Para Muitos entre o Prescicao e Medicamento
namespace SGH2425_V3.Models
{
    public class Solicitar
    {
        public int Qtd_Prescrita { get; set; }

        public string Posologia { get; set; }

        public string Descricao { get; set; }

        public Medicamento Medicamento { get; set; }

        public int MedicamentoId { get; set; }

        public Prescricao Prescricao { get; set; }

        public int PrescricaoId { get; set; }

        public string RegistradoPorId { get; set; }

        public UtilizadorAplicacao RegistradoPor { get; set; }
    }
}
