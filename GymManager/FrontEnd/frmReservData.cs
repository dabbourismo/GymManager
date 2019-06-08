using GymManager.BackEnd;
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
    public partial class frmReservData : Form
    {
        BackEnd.CLASS_Subs _Subs = new BackEnd.CLASS_Subs();
        public frmReservData()
        {
            InitializeComponent();
            ShowSubs();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }
        //اقتناص الباركود
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ArrayList SubIDArray = new ArrayList();
                SubIDArray = _Subs.GetAllSubID();

                ArrayList SubArray = new ArrayList();
                SubArray = _Subs.SubGetData(Convert.ToInt32(textBox2.Text));
                string SubID;
                try
                {
                     SubID = SubArray[9].ToString();
                }
                catch (Exception)
                {
                    new frmDialog("هذا الكود غير موجود").ShowDialog();
                    //MessageBox.Show("هذا الكود غير موجود");
                    textBox2.Text = "";
                    textBox2.Focus();
                    return;
                }
                int RemainingNumOfSessions = _Subs.GetRemainingNumOfSessions(SubID);

                DateTime EndDate = Convert.ToDateTime(SubArray[7].ToString());
                DateTime TodayDate = DateTime.Now;

                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    if (!SubIDArray.Contains(textBox2.Text))
                    {
                        if ((TodayDate > EndDate))
                        {
                            if (RemainingNumOfSessions != 0)
                            {
                                if (RemainingNumOfSessions == 2)
                                {
                                    new frmDialog("تم تسجيل الحضور مع العلم بان الجلسة القادمة اخر جلسة لهذا المتدرب").ShowDialog();
                                    //MessageBox.Show("تم تسجيل الحضور مع العلم بان الجلسة القادمة اخر جلسة");
                                }
                                if (RemainingNumOfSessions == 1)
                                {
                                    new frmDialog("هذة اخر جلسة لهذا المتدرب").ShowDialog();
                                    //MessageBox.Show("هذة اخر جلسة لهذا المتدرب");
                                }
                                _Subs.Reduce_IncreaseNumOfSessions(SubID);

                                ShowSubData(SubArray, SubID);
                                textBox2.Text = "";
                                textBox2.Focus();
                            }
                            else
                            {
                                ShowSubData(SubArray, SubID);
                                new frmDialog("انتهي رصيد الجلسات لهذا المتدرب").ShowDialog();
                                //MessageBox.Show("انتهي رصيد الجلسات لهذا المتدرب");
                                label17.ForeColor = System.Drawing.Color.FromArgb(178, 8, 55);
                                textBox2.Text = "";
                                textBox2.Focus();
                                return;
                            }
                        }
                        else
                        {
                            ShowSubData(SubArray, SubID);
                            new frmDialog("انتهي الشهر لهذا المتدرب").ShowDialog();
                            //MessageBox.Show("انتهي الشهر لهذا المتدرب");
                            //صفر عدد الحصص
                            textBox2.Text = "";
                            label20.ForeColor = System.Drawing.Color.FromArgb(178, 8, 55);
                            textBox2.Focus();
                            return;
                        }
                    }
                    else
                    {
                        new frmDialog("هذا الكود غير موجود").ShowDialog();
                        //MessageBox.Show("هذا الكود غير موجود");
                        textBox2.Text = "";
                        textBox2.Focus();
                        return;
                    }
                }
            }
        }


        //تسجيل حضور
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell==null)
            {
                new frmDialog("فضلا قم بتحديد متدرب").ShowDialog();
                //MessageBox.Show("فضلا قم بتحديد متدرب");
                return;
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string SubID = row.Cells[0].Value.ToString();
                textBox2.Text = SubID;
                textBox2.Focus();
                //Press Enter
                SendKeys.SendWait("{ENTER}");
                //clearSelection
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
            }
        }

        //بحث
        private void button4_Click(object sender, EventArgs e)
        {
            SearchSubs(textBox1.Text);
        }
        //طباعه باركود
        private void button5_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmPrintBarcode>().Any())
            {
                return;
            }
            else
            {
                new frmPrintBarcode().Show();
            }
           
        }









        public void ShowSubData(ArrayList SubArray,string SubID)
        {
            label11.Text = SubArray[0].ToString();
            label12.Text = SubArray[1].ToString();
            label13.Text = SubArray[2].ToString();
            label14.Text = SubArray[3].ToString();
            label15.Text = SubArray[4].ToString();
            label16.Text = SubArray[5].ToString();
            label17.Text = _Subs.GetRemainingNumOfSessions(SubID).ToString();
            string[] StartDateArr = SubArray[7].ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            label18.Text = StartDateArr[0].ToString().Substring(0, 10);

            string[] EndDateArr = SubArray[8].ToString().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            label20.Text = EndDateArr[0].ToString().Substring(0, 10);

            label20.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            label17.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
        }

        public void ShowSubs()
        {
            DataTable dtable = new DataTable();
            dtable = DBLayer.RefreshTable(@"SELECT
                            Subs.SubID,
                            Subs.SubName as 'اسم المتدرب',
                            Subs.SubTele as 'المحمول',
                            Subs.SubTele2,
                            Subs.SubCardNum,
                            Subs.SubAddress,
                            Games.GameName as 'اسم التدريب',
                            Reservations.NumOfSessions as'الجلسات المتبقية',
                            Reservations.StartDate,
                            Reservations.EndDate,
                            Reservations.ResID
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID");
            dataGridView1.DataSource = dtable;
        }

        public void SearchSubs(string SubName)
        {
            if (SubName.Length > 0)
            {
                DataTable dtable = new DataTable();
                dtable = DBLayer.RefreshTable(@"SELECT
                            Subs.SubID,
                            Subs.SubName as 'اسم المتدرب',
                            Subs.SubTele as 'المحمول',
                            Subs.SubTele2,
                            Subs.SubCardNum,
                            Subs.SubAddress,
                            Games.GameName as 'اسم التدريب',
                            Reservations.NumOfSessions as'الجلسات المتبقية',
                            Reservations.StartDate,
                            Reservations.EndDate,
                            Reservations.ResID
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID 
                                WHERE Subs.SubName LIKE '%" + SubName + "%'");
                dataGridView1.DataSource = dtable;
            }
            else
            {
                ShowSubs();
            }
           
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button4_Click(sender, e);
            }
        }

        private void frmReservData_Shown(object sender, EventArgs e)
        {
            textBox2.Focus();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        

        





    }
}
