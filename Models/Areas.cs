using System.ComponentModel.DataAnnotations;

namespace APICursosGratuitos.Models
{
    public class Areas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor, informe uma area!")]
        public string Area { get; set; }    
    }
}
