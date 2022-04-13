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

    public partial class Form3 : Form
    {

        //string myConnectionString = "DataSource=localhost ;username=root;password =;database=bd;";
        



        public Form3()
        {
            InitializeComponent();
           
            

        }

        private void label1_Click(object sender, EventArgs e)
            {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
             string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = copydb;";
             
             
            try
            {
                //string connectionString = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";


               

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //string sqlQuery = "select t1.[DocEntry] , t1.[BaseRef] , t1.ItemCode , t1.Dscription , t1.Quantity , t1.ShipDate , t1.PriceBefDi,t1.DocDate , t2.U_CodeMarque ,t2.U_F_Art ,t2.BWeight1 , t2.U_Volume , t2.OnHand  from INV1 t1 join OITM t2 on t1.[ItemCode] = t2.[ItemCode] where t1.WhsCode = '01' and t1.DocDate between '2018-01-01' and '2019-01-02' order by t1.ItemCode";

                    string sqlQuery = "select TOP(5) ItemCode from INV1";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);


                    DataTable dtbl = new DataTable();


                    dataAdapter.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;

                }
            }
            catch
            {

                MessageBox.Show("Error ");
            }



           

        }

        private void Return_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form8().Show();
            this.Hide();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                using (SqlConnection connection3 = new SqlConnection(connectionString1))
                {
                    string reference = "tedt";
                    string emplacement = "fzezeez";
                    string sql = "insert into testEmp values (@name,@emp)";
                    connection3.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection3))
                    {
                        cmd.Parameters.AddWithValue("@name", reference);
                        cmd.Parameters.AddWithValue("@emp", emplacement);

                        // assign value to parameter 
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                MessageBox.Show("notin");
            }
        }
    }
}
