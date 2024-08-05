using System;

namespace Dominio
{
    public class Ingresos
    {
        public int ingresoId { get; set; }
        public int clienteId { get; set; }
        public int transaccionId { get; set; }
        public DateTime fechaIngreso { get; set; }
        public decimal saldoEstado { get; set; }
        public int cantidad {  get; set; }  
        public decimal monto { get; set; }  
        public int tipoMembresiaId { get; set; }    


    }
}
