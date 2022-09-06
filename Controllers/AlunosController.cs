using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    //rota criada para a url de entrada
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunosRepository _alunosRepository;
        public AlunosController(IAlunosRepository alunosRepository)
        {
            _alunosRepository = alunosRepository;
        }



        // POST - Cadastrar
        /// <summary>
        /// Cadastra os alunos nos cursos
        /// </summary>
        /// <param name="Aluno">Dados dos alunos</param>
        /// <returns>Dados do aluno cadastrado</returns>

        [HttpPost]  //Rota de ação http de busca (GET)
        public IActionResult Cadastrar( [FromForm] Alunos Aluno)
        {
            try
            {
                _alunosRepository.Insert(Aluno);
                return Ok(Aluno);
                
            }
            catch (System.Exception e)
            {
                return StatusCode(500, new      //retorna o código e mensagem
                {
                    msg = "Falha na conexão",
                    erro = e.Message,
                });
            }
        }



        // GET - Listar
        /// <summary>
        /// Lista todos os alunos existentes
        /// </summary>
        /// <returns>Lista do Aluno</returns>
        [HttpGet]  //Rota de ação http de busca (GET)
        public IActionResult Listar()
        {
            try
            {

                var alunos = _alunosRepository.GetAll();
                return Ok(alunos); //retorna a lista de alunos

            }
            catch (System.Exception e)
            {

                return StatusCode(500, new
                {
                    msg = "Falha na conexão",  //retorna o código e mensagem de erro
                    erro = e.Message,
                });
            }

        }


        // PUT - Alterar
        /// <summary>
        /// Altera a senha de um aluno
        /// </summary>
        /// <param name="RA"> RA do Aluno</param>
        /// <param name="Aluno">Todas as informações do aluno</param>
        /// <returns>Aluno Alterado!</returns>

        [HttpPut("{RA}")] //Rota de ação http de alteração através do RA (id da classe Aluno) (PUT)
        public IActionResult Alterar(int RA, Alunos Aluno)
        {
            try
            {
                var buscarAluno = _alunosRepository.GetById(RA);
                if (buscarAluno is null)
                {
                    return NotFound(new
                    {
                        msg = "RA inválido!"
                    });
                }

                var alunoEditado = _alunosRepository.Update(RA, Aluno);
                return Ok(Aluno);

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
        /// Exclui um aluno da aplicação
        /// </summary>
        /// <param name="RA">RA do aluno</param>
        /// <returns>Mensagem de exclusão</returns>

        [HttpDelete("{RA}")] //Rota de ação http de exclusão (DELETE)  

        public IActionResult Deletar(int RA)
        {
            try
            {

                var buscarAluno = _alunosRepository.GetById(RA);
                if (buscarAluno is null)
                {
                    return NotFound(new
                    {
                        msg = "RA inválido!"
                    });
                }

                _alunosRepository.Delete(RA);

                return Ok(new
                {
                    msg = "Aluno deletado com sucesso!"
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
