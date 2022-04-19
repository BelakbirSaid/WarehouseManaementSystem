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
        //Func<ChartPoint, string> labelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        Func<ChartPoint, string> labelPoint1 = chartPoint1 => string.Format("{0} ({1:P})", chartPoint1.Y, chartPoint1.Participation);
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Mag = this.textBox4.Text ;
            string Al = this.textBox2.Text ;
            this.label1.Visible = true;
            label1.Text = "Taux d'occupation  par CM";
            label5.Text = "Taux d'occupation  par CM";


           // this.dataGridView1.Visible = false; 




            // taux d'occupation 
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT  [CM] AS CM, COUNT( Distinct ([EmplacementOpt])) AS OCC FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag+"-A"+Al+"%' Group by [CM] ";
                    string sqlQuery3 = "SELECT [Famille] AS FM, COUNT( Distinct ([EmplacementOpt])) AS OCC1 FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag + "-A" + Al + "%' Group by [Famille] ";


                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                    DataTable dtbl2 = new DataTable();
                    dataAdapter2.Fill(dtbl2);



                    SqlCommand command3 = new SqlCommand(sqlQuery3, connection1);


                    SqlDataAdapter dataAdapter3 = new SqlDataAdapter(command3);


                    DataTable dtbl3 = new DataTable();
                    dataAdapter3.Fill(dtbl3);

                   ///////////////////////////
                    Decimal TotalPrice = 0;
                    for (int i = 0; i < dtbl2.Rows.Count; i++)
                    {
                        TotalPrice += Convert.ToDecimal(dtbl2.Rows[i]["OCC"].ToString());
                    }
                    //ajout du vide 
                    DataRow workRow = dtbl2.NewRow();
                    workRow["CM"] = "Espace Vide";
                    workRow["OCC"] = (decimal) 152 - TotalPrice ;
                    dtbl2.Rows.Add(workRow);

                    dataGridView1.DataSource = dtbl2;
                    dataGridView2.DataSource = dtbl3;
                    dataGridView2.Visible = false; 
                    label5.Visible = true;
                    pieChart1.Visible = true;
                    //dataGridView1.Visible = true;

                    SeriesCollection piechartData = new SeriesCollection();
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        piechartData.Add(new PieSeries { Title = item.Cells["CM"].Value.ToString(), Values = new ChartValues<decimal> { Convert.ToDecimal(item.Cells["OCC"].Value) }, DataLabels = true, LabelPoint = labelPoint1 });
                    }
                    pieChart1.Series = piechartData;



                    //////////////////
                    ///


                }




               
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    
                    string sqlQuery3 = "SELECT [Famille] AS FM, COUNT( Distinct ([EmplacementOpt])) AS OCC1 FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + Mag + "-A" + Al + "%' Group by [Famille] ";

                    SqlCommand command3 = new SqlCommand(sqlQuery3, connection1);


                    SqlDataAdapter dataAdapter3 = new SqlDataAdapter(command3);


                    DataTable dtbl3 = new DataTable();
                    dataAdapter3.Fill(dtbl3);

                    ///////////////////////////
                    Decimal TotalPrice1 = 0;
                    for (int i = 0; i < dtbl3.Rows.Count; i++)
                    {
                        TotalPrice1 += Convert.ToDecimal(dtbl3.Rows[i]["OCC1"].ToString());
                    }
                    //ajout du vide 
                    DataRow workRow = dtbl3.NewRow();
                    workRow["FM"] = "Espace Vide";
                    workRow["OCC1"] = (decimal)152 - TotalPrice1;
                    dtbl3.Rows.Add(workRow);

                   
                    dataGridView2.DataSource = dtbl3;
                    dataGridView2.Visible = false;
                    label5.Visible = true;
                    chart1.Visible = true;
                    //dataGridView1.Visible = true;


                    //Set DataTable as data source to Chart
                    chart1.DataSource = dtbl3;

                    //Mapping a field with x-value of chart
                    this.chart1.Series[0].XValueMember = "FM";
                    this.chart1.Series[0].YValueMembers = "OCC1";
                    this.chart1.DataBind();

          


                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }






        }

        private void dAch_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;

            pieChart1.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            this.label5.Visible = false;
            label5.Visible = false;
            chart1.Visible = false;
            label1.Visible = false; 
            //on maga


           

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.label1.Visible = true;
            label5.Text = "Taux d'occupation Magasin";
            label1.Text = "Taux de solicitation par allée";
            this.label5.Visible = true;

            string mag1 = this.textBox3.Text;



            // taux d'occupation 
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT  [CM] AS CM, COUNT( Distinct ([EmplacementOpt])) AS OCC FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + mag1 + "%' Group by [CM] ";



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
                    workRow["OCC"] = (decimal)1948 - TotalPrice;
                    dtbl2.Rows.Add(workRow);

                    dataGridView1.DataSource = dtbl2;
                    
   
              
                    pieChart1.Visible = true;
                    //dataGridView1.Visible = true;

                    SeriesCollection piechartData = new SeriesCollection();
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        piechartData.Add(new PieSeries { Title = item.Cells["CM"].Value.ToString(), Values = new ChartValues<decimal> { Convert.ToDecimal(item.Cells["OCC"].Value) }, DataLabels = true, LabelPoint = labelPoint1 });
                    }
                    pieChart1.Series = piechartData;



                    //////////////////
                    ///


                }



                

                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {

                    string sqlQuery3 = "SELECT [Famille] AS FM, COUNT( Distinct ([EmplacementOpt])) AS OCC1 FROM [Table_1] Where  [EmplacementOpt] like '%MAG" + mag1+"%' Group by [Famille] ";

                    SqlCommand command3 = new SqlCommand(sqlQuery3, connection1);


                    SqlDataAdapter dataAdapter3 = new SqlDataAdapter(command3);


                    DataTable dtbl3 = new DataTable();
                    dataAdapter3.Fill(dtbl3);

                    ///////////////////////////
                    Decimal TotalPrice1 = 0;
                    for (int i = 0; i < dtbl3.Rows.Count; i++)
                    {
                        TotalPrice1 += Convert.ToDecimal(dtbl3.Rows[i]["OCC1"].ToString());
                    }
                    //ajout du vide 
                    DataRow workRow = dtbl3.NewRow();
                    workRow["FM"] = "Espace Vide";
                    workRow["OCC1"] = (decimal)152 - TotalPrice1;
                    dtbl3.Rows.Add(workRow);


                    dataGridView2.DataSource = dtbl3;
                    dataGridView2.Visible = false;
                    label5.Visible = true;
                    chart1.Visible = true;
                    //dataGridView1.Visible = true;


                    //Set DataTable as data source to Chart
                    chart1.DataSource = dtbl3;

                    //Mapping a field with x-value of chart
                    this.chart1.Series[0].XValueMember = "FM";
                    this.chart1.Series[0].YValueMembers = "OCC1";
                    this.chart1.DataBind();




                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }















        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
