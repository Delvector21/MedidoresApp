using MedidorSocketApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorSocketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            ClienteSocket clienteSocket = new ClienteSocket(ip, puerto);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectandose al servidor {0} en el puerto {1}", ip, puerto);
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("Ingrese Fecha");
                string fechaString = Console.ReadLine().Trim();
                Console.WriteLine("ingrese Numero de medidor");
                string nroSerie = Console.ReadLine().Trim();
                Console.WriteLine("ingrese tipo de Medidor: 'consumo' o 'trafico'");
                string tipo = Console.ReadLine().Trim();
                string mensaje = fechaString + "|" + nroSerie + "|" + tipo;
                //Console.WriteLine(mensaje);
                clienteSocket.Escribir(mensaje);

                string respuesta = clienteSocket.Leer();
                Console.WriteLine(respuesta);

                if (respuesta.Contains("WAIT"))
                {
                    string estado = "";
                    Console.WriteLine("Ingrese Nro Medidor");
                    string medidor = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese Fecha");
                    string fecha2String = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese Tipo: 'trafico' o 'consumo'");
                    string tipo2 = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese valor");
                    string valorstring = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese Estado (opcional)");
                    estado = Console.ReadLine().Trim();
                    
                    string mensaje2;
                    

                    if(estado == string.Empty)
                    {
                        mensaje2 = medidor + "|" + fecha2String + "|" + tipo2 + "|" + valorstring + "|UPDATE";
                        //Console.WriteLine(mensaje2);
                        

                    }
                    else
                    {
                        mensaje2 = medidor + "|" + fecha2String + "|" + tipo2 + "|"  + valorstring + "|" + estado + "|UPDATE";
                        //Console.WriteLine(mensaje2);


                    }
                    clienteSocket.Escribir(mensaje2);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(clienteSocket.Leer());
                    



                }
                else
                {
                    try
                    {
                        Console.WriteLine(clienteSocket.Leer());
                    }
                    catch (NullReferenceException)
                    {

                        Console.WriteLine("Conexion rechazada");
                    }
                    
                    
                }
                
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Error de conexión");
                
            }
        }
    }
}