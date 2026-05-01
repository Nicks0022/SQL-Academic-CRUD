using Model;
using Data;
using System;
using System.Windows.Forms;

namespace FrontDesktop
{
    public partial class FrmProfessor : Form
    {
        private ProfessorRepository _professorRepository;

        public FrmProfessor()
        {
            InitializeComponent();
            _professorRepository = new ProfessorRepository();
            CarregarGrid();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Professor novoProfessor = new Professor
                {
                    Nome = txtNome.Text,
                    Email = txtEmail.Text,
                    Titulo = txtTitulo.Text
                };

                bool sucesso = _professorRepository.Salvar(novoProfessor);

                if (sucesso)
                {
                    MessageBox.Show("Professor cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                dgvProfessores.DataSource = _professorRepository.Listar();
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
            txtTitulo.Clear();
        }
    }
}