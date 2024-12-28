using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDatos;
using System.Net;

namespace Negocio
{
    public class ArticuloNegocio
    {
        AccesoDatoss datos = new AccesoDatoss();
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                datos.SetConsulta("Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion, M.Id, C.Descripcion, C.Id, ImagenUrl, Precio From ARTICULOS A, MARCAS M, CATEGORIAS C Where IdMarca = M.Id AND IdCategoria = C.Id");
                datos.EjecutarLectura();
                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.lector["Id"];
                    aux.Nombre = (string)datos.lector["Nombre"];
                    aux.Codigo = (string)datos.lector["Codigo"];
                    aux.Descripcion = (string)datos.lector["Descripcion"];

                    if(! datos.lector.IsDBNull(datos.lector.GetOrdinal("ImagenUrl")))
                    aux.ImgUrl = (string)datos.lector["ImagenUrl"];

                    aux.precio = (decimal)datos.lector["Precio"];
                    aux.marca = new Marca();
                    aux.marca.Descripcion = datos.lector.GetString(4);
                    aux.marca.Id = datos.lector.GetInt32(5);
                    aux.categoria = new Categoria();
                    aux.categoria.Descripcion = datos.lector.GetString(6);
                    aux.categoria.Id = datos.lector.GetInt32(7);

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
        public void Agregar(Articulo nuevo)
        {
            AccesoDatoss datos = new AccesoDatoss();
            try
            {
                datos.SetConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, ImagenUrl, Precio, IdMarca, IdCategoria) values ('"+ nuevo.Codigo +"', '"+ nuevo.Nombre +"', '"+ nuevo.Descripcion +"', '"+ nuevo.ImgUrl +"', "+ nuevo.precio +", @IdMarca, @IdCategoria)");
                datos.SetParametros("@IdMarca", nuevo.marca.Id);
                datos.SetParametros("@IdCategoria", nuevo.categoria.Id);
                datos.EjecutarAccion();
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
        public void Modificar(Articulo modif)
        {
            try
            {
                datos.SetConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, IdMarca = @IdMarca, IdCategoria = @IdCat, ImagenUrl = @ImgUrl, Precio = @precio Where Id = @id");
                datos.SetParametros("@codigo", modif.Codigo);
                datos.SetParametros("@nombre", modif.Nombre);
                datos.SetParametros("@desc", modif.Descripcion);
                datos.SetParametros("@IdMarca", modif.marca.Id);
                datos.SetParametros("@IdCat", modif.categoria.Id);
                datos.SetParametros("@ImgUrl", modif.ImgUrl);
                datos.SetParametros("@precio", modif.precio);
                datos.SetParametros("@id", modif.Id);
                datos.EjecutarAccion();
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
        public void Eliminar(Articulo eliminado)
        {
            
        }
    }
}
