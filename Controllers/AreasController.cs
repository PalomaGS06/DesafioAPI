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
    //rota criada para a url de entrada
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

        [HttpPost] //Rota de ação http de inserção (POST)
        public IActionResult Cadastrar([FromForm] Areas areas, IFormFile arquivo) 
            //IFormFile é um formulário que serve para fazer o upload de arquivos, extrai atraves de pastas e locais da máquina do usuário 
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
                        msg = "A imagem da àrea é obrigatória! Por favor, inserir uma imagem!"
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
        [HttpGet] //Rota de ação http de busca (GET)
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
        /// Altera uma area e imagem
        /// </summary>
        /// <param name="id">Id da área</param>
        /// <param name="areas">Todas as informações da Área </param>
        /// <returns>Dados Alterados!</returns>

        [HttpPut("{id}")] //Rota de ação http de alteração através do id (PUT)
        public IActionResult Alterar(int id, [FromForm] Areas areas, IFormFile arquivo)
        {
            try
            {
                
                var buscarArea = _areasRepository.GetById(id);
                if (buscarArea is null)
                {
                    return NotFound(new
                    {
                        msg = "ID inválido!"
                    });
                }
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
                        msg = "A imagem da àrea é obrigatória! Por favor, inserir uma imagem!"
                    });
                }
                else
                {
                    areas.Imagem = uploadResultado;
                }
                #endregion


                var AreaAlterada = _areasRepository.Update(id, areas);
                return Ok(areas);

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
        /// Exclui uma área da aplicação
        /// </summary>
        /// <param name="id">Id da área</param>
        /// <returns>Mensagem de exclusão</returns>

        [HttpDelete("{id}")] //Rota de ação http de exclusão (DELETE)

        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarArea = _areasRepository.GetById(id);
                if (buscarArea is null)
                {
                    return NotFound(new
                    {
                        msg = " ID inválido!"
                    });
                }

                _areasRepository.Delete(id);

                return Ok(new
                {
                    msg = "Área excluída com sucesso!"
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
