using System;
using System.Collections.Generic;
using System.IO;
using MedidoresModel.DTO;
using Newtonsoft.Json;


namespace MedidoresModel.DAL
{
    public class LecturaDALArchivos : ILecturaDAL
    {
        private LecturaDALArchivos()
        {

        }

        private static ILecturaDAL instancia;

        public static ILecturaDAL GetInstancia()
        {
            if (instancia == null)
                instancia = new LecturaDALArchivos();
            return instancia;
        }


        private string archivoTrafico = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "traficos.txt";

        private string archivoConsumo = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "consumos.txt";






        public void RegistrarLecturaConsumo(Lectura l)
        {
            
            try {
                List<Lectura> lecturas2 = JsonConvert.DeserializeObject<List<Lectura>>(File.ReadAllText(archivoConsumo));
                lecturas2.Add(l);

                string json = JsonConvert.SerializeObject(lecturas2, Formatting.Indented);
                File.WriteAllText(archivoConsumo, json);

            }
            catch(NullReferenceException ex)
            {
                string json = JsonConvert.SerializeObject(l, Formatting.Indented);
                string json2 = "[" + json + "]";
                File.WriteAllText(archivoConsumo, json2);
            }
            catch(FileNotFoundException ex)
            {
                string json = JsonConvert.SerializeObject(l, Formatting.Indented);
                string json2 = "[" + json + "]";
                File.WriteAllText(archivoConsumo, json2);
            }
             
                                    
        }


        public void RegistrarLecturaTrafico(Lectura l)
        {
            try
            {
                List<Lectura> lecturas2 = JsonConvert.DeserializeObject<List<Lectura>>(File.ReadAllText(archivoTrafico));
                lecturas2.Add(l);

                string json = JsonConvert.SerializeObject(lecturas2, Formatting.Indented);
                File.WriteAllText(archivoTrafico, json);

            }
            catch (NullReferenceException ex)
            {
                string json = JsonConvert.SerializeObject(l, Formatting.Indented);
                string json2 = "[" + json + "]";
                File.WriteAllText(archivoTrafico, json2);
            }
            catch (FileNotFoundException ex)
            {
                string json = JsonConvert.SerializeObject(l, Formatting.Indented);
                string json2 = "[" + json + "]";
                File.WriteAllText(archivoTrafico, json2);
            }
        }

        
        public List<Lectura> ObtenerLecturasConsumo()
        {
            List<Lectura> consumos;
            try
            {
                consumos = JsonConvert.DeserializeObject<List<Lectura>>(File.ReadAllText(archivoConsumo));
            }
            catch (NullReferenceException)
            {

                consumos = null;
            }
            

            return consumos;
        }

        public List<Lectura> ObtenerLecturasTrafico()
        {
            List<Lectura> traficos;
            try
            {
                traficos = JsonConvert.DeserializeObject<List<Lectura>>(File.ReadAllText(archivoTrafico));
            }
            catch (NullReferenceException)
            {

                traficos = null;
            }
            return traficos;
        }

        
    }

}