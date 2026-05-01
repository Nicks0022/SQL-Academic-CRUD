using Model;
using Data;
using System;
using System.Windows.Forms;

namespace FrontDesktop
{
    public partial class FrmProduto : Form
    {
        private ProdutoRepository _produtoRepository;

        public FrmProduto()
        {
            InitializeComponent();
            _produtoRepository = new ProdutoRepository();

            // Carrega a lista de produtos assim que a tela abre
            CarregarGrid();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Produto novoProduto = new Produto
                {
                    Nome = txtNome.Text,
                    // Converte o texto que o usuário digitou para formato de dinheiro (decimal)
                    Preco = Convert.ToDecimal(txtPreco.Text)
                };

                bool sucesso = _produtoRepository.Salvar(novoProduto);

                if (sucesso)
                {
                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    CarregarGrid(); // Atualiza o grid na hora
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
                dgvProdutos.DataSource = _produtoRepository.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a lista: " + ex.Message);
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtPreco.Clear();
        }
    }
}