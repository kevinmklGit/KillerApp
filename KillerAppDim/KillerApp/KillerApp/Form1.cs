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
    public partial class Form1 : Form
    {
        LoginJoin inloggen = new LoginJoin();
        // gebruikersnaam
        public string naam;
        public override string ToString()
        {
            return naam + "!";
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void btnregister_Click(object sender, EventArgs e)
        {
            string telefoonnummer = tbnummer.Text + tbnummer1.Text;
            bool a = leegchecker(tbnaamg.Text, tbwoonplaats.Text, tbmail.Text, tbwachtwoord.Text,telefoonnummer,tbleeftijdg.Text);
            if (a == false)
            {
                MessageBox.Show("Zorg dat je alles invult");
            }
            else
            {
                Registreren_maken registratie = new Registreren_maken();
                bool c = inloggen.registreren(tbnaamg.Text, tbwoonplaats.Text, tbleeftijdg.Text, telefoonnummer, tbmail.Text, tbwachtwoord.Text);
                if (c == true)
                {
                    MessageBox.Show("Gebruiker aangemaakt");
                }
                else
                {
                    MessageBox.Show("Er is iets misgegaan");
                }
            }
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            // inloggen van de gebruiker
            LoginJoin inloggen = new LoginJoin();
            bool b = inloggen.login(tbloginn.Text, tbwachtwoordl.Text);
            if(b == true)
            {
                Form2 app = new Form2();
                app.dbll = inloggen.idl;       
                base.OnVisibleChanged(e);
                this.Visible = false;
                app.Show();              
            }
            else
            {
                MessageBox.Show("verkeerde naam of wachtwoord");
            }
        }
        private bool leegchecker(string naam,string woonplaats, string mail, string wachtwoord,string telefoonnummer,string leeftijd)
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
            else if (string.IsNullOrWhiteSpace(leeftijd))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(telefoonnummer))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(tabPage2);
        }
    }
}
