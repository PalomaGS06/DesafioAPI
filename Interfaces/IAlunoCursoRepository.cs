using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAlunoCursoRepository
    {
        ICollection<AlunoCurso> GetAll();

        //SELECT
        AlunoCurso GetById(int id);

        //CREATE
        AlunoCurso Insert(AlunoCurso alunoCurso);

        //UPDATE
        AlunoCurso Update(int id, AlunoCurso alunoCurso);

        //DELETE
        AlunoCurso Delete(int id);
    }
}
