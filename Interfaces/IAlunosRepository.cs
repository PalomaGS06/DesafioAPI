using APICursosGratuitos.Models;
using System.Collections.Generic;

namespace APICursosGratuitos.Interfaces
{ 
    //Criação da interface com as funções que serão implementadas
    public interface IAlunosRepository
    {
        //SELECT
        ICollection<Alunos> GetAll(); // Lista da classe Alunos utilizada na função de busca, função do tipo Enum ICollection

        Alunos GetById(int Ra); // Busca pelo id, função do tipo Alunos

        //CREATE
        Alunos Insert(Alunos alunos); // Cadastrar/criar alunos, função do tipo Alunos

        //UPDATE
        Alunos Update(int Ra, Alunos alunos);  // Alteração dos atributos, função do tipo Alunos

        //DELETE
        bool Delete(int Ra); // Função de exclusão, do tipo booleano


    }
}
