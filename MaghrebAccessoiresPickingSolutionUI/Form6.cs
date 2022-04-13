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
    public partial class Form6 : Form
    {
        public Form6()
        { 
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            //string emplacement = textBox1.Text;
            string connectionString = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT DISTINCT U_CodeMarque  from oitm";
                    string sqlQuery1 = "SELECT DISTINCT U_F_Art  from oitm"; 

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlCommand command1 = new SqlCommand(sqlQuery1, connection);



                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);


                    DataTable dtbl = new DataTable();
                    DataTable dtbl1 = new DataTable();


                    dataAdapter.Fill(dtbl);
                    dataAdapter1.Fill(dtbl1);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        comboBox1.Items.Add(dtbl.Rows[i]["U_CodeMarque"].ToString());
                    }
                    for (int j = 0; j < dtbl1.Rows.Count; j++)
                    {
                        comboBox2.Items.Add(dtbl1.Rows[j]["U_F_Art"].ToString());
                    }

                    //comboBox1.DataSource = dtbl;


                }
            }
            catch
            {
                MessageBox.Show("Vérfier la cnx à la bd");
            }
        }

        private void dataGridViewSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


            string refe = textBox1.Text;
            string cm = comboBox1.Text;
            string fam = comboBox2.Text;

            string connectionString1 = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT t1.ItemCode as Ref, t2.ItemName as Description , t1.U_emp as Emplacement , t1.OnHand as QtStock ,t2.BWeight1 as poids , t2.U_Volume as volume from oitw t1 join oitm t2 on t1.ItemCode = t2.ItemCode where t1.WhsCode = '01' and (t1.ItemCode like '" + refe+"' or t2.U_F_Art like '"+fam+"' or t2.U_CodeMarque like'%"+cm+"%')";


                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                    DataTable dtbl2 = new DataTable();


                    dataAdapter2.Fill(dtbl2);

                    dataGridViewRes.DataSource = dtbl2;


                }
            }
            catch
            {
                MessageBox.Show("Vérfier la cnx à la bd");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewRes.DataSource = new DataTable();
            dataGridViewRes.Columns.Clear();
            this.textBox1.Clear();
            this.comboBox1.DataSource = new DataTable();
            this.comboBox2.DataSource = new DataTable();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
