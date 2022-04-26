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
            DataTable T3 = new DataTable();


            string mag = this.comboBox1.Text;
            string sqlQuery;

           

           

            try
            {
                
                 string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
             
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {

                    if (mag == "1")
                    {

                        sqlQuery = "select * from Table_4HP1";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);


                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);


                        DataTable dtbl = new DataTable();


                        dataAdapter.Fill(dtbl);
                        T0 = dtbl;
                        //dataGridView1.DataSource = dtbl;


                    }
                    if (mag == "2") { sqlQuery = "select * from Table_4HP2";

                        SqlCommand command1 = new SqlCommand(sqlQuery, connection);


                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);


                        DataTable dtbl1 = new DataTable();


                        dataAdapter1.Fill(dtbl1);
                        T0 = dtbl1;
                       // dataGridView1.DataSource = dtbl1;
                    }

                    if (mag == "3") { sqlQuery = "select * from Table_4HP3";

                        SqlCommand command2 = new SqlCommand(sqlQuery, connection);


                        SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                        DataTable dtbl2 = new DataTable();


                        dataAdapter2.Fill(dtbl2);
                        T0 = dtbl2; 
                        //dataGridView1.DataSource = dtbl2;


                    }
                    dataGridView1.DataSource = T0;
                    







                }



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQueryA = "select * from Table_1";


                    SqlCommand commandA = new SqlCommand(sqlQueryA, connection);


                    SqlDataAdapter dataAdapterA = new SqlDataAdapter(commandA);


                    DataTable dtbl1A= new DataTable();


                    dataAdapterA.Fill(dtbl1A);
                    //dataGridView1.DataSource = dtbl1A;
                    T1 = dtbl1A;

                }


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQueryA1 = "select * from siteAP";


                    SqlCommand commandA1 = new SqlCommand(sqlQueryA1, connection);


                    SqlDataAdapter dataAdapterA1 = new SqlDataAdapter(commandA1);


                    DataTable dtbl1A1 = new DataTable();


                    dataAdapterA1.Fill(dtbl1A1);
                   // dataGridView1.DataSource = dtbl1A1;
                    T3 = dtbl1A1;

                }




            }


            catch(Exception ed )
            {
                MessageBox.Show(ed.ToString());
            }


            try
            {

                string emp = "";
                int A = 0;

                for (int i = 0; i < T0.Columns.Count; i++)
                {
                    for (int j = 0; j < T0.Rows.Count; j++)
                    {
                        string key = T0.Rows[j][i].ToString();
                        string expression = "EmplacementOpt = '" + key+"'";
                        DataRow[] foundRows;
                        

                        // Use the Select method to find all rows matching the filter.
                        foundRows = T1.Select(expression);
                        if (foundRows.Length != 0)
                        { 
                            
                            string EmpO = foundRows[0]["EmplacementAct"].ToString();
                            string classeO = foundRows[0]["Classe"].ToString();
                            emp = EmpO;
                            //this.label1.Text = emp;

                            string expression1 = "Emp = '" + EmpO + "'";
                            DataRow[] foundRows1;

                            foundRows1 = T3.Select(expression1);

                            if (foundRows1.Length != 0)
                            {

                                string classe1 = foundRows1[0]["Classe"].ToString();


                                if (classe1 == "A         ") { dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Red; }

                                if (classe1 == "B         ") { dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Yellow; }
                                if (classe1 == "C         ") { dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.Lime; }



                            }



                        }




                        //cloring datagridView 

                       
                           



                    }
                }



            }
            catch (Exception ad ) 
            {
                MessageBox.Show(ad.ToString());
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
