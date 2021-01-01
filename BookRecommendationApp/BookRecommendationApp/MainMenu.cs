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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            FormHome frmHome = new FormHome() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmHome.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmHome);
            frmHome.Show();
        }

        // Make sure this is disposed when MainMenu is.
        Firebase firebase = Firebase.Ins;

        private void butExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMax_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void butMin_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;

        }

        private void butHome_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            FormHome frmHome = new FormHome() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmHome.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmHome);
            frmHome.Show();
        }

        private void butMybooks_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            FormMyBooks frmMB = new FormMyBooks() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmMB.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmMB);
            frmMB.Show();
        }           
        private void butAcc_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            FormAcc frmAcc = new FormAcc() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmAcc.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmAcc);
            frmAcc.Show();
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            FormHelp frmHelp = new FormHelp() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmHelp.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmHelp);
            frmHelp.Show();
        }


        private void butSearch_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            frmSearch frmSearch = new frmSearch() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmSearch.FormBorderStyle = FormBorderStyle.None;
            frmSearch.SearchCriteria = textBox1.Text;
            this.panelLoad.Controls.Add(frmSearch);
            frmSearch.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearPanelLoad();

            AddBooks frmadd = new AddBooks { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmadd.FormBorderStyle = FormBorderStyle.None;
            this.panelLoad.Controls.Add(frmadd);
            frmadd.Show();
        }

        public void ClearPanelLoad()
        {
            foreach (Control item in this.panelLoad.Controls)
            {
                if (item is Form)
                    (item as Form).Hide();
                else
                    item.Dispose();
            }

            this.panelLoad.Controls.Clear();
        }
    }
}
