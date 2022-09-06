using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

// Na classe Model, haverá todos os atributos/colunas que compõe a classe
namespace APICursosGratuitos.Models
{
    public class Areas
    {
        // métodos declarados com nomes baseados nas colunas da tabela:
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Condição de ignorar atributos para alterações (Update)
        public int Id { get; set; } // chave primária


        [Required(ErrorMessage = "Favor, informe uma area!")] //mensagem de erro para campo obrigatório
        public string Area { get; set; }
                
        public string Imagem { get; set; }
    }
}
