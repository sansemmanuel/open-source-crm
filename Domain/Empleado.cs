using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Empleado
    {
        public int empleadoId { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoEmpleado { get; set; }
        public string dniEmpleado { get; set; }
        public DateTime nacimientoEmpleado { get; set; }
        public DateTime contratacionEmpleado { get; set; }
        public string generoEmpleado { get; set; }
        public string telefonoEmpleado { get; set; }
        public string emailEmpleado { get; set; }
        public int puestoId { get; set; }
    }
}
