using DeliverIT.Api.ViewModels;
using DeliverIT.API;
using DeliverIT.Tests.Config;
using System;
using System.Net;
using Xunit;

namespace DeliverIT.Tests.Conta
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    [TestCaseOrderer("DeliverIt.Tests.config.PriorityOrderer", "DeliverIt.Tests")]
    public class ContaTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        public ContaTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Cadastro de conta Valida sem atraso"), TestPriority(1)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaSemAtraso()
        {
            var novaSemAtrasoConta = new ContaRequestViewModel()
            {
                Nome = "Conta De Telefone",
                ValorOriginal = 500,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now,
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", novaSemAtrasoConta);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.Conta>();

            Assert.NotNull(result);
            Assert.Equal(500, result.ValorCorrigido);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com 3 dias de atraso"), TestPriority(2)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComTresDiasDeAtraso()
        {
            var contaTresDiasDeAtraso = new ContaRequestViewModel()
            {
                Nome = "Conta De Luz",
                ValorOriginal = 450,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now.AddDays(3),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaTresDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.Conta>();

            Assert.NotNull(result);
            Assert.Equal(459.1350M, result.ValorCorrigido);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com mais de 3 dias de atraso"), TestPriority(3)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComMaisDeTresDiasDeAtraso()
        {
            var contaComMaisDETresDiasDeAtraso = new ContaRequestViewModel()
            {
                Nome = "Conta De Agua",
                ValorOriginal = 450,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now.AddDays(4),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaComMaisDETresDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.Conta>();

            Assert.NotNull(result);
            Assert.Equal(463.8600M, result.ValorCorrigido);
        }

        [Fact(DisplayName = "Cadastro de conta Valida com mais de 5 dias de atraso"), TestPriority(4)]
        [Trait("Conta", "Conta")]
        public async void CadastrarUmaContaValidaComMaisDeCincoDiasDeAtraso()
        {
            var contaCincoDiasDeAtraso = new ContaRequestViewModel()
            {
                Nome = "Conta Hospital",
                ValorOriginal = 700,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now.AddDays(8),
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", contaCincoDiasDeAtraso);
            response.EnsureSuccessStatusCodeOrResponseException();
            var result = await response.Content.ReadAsJsonAsync<Business.Models.Conta>();

            Assert.NotNull(result);
            Assert.Equal(736.6800M, result.ValorCorrigido);
        }


        [Fact(DisplayName = "Cadastro de conta Invalida"), TestPriority(5)]
        [Trait("Conta", "Conta")]
        public async void CadastroDeContaInvalida()
        {
            var novaConta = new ContaRequestViewModel()
            {
                Nome = null,
                ValorOriginal = 500,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now,
            };
            var response = await _testsFixture.Client.PostAsJsonAsync("/v1/Conta/CadastrarNovaConta", novaConta);
            Assert.NotEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
