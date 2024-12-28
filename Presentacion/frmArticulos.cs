using Negocio;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Presentacion : Form
    {
        List<Articulo> listaArts;
        public Presentacion()
        {
            InitializeComponent();
        }
        private void Presentacion_Load(object sender, EventArgs e)
        {
            Cargar();
        }
        private void Cargar()
        {
            ArticuloNegocio lista = new ArticuloNegocio();
            try
            {
                listaArts = lista.Listar();
                dgvArticulos.DataSource = listaArts;
                pbxArticulos.Load(listaArts[0].ImgUrl);
                OcultarColumnas();
            }
            catch (Exception E)
            {

                MessageBox.Show(E.ToString());
            }
        }
        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            CargarImagen(seleccionado.ImgUrl);
        }

        private void OcultarColumnas()
        {
            dgvArticulos.Columns["ImgUrl"].Visible = false;
            dgvArticulos.Columns["precio"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlta Alta = new frmAlta();
            Alta.ShowDialog();
            Cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAlta Modificar = new frmAlta(seleccionado);
            Modificar.ShowDialog();
            Cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
           // negocio.Eliminar();
        }

    }
}
