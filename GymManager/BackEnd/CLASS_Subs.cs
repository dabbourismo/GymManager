using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Collections;

namespace GymManager.BackEnd
{
    class CLASS_Subs
    {
        SQLiteCommand cm = new SQLiteCommand();
        SQLiteDataReader reader;
        public CLASS_Subs()
        {
            cm.Connection = CurrentInfo.cn;
        }
        public bool SubsAdd(string SubName, string SubTele, string SubTele2, string SubCardNum, string SubAddress)
        {
            cm.CommandText = @"INSERT INTO Subs (SubName,SubTele,SubTele2,SubCardNum,SubAddress) VALUES ('" + SubName + "','" + SubTele + "','" + SubTele2 + "','" + SubCardNum + "','" + SubAddress + "')";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool SubsEdit(string IDEdit,string SubName, string SubTele, string SubTele2, string SubCardNum, string SubAddress)
        {
            cm.CommandText = @"UPDATE Subs SET SubName='" + SubName + "',SubTele='" + SubTele + "',SubTele2='" + SubTele2 + "',SubCardNum='" + SubCardNum + "',SubAddress='" + SubAddress + "' WHERE SubID='"+IDEdit+"'";
            try
            {
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int SubGetIDByName(string SubName)
        {
            cm.CommandText = @"SELECT SubID FROM Subs WHERE SubName='" + SubName + "'";
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
        public ArrayList SubGetData(int SubID)
        {
            ArrayList SubArray = new ArrayList();
            cm.CommandText = @"SELECT
                            Subs.SubID,
                            Subs.SubName,
                            Subs.SubTele,
                            Subs.SubTele2,
                            Subs.SubCardNum,
                            Subs.SubAddress,
                            Games.GameName,
                            Reservations.NumOfSessions,
                            Reservations.StartDate,
                            Reservations.EndDate
                            FROM
                            Games
                            INNER JOIN Reservations ON Reservations.GameID = Games.GameID
                            INNER JOIN Subs ON Reservations.SubID = Subs.SubID WHERE Subs.SubID = '"+SubID+"'";
            reader = cm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SubArray.Add(reader["SubName"]); //0
                    SubArray.Add(reader["SubTele"]); //1
                    SubArray.Add(reader["SubTele2"]); //2
                    SubArray.Add(reader["SubCardNum"]); //3
                    SubArray.Add(reader["SubAddress"]); //4
                    SubArray.Add(reader["GameName"]); //5
                    SubArray.Add(reader["NumOfSessions"]); //6
                    SubArray.Add(reader["StartDate"]);//7
                    SubArray.Add(reader["EndDate"]);//8
                    SubArray.Add(reader["SubID"]);//9
                }
            }
            reader.Close();
            return SubArray;
        }
        public bool Reduce_IncreaseNumOfSessions(string SubID)
        {
            cm.CommandText = @"SELECT NumOfSessions FROM Reservations WHERE SubID='"+SubID+"'";
            try
            {
                int number = Convert.ToInt32(cm.ExecuteScalar().ToString());
                number = number - 1;
                
                cm.CommandText = @"UPDATE Reservations SET NumOfSessions='" + number + "' WHERE SubID='"+SubID+"'";
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int GetRemainingNumOfSessions(string SubID)
        {
            cm.CommandText = @"SELECT NumOfSessions FROM Reservations WHERE SubID='" + SubID + "'";
            return Convert.ToInt32(cm.ExecuteScalar().ToString());
        }
        public ArrayList GetAllSubID()
        {
            ArrayList SubIDArray = new ArrayList();
            cm.CommandText = @"SELECT SubID FROM Reservations";
            reader = cm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SubIDArray.Add(reader["SubID"]);
                }
            }
            reader.Close();
            return SubIDArray;
        }
        public void SetSessionsToZero(string SubID)
        {

        }

        public void DeleteSub(string SubID)
        {
            cm.CommandText = @"DELETE FROM Subs WHERE SubID='"+SubID+"'";
            cm.ExecuteNonQuery();
            cm.CommandText = @"DELETE FROM Reservations WHERE SubID='"+SubID+"'";
            cm.ExecuteNonQuery();
        }

        public ArrayList GetAllSubName()
        {
            ArrayList SubNameArray = new ArrayList();
            cm.CommandText = @"SELECT SubName From Subs";
            reader = cm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SubNameArray.Add(reader["SubName"]);
                }
            }
            reader.Close();
            return SubNameArray;
        }
    }
}
