//Propriedade do Utente
namespace SGH2425_V3.Models
{
    public class Utente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int NU { get; set; }

        public string Genero { get; set; }

        public DateTime Data_Nascimento { get; set; }

        public int Peso { get; set; }

        public int Telemovel { get; set; }

        public AlergiaUtente AlergiaUtente { get; set; }

        public int AlergiaUtenteId { get; set; }
    }
}
