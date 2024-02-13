using Application.Interfaces;
using Domain.Entities;
using Domain.Validations;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ApiSoliDeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public SolicitudController(IMessageService messageSenderService)
        {
            _messageService = messageSenderService;
        }

        [HttpPost]
        public IActionResult SolicitarAutorizacion([FromBody] Solicitud solicitud)
        {

            StringBuilder errorsSolicitud = new();
            //Validar Request
            SolicitudValidation validationEvent = new(); 
            ValidationResult result = validationEvent.Validate(solicitud);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    errorsSolicitud.Append($"{error.ErrorMessage}");
                    errorsSolicitud.Append('\n');
                }

                return BadRequest(errorsSolicitud.ToString());
            }
            else {
                Random random = new Random();
                int min = 100; 
                int max = 999999; 
                int numeroAleatorio = random.Next(min, max);
                solicitud.IdSolicitud = numeroAleatorio;
                _messageService.SendMessage(solicitud);

                return Ok($"Solicitud de autorización recibida correctamente.\n IdSolicitud: {solicitud.IdSolicitud}");
            }
        }
    }
}
