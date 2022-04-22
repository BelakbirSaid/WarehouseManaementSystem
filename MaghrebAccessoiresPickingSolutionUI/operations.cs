using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class operations : Form
    {
        public operations()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string emplacement; 
            emplacement = "MAG" + comboBox1.Text + "-A" + comboBox2.Text;

            string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT [Reference] ,[Description],[EmplacementOpt] , [EmplacementAct] ,[qtystock] AS Stock,[CM] AS Code_Marque,[blocs] AS QT_MAX FROM [Table_1] Where [EmplacementAct] like '%" + emplacement + "%'";
                   // string sqlQuery1 = "SELECT   U_emp as Emplacement , ItemCode as Réf, OnHand as QtStock from oitw t1 where t1.WhsCode = '01' and t1.U_emp like '" + emplacement + "%'";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataTable dtbl = new DataTable();
                    dataAdapter.Fill(dtbl);
                   // dataGridView1.DataSource = dtbl;

                    //
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    

                    string sqlQueryI = "INsert INTO opera Values('', '','','','')";

                    // 
                   
                    // dataGridView1.DataSource = dtbl;

                    //
                }















            }
            catch ( Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }


        }

        private void operations_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
