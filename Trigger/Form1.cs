using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trigger
{
    public partial class Form1 : Form
    {
        Reader reader = new Reader();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            reader.StartConnection();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string fileName = @"C:\NVS\notallowed.txt";
            if (File.Exists(fileName))
            {
                panel1.Visible = true;
                timer1.Enabled = false;
                timer2.Enabled=true;

                string text = System.IO.File.ReadAllText(fileName);

                label1.Text = text.Trim() + " is not allowed to go out";

                System.Media.SoundPlayer player = new System.Media.SoundPlayer();
                player.SoundLocation = @"c:\NVS\invalid.wav";
                player.Load();
                player.Play();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string fileName = @"C:\NVS\notallowed.txt";
            timer1.Enabled = true;
            timer2.Enabled = false;
            panel1.Visible = false;
          File.Delete(fileName);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }
    }
}
