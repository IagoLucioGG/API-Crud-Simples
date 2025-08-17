using System.ComponentModel.DataAnnotations;

namespace CoreData.Models.Vendas
{
    public class Venda
    {
        [Key]
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public int IdCliente { get; set; }
        public decimal VlVenda { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public DateTime DataCancelamento { get; set; }
        public int IdUsuario { get; set; }
    }
}