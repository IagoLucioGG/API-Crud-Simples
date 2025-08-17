using System;
using System.Collections.Concurrent;

namespace CoreData.Services.Autenticacao.LoginTentativas
{
    public static class LoginTentativas
    {
        private static readonly ConcurrentDictionary<string, (int Tentativas, DateTime? Bloqueio)> TentativasLogin = new();

        private const int MAX_TENTATIVAS = 10;
        private static readonly TimeSpan BLOQUEIO_TEMPO = TimeSpan.FromMinutes(15);

        public static bool PodeTentar(string login)
        {
            if (!TentativasLogin.ContainsKey(login))
            {
                TentativasLogin[login] = (0, null);
            }

            var (tentativas, bloqueio) = TentativasLogin[login];

            if (bloqueio.HasValue && bloqueio.Value > DateTime.UtcNow)
                return false;

            return true;
        }

        public static void RegistrarFalha(string login)
        {
            var (tentativas, bloqueio) = TentativasLogin.GetValueOrDefault(login, (0, null));
            tentativas++;

            if (tentativas >= MAX_TENTATIVAS)
            {
                bloqueio = DateTime.UtcNow.Add(BLOQUEIO_TEMPO);
                tentativas = 0;
            }

            TentativasLogin[login] = (tentativas, bloqueio);
        }

        public static void ResetarTentativas(string login)
        {
            TentativasLogin.TryRemove(login, out _);
        }
    }
}
