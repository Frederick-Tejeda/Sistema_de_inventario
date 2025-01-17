using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Controladores;
using Inicio.Modelos;

namespace Inicio
{
    //Pantalla de Categoría
    public partial class FormCategorias : Form
    {
        CategoriasControlador categoriasControlador = new CategoriasControlador();
        ValidacionDeCampos validacionDeCampos = new ValidacionDeCampos();
        public FormCategorias()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            dataGridView1.DataSource = categoriasControlador.ObtenerCategorias();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lkProv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a proveedores
            FormProveedores formProveedor = new FormProveedores();
            this.Hide();
            formProveedor.Show();
        }

        private void lkProd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acedder a los productos
            FormProductos prod = new FormProductos();
            this.Hide();
            prod.Show();
        }

        private void lkCat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a categoría
            FormCategorias formCategoria = new FormCategorias();
            this.Hide();   
            formCategoria.Show();   
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void LimpiarCampos()
        {
            nombreTxt.Text = "";
            descripcionTxt.Text = "";
            idTxt.Text = "";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = nombreTxt.Text;
            string descripcion = descripcionTxt.Text;

            string[] resultado = validacionDeCampos.ValidarCategorias(nombre, descripcion, "insert", "");

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                Categoria nuevaCategoria = new Categoria();
                nuevaCategoria.nombreCategoria = nombreTxt.Text;
                //nuevaCategoria.idCategoria = int.Parse(categoriaProductoTxt.Text);
                nuevaCategoria.descripcionCategoria = descripcionTxt.Text;
                categoriasControlador.AgregarCategoria(nuevaCategoria);
                MessageBox.Show("Categoria agregada satisfactoriamente");
                CargarCategorias();
            }
            else
            {
                MessageBox.Show($"Algo sucedio mal al actualizar producto. {resultado[0]}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string id = idTxt.Text;
            string nombre = nombreTxt.Text;

            string[] resultado = validacionDeCampos.ValidarCategorias(nombre, "", "select", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Categoria> categorias;
                if (string.IsNullOrWhiteSpace(id))
                {
                    string tipoDato = "nombre";
                    categorias = categoriasControlador.BuscarCategoria(nombre, tipoDato);
                    dataGridView1.DataSource = categorias;

                }
                else if (string.IsNullOrWhiteSpace(nombre))
                {
                    string tipoDato = "id";
                    categorias = categoriasControlador.BuscarCategoria(id, tipoDato);
                    dataGridView1.DataSource = categorias;
                }
                else
                {
                    MessageBox.Show($"Algo sucedio mal al buscar producto...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nombre = nombreTxt.Text;
            string descripcion = descripcionTxt.Text;
            string id = idTxt.Text;

            string[] resultado = validacionDeCampos.ValidarCategorias(nombre, descripcion, "update", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                categoriasControlador.ActualizarCategoria(nombre, int.Parse(id), descripcion);
                MessageBox.Show("Categoria actualizada correctamente.");
                CargarCategorias();
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = idTxt.Text;

            string[] resultado = validacionDeCampos.ValidarCategorias("", "", "delete", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Categoria> categoriasAntes, categoriasAhora;
                categoriasAntes = categoriasControlador.ObtenerCategorias();
                categoriasControlador.EliminarCategoria(id);
                categoriasAhora = categoriasControlador.ObtenerCategorias();
                if ((categoriasAntes.Count - categoriasAhora.Count) == 1)
                {
                    MessageBox.Show("Categoria eliminada correctamente.");
                    CargarCategorias();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show($"Algo sucedio mal al eliminar categoria...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Verifica que haya una sola fila seleccionada
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;

                    // Verifica que el índice sea válido (esto es una precaución extra)
                    if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
                    {
                        // Obtiene la fila seleccionada
                        DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];

                        // Accede a las celdas de la fila y asigna los valores a los TextBox
                        nombreTxt.Text = filaSeleccionada.Cells["nombreCategoria"].Value?.ToString(); // Usamos ?. para evitar excepciones si la celda es null
                        descripcionTxt.Text = filaSeleccionada.Cells["descripcionCategoria"].Value?.ToString();
                        idTxt.Text = filaSeleccionada.Cells["idCategoria"].Value?.ToString();

                        // Si tienes más campos (ej. Proveedor), agrégalos aquí:
                        // txtProveedor.Text = filaSeleccionada.Cells["Proveedor"].Value?.ToString();
                    }
                    else
                    {
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos en los campos: " + ex.Message);
                }
            }
            else
            {
                LimpiarCampos();
            }
        }
    }
}
