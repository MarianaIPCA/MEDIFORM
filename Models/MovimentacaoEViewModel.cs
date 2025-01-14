namespace SGH2425_V3.Models
{
    public class MovimentacaoEViewModel
    {
        public string TipoMov { get; set; }

        public int QtdMov { get; set; }

        public Prescricao Prescricao { get; set; }

        public Medicamento Medicamento { get; set; }

        public List<Prescricao> PrescricaoList { get; set; }

        public List<Medicamento> MedicamentoList { get;set; }
    }
}
