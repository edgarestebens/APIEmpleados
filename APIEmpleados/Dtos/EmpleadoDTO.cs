using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
    }
}
