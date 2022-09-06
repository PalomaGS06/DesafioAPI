using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    //Criação da interface com as funções que serão implementadas
    public interface IAreasRepository
    {
        ICollection<Areas> GetAll(); // Lista da classe Areas utilizada na função de busca, função do tipo Enum ICollection

        //SELECT
        Areas GetById(int id);  // Busca pelo id, função do tipo Areas

        //CREATE
        Areas Insert(Areas areas);  // Cadastrar/criar areas, função do tipo Areas

        //UPDATE
        Areas Update(int id, Areas areas); // Alteração dos atributos, função do tipo Areas 

        //DELETE
        bool Delete(int id);  // Função de exclusão, do tipo booleano
    }
}
