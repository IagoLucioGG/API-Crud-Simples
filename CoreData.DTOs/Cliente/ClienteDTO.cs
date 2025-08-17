namespace CoreData.DTOs.Cliente
{
    public class ClienteDto
    {
        public string NmCliente { get; set; } = string.Empty;
        public string CpfCnpj { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public string Endereco { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TelefoneContato { get; set; } = string.Empty;
    }
}