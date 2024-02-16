using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContextMgmt
    {
        void UpdateSolicitud(Solicitud solicitud);
        Solicitud VerificarEstado(int idSolicitud);
        Solicitud VerificarAutorizacion(int idSolicitud);
    }
}
