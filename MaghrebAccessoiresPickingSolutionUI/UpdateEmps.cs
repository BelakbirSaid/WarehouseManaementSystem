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
    public partial class UpdateEmps : Form
    {
        public UpdateEmps()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {


            new InitForm().Show();

           
            






        }

        

        private void button3_Click(object sender, EventArgs e)
        {


            try { 

            string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // up   t1
                connection.Open();
                string sql = "INsert INTO opera Values('', '','','','')";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    //  cmd.Parameters.AddWithValue("@name", );
                    //  cmd.Parameters.AddWithValue("@emp", emplacementop);
                    //   cmd.Parameters.AddWithValue("@empac", EmplacementAct);


                    // assign value to parameter 
                    cmd.ExecuteNonQuery();
                }
            }


            new operations().Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ViewAllops().Show();






        }
    }
}
