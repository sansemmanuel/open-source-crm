using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Transaccion
    {
    public int transaccionId { get; set; }
    public DateTime fechaTransaccion { get; set; }
    public string metodoPago { get; set; }
    }
}
