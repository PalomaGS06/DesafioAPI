using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface ICursosRepository
    {
        ICollection<Cursos> GetAll();

        //SELECT
        Cursos GetById(int id);

        //CREATE
        Cursos Insert(Cursos cursos);

        //UPDATE
        Cursos Update(int id, Cursos cursos);

        //DELETE
        bool Delete(int id);
    }
}
