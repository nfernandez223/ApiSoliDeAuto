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
        private readonly IContextMgmt _contextMgmt;

        public SolicitudController(IMessageService messageSenderService, IContextMgmt contextMgmt)
        {
            _messageService = messageSenderService;
            _contextMgmt = contextMgmt;
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

        [HttpGet]
        public IActionResult ConsultarSolicitud([FromQuery] int IdSolicitud)
        {
            //Recibe la solicitud y verifica
            var solicitud = _contextMgmt.VerificarEstado(IdSolicitud);

            if (solicitud != null)
            {
                return Ok(solicitud);
            }
            else
            {
                return BadRequest($"No se encontro solicitud para {IdSolicitud}");
            }
        }

        [HttpPost]
        [Route("autorizar")]
        public IActionResult AutorizarSolicitud([FromBody] AutorizacionRequest autorizacion)
        {

            //Recibe la solicitud y autoriza
            var solicitud = _contextMgmt.VerificarAutorizacion(autorizacion.IdSolicitud);

            if (solicitud != null)
            {
                solicitud.Estado = "Autorizada";
                _contextMgmt.UpdateSolicitud(solicitud);
                return Ok($"Solicitud {autorizacion.IdSolicitud} autorizada");
            }
            else
            {
                return BadRequest($"No se encontro solicitud pendiente a autorizar para {autorizacion.IdSolicitud}");
            }
        }
    }
}
