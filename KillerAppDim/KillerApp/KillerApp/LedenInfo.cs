using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace KillerApp
{
    class LedenInfo
    {
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\KillerAppDim\KillerApp\KillerApp\MijnDatabase.mdf;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        public List<string> leden = new List<string>();
        public List<string> groepen = new List<string>();
        public List<string> groepeve = new List<string>();
        public List<string> evenementinfo = new List<string>();
        public List<string> mijnevenementen = new List<string>();
        public List<string> ledenevenementen = new List<string>();
        private int nummereve;
        public int aantal;
        public StringBuilder groepsleden(string vriendengroep)
        {
            //leden in geselecteerde vriendengroep tonen
            StringBuilder leden = new StringBuilder();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT naam FROM gebruiker g,members m WHERE g.Id=m.gebruiker_id AND vriendengroep_naam = '" + vriendengroep + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                leden.AppendLine(dr["naam"].ToString());
            }
            return leden;
        }
        //tostring methode voor aantal leden
        public override string ToString()
        {
            return aantal.ToString();
        }
        //aantal leden laten zien in een vriendengroep
        public void aantalrefresh(string naam)
        {
            string query = "SELECT COUNT (gebruiker_id) AS aantal FROM Members WHERE vriendengroep_naam = @naam";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            conn.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    aantal = reader.GetInt32(0);
                }
            }
            conn.Close();
        }
        public void listleden(string vriendengroep)
        {
            //leden van een bepaalde groep in een list
            leden.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT naam FROM gebruiker g,members m WHERE g.Id=m.gebruiker_id AND vriendengroep_naam = '" + vriendengroep + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                leden.Add(dr["naam"].ToString());
            }
        }
        public void showgroepen()
        {
            //alle groepen in een list zetten
            groepen.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT naam FROM Vriendengroep", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                
                groepen.Add(dr["naam"].ToString());
            }
        }
        public void showgroepenzoek(string letters)
        {
            groepen.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT naam FROM Vriendengroep WHERE naam LIKE '%' + '"+letters+"%'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                groepen.Add(dr["naam"].ToString());
            }
        }
        //evenementen van groepen showen
        public void listgroepeve(string vriendengroep)
        {
            groepeve.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT naam FROM evenement e,planning p WHERE p.evenement_id=e.Id AND vriendengroep_naam = '" + vriendengroep + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                groepeve.Add(dr["naam"].ToString());
            }
        }
        public StringBuilder infoeven(string vriendengroep)
        {
            //info over een evenement in een stringbuilder
            evenementinfo.Clear();
            StringBuilder infoe = new StringBuilder();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM evenement WHERE naam = '" + vriendengroep +"'" , conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                evenementinfo.Add(dr["naam"].ToString());
                evenementinfo.Add(dr["datum"].ToString());
                evenementinfo.Add(dr["tijd"].ToString());
                evenementinfo.Add(dr["locatie"].ToString());
                evenementinfo.Add(dr["prijs"].ToString());
                evenementinfo.Add(dr["opmerking"].ToString());

                infoe.AppendLine("Naam: "+ dr["naam"].ToString());
                infoe.AppendLine("Datum: " + dr["datum"].ToString());
                infoe.AppendLine("Tijd: " + dr["tijd"].ToString());
                infoe.AppendLine("Locatie: " + dr["locatie"].ToString());
                infoe.AppendLine("Prijs: " + dr["prijs"].ToString());
                infoe.AppendLine("Opmerking: " + dr["opmerking"].ToString());
            }
            return infoe;
        }
        // show ingeschreven evenementen van een gebruiker
        public void eveingeschreven(int id)
        {
            mijnevenementen.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT naam FROM evenement e,inschrijving i WHERE e.id=i.evenement_id AND i.gebruiker_id = '" + id + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                mijnevenementen.Add(dr["naam"].ToString());
            }
        }
        // show leden van ingeschreven evenementen
        public void jantje(string naam)
        {
            string query = "SELECT DISTINCT id FROM evenement WHERE naam = @naam";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    nummereve = reader.GetInt32(0);

                }
            }
           

            ledenevenementen.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT naam FROM gebruiker g,inschrijving i WHERE g.id=i.gebruiker_id AND i.evenement_id = '" + nummereve + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                ledenevenementen.Add(dr["naam"].ToString());
            }
            conn.Close();
        }
    }
}
