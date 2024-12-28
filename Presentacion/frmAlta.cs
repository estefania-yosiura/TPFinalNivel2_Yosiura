using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class frmAlta : Form
    {
        private Articulo arti = null;
        public frmAlta()
        {
            InitializeComponent();
        }
        public frmAlta(Articulo arti)
        {
            InitializeComponent();
            this.arti = arti;
            Text = "Mdificar articulo :P";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                if (arti == null)
                    arti = new Articulo();

                arti.Codigo = txtCodigo.Text;
                arti.Nombre = txtNombre.Text;
                arti.Descripcion = txtDescripcion.Text;
                arti.ImgUrl = txtUrlImagen.Text;
                arti.precio = int.Parse(txtPrecio.Text);
                arti.marca = (Marca)cbxMarca.SelectedItem;
                arti.categoria = (Categoria)cbxCategoria.SelectedItem;

                if (arti.Id != 0)
                {
                    negocio.Modificar(arti);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(arti);
                    MessageBox.Show("Agregado exitosamente");
                }
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAlta_Load(object sender, EventArgs e)
        {
            MarcaNegocio MN = new MarcaNegocio();
            CategoriaNegocio CN = new CategoriaNegocio();
            cbxMarca.DataSource = MN.Listarr();
            cbxMarca.ValueMember = "Id";
            cbxMarca.DisplayMember = "Descripcion";
            cbxCategoria.DataSource = CN.Lista();
            cbxCategoria.ValueMember = "Id";
            cbxCategoria.DisplayMember = "Descripcion";
            if (arti != null)
            {
                txtCodigo.Text = arti.Codigo;
                txtNombre.Text = arti.Nombre;
                txtDescripcion.Text = arti.Descripcion;
                txtUrlImagen.Text = arti.ImgUrl;
                CargarImagen(arti.ImgUrl);
                txtPrecio.Text = arti.PrecioFormateado.ToString();
                cbxMarca.SelectedValue = arti.marca.Id;
                cbxCategoria.SelectedValue = arti.categoria.Id;
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            CargarImagen(txtUrlImagen.Text);
        }

        public void CargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception)
            {

                pbxArticulos.Load("https://conference.nbasbl.org/wp-content/uploads/2022/05/placeholder-image-1.png");
            }
        }
    }
}
