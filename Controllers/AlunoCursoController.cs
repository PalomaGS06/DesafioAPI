using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoCursoController : ControllerBase
    {
        private IAlunoCursoRepository _alunoCursoRepository;
        public AlunoCursoController(IAlunoCursoRepository alunoCursoRepository)
        {
            _alunoCursoRepository = alunoCursoRepository;
        }
        // POST - Cadastrar
        /// <summary>
        /// Cadastra os ids dos alunos e cursos
        /// </summary>
        /// <param name="alunoCurso">Dados dos alunos e cursos </param>
        /// <returns>Ids dos alunos e cursos cadastrados com sucesso!</returns>

        [HttpPost]
        public IActionResult Cadastrar([FromForm] AlunoCurso alunoCurso)
        {
            try
            {                
                _alunoCursoRepository.Insert(alunoCurso);
                return Ok(alunoCurso);

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
        /// Lista todas os alunos e cursos existentes 
        /// </summary>
        /// <returns>Lista de Alunos e Cursos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var alunoCurso = _alunoCursoRepository.GetAll();
                return Ok(alunoCurso); //retorna a lista de Alunos e Cursos

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
        /// Altera um id
        /// </summary>
        /// <param name="id">Id do aluno e curso</param>
        /// <param name="alunoCurso">Todas as informações do aluno e curso </param>
        /// <returns>AlunoCurso Alterado!</returns>

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, AlunoCurso alunoCurso)
        {
            try
            {
                var buscarAlunoCurso = _alunoCursoRepository.GetById(id);
                if (buscarAlunoCurso is null)
                {
                    return NotFound(new
                    {
                        msg = "IDs inválidos!"
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

        [HttpDelete("{id}")]

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
