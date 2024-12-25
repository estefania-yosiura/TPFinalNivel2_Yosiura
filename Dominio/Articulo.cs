using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImgUrl { get; set; }
        public Marca marca { get; set; }
        public Categoria categoria { get; set; }
        public decimal precio { get; set; }
        [DisplayName("Precio")]
        public string PrecioFormateado
        {
            get { return precio.ToString("C", CultureInfo.CreateSpecificCulture("es-AR")); }
        }

    }
}