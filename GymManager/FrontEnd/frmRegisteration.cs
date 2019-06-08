using FoxLearn.License;
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
    public partial class frmRegisteration : Form
    {
        BackEnd.CLASS_Resistration _Reg = new BackEnd.CLASS_Resistration();
        string ComputerInfoString;
        string Decrypted;
        string Encrypted;
        public frmRegisteration()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;

        //تفعيل
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                new frmDialog("فضلا ادخل كود التفعيل").ShowDialog();
                return;
            }
            else
            {
                if (_Reg.GetProductKey() == "0")
                {
                    Encrypted = _Reg.Encrypt(ComputerInfoString, "s3lw-3xr8-sqoy18");
                    if (Encrypted == textBox1.Text)
                    {
                        _Reg.StoreKey(textBox1.Text);
                        new frmDialog("تم التسجيل بنجاح ، قم باعادة تشغيل البرنامج").ShowDialog();
                        Application.Exit();
                    }
                    else
                    {
                        new frmDialog("كود التفعيل غير صحيح").ShowDialog();
                        return;
                    }
                }
                else
                {
                    Encrypted = _Reg.Encrypt(ComputerInfoString, "s3lw-3xr8-sqoy18");
                    if (Encrypted == textBox1.Text)
                    {
                        _Reg.UpdateKey(textBox1.Text);
                        new frmDialog("تم التسجيل بنجاح ، قم باعادة تشغيل البرنامج").ShowDialog();
                        Application.Exit();
                    }
                    else
                    {
                        new frmDialog("كود التفعيل غير صحيح").ShowDialog();
                        return;
                    }
                }
            }
        }

        //الغاء
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmRegisteration_Load(object sender, EventArgs e)
        {
            textBox6.Text = ComputerInfo.GetComputerId();
            ComputerInfoString = textBox6.Text;
        }
        //نسخ
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox6.Text);
        }
    }
}
