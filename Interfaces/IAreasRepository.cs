using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAreasRepository
    {
        ICollection<Areas> GetAll();

        //SELECT
        Areas GetById(int id);

        //CREATE
        Areas Insert(Areas areas);

        //UPDATE
        Areas Update(int id, Areas areas);

        //DELETE
        Areas Delete(int id);
    }
}
