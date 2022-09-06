using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAlunoCursoRepository
    {
        //Criação da interface com as funções que serão implementadas
        ICollection<AlunoCurso> GetAll(); // Lista da classe AlunoCurso utilizada na função de busca, função do tipo Enum ICollection

        //SELECT
        AlunoCurso GetById(int id); // Busca pelo id, função do tipo AlunoCurso

        //CREATE
        AlunoCurso Insert(AlunoCurso alunoCurso); // Cadastrar/criar alunoCurso, função do tipo AlunoCurso

        //UPDATE
        AlunoCurso Update(int id, AlunoCurso alunoCurso); // Alteração dos atributos, função do tipo AlunoCurso

        //DELETE
        bool Delete(int id); // Função de exclusão, do tipo booleano
    }
}
