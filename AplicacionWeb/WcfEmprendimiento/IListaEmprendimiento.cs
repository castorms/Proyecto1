using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfEmprendimiento
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IListaEmprendimiento
    {

        [OperationContract]
        List<NuevoEmprendimiento> GetAll();

        [OperationContract]
        NuevoEmprendimiento GetEmprendimiento(string identificador);

        // TODO: agregue aquí sus operaciones de servicio
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class NuevoEmprendimiento
    {
        string identificador;
        [DataMember]
        public string Identificador
        {
            get { return identificador; }
            set { identificador = value; }
        }

        string titulo;
        [DataMember]
        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        string descripcion;
        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }



    }
}
