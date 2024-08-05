using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Elemento
    {
    public int Id { get; set; }
        //ffffff
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public string Marca { get; set; }
        public override string ToString()
        {
            return Descripcion;
        }

    }
}
