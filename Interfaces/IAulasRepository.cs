using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAulasRepository
    {

        ICollection<Aulas> GetAll();

        //SELECT
        Aulas GetById(int id);

        //CREATE
        Aulas Insert(Aulas aulas);

        //UPDATE
        Aulas Update(int id, Aulas aulas);

        //DELETE
        Aulas Delete(int id);
    }
}
