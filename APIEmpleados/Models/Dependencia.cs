using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Models
{
    public class Dependencia
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }


    }
}
