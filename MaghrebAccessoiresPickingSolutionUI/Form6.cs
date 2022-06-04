using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.IO;
//using MySql.Data.MySqlClient;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Form6 : Form
    {
        public Form6()
        { 
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
        }

   

        private void button3_Click(object sender, EventArgs e)
        {


            string refe = textBox1.Text;
            string cm = textBox2.Text;
            string fam = textBox3.Text;

            string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database = EmpOptimisation;";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(connectionString1))
                {
                    string sqlQuery2 = "SELECT [Reference] , [EmplacementAct] EMP_Actuel,[EmplacementOpt] AS Nouvel_Emp  ,[Classe] ,[qtystock] AS Stock,[CM] AS Code_Marque,[Famille],[Description],[blocs] AS Nombre_Unite_Didie FROM [Table_1] Where [Reference] like '%" + refe + "%' AND [CM] like '"+ cm + "'  AND  [Famille] like '%"+ fam + "%'";


                    SqlCommand command2 = new SqlCommand(sqlQuery2, connection1);


                    SqlDataAdapter dataAdapter2 = new SqlDataAdapter(command2);


                    DataTable dtbl2 = new DataTable();


                    dataAdapter2.Fill(dtbl2);

                    dataGridView1.DataSource = dtbl2;


                }
            }
            catch
            {
                MessageBox.Show("Vérfier la cnx à la bd");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CSV (*.csv)|*.csv";
                sfd.FileName = "Output.csv";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Erreur de Stockage du fichier " + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            int columnCount = dataGridView1.Columns.Count;
                            string columnNames = "";
                            string[] outputCsv = new string[dataGridView1.Rows.Count + 1];
                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames += dataGridView1.Columns[i].HeaderText.ToString() + ",";
                            }
                            outputCsv[0] += columnNames;

                            for (int i = 1; i < dataGridView1.Rows.Count; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    outputCsv[i] += dataGridView1.Rows[i - 1].Cells[j].Value.ToString() + ",";
                                }
                            }

                            File.WriteAllLines(sfd.FileName, outputCsv, Encoding.UTF8);
                            MessageBox.Show("opération terminée!", "Info");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("La table est Vide !!!", "Info");
            }


        }
    }
}
