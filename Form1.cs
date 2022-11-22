using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace DefinitivoCreacionBufete
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\BaseAntonio\tutorials.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        int Employeeid;
        public Form1()
        {
            InitializeComponent();
            displayData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("insert into Employee values('" + txtUserName.Text + " ',' " + txtEmail.Text + " ',' " + txtDesignation.Text + " ',' " + txtSalary.Text + "')",con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Your data has been saved in the database");
            con.Close();
            displayData();
            clear();

        }
        private void displayData()
        {
            con.Open();
            adpt = new SqlDataAdapter("select * from Employee", con);
            //adpt = new SqlDataAdapter("select Name,Email from [Employee]", con);
           
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }   
        private void clear()
        {
            txtUserName.Text = "";
            txtDesignation.Text = "";
            txtEmail.Text = "";
            txtSalary.Text = "";

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Employeeid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtUserName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDesignation.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSalary.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("update Employee set Name='" + txtUserName.Text + " ',Email=' " + txtEmail.Text + " ',Salary=' " + txtSalary.Text + "'where Employee_Id='" + Employeeid + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data has been Updated");
                con.Close();
                clear();
                displayData();
                
            }
            catch(Exception)
            {

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("delete from Employee where Employee_Id='" + Employeeid + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("The data has been delated");
            con.Close();
            clear();
            displayData();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 Ir = new Form2();
            Ir.Show();
            this.Hide();

          
                
            
            
        }

        private void txtBuscarname_TextChanged(object sender, EventArgs e)
        {
            txtBuscarEmail.Text = "";
            txtBuscarSalary.Text = "";
            BusquedaDinamica();

           
        }
        
        private void BusquedaDinamica()
        {
            if (txtBuscarname.Text.Length > 0 || txtBuscarEmail.Text.Length > 0 || txtBuscarSalary.Text.Length > 0)
            {
                if (txtBuscarname.Text.Length > 0)
                {
                    con.Open();
                    adpt = new SqlDataAdapter("select * from Employee where Name like '" + txtBuscarname.Text + "%'", con);
                    dt = new DataTable();
                    adpt.Fill(dt);
                    con.Close();
                    dataGridView1.DataSource = dt;

                }
                else if (txtBuscarEmail.Text.Length > 0)
                {
                    con.Open();
                    adpt = new SqlDataAdapter("select * from Employee where Email like '" + txtBuscarEmail.Text + "%'", con);
                    dt = new DataTable();
                    adpt.Fill(dt);
                    con.Close();
                    dataGridView1.DataSource = dt;

                }

                else if (txtBuscarSalary.Text.Length > 0)
                {
                    con.Open();
                    adpt = new SqlDataAdapter("select * from Employee where Salary like '" + txtBuscarSalary.Text + "%'", con);
                    dt = new DataTable();
                    adpt.Fill(dt);
                    con.Close();
                    dataGridView1.DataSource = dt;

                }
            }
            else if(txtBuscarname.Text.Length == 0 || txtBuscarEmail.Text.Length == 0 || txtBuscarSalary.Text.Length == 0)
            {
                displayData();
            }
          
        }

        private void txtBuscarEmail_TextChanged(object sender, EventArgs e)
        {
            txtBuscarname.Text = "";
            txtBuscarSalary.Text = "";
            BusquedaDinamica();

           
        }

        private void txtBuscarSalary_TextChanged(object sender, EventArgs e)
        {
            txtBuscarname.Text = "";
            txtBuscarEmail.Text = "";
            BusquedaDinamica();
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AgregarCarpeta Ir = new AgregarCarpeta();
            Ir.Show();
            this.Hide();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnVerificar_Click(object sender, EventArgs e)
        {

        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss")+".pdf";
            //guardar.ShowDialog();

            string paginahtml_texto = Properties.Resources.plantilla2.ToString();
            paginahtml_texto = paginahtml_texto.Replace("@CLIENTE",txtUserName.Text);
            paginahtml_texto = paginahtml_texto.Replace("@DOCUMENTO", txtEmail.Text);
            paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));




            if (guardar.ShowDialog() == DialogResult.OK)
            {
              
                using (FileStream stream  = new FileStream(guardar.FileName,FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer,pdfDoc,sr);
                    }


                        pdfDoc.Close();
                    stream.Close();
                }
                   
            }



        }
    }
}
