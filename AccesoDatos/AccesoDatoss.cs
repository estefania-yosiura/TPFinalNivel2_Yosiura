using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class AccesoDatoss
    {
        public SqlCommand comando;
        public SqlConnection conexion;
        public SqlDataReader lector;
        public AccesoDatoss()
        {
            conexion = new SqlConnection("Server=.\\SQLEXPRESS; DataBase=CATALOGO_DB; integrated security=true");
            comando = new SqlCommand();
        }
        public void SetConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void SetParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre,valor);
        }
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }
    }
}