using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KillerApp
{
    class Registreren_maken
    {
        //const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\poep\KillerApp\KillerApp\MijnDatabase.mdf;Integrated Security=True";
        //SqlConnection conn = new SqlConnection(connectionString);
        //public bool registreren(string naam, string woonplaats, string leeftijd, string telefoonnummer, string mail, string wachtwoord)
        //{
        //    //gebruiker aanmaken
        //    string query = "INSERT into Gebruiker (naam,woonplaats,leeftijd,telefoonnummer,[E-Mail],wachtwoord) " + " VALUES ('" + naam + "','" + woonplaats + "','" + leeftijd + "','" + telefoonnummer + "','" + mail + "','" + wachtwoord + "');";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    try
        //    {
        //        conn.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {

        //            }
        //        }
        //        conn.Close();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        conn.Close();
        //        return false;
        //    }
        //}
        //public bool vriendgroepaanmaken(string naam, string wachtwoord)
        //{
        //    //vriendengroep aanmaken
        //    string query = "INSERT into vriendengroep (naam,wachtwoord) " + " VALUES ('" + naam + "','" + wachtwoord + "');";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    try
        //    {
        //        conn.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())

        //        {
        //            while (reader.Read())
        //            {

        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception exe)
        //    {
        //        string exception = Convert.ToString(exe);
        //        conn.Close();
        //        return false;
        //    }
        //}
        ////evenement aanmaken voor vriendengroep
        //public bool eventmaken(string naam, string datum, string tijd, decimal prijs, string opmerking, string locatie, string vriendengroepnaam)
        //{

        //    string query = "INSERT into Evenement (naam,datum,tijd,prijs,opmerking,locatie) " + " VALUES ('" + naam + "','" + datum + "','" + tijd + "','" + prijs + "','" + opmerking + "','" + locatie + "');";
        //    string query2 = "INSERT into Planning (vriendengroep_naam) " + " VALUES ('" + vriendengroepnaam + "');";
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    SqlCommand cmd2 = new SqlCommand(query2, conn);
        //    {
        //        conn.Open();
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {

        //            }
        //        }
        //        using (SqlDataReader reader = cmd2.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {

        //            }
        //        }
        //        conn.Close();
        //        return true;
        //    }
        //}
    }
}
