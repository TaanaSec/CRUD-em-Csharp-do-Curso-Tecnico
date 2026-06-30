using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using Mysqlx.Prepare; //Referencia a Biblioteca MySQL

namespace Formulário_CRUD
{
    public partial class Form1 : Form
    {
        //Declaração de variáveis

        MySqlConnection conexao; //Variável de conexão com o banco de dados
        MySqlCommand comando; //Variável de comando SQL
        MySqlDataAdapter da; //Variável de adaptador de dados
        MySqlDataReader dr; //Variável de leitor de dados
        string sql; //Variável de comando SQL



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //----------------------------------------------------------------------------------

        private void btnNovo_Click(object sender, EventArgs e)
        {
            //Codigo Botao Novo

            try //Bloco try
            {
                conexao = new MySqlConnection("Server=localhost;Database=db_cliente;Uid=root;Pwd=;"); //Conexão com o banco de dados.
                sql = "INSERT INTO CLIENTE (codigo, nome, telefone) VALUES (@CODIGO, @NOME, @TELEFONE)"; //Comando SQL para inserir dados na tabela CLIENTE
                comando = new MySqlCommand(sql, conexao); //Criação do comando SQL). Explicação: O comando SQL é criado com a string: sql e a conexão: conexao.
                comando.Parameters.AddWithValue("@CODIGO", txtCod.Text); //Adiciona o valor do parâmetro @CODIGO com o valor do TextBox txtCod
                comando.Parameters.AddWithValue("@NOME", txtNome.Text); //Adiciona o valor do parâmetro @NOME com o valor do TextBox txtNome
                comando.Parameters.AddWithValue("@TELEFONE", txtTele.Text); //Adiciona o valor do parâmetro @TELEFONE com o valor do TextBox txtTele

                conexao.Open();//Abre a conexão com o banco de dados

                comando.ExecuteNonQuery(); //Executa o comando SQL. Explicação: ExecuteNonQuery é usado para comandos SQL que não retornam dados, como INSERT, UPDATE e DELETE.

            }
            catch (Exception ex) //Tratamento de exceção. ex é a variável que armazena a exceção
            {

                MessageBox.Show(ex.Message); //Exibe uma mensagem de erro caso ocorra uma exceção

            }
            finally //Bloco finally
            {

                MessageBox.Show("Cadastro realizado com sucesso!"); //Exibe uma mensagem de sucesso caso o cadastro seja realizado com sucesso
                conexao.Close(); //Fecha a conexão com o banco de dados
                conexao = null; //Libera a memória alocada para a conexão com o banco de dados
                comando = null; //Libera a memória alocada para o comando SQL
                txtCod.Text = null; //Limpa o TextBox txtCod
                txtNome.Text = null; //Limpa o TextBox txtNome
                txtTele.Text = null; //Limpa o TextBox txtTele

                this.txtNome.Focus(); //Coloca o foco no TextBox txtNome. Explicação: Foco é a ação de selecionar um controle para que o usuário possa interagir com ele. Neste caso, o foco é colocado no TextBox txtNome para que o usuário possa digitar o nome do cliente.
            }
        }

        //----------------------------------------------------------------------------------

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //CODIGO BOTAO CONSULTAR

            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=db_cliente;Uid=root;Pwd=;");

                sql = "SELECT * FROM CLIENTE WHERE CODIGO = @CODIGO";

                comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@CODIGO", txtCod.Text);
                //comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                //comando.Parameters.AddWithValue("@TELEFONE", txtTele.Text);

                conexao.Open();

                dr = comando.ExecuteReader();

                txtNome.Clear();
                txtTele.Clear();

                while (dr.Read())
                {
                    txtNome.Text = Convert.ToString(dr["NOME"]);
                    txtTele.Text = Convert.ToString(dr["TELEFONE"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        //----------------------------------------------------------------------------------

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Digite o Código");
            txtCod.Focus();
            return;

            try
            {
                MessageBox.Show("Tem certeza que deseja Excluir");
                txtCod.Focus();

                conexao = new MySqlConnection("Server=localhost;Database=db_cliente;Uid=root;Pwd=;");
                sql = "DELETE FROM CLIENTE WHERE CODIGO = @CODIGO";

                comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@CODIGO", txtCod.Text);
                //comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                //comando.Parameters.AddWithValue("@TELEFONE", txtTele.Text);

                conexao.Open();

                comando.ExecuteNonQuery();

                MessageBox.Show("Cadastro Excluido!");
            }
            catch (Exception ex)
            {

            }
        }

        //----------------------------------------------------------------------------------

        private void btnConsultarTodos_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=db_cliente;Uid=root;Pwd=;");

                sql = "SELECT * FROM CLIENTE";
                da = new MySqlDataAdapter(sql, conexao);
                DataTable dt = new DataTable();

                da.Fill(dt);
                dgvDados.DataSource = dt;

                conexao.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexao.Close();
                conexao = null;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=db_cliente;Uid=root;Pwd=;");
                sql = "UPDATE CLIENTE SET NOME = @NOME, TELEFONE = @TELEFONE WHERE CODIGO = @CODIGO;";

                comando = new MySqlCommand(sql, conexao);

                comando.Parameters.AddWithValue("@CODIGO", txtCod.Text);
                comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                comando.Parameters.AddWithValue("@TELEFONE", txtTele.Text);

                conexao.Open();

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                MessageBox.Show("Edição Realizada com Sucesso!");
                conexao.Close();
                conexao = null;
            }

        }
    }
}
