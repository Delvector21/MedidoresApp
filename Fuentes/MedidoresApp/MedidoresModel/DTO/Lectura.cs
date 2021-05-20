using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresModel.DTO
{
    public class Lectura
    {
        private int nroSerie;
        private DateTime fecha;
        private string tipo;
        private int valor;
        private int estado;

        public int NroSerie { get => nroSerie; set => nroSerie = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Valor { get => valor; set => valor = value; }
        public int Estado { get => estado; set => estado = value; }

        
    }
}
