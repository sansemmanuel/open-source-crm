using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class FichaMedica
    {    
        public int FichaId { get; set; }

        public int ClienteId { get; set; }

        public bool? Fumador { get; set; }
        public bool? Cardiopatias { get; set; }
        public bool? Respiratorios { get; set; }
        public bool? Convulsiones { get; set; }
        public bool? Diabetes { get; set; }
        public bool? AlteracionesSanguineas { get; set; }
        public bool? AfeccionesAuditivas { get; set; }
        public bool? Cirujias { get; set; }
        public bool? Alergias { get; set; }
        public bool? Fracturas { get; set; }
        public bool? Vitaminas { get; set; }
        public bool? Colesterol { get; set; }
        public bool? Obesidad { get; set; }

        public string FichaObservacion { get; set; }

    }
}
