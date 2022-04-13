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

   
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
        }
       
          /*
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Enabled = false;
            this.panel2.Enabled = true;
            
           // type = 1; 

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.panel3.Enabled = true;
            this.panel2.Enabled = false;
           // type = 2; 

        }
       */
        
        private void Form13_Load(object sender, EventArgs e)
        {
           // this.label7.Visible = false;
            radioButton2.Checked = true;

            textBox2.Text = "192.168.50.203";
            textBox1.Text = "MyInstance1";
            textBox5.Text = "dbo";

            comboBox2.Text = "B2B";
            textBox3.Text = "excel1";
            textBox4.Text = "mmmmmm";
            comboBox2.Text = "dbo";
            //radioButton1.Checked = true;
            //type = 1;
        }
        public string serverName;
        public string dataName;
        public string userName;
        public string pw;


        private void button3_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            if (radioButton2.Checked = true ) 
            {

                serverName = this.textBox2.Text;
                dataName = this.comboBox2.Text;
                userName = this.textBox3.Text;
                pw = this.textBox4.Text;

                string connectionString = string.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}", this.textBox2.Text, this.comboBox2.Text, textBox3.Text, textBox4.Text);



                try
                {
                    Alass1 helper = new Alass1(connectionString);
                    if (helper.Isconnection)
                        MessageBox.Show("connected");

                }
                catch
                {
                    MessageBox.Show("FD");
                }
            }
            /*
           // radioButton1.Checked = true;
            if ( radioButton1.Checked =true )
            {
                serverName = this.textBox1.Text;
                dataName = this.comboBox1.Text;
                string connectionString1 = string.Format("Server=(localdb)\\{0};Integrated Security=true; Database = {1};", textBox1.Text, comboBox1.Text);
              //  string connectionString1 = "Server=(localdb)\\MyInstance1;Integrated Security=true; Database =dbo;"; 
                try
                {
                    Alass1 alass1 = new Alass1(connectionString1);
                    if (alass1.Isconnection)
                        MessageBox.Show("bravo");


                }
                catch
                {
                    MessageBox.Show("FD");
                }

            }*/




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
