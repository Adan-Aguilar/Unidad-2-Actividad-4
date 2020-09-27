using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace U2A4.Models
{
    public class InformacionPeliculasViewModel
    {
        public string Nombre { get; set; }
        public string NombreOriginal { get; set; }
        public DateTime?  FechaEstreno { get; set; }
        public string Descripcion { get; set; }
        public int Id { get; set; }

        public IEnumerable<Apariciones> Apariciones { get; set; }

    }
}
