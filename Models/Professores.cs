using System.ComponentModel.DataAnnotations;

namespace APICursosGratuitos.Models
{
    public class Professores
    {
        public int Cpf { get; set; }

        [Required(ErrorMessage = "Informar seu nome completo!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informar seu email!")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informar um email válido!!")]
        public string Email { get; set; }

    }
}
