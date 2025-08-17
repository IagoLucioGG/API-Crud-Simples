namespace CoreData.DTOs.Produtos
{
    public class ProdutoDTO
    {
        public int CdChamada { get; set; }
        public string NmProduto { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
    }
}