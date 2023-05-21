using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KillerApp
{
    public partial class Review : Form
    {
        public int loginid;
        Form2 form2 = new Form2();
        LoginJoin maken = new LoginJoin();
        public Review()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Vul alstublieft een cijfer in.");
            }
            else
            {
                maken.reviewmaken(textBox2.Text, textBox3.Text, Convert.ToDouble(textBox1.Text), loginid);
                MessageBox.Show("Bedankt voor uw review!");
                this.Hide();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
