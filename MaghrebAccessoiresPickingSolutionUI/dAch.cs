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
    public partial class dAch : Form
    {
        public dAch()
        {
            InitializeComponent();
        }
        Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Mag = this.textBox4.Text ;
            string Al = this.textBox2.Text ;
           // this.dataGridView1.Visible = false; 




            // taux d'occupation 
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT  [CM] AS CM, COUNT( Distinct ([EmplacementOpt])) AS OCC FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag+"-A"+Al+"%' Group by [CM] ";


                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                    DataTable dtbl2 = new DataTable();
                    dataAdapter2.Fill(dtbl2);


                    Decimal TotalPrice = 0; 
                    for ( int i=0; i < dtbl2.Rows.Count; i++)
                    {
                        TotalPrice += Convert.ToDecimal(dtbl2.Rows[i]["OCC"].ToString());
                    }


                    //ajout du vide 
                    DataRow workRow = dtbl2.NewRow();
                    workRow["CM"] = "Espace Vide";
                    workRow["OCC"] = (decimal) 152 - TotalPrice ;
                    dtbl2.Rows.Add(workRow);

                    dataGridView1.DataSource = dtbl2;
                    pieChart1.Visible = true;
                    //dataGridView1.Visible = true;

                    SeriesCollection piechartData = new SeriesCollection();
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        piechartData.Add(new PieSeries { Title = item.Cells["CM"].Value.ToString(), Values = new ChartValues<decimal> { Convert.ToDecimal(item.Cells["OCC"].Value) }, DataLabels = true, LabelPoint = labelPoint });
                    }
                    pieChart1.Series = piechartData;

                }

                //taux de solicitaion

                /*

                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT  [CM] AS CM, COUNT( Distinct ([EmplacementOpt])) AS OCC FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag + "-A" + Al + "%' Group by [CM] ";


                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                    DataTable dtbl3 = new DataTable();
                    dataAdapter2.Fill(dtbl3);


                    Decimal TotalPrice = 0;
                    for (int i = 0; i < dtbl3.Rows.Count; i++)
                    {
                        TotalPrice += Convert.ToDecimal(dtbl3.Rows[i]["OCC"].ToString());
                    }


                    //ajout du vide 
                    DataRow workRow = dtbl3.NewRow();
                    workRow["CM"] = "Espace Vide";
                    workRow["OCC"] = (decimal)152 - TotalPrice;
                    dtbl3.Rows.Add(workRow);

                    dataGridView2.DataSource = dtbl3;
                    pieChart2.Visible = true;
                    //dataGridView1.Visible = true;

                    SeriesCollection piechartData = new SeriesCollection();
                    foreach (DataGridViewRow item in dataGridView2.Rows)
                    {
                        piechartData.Add(new PieSeries { Title = item.Cells["CM"].Value.ToString(), Values = new ChartValues<decimal> { Convert.ToDecimal(item.Cells["OCC"].Value) }, DataLabels = true, LabelPoint = labelPoint });
                    }
                    pieChart2.Series = piechartData;

              string sqd=  "select t1.U_CodeMarque , COUNT(DISTINCT t3.DocEntry   ) from [copydb].[dbo].[oitm] t1  join [copydb].[dbo].[inv1] t3 on t1.ItemCode = t3.ItemCode join [EmpOptimisation].[dbo].[Table_1] t2 on  t3.ItemCode = t2.Reference where t3.ShipDate between '2018' and '2022' and t1.U_CodeMarque is not NULL and t2.EmplacementOpt like '%MAG1%' Group by t1.U_CodeMarque ";


                }*/


            }
            catch
            {
                MessageBox.Show("Vezraa");
            }






        }

        private void dAch_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            pieChart1.Visible = false;
            dataGridView1.Visible = false;

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
