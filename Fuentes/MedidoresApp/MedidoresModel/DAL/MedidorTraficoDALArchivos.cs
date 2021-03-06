using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DAL
{
    public class MedidorTraficoDALArchivos : IMedidorTraficoDAL
    {
        private MedidorTraficoDALArchivos()
        {

        }
        private static IMedidorTraficoDAL instancia;

        public static IMedidorTraficoDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorTraficoDALArchivos();
            }
            return instancia;
        }

        private static List<int> medidorTrafico = new List<int>()
        {
            21,22,23,24,25
        };

        public List<int> ObtenerMedidores()
        {
            return medidorTrafico;
        }



    }
}
