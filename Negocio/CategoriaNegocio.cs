using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        AccesoDatoss datos = new AccesoDatoss();
        public List<Categoria> Lista()
        {
            List<Categoria> lista = new List<Categoria>();
            try
            {
                datos.SetConsulta("Select Id, Descripcion From CATEGORIAS");
                datos.EjecutarLectura();
                while (datos.lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.Id = datos.lector.GetInt32(0);
                    aux.Descripcion = datos.lector.GetString(1);

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