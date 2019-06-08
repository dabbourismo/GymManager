using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Collections;

namespace GymManager.BackEnd
{
    class CLASS_Reservations
    {
        SQLiteCommand cm = new SQLiteCommand();
        BackEnd.CLASS_Games _Games = new BackEnd.CLASS_Games();
        BackEnd.CLASS_Subs _Subs = new BackEnd.CLASS_Subs();
        public CLASS_Reservations()
        {
            cm.Connection = CurrentInfo.cn;
        }
        public bool AddReserv(string GameName, string SubName, int NumOfSessions, string StartDate, string EndDate)
        {
            cm.CommandText = @"INSERT INTO Reservations (GameID,SubID,NumOfSessions,StartDate,EndDate) VALUES ('"+_Games.GameGetIDByName(GameName)+"','"+_Subs.SubGetIDByName(SubName)+"','"+NumOfSessions+"','"+StartDate+"','"+EndDate+"')";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false ;
            }
         
        }
        public bool EditReserv(string ResID, string GameName, int NumOfsessions, string StartDate, string EndDate)
        {
            cm.CommandText = @"UPDATE Reservations SET GameID='" + _Games.GameGetIDByName(GameName) + "',NumOfSessions='" + NumOfsessions + "',StartDate='" + StartDate + "',EndDate='" + EndDate + "' WHERE ResID='"+ResID+"'";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
