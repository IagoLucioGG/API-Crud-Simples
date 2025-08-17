using System.ComponentModel.DataAnnotations;

namespace CoreData.Models.Produtos
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public int CdChamada { get; set; }
        public string NmProduto { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}