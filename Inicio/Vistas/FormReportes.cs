using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Controladores;
using Inicio.Modelos;
using OfficeOpenXml;

namespace Inicio.Vistas
{
    public partial class FormReportes : Form
    {
        ProductosControlador productosControlador = new ProductosControlador();
        CategoriasControlador categoriasControlador = new CategoriasControlador();
        ProveedoresControlador proveedorControlador = new ProveedoresControlador();
        public FormReportes()
        {
            InitializeComponent();
            CargarProductos();
        }
        private void CargarProductos()
        {
            dataGridView1.DataSource = productosControlador.ObtenerProductos();
        }

        private void btnBajoInventario_Click(object sender, EventArgs e)
        {
            List<Producto> productos = new List<Producto>();
            List<Producto> productosFiltrados = new List<Producto>();
            productos = productosControlador.ObtenerProductos();
            foreach (Producto prod in productos)
            {
                if (prod.cantidadProducto < 10) productosFiltrados.Add(prod);
            }
            dataGridView1.DataSource = productosFiltrados;
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void btnCategorias_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = categoriasControlador.ObtenerCategorias();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = proveedorControlador.ObtenerProveedores();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0) // Verificar si hay datos en el DataGridView
            {
                try
                {
                    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // ¡Esta es la línea clave!
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Hoja1"); // Crear una nueva hoja de cálculo

                        // Agregar encabezados
                        for (int i = 1; i <= dataGridView1.ColumnCount; i++)
                        {
                            worksheet.Cells[1, i].Value = dataGridView1.Columns[i - 1].HeaderText;
                        }

                        // Agregar datos
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                        }

                        // Mostrar el cuadro de diálogo para guardar el archivo
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.FileName = "datos_exportados.xlsx"; // Nombre predeterminado del archivo
                        saveFileDialog.Filter = "Archivos de Excel (*.xlsx)|*.xlsx|Todos los archivos (*.*)|*.*"; // Filtros de archivo

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                            excel.SaveAs(excelFile);
                            MessageBox.Show("Datos exportados a Excel correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al exportar a Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay datos para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
