using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GymManager.FrontEnd
{
    public partial class frmDialog : Form
    {
        string Direction = "";
        ArrayList PARAMS = new ArrayList();
        public static bool State = false;

        public frmDialog()
        {
            InitializeComponent();
        }
        public frmDialog(string text)
        {
            InitializeComponent();
            //this.Width = Screen.PrimaryScreen.Bounds.Width;
            label1.Text = text;
        }
        public frmDialog(string text, bool showno)
        {
            InitializeComponent();
            //this.Width = Screen.PrimaryScreen.Bounds.Width;
            label1.Text = text;
            button2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            State = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            State = false;
            this.Close();
        }
    }
}
