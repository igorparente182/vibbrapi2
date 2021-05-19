### Nome do Projeto
<h1 align="center">
    <a href="https://vibbraapi20210501125029.azurewebsites.net/swagger/index.html">🔗 vibbraapi</a>
</h1>

<p align="center">🚀 api de cadastro de atividades de usuários no projeto</p>

### Sobre
<p>O sistema tem como finalidade registrar o tempo de de atividades do usuario nos projetos</p>

### tabela-de-conteudo
<!--ts-->
   * [Sobre](#Sobre)
   * [Tabela de Conteudo](#tabela-de-conteudo)
   * [Instalação](#instalacao)
   * [Como usar](#como-usar)
   * [Tecnologias](#tecnologias)
<!--te-->


### instalacao
<h1>Passo 1 (Habilitar IIS)</h1>
<p>Por default, algumas máquinas não tem o IIS habilitado, para isso faça:</p>

- [x] Acesse o painel de controle <br>
- [x] Clique em adicionar e/ou remover programas <br>
- [x] Clique em Habilitar/desabilitar recursos do windows <br>
- [x] Selecione a opção de “Serviços de Informações da Internet” (IIS) <br>
- [x] Pressione “OK” e aguarde enquanto o Windows realiza as configurações

<h1>Passo 2 - Baixar a versao do Net 5.0</h1>
<p>Instalação do <a href="https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.202-windows-x64-installer"><b>Net 5.0</b></a></p>

<h1>Passo 3 - Compilar o projeto</h1>

- [x] Abra o <a href="https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&rel=16">visual studio 2019 community</a> <br>
- [x] Selecione a opção de Arquivo=> Abrir => "Projeto/Solucao" escolher a pasta aonde esta a solucao e apertar vibbraapi.sln <br>
- [x] Clique em cima do projeto vibbraapi <br>
- [x] Compilacao => Publicar vibbraapi<br>
- [x] Destino opcao "Pasta" escolha a pasta para fazer a publicacao neste caso escolha a pasta aonde esta o wwwroot do iis caso esteja usando o IIS como servidor de aplicacao caso deseja usar o docker seguir este tutorial <a href="https://docs.microsoft.com/pt-br/visualstudio/containers/hosting-web-apps-in-docker?view=vs-2019">"clique aqui"</a>

<h1>Passo 4 - Configurar o iis para aplicacoes em asp.net core </h1>

- [x] Abra o Gerenciador do Serviços de Informações da Internet  <br>
- [x] Aperte no Pools de Aplicativo <br>
- [x] Na aba direita selecione Adicionar Pool de Aplicativo <br>
- [x] Coloque um nome que achar melhor neste caso coloquei vibbraapi <br>
- [x] Selecione opcao da Versao do .Net CLR a escolha  Sem Codigo Gerenciado(nao esqueca de instalar a versao do .net 5.0) aperta ok
- [x] Ainda no Gerenciador abra opcao Sites => Default Web Site e selecione o projeto compilado que voce fez no passo 3
- [x] Aperta no botao direito do mouse e selecione Converter para Aplicativo coloque opcao Alias: vibbraapi o  Pool de Aplicativo o que voce neste caso usei vibbraapi caminho fisico aonde se encontra o projeto compilado

### como-usar

<p>Para os teste estou usando o swagger para poder documentar e testar api</p>
Link para teste https://vibbraapi20210501125029.azurewebsites.net/swagger/index.html

<p><b>Obeservacoes</b></p>
- [x] Para consumir api terar que fazer login usando https://vibbraapi20210501125029.azurewebsites.net/api/v1/users/authenticate passando no body 
{
  "login": "srvibbraneo@gmail.com",
  "password": "123"
} <br>

- [x] Pegar o token e colocar no Authorize ` Bearer [token]`


### tecnologias

- [x] Asp.Net Core 5.0 <br>
- [x] Banco de Dados Sql server(os script se encontrar dentro projeto vibbraapi.infra/ vibbraapi.sql)<br>
- [x] Azure para hospedagem da api <br>
