using APICursosGratuitos.Models;
using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private AlunosRepository repositorio = new AlunosRepository();
        

        // POST - Cadastrar
        /// <summary>
        /// Cadastra os alunos nos cursos
        /// </summary>
        /// <param name="alunos">Dados dos alunos</param>
        /// <returns>Dados do aluno cadastrado</returns>

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Alunos alunos, IFormFile arquivo)
        {
            try
            {
                repositorio.Insert(alunos);
                return Ok(alunos);
                
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
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var alunos = repositorio.GetAll();
                return Ok(alunos); //retorna a lista de alunos

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


        // PUT - Alterar
        /// <summary>
        /// Altera os dados de um aluno
        /// </summary>
        /// <param name="ra"> RA do Aluno</param>
        /// <param name="aluno">Todas as informações do aluno</param>
        /// <returns>Aluno Alterado!</returns>

        [HttpPut("{ra}")]
        public IActionResult Alterar(int ra, Alunos aluno)
        {
            try
            {
                var buscarAluno = repositorio.GetById(ra);
                if (buscarAluno == null)
                {
                    return NotFound();
                }

                var alunoEditado = repositorio.Update(ra, aluno);
                return Ok(aluno);

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
        /// <param name="ra">RA do aluno</param>
        /// <returns>Mensagem de exclusão</returns>

        [HttpDelete("{ra}")]

        public IActionResult Deletar(int ra)
        {
            try
            {

                var buscarAluno = repositorio.GetById(ra);
                if (buscarAluno == null)
                {
                    return NotFound();
                }

                repositorio.Delete(ra);

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
