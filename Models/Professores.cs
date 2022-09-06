using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    // Na classe Model, haverá todos os atributos/colunas que compõe a classe
    public class Professores
    {
        // métodos declarados com nomes baseados nas colunas da tabela:
        public int Cpf { get; set; } // chave primária

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]// Condição de ignorar atributos para alterações (Update)
                                                                        // Essa condição faz com que os atributos contidos nela, não sejam exibidos
        //[Required(ErrorMessage = "Informar seu nome completo!")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "Informar seu email!")]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informar um email válido!!")]
        public string Email { get; set; }

    }
}
