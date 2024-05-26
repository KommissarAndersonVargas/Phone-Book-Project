
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.DefaultExt = "xml";
            openFileDialog.AddExtension = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                personinformation = DesserializarXmlParaBindingList(openFileDialog.FileName);
                dataGridView1.DataSource = personinformation;
                GenerateCollunsWithName();
            }
            InitializeNameOfFilter();
            InitComboBoxOfDataFilter();
            showingTheSize.Text = $"Dados: {personinformation.Count()}";
        }
        public void InitializeNameOfFilter()
        {
            selcao_de_ordenamento.Text = "Ordenar por: ";
        }
        public void InitComboBoxOfDataFilter()
        {
            selcao_de_ordenamento.Items.Add("Ordenar por CPF");
            selcao_de_ordenamento.Items.Add("Ordenar por Nome");
            selcao_de_ordenamento.Items.Add("Ordenar por Endereço");
        }
        public static BindingList<PersonInformation> DesserializarXmlParaBindingList(string caminhoArquivo)
        {
            var serializer = new XmlSerializer(typeof(BindingList<PersonInformation>));
            using (TextReader reader = new StreamReader(caminhoArquivo))
            {
                return (BindingList<PersonInformation>)serializer.Deserialize(reader);
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
                else if (coluna.DataPropertyName == "fullName")
                {
                    coluna.HeaderText = "Nome Completo";
                }
                else if (coluna.DataPropertyName == "cellPhoneNumber")
                {
                    coluna.HeaderText = "Numero de telefone";
                }
                else if (coluna.DataPropertyName == "email")
                {
                    coluna.HeaderText = "E-mail";
                }
                else if (coluna.DataPropertyName == "address")
                {
                    coluna.HeaderText = "Endereço";
                }
                else if (coluna.DataPropertyName == "Hora_da_entrada")
                {
                    coluna.HeaderText = "Dia de entrada";
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SalvarBindingListEmExcel(personinformation);
        }
        public void SalvarBindingListEmExcel(BindingList<PersonInformation> pessoas)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Planilha1");
                    worksheet.Cell(1, 1).Value = "Cpf";
                    worksheet.Cell(1, 2).Value = "Nome completo";
                    worksheet.Cell(1, 3).Value = "Numero de telefone";
                    worksheet.Cell(1, 4).Value = "E-mail";
                    worksheet.Cell(1, 5).Value = "Enderço curto";
                    worksheet.Cell(1, 6).Value = "Horário de entrada";

                    int rowStart = 2;
                    foreach (var pessoa in pessoas)
                    {
                        worksheet.Cell(rowStart, 1).Value = pessoa.cpf;
                        worksheet.Cell(rowStart, 2).Value = pessoa.fullName;
                        worksheet.Cell(rowStart, 3).Value = pessoa.cellPhoneNumber;
                        worksheet.Cell(rowStart, 4).Value = pessoa.email;
                        worksheet.Cell(rowStart, 5).Value = pessoa.address;
                        worksheet.Cell(rowStart, 6).Value = pessoa.Hora_da_entrada;
                        rowStart++;
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                }
                if(saveFileDialog!= null)
                {
                    MessageBox.Show("Arquivo Excel gerado com sucesso");
                }
                else
                {
                    MessageBox.Show("Erro: O arquivo Excel não foi gerado");
                }
            }
        }

        private void RemoveBotton_Click(object sender, EventArgs e)
        {
            RemoveSelectedItem();
            showingTheSize.Text = $"Dados: {personinformation.Count()}";
        }
        private void RemoveSelectedItem()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtenha o índice da linha selecionada
                int selectedIndex = dataGridView1.SelectedRows[0].Index;

                // Remova o item da lista com base no índice
                if (selectedIndex >= 0 && selectedIndex < personinformation.Count)
                {
                    personinformation.RemoveAt(selectedIndex);
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para remover.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(selecao_de_procura.Text == "Nome" )
            {
                ProcurarESelecionarPessoaByName(procura_texto.Text.ToString());
            }
            if (selecao_de_procura.Text == "CPF")
            {
                ProcurarESelecionarPessoaByCpf(procura_texto.Text.ToString());
            }
            procura_texto.Clear();
            procura_texto.Focus();
           
        }
        public void ProcurarESelecionarPessoaByCpf(string cpf)
        {
            // Procura a pessoa pelo cpf na lista de pessoas
            var people = personinformation.FirstOrDefault(p => p.cpf == cpf);

            if (people != null)
            {
                // Encontra o índice da pessoa na lista
                int index = personinformation.IndexOf(people);

                // Seleciona a linha correspondente no DataGridView
                dataGridView1.Rows[index].Selected = true;

                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
            else
            {
                MessageBox.Show("Pessoa não encontrada");
            }
        }
        public void ProcurarESelecionarPessoaByName(string name)
        {
            // Procura a pessoa pelo nome na lista de pessoas
            var people = personinformation.FirstOrDefault(p => p.fullName == name);

            if (people != null)
            {
                // Encontra o índice da pessoa na lista
                int index = personinformation.IndexOf(people);

                // Seleciona a linha correspondente no DataGridView
                dataGridView1.Rows[index].Selected = true;

                dataGridView1.FirstDisplayedScrollingRowIndex = index;
            }
            else
            {
                MessageBox.Show("Pessoa não encontrada");
            }
        }

        public void OrderToDataGrid()
        {
            try
            {

                if (selcao_de_ordenamento.Text == "Ordenar por CPF")
                {
                    var newOrnedData = PersonInformation.GetOrderByCpf(personinformation).ToList();
                    personinformation.Clear();

                    foreach (var person in newOrnedData)
                    {
                        personinformation.Add(person);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = personinformation;
                }
                if (selcao_de_ordenamento.Text == "Ordenar por Endereço")
                {
                    var newOrnedData = PersonInformation.GetOrderByAddress(personinformation).ToList();
                    personinformation.Clear();

                    foreach (var person in newOrnedData)
                    {
                        personinformation.Add(person);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = personinformation;
                }
                if (selcao_de_ordenamento.Text == "Ordenar por Nome")
                {
                    var newOrnedData = PersonInformation.GetOrderByName(personinformation).ToList();
                    personinformation.Clear();

                    foreach (var person in newOrnedData)
                    {
                        personinformation.Add(person);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = personinformation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocorreu um erro");
            }
        }

        private void FilterBotton_Click(object sender, EventArgs e)
        {
            OrderToDataGrid();
            GenerateCollunsWithName();
        }
    }
}
