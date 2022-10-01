using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MMProgram
{
    public partial class Power : Form
    {
        public int poi, poj;
        public Power()
        {
            Main main = new Main(poi, poj);
            InitializeComponent();
        }

        private void Power_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Power_Load(object sender, EventArgs e)
        {
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            poi = Convert.ToInt32(comboBox1.Text);
            poj = Convert.ToInt32(comboBox2.Text);
            this.Hide();
            Main main = new Main(poi, poj); main.Show();
        }
    }
}
