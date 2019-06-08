using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using System.Security.Cryptography;
using System.Text;  

namespace GymManager.BackEnd
{
    class CLASS_Resistration
    {
        SQLiteCommand cm = new SQLiteCommand();
        public CLASS_Resistration()
        {
            cm.Connection = CurrentInfo.cn;
        }
        public void StoreKey(string ProductKey)
        {
            string EncryptedKey = Encrypt(ProductKey, "s3lw-3xr8-sqoy18");
            cm.CommandText = @"INSERT INTO Activation (Serial) VALUES ('" + EncryptedKey + "')";
            cm.ExecuteNonQuery();
        }
        public string GetProductKey()
        {
            cm.CommandText = @"SELECT Serial FROM Activation WHERE ID= 1";
            try
            {
                string DecryptedKey = Decrypt(cm.ExecuteScalar().ToString(), "s3lw-3xr8-sqoy18");
                
                return DecryptedKey;
            }
            catch (Exception)
            {
                return "0";
            }
        }
        public void UpdateKey(string ProductKey)
        {

            string EncryptedKey = Encrypt(ProductKey, "s3lw-3xr8-sqoy18");
            cm.CommandText = @"UPDATE Activation SET Serial='" + EncryptedKey + "' WHERE ID = 1";
            cm.ExecuteNonQuery();
        }



        public  string Encrypt(string input, string key)
        {
            //Key Must Be 128 or 192 bit
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public  string Decrypt(string input, string key)
        {
            //Key Must Be 128 or 192 bit
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        } 
    }
}
