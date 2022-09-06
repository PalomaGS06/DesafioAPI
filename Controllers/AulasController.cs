using APICursosGratuitos.Interfaces;
using APICursosGratuitos.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace APICursosGratuitos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AulasController : ControllerBase
    {
        private IAulasRepository _aulasRepository;
        public AulasController(IAulasRepository aulasRepository)
        {
            _aulasRepository = aulasRepository;
        }
        // POST - Cadastrar
        /// <summary>
        /// Cadastra as aulas
        /// </summary>
        /// <param name="aulas">Dados das aulas </param>
        /// <returns>Dados das aulas cadastrados com sucesso!</returns>

        [HttpPost]
        public IActionResult Cadastrar(Aulas aulas)
        {
            try
            {                
                _aulasRepository.Insert(aulas);
                return Ok(aulas);

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
        /// Lista todas as aulas existentes 
        /// </summary>
        /// <returns>Lista de Aulas</returns>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {

                var aula = _aulasRepository.GetAll();
                return Ok(aula); //retorna a lista de aulas

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
        /// Altera o título e ementa da aula
        /// </summary>
        /// <param name="id">Id da aula</param>
        /// <param name="aulas">Todas as informações da aula </param>
        /// <returns>Aula Alterada!</returns>

        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Aulas aulas)
        {
            try
            {
                var buscarAula = _aulasRepository.GetById(id);
                if (buscarAula is null)
                {
                    return NotFound(new
                    {
                        msg = "ID inválido!"
                    });
                }

                var AulaAlterada = _aulasRepository.Update(id, aulas);
                return Ok(aulas);

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
        /// Exclui uma aulas da aplicação
        /// </summary>
        /// <param name="id">Id da aulas</param>
        /// <returns>Mensagem de exclusão!</returns>

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                var buscarAula = _aulasRepository.GetById(id);
                if (buscarAula is null)
                {
                    return NotFound(new
                    {
                        msg = " ID inválido!"
                    });
                }

                _aulasRepository.Delete(id);

                return Ok(new
                {
                    msg = "Aula deletada com sucesso!"
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
