using DeliverIT.Api.ViewModels;
using DeliverIT.API;
using DeliverIT.Tests.Config;
using System.Net;
using Xunit;

namespace DeliverIT.Tests.Autenticacao
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    [TestCaseOrderer("DeliverIt.Tests.config.PriorityOrderer", "DeliverIt.Tests")]
    public class AutenticacaoTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        public AutenticacaoTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Gerando Token de Acesso Valido"), TestPriority(1)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void GerarTokenValidoParaAutenticacao()
        {
            var login = new UsuarioViewModel()
            {
                Username = "teste",
                Password = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            response.EnsureSuccessStatusCodeOrResponseException();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Gerando Token de Acesso Invalido"), TestPriority(2)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void GerarTokenInalidoParaAutenticacao()
        {
            var login = new UsuarioViewModel()
            {
                Username = "teste2",
                Password = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/Login", login);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Criar nova conta de Usuário Valida"), TestPriority(3)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void CriarNovaContaDeUsuarioValido()
        {
            var login = new UsuarioViewModel()
            {
                Username = "teste3",
                Password = "12345678"
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/CriarNovoUsuario", login);
            response.EnsureSuccessStatusCodeOrResponseException();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact(DisplayName = "Criar nova conta de Usuário Invalida"), TestPriority(4)]
        [Trait("Autenticacao", "Autenticacao")]
        public async void CriarNovaContaDeUsuarioInvalido()
        {
            var login = new UsuarioViewModel()
            {
                Username = "teste3",
                Password = null
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Autenticacao/CriarNovoUsuario", login);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
