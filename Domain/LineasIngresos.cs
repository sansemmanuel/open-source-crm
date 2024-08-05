using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class LineasIngresos
    {
        public int lineaIngresoId { get; set; }
        public int ingresoId { get; set; }
        public int tipoMembresiaId { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }


    }
}
