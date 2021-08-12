namespace Prueba.Infrastructure.Crosscutting
{
    public class TraceLog
    {
        public string Pais { get; set; }

        public string IdTransaccion { get; set; }

        public string Terminal { get; set; }

        public string Usuario { get; set; }

        public string Aplicacion { get; set; }

        public string NombreInterface { get; set; }

        public string SistemaOrigen { get; set; }

        public string SistemaDestino { get; set; }

        public string FechaTransaccion { get; set; }

        public string TiempoProceso { get; set; }

        public string EntradaProceso { get; set; }

        public string RespuestaProceso { get; set; }

        public string TramaBackend { get; set; }

        public string Estado { get; set; }
    }
}
