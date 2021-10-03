using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Dtos
{
    public class EliminarDependenciaResponse
    {
        public bool IsSuccessfullProcess { get; set; }
        public List<string> ErrorMessages { get; set; }

        public EliminarDependenciaResponse()
        {
            ErrorMessages = new List<string>();
            IsSuccessfullProcess = false;
        }
    }
}
