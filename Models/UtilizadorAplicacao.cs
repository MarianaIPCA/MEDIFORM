using Microsoft.AspNetCore.Identity;

namespace SGH2425_V3.Models
{
    public class UtilizadorAplicacao:IdentityUser 
    {
        public string Name { get; set; }

        public string NivelAcesso { get; set; }
    }
}
