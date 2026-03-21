using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Entities
{
    /// <summary>
    /// Clase que representa la entidad "departments" en el dominio de la aplicación. Esta clase contiene propiedades que corresponden a los campos de la tabla "departments" en la base de datos, así como una lista de empleados asociados a cada departamento.
    /// </summary>
    public class departments
    {

        public int id { get; set; }

        public string name { get; set; }

        public List<employee> Employees { get; set; }
    }
}
