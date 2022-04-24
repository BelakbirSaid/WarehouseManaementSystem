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
        public DataTable dtbL;
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string emplacement;
            string op;
            
            emplacement = "MAG" + comboBox1.Text + "-A" + comboBox2.Text;

            string connectionString = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT [Reference] ,[Description],[EmplacementOpt] , [EmplacementAct] ,[qtystock] AS Stock,[CM] AS Code_Marque,[blocs] AS QT_MAX FROM [Table_1] Where [EmplacementAct] like '%" + emplacement + "%'";
                    string sqlQuery1 = "SELECT * from opera ";

                    

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlCommand command1 = new SqlCommand(sqlQuery1, connection);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);
                    DataTable dtbl = new DataTable();
                    


                    dataAdapter.Fill(dtbl);

                    
                    DataTable dtbl11 = new DataTable();

                    dataAdapter1.Fill(dtbl11);


                    
                    op = dtbl11.Rows[dtbl11.Rows.Count-1]["Nu_op"].ToString();

                    textBox2.Text = op.ToString();
                    //this.textBox1.Text = op.ToString();
                    dtbL = dtbl.Copy();
                   // dataGridView1.DataSource = dtbL;

                    // dataGridView1.DataSource = dtbl;
                    connection.Close();
                    //
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    // up   t1
                    string mag  = this.comboBox1.Text ;
                    String magas1 = this.comboBox3.Text;

                   /// string magas1 = this.comboBox3.Text ;
                    connection.Open();
                    string sql = "UPDATE opera  SET  Magasin = @mag, magasinier = @magas1 , EtatOp = 'EnAttente'  Where Nu_op = @op";

                    using (SqlCommand cmd1 = new SqlCommand(sql, connection))
                    {

                        
                        cmd1.Parameters.AddWithValue("@mag", mag);
                        cmd1.Parameters.AddWithValue("@magas1", magas1);
                        cmd1.Parameters.AddWithValue("@op", op);

                       
                        cmd1.ExecuteNonQuery();
                        connection.Close();
                    }


                    // aj  t2 
                    connection.Open();
                  

                    for 
                        (int i = 0; i<dtbL.Rows.Count; i++ ) { 

                    string connectionString2 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
                    using (SqlConnection connection3 = new SqlConnection(connectionString2))
                    {
                        string Num_op = op;
                        string EmAc = dtbL.Rows[i]["EmplacementAct"].ToString();
                        string EmOp = dtbL.Rows[i]["EmplacementOpt"].ToString();
                        string Quant = dtbL.Rows[i]["Stock"].ToString();
                        string QuantMax = dtbL.Rows[i]["QT_MAX"].ToString();

                        string EtatRef = "EnAttente";
                        string sql1 = "insert INTO Table_2OP Values(@1,@2,@3,@4,@5,'',@7)";

                        connection3.Open();
                        using (SqlCommand cmd2 = new SqlCommand(sql1, connection3))
                        {

                            cmd2.Parameters.AddWithValue("@1", Num_op);
                            cmd2.Parameters.AddWithValue("@2", EmAc);
                            cmd2.Parameters.AddWithValue("@3", EmOp);
                            cmd2.Parameters.AddWithValue("@4", Quant);
                            cmd2.Parameters.AddWithValue("@5", QuantMax);
                            cmd2.Parameters.AddWithValue("@7", EtatRef);
                          


                                // assign value to parameter 

                                cmd2.ExecuteNonQuery();
                        }
                    
                        }


                    }



                    // 


                    // dataGridView1.DataSource = dtbl;

                    //
                }


                this.textBox2.Text = "";
                this.comboBox3.Text = "";
                this.comboBox1.Text = "";
                this.comboBox2.Text = "";
                MessageBox.Show("Opération ajoutée ! ");















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

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
