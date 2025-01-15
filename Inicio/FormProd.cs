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
{
    //Panttalla de Productos
    public partial class FormProd : Form
    {
        public FormProd()
        {
            InitializeComponent();
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

        }

        private void lkCat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Acceder a categoría
            FormCategoria formCategoria = new FormCategoria();
            this.Hide();
            formCategoria.Show();
        }

        private void lkProv_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { //Acceder a proveedores
            FormProveedor formProveedor = new FormProveedor();
            this.Hide();
            formProveedor.Show();
        }

        private void lkProd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//Acceder a productos
            FormProd prod = new FormProd();
            this.Hide();
            prod.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
