using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAlunosRepository
    {
        //SELECT
        ICollection<Alunos> GetAll();

        Alunos GetById(int Ra);

        //CREATE
        Alunos Insert(Alunos alunos);

        //UPDATE
        Alunos Update(int Ra, Alunos alunos);

        //DELETE
        Alunos Delete(int Ra);


    }
}
