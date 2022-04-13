using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {
            string connectionString3 = "server=127.0.0.1;database=place;uid=root;pwd=";
            try
            {

                using (MySqlConnection connection1 = new MySqlConnection(connectionString3))
                {
                    string sqlQuery5 = "SELECT * from mag1";


                    MySqlCommand command5 = new MySqlCommand(sqlQuery5, connection1);


                    MySqlDataAdapter dataAdapter2 = new MySqlDataAdapter(command5);


                    DataTable dtbl5 = new DataTable();


                    dataAdapter2.Fill(dtbl5);

                    // dataGridViewRes.DataSource = dtbl5;
                    this.dataGridView1.DataSource = dtbl5;


                }

            }
            catch
            {
                MessageBox.Show("Connection");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            

        }
    }
}
