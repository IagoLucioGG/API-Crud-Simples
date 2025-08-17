using System.ComponentModel.DataAnnotations;

namespace CoreData.Models.Clientes
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required]
        public string NmCliente { get; set; } = string.Empty;
        public string CpfCnpj { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao = DateTime.Now;
        public string Endereco { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TelefoneContato { get; set; } = string.Empty;

    }
}