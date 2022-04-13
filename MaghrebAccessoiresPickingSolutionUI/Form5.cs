using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;


namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "01";
            comboBox1.Enabled = false;
            comboBox2.SelectedItem = "Autoplus";
            comboBox2.Enabled = false; 





        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            string emplacement = textBox1.Text;  
            string connectionString = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT   U_emp as Emplacement , ItemCode as Réf, OnHand as QtStock from oitw t1 where t1.WhsCode = '01' and t1.U_emp like '%" + emplacement+"%'";


                    SqlCommand command = new SqlCommand(sqlQuery, connection);


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);


                    DataTable dtbl = new DataTable();


                    dataAdapter.Fill(dtbl);

                    dataGridViewSearch.DataSource = dtbl;


                }
            }
            catch
            {
                MessageBox.Show("err");
            }

           
        }

        private void dataGridViewSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewSearch.DataSource = new DataTable();
            dataGridViewSearch.Columns.Clear();
            this.textBox1.Clear();

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT DISTINCT whscode  from oitw";
                    //string sqlQuery1 = "SELECT DISTINCT U_F_Art  from oitm";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                   // SqlCommand command1 = new SqlCommand(sqlQuery1, connection);



                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    //SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);


                    DataTable dtbl = new DataTable();
                    DataTable dtbl1 = new DataTable();


                    dataAdapter.Fill(dtbl);
                   // dataAdapter1.Fill(dtbl1);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        comboBox1.Items.Add(dtbl.Rows[i]["whscode"].ToString());
                    }
                   //

                    //comboBox1.DataSource = dtbl;


                }
            }
            catch
            {
                MessageBox.Show("Vérfier la cnx à la bd");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
