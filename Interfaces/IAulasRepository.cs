using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    public interface IAulasRepository
    {
        //Criação da interface com as funções que serão implementadas
        ICollection<Aulas> GetAll();  // Lista da classe Aulas utilizada na função de busca, função do tipo Enum ICollection

        //SELECT
        Aulas GetById(int id);  // Busca pelo id, função do tipo Aulas

        //CREATE
        Aulas Insert(Aulas aulas);  // Cadastrar/criar aulas, função do tipo Aulas

        //UPDATE
        Aulas Update(int id, Aulas aulas);  // Alteração dos atributos, função do tipo Aulas

        //DELETE
        bool Delete(int id);  // Função de exclusão, do tipo booleano
    }
}
