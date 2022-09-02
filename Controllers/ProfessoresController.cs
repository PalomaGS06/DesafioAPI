using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using APICursosGratuitos.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessoresController : ControllerBase
    {
        private IProfessoresRepository _profsRepository;
        public ProfessoresController(IProfessoresRepository profsRepository)
        {
            _profsRepository = profsRepository;
        }
        // POST - Cadastrar
        /// <summary>
        /// Cadastra os professores na aplicação
        /// </summary>
        /// <param name="Professores">Dados dos professores</param>
        /// <returns>Dados do prof cadastrados com sucesso</returns>

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Professores Professores)
        {
            try
            {
                _profsRepository.Insert(Professores);
                return Ok(Professores);

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
        /// Lista todos os professores existentes
        /// </summary>
        /// <returns>Lista de Professores</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var prof = _profsRepository.GetAll();
                return Ok(prof); //retorna a lista de professores

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
                var buscarProf = _profsRepository.GetById(Cpf);
                if (buscarProf is null)
                {
                    return NotFound(new
                    {
                        msg = "CPF inválido!"
                    });
                }

                var profEditado = _profsRepository.Update(Cpf, Professor);
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

                var buscarProf = _profsRepository.GetById(Cpf);
                if (buscarProf is null)
                {
                    return NotFound(new
                    {
                        msg = "CPF inválido!"
                    });
                }

                _profsRepository.Delete(Cpf);

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
