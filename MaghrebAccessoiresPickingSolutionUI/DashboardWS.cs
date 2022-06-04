using LiveCharts;
using LiveCharts.Wpf;
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
    public partial class DashboardWS : Form
    {
        public DashboardWS()
        {
            InitializeComponent();
        }
        Func<ChartPoint, string> labelPoint1 = chartPoint1 => string.Format("{0} ({1:P})", chartPoint1.Y, chartPoint1.Participation);

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            string Mag = this.comboBox1.Text;
            string Al = this.comboBox2.Text;

            if (Mag!="" && Al != "")
            {
                try
                {
                    using (SqlConnection connection1 = new SqlConnection(connectionString1))
                    {
                        string sqlQuery2 = "SELECT  [CM] AS CM, COUNT( Distinct ([EmplacementOpt])) AS OCC FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag + "-A" + Al + "%' Group by [CM] ";
                 


                        SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                        SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                        DataTable dtbl2 = new DataTable();
                        dataAdapter2.Fill(dtbl2);


                        ///////////////////////////
                        Decimal TotalPrice = 0;
                        for (int i = 0; i < dtbl2.Rows.Count; i++)
                        {
                            TotalPrice += Convert.ToDecimal(dtbl2.Rows[i]["OCC"].ToString());
                        }
                        //ajout du vide 
                        DataRow workRow = dtbl2.NewRow();
                        workRow["CM"] = "Espace Vide";
                        workRow["OCC"] = (decimal)152 - TotalPrice;
                        dtbl2.Rows.Add(workRow);

                        dataGridView1.DataSource = dtbl2;
                        
                        dataGridView1.Visible = true;
                         

                        pieChart1.Visible = true;


                        //dataGridView1.Visible = true;
                       
                        SeriesCollection piechartData = new SeriesCollection();
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            piechartData.Add(new PieSeries { Title = item.Cells["CM"].Value.ToString(), Values = new ChartValues<decimal> { Convert.ToDecimal(item.Cells["OCC"].Value) }, DataLabels = true, LabelPoint = labelPoint1 });
                        }
                        pieChart1.Series = piechartData;
                        


                    }



                }

                catch (Exception el)
                {
                    MessageBox.Show(el.ToString()); ; 
                }

            }

        }

        private void DashboardWS_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            panel5.Visible = false;

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
