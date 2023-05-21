using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KillerApp
{
    class LoginJoin
    {
        public int idl;
        public int ide;
        //gegevens van gebruiker
        public string Naam;
        public int Leeftijd;
        public string Woonplaats;
        public string email;
        //gegevens van een gebruiker ToString methode
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ID: {0} ", idl);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Naam: {0}",Naam);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Leeftijd: {0}",Leeftijd);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Woonplaats: {0}",Woonplaats);
            sb.Append(Environment.NewLine);
            sb.AppendFormat("Email: {0}",email);
            return sb.ToString();
        }
        //connectionstring
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\KillerAppDim\KillerApp\KillerApp\MijnDatabase.mdf;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        //gegevens van gebruiker opslaan
        public void gegevenssave(int id)
        {
            string query = "SELECT id,naam,leeftijd,woonplaats,[e-mail] FROM Gebruiker WHERE id = @id";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idl = dr.GetInt32(0);
                Naam = dr.GetString(1);
                Leeftijd = dr.GetInt32(2);
                Woonplaats = dr.GetString(3);
                email = dr.GetString(4);

            }
            conn.Close();
        }

        public bool login(string naam,string wachtwoord)
        {
            //login van een gebruiker
            string query = "SELECT Id,Naam,wachtwoord FROM Gebruiker WHERE Naam = @Naam AND wachtwoord = @wachtwoord";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Naam", naam);
            cmd.Parameters.AddWithValue("@Wachtwoord", wachtwoord);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idl = dr.GetInt32(0);
            }
            if (dr.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }

        }
        // joinen van de gebruiker
        public bool joincheck(string naam, string wachtwoord)
        {
            //login van een gebruiker
            string query = "SELECT Naam,wachtwoord FROM Vriendengroep WHERE Naam = @naam AND wachtwoord = @wachtwoord";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        //inserten in de members koppeltabel(joinen van een groep)
        public bool membermaken(int id, string naam)
        {
            string query = "INSERT into members (Gebruiker_id,Vriendengroep_naam) " + " VALUES (@Gebruiker_id,@Vriendengroep_naam);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Gebruiker_id", id);
            cmd.Parameters.AddWithValue("@Vriendengroep_naam", naam);
            conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())

                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            

        }
        public int Idl   // the Name property
        {
            get
            {
                return idl;
            }
        }
        //gebruiker inschrijven voor een evenement
        public bool inschrijven(int gid, int eid)
        {
            string query = "INSERT into inschrijving (Gebruiker_id,Evenement_id) VALUES (@gebruiker_id,@evenement_id);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@gebruiker_id", gid);
            cmd.Parameters.AddWithValue("@evenement_id", eid);
            conn.Open();
            try
            {
                using (SqlDataReader reader = cmd.ExecuteReader())

                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            }
            catch (Exception exe)
            {
                string exception = Convert.ToString(exe);
                conn.Close();
                return false;
            }
        }
        //show evenement id
        public bool showid(string evenement)
        {
            string query = "SELECT Id FROM Evenement WHERE naam = @naam";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", evenement);
            conn.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ide = reader.GetInt32(0);
                    conn.Close();
                    return true;                
                }
            }
            conn.Close();
            return false;

        }
        //check of gebruiker niet al in het evenement zit
        public bool joincheckeve(int idg,int ide)
        {
            //login van een gebruiker
            string query = "SELECT gebruiker_id,evenement_id FROM inschrijving WHERE gebruiker_id = @gebruiker_id AND evenement_id = @evenement_id";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@gebruiker_id", idg);
            cmd.Parameters.AddWithValue("@evenement_id", ide);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                conn.Close();
                return true;
            }
        }
        //check of vriendengroepnaam al bestaat
        public bool creategroepcheck(string naam)
        {
            //login van een gebruiker
            string query = "SELECT naam FROM vriendengroep WHERE naam = @naam";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                conn.Close();
                return true;
            }
        }
        public bool groepjoincheck(int id,string naam)
        {
            //checken of de gebruiker niet al in de groep zit
            string query = "SELECT gebruiker_id,vriendengroep_naam FROM members WHERE gebruiker_id = @gebruiker_id AND vriendengroep_naam = @vriendengroep_naam";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@gebruiker_id", id);
            cmd.Parameters.AddWithValue("@vriendengroep_naam", naam);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                conn.Close();
                return false;
            }
            else
            {
                conn.Close();
                return true;
            }
        }
        public bool registreren(string naam, string woonplaats, string leeftijd, string telefoonnummer, string mail, string wachtwoord)
        {
            //gebruiker aanmaken
            string query = "INSERT into Gebruiker (naam,woonplaats,leeftijd,telefoonnummer,[E-Mail],wachtwoord) " + " VALUES (@naam,@woonplaats,@leeftijd,@telefoonnummer,@mail,@wachtwoord);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@woonplaats", woonplaats);
            cmd.Parameters.AddWithValue("@leeftijd", leeftijd);
            cmd.Parameters.AddWithValue("@telefoonnummer", telefoonnummer);
            cmd.Parameters.AddWithValue("@mail", mail);
            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public bool vriendgroepaanmaken(string naam, string wachtwoord)
        {
            //vriendengroep aanmaken
            string query = "INSERT into vriendengroep (naam,wachtwoord) " + " VALUES (@naam,@wachtwoord);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@wachtwoord", wachtwoord);
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())

                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            }
            catch (Exception exe)
            {
                string exception = Convert.ToString(exe);
                conn.Close();
                return false;
            }
        }
        //evenement aanmaken voor vriendengroep
        public bool eventmaken(string naam, string datum, string tijd, string prijs, string opmerking, string locatie, string vriendengroepnaam)
        {

            string query = "INSERT into Evenement (naam,datum,tijd,prijs,opmerking,locatie) " + " VALUES (@naam,@datum,@tijd,@prijs,@opmerking,@locatie);";
            string query2 = "INSERT into Planning (vriendengroep_naam) " + " VALUES (@vnaam);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@naam", naam);
            cmd.Parameters.AddWithValue("@datum", datum);
            cmd.Parameters.AddWithValue("@tijd", tijd);
            cmd.Parameters.AddWithValue("@prijs", prijs);
            cmd.Parameters.AddWithValue("@opmerking", opmerking);
            cmd.Parameters.AddWithValue("@locatie", locatie);
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            cmd2.Parameters.AddWithValue("@vnaam", vriendengroepnaam);
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            }
        }
        //review aanmaken
        public bool reviewmaken(string tip, string opmerking, double cijfer,int gebruikerid)
        {
            //gebruiker aanmaken
            string query = "INSERT into Review (tip,opmerking,cijfer,gebruiker_id) " + " VALUES (@tip,@opmerking,@cijfer,@gebruikerid);";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@tip", tip);
            cmd.Parameters.AddWithValue("@opmerking", opmerking);
            cmd.Parameters.AddWithValue("@cijfer", cijfer);
            cmd.Parameters.AddWithValue("@gebruikerid", gebruikerid);
            try
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                    }
                }
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }


    }
}
