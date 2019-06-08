using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;


namespace GymManager.BackEnd
{
    class DBLayer
    {
        public static DataTable RefreshTable(string command)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command, CurrentInfo.cn);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
