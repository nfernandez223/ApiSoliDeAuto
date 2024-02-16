using Domain.Entities;
using FluentValidation;
using FluentValidation.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validations
{
    public class SolicitudValidation : AbstractValidator<Solicitud>
    {
        public SolicitudValidation()
        {
            RuleFor(soli => soli.Cliente).NotNull().NotEmpty().WithMessage("El campo Cliente es Invalido");
            RuleFor(soli => soli.Tipo).NotNull().Must(tipo => tipo == "Cobro" || tipo == "Devolucion" || tipo == "Reversa").WithMessage("El campo Tipo de ser Cobro, Devolucion o Reversa");
            RuleFor(soli => soli.Monto).NotNull().GreaterThan(0).WithMessage("El campo Monto es Invalido");
            RuleFor(soli => soli.TipoCliente).NotNull().Must(tipo => tipo == 1 || tipo == 2).WithMessage("El campo TipoCliente debe ser 1 o 2.");
        }
    }
}
