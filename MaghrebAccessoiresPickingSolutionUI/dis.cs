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

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class dis : Form
    {
        public dis()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            //correction des emplacement 

                radioButton2.Checked = false;
            string fac = this.textBox1.Text;

            decimal gai = 0; 
            string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = copydb;";
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";

            try
            {


                if (radioButton1.Checked == true) { 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select ItemCode from INV1 where DocEntry like '%"+fac+"%' or BaseEntry like '%"+fac+"%'";
                   

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                  
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                 
                    DataTable dtbl = new DataTable();
     
                    dataAdapter.Fill(dtbl);
                    dataGridView1.DataSource = dtbl;

                        string val2;
                       

                   for (int j2 = 0; j2 < dtbl.Rows.Count; j2++)
                        {
                          val2 = dtbl.Rows[j2]["ItemCode"].ToString();


                            using (SqlConnection connection1 = new SqlConnection(connectionString1))
                            {
                                string sqlQuery1 = "select t1.Reference , t1.EmplacementAct  AS ACT ,t1.EmplacementOpt , t2.Distance AS D1 From Table_1 t1 join siteAP t2  on t1.EmplacementOpt= t2.Emp  where t1.Reference = '" + val2+"'";


                                SqlCommand command1 = new SqlCommand(sqlQuery1, connection1);

                                SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);

                                DataTable dtbl1 = new DataTable();

                                dataAdapter1.Fill(dtbl1);

                                
                                dataGridView2.DataSource = dtbl1;
                                string empact = dtbl1.Rows[0]["ACT"].ToString();
                                dtbl1.Columns.Add("D2",typeof(decimal));


                                using (SqlConnection connection2 = new SqlConnection(connectionString1))
                                {

                                   
                                    string sqlQuery2 = "SELECT  [Distance] AS D21 FROM [siteAP] where EMP like '%" + empact + "%'";


                                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection2);

                                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);

                                    DataTable dtbl2 = new DataTable();

                                    dataAdapter2.Fill(dtbl2);
                                    dtbl1.Rows[0]["D2"] = Convert.ToDecimal(dtbl2.Rows[0]["D21"].ToString()) ;

                                    dataGridView3.DataSource = dtbl2;
                                    //dtbl1.Columns.Add("D");



                                }



                            }


                        }




                        this.dataGridView1.DataSource = dtbl;
                    

                }





                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }





        }
    }
}
