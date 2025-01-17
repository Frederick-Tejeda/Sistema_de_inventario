using Inicio.Controladores;
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

namespace Inicio.Vistas
{
    public partial class FormConsultas : Form
    {
        ProductosControlador productosControlador = new ProductosControlador();
        CategoriasControlador categoriasControlador = new CategoriasControlador();
        ProveedoresControlador proveedorControlador = new ProveedoresControlador();
        public FormConsultas()
        {
            InitializeComponent();
        }
        private void LimpiarCampos() {
            proveedorTxt.Text = "";
            categoriaTxt.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string proveedor = proveedorTxt.Text;
            string categoria = categoriaTxt.Text;

            if(string.IsNullOrWhiteSpace(proveedor) && string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show($"Al menos uno de los campos debe estar lleno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!string.IsNullOrWhiteSpace(proveedor) && !string.IsNullOrWhiteSpace(categoria))
            {
                MessageBox.Show($"Solo uno de los campos puede estar lleno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                List<Producto> productos, productosBuscados = new List<Producto>();
                productos = productosControlador.ObtenerProductos();
                List<Proveedor> proveedores = new List<Proveedor>();
                proveedores = proveedorControlador.ObtenerProveedores();
                List<Categoria> categorias = new List<Categoria>();
                categorias = categoriasControlador.ObtenerCategorias();

                if (!string.IsNullOrWhiteSpace(proveedor))
                {
                    Proveedor proveedorBuscado = proveedores.FirstOrDefault(prov => prov.nombreEmpresa == proveedor); // Usar LINQ para buscar el proveedor

                    if (proveedorBuscado != null) // Verificar si se encontró el proveedor
                    {
                        productosBuscados = productos.Where(prod => prod.idProveedor == proveedorBuscado.idProveedor).ToList(); //Usar Linq para filtrar los productos
                    }
                }
                else
                {
                    Categoria categoriaBuscada = categorias.FirstOrDefault(ctg => ctg.nombreCategoria == categoria); // Usar LINQ para buscar la categoría

                    if (categoriaBuscada != null) // Verificar si se encontró la categoría
                    {
                        productosBuscados = productos.Where(prod => prod.idCategoria == categoriaBuscada.idCategoria).ToList();//Usar Linq para filtrar los productos
                    }
                }

                dataGridView1.DataSource = productosBuscados; // Asignar la lista (vacía o con resultados) al DataGridView
                LimpiarCampos();
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

        private void linkReportes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormReportes formReportes = new FormReportes();
            this.Hide();
            formReportes.Show();
        }
    }
}
