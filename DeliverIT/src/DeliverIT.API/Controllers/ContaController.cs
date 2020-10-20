using DeliverIT.Api.ViewModels;
using DeliverIT.Business.Interfaces;
using DeliverIT.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIT.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaRepository _repository;
        public ContaController(IContaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("CadastrarNovaConta")]
        [Authorize]
        public ActionResult CadastrarNovaConta([FromBody] ContaRequestViewModel conta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return NotFound(conta);
                }
                int diasAtraso = _repository.CalcularDiasEmAtraso(conta.DataVencimento, conta.DataPagamento);
                decimal valorCorrigido = _repository.CalcularMultaEJuros(diasAtraso, conta.ValorOriginal);
                var novaconta = new Conta
                {
                    Nome = conta.Nome,
                    DiasEmAtraso = diasAtraso,
                    ValorOriginal = conta.ValorOriginal,
                    ValorCorrigido = valorCorrigido,
                    DataPagamento = conta.DataPagamento,
                    DataVencimento = conta.DataVencimento
                };
                _repository.Adicionar(novaconta);
                _repository.SaveChanges();
                return Ok(novaconta);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível cadastrar a conta" });
            }
        }

        [HttpGet]
        [Route("ListarContasCadastradas")]
        [Authorize]
        public ActionResult<List<ContaResponseViewModel>> ListarContasCadastradas()
        {
            try
            {
                var lista = _repository.ListarTodasAsContas()
                                       .Select(p => new ContaResponseViewModel
                                       {
                                           Nome = p.Nome,
                                           ValorOriginal = p.ValorOriginal.ToString("N2"),
                                           ValorCorrigido = p.ValorCorrigido.ToString("N2"),
                                           DiasEmAtraso = p.DiasEmAtraso,
                                           DataPagamento = p.DataPagamento.ToString("dd/MM/yyyy HH:mm:ss"),
                                       });
                return Ok(lista);
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível obter  a lista de contas" });
            }
        }
    }
}
