using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    // Na classe Model, haverá todos os atributos/colunas que compõe a classe
    public class Aulas
    {
        // métodos declarados com nomes baseados nas colunas da tabela:

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]  // Essa condição faz com que os atributos contidos nela, não sejam exibidos
        public int Id { get; set; } // chave primária


        [Required(ErrorMessage = "Informar o título da aula!")] //mensagem de erro em campo obrigatório
        public string Titulo { get; set; }
        public string Imenta { get; set; }
               
        public int Duracao { get; set; }


        public Cursos Curso { get; set; } //chave estrangeira

        public Professores Professor { get; set; } // chave estrangeira
    }
}
