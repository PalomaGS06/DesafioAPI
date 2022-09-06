using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    // Na classe Model, haverá todos os atributos/colunas que compõe a classe
    public class Cursos
    {
        // métodos declarados com nomes baseados nas colunas da tabela:
        public int Id { get; set; } // chave primária

        [Required(ErrorMessage = "Informar o nome do curso!")]  //mensagem de erro em campo obrigatório
        public string Nome { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Condição de ignorar atributos para alterações (Update)
        //[Required(ErrorMessage = "Informar as horas de curso!")]
        public int CargaHoraria { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //[Required(ErrorMessage = "Informar a area do curso!")]

        public Areas Area { get; set; } // chave estrangeira
        public string Imagem { get; set; }
    }
}
