﻿using System;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            
            InitializeComponent();
            customdesign();
            //this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            

        }
        


        private void customdesign()
        {
            panelclassSub.Visible = false;
            panelHeatmapsub.Visible = false;
            panelNavsub.Visible = false;
            paneltoolssub.Visible = false; 
        }

        
        /// fonction pour faire  disparaitre le  submenue 
        private void hideSub()
        {
            if (panelclassSub.Visible == true)
                panelclassSub.Visible = false ;
            if (panelHeatmapsub.Visible == true )
                panelHeatmapsub.Visible = false;
            if(panelNavsub.Visible ==true )
                panelNavsub.Visible = false;
            if (paneltoolssub.Visible == true )
                paneltoolssub.Visible = false;
        }


        /// fonction pour afficher le  submenue 
        /// 
        private void showSub(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                hideSub();
                submenu.Visible = true;
            }
            else
                submenu.Visible = false; 

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //this.button11.Visible = false;
          //  this.button3.Visible = false; 
           
            openChildFormInPanel(new Home());

            //this.Style.BackColor = Color.DarkGray;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showSub(panelNavsub);

            openChildFormInPanel(new NavLogMenu());









        }

        private void button8_Click(object sender, EventArgs e)
        {
            //..

            openChildFormInPanel(new Form5());

            hideSub();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new Form6());
            //..
            hideSub();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //..
            hideSub();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            showSub(panelclassSub);

        }

        private void button12_Click(object sender, EventArgs e)
        {

            openChildFormInPanel(new UpdateEmps());


            
           
           //

            hideSub();

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //..
            openChildFormInPanel(new Form7());

            hideSub();
        }

        

        private void button13_Click(object sender, EventArgs e)
        {
            showSub(panelHeatmapsub);


        }

        

        private void button15_Click(object sender, EventArgs e)
        {
            //..
            hideSub();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //..
            hideSub();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showSub(paneltoolssub);

        }

        private void button17_Click(object sender, EventArgs e)
        {
            openChildFormInPanel(new dis());

          
            //..
            hideSub();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //..
            hideSub();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //..
            hideSub();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Process.Start("https://sites.google.com/view/pfeavancementorderpicking/home");


            //..
            hideSub();
        }

        private void button19_Click(object sender, EventArgs e)
        {


            openChildFormInPanel(new et());


            //..
            hideSub();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            openChildFormInPanel(new Home());

            //..
            hideSub();
        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

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

        private void button6_Click_1(object sender, EventArgs e)
        {
            //

            hideSub();

        }

        private void button11_Click_1(object sender, EventArgs e)
        {

           

        }

        private void button10_Click(object sender, EventArgs e)
        {






        }

        private void button16_Click(object sender, EventArgs e)
        {


            openChildFormInPanel(new HP());//..

            hideSub();

            //new HP().Show();
            //this.Hide();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            new DashboardWS().Show();
            this.Hide();


        }
    }
}
