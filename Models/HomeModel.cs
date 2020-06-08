using System.Data.SQLite;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace CurrencyWebApp.Models
{
    public class HomeModel
    {

        public string startDate { get; set; }
        public string endDate { get; set; }
        public double input { get; set; }
        public string currency { get; set; }

        public double Convert(double input, string currency) {
            double[] conversionRates = new double[] { 1.82, 1.12, 1.27, 0.55, 0.62, 0.698, 1.62, 0.89, 1.13, 1.43, 0.882, 0.787 };
            //use a switch case statement to selec the correct conversion rate
            switch (currency) {
                case "GBP to AUD":
                    input = input * conversionRates[0];
                    break;
                case "GBP to EUR":
                    input = input * conversionRates[1];
                    break;
                case "GBP to USD":
                    input = input * conversionRates[2];
                    break;
                case "AUD to EUR":
                    input = input * conversionRates[3];
                    break;
                case "AUD to GBP":
                    input = input * conversionRates[4];
                    break;
                case "AUD to USD":
                    input = input * conversionRates[5];
                    break;
                case "EUR to AUD":
                    input = input * conversionRates[6];
                    break;
                case "EUR to GBP":
                    input = input * conversionRates[7];
                    break;
                case "EUR to USD":
                    input = input * conversionRates[8];
                    break;
                case "USD to AUD":
                    input = input * conversionRates[9];
                    break;
                case "USD to EUR":
                    input = input * conversionRates[10];
                    break;
                case "USD to GBP":
                    input = input * conversionRates[11];
                    break;
            }
            //return the new value and round to 2 decimal places
            return Math.Round(input, 2);
        }

        public SQLiteConnection CreateConnection()
        {
            SQLiteConnection conn;
            //create a new database connection
            conn = new SQLiteConnection("Data Source = C:\\Users\\User\\source\\repos\\CurrencyWebApp\\CurrencyWebApp\\CurrencyDB.db; Version=3;");
            //open the connection
            try
            {
                conn.Open();
            }
            catch(Exception e){ 
                Console.WriteLine(e.StackTrace);
            }
            return conn;
        }

        public void InsertData(SQLiteConnection conn, String currency, double input)
        {
            SQLiteCommand SQLcmd = conn.CreateCommand();
            String time = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            SQLcmd.CommandText = "INSERT INTO Currency(Amount, Conversion, Time) VALUES('" + input + "','" + currency + "','" + time +"');";
            SQLcmd.ExecuteNonQuery();

        }

        public DataTable ReadData(SQLiteConnection conn, string startDate, string endDate)
        {
            DataTable dt = new DataTable();
            SQLiteDataReader SQLread;
            SQLiteCommand SQLcmd = conn.CreateCommand();
            SQLcmd.CommandText = "SELECT * FROM Currency WHERE Time BETWEEN '" + startDate + "' and '" + endDate + "';";
            SQLread = SQLcmd.ExecuteReader();
            dt.Load(SQLread);
            Console.WriteLine(dt);
            return dt;
        }


    }
}