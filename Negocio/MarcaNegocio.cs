using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        AccesoDatoss datos = new AccesoDatoss();
        public List<Marca> Listarr()
        {
            List<Marca> lista = new List<Marca>();
            try
            {
                datos.SetConsulta("Select Id, Descripcion From MARCAS");
                datos.EjecutarLectura();
                while (datos.lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.lector["Id"];
                    aux.Descripcion = (string)datos.lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}
