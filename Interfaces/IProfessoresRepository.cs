using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IProfessoresRepository
    {
        ICollection<Professores> GetAll();

        //SELECT
        Professores GetById(int Cpf);

        //CREATE
        Professores Insert(Professores profs);

        //UPDATE
        Professores Update(int Cpf, Professores profs);

        //DELETE
        Professores Delete(int Cpf);
    }
}
