using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; //Importamos esta libreria para que permite utilizar las funciones para crear carpetas

namespace DefinitivoCreacionBufete
{
    public partial class AgregarCarpeta : Form
    {
        public AgregarCarpeta()
        {
            InitializeComponent();
        }
        string NombreCarpeta;
        private void button1_Click(object sender, EventArgs e)
        {
           
           
            DirectoryInfo DIR = new DirectoryInfo(@"C:\ProcuAbogadosrr\"+NombreCarpeta);
            
            
            try
            {
                if(DIR.Exists)
                {
                    MessageBox.Show("This folder already exist......");
                   
                }
                else
                {
                    DIR.Create();
                    MessageBox.Show("Folder Created...");
                }

            }
            catch(Exception E)
            {
               
                MessageBox.Show("Folder could not be created due to "+E);
            }
        
        
        }
       
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            
            NombreCarpeta = txtNombre.Text;
            MessageBox.Show("Nombre guardado exitosamente");

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 ir = new Form1();
            ir.Show();
            this.Hide();
        }
    }
}
