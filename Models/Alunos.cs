using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    public class Alunos
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Ra { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu usuário!")]
        public string Usuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu nome completo!")]
        public string Nome { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //Required(ErrorMessage = "Informar seu CPF!")]
        //[RegularExpression("..+\\..+\\-.+", ErrorMessage = "Informar seu CPF com as separações!!")]
        [MaxLength(14)]
        public string Cpf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu email!")]
       // [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informar um email válido!!")]
        public string Email { get; set; }

 
        [Required(ErrorMessage = "Informar sua senha!")]
        [MinLength(12)] // Colocar senha de até 8 dígitos 
                        // definindo um valor mínimo para a senha
        public string Senha { get; set; }

    }
}
