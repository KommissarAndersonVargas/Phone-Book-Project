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
    public partial class Search_Form : Form
    {
        public BindingList<PersonInformation> personinformation { get; set; }
        public Search_Form()
        {
            InitializeComponent();
            
        }

        private void Search_Form_Load(object sender, EventArgs e)
        {
            label1.Text = personinformation.Count().ToString();
        }
        
    }
}
