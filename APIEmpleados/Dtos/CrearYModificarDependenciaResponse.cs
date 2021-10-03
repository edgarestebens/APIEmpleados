using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class CrearYModificarDependenciaResponse
    {
        public int DependenciaId { get; set; }
        public bool IsSuccessfullProcess { get; set; }
        public List<string> ErrorMessages { get; set; }

        public CrearYModificarDependenciaResponse()
        {
            ErrorMessages = new List<string>();
            IsSuccessfullProcess = false;
        }
    }
}
