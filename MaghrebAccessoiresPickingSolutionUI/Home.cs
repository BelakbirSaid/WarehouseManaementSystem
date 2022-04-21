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

        private void button3_Click(object sender, EventArgs e)
        {



            openChildFormInPanel(new NavLogMenu());




        }


        private Form activeForm = null;
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




            openChildFormInPanel(new IP());



        }

        private void button2_Click(object sender, EventArgs e)
        {



            openChildFormInPanel(new operations());


        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://sites.google.com/view/pfeavancementorderpicking/home");


        }

        private void button4_Click(object sender, EventArgs e)
        {




        }
    }
}
