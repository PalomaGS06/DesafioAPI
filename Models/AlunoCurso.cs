using System.Text.Json.Serialization;

namespace APICursosGratuitos.Models
{
    public class AlunoCurso
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public int AlunoRa { get; set; }
        public int CursoId { get; set; }
    }
}
