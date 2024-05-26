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
            try
            {
                var isNotVazio = FNtextBox2.Text.Length > 0 && FNtextBox2.Text.Length > 0
                    && CNtextBox4.Text.Length > 0
                    && EmailtextBox5.Text.Length > 0 && AddressTextBox6.Text.Length > 0;

                if (isNotVazio)
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
                else
                {
                    MessageBox.Show("Pelo menos um dos campos está vazio");
                }
            }
            catch(Exception error)
            {
                MessageBox.Show($"Um erro ocorreu: {error.ToString()}");
            }

        }

        private void SavaBotton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
                saveFileDialog.DefaultExt = "xml";
                saveFileDialog.AddExtension = true;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (personInformation != null)
                    {
                        SalvarData(saveFileDialog.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Voce não pode salvar um arquivo vazio");
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Um erro ocorreu: {error.ToString()}");
            }
        }
        public void SalvarData(string path)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
                using (TextWriter writer = new StreamWriter(path))
                {
                    serializer.Serialize(writer, personInformation);
                }

                MessageBox.Show("Gerado com sucesso");
            }
            catch (Exception error)
            {
                MessageBox.Show($"Um erro ocorreu: {error.ToString()}");
            }
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

        private void open_file_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML Files (*.xml)|*.xml";
                openFileDialog.DefaultExt = "xml";
                openFileDialog.AddExtension = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var copyinfolist = personInformation;
                    personInformation = DesserializarXmlParaBindingList(openFileDialog.FileName);

                    if (personInformation != null && !copyinfolist.Equals(personInformation))
                    {
                        MessageBox.Show("Arquivo carregado com sucesso");
                    }
                    else
                    {
                        MessageBox.Show("Arquivo NÃO carregado");
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Um erro ocorreu: {error.ToString()}");
            }
        }
        public static BindingList<PersonInformation> DesserializarXmlParaBindingList(string caminhoArquivo)
        {
            var serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
            using (TextReader reader = new StreamReader(caminhoArquivo))
            {
                return (BindingList<PersonInformation>)serializer.Deserialize(reader);
            }
        }
    }
}
