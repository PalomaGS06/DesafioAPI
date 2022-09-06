using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{
    //Criação da interface com as funções que serão implementadas
    public interface ICursosRepository
    {
        ICollection<Cursos> GetAll();  // Lista da classe Cursos utilizada na função de busca, função do tipo Enum ICollection

        //SELECT
        Cursos GetById(int id);  // Busca pelo id, função do tipo Cursos

        //CREATE
        Cursos Insert(Cursos cursos);  // Cadastrar/criar cursos, função do tipo Cursos

        //UPDATE
        Cursos Update(int id, Cursos cursos);  // Alteração dos atributos, função do tipo Cursos

        //DELETE
        bool Delete(int id);   // Função de exclusão, do tipo booleano
    }
}
