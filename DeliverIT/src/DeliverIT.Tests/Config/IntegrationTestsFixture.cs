using DeliverIT.Api.ViewModels;
using DeliverIT.API;
using DeliverIT.Business.Models;
using DeliverIT.Business.Services;
using DeliverIT.Data.Context;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using Xunit;

namespace DeliverIT.Tests.Config
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }
    public class IntegrationTestsFixture<TStartup> where TStartup : class
    {
        public readonly DeliverItFactory<TStartup> Factory;
        public readonly DeliverDbContext Context;
        public HttpClient Client;
        public HttpClient ClientAuth;
        public TokenAcesso Token { get; set; }
        public int Prioridade { get; set; }

        public IntegrationTestsFixture()
        {
            Factory = new DeliverItFactory<TStartup>();
            Context = (DeliverDbContext)Factory.Services.GetService(typeof(DeliverDbContext));
            Client = Factory.CreateClient();
            ClientAuth = Factory.CreateClient();
            Autenticar();
            CriaUsuarioApi();
            CriarConta();
        }

        public async void Autenticar()
        {
            var login = new UsuarioViewModel()
            {
                Username = "teste",
                Password = "12345678"
            };
            var response = await Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            response.EnsureSuccessStatusCodeOrResponseException();

            string conteudo = response.Content.ReadAsStringAsync().Result;
            Token = JsonConvert.DeserializeObject<TokenAcesso>(conteudo);
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token.Token);
            ClientAuth.SetAuthorizeBearer(Token.Token);
        }

        private  void CriaUsuarioApi()
        {
            var login = new Usuario()
            {
                Username = "teste",
                Password = "12345678"
            };
            login.Password = PasswordService.GeneratePassword(login.Password);
            Context.usuario.Add(login);
            Context.SaveChanges();
        }
        private void CriarConta()
        {
            var novaSemAtrasoConta = new Business.Models.Conta()
            {
                Nome = "Cartão Credito",
                ValorOriginal = 1300,
                ValorCorrigido = 1300,
                DiasEmAtraso = 0,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now,
            };
            Context.conta.Add(novaSemAtrasoConta);
            Context.SaveChanges();
        }

    }
}
