using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using APICursosGratuitos.Repositories;
using APICursosGratuitos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private IAreasRepository _areasRepository;
        public AreasController(IAreasRepository areasRepository)
        {
            _areasRepository = areasRepository;
        }
        // POST - Cadastrar
        /// <summary>
        /// Cadastra as áreas existentes
        /// </summary>
        /// <param name="areas">Dados das áreas </param>
        /// <returns>Dados das areas cadastradas com sucesso!</returns>

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Areas areas, IFormFile arquivo)
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
                    return BadRequest(new
                    {
                        msg = "A imagem da àrea é obrigatória! Por favor, inserir uma imagem"
                    });
                }
                else
                {
                    areas.Imagem = uploadResultado;
                }
                #endregion

                _areasRepository.Insert(areas);
                return Ok(areas);

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
        /// Lista todas as areas 
        /// </summary>
        /// <returns>Lista de Áreas!</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var area = _areasRepository.GetAll();
                return Ok(area); //retorna a lista de areas

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
        /// Altera o email de um professor
        /// </summary>
        /// <param name="Cpf"> CPF do Professor</param>
        /// <param name="Professor">Todas as informações do Professor</param>
        /// <returns>Email Alterado!</returns>

        [HttpPut("{Cpf}")]
        public IActionResult Alterar(int Cpf, Professores Professor)
        {
            try
            {
                var buscarProf = _areasRepository.GetById(Cpf);
                if (buscarProf is null)
                {
                    return NotFound(new
                    {
                        msg = "CPF inválido!"
                    });
                }

                var profEditado = _areasRepository.Update(Cpf, Professor);
                return Ok(Professor);

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
        /// Exclui um professor da aplicação
        /// </summary>
        /// <param name="Cpf">CPF do professor</param>
        /// <returns>Mensagem de exclusão</returns>

        [HttpDelete("{Cpf}")]

        public IActionResult Deletar(int Cpf)
        {
            try
            {

                var buscarProf = _areasRepository.GetById(Cpf);
                if (buscarProf is null)
                {
                    return NotFound(new
                    {
                        msg = "CPF inválido!"
                    });
                }

                _areasRepository.Delete(Cpf);

                return Ok(new
                {
                    msg = "Professor excluído com sucesso!"
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
