namespace CoreData.DTOs.Vendas
{
    public class VendaDTO
    {
        public int IdProduto { get; set; }
        public int IdCliente { get; set; }
        public decimal VlVenda { get; set; }
        public string? Observacao { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public DateTime? DataCancelamento { get; set; }
        public int IdUsuario { get; set; }
    }
}