using System.ComponentModel.DataAnnotations;

namespace APICursosGratuitos.Models
{
    public class Aulas
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Informar o título da aula!")]
        public string Titulo { get; set; }
        public string Ementa { get; set; }
        public int Duracao { get; set; }                
        public Cursos Curso { get; set; }               
        public Professores Professor { get; set; }
    }
}
