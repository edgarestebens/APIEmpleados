using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class CrearyModificarEmpleadoResponse
    {
        public int EmpleadoId { get; set; }
        public bool IsSuccessfullProcess { get; set; }
        public List<string> ErrorMessages { get; set; }

        public CrearyModificarEmpleadoResponse()
        {
            ErrorMessages = new List<string>();
            IsSuccessfullProcess = false;
        }
    }
}
