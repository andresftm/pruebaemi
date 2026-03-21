using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Entities
{
    /// <summary>
    /// Clase que representa la entidad "projects" en el dominio de la aplicación. Esta clase contiene propiedades que corresponden a los campos de la tabla "projects" en la base de datos, así como una lista de objetos "EmployeeProject" que representan la relación entre proyectos y empleados.
    /// </summary>
    public class projects
    {
        public int id { get; set; }
        public string name { get; set; }

        public List<EmployeeProject> EmployeeProjects { get; set; }
    }
}
