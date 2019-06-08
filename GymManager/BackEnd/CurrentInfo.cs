using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace GymManager.BackEnd
{
    static class CurrentInfo
    {
        public static SQLiteConnection cn;
        public static string Connection = @"Data Source=GymManager.db;MultipleActiveResultSets=true;";
        public static string UserName = "";
        public static string Permissions = "";
    }
}
