using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NegociacaoMercadoria
{
    public partial class Default : System.Web.UI.Page
    {
        public string conexao = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                    carregarLista();

            }
            catch (Exception erro)
            {
                this.mensagemLabel.Text = erro.Message;
            }

        }

        protected void novoButton_Click(object sender, EventArgs e)
        {
            try
            {
                //exbir os campos para entrada de dados controle dadosView
                this.negociacaoMultiView.ActiveViewIndex = 1;
                this.mensagemLabel.Text = string.Empty;
            }
            catch (Exception erro)
            {

                this.mensagemLabel.Text = erro.Message;
            }
        }

        protected void voltarButton_Click(object sender, EventArgs e)
        {
            try
            {
                //exbir a visão da lista de dados controle listaView
                this.negociacaoMultiView.ActiveViewIndex = 0;
                this.mensagemLabel.Text = string.Empty;
                this.carregarLista();
            }
            catch (Exception erro)
            {

                this.mensagemLabel.Text = erro.Message;
            }
        }

        protected void salvarButton_Click(object sender, EventArgs e)
        {
            try
            {
                //salvar banco de dados
                this.salvar();
                
            }
            catch (Exception erro)
            {

                this.mensagemLabel.Text = erro.Message;
            }
        }

        protected void excluirButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.excluir();
            }
            catch (Exception erro)
            {

                this.mensagemLabel.Text = erro.Message;
            }
        }


        protected void listaGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            try
            {
                listaGridView.PageIndex = e.NewPageIndex;
                carregarLista();
            }
            catch (Exception erro)
            {
                this.mensagemLabel.Text = erro.Message;
            }
        }

        protected void listaGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            try
            {
                var gv = (GridView)sender;
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int id = (int)gv.DataKeys[rowIndex].Value;
                if (  this.pesquisar(id) == false )
                    throw new Exception("Mercadoria não cadastrada.");
                else
                    this.negociacaoMultiView.ActiveViewIndex = 1;

            }
            catch (Exception erro)
            {
                this.mensagemLabel.Text = erro.Message;
            }
        }

        private void salvar()
        {

            //Definir a conexão
            var con = new SqlConnection(conexao);

            //Definir o comando
            var sql = @"SELECT 
                        codigoMercadoria, 
                        tipoMercadoria,
                        nomeMercadoria,
                        quantidade,
                        preco,
                        tipoNegocio  
                        FROM NegociacaoMercadoria 
                        WHERE codigoMercadoria=@codigoMercadoria";
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@codigoMercadoria", this.codigoMercadoriaTextBox.Text);

            //Abrir conexão
            con.Open();

            //Definir o leitor
            var leitor = cmd.ExecuteReader();

            if (leitor.Read() == false)
                this.incluir();
            else
                this.alterar();
        }


        private void incluir()
        {
            //Definir a conexão
            var con = new SqlConnection(conexao);

            //Definir o comando
            var sql = @"INSERT INTO NegociacaoMercadoria 
                        (
                        codigoMercadoria,
                        tipoMercadoria,
                        nomeMercadoria,
                        quantidade,
                        preco,
                        tipoNegocio
                        )
                        VALUES (
                        @codigoMercadoria,
                        @tipoMercadoria,
                        @nomeMercadoria,
                        @quantidade,
                        @preco,
                        @tipoNegocio
                        )";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@codigoMercadoria", this.codigoMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@tipoMercadoria", this.tipoMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@nomeMercadoria", this.nomeMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@quantidade", this.quantidadeTextBox.Text);
            cmd.Parameters.AddWithValue("@preco", this.precoTextBox.Text.Replace(".", "").Replace(",", "."));
            cmd.Parameters.AddWithValue("@tipoNegocio", this.tipoNegocioDropDownList.Text);


            //Abrir conexão
            con.Open();

            //Executar comando
            cmd.ExecuteNonQuery();

            //Fechar conexão
            con.Close();

            this.codigoMercadoriaTextBox.Text = string.Empty;
            this.tipoMercadoriaTextBox.Text = string.Empty;
            this.nomeMercadoriaTextBox.Text = string.Empty;
            this.quantidadeTextBox.Text = string.Empty;
            this.precoTextBox.Text = string.Empty;
            this.tipoNegocioDropDownList.SelectedIndex = 0;

            this.mensagemLabel.Text = "Inclusão efetuada com sucesso";
        }

        private void alterar()
        {
            //Definir a conexão
            var con = new SqlConnection(conexao);

            //Definir o comando
            var sql = @"UPDATE NegociacaoMercadoria 
                        SET
                        tipoMercadoria=@tipoMercadoria
                        nomeMercadoria=@nomeMercadoria
                        quantidade=@quantidade
                        preco=@preco
                        tipoNegocio=@tipoNegocio
                        WHERE
                        codigoMercadoria=@codigoMercadoria
                        ";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@codigoMercadoria", this.codigoMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@tipoMercadoria", this.tipoMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@nomeMercadoria", this.nomeMercadoriaTextBox.Text);
            cmd.Parameters.AddWithValue("@quantidade", this.quantidadeTextBox.Text);
            cmd.Parameters.AddWithValue("@preco", this.precoTextBox.Text.Replace(".", "").Replace(",", "."));
            cmd.Parameters.AddWithValue("@tipoNegocio", this.tipoNegocioDropDownList.Text);


            //Abrir conexão
            con.Open();

            //Executar comando
            cmd.ExecuteNonQuery();

            //Fechar conexão
            con.Close();
                       
            this.mensagemLabel.Text = "Inclusão efetuada com sucesso";
        }

        private void excluir()
        {
            //Definir a conexão
            var con = new SqlConnection(conexao);

            //Definir o comando
            var sql = @"DELETE FROM NegociacaoMercadoria 
                      WHERE  codigoMercadoria=@codigoMercadoria";

            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@codigoMercadoria", this.codigoMercadoriaTextBox.Text);
           
            //Abrir conexão
            con.Open();

            //Executar comando
            cmd.ExecuteNonQuery();

            //Fechar conexão
            con.Close();

            this.mensagemLabel.Text = "Inclusão efetuada com sucesso";
        }

        private bool pesquisar(int codigoMercadoria)
        {
            //Definir a conexão
            var con = new SqlConnection(conexao);

            //Definir o comando
            var sql = @"SELECT 
                        codigoMercadoria, 
                        tipoMercadoria,
                        nomeMercadoria,
                        quantidade,
                        preco,
                        tipoNegocio  
                        FROM NegociacaoMercadoria 
                        WHERE codigoMercadoria=@codigoMercadoria";
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@codigoMercadoria", codigoMercadoria);

            //Abrir conexão
            con.Open();

            //Definir o leitor
            var leitor = cmd.ExecuteReader();

            if ( leitor.Read() ==  false )
            {
                this.codigoMercadoriaTextBox.Text = string.Empty;
                this.tipoMercadoriaTextBox.Text = string.Empty;
                this.nomeMercadoriaTextBox.Text = string.Empty;
                this.quantidadeTextBox.Text = string.Empty;
                this.precoTextBox.Text = string.Empty;
                this.tipoNegocioDropDownList.SelectedIndex = 0;
                //Fechar conexão
                con.Close();
                return false;
            }
            else
            {
                this.codigoMercadoriaTextBox.Text = leitor["codigoMercadoria"].ToString();
                this.tipoMercadoriaTextBox.Text = leitor["tipoMercadoria"].ToString();
                this.nomeMercadoriaTextBox.Text = leitor["nomeMercadoria"].ToString();
                this.quantidadeTextBox.Text = leitor["quantidade"].ToString();
                this.precoTextBox.Text = leitor["preco"].ToString();
                this.tipoNegocioDropDownList.SelectedValue = leitor["codigoMercadoria"].ToString();
            }

            //Fechar conexão
            con.Close();

            return true;

        }

        private void carregarLista()
        {
            try
            {
                //Definir comando SQL
                var sql = @"SELECT 
                        codigoMercadoria, 
                        tipoMercadoria,
                        nomeMercadoria,
                        quantidade,
                        preco,
                        tipoNegocio  
                        FROM NegociacaoMercadoria";

                //DataAdapter
                var da = new SqlDataAdapter(sql, conexao);

                //DataSet 
                var ds = new DataSet();

                //Preencher o DataSet
                da.Fill(ds);

                //Vincular o DataSet ao GridView
                this.listaGridView.DataSource = ds;
                this.listaGridView.DataBind();

            }
            catch (Exception erro)
            {
                Response.Write(erro.Message);
            }

        }

    }




}