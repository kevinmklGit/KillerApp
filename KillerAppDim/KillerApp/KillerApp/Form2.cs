using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KillerApp
{
    public partial class Form2 : Form
    {
        public int dbll;
        LedenInfo infos = new LedenInfo();
        LoginJoin check = new LoginJoin();
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\KillerAppDim\KillerApp\KillerApp\MijnDatabase.mdf;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);
        public Form2()
        {
            InitializeComponent();

        }
        //Vriendgroep aanmaken
        private void button1_Click(object sender, EventArgs e)
        {
            if (leegchecker2(tbnaam.Text, tbwachtwoord.Text) == false)
            {
                MessageBox.Show("Vergeet niet alles in te vullen");
            }
            else
            {

                bool c = check.creategroepcheck(tbnaam.Text);
                if (c == true)
                {
                    check.vriendgroepaanmaken(tbnaam.Text, tbwachtwoord.Text);
                    MessageBox.Show("Vriendengroep aangemaakt");
                    if(check.membermaken(dbll, tbnaam.Text) == true)
                    {
                        MessageBox.Show("je zit in " + tbnaam.Text);
                        ClearTextBoxes();
                        button2_Click(sender, e);
                        button8_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("het is niet gelukt om je in deze groep te plaatsen");
                    }
                }
                else
                {
                    MessageBox.Show("Naam bestaat al");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // joinen van een vriendengroep
            bool b = check.groepjoincheck(dbll, textBox1.Text);
            if (b == true)
            {
                if (check.joincheck(textBox1.Text, textBox2.Text) == true)
                {
                    bool a = check.membermaken(dbll, textBox1.Text);
                    if (a == true)
                    {
                        MessageBox.Show("Je zit nu in: " + textBox1.Text);
                        listBox4_SelectedIndexChanged(sender, e);
                        ClearTextBoxes();
                    }

                }
                else
                {
                    MessageBox.Show("Verkeerd wachtwoord of naam");
                }

            }
            else
            {
                MessageBox.Show("je zit al in deze groep");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//laden van mijn vriendengroepen
            listBox1.Items.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Vriendengroep_naam FROM members WHERE Gebruiker_id = '" + dbll + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Vriendengroep_naam"].ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //vriendengroepen tonen
            listBox4.Items.Clear();
            infos.showgroepen();
            listBox4.Items.AddRange(infos.groepen.ToArray());

        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            check.gegevenssave(dbll);
            //leden tonen van een geselecteerde vriendengroep
            textBox1.Text = Convert.ToString(listBox4.SelectedItem);
            listBox5.Items.Clear();
            infos.listleden(Convert.ToString(listBox4.SelectedItem));
            foreach (string element in infos.leden)
            {
                if (element.Contains(check.Naam))
                {
                    MessageBox.Show("hier zit jij ook in!");
                    listBox5.Items.Add(element);
                }
                else
                {
                    listBox5.Items.Add(element);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
        //einde uitloggen
        //info voor vriendengroep leden bij mijn vriendengroepen
        private void button14_Click(object sender, EventArgs e)
        {
            infos.groepsleden(Convert.ToString(listBox1.SelectedItem));
            MessageBox.Show(infos.groepsleden(Convert.ToString(listBox1.SelectedItem)).ToString(),"leden");
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            //startup
            //laden van mijn vriendengroepen
            listBox1.Items.Clear();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Vriendengroep_naam FROM members WHERE Gebruiker_id = '" + dbll + "'", conn);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Vriendengroep_naam"].ToString());
            }
            //verschillende vriendengroepen tonen
            listBox4.Items.Clear();
            infos.showgroepen();
            listBox4.Items.AddRange(infos.groepen.ToArray());
            //ingeschreven evenemten tonen
            infos.eveingeschreven(dbll);
            listBox3.Items.AddRange(infos.mijnevenementen.ToArray());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //toevoegen event
            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vergeet niet de vriendengroep te selecteren.", "Oeps");
            }
            else
            {
                bool checker = leegchecker(textBox3.Text,textBox4.Text,textBox5.Text,textBox6.Text, textBox7.Text, textBox8.Text);
                if (checker == false)
                {
                    MessageBox.Show("Vergeet niet alles in te vullen");
                }
                else
                {
                    bool eventmaker = check.eventmaken(textBox3.Text, textBox4.Text, textBox5.Text,textBox6.Text, textBox7.Text, textBox8.Text, Convert.ToString(listBox1.SelectedItem));
                    if (eventmaker == true)
                    {
                        MessageBox.Show("event aangemaakt voor " + Convert.ToString(listBox1.SelectedItem));
                        ClearTextBoxes();
                        listBox1_SelectedIndexChanged(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Niet gelukt om event te maken");
                    }
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // laat evenementen zien van een vriendengroep
            listBox2.Items.Clear();
            infos.listgroepeve(Convert.ToString(listBox1.SelectedItem));
            listBox2.Items.AddRange(infos.groepeve.ToArray());
            // laat aantal leden zien van vriendengroep
            infos.aantalrefresh(Convert.ToString(listBox1.SelectedItem));
            //gebruikmaken van tostring methode in LedenInfo klasse
            label22.Text = infos.ToString();

        }
        //info over evenement laten zien in messagebox
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(infos.infoeven(Convert.ToString(listBox2.SelectedItem)).ToString(), "Info Evenement");
        }

        //evenement joinen
        private void button3_Click(object sender, EventArgs e)
        {
            check.showid(Convert.ToString(listBox2.SelectedItem));
            bool g = check.joincheckeve(dbll, check.ide);
            if(g == false)
            {
                MessageBox.Show("Je zit al in dit evenement");
            }
            else
            {
                check.inschrijven(dbll, check.ide);
                MessageBox.Show("Inschrijving voltooid voor: " + (Convert.ToString(listBox2.SelectedItem)));
                listBox3.Items.Clear();
                Form2_Shown(sender, e);
            }

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            // zoekoptie
            listBox4.Items.Clear();
            infos.showgroepenzoek(textBox9.Text);
            listBox4.Items.AddRange(infos.groepen.ToArray());
        }
        //info joinde evenementen
        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show(infos.infoeven(Convert.ToString(listBox3.SelectedItem)).ToString(), "Info Evenement");
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //leden van ingeschreven evenementen zien
            listBox6.Items.Clear();
            infos.jantje(Convert.ToString(listBox3.SelectedItem));
            listBox6.Items.AddRange(infos.ledenevenementen.ToArray());

        }
        private void button17_Click(object sender, EventArgs e)
        {
            //gebruikmaken van tostring methode in klasse loginjoin
            check.gegevenssave(dbll);
            MessageBox.Show(check.ToString(), "profiel");

        }
        private bool leegchecker(string naam, string woonplaats, string mail, string wachtwoord, string opmerking, string datum)
        {
            if (string.IsNullOrWhiteSpace(naam))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(woonplaats))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(mail))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(wachtwoord))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(opmerking))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(datum))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool leegchecker2(string naam, string woonplaats)
        {
            if (string.IsNullOrWhiteSpace(naam))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(woonplaats))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
        }
        //review scherm openen
        private void button16_Click(object sender, EventArgs e)
        {
            Review review = new Review();
            review.loginid = dbll;
            review.Show();
        }
        // Subquery
        private void button15_Click_1(object sender, EventArgs e)
        {
            //string tilburg = "Tilburg";
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter("SELECT Vriendengroep_naam FROM planning WHERE evenement_id IN (SELECT id FROM evenement WHERE locatie = '" + tilburg + "')", conn);
            //da.Fill(dt);
            //foreach (DataRow dr in dt.Rows)
            //{
            //    listBox7.Items.Add(dr["Vriendengroep_naam"].ToString());
            //}
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kijken of het evenement gratis is
            infos.infoeven(Convert.ToString(listBox2.SelectedItem));
            if (infos.evenementinfo.Contains("Gratis"))
            {
                int index = infos.evenementinfo.IndexOf("Gratis");
                if (index == 4)
                {
                    MessageBox.Show("Dit is een gratis evenement");
                }
                else
                {
                    
                }
            }
            else
            {
                
            }
            
        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }
    }
}
