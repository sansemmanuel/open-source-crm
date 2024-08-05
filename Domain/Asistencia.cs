using System;

namespace Dominio
{
    public class Asistencia
    {
        public int asistenciaId { get; set; }
        public DateTime fechaAsistencia { get; set; }
        public bool presenteCheck { get; set; }
        public int clienteId { get; set; }

    }
}
