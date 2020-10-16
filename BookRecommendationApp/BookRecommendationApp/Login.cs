using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRecommendationApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            MainMenu mMenu = new MainMenu();
            mMenu.Show();
            mMenu.FormClosing += (obj, arg) => { this.Visible = true; };
            this.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
