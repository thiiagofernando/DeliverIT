using DeliverIT.Business.Interfaces;
using DeliverIT.Business.Models;
using DeliverIT.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIT.Data.Repository
{
    public class ContaRepository : Repository<Conta>, IContaRepository
    {
        private const decimal taxaMultaAte3Dias = 0.02M;
        private const decimal taxaJurosMoraPorDiaAtrasoAte3Dias = 0.0001M;
        private const decimal taxaMultaSuperior3Dias = 0.03M;
        private const decimal taxaJurosMoraPorDiaAtrasoSuperior3Dias = 0.0002M;
        private const decimal taxaMultaSuperior5Dias = 0.05M;
        private const decimal taxaJurosMoraPorDiaAtrasoSuperior5Dias = 0.0003M;
        public ContaRepository(DeliverDbContext context) : base(context) { }

        public int CalcularDiasEmAtraso(DateTime dataVencimento, DateTime dataPagamento)
        {
            int dias = (dataPagamento.Subtract(dataVencimento)).Days;

            if (dias <  0)
                dias = 0;

            return dias;
        }

        public decimal CalcularMultaEJuros(int diasEmAtraso, decimal valorOriginal)
        {
            decimal valorCorrigido = 0;

            if (diasEmAtraso > 0 && diasEmAtraso <= 3)
            {
                valorCorrigido = valorOriginal + taxaMultaAte3Dias * valorOriginal;
                valorCorrigido += valorOriginal * (taxaJurosMoraPorDiaAtrasoAte3Dias * diasEmAtraso);
            }
            if (diasEmAtraso > 3 && diasEmAtraso < 5)
            {
                valorCorrigido = valorOriginal + taxaMultaSuperior3Dias * valorOriginal;
                valorCorrigido += valorOriginal * (taxaJurosMoraPorDiaAtrasoSuperior3Dias * diasEmAtraso);
            }
            if (diasEmAtraso > 5)
            {
                valorCorrigido = valorOriginal + taxaMultaSuperior5Dias * valorOriginal;
                valorCorrigido += valorOriginal * (taxaJurosMoraPorDiaAtrasoSuperior5Dias * diasEmAtraso);
            }
            if (valorCorrigido == 0)
            {
                valorCorrigido = valorOriginal;
            }
            return valorCorrigido;
        }

        public IEnumerable<Conta> ListarTodasAsContas()
        {
            return Db.conta
                     .AsNoTracking().ToList();
        }
    }
}
