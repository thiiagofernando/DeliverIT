using System;

namespace DeliverIT.Business.Models
{
    public class Conta : Entity
    {
        public string Nome { get; set; }
        public int DiasEmAtraso { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorCorrigido { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
