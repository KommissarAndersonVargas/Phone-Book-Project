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
    public partial class Registration_data : Form
    {
        BindingList<PersonInformation> personInformation = new BindingList<PersonInformation>();
        public Registration_data()
        {
            InitializeComponent();
        }

        private void newData_Click(object sender, EventArgs e)
        {
            string cpf = IDtextBox1.Text.ToString();
            string firstName = FNtextBox2.Text.ToString();
            string lastName = LNtextBox3.Text.ToString();
            string cellNumber = CNtextBox4.Text.ToString();
            string email = EmailtextBox5.Text.ToString();
            string address = AddressTextBox6.Text.ToString();
            DateTime time_of_register = DateTime.Now;
            PersonInformation person = new PersonInformation(cpf, firstName, lastName, cellNumber, email, address, time_of_register);
            personInformation.Add(person);

            InitializeSearchFormWithData();
            ResetTextOfBoxes();

        }

        private void SavaBotton_Click(object sender, EventArgs e)
        {

        }


        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Registration_data_Load(object sender, EventArgs e)
        {
            this.info_gbox.Enabled = true;
        }
        public void ResetTextOfBoxes()
        {
            this.IDtextBox1.ResetText();
            this.FNtextBox2.ResetText();
            this.LNtextBox3.ResetText();
            this.CNtextBox4.ResetText();
            this.EmailtextBox5.ResetText();
            this.AddressTextBox6.ResetText();
        }
        public void InitializeSearchFormWithData()
        {
            Search_Form search = new Search_Form
            {
                personinformation = personInformation
            };
            if (personInformation.Count() == 1)
            {
                search.Show();
            }

        }
    }
}
