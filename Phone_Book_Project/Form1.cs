using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phone_Book_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Registration_data dataForm = new Registration_data();
            dataForm.MdiParent = this;
            dataForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Search_Form searchData = new Search_Form();
            searchData.MdiParent = this;
            searchData.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = System.Diagnostics.Process.Start("calc.exe");
            process.WaitForInputIdle();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            About_Us aboutUs = new About_Us();
            aboutUs.MdiParent = this;
            aboutUs.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            
            this.Close();
            
        }
    }
}
