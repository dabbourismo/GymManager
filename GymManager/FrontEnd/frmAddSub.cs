using GymManager.FrontEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GymManager
{
    public partial class frmAddSub : Form
    {
        BackEnd.CLASS_Subs _Subs = new BackEnd.CLASS_Subs();
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();
        BackEnd.CLASS_Reservations _Res = new BackEnd.CLASS_Reservations();
        frmSubData frmSubData;
        string IDSubEdit = "";
        string IDResEdit = "";
        string XYEdit = "";
        public frmAddSub()
        {
            InitializeComponent();
        }
        public frmAddSub(frmSubData owner)
        {
            InitializeComponent();
           
            frmSubData = owner;
            comboBox1.DataSource = _Games.GetAllGames();
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            textBox2.TextChanged += new EventHandler(textBoxes2_TextChanged);
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            this.Text = "اضافة مشترك";
            button1.Text = "اضافة";
        }
        public frmAddSub(frmSubData owner, string SubID, string SubName, string SubTele, string SubTele2, string SubCardNum, string SubAddress,string GameName,string NumOfSessions,string StartDate,string EndDate,string ResID)
        {
            InitializeComponent();
            frmSubData = owner;
            comboBox1.DataSource = _Games.GetAllGames();

            IDSubEdit = SubID;
            IDResEdit = ResID;
            textBox1.Text = SubName;
            textBox2.Text = SubTele;
            textBox3.Text = SubTele2;
            textBox4.Text = SubCardNum;
            textBox5.Text = SubAddress;

            comboBox1.Text = GameName;
            numericUpDown1.Value = decimal.Parse(NumOfSessions);
            dateTimePicker1.Value = Convert.ToDateTime(StartDate);
            dateTimePicker2.Value = Convert.ToDateTime(EndDate);
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            textBox2.TextChanged += new EventHandler(textBoxes2_TextChanged);
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;

            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            numericUpDown1.Enabled = false;

            this.Text = "تعديل مشترك";
            button1.Text = "تعديل";
        }
        //تجديد اشتراك
        //xy دي عشان تميز تجديد الاشتراك فقط
        public frmAddSub(frmSubData owner, string SubID, string SubName, string SubTele, string SubTele2, string SubCardNum, string SubAddress, string GameName, string NumOfSessions, string StartDate, string EndDate, string ResID,string XY)
        {
            InitializeComponent();
            frmSubData = owner;

            IDSubEdit = SubID;
            IDResEdit = ResID;
            XYEdit = XY;

            label10.Text = SubName;
            label11.Text = SubTele;
            label12.Text = SubTele2;
            label13.Text = SubCardNum;
            label14.Text = SubAddress;
            label15.Text = GameName;
            textBox2.TextChanged += new EventHandler(textBoxes2_TextChanged);


            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            comboBox1.Visible = false;
            label16.Visible = false;

            numericUpDown1.Value = decimal.Parse(NumOfSessions);
            dateTimePicker1.Value = Convert.ToDateTime(StartDate);
            dateTimePicker2.Value = Convert.ToDateTime(EndDate);

            this.Text = "تجديد اشتراك";
            button1.Text = "تجديد";
        }
        //اضافة
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(textBox1.Text) && (button1.Text =="اضافة"|| button1.Text=="تعديل"))
            {
                new frmDialog("فضلا ادخل اسم المشترك على الاقل").ShowDialog();
                //MessageBox.Show("فضلا ادخل اسم المشترك على الاقل");
                return;
            }
            if (Convert.ToInt32( numericUpDown1.Value) == 0)
            {
                new frmDialog("فضلا ادخل عدد الجلسات على الاقل").ShowDialog();
                //MessageBox.Show("فضلا ادخل عدد الجلسات على الاقل");
                return;
            }

            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            if (XYEdit.Length >0)
            {
                if (dt1.Date > dt2.Date)
                {
                    new frmDialog("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز").ShowDialog();
                    //MessageBox.Show("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز");
                    return;
                }
                else
                {
                    //تعديل
                    _Subs.SubsEdit(IDSubEdit, label10.Text, label11.Text, label12.Text, label13.Text, label14.Text);
                    _Res.EditReserv(IDResEdit, label15.Text, Convert.ToInt32(numericUpDown1.Value.ToString()), dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    frmSubData.Focus();
                    frmSubData.RefreshAfterEdit();
                    this.Close();
                    return;
                }
            }
        


            if ((IDSubEdit.Length > 0||IDResEdit.Length >0)&&XYEdit.Length==0)
            {
                if (dt1.Date > dt2.Date)
                {
                    new frmDialog("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز").ShowDialog();
                    //MessageBox.Show("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز");
                    return;
                }
                else
                {
                    //تعديل
                    _Subs.SubsEdit(IDSubEdit, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                    _Res.EditReserv(IDResEdit, comboBox1.Text, Convert.ToInt32(numericUpDown1.Value.ToString()), dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    frmSubData.Focus();
                    frmSubData.RefreshAfterEdit();
                    this.Close();
                }
                

            }
            else
            {
                if (dt1.Date > dt2.Date)
                {
                    new frmDialog("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز").ShowDialog();
                    //MessageBox.Show("تاريخ بداية الحجز اكبر من تاريخ نهاية الحجز");
                    return;
                }
                else
                {
                    //ضيف
                    _Subs.SubsAdd(textBox1.Text, Convert.ToString(textBox2.Text), Convert.ToString(textBox3.Text), textBox4.Text, Convert.ToString(textBox5.Text));
                    _Res.AddReserv(comboBox1.Text, textBox1.Text, Convert.ToInt32(numericUpDown1.Value.ToString()), dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    frmSubData.Focus();
                    frmSubData.RefreshAfterAdd();
                    //frmSubData.ShowSubs();
                    this.Close();
                }
                
                
            }

        }












        private void NumberValidation(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.')
            {
                e.Handled = true;
            }
            else if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 46 || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }



        private void textBoxes2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength == 11 && textBox2.Text.Substring(0, 2).Equals("01"))
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }
            if (textBox2.TextLength < 11)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }            
            if (textBox2.Text == string.Empty)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberValidation(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberValidation(sender, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumberValidation(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddSub_Shown(object sender, EventArgs e)
        {
            if (button1.Text=="تجديد")
            {
                numericUpDown1.Focus();
            }
            textBox1.Focus();

        }

        private void frmAddSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Focused || textBox2.Focused || textBox3.Focused || textBox4.Focused || textBox5.Focused || dateTimePicker1.Focused || dateTimePicker2.Focused || comboBox1.Focused || numericUpDown1.Focused)
                {
                    button1_Click(sender, e);
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            
        }
        
       
        

        
    }
}
