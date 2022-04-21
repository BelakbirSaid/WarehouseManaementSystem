using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaghrebAccessoiresPickingSolutionUI
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        private Form activeForm = null;
        private void button3_Click(object sender, EventArgs e)
        {



            
            panelChildForm.Visible = true; 
            openChildFormInPanel(new NavLogMenu());





        }


        
        private void openChildFormInPanel(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            panelChildForm.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {


            panelChildForm.Visible = true;

            openChildFormInPanel(new IP());



        }

        private void button2_Click(object sender, EventArgs e)
        {

            panelChildForm.Visible = true;

            openChildFormInPanel(new operations());


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://sites.google.com/view/pfeavancementorderpicking/home");


        }

        private void button4_Click(object sender, EventArgs e)
        {

            panelChildForm.Visible = true;
            openChildFormInPanel(new HP());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            panelChildForm.Visible = true;
            panelChildForm.Visible = true;

            openChildFormInPanel(new et());
        }
    }
}
