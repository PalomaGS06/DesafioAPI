using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    // Na classe Model, haverá todos os atributos/colunas que compõe a classe
    public class Alunos
    {
        // métodos com nomes baseados nas colunas da tabela:
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]// Condição de ignorar atributos para alterações (Update)
                                                                        // Essa condição faz com que os atributos contidos nela, não sejam exibidos
        public int Ra { get; set; } // chave primária

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu usuário!")]  // Required serve como campo obrigatório para preenchimento
        public string Usuario { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu nome completo!")]
        public string Nome { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //Required(ErrorMessage = "Informar seu CPF!")]
        //[RegularExpression("..+\\..+\\-.+", ErrorMessage = "Informar seu CPF com as separações!!")] //condições e restrições
        [MaxLength(14)] //maximo de 14 caracteres
        public string Cpf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar seu email!")]
       // [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informar um email válido!!")]
        public string Email { get; set; }

 
        [Required(ErrorMessage = "Informar sua senha!")]
        [MinLength(12)] // Colocar senha de até 12 dígitos 
                        // definindo um valor mínimo para a senha
        public string Senha { get; set; }

    }
}
