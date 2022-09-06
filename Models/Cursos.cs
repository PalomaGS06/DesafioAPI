using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    public class Cursos
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Informar o nome do curso!")]
        public string Nome { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar as horas de curso!")]
        public int CargaHoraria { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar a area do curso!")]

        public Areas Area { get; set; }
        public string Imagem { get; set; }
    }
}
