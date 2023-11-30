using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;   

namespace aula_25_08
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string MysqlClientString = "Server = localhost; Port=3306;" +
            "User Id = root; Database = bdcadastro; SSl Mode = 0";

        private void btn_novo_Click(object sender, EventArgs e)
        {
            txt_codigo.Clear();
            txt_titulo.Clear();
            txt_sinopse.Clear();
            txt_duracao.Clear();
            txt_codigo.Focus();

        }

        private void btn_pesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MysqlClientString);
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("select * from tbfilmes " +
                "where codigo = '" + txt_codigo.Text + "'", conn);
                da.Fill(dt);
                txt_titulo.Text = dt.Rows[0].Field<string>("titulo");
                cbo_classificacao.Text = dt.Rows[0].Field<string>("classificacao");
                cbo_genero.Text = dt.Rows[0].Field<string>("genero");
                txt_duracao.Text = dt.Rows[0].Field<string>("duracao");
                txt_sinopse.Text = dt.Rows[0].Field<string>("sinopse");
            }
            catch
            {
                btn_novo_Click(btn_novo, e);
                MessageBox.Show("Filme não cadastrado", "Alerta",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO tbfilmes(codigo, titulo, classificacao, genero," +
                " duracao, sinopse)" +
            "VALUES('" + txt_codigo.Text + "','" + txt_titulo.Text + "'," +
            "'" + cbo_classificacao.Text + "','" + cbo_genero.Text + "'," +
            "'" + txt_duracao.Text + "','" + txt_sinopse.Text + "')";
            MySqlConnection conn = new MySqlConnection(MysqlClientString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Filme gravado com sucesso", "Alerta",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
            btn_novo_Click(btn_novo, e);
        }

        private void btn_alterar_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE tbfilmes SET titulo = '" + txt_titulo.Text + "',classificacao = '" +
                cbo_classificacao.Text + "', genero = '" + cbo_genero.Text + "', duracao = '" +
                txt_duracao.Text + "', sinopse = '" + txt_sinopse.Text + "' where codigo = '" +
                txt_codigo.Text + "'";
            MySqlConnection conn = new MySqlConnection(MysqlClientString);
            MySqlCommand cmd = new MySqlCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Filme alterado com sucesso", "Alerta",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
