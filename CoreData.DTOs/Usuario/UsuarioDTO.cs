namespace CoreData.DTOs.Usuario
{
    public class UsuarioCriarDto
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public string LoginUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioLoginDto
    {
        public string LoginUsuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }

    public class UsuarioLerDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string LoginUsuario { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
