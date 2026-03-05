using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.Domains;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogAlteracaoJogoController : Controller
    {
        private readonly LogAlteracaoJogoService _service;

        public LogAlteracaoJogoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("jogo/{id}")]
        public ActionResult ListarPorJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }
    }
}
