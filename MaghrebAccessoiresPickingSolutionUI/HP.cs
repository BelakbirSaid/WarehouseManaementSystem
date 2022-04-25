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
    public partial class HP : Form
    {
        public HP()
        {
            InitializeComponent();
        }

       // public string[] coul1 = { "05", "02", "01", "03", "06", "04", "07", "08", "09","10","11","12","13" };
        private void button3_Click(object sender, EventArgs e)
        {

            DataTable T0 = new DataTable();
            DataTable T1 = new DataTable();


            try
            {
                
                 string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
             
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string sqlQuery = "select * from Hmap";
                    

                            SqlCommand command = new SqlCommand(sqlQuery, connection);


                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);


                            DataTable dtbl = new DataTable();


                            dataAdapter.Fill(dtbl);
                            dataGridView1.DataSource = dtbl;
                    T0 = dtbl;



                }



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select * from Table_1";


                    SqlCommand command = new SqlCommand(sqlQuery, connection);


                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);


                    DataTable dtbl1= new DataTable();


                    dataAdapter.Fill(dtbl1);
                    dataGridView1.DataSource = dtbl1;
                    T1 = dtbl1;




                }

            }


            catch(Exception ed )
            {
                MessageBox.Show(ed.ToString());
            }


            try
            {

               for (int i = 0; i < T0.Columns.Count; i++)
                {
                    for (int j = 0; j < T0.Rows.Count; j++)
                    {
                        string key = T0.Rows[j][i].ToString(); 





                    }
                }



            }
            catch (Exception ad ) 
            {
                MessageBox.Show(ad.ToString());
            }

        }
    }
}
