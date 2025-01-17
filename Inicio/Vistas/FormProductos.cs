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
    //Panttalla de Productos
    public partial class FormProductos : Form
    {
        ProductosControlador productosControlador = new ProductosControlador();
        ValidacionDeCampos validacionDeCampos = new ValidacionDeCampos();
        public FormProductos()
        {
            InitializeComponent();
            CargarProductos();
        }
        private void CargarProductos()
        { 
             dataGridView1.DataSource = productosControlador.ObtenerProductos(); 
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = nombreProductoTxt.Text;
            string proveedor = proveedorProductoTxt.Text;
            string cantidad = existenciaProductoTxt.Text;
            string categoria = categoriaProductoTxt.Text;
            string precio = precioProductoTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProductos(nombre, proveedor, categoria, precio, cantidad, "insert", "");

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                Producto nuevoProducto = new Producto();
                nuevoProducto.nombreProducto = nombreProductoTxt.Text;
                nuevoProducto.idCategoria = int.Parse(categoriaProductoTxt.Text);
                nuevoProducto.precioProducto = double.Parse(precioProductoTxt.Text);
                nuevoProducto.cantidadProducto = int.Parse(existenciaProductoTxt.Text);
                nuevoProducto.idProveedor = int.Parse(proveedorProductoTxt.Text);
                productosControlador.AgregarProducto(nuevoProducto);
                MessageBox.Show("Producto agregado satisfactoriamente");
                CargarProductos();
            }
            else
            {
                MessageBox.Show($"Algo sucedio mal al actualizar producto...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void lkCat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Acceder a categoría
            FormCategorias formCategoria = new FormCategorias();
            this.Hide();
            formCategoria.Show();
        }

        private void lkProv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { //Acceder a proveedores
            FormProveedores formProveedor = new FormProveedores();
            this.Hide();
            formProveedor.Show();
        }

        private void lkProd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a productos
            FormProductos prod = new FormProductos();
            this.Hide();
            prod.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LimpiarCampos()
        {
            nombreProductoTxt.Text = "";
            existenciaProductoTxt.Text = "";
            precioProductoTxt.Text = "";
            proveedorProductoTxt.Text = "";
            categoriaProductoTxt.Text = "";
            idProductoTxt.Text = "";
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Verifica que haya una sola fila seleccionada
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;

                    // Verifica que el índice sea válido (esto es una precaución extra)
                    if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count) {
                        // Obtiene la fila seleccionada
                        DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];

                    // Accede a las celdas de la fila y asigna los valores a los TextBox
                    nombreProductoTxt.Text = filaSeleccionada.Cells["nombreProducto"].Value?.ToString(); // Usamos ?. para evitar excepciones si la celda es null
                    existenciaProductoTxt.Text = filaSeleccionada.Cells["cantidadProducto"].Value?.ToString();
                    precioProductoTxt.Text = filaSeleccionada.Cells["precioProducto"].Value?.ToString();
                    proveedorProductoTxt.Text = filaSeleccionada.Cells["idProveedor"].Value?.ToString();
                    categoriaProductoTxt.Text = filaSeleccionada.Cells["idCategoria"].Value?.ToString();
                    idProductoTxt.Text = filaSeleccionada.Cells["idProducto"].Value?.ToString();
                    }
                    else{
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nombre = nombreProductoTxt.Text;
            string proveedor = proveedorProductoTxt.Text;
            string cantidad = existenciaProductoTxt.Text;
            string categoria = categoriaProductoTxt.Text;
            string precio = precioProductoTxt.Text;
            string id = idProductoTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProductos(nombre, proveedor, categoria, precio, cantidad, "update", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                productosControlador.ActualizarProducto(nombre, int.Parse(proveedor), int.Parse(cantidad), int.Parse(categoria), double.Parse(precio), int.Parse(id));
                MessageBox.Show("Producto actualizado correctamente.");
                CargarProductos();
            }
            else
            {   
                MessageBox.Show($"Algo no esta bien, intente mas tardeo...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string id = idProductoTxt.Text;
            string nombre = nombreProductoTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProductos(nombre, "", "", "", "", "select", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Producto> productos;
                if (string.IsNullOrWhiteSpace(id))
                {
                    string tipoDato = "nombre";
                    productos = productosControlador.BuscarProducto(nombre, tipoDato);
                    dataGridView1.DataSource = productos;
                }
                else if (string.IsNullOrWhiteSpace(nombre))
                {
                    string tipoDato = "id";
                    productos = productosControlador.BuscarProducto(id, tipoDato);
                    dataGridView1.DataSource = productos;
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string id = idProductoTxt.Text;

            string[] resultado = validacionDeCampos.ValidarProductos("", "", "", "", "", "delete", id);

            if (resultado[0] == "error")
            {
                MessageBox.Show(resultado[1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (resultado[0] == "success")
            {
                List<Producto> productosAntes, productosAhora;
                productosAntes = productosControlador.ObtenerProductos();
                productosControlador.EliminarProducto(id);
                productosAhora = productosControlador.ObtenerProductos();
                if ((productosAntes.Count - productosAhora.Count) == 1)
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                    CargarProductos();
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show($"Algo sucedio mal al eliminar producto...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"Algo no esta bien, intente mas tarde...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
