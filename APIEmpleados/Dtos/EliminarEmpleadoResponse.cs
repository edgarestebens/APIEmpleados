using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class EliminarEmpleadoResponse
    {
        public bool IsSuccessfullProcess { get; set; }
        public List<string> ErrorMessages { get; set; }

        public EliminarEmpleadoResponse()
        {
            ErrorMessages = new List<string>();
            IsSuccessfullProcess = false;
        }
    }
}
