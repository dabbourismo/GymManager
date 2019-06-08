using GymManager.BackEnd;
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
    public partial class frmGamesData : Form
    {
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();

        public frmGamesData()
        {
            InitializeComponent();
            ShowGames();
            this.dataGridView1.Columns[0].Visible = false;
        }
        //اضافة
        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmAddGames>().Any())
            {
                return;
            }
            else
            {
                new frmAddGames(this).Show();
            }
        }
        //تعديل
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                if (Application.OpenForms.OfType<frmAddGames>().Any())
                {
                    return;
                }
                else
                {
                    new frmAddGames(this, dataGridView1[0, Row].Value.ToString(), dataGridView1[1, Row].Value.ToString()).Show();
                }
            }
            else
            {
                new frmDialog("لا يوجد العاب لتعديلها").ShowDialog();
                //MessageBox.Show("لا يوجد العاب لتعديلها");
                this.Focus();
            }
        }
        //مسح
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int Row = dataGridView1.CurrentRow.Index;
                    frmDialog dialog = new frmDialog("هل انت متأكد من رغبتك بحذف التدريب المحدد؟ سيتم حذف جميع المتدربين المشتركين بهذا التدريب", true);
                    dialog.ShowDialog();
                    if (frmDialog.State)
                    {
                        if (_Games.DeleteGame(row.Cells[0].Value.ToString()))
                        {
                            new frmDialog("تم مسح التدريب").ShowDialog();
                            //MessageBox.Show("تم مسح التدريب");
                            dataGridView1.Focus();
                            ShowGames();
                            try
                            {
                                this.dataGridView1.CurrentCell = this.dataGridView1[1, Row - 1];
                                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                            }
                            catch
                            {

                            }
                        }

                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                new frmDialog("لا يوجد تدريبات لمسحها").ShowDialog();
                return;
            }

        }







        public void ShowGames()
        {
            DataTable dtable = new DataTable();
            dtable = DBLayer.RefreshTable(@"SELECT GameID,GameName as'اسم التدريب' FROM Games");
            dataGridView1.DataSource = dtable;
        }
        public void RefreshAfterAdd()
        {
            try
            {
                ShowGames();
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
                ShowGames();
                try
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1[1, Row];
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.SelectedRows[0].Index;
                }
                catch { }
            }
        }

        private void frmGamesData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(sender, e);
            }
            if (e.KeyCode == Keys.Delete)
            {
                dataGridView1.Focus();
                button3_Click(sender, e);
                ShowGames();
            }
        }

        private void frmGamesData_Shown(object sender, EventArgs e)
        {
            this.Focus();
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
                    MenuItem x = new MenuItem(string.Format("تعديل التدريب", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف التدريب", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
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
                    MenuItem x = new MenuItem(string.Format("تعديل التدريب", currentMouseOverRow.ToString()));
                    x.Click += MenuClicked;
                    MenuItem y = new MenuItem(string.Format("حذف التدريب", currentMouseOverRow.ToString()));
                    y.Click += MenuClicked2;
                    m.MenuItems.Add(x);
                    m.MenuItems.Add(y);
                }
                m.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }


    }
}
