using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Memberships
    {
        public int membresiaId { get; set; }
        public DateTime inicioMembresia { get; set; }
        public DateTime finMembresia { get; set; }
        public bool vencidoMembresia { get; set; }
        public int clienteId { get; set; }
        public int tipoMembresiaId { get; set; }
        public bool pagoMembresia { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string telefonoCliente {  get; set; }   
        public bool pagoProgramadoMembresia { get; set; }
        public bool pagoPendienteMembresia { get; set; }
        public bool pagoRechazadoMembresia { get; set; }

    }
}
