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


    }
}
