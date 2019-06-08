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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data;

namespace GymManager.FrontEnd
{
    public partial class frmSubData : Form
    {
        BackEnd.CLASS_Subs _Subs = new BackEnd.CLASS_Subs();
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();
        public frmSubData()
        {
            InitializeComponent();
            ShowSubs();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            //dataGridView1.Columns[9].Visible = false;
            //dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            comboBox1.SelectedIndex = 0;
        }
        //اضافة
        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList Games = new ArrayList();
            Games = _Games.GetAllGames();
            if (Games.Count == 0)
            {
                new frmDialog("فضلا قم باضافة تدريبات اولا من شاشة التدريبات").ShowDialog();
                //MessageBox.Show("فضلا قم باضافة تدريبات اولا من شاشة التدريبات");
                return;
            }
            else
            {
                if (Application.OpenForms.OfType<frmAddSub>().Any())
                {
                    return;
                }
                else
                {
                    new frmAddSub(this).Show();
                }
            }
            
        }
        //تعديل
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                if (Application.OpenForms.OfType<frmAddSub>().Any())
                {
                    return;
                }
                else
                {
                    new frmAddSub(this, dataGridView1[0, Row].Value.ToString(),
                        dataGridView1[1, Row].Value.ToString(),
                        dataGridView1[2, Row].Value.ToString(),
                        dataGridView1[3, Row].Value.ToString(),
                        dataGridView1[4, Row].Value.ToString(),
                        dataGridView1[5, Row].Value.ToString(),
                        dataGridView1[6, Row].Value.ToString(),
                        dataGridView1[7, Row].Value.ToString(),
                        dataGridView1[8, Row].Value.ToString(),
                        dataGridView1[9, Row].Value.ToString(),
                        dataGridView1[10, Row].Value.ToString()).Show();
                }
            }
            else
            {
                new frmDialog("فضلا قم باضافة مشتركين اولا").ShowDialog();
                //MessageBox.Show("فضلا قم باضافة مشتركين اولا");
                this.Focus();
            }
        }
        //تجديد اشتراك
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                if (Application.OpenForms.OfType<frmAddSub>().Any())
                {
                    return;
                }
                else
                {
                    new frmAddSub(this, dataGridView1[0, Row].Value.ToString(),
                        dataGridView1[1, Row].Value.ToString(),
                        dataGridView1[2, Row].Value.ToString(),
                        dataGridView1[3, Row].Value.ToString(),
                        dataGridView1[4, Row].Value.ToString(),
                        dataGridView1[5, Row].Value.ToString(),
                        dataGridView1[6, Row].Value.ToString(),
                        dataGridView1[7, Row].Value.ToString(),
                        dataGridView1[8, Row].Value.ToString(),
                        dataGridView1[9, Row].Value.ToString(),
                        dataGridView1[10, Row].Value.ToString(), "YY").Show();
                }
            }
            else
            {
                new frmDialog("فضلا قم باضافة مشتركين اولا").ShowDialog();
                //MessageBox.Show("فضلا قم باضافة مشتركين اولا");
                this.Focus();
            }
        }


        //بحث
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                SearchSubs(textBox1.Text, "");
            }
            if (comboBox1.SelectedIndex == 2)
            {
                SearchSubs("", textBox1.Text);
            }
        }

        //الباركود
        private void button6_Click(object sender, EventArgs e)
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






        public void SearchSubs(string SubName, string GameName)
        {
            DataTable dtable = new DataTable();
            if (SubName.Length > 0)
            {
                dtable = DBLayer.RefreshTable(@"SELECT
                            Subs.SubID,
                            Subs.SubName as 'اسم المتدرب',
                            Subs.SubTele as 'المحمول',
                            Subs.SubTele2,
                            Subs.SubCardNum,
                            Subs.SubAddress,
                            Games.GameName as 'اسم التدريب',
                            Reservations.NumOfSessions as'الجلسات المتبقية',
                            Reservations.StartDate as 'بداية الاشتراك',
                            Reservations.EndDate as 'نهاية الاشتراك',
                            Reservations.ResID
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID 
                            WHERE Subs.SubName LIKE '%" + SubName + "%'");
                dataGridView1.DataSource = dtable;
            }
            if (GameName.Length > 0)
            {
                dtable = DBLayer.RefreshTable(@"SELECT
                            Subs.SubID,
                            Subs.SubName as 'اسم المتدرب',
                            Subs.SubTele as 'المحمول',
                            Subs.SubTele2,
                            Subs.SubCardNum,
                            Subs.SubAddress,
                            Games.GameName as 'اسم التدريب',
                            Reservations.NumOfSessions as'الجلسات المتبقية',
                            Reservations.StartDateas as 'بداية الاشتراك',
                            Reservations.EndDate as'نهاية الاشتراك',
                            Reservations.ResID
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID 
                            WHERE Games.GameName LIKE '%" + GameName + "%'");
                dataGridView1.DataSource = dtable;
            }
            if (GameName.Length == 0 && SubName.Length == 0)
            {
                ShowSubs();
            }

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
                            Reservations.StartDate as 'بداية الاشتراك',
                            Reservations.EndDate as'نهاية الاشتراك',
                            Reservations.ResID
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID");
            dataGridView1.DataSource = dtable;
        }

        public void RefreshAfterAdd()
        {
            try
            {
                ShowSubs();
                this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.Rows.Count - 1];
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
            }
            catch { }
        }
        public void RefreshAfterEdit()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                ShowSubs();
                try
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1[1, Row];
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                }
                catch { }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Text = "";
                textBox1.Enabled = false;
                button4_Click(sender, e);
            }
            else
            {
                textBox1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                button4_Click(sender, e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int Row = dataGridView1.CurrentRow.Index;
                    frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك بحذف هذا المتدرب؟", true);
                    dialog.ShowDialog();
                    if (frmDialog.State)
                    {
                        _Subs.DeleteSub(row.Cells[0].Value.ToString());
                        new frmDialog("تم مسح المتدرب").ShowDialog();
                        //MessageBox.Show("تم مسح المتدرب");
                        ShowSubs();
                        dataGridView1.Focus();
                        try
                        {
                            this.dataGridView1.CurrentCell = this.dataGridView1[1, Row - 1];
                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                        }
                        catch { }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                new frmDialog("لا يوجد مشتركين لمسحهم").ShowDialog();
                //MessageBox.Show("!لايوجد مشتركين لمسحهم");
                this.Focus();
            }
            
        }

        private void frmSubData_Shown(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void frmSubData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(sender, e);
            }
            if (e.KeyCode == Keys.F3)
            {
                button5_Click(sender, e);
            }
            if (e.KeyCode == Keys.Delete)
            {
                dataGridView1.Focus();
                button3_Click(sender, e);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
            }
        }

        //تعديل ضغطة الزرار اليمين
        private void MenuClicked(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                button2_Click(sender, e);
            }
        }
        // حذف ضغطة زرار يمين
        private void MenuClicked2(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                button3_Click(sender, e);
            }
        }
        private void MenuClicked3(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                button5_Click(sender, e);
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                // Can leave these here - doesn't hurt
                dataGridView1.Rows[e.RowIndex].Selected = true;
                dataGridView1.Focus();

                ContextMenu m = new ContextMenu();
                m.RightToLeft = RightToLeft.Yes;
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    MenuItem x = new MenuItem(string.Format("تعديل المشترك", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف المشترك", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    MenuItem z = new MenuItem(string.Format("تجديد اشتراك ", currentMouseOverRow.ToString()));
                    z.Click += MenuClicked3;

                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
                    m.MenuItems.Add(z);
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.RightToLeft = RightToLeft.Yes;
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    MenuItem x = new MenuItem(string.Format("تعديل المشترك", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف المشترك", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    MenuItem z = new MenuItem(string.Format("تجديد اشتراك", currentMouseOverRow.ToString()));
                    z.Click += MenuClicked3;
                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
                    m.MenuItems.Add(z);
                }
                m.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Row = dataGridView1.CurrentRow.Index;
            button5_Click(sender, e);
        }

        



    }

}
