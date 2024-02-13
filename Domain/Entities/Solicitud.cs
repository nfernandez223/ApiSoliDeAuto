namespace Domain.Entities
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
        public float Monto { get; set; }
        public int TipoCliente { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Observacion { get; set; }
    }
}