using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using APICursosGratuitos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private ICursosRepository _cursosRepository;
        public CursosController(ICursosRepository cursosRepository)
        {
            _cursosRepository = cursosRepository;
        }
        // POST - Cadastrar
        /// <summary>
        /// Cadastra os cursos
        /// </summary>
        /// <param name="cursos">Dados dos cursos </param>
        /// <returns>Dados dos cursos cadastrados com sucesso!</returns>

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Cursos cursos, IFormFile arquivo)
        {
            try
            {
                #region Upload de Imagem
                string[] extensoesPermitidas = { "jpeg", "jpg", "png", "svg" };
                string uploadResultado = Upload.UploadFile(arquivo, extensoesPermitidas, "Images");

                if (uploadResultado == "")
                {
                    return BadRequest("Arquivo não encontrado ou extensão não permitida!");
                }
                else if (uploadResultado is null)
                {
                    cursos.Imagem = "Null";
                }
                else
                {
                    cursos.Imagem = uploadResultado;
                }
                #endregion

                _cursosRepository.Insert(cursos);
                return Ok(cursos);

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
        /// Lista todas os cursos existentes 
        /// </summary>
        /// <returns>Lista de Cursos</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var curso = _cursosRepository.GetAll();
                return Ok(curso); //retorna a lista de cursos

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
        /// Altera um curso e imagem
        /// </summary>
        /// <param name="id">Id do curso</param>
        /// <param name="cursos">Todas as informações do curso </param>
        /// <returns>Curso Alterado!</returns>

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Cursos cursos)
        {
            try
            {              

                var buscarCurso = _cursosRepository.GetById(id);
                if (buscarCurso is null)
                {
                    return NotFound(new
                    {
                        msg = "ID inválido!"
                    });
                }

                var CursoAlterado = _cursosRepository.Update(id, cursos);
                return Ok(cursos);

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
        /// Exclui um curso da aplicação
        /// </summary>
        /// <param name="id">Id do curso</param>
        /// <returns>Mensagem de exclusão!</returns>

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarCurso = _cursosRepository.GetById(id);
                if (buscarCurso is null)
                {
                    return NotFound(new
                    {
                        msg = " ID inválido!"
                    });
                }

                _cursosRepository.Delete(id);

                return Ok(new
                {
                    msg = "Curso excluído com sucesso!"
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
