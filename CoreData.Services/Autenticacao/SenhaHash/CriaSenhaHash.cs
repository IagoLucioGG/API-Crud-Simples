using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CoreData.Services.Autenticacao.SenhaHash
{
    public static class SenhaHash
    {
        public static string GerarHash(string senha)
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        public static bool VerificarHash(string senha, string hashArmazenado)
        {
            var partes = hashArmazenado.Split('.');
            if (partes.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(partes[0]);
            string hashComparar = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashComparar == partes[1];
        }
    }
}
