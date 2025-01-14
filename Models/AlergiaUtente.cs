//Propriedades da Alergia do Utente
namespace SGH2425_V3.Models
{
    public class AlergiaUtente
    {
        public int Id { get; set; }

        public string Tipo_Alergia { get; set; }

        public string Descricao { get; set; }

        public DateTime Data_Ultima_Alergia { get; set; }
    }
}
