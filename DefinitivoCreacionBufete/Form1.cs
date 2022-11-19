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



namespace DefinitivoCreacionBufete
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ismae\Documents\tutorials.mdf;Integrated Security=True;Connect Timeout=30");
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
            //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ismae\Documents\tutorials.mdf;Integrated Security=True;Connect Timeout=30");
            //DataTable dt = new DataTable();
            //con.Open();

          
                
            
            
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
    }
}
