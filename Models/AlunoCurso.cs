using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    // Na classe Model, haverá todos os atributos/colunas que compõe a classe
    public class AlunoCurso
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] // Condição de ignorar atributos para alterações (Update)
                                                                         // Essa condição faz com que os atributos contidos nela, não sejam exibidos

        // métodos declarados com nomes baseados nas colunas da tabela:
        public int Id { get; set; } //chave primária
        public int AlunoRa { get; set; } //chave estrangeira
        public int CursoId { get; set; } //chave estrangeira
    }
}
