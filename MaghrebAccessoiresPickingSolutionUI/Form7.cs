using System;
using System.Data;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Form7 : Form
    {
        public decimal cl1 { get; set; }
        public decimal cl2 { get; set;  }
        public Form7()
        {

            InitializeComponent();
            this.panelfamille.Visible = false;
            this.label6.Visible = false;
            this.comboBox4.Visible = false;
            string connectionString = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT DISTINCT U_CodeMarque  from oitm";
                    string sqlQuery1 = "SELECT DISTINCT U_F_Art  from oitm";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    SqlCommand command1 = new SqlCommand(sqlQuery1, connection);



                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command1);


                    DataTable dtbl = new DataTable();
                    DataTable dtbl1 = new DataTable();


                    dataAdapter.Fill(dtbl);
                    dataAdapter1.Fill(dtbl1);

                    for (int i = 0; i < dtbl.Rows.Count; i++)
                    {
                        comboBox5.Items.Add(dtbl.Rows[i]["U_CodeMarque"].ToString());
                    }
                    for (int j = 0; j < dtbl1.Rows.Count; j++)
                    {
                        comboBox4.Items.Add(dtbl1.Rows[j]["U_F_Art"].ToString());
                    }

                    //comboBox1.DataSource = dtbl

                }
            }
            catch
            {
                MessageBox.Show("Vérfier la cnx à la bd");
            }


            //this.panelfamille.Visible = false; 
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.panelfamille.Visible = true;
            this.label6.Visible = true;
            this.comboBox4.Visible = true;
            this.label7.Visible = false;
            this.comboBox5.Visible = false;

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            this.panelfamille.Visible = true;
            this.label6.Visible = false;
            this.comboBox4.Visible = false;
            this.label7.Visible = true;
            this.comboBox5.Visible = true;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = copydb;"; 
            //string connectionString1 = "Data Source=192.168.50.203;Initial Catalog=B2B;User ID=excel1;Password=mmmmmm";
            string site = this.comboBox1.Text;
            int choix = 2;
            string niveau = this.comboBox3.Text;
            string CodeM = this.comboBox5.Text;// depending on choice 

            string Famille = this.comboBox4.Text; // depend on choice 

            string date1 = dateTimePicker1.Value.ToString("yyyy-dd-MM");
            string date2 = dateTimePicker2.Value.ToString("yyyy-dd-MM");


            // choosing 
            if (radioButton2.Checked == true)
            {
                radioButton3.Checked = false; 
                choix = 2; 

            }
            if ( radioButton3 .Checked == true )
            {
                radioButton2.Checked = false;
                choix = 1; 


            }
            
            if ( radioButton2.Checked == false && radioButton3.Checked== false )
            {
                choix = 3; 

            }

            try
            {

                using (SqlConnection connection2 = new SqlConnection(connectionString1))
                {
                   // string sqlQuery3;
                    string sqlQueryF = "select t1.ItemCode AS Reference , t3.U_emp AS Emplacement  , AVG(t1.PriceBefDi) AS PUM  , SUM( t1.Quantity ) as Quantity , Count( t1.ItemCode ) AS Nombre_factures ,t2.U_Volume , t2.SWeight1 from INV1 t1 join OITM t2 on t1.[ItemCode] = t2.[ItemCode] join OITW t3 on (t1.ItemCode = t3.ItemCode) where (t3.WhsCode = '01') and(t1.DocDate between '" + date1+"' and '"+date2+"') and(t3.U_emp like '%MAG"+niveau+"%') and(t2.U_F_Art like '"+Famille+"') group by t1.ItemCode ,t3.U_emp ,t2.U_Volume ,  t2.SWeight1 order by Quantity Desc";
                    string sqQueryC = "select t1.ItemCode AS Reference ,t3.U_emp AS Emplacement , AVG(t1.PriceBefDi) AS PUM  , SUM( t1.Quantity ) as Quantity , Count( t1.ItemCode ) AS Nombre_factures ,t2.U_Volume , t2.SWeight1 from INV1 t1 join OITM t2 on t1.[ItemCode] = t2.[ItemCode] join OITW t3 on (t1.ItemCode = t3.ItemCode) where (t3.WhsCode = '01') and(t1.DocDate between '" + date1+"' and '"+date2+"') and(t3.U_emp like '%MAG"+niveau+"%') and(t2.U_CodeMarque like '"+CodeM+"') group by t1.ItemCode ,t3.U_emp ,t2.U_Volume ,  t2.SWeight1 order by Quantity Desc";
                    
                    string sqQueryG = "select t1.ItemCode AS Reference ,t3.U_emp AS Emplacement , AVG(t1.PriceBefDi) AS PUM  , SUM( t1.Quantity ) as Quantity , Count( t1.ItemCode ) AS Nombre_factures ,t2.U_Volume , t2.SWeight1 from INV1 t1 join OITM t2 on t1.[ItemCode] = t2.[ItemCode] join OITW t3 on (t1.ItemCode = t3.ItemCode) where (t3.WhsCode = '01') and(t1.DocDate between '" + date1 + "' and '" + date2 + "') and(t3.U_emp like '%MAG" + niveau + "%')  group by t1.ItemCode ,t3.U_emp ,t2.U_Volume ,  t2.SWeight1 order by Quantity Desc";

                    DataTable dtbl3 = new DataTable();
                    switch (choix)
                    {
                        case 1:
                            //sqlQuery3 = sqlQueryC;
                            SqlCommand command3 = new SqlCommand(sqQueryC, connection2);
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command3); 
                            dataAdapter.Fill(dtbl3);
                           // dataGridViewSearch.DataSource = dtbl3;
                            break;
                        case 2:
                            //sqlQuery3 = sqQueryF;
                            SqlCommand command4 = new SqlCommand(sqlQueryF, connection2);
                            SqlDataAdapter dataAdapter4 = new SqlDataAdapter(command4);
                            //DataTable dtbl4 = new DataTable();
                            dataAdapter4.Fill(dtbl3);
                           // dataGridViewSearch.DataSource = dtbl3;

                            break;
                        case 3:
                            //sqlQuery3 = sqQueryF;
                            SqlCommand command5 = new SqlCommand(sqQueryG, connection2);
                            SqlDataAdapter dataAdapter5 = new SqlDataAdapter(command5);
                           // DataTable dtbl5 = new DataTable();
                            dataAdapter5.Fill(dtbl3);
                           // dataGridViewSearch.DataSource = dtbl3;  
                            break;
                                
                    }

                    // List des variabes   :Reference,Emplacement,PUM  (Prix Unitaire Moyen) ,Quantity (qté vendu pendant la période T) ,Nombre_factures,t2.U_Volume,t2.SWeight1


                    //Normalisation
                    //calcul des min et max  
                    dtbl3.Columns.Add("produ", typeof(decimal), "PUM*Quantity");
                    decimal minPum = Convert.ToDecimal(dtbl3.Compute("min([produ])", string.Empty));
                    decimal maxPum = Convert.ToDecimal(dtbl3.Compute("max([produ])", string.Empty));
                    decimal minNB = Convert.ToDecimal(dtbl3.Compute("min([Nombre_factures])", string.Empty));
                    decimal maxNB = Convert.ToDecimal(dtbl3.Compute("max([Nombre_factures])", string.Empty));



                    // normalisaton des variables 
                    dtbl3.Columns.Add("produN", typeof(decimal));
                    decimal val1;

                    for (int j1 = 0; j1 < dtbl3.Rows.Count; j1++)
                    {
                        val1 = Convert.ToDecimal(dtbl3.Rows[j1]["produ"].ToString());
                        dtbl3.Rows[j1]["produN"] = (decimal)( (val1 - minPum) /(maxPum-minPum ) );
                    }
                    //
                    
                    dtbl3.Columns.Add("NBN", typeof(decimal));
                    decimal val2;

                    for (int j2 = 0; j2 < dtbl3.Rows.Count; j2++)
                    {
                        val2 = Convert.ToDecimal(dtbl3.Rows[j2]["Nombre_factures"].ToString());
                        dtbl3.Rows[j2]["NBN"] = (decimal)((val2 - minNB) / (maxNB - minNB));
                    }

                    //
                    /*
                    dtbl3.Columns.Add("Nfact", typeof(decimal));
                    decimal val3;

                    for (int j3 = 0; j3 < dtbl3.Rows.Count; j3++)
                    {
                        val3 = Convert.ToDecimal(dtbl3.Rows[j3]["Nombre_factures"].ToString());
                        dtbl3.Rows[j3]["Nfact"] = (decimal)((val3 - minFact) / (maxFact - minFact));
                    }*/


                    //conditions on masse volumiques 
                    
                   // dtbl3.Columns.Add("masseVol", typeof(decimal)); 





                    

                    ///combinaison 
                   // dtbl3.Columns.Add("Xcombinaison", typeof(decimal), "Quantity");
                   // dtbl3.Columns.Add("Xcombinaison", typeof(decimal), "pumN *QN*Nfact");

                    /*

                    /// calcul des pourcentages 
                    dtbl3.Columns.Add("Pourcentages", typeof(decimal));

                   
                    decimal m = 0; 
                    for (int i = 0; i < dtbl3.Rows.Count;i++)
                    {
                         m += Convert.ToDecimal(dtbl3.Rows[i]["Xcombinaison"].ToString());
                    }

                    decimal pe;
                    for (int j = 0; j < dtbl3.Rows.Count; j++)
                    {
                        pe = Convert.ToDecimal(dtbl3.Rows[j]["Xcombinaison"].ToString());
                        dtbl3.Rows[j]["Pourcentages"] = (decimal)(pe / m); 
                    }
                    */

                    // Tri des tableaux 

                    //dtbl3.DefaultView.Sort = "Xcombinaison DESC";
                    //dtbl3 = dtbl3.DefaultView.ToTable(true);

                    // cumulation 
                    /*

                    dtbl3.Columns.Add("Cumu", typeof(decimal));
                    decimal cm = 0; 
                    for (int k= 0; k < dtbl3.Rows.Count; k++ ) 
                    {
                        cm += Convert.ToDecimal(dtbl3.Rows[k]["Pourcentages"]);

                        dtbl3.Rows[k]["Cumu"] = cm;
                    }*/

                    

                    //classes ABC  avec des seuils fixes 

                    //définition des seuils des classes ABC 

                    ///

                    //dtbl3.Columns.Add("Classe", typeof(string));
                    //string classe = "C";
                    //double p = 1; 


                    /*
                    for (int l = 0; l < dtbl3.Rows.Count; l++)
                    {
                        p= Convert.ToDouble(dtbl3.Rows[l]["Cumu"]);

                        if  (  p<=0.4  )
                        {
                            dtbl3.Rows[l]["classe"] = "A"; 
                        }
                        if (0.4 < p & p < 0.5)
                        {
                            dtbl3.Rows[l]["classe"] = "B";
                        }
                        if ( p >= 0.5 )
                        {
                            dtbl3.Rows[l]["classe"] = "C";

                        }



                       // dtbl3.Rows[l]["Cumu"] = classe;
                    }
                    */

                    DataTable copy = new DataTable();

                 //   copy = dtbl3.Columns
                    


                    this.dataGridViewSearch.DataSource = dtbl3;
                    /*
                    this.dataGridViewSearch.Columns[2].Visible = false;
                    this.dataGridViewSearch.Columns[3].Visible = false;
                    this.dataGridViewSearch.Columns[4].Visible = false;
                    this.dataGridViewSearch.Columns[5].Visible = false;
                    this.dataGridViewSearch.Columns[6].Visible = false;
                   // this.dataGridViewSearch.Columns[7].Visible = false;
                    this.dataGridViewSearch.Columns[8].Visible = false;
                    //   this.dataGridViewSearch.Columns[9].Visible = false;

                    */
                    //ploting pareto chart 

                    /*
                    this.chart1.DataSource = dtbl3;
                    chart1.Series["Cumu"].YValueMembers = "Cumu";
                    chart1.Series["Cumu"].XValueMember = "Reference";
                    //chart1.Series["Cumu"].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    //  chart1.Series["Cumu"].YValueType = decimal;  
                    */
                    //ploting pie classes

         
                }


            }
            catch 
            {
                MessageBox.Show("Vérifier  Votre connexion à la Base de données");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // detail 

            //dataGridViewSearch.DataSource = new DataTable();
            //dataGridViewSearch.Columns.Clear();
            //this.textBox1.Clear();
            this.comboBox4.Text = "";
            this.comboBox5.Text = "";
            this.comboBox1.Text = "";
            this.comboBox2.Text = "";
            this.comboBox3.Text = "";
            
           
            // 
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1Site_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panelfamille_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panelfree_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat ="yyyy-dd-MM";
            dateTimePicker2.CustomFormat = "yyyy-dd-MM ";
            this.comboBox1.Text = "01";
            this.comboBox1.Enabled = false;
            this.comboBox2.Text = "Autoplus" ;
            this.comboBox2.Enabled = false; 


           
            

 

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
