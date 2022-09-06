using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    public class Aulas
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }


        [Required(ErrorMessage = "Informar o título da aula!")]
        public string Titulo { get; set; }
        public string Imenta { get; set; }
               
        public int Duracao { get; set; }


        public Cursos Curso { get; set; }

        public Professores Professor { get; set; }
    }
}
