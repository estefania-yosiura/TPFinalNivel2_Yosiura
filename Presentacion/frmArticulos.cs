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
        List<Articulo> listaFiltrada;
        public Presentacion()
        {
            InitializeComponent();
        }
        private void Presentacion_Load(object sender, EventArgs e)
        {
            Cargar();
            CboCampo.Items.Add("Precio");
            CboCampo.Items.Add("Codigo");
            CboCampo.Items.Add("Nombre");
            CboCampo.Items.Add("Descripcion");
          
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
            if (dgvArticulos.CurrentRow != null)
            {
               Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                CargarImagen(seleccionado.ImgUrl);
            }
           
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
            try
            {
                if (dgvArticulos.Rows.Count > 0)
                {
                   seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    frmAlta Modificar = new frmAlta(seleccionado);
                    Modificar.ShowDialog();
                    Cargar();
                }
                else
                {
                    MessageBox.Show("No podes modificar si la lista esta vacia");
                }
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            ArticuloNegocio negocio = new ArticuloNegocio();

           if(dgvArticulos.Rows.Count > 0)
           {
            DialogResult resultado = MessageBox.Show("¿En serio quieres eliminarlo?", "Eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            try
            {
                  if (resultado == DialogResult.OK)
                  {
                    try
                    {
                        
                           seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                            negocio.Eliminar(seleccionado.Id);
                            Cargar();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                  }
            }
            catch (Exception ex)
            {
              MessageBox.Show(ex.ToString());
            }
           } else
           {
                MessageBox.Show("No podes eliminar si la lista esta vacia");
           }

               
        }

        private bool ValidarFiltro()
        {
            if (CboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona algun campo antes de buscar :3");
                return true;
            }

            if (cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona algun criterio antes de buscar :3");
                return true;
            }

            if (string.IsNullOrEmpty(txtbBuscar.Text))
            {
                MessageBox.Show("Tipee numeros para continuar");
                return true;
            }

            if (CboCampo.SelectedItem.ToString() == "Precio")
            {
                if (!(SoloNumeros(txtbBuscar.Text)))
                {
                  MessageBox.Show("Solo hay que poner numeros en este campo :O");
                    return true;
                }
            }
            
            return false;
        }

        private bool SoloNumeros(string cadena)
        {
            foreach(char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if (ValidarFiltro())
                return;
           
            try
            {
              string campo = CboCampo.SelectedItem.ToString();
              string criterio = cboCriterio.SelectedItem.ToString();
              string filtro = txtbBuscar.Text;
              dgvArticulos.DataSource = negocio.Filtrar(campo, criterio, filtro);
              listaFiltrada = negocio.Filtrar(campo, criterio, filtro);
                OcultarColumnas();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            
        }

        private void CboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = CboCampo.SelectedItem.ToString();
            cboCriterio.Items.Clear();
            if (opcion == "Codigo")
            {
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Termina en");
            }
            else if (opcion == "Nombre")
            {
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Termina en");
            }
            else if (opcion == "Descripcion")
            {
                cboCriterio.Items.Add("Empieza con");
                cboCriterio.Items.Add("Contiene");
                cboCriterio.Items.Add("Termina en");
            }
            else if (opcion == "Precio")
            {
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Igual a");
                cboCriterio.Items.Add("Menor a");
            }
        }
    }
}
