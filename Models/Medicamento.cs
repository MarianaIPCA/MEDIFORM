//Propriedades do Medicamento
namespace SGH2425_V3.Models
{
    public class Medicamento
    {
        public int Id { get; set; }

        public string Nome_medicamento { get; set; }

        public string Dosagem { get; set; }

        public int Lote { get; set; }

        public string Localicao { get; set; }

        public int stock { get; set; }

        public int Stock_Min { get; set; }

        public DateTime Data_Producao { get; set; }
        public DateTime Data_Validade { get; set; }
        public DateTime DataRegistro { get; set; }
        public Tipo_Medicamento TipoMedicamento { get; set; }
        public int TipoMedicamentoId { get; set; }
        public UtilizadorAplicacao RegistradoPor { get; set; }
        public string RegistradoPorId { get; set; }
        public ICollection<Solicitar> SolicitarMedicamento { get; set; }


    }
}
