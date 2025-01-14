namespace SGH2425_V3.Models
{
    public class Movimento
    {
        public int Id { get; set; }

        public string Tipo_Movimento { get; set; }

        public int Qtd_Movimento { get; set; }
        public DateTime DataMovimento { get; set; }

        public string Descricao { get; set; }

        public Solicitar Solicitar { get; set; }

        public int PrescricaoId { get; set; }

        public int MedicamentoId { get; set; }


        public string RegistradoPorId { get; set; }

        public UtilizadorAplicacao RegistradoPor { get; set; }


    }
}
