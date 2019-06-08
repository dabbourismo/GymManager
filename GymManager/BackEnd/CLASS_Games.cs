using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using System.Collections;
namespace GymManager.BackEnd
{
    class CLASS_Games
    {
        SQLiteCommand cm = new SQLiteCommand();
        SQLiteDataReader reader;
        public CLASS_Games()
        {
            cm.Connection = CurrentInfo.cn;
        }
        public bool InsertGame(string GameName)
        {
            cm.CommandText = @"INSERT INTO Games (GameName) VALUES ('" + GameName + "')";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            { return false; }
        }
        public bool EditGame(string GameID, string GameName)
        {
            cm.CommandText = @"UPDATE Games SET GameName='"+GameName+"' WHERE GameID='"+GameID+"'";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            { return false; }

        }
        public bool DeleteGame(string GameID)
        {
            DataTable dtable = new DataTable();
            dtable = DBLayer.RefreshTable(@"SELECT (SubID) FROM Reservations WHERE GameID = '" + GameID + "'");
            foreach (DataRow row in dtable.Rows)
            {
                cm.CommandText = @"DELETE FROM Subs WHERE SubID = '" + row[0].ToString() + "'";
                cm.ExecuteNonQuery();
            }
            try
            {
                cm.CommandText = @"DELETE FROM Games WHERE GameID='" + GameID + "'";
                cm.ExecuteNonQuery();
                cm.CommandText = @"DELETE FROM Reservations WHERE GameID='" + GameID + "'";
                cm.ExecuteNonQuery();
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
            
        }
        public ArrayList GetAllGames()
        {
            ArrayList Items = new ArrayList();
            cm.CommandText = "Select GameName from Games";
            reader = cm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Items.Add(reader["GameName"].ToString());
                }
            }
            reader.Close();
            return Items;
        }
        public int GameGetIDByName(string GameName)
        {
            cm.CommandText = @"SELECT GameID FROM Games WHERE GameName='"+GameName+"'";
            try
            {
                int ID = Convert.ToInt32(cm.ExecuteScalar().ToString());
                return ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
