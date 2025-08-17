using System.Reflection;

namespace CoreData.Services.Utils
{

    public static class MapeadorModels
    {
        public static TDestino Montar<TDestino, TOrigem>(TOrigem origem)
            where TDestino : new()
        {
            var destino = new TDestino();
            CopiarPropriedades(origem, destino);
            return destino;
        }

        public static void CopiarPropriedades<TOrigem, TDestino>(TOrigem origem, TDestino destino)
        {
            var propsDestino = typeof(TDestino).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var propsOrigem = typeof(TOrigem).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propDestino in propsDestino)
            {
                // Pula propriedades somente leitura
                if (!propDestino.CanWrite) continue;

                // Encontra propriedade equivalente na origem
                var propOrigem = propsOrigem.FirstOrDefault(p => p.Name == propDestino.Name && p.PropertyType == propDestino.PropertyType);
                if (propOrigem != null)
                {
                    var valor = propOrigem.GetValue(origem);
                    propDestino.SetValue(destino, valor);
                }
            }
        }
    }
}
