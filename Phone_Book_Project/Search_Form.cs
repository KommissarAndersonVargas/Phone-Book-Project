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
    public partial class Search_Form : Form
    {
        public BindingList<PersonInformation> personinformation = new BindingList<PersonInformation>(); 
        public Search_Form()
        {
            InitializeComponent();
            
        }

        private void Search_Form_Load(object sender, EventArgs e)
        {
            personinformation = DesserializarXmlParaBindingList(@"C:\Users\Usuario\OneDrive\Documentos\Images\ArquivoProjeto.xml");
            dataGridView1.DataSource = personinformation;
            GenerateCollunsWithName();
        }
        public static BindingList<PersonInformation> DesserializarXmlParaBindingList(string caminhoArquivo)
        {
            var serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
            using (var stream = new FileStream(caminhoArquivo, FileMode.Open))
            {
                return (BindingList<PersonInformation>)serializer.Deserialize(stream);
            }
        }

        public void GenerateCollunsWithName()
        {

            foreach (DataGridViewColumn coluna in dataGridView1.Columns)
            {
                if (coluna.DataPropertyName == "cpf")
                {
                    coluna.HeaderText = "CPF";
                }
                else if (coluna.DataPropertyName == "firstName")
                {
                    coluna.HeaderText = "First Name";
                }
                else if (coluna.DataPropertyName == "lastName")
                {
                    coluna.HeaderText = "Last Name";
                }
                else if (coluna.DataPropertyName == "cellPhoneNumber")
                {
                    coluna.HeaderText = "Cell Phone Number";
                }
                else if (coluna.DataPropertyName == "email")
                {
                    coluna.HeaderText = "E-mail";
                }
                else if (coluna.DataPropertyName == "address")
                {
                    coluna.HeaderText = "Address";
                }
                else if (coluna.DataPropertyName == "Hora_da_entrada")
                {
                    coluna.HeaderText = "Arrived Time";
                }
            }


        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                SalvaData(saveFileDialog.FileName);
            }
        }
        private void SalvaData(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, personinformation);
            }


        }
    }
}
