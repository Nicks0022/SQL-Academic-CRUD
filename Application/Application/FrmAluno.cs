using Model;
using Data;
using System;
using System.Windows.Forms;

namespace FrontDesktop
{
    public partial class FrmAluno : Form
    {
        // Cria a variável do repositório para a tela inteira poder usar
        private AlunoRepository _alunoRepository;

        public FrmAluno()
        {
            InitializeComponent();
            _alunoRepository = new AlunoRepository();

            // Carrega os alunos do banco logo que a tela abre
            CarregarGrid();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Pega os dados das caixinhas da tela e coloca no objeto Aluno
                Aluno novoAluno = new Aluno
                {
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    DataNascimento = dtpNascimento.Value.Date
                };

                // 2. Manda o repositório salvar no banco de dados
                bool sucesso = _alunoRepository.Salvar(novoAluno);

                if (sucesso)
                {
                    MessageBox.Show("Aluno cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid(); // Atualiza a tabela na tela para mostrar o aluno novo
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
                // Busca a lista do banco e joga direto no DataGridView
                dgvAlunos.DataSource = _alunoRepository.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a lista: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            dtpNascimento.Value = DateTime.Now;
        }
    }
}