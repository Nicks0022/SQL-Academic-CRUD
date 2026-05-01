using Model;
using Data;
using System;
using System.Windows.Forms;

namespace FrontDesktop
{
    public partial class FrmDisciplina : Form
    {
        private DisciplinaRepository _disciplinaRepository;

        public FrmDisciplina()
        {
            InitializeComponent();
            _disciplinaRepository = new DisciplinaRepository();
            CarregarGrid();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Disciplina novaDisciplina = new Disciplina
                {
                    Codigo = txtCodigo.Text,
                    Nome = txtName.Text,

                    // O NumericUpDown retorna um 'decimal', então convertemos para 'int'
                    CargaHoraria = Convert.ToInt32(numCargaHoraria.Value)
                };

                bool sucesso = _disciplinaRepository.Salvar(novaDisciplina);

                if (sucesso)
                {
                    MessageBox.Show("Disciplina cadastrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarGrid()
        {
            try
            {
                dgvDisciplinas.DataSource = _disciplinaRepository.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a lista: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtName.Clear();
            numCargaHoraria.Value = 0; 
        }
    }
}