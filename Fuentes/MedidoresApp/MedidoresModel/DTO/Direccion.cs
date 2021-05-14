using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DTO
{
    public class Direccion
    {
        private int codigoPostal;
        private string detalle;
        private int nro;
        private string adicional;

        public int CodigoPostal { get => codigoPostal; set => codigoPostal = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public int Nro { get => nro; set => nro = value; }
        public string Adicional { get => adicional; set => adicional = value; }
    }
}
