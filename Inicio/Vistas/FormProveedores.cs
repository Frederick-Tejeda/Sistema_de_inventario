using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inicio
{//Pantalla de Proveedores
    public partial class FormProveedores : Form
    {
        public FormProveedores()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

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
    }
}
