using System;

namespace Dominio
{
    public class Member
    {
        public int clienteId { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string dniCliente { get; set; }
        public DateTime nacimientoCliente { get; set; }
        public int pesoCliente { get; set; }
        public decimal alturaCliente { get; set; }
        public string generoCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string emailCliente { get; set; }
      //  public bool activoCliente { get; set; }
    }
}
