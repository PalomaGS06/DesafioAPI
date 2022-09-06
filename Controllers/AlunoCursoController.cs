using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    //rota criada para a url de entrada
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoCursoController : ControllerBase //Controlador criado fazendo herança com o ControllerBase, no modo MVC sem o View
    {
        private IAlunoCursoRepository _alunoCursoRepository; //criação de um objeto chamado '_alunoCursoRepositor'

        //Método construtor criado, levando como parâmetro 'alunoCursoRepository'
        public AlunoCursoController(IAlunoCursoRepository alunoCursoRepository)
        {
            _alunoCursoRepository = alunoCursoRepository;
        }


        // POST - Cadastrar   //Dados para as informaçoes das funções de Controller
        /// <summary>
        /// Cadastra os ids dos alunos e cursos //quais as colunas e informações que serão cadastradas
        /// </summary>
        /// <param name="alunoCurso">Dados dos alunos e cursos </param>
        /// <returns>Ids dos alunos e cursos cadastrados com sucesso!</returns>  //mensagem de retorno após cadastrar as informações da classe

        [HttpPost] //Rota de ação http de inserção (POST)   
        public IActionResult Cadastrar([FromForm] AlunoCurso alunoCurso)
            //Fromform serve para deixar o layout do swagger com aparencia                                                                    formulário
            
        {   //uso da exceção try catch
            try
            {                
                _alunoCursoRepository.Insert(alunoCurso);//parâmetro 'alunocurso' passado como argumento dentro da função Insert
                return Ok(alunoCurso);//retorna esse parametro

            }
            catch (System.Exception e)
            {
                return StatusCode(500, new      //retorna o código e mensagem de erro
                {
                    msg = "Falha na conexão",
                    erro = e.Message,
                });
            }
        }



        // GET - Listar  //Dados para as informaçoes das funções de Controller

        /// <summary>
        /// Lista todas os alunos e cursos existentes 
        /// </summary>
        /// <returns>Lista de Alunos e Cursos</returns>
        /// 
        [HttpGet]  //Rota de ação http de busca (GET) 
        public IActionResult Listar()
        {
            try
            {

                var alunoCurso = _alunoCursoRepository.GetAll();
                return Ok(alunoCurso); //retorna a lista de Alunos e Cursos

            }
            catch (System.Exception e)
            {

                return StatusCode(500, new   // erro 500, erro de servidor
                {
                    msg = "Falha na conexão",
                    erro = e.Message,
                });
            }

        }


        // PUT - Alterar       
        /// <summary>
        /// Altera um id
        /// </summary>
        /// <param name="id">Id do aluno e curso</param>
        /// <param name="alunoCurso">Todas as informações do aluno e curso </param>
        /// <returns>AlunoCurso Alterado!</returns>

        [HttpPut("{id}")] //Rota de ação http de alteração através do id (PUT) 
        public IActionResult Alterar(int id, AlunoCurso alunoCurso)
        {
            try
            {
                var buscarAlunoCurso = _alunoCursoRepository.GetById(id);
                if (buscarAlunoCurso is null)
                {
                    return NotFound(new
                    {
                        msg = "IDs inválidos!"  //mensagem de erro
                    });
                }

                var AlunoCursoAlterado = _alunoCursoRepository.Update(id, alunoCurso);
                return Ok(alunoCurso);

            }
            catch (InvalidOperationException e)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na conexão...",
                    erro = e.Message,
                });
            }
            catch (SqlException e)
            {
                return StatusCode(500, new
                {
                    msg = "Falha na sintaxe do código SQL...",
                    erro = e.Message,
                });
            }
            catch (System.Exception e)
            {

                return StatusCode(500, new
                {
                    msg = "Falha na definição do código",
                    erro = e.Message,
                });
            }
        }



        // DELETE - Excluir
        /// <summary>
        /// Exclui um aluno e curso da aplicação
        /// </summary>
        /// <param name="id">Ids dos alunos e cursos</param>
        /// <returns>Mensagem de exclusão!</returns>

        [HttpDelete("{id}")] //Rota de ação http de exclusão (DELETE) 

        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarAlunoCurso = _alunoCursoRepository.GetById(id);
                if (buscarAlunoCurso is null)
                {
                    return NotFound(new
                    {
                        msg = " ID inválido!"
                    });
                }

                _alunoCursoRepository.Delete(id);

                return Ok(new
                {
                    msg = "Ids excluídos com sucesso!"
                });
            }
            catch (System.Exception e)
            {

                return StatusCode(500, new
                {
                    msg = "Falha na conexão",
                    erro = e.Message,
                });
            }

        }
    }
}
