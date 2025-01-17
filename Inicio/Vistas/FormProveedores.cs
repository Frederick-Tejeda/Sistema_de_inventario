using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Modelos;
using Inicio.Controladores;
using Inicio.Vistas;

namespace Inicio
{//Pantalla de Proveedores
    public partial class FormProveedores : Form
    {
        ProveedoresControlador proveedorControlador = new ProveedoresControlador();
        ValidacionDeCampos validacionDeCampos = new ValidacionDeCampos();
        public FormProveedores()
        {
            InitializeComponent();
            CargarProveedores();
        }
        private void CargarProveedores()
        {
            dataGridView1.DataSource = proveedorControlador.ObtenerProveedores();
        }
        private void LimpiarCampos()
        {
            empresaTxt.Text = "";
            direccionTxt.Text = "";
            idTxt.Text = "";
            telefonoTxt.Text = "";
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombreEmpresa = empresaTxt.Text;
            string direccion = direccionTxt.Text;
            string telefono = telefonoTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProveedores(nombreEmpresa, direccion, telefono, "insert", "");

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                Proveedor proveedor = new Proveedor();
                proveedor.nombreEmpresa = empresaTxt.Text;
                proveedor.direccion = direccionTxt.Text;
                proveedor.telefono = long.Parse(telefonoTxt.Text);
                proveedorControlador.AgregarProveedor(proveedor);
                MessageBox.Show("Proveedor agregado satisfactoriamente");
                CargarProveedores();
            }
            else
            {
                MessageBox.Show($"Algo sucedio mal al agregar producto...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string empresa = empresaTxt.Text;
            string direccion = direccionTxt.Text;
            string telefono = telefonoTxt.Text;
            string id = idTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProveedores(empresa, direccion, telefono, "update", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                proveedorControlador.ActualizarProveedor(empresa, int.Parse(id), direccion, long.Parse(telefono));
                MessageBox.Show("Categoria actualizada correctamente.");
                CargarProveedores();
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void lkCat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a categoría
            FormCategorias formCategoria = new FormCategorias();
            this.Hide();
            formCategoria.Show();
        }

        private void lkProd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acedder a los productos
            FormProductos prod = new FormProductos();
            this.Hide(); 
            prod.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lkProv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a proveedores
            FormProveedores formProveedor = new FormProveedores();
            this.Hide();
            formProveedor.Show();
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
                        empresaTxt.Text = filaSeleccionada.Cells["nombreEmpresa"].Value?.ToString(); // Usamos ?. para evitar excepciones si la celda es null
                        direccionTxt.Text = filaSeleccionada.Cells["direccion"].Value?.ToString();
                        idTxt.Text = filaSeleccionada.Cells["idProveedor"].Value?.ToString();
                        telefonoTxt.Text = filaSeleccionada.Cells["telefono"].Value?.ToString();

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

        private void FormProveedores_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string empresa = empresaTxt.Text;
            string id = idTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProveedores(empresa, "", "", "select", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Proveedor> proveedores;
                if (string.IsNullOrWhiteSpace(id))
                {
                    string tipoDato = "nombre";
                    proveedores = proveedorControlador.BuscarProveedor(empresa, tipoDato);
                    dataGridView1.DataSource = proveedores;

                }
                else if (string.IsNullOrWhiteSpace(empresa))
                {
                    string tipoDato = "id";
                    proveedores = proveedorControlador.BuscarProveedor(id, tipoDato);
                    dataGridView1.DataSource = proveedores;
                }
                else
                {
                    MessageBox.Show($"Algo sucedio mal al buscar Proveedor...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = idTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProveedores("", "", "", "delete", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Proveedor> proveedoresAntes, proveedoresAhora;
                proveedoresAntes = proveedorControlador.ObtenerProveedores();
                proveedorControlador.EliminarProveedor(id);
                proveedoresAhora = proveedorControlador.ObtenerProveedores();
                if ((proveedoresAntes.Count - proveedoresAhora.Count) == 1)
                {
                    MessageBox.Show("Categoria eliminada correctamente.");
                    CargarProveedores();
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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCategorias formCategorias = new FormCategorias();
            this.Hide();
            formCategorias.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormProveedores formProveedores = new FormProveedores();
            this.Hide();
            formProveedores.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormProductos formProductos = new FormProductos();
            this.Hide();
            formProductos.Show();
        }

        private void linkConsultas_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormConsultas formConsultas = new FormConsultas();
            this.Hide();
            formConsultas.Show();
        }

        private void linkReportes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormReportes formReportes = new FormReportes();
            this.Hide();
            formReportes.Show();
        }
    }
}
