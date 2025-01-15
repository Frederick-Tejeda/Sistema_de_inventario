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
    public partial class FormProveedor : Form
    {
        public FormProveedor()
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
            FormCategoria formCategoria = new FormCategoria();
            this.Hide();
            formCategoria.Show();
        }

        private void lkProd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acedder a los productos
            FormProd prod = new FormProd();
            this.Hide(); 
            prod.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lkProv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a proveedores
            FormProveedor formProveedor = new FormProveedor();
            this.Hide();
            formProveedor.Show();
        }
    }
}
