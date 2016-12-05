<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NegociacaoMercadoria.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Negociação de Mercadoria
    </title>

    <script src="Scripts/jquery-3.1.1.min.js"></script>
    <script src="Scripts/jquery.maskMoney.min.js"></script>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />


    <script type="text/javascript">



        $(function () {

            $('#codigoMercadoriaTextBox').maskMoney({
                showSymbol: true,
                symbol: "",
                decimal: "",
                thousands: ""
            });

            $('#quantidadeTextBox').maskMoney({
                showSymbol: true,
                symbol: "",
                decimal: "",
                thousands: ""
            });


            $('#precoTextBox').maskMoney({
                showSymbol: true,
                symbol: "R$",
                decimal: ",",
                thousands: "."
            });
        });

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>


            <header>
                <div style="text-align: center">
                    <h2>Negociação de Mercadoria </h2>
                </div>
            </header>

            <main>
                <div class="container" style="text-align: center">
                    <asp:Label ID="mensagemLabel" runat="server" Font-Bold="True"></asp:Label>
                </div>

                <asp:MultiView ID="negociacaoMultiView" runat="server" ActiveViewIndex="0">
                    <asp:View ID="listaView" runat="server" ViewStateMode="Enabled">
                        <div class="container">
                            <asp:GridView CssClass="form-control"
                                ID="listaGridView"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id"
                                runat="server"
                                OnRowCommand="listaGridView_RowCommand1" 
                                AllowPaging="True" 
                                OnPageIndexChanging="listaGridView_PageIndexChanging" 
                                PageSize="5">
                                <Columns>
                                    <asp:BoundField DataField="codigoMercadoria"
                                        ItemStyle-CssClass=""
                                        HeaderText="Código"
                                        HeaderStyle-CssClass="" />
                                    <asp:ButtonField DataTextField="nomeMercadoria"
                                        ItemStyle-CssClass=""
                                        HeaderStyle-CssClass=""
                                        CommandName="alterar"
                                        HeaderText="Nome da Mercadoria" />
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="novoButton" runat="server" Text="Novo" CssClass="btn btn-primary" OnClick="novoButton_Click" />

                        </div>
                    </asp:View>
                    <asp:View ID="dadosView" runat="server">
                        <div class="container">



                            <div class="form-group">
                                <asp:Label ID="codigoMercadoriaLabel" runat="server" Text="Código da Mercadoria:"></asp:Label>
                                <asp:TextBox ID="codigoMercadoriaTextBox" runat="server" CssClass="form-control"></asp:TextBox>

                            </div>
                            <br />


                            <div class="form-group">
                                <asp:Label ID="tipoMercadoriaLabel" runat="server" Text="Tipo da Mercadoria:"></asp:Label>
                                <asp:TextBox ID="tipoMercadoriaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <br />

                            <div class="form-group">
                                <asp:Label ID="nomeMercadoriaLabel" runat="server" Text="Nome da Mercadoria:"></asp:Label>
                                <asp:TextBox ID="nomeMercadoriaTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <br />

                            <div class="form-group">
                                <asp:Label ID="quantidadeLabel" runat="server" Text="Quantidade:"></asp:Label>
                                <asp:TextBox ID="quantidadeTextBox" runat="server" CssClass="form-control" onkeyup="javascript:formataInteiro(this,event);"></asp:TextBox>
                            </div>
                            <br />

                            <div class="form-group">
                                <asp:Label ID="precoLabel" runat="server" Text="Preço"></asp:Label>
                                <div class="input-group">
                                    <div class="input-group-addon">R$</div>
                                    <asp:TextBox ID="precoTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <br />

                            <div class="form-group">
                                <asp:Label ID="tipoNegocioLabel" runat="server" Text="Tipo de Negocio"></asp:Label>
                                <asp:DropDownList ID="tipoNegocioDropDownList" runat="server" CssClass="form-control">
                                    <asp:ListItem>Compra</asp:ListItem>
                                    <asp:ListItem>Venda</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />

                            <asp:Button ID="salvarButton" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="salvarButton_Click" />
                            <asp:Button ID="excluirButton" runat="server" Text="Excluir" OnClientClick="return confirm (&quot;Deseja excluir registro ?&quot;);" CssClass="btn btn-primary" />
                            <asp:Button ID="voltarButton" runat="server" Text="Voltar" CssClass="btn btn-primary" OnClick="voltarButton_Click" />

                            <br />
                            <br />

                            <asp:GridView ID="lsitaGridView" runat="server"></asp:GridView>



                        </div>
                    </asp:View>
                </asp:MultiView>






            </main>


            <footer>
                <div>
                </div>
            </footer>
        </div>
    </form>
</body>
</html>
