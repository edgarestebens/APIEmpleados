using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class CrearYModificarDependenciaRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int EmpleadoId { get; set; }
    }
}
