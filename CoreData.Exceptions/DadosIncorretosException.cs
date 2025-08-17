using System;
using CoreData.Models.ResponseModel;

namespace CoreData.Exceptions
{
    public class DadosIncorretosException<T> : Exception
    {
        public DadosIncorretosException()
        {
        }

        public DadosIncorretosException(ResponseModel<T> response)
            : base(response.Mensagem)
        {
        }

        public DadosIncorretosException(ResponseModel<T> response, Exception innerException)
            : base(response.Mensagem, innerException)
        {
        }
    }
}
