using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inicio.Vistas.Modelos;


/*Significado de siglas:
lb: label
lk: link label
btn: button
tb: text box
 */

namespace Inicio
{
    //Ventana de inicio de sesión
    public partial class FormInicio : Form
    {
        Database db = new Database();
        public FormInicio()
        {
            InitializeComponent();
            db.InitializeDatabase();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //Condición de entrada al sistema
            if (db.ValidateUser(tbUsuario.Text, tbpassword.Text))
            {
                MessageBox.Show("Inicio de sesión exitoso.");
                // Aquí podrías abrir otro formulario
                FormProductos formProductos = new FormProductos();
                this.Hide();
                formProductos.ShowDialog();
            }
            else
            {
                MessageBox.Show("Credenciales inválidas.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    
        private void tbpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbUsuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
