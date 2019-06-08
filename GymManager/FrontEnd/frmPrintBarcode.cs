using GymManager.BackEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GymManager.FrontEnd
{
    public partial class frmPrintBarcode : Form
    {
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();
        public frmPrintBarcode()
        {
            InitializeComponent();
            comboBox2.DataSource = _Games.GetAllGames();
            comboBox1.SelectedIndex = 0;
            this.Text = "طباعة باركود";
        }

        //معاينة
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                new frmReporting("All", "All").Show();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                new frmReporting(comboBox2.Text, "Game").Show();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("فضلا ادخل الاسم");
                }
                else
                {
                    new frmReporting(textBox1.Text, "Name").Show();
                }

            }

        }


        //اغلاق
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Enabled = false;
                comboBox2.Enabled = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Enabled = false;
                comboBox2.Enabled = true;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Enabled = true;
                comboBox2.Enabled = false;
                AutoCompleteText();
            }
        }
        private void AutoCompleteText()
        {
            SQLiteCommand cm = new SQLiteCommand();
            cm.Connection = CurrentInfo.cn;
            SQLiteDataReader reader;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection AutoCompeleteString = new AutoCompleteStringCollection();

            cm.CommandText = @"SELECT SubName FROM Subs";
            using (reader = cm.ExecuteReader())
            {
                while (reader.Read())
                {
                    string Output = Convert.ToString(reader["SubName"]);
                    AutoCompeleteString.Add(Output);
                }
            }
            textBox1.AutoCompleteCustomSource = AutoCompeleteString;
        }
    }
}
