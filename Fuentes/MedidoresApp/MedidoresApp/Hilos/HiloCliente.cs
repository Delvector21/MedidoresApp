using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedidoresModel.DAL;
using MedidoresModel.DTO;
using Newtonsoft.Json;
using SocketUtils;

namespace MedidoresApp.Hilos
{
    class HiloCliente
    {
        private ClienteSocket clienteSocket;
        private ILecturaDAL dal = LecturaDALFactory.CreateDal();
        static IMedidorConsumoDAL dalConsumo = MedidorConsumoDALFactory.CreateDal();
        static IMedidorTraficoDAL dalTrafico = MedidorTraficoDALFactory.CreateDal();

        public HiloCliente(ClienteSocket clienteSocket)
        {
            this.clienteSocket = clienteSocket;
        }

        public void Ejecutar()
        {
            bool verificado = false;
            string prueba;
            try
            {
                prueba = clienteSocket.Leer();
            }
            catch (NullReferenceException ex)
            {

                prueba = "n|n|n";
            }

            Console.WriteLine(prueba);
            string fechaString;
            int medidorV;
            try
            {
                fechaString = (prueba.Split('|'))[0];
            }
            catch (Exception)
            {

                fechaString = "fechanovalida";
            }
            
            try
            {
                medidorV = Int32.Parse((prueba.Split('|'))[1]);
            }
            catch (Exception ex)
            {
                medidorV = 0;
            }
            string tipoV;
            try
            {
                tipoV = (prueba.Split('|'))[2];
            }
            catch (Exception)
            {

                tipoV = "cadena";
            }


            DateTime fecha;
            try
            {
                fecha = DateTime.ParseExact(fechaString, "yyyy-MM-dd-HH-mm-ss",
                System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {

                fecha = new DateTime(2021, 01, 01, 00, 00, 00);
            }

            string fecha2string;
            TimeSpan intervalo = DateTime.Now - fecha;

            if (tipoV == "consumo")
            {
                foreach (var medidor in dalConsumo.ObtenerMedidores())
                {
                    if (medidorV == medidor)
                    {
                        verificado = true;
                    }
                }
            }
            else if (tipoV == "trafico")
            {
                foreach (var medidor in dalTrafico.ObtenerMedidores())
                {
                    if (medidorV == medidor)
                    {
                        verificado = true;
                    }
                }
            }


            if (verificado == true && ((intervalo.TotalMinutes < 30) && (tipoV == "trafico" || tipoV == "consumo")))
            {
                clienteSocket.Escribir(DateTime.Now + "| WAIT");
                
                string input;
                try
                {
                    input = clienteSocket.Leer();
                }
                catch (Exception)
                {

                    input = "n|n|n|n|n|n";
                }

                string[] mensaje2;
                try
                {
                    mensaje2 = input.Split('|');
                }
                catch (Exception)
                {

                    input = "n|n|n|n|n|n";
                    mensaje2 = input.Split('|');
                }
                 

                if (mensaje2.Length == 6)
                {
                    
                    int medidor;
                    DateTime fecha2;
                    int valor;
                    int estado;


                    try
                    {
                        medidor = Int32.Parse((input.Split('|'))[0]);
                    }
                    catch (Exception)
                    {

                        medidor = 0;
                    }

                    fecha2string = (input.Split('|'))[1];

                    try
                    {
                        fecha2 = DateTime.ParseExact(fecha2string, "yyyy-MM-dd-HH-mm-ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        fecha2 = new DateTime(2021, 01, 01, 00, 00, 00);
                    }

                    string tipo = (input.Split('|'))[2];

                    try
                    {
                        valor = Int32.Parse((input.Split('|'))[3]);
                    }
                    catch (Exception)
                    {

                        valor = -1;
                    }

                    try
                    {
                        estado = Int32.Parse((input.Split('|'))[4]);
                    }
                    catch (Exception)
                    {

                        estado = 7;
                    }

                    string confirmar = (input.Split('|'))[5];

                    if ((medidorV == medidor) && (fecha2 == fecha) && (tipo == "consumo" || tipo == "trafico") && (valor >= 0 && valor <= 1000) && (estado >= -1 && estado <= 2) && (confirmar == "UPDATE"))
                    {
                        if (tipo == "consumo")
                        {
                            Lectura l = new Lectura()
                            {
                                NroSerie = medidor,
                                Fecha = fecha2,
                                Tipo = tipo,
                                Valor = valor,
                                Estado = estado
                            };

                            lock (dal)
                            {
                                dal.RegistrarLecturaConsumo(l);
                                clienteSocket.Escribir(medidor + "|OK");
                                Console.WriteLine(medidor + "|OK");

                            }
                        }
                        else if (tipo == "trafico")
                        {
                            Lectura l = new Lectura()
                            {
                                NroSerie = medidor,
                                Fecha = fecha2,
                                Tipo = tipo,
                                Valor = valor,
                                Estado = estado
                            };

                            lock (dal)
                            {

                                clienteSocket.Escribir(medidor + "|OK");
                                dal.RegistrarLecturaTrafico(l);
                                Console.WriteLine(medidor + "|OK");

                            }
                        }

                    }
                    else
                    {
                        clienteSocket.Escribir(fecha2string + "|" + medidor + "| ERROR");
                        Console.WriteLine(fecha2string + "|" + medidor + "| ERROR");

                    }

                }
                else if(mensaje2.Length == 5)
                {
                    List<string> errores = new List<string>();
                    int medidor;
                    DateTime fecha2;
                    int valor;
                    
                    try
                    {
                        medidor = Int32.Parse((input.Split('|'))[0]);
                    }
                    catch (Exception)
                    {

                        medidor = 0;
                    }

                    fecha2string = (input.Split('|'))[1];

                    try
                    {
                        fecha2 = DateTime.ParseExact(fecha2string, "yyyy-MM-dd-HH-mm-ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        fecha2 = new DateTime(2021, 01, 01, 00, 00, 00);
                    }

                    string tipo = (input.Split('|'))[2];

                    try
                    {
                        valor = Int32.Parse((input.Split('|'))[3]);
                    }
                    catch (Exception ex)
                    {

                        valor = -1;
                    }



                    string confirmar = (input.Split('|'))[4];

                    if ((medidorV == medidor) && (fecha2 == fecha) && (tipo == "consumo" || tipo == "trafico") && (valor >= 0 && valor <= 1000) && (confirmar == "UPDATE"))
                    {
                        if (tipo == "consumo")
                        {
                            Lectura l = new Lectura()
                            {
                                NroSerie = medidor,
                                Fecha = fecha2,
                                Tipo = tipo,
                                Valor = valor,

                            };

                            lock (dal)
                            {
                                dal.RegistrarLecturaConsumo(l);
                                clienteSocket.Escribir(medidor + "|OK");
                                Console.WriteLine(medidor + "|OK");
                                //clienteSocket.CerrarConexion();
                            }
                        }
                        else
                        {
                            Lectura l = new Lectura()
                            {
                                NroSerie = medidor,
                                Fecha = fecha2,
                                Tipo = tipo,
                                Valor = valor,

                            };

                            lock (dal)
                            {
                                dal.RegistrarLecturaTrafico(l);
                                clienteSocket.Escribir(medidor + "|OK");
                                Console.WriteLine(medidor + "|OK");


                            }
                        }

                    }
                    else
                    {
                        clienteSocket.Escribir(fecha2string + "|" + medidor + "| ERROR");
                        Console.WriteLine(fecha2string + "|" + medidor + "| ERROR");

                    }
                }
                else
                {

                    clienteSocket.Escribir(fechaString + "|" + medidorV + "| ERROR");
                    Console.WriteLine(fechaString + "|" + medidorV + "| ERROR");
                    //Console.WriteLine("Conexion Rechazada");
                    clienteSocket.CerrarConexion();
                }
            }
            else
            {

                clienteSocket.Escribir("Error en solicitud");
                //Console.WriteLine("Conexion Rechazada");
                clienteSocket.CerrarConexion();
            }
        }
    }
}


