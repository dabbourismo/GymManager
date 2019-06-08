using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GymManager.FrontEnd
{
    public partial class frmAddGames : Form
    {
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();
        frmGamesData frmGamesData;
        string IDEdit = "";
        public frmAddGames()
        {
            InitializeComponent();
        }
        public frmAddGames(frmGamesData owner)
        {
            InitializeComponent();
            frmGamesData = owner;
            this.Text = "اضافة تدريب";
        }
        public frmAddGames(frmGamesData owner, string GameID, string GameName)
        {
            InitializeComponent();
            frmGamesData = owner;
            this.Text = "تعديل تدريب";
            IDEdit = GameID;
            textBox1.Text = GameName;
        }
        //اضافة لعبة
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                new frmDialog("فضلا ادخل اسم التدريب").ShowDialog();
                //MessageBox.Show("فضلا ادخل اسم التدريب ");
                return;
            }
            else
            {
                if (IDEdit.Length > 0)
                {
                    if (_Games.EditGame(IDEdit, textBox1.Text)) ;
                    {
                        //MessageBox.Show("تم التعديل بنجاح");
                        frmGamesData.Focus();
                        frmGamesData.RefreshAfterEdit();
                        this.Close();
                    }
                }
                else
                {
                    if (_Games.InsertGame(textBox1.Text))
                    {
                        //MessageBox.Show("تمت الاضافة بنجاح");
                        frmGamesData.Focus();
                        frmGamesData.RefreshAfterAdd();
                        this.Close();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(sender, e);
            }
            
        }

        private void frmAddGames_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
