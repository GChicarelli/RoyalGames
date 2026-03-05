using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.JogoDto;
using Royal_Games.Exceptions;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;
        private JogoController(JogoService service)
        {
            _service = service;
        }

        private int obterUsuarioIdLogado()
        {
            string? idtexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idtexto))
            {
                throw new DomainException("Usuario não autenticado");
            }

            return int.Parse(idtexto);
        }       

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> Jogos = _service.Listar();

            return Ok(Jogos);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorID(int id)
        {
            LerJogoDto Jogo = _service.ObterPorID(id);

            if (Jogo == null)
            {
                return NotFound();
            }

            return Ok(Jogo);
        }

        [HttpGet("{id}/imagem")]

        public ActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);

                return File(imagem, "image/jpeg");
            }
            catch (Royal_Games.Exceptions.DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize] 

        public ActionResult Adicionar([FromForm] CriarJogoDto JogoDto)
        {
            try
            {
                int usuarioId = obterUsuarioIdLogado();
                _service.Adicionar(JogoDto, usuarioId);
                return StatusCode(201); 
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        [Consumes("multpart/form-data")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] AtualizarJogoDto JogoDto)
        {
            try
            {
                _service.Atualizar(id, JogoDto);
                return NoContent();

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
