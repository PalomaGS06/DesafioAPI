using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    public class Areas
    {
        public int Id { get; set; }

       
        [Required(ErrorMessage = "Favor, informe uma area!")]
        public string Area { get; set; }
                
        public string Imagem { get; set; }
    }
}
