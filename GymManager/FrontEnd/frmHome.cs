using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.IsolatedStorage;
using FoxLearn.License;

namespace GymManager.FrontEnd
{
    public partial class frmHome : Form
    {
        BackEnd.CLASS_Resistration _Reg = new BackEnd.CLASS_Resistration();
        FrontEnd.frmSubData frmsubdata;
        FrontEnd.frmGamesData frmGamesData;
        FrontEnd.frmReservData frmReservData;
        FrontEnd.frmRegisteration frmReg;
        string ComputerInfoString;
        string ProductKey;
        public frmHome()
        {
            InitializeComponent();
            this.Text = "الرئيسية";
            ComputerInfoString = ComputerInfo.GetComputerId();
            ProductKey = _Reg.GetProductKey();
            string Decrypted = _Reg.Decrypt(ProductKey, "s3lw-3xr8-sqoy18");
            if (Decrypted == ComputerInfoString)
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button1.Visible = false;
                panel3.Height = button2.Height;
                panel3.Top = button2.Top;
                this.Text = "المشتركين";
                frmsubdata = new FrontEnd.frmSubData();
                groupBox1.Controls.Clear();
                frmsubdata.TopLevel = false;
                groupBox1.Controls.Add(frmsubdata);
                frmsubdata.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmsubdata.Dock = DockStyle.Fill;
                frmsubdata.Show();

            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                panel3.Height = button1.Height;
                panel3.Top = button1.Top;
                this.Text = "الرئيسية";
                frmReg = new FrontEnd.frmRegisteration();
                groupBox1.Controls.Clear();
                frmReg.TopLevel = false;
                groupBox1.Controls.Add(frmReg);
                frmReg.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                frmReg.Dock = DockStyle.Fill;
                frmReg.Show();

            }

        }

        public static void Loading()
        {
            FrontEnd.frmLoading loading = new FrontEnd.frmLoading();
            Application.Run(loading);
        }
        //المشتركين
        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Height = button2.Height;
            panel3.Top = button2.Top;
            this.Text = "المشتركين";
            frmsubdata = new FrontEnd.frmSubData();
            groupBox1.Controls.Clear();
            frmsubdata.TopLevel = false;
            groupBox1.Controls.Add(frmsubdata);
            frmsubdata.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmsubdata.Dock = DockStyle.Fill;
            frmsubdata.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Height = button3.Height;
            panel3.Top = button3.Top;
            this.Text = "التدريبات";
            frmGamesData = new FrontEnd.frmGamesData();
            groupBox1.Controls.Clear();
            frmGamesData.TopLevel = false;
            groupBox1.Controls.Add(frmGamesData);
            frmGamesData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmGamesData.Dock = DockStyle.Fill;
            frmGamesData.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel3.Height = button4.Height;
            panel3.Top = button4.Top;
            this.Text = "الحجوزات";
            frmReservData = new FrontEnd.frmReservData();
            groupBox1.Controls.Clear();
            frmReservData.TopLevel = false;
            groupBox1.Controls.Add(frmReservData);
            frmReservData.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmReservData.Dock = DockStyle.Fill;
            frmReservData.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Height = button1.Height;
            panel3.Top = button1.Top;
            this.Text = "الرئيسية";
            frmReg = new FrontEnd.frmRegisteration();
            groupBox1.Controls.Clear();
            frmReg.TopLevel = false;
            groupBox1.Controls.Add(frmReg);
            frmReg.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            frmReg.Dock = DockStyle.Fill;
            frmReg.Show();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            //panel3.Height = button1.Height;
            //panel3.Top = button1.Top;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            //panel3.Height = button2.Height;
            //panel3.Top = button2.Top;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            //panel3.Height = button3.Height;
            //panel3.Top = button3.Top;
        }

        private void frmHome_Load(object sender, EventArgs e)
        {

        }




    }
}
