using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso
{
     public interface ActiveRecord

    {

         bool Insertar();
         bool Modificar();
         bool Eliminar();

    }
}
