using MedidoresModel.DAL;
using MedidoresModel.DTO;
using SocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidoresApp
{
    public partial class Program
    {
        private static ClienteSocket clienteSocket;
        private static ILecturaDAL dal = LecturaDALFactory.CreateDal();
        private static IMedidorConsumoDAL dalConsumo = MedidorConsumoDALFactory.CreateDal();


        static bool Menu()
        {
            bool continuar = true;
            //Console.WriteLine("Ingrese opcion del menu:");
            //Console.WriteLine("1.- Modificar Puerto de Comunicación");
            //Console.WriteLine("2.- Mostrar Lecturas de Consumo");
            //Console.WriteLine("3.- Mostrar Lecturas de Trafico");
            string opcion = Console.ReadLine().Trim();
            switch (opcion)
            {
                case "1":
                    ModificarPuerto();
                    break;
                case "2":
                    MostrarLecturasConsumo();
                    break;
                case "3":
                    MostrarLecturasTrafico();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese opcion Válida");
                    break;
            }
            return continuar;
        }

        

        private static void ModificarPuerto()
        {
            Console.WriteLine("Puerto actual: " + ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Ingresar nuevo Puerto:");
            string nuevoPuerto = Console.ReadLine();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["puerto"].Value = nuevoPuerto;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static void MostrarLecturasConsumo()
        {
            try
            {
                List<Lectura> lecturas = dal.ObtenerLecturasConsumo();
                foreach (var item in lecturas)
                {
                    Console.WriteLine(item.Tipo);
                    Console.WriteLine(item.NroSerie);
                    Console.WriteLine(item.Fecha);
                    Console.WriteLine(item.Valor);
                    Console.WriteLine(item.Estado);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("No hay datos almacenados");
            }

        }

        private static void MostrarLecturasTrafico()
        {
            try
            {
                List<Lectura> lecturas = dal.ObtenerLecturasTrafico();
                foreach (var item in lecturas)
                {
                    Console.WriteLine(item.Tipo);
                    Console.WriteLine(item.NroSerie);
                    Console.WriteLine(item.Fecha);
                    Console.WriteLine(item.Valor);
                    Console.WriteLine(item.Estado);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("No hay datos almacenados");
            }


        }

    }
}
