using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    //Criação da interface com as funções que serão implementadas
    public interface IProfessoresRepository
    {
        ICollection<Professores> GetAll(); // Lista da classe Professores utilizada na função de busca, função do tipo Enum ICollection

        //SELECT
        Professores GetById(int Cpf); // Busca pelo id, função do tipo Professores

        //CREATE
        Professores Insert(Professores profs); // Cadastrar/criar profs, função do tipo Professores

        //UPDATE
        Professores Update(int Cpf, Professores profs); // Alteração dos atributos, função do tipo Professores

        //DELETE
        bool Delete(int Cpf); // Função de exclusão, do tipo booleano  
    }
}
