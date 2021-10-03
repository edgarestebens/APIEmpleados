using APIEmpleados.Dtos;
using APIEmpleados.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEmpleados.Helpers.AutoMapper
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<CrearyModificarEmpleadorequest, Empleado>().ReverseMap();
            CreateMap<CrearYModificarDependenciaRequest, Dependencia>().ReverseMap();
        }
       

    }
}
