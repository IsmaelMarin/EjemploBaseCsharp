using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\BaseAntonio\tutorials.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd;
        SqlDataAdapter adpt;
        DataTable dt;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnDP_Click(object sender, EventArgs e)
        {
            con.Open();
            adpt = new SqlDataAdapter("select * from Employee where Employee_Id='"+txtDP.Text+"'", con);
            dt = new DataTable();
            adpt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void button1_Click(object sender,EventArgs e)
        {
           
            

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf";
            //guardar.ShowDialog();

            string paginahtml_texto = Properties.Resources.plantilla2.ToString();
            paginahtml_texto = paginahtml_texto.Replace("@USER", dataGridView1.CurrentRow.Cells["Name"].Value.ToString());
            //paginahtml_texto = paginahtml_texto.Replace("@DOCUMENTO", txtEmail.Text);
            paginahtml_texto = paginahtml_texto.Replace("@FECHA", DateTime.Now.ToString("dd/MM/yyyy"));
            paginahtml_texto = paginahtml_texto.Replace("@ID", dataGridView1.CurrentRow.Cells["Employee_Id"].Value.ToString());
            paginahtml_texto = paginahtml_texto.Replace("@EMAIL", dataGridView1.CurrentRow.Cells["Email"].Value.ToString());
            paginahtml_texto = paginahtml_texto.Replace("@DESIGNATION", dataGridView1.CurrentRow.Cells["Designation"].Value.ToString());
            paginahtml_texto = paginahtml_texto.Replace("@SALARY", dataGridView1.CurrentRow.Cells["Salary"].Value.ToString());






            if (guardar.ShowDialog() == DialogResult.OK)
            {

                using (FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Phrase(""));

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }


                    pdfDoc.Close();
                    stream.Close();
                }

            }
        }
    }
}
