using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
           


            if (comboBox1.SelectedItem.ToString() == "Profile1" && textBox1.Text == "admin")
            {
                new Form4().Show();
                this.Hide(); 
            }
            
            else
            {

                if (comboBox1.SelectedItem.ToString() == "Admin" && textBox1.Text == "admin")
                {
                    new Form3().Show();
                    this.Hide();
                }
                else
                {

                    MessageBox.Show("identifiant ou mot de passe incorrecte");
                    comboBox1.Focus();
                    textBox1.Clear();
                }
            }
    
            
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to close?", "Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Application.Exit();
            }
           
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           //
           //
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
          //
            this.Hide();

        }
    }
}
