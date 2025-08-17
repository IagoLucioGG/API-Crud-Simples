using System.ComponentModel.DataAnnotations;

namespace CoreData.Models.Usuario
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required, MaxLength(50)]
        public string NomeUsuario { get; set; } = string.Empty;

        [Required, MaxLength(30)]
        public string LoginUsuario { get; set; } = string.Empty;

        [Required]
        public string SenhaHash { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataAtualizacao { get; set; }
    }
}
