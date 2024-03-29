﻿using Application.Interfaces;
using Domain.Entities;
using InfraEstructure.Persistence;

namespace InfraEstructure.Services
{
    public class ContextMgmt: IContextMgmt
    {
        private readonly AppDbContext _dbContext;

        public ContextMgmt(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void UpdateSolicitud(Solicitud solicitud)
        {
            _dbContext.Update(solicitud);
            _dbContext.SaveChanges();
        }

        public Solicitud VerificarEstado(int idSolicitud)
        {
            var solicitud = _dbContext.Solicitud
            .Where(s => s.IdSolicitud == idSolicitud).FirstOrDefault();

            return solicitud;
        }

        public Solicitud VerificarAutorizacion(int idSolicitud)
        {
            var solicitud = _dbContext.Solicitud
            .Where(s => s.Estado == "Pendiente" && s.IdSolicitud == idSolicitud).FirstOrDefault();

            return solicitud;
        }
    }
}
