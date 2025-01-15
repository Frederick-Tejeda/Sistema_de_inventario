using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
        public FormInicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //Condición de entrada al sistema

            if (tbpassword.Text == "1234" && tbUsuario.Text == "admin"){

                FormProd formProd = new FormProd();
                MessageBox.Show("Bienvenido");
                this.Hide();
                formProd.ShowDialog();
            }
            
            else {
                MessageBox.Show("Hubo algún error ¡Intente nuevamente!");
                tbUsuario.Clear();
                tbpassword.Clear();
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
