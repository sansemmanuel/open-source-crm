using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Egresos
    {
        public int egresoId { get; set; }
        public int transaccionId { get; set; }
        public decimal egreso { get; set; }
        public string descripcion { get; set; }
        public decimal saldoActual { get; set; }
        public int sueldoId { get; set; }
        public decimal saldoAnterior { get; set; }
    }
}
