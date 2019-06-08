using GymManager.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace GymManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BackEnd.CurrentInfo.cn = new System.Data.SQLite.SQLiteConnection();
            BackEnd.CurrentInfo.cn.ConnectionString = BackEnd.CurrentInfo.Connection;
            BackEnd.CurrentInfo.cn.Open();
            Application.Run(new frmHome());


        }
    }
}
