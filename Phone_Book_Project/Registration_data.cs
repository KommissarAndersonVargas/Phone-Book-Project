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
using System.Xml.Serialization;

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
            string fullName = FNtextBox2.Text.ToString();
            string cellNumber = CNtextBox4.Text.ToString();
            string email = EmailtextBox5.Text.ToString();
            string address = AddressTextBox6.Text.ToString();
            DateTime time_of_register = DateTime.Now;
            PersonInformation person = new PersonInformation(cpf, fullName, cellNumber, email, address, time_of_register);
            personInformation.Add(person);

          
            ResetTextOfBoxes();

        }

        private void SavaBotton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SalvarData(saveFileDialog.FileName);
            }
        }
        public void SalvarData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, personInformation);
            }

            MessageBox.Show("Gerado com sucesso");

            this.Close();
        }

        private void Registration_data_Load(object sender, EventArgs e)
        {
            this.info_gbox.Enabled = true;
        }
        public void ResetTextOfBoxes()
        {
            this.IDtextBox1.ResetText();
            this.FNtextBox2.ResetText();
            this.CNtextBox4.ResetText();
            this.EmailtextBox5.ResetText();
            this.AddressTextBox6.ResetText();
        }
        
    }
}
